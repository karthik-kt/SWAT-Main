using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;

namespace SWAT.TFSClient
{
    public class TfsRequestBuilder
    {
        private readonly string instance = @"http://vchitfs:8080/tfs/defaultcollection";

        private readonly string version = @"1.0";

        private VssConnection connection;

        private string project;

        public TfsRequestBuilder()
        {

        }

        public TfsRequestBuilder(string project)
        {
            this.project = project;
        }

        private TtsHttpClient _TtsHttpClient
        {
            get
            {
                return new TtsHttpClient();
            }

        }

        public async Task UpdateWorkItemById(int Id, string Field, string Value)
        {
            connection = new VssConnection(new Uri(instance), new VssCredentials());
            WorkItemTrackingHttpClient witClient = connection.GetClient<WorkItemTrackingHttpClient>();
            WorkItem workitems = await
                witClient.GetWorkItemAsync(Id, null, null, null, null, default(System.Threading.CancellationToken));
            workitems.Fields[Field] = Value;
            JsonPatchDocument jsonDocument = new JsonPatchDocument();
            JsonPatchOperation jsonOperation = new JsonPatchOperation();
            jsonOperation.Operation = Microsoft.VisualStudio.Services.WebApi.Patch.Operation.Replace;
            jsonOperation.Value = Value;// "In Progress";
            jsonOperation.Path = Field;// "/fields/System.State";
            jsonDocument.Add(jsonOperation);
            await witClient.UpdateWorkItemAsync(jsonDocument, Id, null, false, null, default(System.Threading.CancellationToken));
        }

        public async Task<string> GetTeamProjects()
        {
            string url = $"{instance}/_apis/projects?api-version={version}";
            return await _TtsHttpClient.Get(url);
        }

        public async Task<string> GetTestPlans(string project)
        {
            string url = $"{instance}/{project}/_apis/test/plans?api-version={version}";
            return await _TtsHttpClient.Get(url);
        }

        public async Task<string> GetTestSuites(string project,string planId)
        {
            string url = $"{instance}/{project}/_apis/test/plans/{planId}/suites?api-version={version}";
            return await _TtsHttpClient.Get(url);
        }

        public async Task<string> GetTestPoints(string project, string planId,string suiteId)
        {
            string url = $"{instance}/{project}/_apis/test/plans/{planId}/suites/{suiteId}/points?api-version={version}";
            return await _TtsHttpClient.Get(url);
        }

        public async Task<string> SetOutCome(string project, string planId, string suiteId, string pointId, string jsonString)
        {
            string url = $"{instance}/{project}/_apis/test/plans/{planId}/suites/{suiteId}/points/{pointId}?api-version={version}";
            return await _TtsHttpClient.Patch(url, jsonString);// "{ \"outcome\": \"Failed\" }");
        }

        public async Task<string> CreateTestRun(string project,string jsonString)
        {
            string url = $"{instance}/{project}/_apis/test/runs?api-version={version}";
            return await _TtsHttpClient.PostSend(url, jsonString);
        }

        public async Task<string> GetTestResult(string project,string runId)
        {
            string url = $"{instance}/{project}/_apis/test/runs/{runId}/results?api-version={version}";
            return await _TtsHttpClient.Get(url);
        }

        public async Task<List<TestCaseResult>> UpdateTestResult(string project, string runId,TestCaseResultUpdateModel[] results)
        {
            try
            {
                connection = new VssConnection(new Uri(instance), new VssCredentials());
                TestManagementHttpClient witClient = connection.GetClient<TestManagementHttpClient>();
                List<TestCaseResult> testcaseresults = await witClient.UpdateTestResultsAsync(results, project, Int32.Parse(runId), null, default(CancellationToken));
                return testcaseresults;
            }
            catch
            {
                throw;
            }
        }
       
    }
}

