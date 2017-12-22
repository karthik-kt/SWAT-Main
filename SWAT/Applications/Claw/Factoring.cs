using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWAT.Data;
using SWAT.FrameWork.Utilities;
using SWAT.Configuration;
using SWAT.Applications.Claw.ObjectRepository;
using SWAT.Applications.Claw.DAL;

namespace SWAT.Applications.Claw
{

    internal class Factoring : UIActionsAndStates
    {
        FactoringPage _FactoringPage;
        AccoutingData _AccoutingData;

        public Factoring(TestStartInfo teststartinfo, DataManager datamanager)
        {
            testConfig = teststartinfo;
            driver = testConfig.Driver;
            _FactoringPage = new FactoringPage(teststartinfo);
            _AccoutingData = new AccoutingData(datamanager);
        }

        public string UIVerify()
        {
            try
            {
               Assert.IsTrue(_FactoringPage.Accouting_Table.UIVerify_Table(_AccoutingData.Table));
               return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
        }
    }
}