using System;
using System.Linq;
using System.Data;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using SWAT.FrameWork.TestManager;
using SWAT.Configuration;
using Settings = SWAT.Configuration.SelectedSettings;
using SWAT.TFSClient;
using Newtonsoft.Json;
using SWAT.TFSModels;
using SWAT.TestManager;
namespace SWAT.Agent
{
    public partial class Agent : Form
    {
        private TfsHandler _TfsInformer;

        private string SelectedTestSuite { get { return this.comboBoxTFSTestSuites.SelectedValue.ToString(); } }

        private string SelectedProject { get { return this.comboBoxTFSProject.SelectedValue.ToString(); } }

        public Agent()
        {
            _TfsInformer = new TfsHandler();
            InitializeComponent();
            buttonAssign.Enabled = false;
            comboBoxTFSProject.Enabled = false;
            comboBoxTFSTestSuites.Enabled = false;
            LoadProjectList();
        }

        private async void LoadProjectList()
        {
            this.comboBoxTFSProject.AutoCompleteCustomSource = null;
            this.comboBoxTFSProject.DataSource = await _TfsInformer.GetProjects();
            this.comboBoxTFSProject.Enabled = true;
        }

        private async Task LoadTestSuiteList()
        {
            string selectedProject = this.comboBoxTFSProject.SelectedValue.ToString();
            this.comboBoxTFSTestSuites.DataSource = await _TfsInformer.GetPlans(selectedProject);
        }

        private async void comboBoxTFSProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedProject = this.comboBoxTFSProject.SelectedValue.ToString();
            this.comboBoxTFSTestSuites.DataSource = await _TfsInformer.GetPlans(selectedProject);
            comboBoxTFSTestSuites.Enabled = true;
        }

        private void comboBoxTFSTestSuites_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonAssign.Enabled = true;
        }

        private async void AssignButton_Click(object sender, EventArgs e)
        {
            buttonAssign.Enabled = false;
            comboBoxTFSProject.Enabled = false;
            comboBoxTFSTestSuites.Enabled = false;
            //Transmitter.TestCaseStatusChanged += DispatchTestCaseStatus;
            string selectedTestSuiteId = Regex.Match(SelectedTestSuite, @"(\d+)").ToString();
            Transmitter Transmitter = new Transmitter(SelectedProject, selectedTestSuiteId);
            string Completed = await Transmitter.ExecuteSuites();
            //while (await GetAvailableTestSuites())
            //{
            //    await GetAvailableTestSuites();
            //    CurrentTestSuiteName = AvilableTestSuiteIds.FirstOrDefault().Split(':').LastOrDefault();
            //    CurrentTestSuiteId = int.Parse(AvilableTestSuiteIds.FirstOrDefault().Split(':').FirstOrDefault());
            //    SetupSettings();
            //    await _Tfs.UpdateWorkItemById(CurrentTestSuiteId, "/fields/System.State", "In Progress");
            //    StartExecution();
            //}
            buttonAssign.Enabled = true;
            comboBoxTFSProject.Enabled = true;
            comboBoxTFSTestSuites.Enabled = true;
            MessageBox.Show("(1)None of Test suite state = 'In Planning' (2)Test Suites should have valid SWAT testcase ");
        }

        //public async void UpdateTestCaseOutCome()
        //{
        //    _Tfs = new TfsHelper();
        //    TestPoints TestPoints = new TestPoints() { outcome = "Passed" };
        //    string jsonstring = JsonConvert.SerializeObject(TestPoints);
        //    await _Tfs.SetOutCome("QA","572884", "582146", "2304187", jsonstring);
        //}

        //public async void CreateTestRun()
        //{
        //    TestRun testrun = new TestRun()
        //    {
        //        name = "SWAT Automated Tests",
        //        plan = new Plan() { id = "572884" },
        //        pointIds = new List<string> { "2304187" }
        //    };
        //    string jsonString = JsonConvert.SerializeObject(testrun);
        //    await _Tfs.CreateTestRun("QA", jsonString);
        //}

        //private async Task<bool> GetAvailableTestSuites()
        //{
        //    try
        //    {
        //        //_Tfs.TeamProject = SelectedProject;
        //        string selectedTestSuiteId = Regex.Match(SelectedTestSuite, @"(\d+)").ToString();
        //        JObject projectsJObject = JObject.Parse(await _Tfs.GetTestSuites(SelectedProject,selectedTestSuiteId));
        //        AvilableTestSuiteIds = projectsJObject.SelectToken("value").
        //                                                Where(p => Int32.Parse(p["testCaseCount"].ToString()) > 0 && p["state"].ToString() == "In Planning").
        //                                                Select(p => p["id"].ToString() + ":" + p["name"].ToString()).
        //                                                ToArray();
        //        return (AvilableTestSuiteIds.Count != 0);
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //private void StartExecution()
        //{
        //    try
        //    {
        //        UpdateConfig.LoadConfig();
        //        UpdateConfig.SetEnivromnet("STAGING");
        //        //TestManager TestManager = new TestManager();
        //        //TestManager.TestCaseStatusChanged += UpdateTestCaseStatus;
        //        //TestManager.StartRun(_settings);
        //    }
        //    catch
        //    {

        //    }
        //}

        //private void UpdateTestCaseStatus(object sender, TestCaseStatusEventArgs e)
        //{
        //    MessageBox.Show("TestCase" + e.TestCaseId + ":" + e.TestCaseStatus + ":" + e.TestResultPath);
        //}

        //private void DispatchTestCaseStatus(object sender, TestCaseStatusEventArgs e)
        //{

        //}

        //private void SetupSettings()
        //{
        //    _settings.Application = "CLAW";
        //    _settings.Environment = "STAGING";
        //    _settings.Browser = "CHROME";
        //    _settings.Suite = @"\SMOKETESTS\" + CurrentTestSuiteName;
        //    _settings.PublishResult = false;
        //}
    }

}
