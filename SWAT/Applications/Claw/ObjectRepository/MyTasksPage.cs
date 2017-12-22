using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAT.Applications.Claw.ObjectRepository
{
    using OpenQA.Selenium;
    using SWAT.FrameWork.Utilities;
    using SWAT.Configuration;

    class MyTasksPage : Page
    {
        public MyTasksPage(IWebDriver driver)
        {
            _driver = driver;
            url = "#mytasks/all-tasks";
            //navigationlink = "#";
        }

        public MyTasksPage(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#mytasks/all-tasks";
        }

        private By sidePanelHeader = By.CssSelector("#side-panel-header-text");
        private By confirmLoadsTab = By.XPath(".//*[@id='confirm-loads-container']/a");
        private By confirmLoadsPanel = By.Id("confirm-loads");
        private By confirmLoadsCount = By.CssSelector("#confirm-total-loads");
        private By confirmLoadsList = By.CssSelector("#confirm-loads-list>tr");
        private By accountingTotalLoads = By.CssSelector("#accounting-total-loads");
        private By linkMoreMissingPaperworkLoads = By.CssSelector("#show-more-missing-paperwork-loads > a");
        private By accountingTab = By.CssSelector("#accounting-container > a");
        private By trackingTab = By.CssSelector("#tracking-container > a");
        private By accountingPanel = By.CssSelector("#loads-missing-paperwork");
        private By missingPaperWorkListRows = By.CssSelector("#loads-missing-paperwork-list > tr");
        private By loadIdInSummary = By.CssSelector(".hook--load-details-summary > dl > dd > a");
        private By loadDetailsDocumentsPanel = By.CssSelector("#documents");
        private By missingDocumentsList = By.CssSelector("#missing-documents > tr");
        private By loadsTracking = By.CssSelector("#loads-tracking");
        private By loadsTrackingListRow = By.CssSelector("#loads-tracking-list > tr");
        private By loadIdInStopSummary = By.CssSelector(".hook--loadstop-summary > dl > dd > a");
        private By summaryRegion = By.CssSelector("#summary-region");

        // ConfirmLoad Form
        private By confirmLoadDriverName = By.XPath(".//input[starts-with(@id, 'driver-name-')]");
        private By confirmLoadDriverPhone = By.XPath(".//input[starts-with(@id, 'driver-phone-')]");
        private By confirmLoadEmptyLocation = By.XPath(".//input[starts-with(@id, 'empty-location-')]");
        private By confirmLoadEmptyDate = By.CssSelector("#empty-date");
        private By confirmLoadEmptyTime = By.CssSelector("#empty-time");
        private By confirmLoadConfirmButton = By.XPath(".//*[@class='button button--loud hook--button-confirm']");
        private By confirmLoadCancelButton = By.XPath(".//*[@class='text -link nudge-half--left hook--button-cancel']");

        public List<AccountingRow> AccountingRows
        {
            get
            {
                List<AccountingRow> accountingRows = new List<AccountingRow>();
                foreach (var element in this.MissingPaperWorkListRows.FindElements())
                {
                    accountingRows.Add(new AccountingRow(element));
                }
                return accountingRows;
            }

        }

        public List<TrackingRow> TrackingRows
        {
            get
            {
                List<TrackingRow> trackingRows = new List<TrackingRow>();
                foreach (var element in this.LoadsTrackingListRow.FindElements())
                {
                    trackingRows.Add(new TrackingRow(element));
                }
                return trackingRows;
            }

        }

        public UIItem SummaryRegion { get { return new UIItem("MyTasks >> Summary Region", this.summaryRegion, _driver); } }
        public UIItem LoadIdInStopSummary { get { return new UIItem("MyTasks >> LoadId In Stop Summary", this.loadIdInStopSummary, _driver); } }
        public UIItem LoadsTrackingListRow { get { return new UIItem("MyTasks >> Loads Tracking List row", this.loadsTrackingListRow, _driver); } }
        public UIItem LoadsTracking { get { return new UIItem("MyTasks >> Loads Tracking", this.loadsTracking, _driver); } }
        public UIItem MissingDocumentsList { get { return new UIItem("MyTasks >> Missing Documents List", this.missingDocumentsList, _driver); } }
        public UIItem LoadDetailsDocumentsPanel { get { return new UIItem("MyTasks >> LoadDetailsDocumentsPanel", this.loadDetailsDocumentsPanel, _driver); } }
        public UIItem LoadIdInSummary { get { return new UIItem("MyTasks >> LoadIdInSummary", this.loadIdInSummary, _driver); } }
        public UIItem MissingPaperWorkListRows { get { return new UIItem("MyTasks >> MissingPaperWorkListRows", this.missingPaperWorkListRows, _driver); } }
        public UIItem AccountingPanel { get { return new UIItem("MyTasks >> Accounting Panel", this.accountingPanel, _driver); } }
        public UIItem AccountingTab { get { return new UIItem("MyTasks >> Accounting Tab", this.accountingTab, _driver); } }
        public UIItem TrackingTab { get { return new UIItem("MyTasks >> Tracking Tab", this.trackingTab, _driver); } }
        public UIItem LinkMoreMissingPaperworkLoads { get { return new UIItem("MyTasks >> Link More Missing Paperwork Loads", this.linkMoreMissingPaperworkLoads, _driver); } }
        public UIItem AccountingTotalLoads { get { return new UIItem("MyTasks >> Accounting Total Loads", this.accountingTotalLoads, _driver); } }
        public UIItem SidePanelHeader { get { return new UIItem("MyTasks >> Side Panel Header", this.sidePanelHeader, _driver); } }
        public UIItem ConfirmLoadsTab { get { return new UIItem("MyTasks >> ConfirmLoads Tab", this.confirmLoadsTab, _driver); } }
        public UIItem ConfirmLoadsPanel { get { return new UIItem("MyTasks >> ConfirmLoads Panel", this.confirmLoadsPanel, _driver); } }
        public UIItem ConfirmLoadsCount { get { return new UIItem("MyTasks >> ConfirmLoads Count", this.confirmLoadsCount, _driver); } }
        public UIItem ConfirmLoadsList { get { return new UIItem("MyTasks >> ConfirmLoads List", this.confirmLoadsList, _driver); } }

        public UIItem ConfirmLoadDriverName { get { return new UIItem("MyTasks >> ConfirmLoad DriverName", this.confirmLoadDriverName, _driver); } }
        public UIItem ConfirmLoadDriverPhone { get { return new UIItem("MyTasks >> ConfirmLoad DriverPhone", this.confirmLoadDriverPhone, _driver); } }
        public UIItem ConfirmLoadEmptyLocation { get { return new UIItem("MyTasks >> ConfirmLoad EmptyLocation", this.confirmLoadEmptyLocation, _driver); } }
        public UIItem ConfirmLoadEmptyDate { get { return new UIItem("MyTasks >> ConfirmLoad EmptyDate", this.confirmLoadEmptyDate, _driver); } }
        public UIItem ConfirmLoadEmptyTime { get { return new UIItem("MyTasks >> ConfirmLoad EmptyTime", this.confirmLoadEmptyTime, _driver); } }
        public UIItem ConfirmLoadConfirmButton { get { return new UIItem("MyTasks >> ConfirmLoad ConfirmButton", this.confirmLoadConfirmButton, _driver); } }
        public UIItem ConfirmLoadCancelButton { get { return new UIItem("MyTasks >> ConfirmLoad CancelButton", this.confirmLoadCancelButton, _driver); } }

    }

    public class AccountingRow
    {
        private IWebElement _driver;
        private By loadID = By.XPath(".//td[1]");
        private By remark = By.XPath(".//td[2]");
        public AccountingRow(IWebElement driver)
        {
            _driver = driver;
        }
        public UIItem LoadID { get { return new UIItem("My Order >> LoadID", this.loadID, _driver); } }
        public UIItem Remark { get { return new UIItem("My Order >> Remark", this.remark, _driver); } }
    }

    public class TrackingRow
    {
        private IWebElement _driver;
        private By loadID = By.XPath(".//td[1]");
        private By remark = By.XPath(".//td[2]");
        private By callBackTime = By.XPath(".//td[6]");
        public TrackingRow(IWebElement driver)
        {
            _driver = driver;
        }
        public UIItem LoadID { get { return new UIItem("My task tracking >> LoadID", this.loadID, _driver); } }
        public UIItem Remark { get { return new UIItem("My task tracking >> Remark", this.remark, _driver); } }
        public UIItem CallBackTime { get { return new UIItem("My task tracking >> CallBackTime", this.callBackTime, _driver); } }
    }
}
