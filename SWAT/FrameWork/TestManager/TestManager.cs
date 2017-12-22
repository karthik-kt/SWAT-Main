using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Linq;
using SWAT.Data;
using SWAT.Logger;
using SWAT.Configuration;
using System.Collections.Generic;
using BazookaKeyword = SWAT.Applications.Bazooka.Keywords;
using ClawKeyword = SWAT.Applications.Claw.Keywords;
using TestResult = SWAT.FrameWork.Results.TestResult;
using System.Text.RegularExpressions;

namespace SWAT.FrameWork.TestManager
{
    public class TestSuiteManager
    {
        public event EventHandler<TestCaseStatusEventArgs> TestCaseStatusChanged;

        private TestStartInfo _TestStartInfo { get; set; }

        private DataTable _InstrSheet { get; set; }

        private DataManager _Testdata { get; set; }
        
        private bool TfsPublish = false;
        
        public TestSuiteManager()
        {
            _InstrSheet = null;
            _Testdata = new DataManager();
        }

        public bool StartRun(SelectedSettings settings)
        {
            try
            {
                List<string> CompletedTestResultPath = new List<string>();
                MyLogger.ClearAllMsg();
                if (settings.Suite != null)
                {
                    Assert.IsTrue(Setup(settings));
                    MyLogger.Log("********************************EXECUTION STARTED*********************************");
                    MyLogger.clearMsg();
                    Driver();
                    MyLogger.Log("********************************EXECUTION COMPLETED********************************");
                    settings.Suite = null;
                }

                if (settings.BulkSuite != null)
                {
                    foreach (string suite in settings.BulkSuite)
                    {
                        MyLogger.ClearAllMsg();
                        if (_TestStartInfo != null)
                            CompletedTestResultPath.Add(_TestStartInfo.ResultPath);
                        CompletedResultLog(CompletedTestResultPath);
                        settings.Suite = suite;
                        Assert.IsTrue(Setup(settings));
                        MyLogger.Log("********************************EXECUTION STARTED*********************************");
                        MyLogger.clearMsg();
                        Driver();
                        MyLogger.Log("********************************EXECUTION COMPLETED********************************");
                    }
                }
                settings.BulkSuite = null;
                return true;
            }
            catch 
            {
                return false;
            }
        }
                      
        private bool Setup(SelectedSettings settings)
        {
            try
            {
                TfsPublish = settings.PublishResult;
                PathManager Paths = new PathManager(settings);
                WebDriver MyDriver = new WebDriver(settings.Browser, Paths.DriversPath);
                _TestStartInfo = new TestStartInfo(Paths, MyDriver);
                MyLogger.Log("********************************TEST SETTINGS********************************");
                MyLogger.Log("Browser:[ " + _TestStartInfo.Browser + " ]");
                MyLogger.Log("Environment:[ " + _TestStartInfo.Environment + " ]");
                MyLogger.Log("TestSuite:[ " + _TestStartInfo.Suite + " ]");
                MyLogger.Log("Driver:[ " + _TestStartInfo.Driver + " ]");
                MyLogger.Log("URL:[ " + _TestStartInfo.BaseURL + " ]");
                _InstrSheet = _Testdata.ImportWorkSheet("Instructions", _TestStartInfo.SuitePath);
                MyLogger.Log("FilePath:[ " + _TestStartInfo.SuitePath + " ]");
                if (!_Testdata.ImportExcel(_TestStartInfo.DataPath, ".xls"))
                    _Testdata.ImportExcel(_TestStartInfo.SuitePath, ".xlsx");
                _Testdata.AddNewDataSheet("Users", Paths.GlobalUserData);
                MyLogger.Log("DataFilePath:[ " + _TestStartInfo.DataPath + " ]");
                return true;
            }
            catch (Exception ex)
            {
                MyLogger.Log(ex.Message);
                MyLogger.Log("Execution stopped due to error in initial setups...");
                return false;
            }
        }

        private void Driver()
        {
            try
            {
                string testCaseStatus = Constants.ResultStatus_Passed;
                StepProcessor stepprocessor = new StepProcessor();
                stepprocessor.TestStepStatus = new List<string>();
                stepprocessor.TestData = _Testdata;
                stepprocessor.InstrSheet = _InstrSheet;
                stepprocessor.TestStartInfo = _TestStartInfo;
                //ToDo: This is temp solution till we execute from TFS directly.
                stepprocessor.OnTestCaseCompletion += TestCaseCompletion;
                TestSteps teststeps = new TestSteps();
                for (int index = 0; index <= (_InstrSheet.Rows.Count-1); index++)
                {                    
                    teststeps.Index = index; 
                    teststeps.Action = _InstrSheet.Rows[index][Constants.Column_Actions].ToString().ToUpper().Trim();
                    teststeps.DataReference = _InstrSheet.Rows[index][Constants.Column_Data].ToString().Trim();
                    teststeps.Option = _InstrSheet.Rows[index][Constants.Column_Option].ToString().ToUpper().Trim();
                    teststeps.ExpectedResult = _InstrSheet.Rows[index][Constants.Column_ExpectedResult].ToString().ToUpper().Trim();                                        
                    stepprocessor.TestStepProcessor(teststeps);
                    _InstrSheet.Rows[index][Constants.Column_ActulaResult] = teststeps.ActualResult;
                    _InstrSheet.Rows[index][Constants.Column_Status] = stepprocessor.TestStepStatus.Last();
                    _InstrSheet.Rows[index][Constants.Column_Comment] = MyLogger.Message();
                }
                ComplileResult(_InstrSheet);
            }
            catch (Exception ex)
            {
                if (_TestStartInfo.Driver != null) _TestStartInfo.Driver.Quit();
                MyLogger.Log("Exception : [ " + ex.Message + " ]");
                _TestStartInfo.Dispose();
                CreateLog(_TestStartInfo.ResultPath, MyLogger.completeMessage());
            }
        }

        private void ComplileResult(DataTable instrutionsheet)
        {
            MyLogger.Log("Generating results.");
            _TestStartInfo.Driver.Quit();
            _TestStartInfo.Driver.Dispose();
            using (TestResult testResult = new TestResult(this._TestStartInfo))
            {
                testResult.AddSheets(instrutionsheet);
                testResult.ColorMe();
                testResult.ExportResults();
            }
            _TestStartInfo.ResultSheet = instrutionsheet;
            CreateLog(_TestStartInfo.ResultPath, MyLogger.completeMessage());
        }

        private void CreateLog(string path, string content)
        {
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            System.IO.File.WriteAllText(path + @"\TestLog.txt", content);
        }

        private void CompletedResultLog(List<string> CompletedTestResultPath)
        {
            try
            {
                MyLogger.Log("Completed Test Suites Results:");
                foreach (var reusltpath in CompletedTestResultPath)
                {
                    MyLogger.Log(reusltpath);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void TestCaseCompletion(object sender, TCCompletionEventArgs e)
        {
            TestCaseStatusChanged(null, new TestCaseStatusEventArgs(e.TestCaseId, e.TestCaseStatus, _TestStartInfo.ResultPath));
        }

    }

    public class TCCompletionEventArgs : EventArgs
    {
        public TCCompletionEventArgs(int testCaseId, string testCaseStatus)
        {
            TestCaseStatus = testCaseStatus;
            TestCaseId = testCaseId;
        }
        
        public string TestCaseStatus { get; set; }
        public int TestCaseId { get; set; }
    }

}