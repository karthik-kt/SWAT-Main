using OpenQA.Selenium;
using SWAT.FrameWork.Utilities;
using SWAT.Configuration;

namespace SWAT.Applications.Claw.ObjectRepository
{
    public class FactoringPage : Page
    {
        public FactoringPage(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
        }
        public UIItem Accouting_Table { get { return new UIItem("Factoring Company>> Accouting Tab>> Accouting Tab", By.CssSelector("#loaddetails-results-container>tr>td"), _driver); } }
    }
}
