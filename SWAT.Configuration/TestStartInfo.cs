using OpenQA.Selenium;
using System.Data;

namespace SWAT.Configuration
{

    public class TestStartInfo : ITestStartInfo
    {
        public TestStartInfo(PathManager paths, WebDriver mydriver)
        {
            ResultPath = paths.ResultPath;
            SuitePath = paths.SuitePath;
            DataPath = paths.DataPath;
            BaseURL = paths.BaseURL;
            Driver = mydriver.Driver;
            Application = paths.Settings.Application;
            Browser = paths.Settings.Browser;
            Environment = paths.Settings.Environment;
            Suite = paths.Settings.Suite;
        }

        //Craeted for testing
        public TestStartInfo()
        {

        }

        public string ResultPath { get; private set; }

        public string SuitePath { get; set; }

        public string DataPath { get; private set; }

        public string BaseURL { get; private set; }

        public IWebDriver Driver { get; private set; }

        public string Application { get; private set; }

        public string Environment { get; private set; }

        public string Browser { get; private set; }

        public string Suite { get; private set; }

        public DataTable ResultSheet { get; set; }

        public void Dispose()
        {
            if (this.Driver != null)
            {
                this.Driver.Dispose();
            }
}
    }
}
