using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace SWAT.Client.WebClient.Models
{
    public class TFSConnect
    {
        VssConnection connection = null;
        public string Project { get; set; }

        public TFSConnect()
        {
            string collectionUri = @"http://vchitfs:8080/tfs/DefaultCollection/";
            connection = new VssConnection(new Uri(collectionUri), new VssCredentials());
        }

        public async Task<List<WorkItem>> GetTestCaseByID(int id)
        {
            WorkItemTrackingHttpClient witClient = connection.GetClient<WorkItemTrackingHttpClient>();
            string Wiql = "SELECT [System.Title], [System.WorkItemType] FROM WorkItems WHERE [System.ID] = " + id;
                               // "'PlayGround' AND" + "[System.WorkItemType] = 'Test Suite'";
            WorkItemQueryResult result = await witClient.QueryByWiqlAsync(new Wiql { Query = Wiql });
            IEnumerable<int> woritem_ids = result.WorkItems.Select(p => p.Id);
            List<WorkItem> workitems = await
                witClient.GetWorkItemsAsync(woritem_ids, null, null, null, null, default(System.Threading.CancellationToken));
            return workitems;
        }

        public async Task<string> GetAllTFSProjects()
        {
            string url = "http://vchitfs:8080/tfs/DefaultCollection/_apis/projects?api-version=1.0";
            return await GetTFSDetails(url);
        }

        public async Task<List<TestPlan>> GetTestPlan()
        {
            TestManagementHttpClient witClient = connection.GetClient<TestManagementHttpClient>();
            List<TestPlan> TestPlanList = await witClient.GetPlansAsync(Project, null, null, null,
                                                                null, null, null, default(System.Threading.CancellationToken));
            return TestPlanList;
        }

        public async Task<List<TestSuite>> GetTestSuites(int testPlanId,bool asTree)
        {
            TestManagementHttpClient witClient = connection.GetClient<TestManagementHttpClient>();
            List<TestSuite> TestSuiteList = await witClient.GetTestSuitesForPlanAsync(Project, testPlanId, null,
                                                null, null, asTree, null, default(System.Threading.CancellationToken));
            return TestSuiteList;
        }

        public async Task<List<TestSuite>> GetTestCaseResultByID(int testPlanId, bool asTree)
        {
            TestManagementHttpClient witClient = connection.GetClient<TestManagementHttpClient>();
            List<TestSuite> TestSuiteList = await witClient.GetTestSuitesForPlanAsync(Project, testPlanId, null,
                                                null, null, asTree, null, default(System.Threading.CancellationToken));
            return TestSuiteList;
        }

        public async Task<string> GetTestResults(string testPlan, string testSuite)
        {
            string url = $"http://vchitfs:8080/tfs/DefaultCollection/{Project}/_apis/test/plans/{testPlan}/suites/{testSuite}/points?witFields=System.Title,System.Id&api-version=1.0";
            return await GetTFSDetails(url);
        }

        public async Task<string> GetTFSDetails(string url)
        {
            System.Net.Http.HttpClientHandler handler = new System.Net.Http.HttpClientHandler()
            {
                UseDefaultCredentials = true
            };
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient(handler))
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                System.Net.Http.HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    return data;
                }
            }
            return null;
        }
    }
}