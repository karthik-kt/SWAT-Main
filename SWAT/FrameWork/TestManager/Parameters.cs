using OpenQA.Selenium;
using SWAT.Configuration;

namespace SWAT.FrameWork.TestManager
{
    public class Parameters
    {
        private string _application;
        private string _environment;
        private string _browser;
        private string _suite;
        private string _resultpath;
        private string _suitepath;
        private string _datapath;
        private string _baseurl;
        private IWebDriver _driver;

        public Parameters(PathManager paths, WebDriver mydriver)
        {
            _resultpath = paths.ResultPath;
            _suitepath = paths.SuitePath;
            _datapath = paths.DataPath;
            _baseurl = paths.BaseURL;
            _driver = mydriver.Driver;
            // Application is launched
            _driver.Url = _baseurl;
            _application = paths.Settings.Application;
            _browser = paths.Settings.Browser;
            _environment = paths.Settings.Environment;
            _suite = paths.Settings.Suite;
        }

        public string ResultPath
        {
            get
            {
                return _resultpath;
            }
        }

        public string SuitePath
        {
            get
            {
                return _suitepath;
            }
        }

        public string DataPath
        {
            get
            {
                return _datapath;
            }
        }

        public string BaseURL
        {
            get
            {
                return _baseurl;
            }
        }

        public IWebDriver Driver
        {
            get
            {
                return _driver;
            }
        }

        public string Application
        {
            get
            {
                return _application;
            }
        }

        public string Environment
        {
            get
            {
                return _environment;
            }
        }

        public string Browser
        {
            get
            {
                return _browser;
            }
        }

        public string Suite
        {
            get
            {
                return _suite;
            }
        }
    }
}