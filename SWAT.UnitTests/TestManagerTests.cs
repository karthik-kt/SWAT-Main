using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWAT.TFSClient;
using SWAT.TFSModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using SWAT.TestManager;

namespace SWAT.UnitTests
{
    [TestClass]
    public class TestManagerTests
    {
        [TestMethod]
        public void AddTestResult()
        {
            AddResults();
        }

        private async void AddResults()
        {
            TfsHandler _TfsInformer = new TfsHandler("QA", "572884");
            await _TfsInformer.GetResults("QA", "180223");
        }
    }
}
