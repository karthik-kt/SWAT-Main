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
using Microsoft.TeamFoundation.TestManagement.WebApi;

namespace SWAT.TestManager
{
    public class TfsHandler
    {
        private TfsRequestBuilder _tfsRequest;
        private string _project;
        private string _planId;
        private string _runId;
        private IDictionary<string, string> _testIdResultsId;
        public TfsHandler(string project,string planid)
        {
            _tfsRequest = new TfsRequestBuilder();
            _project = project;
            _planId = planid;
        }

        public TfsHandler()
        {
            _tfsRequest = new TfsRequestBuilder();
        }

        #region read details
        public async Task<Array> GetProjects()
        {
            string projectsString = await _tfsRequest.GetTeamProjects();
            JObject projectsJObject = JObject.Parse(projectsString);
            return projectsJObject.SelectToken("value").
                            Select(p => p["name"].ToString()).
                            ToArray();
        }

        public async Task<Array> GetPlans(string project)
        {
            string plansString = await _tfsRequest.GetTestPlans(project);
            JObject projectsJObject = JObject.Parse(plansString);
            return projectsJObject.SelectToken("value").
                        Select(p => p["id"].ToString() + " : " + p["name"].
                        ToString()).
                        ToArray();
        }

        public async Task<List<string>> GetPoints(string suiteId)
        {
            string points = await _tfsRequest.GetTestPoints(_project, _planId, suiteId);
            JObject projectsJObject = JObject.Parse(points);
            return projectsJObject.SelectToken("value").
                        Select(p => p["id"].ToString()).
                        ToList();
        }

        public async Task<Dictionary<string, string>> GetResults(string project, string runId)
        {
            try
            {
                string testResult = await _tfsRequest.GetTestResult(project, runId);
                JObject projectsJObject = JObject.Parse(testResult);
                Dictionary<string, string> testCaseIdResultIds =
                            projectsJObject.SelectToken("value").
                            ToDictionary(p => p["testCase"]["id"].ToString(), p => p["id"].ToString());
                return testCaseIdResultIds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<ICollection<string>> GetSuites(string state)
        {
            try
            {
                JObject projectsJObject = JObject.Parse(await _tfsRequest.GetTestSuites(_project, _planId));
                ICollection<string> _AvilableTestSuiteIds =
                    projectsJObject.SelectToken("value").
                    Where(p => Int32.Parse(p["testCaseCount"].ToString()) > 0 && p["state"].ToString() == state).
                    Select(p => p["id"].ToString() + ":" + p["name"].ToString()).
                    ToArray();
                return _AvilableTestSuiteIds;
            }
            catch
            {
                return new List<string>();
            }
        }
        #endregion

        private async Task<List<TestCaseResult>> UpdateResult(string project,string runId,string resultId, string status, string resultPath)
        {
            TestCaseResultUpdateModel[] results = new TestCaseResultUpdateModel[1];
            results[0] = new TestCaseResultUpdateModel()
            {
                TestResult = new ShallowReference() { Id = resultId },
                Outcome = status,
                Comment = resultPath,
                State = "Completed"

            };
            return await _tfsRequest.UpdateTestResult(project, runId, results);
        } 
              
        public async Task UpdateWorkItemById(int id, string field, string value)
        {
            await _tfsRequest.UpdateWorkItemById(id, field, value);
        }

        public async void UpdateResult(string testCaseId, string status, string resultPath)
        {
            string resultId = null;
            if (_testIdResultsId.TryGetValue(testCaseId, out resultId))
            {
                await UpdateResult(_project, _runId, resultId, status, resultPath);
            }
        }

        public async Task UpdateSuiteState(string suiteId, string state)
        {
            await UpdateWorkItemById(Int32.Parse(suiteId), "/fields/System.State", state);
        }

        private async Task<string> CreateRun(string suiteId, string suiteName)
        {
            try
            {
                List<string> points = await GetPoints(suiteId);
                TFSModels.TestRun testrun = new TFSModels.TestRun()
                {
                    name = suiteName + "(SWAT-Automation)",
                    plan = new Plan() { id = _planId },
                    pointIds = points
                };
                string jsonString = JsonConvert.SerializeObject(testrun);
                string testRun = await _tfsRequest.CreateTestRun(_project, jsonString);
                JObject projectsJObject = JObject.Parse(testRun);
                return projectsJObject.SelectToken("id").ToString();
            }
            catch
            {
                return null;
            }
        }

        public async Task<IDictionary<string, string>> CreateTestRun(string suiteId, string suiteName)
        {
            try
            {

                _runId = await CreateRun(suiteId, suiteId);
                _testIdResultsId = await GetResults(_project, _runId);
                return _testIdResultsId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
