using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using SWAT.Data;
using SWAT.Logger;
using OpenQA.Selenium;
using SWAT.Configuration;
using Keyword = SWAT.Applications.Bazooka.Keywords;
using ClawKeyword = SWAT.Applications.Claw.Keywords;

namespace SWAT.FrameWork.TestManager
{
    public class StepProcessor
    {
        public TestStartInfo TestStartInfo { get; set; }

        public event EventHandler<TestCaseStatusEventArgs> TestCaseStatusChanged;

        public event EventHandler<TCCompletionEventArgs> OnTestCaseCompletion;

        public  int TestCaseId { get; set; }
        public List<string> TestStepStatus { get; set; }
        public string TestCaseStatus { get; set; }
        public DataManager TestData { get; set; }
        public int TestCaseStartIndex { get; set; }
        public DataTable InstrSheet { get; set; }

        private bool ProcessStepRunOption(string option)
        {
            if (option == Constants.Option_Ignore)
            {
                TestStepStatus.Add(Constants.ResultStatus_Ignored);
                return false;
            }
            if (option == Constants.Option_DependsOnAbove &&
                TestStepStatus.Last() != Constants.ResultStatus_Passed)
            {
                TestStepStatus.Add(Constants.TCResultStatus_Skipped);
                return false;
            }
            return true;
        }

        public void TestStepProcessor(TestSteps testSteps)
        {
            try
            {
                if(!ProcessStepRunOption(testSteps.Option))
                {
                    return;
                }
                string actualResult = "NotExecuted";
                string stepResult = Constants.ResultStatus_Failed;
                int numberOfRetry = 1;
                if (testSteps.Action.ToUpper() == "TESTCASE.START")
                {
                    //ToDo : Testcases without id.
                    TestCaseStart(testSteps);
                    return;
                }
                if (testSteps.Action.ToUpper() == "TESTCASE.STOP")
                {
                    TestCaseStop(testSteps);
                    return;
                }
                string[] arraction = testSteps.Action.Split('.');
                TestData.GetData(testSteps.DataReference);
                do
                {
                    if (arraction[0] == Constants.Bazooka)
                    {
                        var keywords = new Keyword(TestStartInfo);
                        actualResult = keywords.ExecuteTestStep(testSteps.Action, TestData);
                    }
                    else
                    {
                        var KeyWords = new ClawKeyword(TestStartInfo, testSteps.DataReference);
                        actualResult = KeyWords.ExecuteTestStep(testSteps.Action, TestData);
                    }
                    if (actualResult.ToUpper().Trim() != testSteps.ExpectedResult)
                    {
                        MyLogger.Log("Refer " + TakeScreenshot(TestStartInfo.ResultPath + testSteps.Action.Replace('.', '_')));
                        stepResult = Constants.ResultStatus_Failed;
                    }
                    else
                    {
                        stepResult = Constants.ResultStatus_Passed;
                    }
                    numberOfRetry--;
                }
                while (stepResult == Constants.ResultStatus_Failed
                        && testSteps.Option == Constants.Option_Retry
                        && numberOfRetry >= 0);
                testSteps.ActualResult = actualResult;
                TestStepStatus.Add(stepResult);
            }
            catch
            {
                TestStepStatus.Add(Constants.ResultStatus_Failed);
            }
        }

        private void TestCaseStart(TestSteps testSteps)
        {
            TestCaseId = Int32.Parse((testSteps.DataReference.Split('_')[0]));
            TestStepStatus.Clear();
            TestCaseStartIndex = testSteps.Index;
            TestStepStatus.Add(Constants.ResultStatus_Passed);
            testSteps.ActualResult = "";
        }

        private void TestCaseStop(TestSteps testSteps)
        {
            string stepResult = UpdateTestCaseResult();
            ShareTestCaseResult(stepResult);
            InstrSheet.Rows[TestCaseStartIndex][Constants.Column_Status] = stepResult;
            testSteps.ActualResult = "";
            TestStepStatus.Add(stepResult);
        }

        private string UpdateTestCaseResult()
        {
            string testCaseStatus = Constants.ResultStatus_Passed;
            if (TestStepStatus.Contains(Constants.ResultStatus_Failed))
                testCaseStatus = Constants.ResultStatus_Failed;
            return testCaseStatus;
        }

        private void ShareTestCaseResult(string testCaseStatus)
        {
            if (OnTestCaseCompletion != null)
                OnTestCaseCompletion(null, new TCCompletionEventArgs(TestCaseId, testCaseStatus));
        }

        private string TakeScreenshot(String strFileName)
        {
            try
            {
                if (TestStartInfo.Driver == null)
                {
                    return "ExecutionNotStarted";
                }
                DateTime time = DateTime.Now;
                string format = "yyyy_MM_dd_HHMM";
                try
                {
                    Screenshot ss = ((ITakesScreenshot)TestStartInfo.Driver).GetScreenshot();
                    string strScreenshotpath = strFileName + "_" + time.ToString(format) + ".png";
                    ss.SaveAsFile(strScreenshotpath, System.Drawing.Imaging.ImageFormat.Png);
                    return strScreenshotpath;
                }
                catch
                {
                    var screenshot = ((ITakesScreenshot)TestStartInfo.Driver).GetScreenshot();
                    byte[] imageBytes = Convert.FromBase64String(screenshot.ToString());
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(strFileName, FileMode.Append, FileAccess.Write)))
                    {
                        bw.Write(imageBytes);
                        bw.Close();
                    }
                    return "";
                }
            }
            catch (Exception ex)
            {
                MyLogger.Log(ex.Message);
                return "";
            }
        }
    }
}
