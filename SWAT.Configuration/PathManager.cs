using System;

namespace SWAT.Configuration
{
    public class PathManager
    {
        private string _environment = ConfigManager.EnironmentName;
        private string _browser = null;
        private string _application = null;        
        private string _testcasename = null;
        private string _testsuitename = null;
        private string _baseurl = ConfigManager.Link;
        private string _dataPath = ConfigManager.TestDataFolder;
        private string _resultspath = ConfigManager.TestResultFolder;
        private string _resultpath = null;
        private string _suitepath = ConfigManager.TestSuiteFolder;
        public  string DriversPath = ConfigManager.TestDriver;

        public SelectedSettings Settings { get; set; }

        public PathManager(string app, string env, string brwr, string suite)
        {
            _environment = env;
            _browser = brwr;
            _application = app;
            _testcasename = suite;
            //_baseurl = GetURL(env);
        }

        public PathManager(SelectedSettings settings)
        {
            _environment = settings.Environment;
            _browser = settings.Browser;
            _application = settings.Application;            
            try
            {
                string[] testsuite = settings.Suite.Split('\\');
                _testsuitename = testsuite[1];
                _testcasename = testsuite[2];
            }
            catch
            {

            }           
            //_baseurl = GetURL(_environment);
            Settings = settings;
        }

        public string ResultPath
        {
            get
            {
                return GetResultPath();
            }
            set
            {
                _resultpath = value;
            }
        }

        public string SuitePath
        {
            get
            {
                return _suitepath +"\\"+_testsuitename+"\\"+ _testcasename + ".xlsx";
            }
        }

        public string DataPath
        {
            get
            {
                return _dataPath + "\\" + _environment + "\\" + _testsuitename + "\\Data_" + _testcasename + ".xls";
            }
        }

        public string BaseURL
        {
            get { return _baseurl; }
        }

        public string GlobalUserData
        {
            get
            {
                return this._dataPath + "\\" + _environment + "\\" + "Data_Users.xls";
            }
        }

        private string GetResultPath()
        {
            if (_resultpath == null)
            {
                // ToDo : Check if this path is already exists then suffix _01
                string format = "yyyy_MM_dd_HHMM";
                _resultpath = _resultspath + _environment + @"\" + _testcasename.Replace("TESTSUITE","") + "_" + _browser + "_" + DateTime.Now.ToString(format) + @"\";
                if (!System.IO.Directory.Exists(_resultpath))
                    System.IO.Directory.CreateDirectory(_resultpath + "_01");
                return _resultpath;
            }
            return _resultpath;
        }
    }
}