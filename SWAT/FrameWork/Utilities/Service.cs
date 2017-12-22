namespace SWAT.FrameWork.Utilities
{
    using SWAT.Data;
    using SWAT.Configuration;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    public class Service
    {
        private DataManager _datamanager;
        //private TestStartInfo _teststartInfo;

        public Service(TestStartInfo c, DataManager datamanager)
        {           
            _datamanager = datamanager;
        }

        public Service()
        {
        }

        public string CopyRow()
        {
            try
            {
                Assert.IsTrue(_datamanager.CopyRow(_datamanager.RawData));
                return "Done";
            }
            catch
            {
                return "Failed";
            }
        }

        public string CopyColoumn(string option)
        {
            try
            {
                if(option == null)
                    Assert.IsTrue(_datamanager.CopyCol(_datamanager.RawData));
                if(option.ToUpper() == "RETAIN")
                    Assert.IsTrue(_datamanager.CopyCol_Retain(_datamanager.RawData));
                return "Done";
            }
            catch
            {
                return "Failed";
            }
        }

        public string ImportExcel()
        {
            try
            {
                DataManager newTestData = new DataManager();
                //newTestData.ImportExcel(_datamanager.RawData);
                //testConfig.testData = newTestData;
                newTestData = null;
                return "Done";
            }
            catch
            {
                return "Failed";
            }
        }
        
        public string OpenBrower()
        {
            Open(_datamanager.RawData.ToUpper());
            return "Done";
        }

        public void Open(string strBrowerName)
        {
            try
            {
                //switch (strBrowerName.ToUpper())
                //{
                //    case "CHROME":
                //        TestSettings.driver = new ChromeDriver(TestSettings.strDriversPath);
                //        break;
                //    case "IE":
                //        TestSettings.driver = new InternetExplorerDriver(TestSettings.strDriversPath);
                //        break;
                //    case "INTERNETEXPLORER":
                //        TestSettings.driver = new InternetExplorerDriver(TestSettings.strDriversPath);
                //        break;
                //    case "FIREFOX":
                //        TestSettings.driver = new FirefoxDriver();
                //        break;
                //}
                //MyLogger.Log("Selected Browser :=[" + strBrowerName + "]");
            }
            catch
            {
            }
        }

        internal string Wait(string time)
        {
            try
            {
                int waittime = int.Parse(time);
                System.Threading.Thread.Sleep(60000*waittime);
                return "Done";
            }
            catch
            {
                return "Failed";
            }
        }

        internal string CopyCell()
        {
            try
            {
                Assert.IsTrue(_datamanager.CopyCellBetweenTables());
                return "Done";
            }
            catch
            {
                return "Failed";
            }
        }
    }
}