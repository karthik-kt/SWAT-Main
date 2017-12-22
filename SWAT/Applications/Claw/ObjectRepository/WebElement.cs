using OpenQA.Selenium;
using SWAT.Configuration;
using SWAT.FrameWork.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using Microsoft.TeamFoundation.TestManagement.Common;
using SWAT.Applications.Claw;
namespace SWAT.Applications.Claw.ObjectRepository
{
    public class TFSTests
    {
        VssConnection connection = null;
        public TFSTests()
        {
            string collectionUri = @"http://vchitfs:8080/tfs/DefaultCollection/";
            connection = new VssConnection(new Uri(collectionUri), new VssCredentials());
        }

        //public async Task<List<TestSuite>> GetTestCaseByID(int id)
        //{
        //    ITestManagementHttpClient witClient = connection.GetClient<TestManagementHttpClient>();
        //    return await witClient.GetTestSuitesForPlanAsync("Playground", id, null, null, null, true);
        //}

        public async Task<List<WorkItem>> GetTestCaseByID(int id)
        {
            WorkItemTrackingHttpClient witClient = connection.GetClient<WorkItemTrackingHttpClient>();
            string Wiql = "SELECT * FROM WorkItems WHERE [System.TeamProject] = @project AND [System.WorkItemType] = 'Bug'";
            var result = await witClient.QueryByWiqlAsync(new Wiql { Query = Wiql });
            var workitems = await GetWorkItems(result);
            return workitems;
        }
        private async Task<List<WorkItem>> GetWorkItems(WorkItemQueryResult result)
        {
            WorkItemTrackingHttpClient witClient = connection.GetClient<WorkItemTrackingHttpClient>();
            var id = Ids(result);
            return await witClient.GetWorkItemsAsync(id, null, null, WorkItemExpand.Fields);
        }
        private IEnumerable<int> Ids(WorkItemQueryResult result, int skip = 0)
        {
            IEnumerable<WorkItemLink> workitems = result.WorkItemRelations;
            IEnumerator<WorkItemLink> test = workitems.GetEnumerator();
            return workitems.Where(r => r.Target != null).Select(r => r.Target.Id)
                .Union(result.WorkItemRelations.Where(r => r.Source != null).Select(r => r.Source.Id))
                .Skip(skip)
                .Take(100);
        }
    }
    public class UIMap
    {
        public string Locator { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string CSS { get; set; }

        public string ClassName { get; set; }

        public string Id { get; set; }

        public string XPath { get; set; }
    }

    public class UIItems
    {
        public IEnumerable<UIMap> UIFields;

        public void Load(string filename)
        {
            UIFields = JsonConvert.DeserializeObject<IEnumerable<UIMap>>(File.ReadAllText(filename));
        }

        public Dictionary<string,By> by = new Dictionary<string, By>();

        public void BuildBy()
        {
            foreach(var item in UIFields)
            {
                if (item.CSS != "none")
                    by.Add(item.Name, By.CssSelector(item.CSS));
                else if (item.Id != "none")
                    by.Add(item.Name, By.Id(item.Id));
                else if (item.XPath != "none")
                    by.Add(item.Name, By.XPath(item.XPath));
                else if (item.ClassName != "none")
                    by.Add(item.Name, By.ClassName(item.ClassName));
            }
        }
    }

    public class Step
    {
        public string Page { get; set; }
        public string Field { get; set; }
        public By By { get; set; }
        public string Action { get; set; }
        public string Data { get; set; }
        public string ExpectedResult { get; set; }
        public string Opetion { get; set; }
  }
    
    public class TestProccessor
    {
        public void Execute(By by, string action,string data, IWebDriver _driver)
        {
            _driver.FindElement(by).SendKeys(data);
        }
    }
        
    public class StepProcessor
    {
        UIItems UIItems1 = null;
        TestProccessor TestProccessor= null;
        public StepProcessor(Step step,IWebDriver _driver)
        {
            TestProccessor = new TestProccessor();
            UIItems1 = new UIItems();
            UIItems1.Load(@"C:\"+step.Page+ ".json");
            UIItems1.BuildBy();
            TestProccessor.Execute(UIItems1.by[step.Field], step.Action,step.Data,_driver);
        }
    }

    public class TestManager
    {

    }

    [TestClass]
    public class StepProcessorTest
    {
        [TestMethod]
        public async void BasicTest1()
        {
            TFSTests TFSTests = new TFSTests();
            await TFSTests.GetTestCaseByID(269027);
        }

        [TestMethod]
        public void BasicTest()
        {
            BasicTest1();
            //WebDriver _driver = new WebDriver("CHROME", @"\\gxstorage\GXData\SmokeTests\Claw\Common\Drivers");
            //string url = @"https://connect-test.coyote.com/";
            //_driver.Driver.Navigate().GoToUrl(url);
            //Step step = new Step();
            //step.Page = "Login";
            //step.Field = "UserName";
            //step.Action = "Edit";
            //step.Data = "Login";
            //StepProcessor Step = new StepProcessor(step,_driver.Driver);
        }
    }

