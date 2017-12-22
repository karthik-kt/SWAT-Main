using System;
using System.IO;
using System.Linq;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using SWAT.Logger;
using SWAT.FrameWork.TestManager;
using SWAT.Configuration;
using System.Collections.Generic;
using Settings = SWAT.Configuration.SelectedSettings;

namespace FireInTheHole
{
    public partial class FireInTheHole : Form
    {


        //bool isPaused;
        private Settings _settings;
        private static string fileType = ".xlsx";

        public FireInTheHole()
        {
            InitializeComponent();            
        }

       private void Form1_Load(object sender, EventArgs e)
        {
            OnLoad();
            //string[] listoftestsuites = getxlsfilesnamefrmpath(ConfigManager.TestSuiteFolder);
            //listoftestsuites = cleanfilename(listoftestsuites);
            //System.Object[] ItemObject = stringtosysobj(listoftestsuites);
        }

        #region Load and refresh winform
        public void OnLoad()
        {
            UpdateConfig.LoadConfig();
            ListDirectory(treeView1, ConfigManager.TestSuiteFolder);

            comboBox4.DataSource = ConfigManager.ListOfEnvironments;
            comboBox4.SelectedItem = ConfigManager.ListOfEnvironments[0];
            comboBox2.SelectedItem = "";
            comboBox2.SelectedItem = "CHROME";
            //UpdateConfig.SetEnivromnet(comboBox4.SelectedItem.ToString());
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            OnLoad();
        }

        #endregion

        #region LAN files to list option

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
            {
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            }

            foreach (var file in directoryInfo.GetFiles())
            {
                string filename = file.Name;
                filename = filename.Replace(fileType, "");
                filename = filename.Replace("TestSuite_", "");
                directoryNode.Nodes.Add(new TreeNode(filename));
            }
            return directoryNode;
        }

        private string[] getxlsfilesnamefrmpath(string filpath)
        {
            try
            {
                return Directory.GetFiles(filpath, "*"+fileType)
                                       .Select(path => Path.GetFileName(path))
                                       .ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string[] cleanfilename(string[] listoffliename)
        {
            for (int iloop = 0; iloop < listoffliename.Count(); iloop++)
            {
                listoffliename[iloop] = listoffliename[iloop].Replace(fileType, "");
                listoffliename[iloop] = listoffliename[iloop].Replace("TestSuite_", "").ToUpper();
            }
            return listoffliename;
        }

        private System.Object[] stringtosysobj(string[] stringarray)
        {
            try
            {
                System.Object[] ItemObject = new System.Object[stringarray.Count()];
                for (int i = 0; i < stringarray.Count(); i++)
                {
                    ItemObject[i] = stringarray[i];
                }
                return ItemObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
            treeView.Sort();
        }

        #endregion 
        
        #region Event - Delegates - BackGround

        private delegate void SetTextCallback(object sender, LogEvenArgs e);

        private void SetText(object sender, LogEvenArgs e)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                this.textBox1.Text = e.Log;
            }
        }

        private delegate void EnableButtonCallback(bool text);

        private void EnableButton(bool text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox1.InvokeRequired)
            {
                EnableButtonCallback d = new EnableButtonCallback(EnableButton);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                //this.button1.Enabled = text;
            }
        }

        public void StartExecution(BackgroundWorker instance, DoTestEventArgs e)
        {
            TestSuiteManager TestManager = new TestSuiteManager();
            Settings setting = e.Settings;
            TestManager.StartRun(_settings);
            TestManager = null;
        }

        void tm_ExecutionCompleted(object sender, EventArgs e)
        {
            EnableButton(true);
        }

        #endregion

