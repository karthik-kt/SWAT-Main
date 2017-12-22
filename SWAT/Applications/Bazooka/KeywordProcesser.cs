using System;
using System.Collections.Generic;
using System.IO;
using SWAT.Configuration;
using SWAT.Logger;
using TestProcessor;
using WatchDogs.Models.StepTest;
using WatchDogs.Models;
using LegionFramework.Extensions;
using WTFTestManager = TestProcessor.TestManager;

namespace SWAT.Applications.Bazooka
{
    public class KeywordProcesser
    {

        private string _resultlog = null;

        private string _result = null;

        private string _environment = null;

        private int? _startingstep = null;

        private int? _endingstep = null;

        private bool _startnewinstence = false;

        private bool _shutdownoncompletion = false;

        public int? StartingStep
        {
            set
            {
                _startingstep = value;
            }
        }

        public int? EndingStep
        {
            set
            {
                _endingstep = value;
            }
        }

        public bool StartNewInstence
        {
            set
            {
                _startnewinstence = value;
            }
        }

        public bool ShutdownOnCompletion
        {
            set
            {
                _shutdownoncompletion = value;
            }
        }

        public string Result
        {
            get { return _result; }
            set { _result = value; }
        }

        public string Log
        {
            get { return _resultlog; }
            set { _resultlog = value; }
        }

        public KeywordProcesser(TestStartInfo teststartinfo)
        {
            _environment = teststartinfo.Environment;
        }

        public string Execute(string keyword, Dictionary<string, string> testparameters)
        {
            try
            {
                WTFTestManager TestManager = new WTFTestManager();
                TestRunStartInfo TestRunStartInfo = ConvertKeyword(keyword, testparameters);
                TestManager.RunTest(TestRunStartInfo);
                ResultManager ResultManager = TestManager.ResultManager;
                ProccessResult(ResultManager);
                return ResultManager.Result.ToString();
            }
            catch (Exception ex)
            {
                MyLogger.Log(ex.Message);
                return "Failed";
            }
        }

        public bool ShouldCreateResultFiles { get; set; }

        private TestRunStartInfo ConvertKeyword(string keyword, Dictionary<string, string> testparameters)
        {
            TestRunStartInfo TestRunStartInfo = new TestRunStartInfo();
            TestRunStartInfo.AgentName = "localhost";
            TestRunStartInfo.BazookaRepositoryNetworkDir = ConfigManager.BazookaRepositoryNetworkDir;
            TestRunStartInfo.BazookaReposityPath = ConfigManager.BazookaReposityPath;
            TestRunStartInfo.BuildNumber = "null";
            TestRunStartInfo.BazookaWorkingDirectory = ConfigManager.BazookaWorkingDirectory;
            TestRunStartInfo.ConnectionString = ConfigManager.ConnectionString;
            TestRunStartInfo.DataFilesDir = ConfigManager.BazookaDataFilesDir;
            TestRunStartInfo.Iteration = null;
            TestRunStartInfo.RunType = null;
            TestRunStartInfo.ShutdownDelay = 0;
            //External optional
            TestRunStartInfo.StartNewInstence = _startnewinstence;
            TestRunStartInfo.ShutdownOnCompletion = _shutdownoncompletion;
            TestRunStartInfo.StartingStep = _startingstep;
            TestRunStartInfo.EndingStep = _endingstep;
            TestRunStartInfo.StepToRun = null;

            TestRunStartInfo.ShouldCreateResultFiles = ShouldCreateResultFiles;
            TestRunStartInfo.TestRunParameters = testparameters;
            TestRunStartInfo.TeamProject = "Claw";
            //TestRunStartInfo.TestFile = Path.Combine(ConfigManager.BazookaTestFile, "SWAT_" + keyword + ".xml");
            TestRunStartInfo.TestFile = Path.Combine(Path.GetTempPath(), "SWAT_" + keyword + ".xml");
            TestRunStartInfo.TestPlan = "WTF Test";
            return TestRunStartInfo;
        }

        private void ProccessResult(ResultManager resultmanager)
        {
            List<TestRunIteration> iteration = resultmanager.IterationResults;
            _result = resultmanager.Result.ToString();
            //SerializableDictionary<int, WatchDogs.Models.StepTest.StepRun> steps = iteration[0].Steps;
            ResultLog();//steps);
        }

        private void ResultLog()//SerializableDictionary<int, WatchDogs.Models.StepTest.StepRun> steps)
        {
            //string stepdetails = null;
            //foreach (var step in steps)
            //{
            //    StepRun stepdetail = step.Value;
            //    stepdetails = stepdetail.EndTime.ToString() + " >> " +
            //                    stepdetail.Control.ToString() + " >> " +
            //                    stepdetail.Action.ToString() + " >> " +
            //                    stepdetail.InputData + " >> " +
            //                    stepdetail.Result.ToString();
            //    _resultlog = _resultlog + stepdetails + Environment.NewLine;
            //    MyLogger.Log(stepdetails);
            //}
        }
    }
}