    [TestClass]
    public class ObjectsToJsonTests
    {
        [TestMethod]
        public void LoadSerializedObjects()
        {
            UIItems UIItems = new UIItems();
            UIItems.Load(@"c:\login.json");
            UIItems.BuildBy();
        }

        [TestMethod]
        public void SaveSerializedObject()
        {
            UIMap UIField1 = new UIMap();
            UIField1.CSS = "CSS";
            UIField1.Name = "TestObject";
            using (FileStream fs = File.Open(@"c:\login.json", FileMode.OpenOrCreate))
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(jw, UIField1);
            }
        }

        [TestMethod]
        public void SaveSerializedObjects()
        {
            IEnumerable<UIMap> UIField1 = new List<UIMap>
            {
                new UIMap {Name="UserName",ClassName = "none",CSS ="#UserName",Id ="UserName",XPath=".//*[@id='UserName']",Type="EditBox"},
                new UIMap {Name="Password",ClassName = "none",CSS ="#Password",Id ="Password",XPath=".//*[@id='Password']",Type="EditBox"},
                new UIMap {Name="Submit",ClassName = "none",CSS ="#saveForm",Id ="saveForm",XPath=".//*[@id='saveForm']",Type="EditBox"}
            };
            using (FileStream fs = File.Open(@"c:\UIMap\login.json", FileMode.OpenOrCreate))
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(jw, UIField1);
            }
        }

        [TestMethod]
        public void SerializeObject()
        {
            UIMap UIField = new UIMap();
            UIField.CSS = "CSS";
            UIField.Name = "TestObject";
            string output = JsonConvert.SerializeObject(UIField);
        }

        [TestMethod]
        public void DeSerializeObject()
        {
            UIMap UIField1 = new UIMap();
            UIField1.CSS = "CSS";
            UIField1.Name = "TestObject";
            string output = JsonConvert.SerializeObject(UIField1);
            UIMap UIField2 = JsonConvert.DeserializeObject<UIMap>(output);
            Assert.IsTrue(UIField1.Equals(UIField2));
        }
    }

    [TestClass]
    public class UIFieldTests
    {
  
        [TestMethod]
        public void SerializeWebDriver()
        {
            UIItems UIItems = new UIItems();
            UIItems.Load(@"c:\login.json");
            UIItem UIItem = new UIItem();
        }
        
        [TestMethod]
        public void GetElements()
        {
            List<IWebElement> elements = new List<IWebElement>(_driver.Driver.FindElements(By.TagName("input")));
            IJavaScriptExecutor js = _driver.Driver as IJavaScriptExecutor;
            using (FileStream fs = File.Open(@"c:\person.json", FileMode.OpenOrCreate))
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(jw, js.ExecuteScript("return $('label')"));
            }            
        }

        [TestCleanup]
        public void Cleanup()
        {
            _driver.Driver.Close();
            _driver.Driver.Dispose();

        }

        [TestInitialize]
        public void Setup()
        {
            _driver = new WebDriver("CHROME", @"\\gxstorage\GXData\SmokeTests\Claw\Common\Drivers");
             url = @"https://connect-test.coyote.com/";
            _driver.Driver.Navigate().GoToUrl(url);
        }

        public WebDriver _driver;
        public string url = @"https://connect-test.coyote.com/";
    }

    public class UILoad
    {
        public string ObjectName { get; set; }
        public string LocatorType { get; set; }
        public string LocatorString { get; set; }
    }

    [TestClass]
    public class UIObjectLoad
    {
        [TestMethod]
        public void test()
        {
            ICollection<UILoad> UILoads = new List<UILoad>
                {
                     new UILoad { ObjectName ="BasicSearchButton",LocatorType="CssSelector", LocatorString ="#basic-search-button" },
                     new UILoad { ObjectName ="BasicSearchInput",LocatorType="CssSelector",LocatorString ="#basic-search-number-parameter-input" },
                     new UILoad { ObjectName ="BasicSearchSubmit",LocatorType="CssSelector",LocatorString ="#basic-search-submit"}
                };

            using (FileStream fs = File.Open(@"c:\UIMap\MyLoad2.json", FileMode.OpenOrCreate))
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(jw, UILoads);
            }
            UILoads = null;
            UILoads = JsonConvert.DeserializeObject<ICollection<UILoad>>(File.ReadAllText(@"c:\UIMap\MyLoad2.json"));
            Dictionary<string, UIItem> test = new Dictionary<string, UIItem>();
            foreach(UILoad uiload in UILoads)
            {
                switch (uiload.LocatorType.ToLower())
                {
                    case "cssselector":
                        test.Add(uiload.ObjectName, new UIItem(uiload.ObjectName, By.CssSelector(uiload.LocatorString)));
                        break;
                }
            }
        }       
        
    }
}
