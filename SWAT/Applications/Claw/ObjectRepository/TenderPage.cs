using OpenQA.Selenium;
using SWAT.FrameWork.Utilities;
using SWAT.Configuration;


namespace SWAT.Applications.Claw.ObjectRepository
{
    public class TenderPage : Page
    {
        public TenderPage(IWebDriver driver)
        {
            _driver = driver;
            url = "#tenderedloads";
        }

        public TenderPage(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#tenderedloads";
        }

        By apptitle = By.CssSelector("#app-title");
        By loadids = By.CssSelector(".nudge-half--right");
        By rushorder = By.CssSelector(".loz.epsilon.box--red");
        By expiredate = By.CssSelector(".grid.one-third.text-center");
        By primary = By.CssSelector(".loz.epsilon.box--green");
        By acceptbydate = By.CssSelector(".button.button--loud.one-whole.flyout__title.add-on__item.hook--popup-trigger.hook--accept-button");
        By acceptbtn = By.CssSelector(".button.button--loud.one-whole.add-on__item.hook--accept-button");
        By reject = By.CssSelector(".button.one-whole.add-on__item.flyout__title.hook--popup-trigger.hook--reject-button");
        By noloadavilablemsg = By.CssSelector(".alert__title");
        By note = By.XPath(".//*[@id='tenderedloads-results-container']/div[1]/div[2]/div/div[3]/div/dl[7]/dd");
        By tenderHistoryContainer = By.CssSelector("#tender-history-container");
        By spotQuotes = By.CssSelector("#spot-quotes");
        By spotQuotesResultsRows = By.CssSelector("#spot-quotes-results-container > tr");
        By loadSummary = By.CssSelector("#summary");
        By bookLoad = By.CssSelector(".hook--book-load");

        //By accept1stbtn = By.XPath(".//*[@id='tenderedloads-results-container']/div[1]/div[2]/div/div[1]/div[1]/div[1]/button");
        By accept1stbtn = By.CssSelector(".button.button--loud.one-whole.add-on__item.hook--accept-button");
        By hookSpotOfferRate = By.CssSelector(".hook--spot-offer-rate");
        By hookSpotOfferNotes = By.CssSelector(".hook--spot-offer-notes");


        public UIItem BookLoad { get { return new UIItem("Book Load", this.bookLoad, _driver); } }
        public UIItem LoadSummary { get { return new UIItem("Load summary", this.loadSummary, _driver); } }
        public UIItem SpotQuotesResultsRows { get { return new UIItem("Spot quotes result rows", this.spotQuotesResultsRows, _driver); } }
        public UIItem SpotQuotes { get { return new UIItem("Spot quotes table region", this.spotQuotes, _driver); } }
        public UIItem TenderHistoryContainer { get { return new UIItem("Tender history container tab", this.tenderHistoryContainer, _driver); } }
        public UIItem SpotOfferNotes { get { return new UIItem("Tender load spot offer note", this.hookSpotOfferNotes, _driver); } }
        public UIItem SpotOfferRate { get { return new UIItem("Tender load spot offer rate", this.hookSpotOfferRate, _driver); } }
        public UIItem AppTile { get { return new UIItem("Tender load page title", this.apptitle , _driver ); } }
        
        public UIItem LoadIDs { get { return new UIItem("LoadID", this.loadids , _driver ); } }

        public UIItem RushOrder { get { return new UIItem("Rush Order", this.rushorder , _driver ); } }

        public UIItem ExpireDate { get { return new UIItem("Expire Date", this.expiredate , _driver ); } }

        public UIItem PrimaryTxt { get { return new UIItem("Primary", this.primary , _driver ); } }

        public UIItem AcceptByDateBtns { get { return new UIItem("Accept Button", this.acceptbydate , _driver ); } }
        public UIItem AcceptBtn { get { return new UIItem("Accept Button", this.accept1stbtn, _driver); } }

        public UIItem RejectBtn { get { return new UIItem("Reject Button", this.reject , _driver ); } }

        public UIItem NoLoadAvilableMsg { get  {return new UIItem("Alert Msg - No tender loads available at this time.", this.noloadavilablemsg , _driver );} }

        public UIItem Notes { get { return new UIItem("Notes", note, _driver); } }

     }
}
