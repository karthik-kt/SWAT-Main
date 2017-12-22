using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SWAT.Configuration;
using SWAT.TFSClient;
using SWATTestManager = SWAT.FrameWork.TestManager.TestSuiteManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWAT.TFSModels;
using Settings = SWAT.Configuration.SelectedSettings;

namespace SWAT.TestManager
{
    public class Transmitter
    {
        public event EventHandler<TestCaseStatusEventArgs> TestCaseStatusChanged;
        private TfsHandler _tfsHandler;
        private Settings _settings;
        private string _application = "CLAW";
        private string _environment = "STAGING";
        private string _browser = "CHROME";
        private string _project;
        private string _planId;
        public Transmitter(string project, string planId)
        {
            _tfsHandler = new TfsHandler(project, planId);
            _project = project;
            _planId = planId;
        }

        public async Task<string> ExecuteSuites()
        {
            try
            {
                string suiteState = "In Planning";
                ICollection<string> availableSuites = await _tfsHandler.GetSuites(suiteState);
                while (availableSuites.Count != 0)
                {
                    string suiteName = availableSuites.FirstOrDefault().Split(':').LastOrDefault();
                    string suiteId = availableSuites.FirstOrDefault().Split(':').FirstOrDefault();
                    SetupSettings(suiteName);
                    //ToDo: Handle error on run creation
                    await _tfsHandler.CreateTestRun(suiteId, suiteName);
                    await _tfsHandler.UpdateSuiteState(suiteId, "In Progress");
                    ExecuteSuite();
                    availableSuites = await _tfsHandler.GetSuites(suiteState);
                }
                return "Done";
            }
            catch(Exception)
            {
                throw;
            }
         }

        private void ExecuteSuite()
        {
            try
            {
                UpdateConfig.LoadConfig();
                UpdateConfig.SetEnivromnet(_environment);
                SWATTestManager TestManager = new SWATTestManager();
                TestManager.TestCaseStatusChanged += OnTestCaseCompletion;
                TestManager.StartRun(_settings);
            }
            catch
            {

            }
        }

        private void OnTestCaseCompletion(object sender, TestCaseStatusEventArgs e)
        {
            _tfsHandler.UpdateResult(e.TestCaseId.ToString(), e.TestCaseStatus, e.TestResultPath);
        }

        private void SetupSettings(string suiteName)
        {
            _settings.Application = _application;
            _settings.Environment = _environment;
            _settings.Browser = _browser;
            _settings.Suite = @"\SMOKETESTS\" + suiteName;
            //_settings.PublishResult = false;
        }

    }
}