        #region Mouse menu options.
        //Excuting the tests
        private void Execute()
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            UpdateConfig.SetEnivromnet(comboBox4.SelectedItem.ToString());
            try
            {
                Task t1 = Task.Factory.StartNew(() =>
                {
                    TestSuiteManager TestManager = new TestSuiteManager();
                    TestManager.StartRun(_settings);
                    //EnableButton(true);
                    //TCResult TCResult = new TCResult(TestManager.TestStartInfo);
                    //TCResult.PublicResult();
                });
                Task t2 = Task.Factory.StartNew(() =>
                {
                    MyLogger.LogAdded += SetText;
                });
            }
            catch
            {
            }
        }

        //Excuting the tests
        private void mnuExecute_Click(object sender, System.EventArgs e)
        {
            TreeNode node = this.treeView1.SelectedNode;
            _settings.Application = "CLAW";
            _settings.Environment = comboBox4.Text.ToUpper().Trim();
            _settings.Browser = comboBox2.Text.ToUpper().Trim();
            _settings.Suite = node.FullPath.ToUpper().Trim();
            _settings.PublishResult = TFSPublish.Checked;
            textBox1.Text = null;
            Execute();
        }

        //Open testsuite excel
        private void mnuOpen_Click(object sender, System.EventArgs e)
        {
            TreeNode node = this.treeView1.SelectedNode;
            string fileloaction = ConfigManager.TestFolder + node.FullPath;
            Process proc = new Process();
            proc.StartInfo.FileName = fileloaction + fileType;
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
        }
        
        //Open data excel
        private void openDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode node = this.treeView1.SelectedNode;
                string env = comboBox4.Text;
                string[] suitepath = nodesplitter(node.FullPath);
                string datapath = "Data_"+ suitepath[2]+ ".xls";
                string fileloaction = Path.Combine(ConfigManager.TestDataFolder, env, suitepath[1], datapath);
                Process proc = new Process();
                proc.StartInfo.FileName = fileloaction;
                proc.StartInfo.UseShellExecute = true;
                proc.Start();
            }
            catch
            {

            }
        }

        //Data folder
        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode node = this.treeView1.SelectedNode;
                string env = comboBox4.Text;
                string[] suitepath = nodesplitter(node.FullPath);
                string fileloaction = Path.Combine(ConfigManager.TestDataFolder, env, suitepath[1]);
                Process proc = new Process();
                proc.StartInfo.FileName = fileloaction;
                proc.StartInfo.UseShellExecute = true;
                proc.Start();
            }
            catch
            {

            }

        }

        //Testsuite folder
        private void openTestFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode node = this.treeView1.SelectedNode;
                string env = comboBox4.Text;
                string[] suitepath = nodesplitter(node.FullPath);
                string fileloaction = Path.Combine(ConfigManager.TestFolder, node.FullPath.ToString());
                Process proc = new Process();
                proc.StartInfo.FileName = fileloaction;
                proc.StartInfo.UseShellExecute = true;
                proc.Start();
            }
            catch
            {

            }
        }

        private void treeView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Show menu only if the right mouse button is clicked.
            if (e.Button == MouseButtons.Right)
            {
                Point p = new Point(e.X, e.Y);
                TreeNode node = treeView1.SelectedNode;
                if (node != null)
                {
                    int test = node.Level;
                    if (node.Level == 2) // Testcase level
                    {
                        mnuTextFile.Show(treeView1, p);
                    }
                    if (node.Level == 1) // Testcase level
                    {
                        contextMenuLevel2.Show(treeView1, p);
                    }

                }
            }
        }

        private string[] nodesplitter(string path)
        {
            return path.Split('\\');
        }

        #endregion

        private void addTestSuiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ShowDialog.
            //   ShowDialog(new NewTestSuite());
        }

        private void executeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> bulksuites = new List<string>();
                TreeNode SelectedNode = this.treeView1.SelectedNode;
                foreach(TreeNode node in SelectedNode.Nodes )
                {
                    bulksuites.Add(node.FullPath.ToString());
                }
                _settings.Application = "CLAW";
                _settings.Environment = comboBox4.Text.ToUpper().Trim();
                _settings.Browser = comboBox2.Text.ToUpper().Trim();
                _settings.BulkSuite = bulksuites;
                _settings.Suite = null;
                textBox1.Text = null;
                Execute();
            }
            catch
            {

            }
        }

        private void mnuTextFile_Opening(object sender, CancelEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    public class DoTestEventArgs : EventArgs
    {
        public Settings Settings { get; set; }
        public DoTestEventArgs(Settings settings)
        {
            Settings = settings;
        }
    }

}