using System;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using SWAT.Domain.Interfaces;
using SWAT.Client.WebClient.Models;
using System.Diagnostics;

namespace SWAT.Client.WebClient.Controllers
{
    public class TestSuiteController : Controller
    {
        private TFSConnect _TFS = new TFSConnect();

        private readonly ITestSuiteRepository _TestSuiteRepository;

        public int PageSize = 20;
        public TestSuiteController(ITestSuiteRepository testSuiteRepository)
        {
            _TestSuiteRepository = testSuiteRepository;
        }

        public TestSuiteController()
        {

        }

        public void OpenBrowser(int browser=0)
        {
            Process p = new Process();
            if (browser == 0)
            {
                p.StartInfo.FileName = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
                p.StartInfo.Arguments = "google.com";
                p.Start();
            }
            if (browser == 1)
            {
                p.StartInfo.FileName = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
                p.StartInfo.Arguments = "google.com";
                p.Start();
            }
            if (browser == 2)
            {
                p.StartInfo.FileName = @"C:\Program Files\Internet Explorer\iexplore.exe";
                p.StartInfo.Arguments = "google.com";
                p.Start();
            }
        }

        public ViewResult List(int page = 1)
        {
            TestSuiteListViewModel TestSuiteListViewModel = new TestSuiteListViewModel
            {
                TestSuites = _TestSuiteRepository.TestSuites
                                                 .OrderBy(p => p.ID)
                                                 .Skip((page-1)*PageSize)
                                                 .Take(PageSize),
                PageInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _TestSuiteRepository.TestSuites.Count()
                },
                CurrentStatus = null
            };

            return View(TestSuiteListViewModel);
        }

        public async Task<ViewResult> TFSProjects_01()
        {
            string url = "http://vchitfs:8080/tfs/DefaultCollection/_apis/projects?api-version=1.0";
            SWAT.Client.WebClient.Models.TFSProjectResults table = null;
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
                    var data = await response.Content.ReadAsStringAsync();
                    table = Newtonsoft.Json.JsonConvert.DeserializeObject<SWAT.Client.WebClient.Models.TFSProjectResults>(data); 
                    using (System.IO.StringWriter sw = new System.IO.StringWriter())
                    {
                        using (System.Web.UI.HtmlTextWriter htw = new
                        System.Web.UI.HtmlTextWriter(sw))
                        {
                            ViewBag.WorkItems = table;
                        }
                    }
                }
            }
            return View(table);
        }

        [Authorize]
        public async Task<ViewResult> TFSProjects()
        {
            string listOfProjects = await _TFS.GetAllTFSProjects();
            TFSProjectResults ListOfWorkItems = Newtonsoft.Json.JsonConvert.DeserializeObject<TFSProjectResults>(listOfProjects);
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                using (System.Web.UI.HtmlTextWriter htw = new
                System.Web.UI.HtmlTextWriter(sw))
                {
                    ViewBag.WorkItems = ListOfWorkItems;
                }
            }
            return View(ListOfWorkItems);
        }

        public async Task<ActionResult> TFSTestPlan(string selectedProject)
        {
            _TFS.Project = selectedProject;
            List<TestPlan> testplans = await _TFS.GetTestPlan();
            return Json(testplans, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> TFSTestSuite(string selectedProject, string selectedTestPlanId)
        {
            _TFS.Project = selectedProject;
            List<TestSuite> testplans = await _TFS.GetTestSuites(Int32.Parse(selectedTestPlanId), false);
            return Json(testplans, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> TFSTestResult(string selectedProject, string selectedTestPlanId, string testSuite)
        {
            _TFS.Project = selectedProject;
            string testplans = await _TFS.GetTestResults(selectedTestPlanId, testSuite);
            return Json(testplans, JsonRequestBehavior.AllowGet);
        }
    }
}