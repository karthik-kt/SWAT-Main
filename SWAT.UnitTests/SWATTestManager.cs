using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWAT.FrameWork.TestManager;
using SWAT.Configuration;
using SWAT.Applications.Claw;
namespace SWAT.UnitTests
{
    [TestClass]
    public class SWATTestManager
    {
        [TestMethod]
        public void BasicRun()
        {
            UpdateConfig.LoadConfig();
            UpdateConfig.SetEnivromnet("STAGING");
            SelectedSettings _settings = new SelectedSettings
            {
                Application = "CLAW",
                Environment = "STAGING",
                Browser = "CHROME",
                BulkSuite = null,
                isBulkExecution = false,
                Suite = @"\SMOKETESTS\03_ADMIN",
                PublishResult = false
            };
            TestSuiteManager TestManager = new TestSuiteManager();
           Assert.IsTrue(TestManager.StartRun(_settings));
        }
    }
}
