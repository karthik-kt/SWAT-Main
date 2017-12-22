using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

namespace SWAT.Configuration
{

    [XmlRootAttribute("ConfigManager", Namespace = "http://www.cpandl.com", IsNullable = false)]
    public class Settings
    {
        public string TestFolder;
        public string TestSuiteFolder;
        public string TestDataFolder;
        public string TestResultFolder;
        public string TestDriver;
        public string BazookaRepositoryNetworkDir;
        public string BazookaReposityPath;
        public string BazookaDataFilesDir;
        public string BazookaTestFile;
        public string BazookaTestPlan;
        public string BazookaTeamProject;
        [XmlArrayAttribute("Items")]
        public Enironments[] Enironments;
    }

    public class Enironments
    {
        public string EnironmentName;
        public string Link;
        public string BazookaWorkingDirectory;
        public string ConnectionString;
    }

    public static class ConfigManager
    {
        public static string TestFolder { get; set; }
        public static string TestSuiteFolder { get; set; }
        public static string TestDataFolder { get; set; }
        public static string TestResultFolder { get; set; }
        public static string TestDriver { get; set; }
        public static string BazookaRepositoryNetworkDir { get; set; }
        public static string BazookaReposityPath { get; set; }
        public static string BazookaDataFilesDir { get; set; }
        public static string BazookaTestFile { get; set; }
        public static string BazookaTestPlan { get; set; }
        public static string BazookaTeamProject { get; set; }
        public static string EnironmentName { get; set; }
        public static string Link { get; set; }
        public static string BazookaWorkingDirectory { get; set; }
        public static string ConnectionString { get; set; }
        public static List<string> ListOfEnvironments { get; set; }
    }

    public class UpdateConfig
    {
        private static Settings _settings { get; set; }
        private static string configfileName { get; set; }

        public static void LoadConfig()
        {
            configFileSelector();
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            serializer.UnknownNode += new
            XmlNodeEventHandler(serializer_UnknownNode);
            serializer.UnknownAttribute += new
            XmlAttributeEventHandler(serializer_UnknownAttribute);
            FileStream fs = new FileStream(configfileName, FileMode.Open);
            _settings = (Settings)serializer.Deserialize(fs);
            ConfigManager.TestFolder = _settings.TestFolder;
            ConfigManager.TestSuiteFolder = _settings.TestSuiteFolder;
            ConfigManager.TestDataFolder = _settings.TestDataFolder;
            ConfigManager.TestResultFolder = _settings.TestResultFolder;
            ConfigManager.TestDriver = _settings.TestDriver;
            ConfigManager.BazookaRepositoryNetworkDir = _settings.BazookaRepositoryNetworkDir;
            ConfigManager.BazookaReposityPath = _settings.BazookaReposityPath;
            ConfigManager.BazookaDataFilesDir = _settings.BazookaDataFilesDir;
            ConfigManager.BazookaTestFile = _settings.BazookaTestFile;
            ConfigManager.BazookaTestPlan = _settings.BazookaTestPlan;
            ConfigManager.BazookaTeamProject = _settings.BazookaTeamProject;
            ListOfEnvironment();
            fs.Close();
        }

        private static void ListOfEnvironment()
        {
            Enironments[] enironmentsItems = _settings.Enironments;
            ConfigManager.ListOfEnvironments = new List<string>();
            foreach (var item in enironmentsItems)
            {
                ConfigManager.ListOfEnvironments.Add(item.EnironmentName);
            }
            ConfigManager.ListOfEnvironments.Sort();
        }

        private static void configFileSelector()
        {
            try
            {
                if (File.Exists(@"C:\SWAT\Config.xml"))
                {
                    configfileName = @"C:\SWAT\Config.xml";
                }
                else
                {
                    configfileName = @"\\Gxstorage\gxdata\SmokeTests\Config\Config.xml";
                }
            }
            catch
            {

            }
        }

        static public void SetEnivromnet(string environmentname)
        {
            Enironments[] enironmentsItems = _settings.Enironments;
            foreach (var item in enironmentsItems)
            {
                if (item.EnironmentName.ToUpper() == environmentname.ToUpper())
                {
                    ConfigManager.EnironmentName = item.EnironmentName;
                    ConfigManager.Link = item.Link;
                    ConfigManager.BazookaWorkingDirectory = item.BazookaWorkingDirectory;
                    ConfigManager.ConnectionString = item.ConnectionString;
                    break;
                }
            }
        }

        static private void serializer_UnknownNode(object sender, XmlNodeEventArgs e)
        {
            //Console.WriteLine("Unknown Node:" + e.Name + "\t" + e.Text);
        }

        static private void serializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            //System.Xml.XmlAttribute attr = e.Attr;
            //Console.WriteLine("Unknown attribute " + attr.Name + "='" + attr.Value + "'");
        }

    }
}
