using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWAT.TFSModels;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SWAT.UnitTests
{
    [TestClass]
    public class TFSModelsTest
    {
        [TestMethod]
        public void ToJsonTest()
        {
            TestRun testrun = new TestRun()
            {
                name = "test",
                plan = new Plan() { id = "121312312" },
                    //pointIds = new List<int> { 12312, 123123 },
                    //iteration = "iteration",
                    //build = new Build() { id = "123123123123" },
                    //state = TestRunState.InProgress
                    //dueDate = "12/12/2015",                
                    //isAutomated = true,
                    //controller = "testcontroller",
                    //errorMessage ="errormessage",
                    //comment = "comment",
                    //testSettings = new TestSettings() {id = 65664 },
                    //testEnvironmentId = "testEnvironmentid",
                    //startedDate ="",
                    //completedDate ="",
                    //owner = new Owner() { displayName = "tester"},
                    //buildDropLocation =  "builddroploaction",
                    //buildFlavor = "buildflavor",
                    //configIds = new List<int>() { 21212,2,4545},
                    //releaseUri = "releaseUri",
                    //releaseEnvironmentUri = "releaseEnvironmentUri"
                };
            string jsonstring = JsonConvert.SerializeObject(testrun);
        }
    }
}
