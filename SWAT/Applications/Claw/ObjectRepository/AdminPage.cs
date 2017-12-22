
namespace SWAT.Applications.Claw.ObjectRepository
{
    using OpenQA.Selenium;
    using SWAT.FrameWork.Utilities;
    using SWAT.Configuration;

    public class AdminPage : Page
    {
        public AdminPage(IWebDriver driver)
        {
            _driver = driver;
            url = "#admin";
            navigationlink = admintab;
        }

        public AdminPage(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#admin";
        }
        private By searchtype = By.CssSelector("#entity-type-select");
        private By valuetosearch = By.CssSelector("#search-term-input");
        private By searchbutton = By.CssSelector("#search-term-submit-button");
        private By searchhyperlink = By.CssSelector("#focus-search-term-input-button");
        private By searchresult1strow = By.CssSelector("#search-results-table-body>tr>td>a");
        private By defaulttext = By.CssSelector(".alert__title");
        private By coyotesymbol = By.CssSelector("#header-logo");
        private By homepage = By.CssSelector("#dashboard-view");
        private By admintab = By.LinkText("Admin");
        private By backtosearchbutton = By.CssSelector(".button");
        private By customerSummaryDetailsRegion = By.CssSelector("#customer-summary-details-region");
        private By searchHeaderName = By.XPath(".//*[@id='search-results-region']/div/div[1]/div[2]/table/thead/tr/th[1]");
        private By searchHeaderUserName = By.XPath(".//*[@id='search-results-region']/div/div[1]/div[2]/table/thead/tr/th[2]");
        private By searchHeaderLastLogin = By.XPath(".//*[@id='search-results-region']/div/div[1]/div[2]/table/thead/tr/th[2]");
        private By searchResultLastLogin1stRow = By.CssSelector("#search-results-table-body > tr > td:nth-child(3)");

        public UIItem SearchResultLastLogin1stRow { get { return new UIItem("Admin page>>  Search Result Last Login", this.searchResultLastLogin1stRow, _driver); } }
        public UIItem SearchHeaderLastLogin { get { return new UIItem("Admin page>>  Search Header Last Login", this.searchHeaderLastLogin, _driver); } }
        public UIItem SearchHeaderUserName { get { return new UIItem("Admin page>>  Search Header User Name", this.searchHeaderUserName, _driver); } }
        public UIItem SearchHeaderName { get { return new UIItem("Admin page>>  Search Header Name", this.searchHeaderName, _driver); } }
        public UIItem SearchResult1stRow { get { return new UIItem("Admin page>>  Search result 1st row", this.searchresult1strow, _driver); } }
        public UIItem ValueToSearch { get { return new UIItem("Admin page>>  Value to search", this.valuetosearch, _driver); } }
        public UIItem SearchButton { get { return new UIItem("Admin page>>  Search Button", this.searchbutton, _driver); } }
        public UIItem SearchHyperLink { get { return new UIItem("Admin page>>  Search Hyper link", this.searchhyperlink, _driver); } }
        public UIItem AdminTab { get { return new UIItem("Admin page>>  Admin Tab", this.admintab, _driver); } }
        public UIItem SearchType { get { return new UIItem("Admin page>>  Search Type", this.searchtype, _driver); } }
        public UIItem DefaultText { get { return new UIItem("Admin page>>  Default Text", this.defaulttext, _driver); } }
        public UIItem CoyoteSymbol { get { return new UIItem("Admin page>> Header Coyote Symbol", this.coyotesymbol, _driver); } }
        public UIItem HomePage { get { return new UIItem("Admin page>> Home Page", this.homepage, _driver); } }
        public UIItem byAccountInfo { get { return new UIItem("Account Information", By.CssSelector("#account-information-details"), _driver); } }
        public UIItem BackToSearchButton { get { return new UIItem("Admin page>> Manage Carriers/Cutomers/Facilities>> Back To Search Button", this.backtosearchbutton, _driver); } }
        public UIItem CustomerSummaryDetailsRegion { get { return new UIItem("Admin page>> Manage Carriers/Cutomers/Facilities>> Customer Summary Details Region", this.customerSummaryDetailsRegion, _driver); } }
    }
}
