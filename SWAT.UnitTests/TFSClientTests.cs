
namespace SWAT.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SWAT.TFSClient;
    using SWAT.TFSModels;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    [TestClass]
    public class TFSClientTests
    {
        private TfsRequestBuilder _Tfs;

        [TestInitialize]
        public void Setup()
        {
            _Tfs = new TfsRequestBuilder();
        }

        [TestMethod]
        public void UpdateTestCaseOutComeTest()
        {
            UpdateTestCaseOutCome();
        }

        public  void UpdateTestCaseOutCome()
        {

        }

        [TestMethod]
        public void CreateTestRunTest()
        {
            CreateTestRun();
        }

        public async void CreateTestRun()
        {
            TestRun testrun = new TestRun()
            {
                name = "SWAT Automated Tests",
                plan = new Plan() { id = "572884" },
                //pointIds = new List<int> { 2304187, 2304188, 2304189, 2304190 }
            };
            string jsonString = JsonConvert.SerializeObject(testrun);
            await _Tfs.CreateTestRun("QA", jsonString);
        }

    }
}
