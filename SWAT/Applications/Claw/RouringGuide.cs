using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Threading;
using SWAT.Data;
using SWAT.FrameWork.Utilities;
using System.Collections.Generic;
using SWAT.Configuration;
using OpenQA.Selenium.Support.UI;
using SWAT.Applications.Claw.ObjectRepository;

namespace SWAT.Applications.Claw
{
    public class RouringGuide : UIObjects
    {
        //static By byLane = By.CssSelector(".hook--shipping-lane.block-list__link.block-list__link--right-arrow");
        static private By byLane = By.CssSelector(".hook--shipping-lane.block-list__link");

        static private By byPcarrier = By.CssSelector(".hook--carrier-name.nudge-half--ends");
        static private By byObjRtGdDD = By.CssSelector("#routing-guide-options");
        static private By byURLRtGd = By.CssSelector("/#routingguide");
        static private By byObjRtGdAdvSrch = By.CssSelector("#advanced-search-options-button");
        static private By byObjOstate = By.CssSelector("#origin-state-search-input");
        static private By byObjOcity = By.CssSelector("#origin-city-search-input");
        static private By byObjDstate = By.CssSelector("#destination-state-search-input");
        static private By byObjDcity = By.CssSelector("#destination-city-search-input");
        static private By byObjSrchBtn = By.CssSelector("#advanced-lane-search-submit");
        static private By byAddCarrierBtn = By.XPath(".//*[@id='view--customerlane-layout']/div/ul/li/div[2]/div/div/form/div[2]/button[1]");
        static private By byUpdateCarrierBtn = By.XPath(".//*[@id='view--customerlane-layout']/div/ul/li/div[2]/div/div/form/div[2]/button[3]");
        static private By byCarrierElement = By.CssSelector("[class*='hook--carrier-lane-row movable']");
        static private By byUndoCangesBtn = By.CssSelector("[class*='hook--cancel-changes-button']");
        static private By byCustLaneEditBtn = By.CssSelector(".hook--customer-lane-edit-button");
        static private By byObjOfacility = By.CssSelector("#origin-facility-search-input");
        static private By byObjDfacility = By.CssSelector("#destination-facility-search-input");
        static private By byCarrierName = By.CssSelector(".hook--carrier-name");
        static private By byCarrierCode = By.CssSelector(".hook--carrier-code-input");
        static private By byLineItems = By.CssSelector("[class*='hook--per-input']");
        static private By byLineItemsUnit = By.CssSelector("[class*='hook--units-input']");
        static private By byTimeAllowed = By.CssSelector("[class*='hook--allowed-time-input ']");
        static private By byCarrierEffDate = By.CssSelector("[class*='hook--effective-date-input']");
        static private By byCarrierExpDate = By.CssSelector("[class*='hook--expiration-date-input']");
        static private By byCarrierEffDateDisplay = By.CssSelector(".hook--effective-date-display");
        static private By byCarrierExpDateDisplay = By.CssSelector(".hook--expiration-date-display");
        static private By byCarrierErrorMessage = By.CssSelector(".hook--error-message");

        private string strExpPcarrier;
        private string strOstate;
        private string strDstate;
        private string strOcity;
        private string strDcity;
        private string strRtGd;
        private string strbaseURL;
        private string strURLRtGd;
        private string customerVal = null;
        private string addLaneBtn;
        private string cancelLaneBtn;
        private string origin;
        private string destination;
        private string distnace;
        private string laneType;
        private string equipMode;
        private string equipType;
        private string equipLength;
        private string minRate;
        private string maxPay;
        private string effDate;
        private string expDate;
        private string volType;
        private string volTotal;
        private string coyoteRank;
        private string bidID;
        private string custLaneID;
        private string custCommitment;
        private string rateQuoteType;
        private string offerDistType;
        private string preTender;
        //private string routingGuideOption;
        private string strOFacility;
        private string strDFacility;
        private string isLaneEditable;
        private string carrierCode;
        private string lineItems;
        private string lineItemsUnit;
        private string timeAllowed;
        private string carrierEffDate;
        private string carrierExpDate;
        private string action;

        static private IWebDriver _driver;

        private RoutingGuidePage _RoutingGuidePage;

        public RouringGuide(TestStartInfo teststartinfo, DataManager t)
            : base(teststartinfo)
        {
            _RoutingGuidePage = new RoutingGuidePage(teststartinfo);
            testConfig = teststartinfo;
            _driver = testConfig.Driver;
            strExpPcarrier = t.Data("PrimaryCarrier");
            strOstate = t.Data("OrginState");
            strDstate = t.Data("DestState");
            strOcity = t.Data("OrginCity");
            strDcity = t.Data("DestCity");
            strRtGd = t.Data("RoutingGuide");
            strbaseURL = t._baseurl;
            strURLRtGd = "#routingguide";
            customerVal = t.Data("Customer");
            addLaneBtn = t.Data("AddLaneBtn");
            cancelLaneBtn = t.Data("CancelLaneBtn");
            origin = t.Data("Origin");
            destination = t.Data("Destination");
            distnace = t.Data("Distance");
            laneType = t.Data("LaneType");
            equipMode = t.Data("EquipMode");
            equipType = t.Data("EquipType");
            equipLength = t.Data("EquipLength");
            minRate = t.Data("MinRate");
            maxPay = t.Data("MaxPay");
            effDate = t.Data("EffDate");
            expDate = t.Data("ExpDate");
            volType = t.Data("VolType");
            volTotal = t.Data("VolTotal");
            coyoteRank = t.Data("CoyoteRank");
            bidID = t.Data("BidID");
            custLaneID = t.Data("CustLaneID");
            custCommitment = t.Data("CustCommitment");
            rateQuoteType = t.Data("RateQuoteType");
            offerDistType = t.Data("OfferDistType");
            preTender = t.Data("PreTender");
            strOFacility = t.Data("OriginFacility");
            strDFacility = t.Data("DestFacility");
            isLaneEditable = t.Data("IsLaneEditable");
            carrierCode = t.Data("CarrierCode");
            lineItems = t.Data("LineItems");
            lineItemsUnit = t.Data("LineItemsUnit");
            timeAllowed = t.Data("TimeAllowed");
            carrierEffDate = t.Data("CarrierEffDate");
            carrierExpDate = t.Data("CarrierExpDate");
            action = t.Data("Action");
        }

        #region Functions

        //Open the 1st lane displayed and validate the
        public string VerifyLane()
        {
            try
            {
                //Open the 1st result
                Click(byLane);
                Assert.IsTrue(WaitUtilDisplayed(byPcarrier));
                //get the primary carrier name.
                string strActPcarrier = _driver.FindElement(byPcarrier).Text;
                if (isLaneEditable.ToUpper() == "NO")
                {
                    Assert.IsFalse(IsDisplayed(byCustLaneEditBtn));
                    Assert.IsFalse(IsDisplayed(byAddCarrierBtn));
                }
                if (strExpPcarrier == strActPcarrier)
                {
                    return "LaneVerificationSuccess";
                }
                return "PrimariyCarrierNotMatching";
            }
            catch
            {
                return "LaneVerificationFailed";
            }
        }
        public string VerifyLaneDetails()
        {
            try
            {
                Click(byLane);
                Assert.IsTrue(WaitUtilDisplayed(byCustLaneEditBtn));
                Click(byCustLaneEditBtn);
                Assert.IsTrue(OriginLabel.GetText(0).Contains(origin));
                Assert.IsTrue(DestinationLabel.GetText(0).Contains(destination));
                Assert.IsTrue(EquipModeLabel.GetText(0).Contains(equipMode));
                SelectElement equipTypes = new SelectElement(_driver.FindElement(byEqType));
                Assert.IsTrue(equipTypes.SelectedOption.Text.Equals(equipType));
                Assert.IsTrue(MaxPay.GetValue().Equals(maxPay));
                Assert.IsTrue(BidID.GetValue().Equals(bidID));
                Assert.IsTrue(CustLaneID.GetValue().Equals(custLaneID));
                Assert.IsTrue(CustCommitment.GetValue().Equals(custCommitment));
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
        }
        public string AdvSearch()
        {
            try
            {
                _RoutingGuidePage.Navigate();
                _RoutingGuidePage.WaitUntilLoading();
                Assert.IsTrue(_RoutingGuidePage.RoutingGuide_DD.WaitUntilDisplayed());
                _RoutingGuidePage.RoutingGuide_DD.Edit(strRtGd);
                _RoutingGuidePage.WaitUntilLoading();
                Assert.IsTrue(_RoutingGuidePage.AdvSearch.Click());
                Assert.IsTrue(_RoutingGuidePage.objOriginState.WaitUntilDisplayed());
                Assert.IsTrue(_RoutingGuidePage.AdvSearch_OrginFacility.TypeAndSelect(strOFacility));
                Assert.IsTrue(_RoutingGuidePage.objOriginState.SelectByText(strOstate));
                Assert.IsTrue(_RoutingGuidePage.objOriginCity.TypeAndSelect(strOcity));
                Assert.IsTrue(_RoutingGuidePage.objDestinationState.SelectByText(strDstate));
                Assert.IsTrue(_RoutingGuidePage.objDestinationCity.TypeAndSelect(strDcity));
                Assert.IsTrue(_RoutingGuidePage.AdvSearch_DestinationFacility.TypeAndSelect(strDFacility));
                //Search Button
                Click(byObjSrchBtn);

                Thread.Sleep(Constants.Wait_Short);
                return "SearchSuccess";
            }
            catch 
            {
                return "SearchFailed";
            }
        }

        public string RtGdSelect()
        {
            throw new NotImplementedException();
        }

        //Navigate to routing guide and verify we are on the correct page.
        public string Navigate()
        {
            By PageTitle = By.CssSelector("#app-title");
            try
            {
                Navigate(strURLRtGd);
                WaitUtilDisplayed(PageTitle, 30);
                Assert.IsTrue(GetText(PageTitle) == "Routing Guide");
                return "NavigationSuccess";
            }
            catch
            {
                return "NavigationFailed";
            }
        }

        //Search for a customer and verify the results displayed.
        public string Customer_Search()
        {
            By byCutomerSearch = By.CssSelector("#customer-search-input");
            UIItem byCustomerSearch = new UIItem("Customer search Box", byCutomerSearch, _driver);

            try
            {
                Assert.IsTrue(WaitUtilEnabled(byCutomerSearch, 30));
                ClearAndEdit(byCutomerSearch, customerVal);
                Thread.Sleep(Constants.Wait_Short);
                Edit(byCutomerSearch, Keys.Enter);
                Assert.IsTrue(WaitUtilEnabled(byObjRtGdDD, 30));
                return "SearchSuccess";
            }
            catch
            {
                return "SearchFailed";
            }
        }

        //Click on add RG button and verify the UI.
        public string Add_Verify()
        {
            By byAddRGBtn = By.CssSelector("#create-routing-guide-button");
            By byAddRGName = By.CssSelector("#routing-guide-name-input");
            By byAddRGPopupBtn = By.CssSelector("#add-routing-guide-button");
            By byAddRGCancel = By.XPath(".//*[@id='create-routing-guide-form']/ul/li[2]/button[2]");
            try
            {
                Assert.IsTrue(WaitUtilDisplayed(byObjRtGdDD));
                WaitUtilDisplayed(byAddRGBtn);
                Click(byAddRGBtn);
                Assert.IsTrue(WaitUtilDisplayed(byAddRGCancel));
                Assert.IsTrue(WaitUtilDisplayed(byAddRGPopupBtn));
                Assert.IsTrue(WaitUtilDisplayed(byAddRGName));
                try
                {
                    Click(byAddRGCancel);
                    Assert.IsFalse(WaitUtilDisplayed(byAddRGName));
                }
                catch
                {
                    Click(byAddRGBtn);
                    Assert.IsFalse(WaitUtilDisplayed(byAddRGName));
                }
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
        }

        #region Add Lane objects

        static private By byAddLaneBtn = By.XPath("//*[@id='add-customer-lane-button']");
        static private By byAddLaneSubmitBtn = By.XPath("//*[@id='add-lane-submit-button']");
        static private By byAddLaneTtl = By.XPath(".//*[@id='view--customerlaneform-itemview']/div/h2");
        static private By byOrgin = By.CssSelector("#origin-facility-input");
        static private By byDestination = By.CssSelector("#destination-facility-input");
        static private By byDistance = By.CssSelector("#distance-input");
        static private By byLaneType = By.CssSelector("#direction-select");
        static private By byEqMode = By.CssSelector("#equipment-mode-select");
        static private By byEqType = By.CssSelector("#equipment-type-select");
        static private By byEqLen = By.CssSelector("#equipment-length-input");
        static private By byMinRate = By.CssSelector("#minimum-rate-input");
        static private By byMaxPay = By.CssSelector("#max-pay-input");
        static private By byEffDate = By.CssSelector("#effective-date-input");
        static private By byExpDate = By.CssSelector("#expiration-date-input");
        static private By byVolType = By.CssSelector("#volume-type-select");
        static private By byVolTotal = By.CssSelector("#volume-total-input");
        static private By byCoyoteRank = By.CssSelector("#coyote-rank-select");
        static private By byBidID = By.CssSelector("#bid-identifier-input");
        static private By byCusLaneID = By.CssSelector("#cust-lane-identifier-input");
        static private By byCusCommitment = By.CssSelector("#cust-commitment-input");
        static private By byRateQuoteType = By.CssSelector("#rate-quote-type-select");
        static private By byOfferDistributionType = By.CssSelector("#offer-distribution-type-select");
        static private By byPreTender = By.CssSelector("#pretender-checkbox");
        static private By byAddLaneBtun = By.CssSelector("#add-lane-submit-button");
        static private By byCancelAddLane = By.CssSelector("#add-lane-back-link");

        static private By byOriginLbl = By.XPath(".//*[@id='customer-lane-form']/fieldset[1]/ul/li[1]");
        static private By byDestinationLbl = By.XPath(".//*[@id='customer-lane-form']/fieldset[1]/ul/li[2]");
        static private By byEquipModeLbl = By.XPath(".//*[@id='customer-lane-form']/fieldset[2]/ul/li[1]");


        UIItem AddLaneSubmitButton = new UIItem("Routing guide>> Add Lane Button", byAddLaneSubmitBtn, _driver);
        UIItem CancelAddLaneButton = new UIItem("Routing guide>> Cancel Add Lane Button", byCancelAddLane, _driver);
        UIItem Origin = new UIItem("Routing guide>> Add Lane>> Route>> Origin", byOrgin, _driver);
        UIItem Destination = new UIItem("Routing guide>> Add Lane>> Route>> Destination", byDestination, _driver);
        UIItem Distance = new UIItem("Routing guide>> Add Lane>> Route>> Distance", byDistance, _driver);
        UIItem LaneType = new UIItem("Routing guide>> Add Lane>> Route>> Lane Type", byLaneType, _driver);

        UIItem EquipMode = new UIItem("Routing guide>> Add Lane>> Equipment>> Mode", byEqMode, _driver);
        UIItem EquipType = new UIItem("Routing guide>> Add Lane>> Equipment>> Type", byEqType, _driver);
        UIItem EquipLength = new UIItem("Routing guide>> Add Lane>> Equipment>> Length", byEqLen, _driver);

        UIItem MinRate = new UIItem("Routing guide>> Add Lane>> Rates>> Min Rate", byMinRate, _driver);
        UIItem MaxPay = new UIItem("Routing guide>> Add Lane>> Rates>> Max Pay", byMaxPay, _driver);
        UIItem EffDate = new UIItem("Routing guide>> Add Lane>> Rates>> Effective Date", byEffDate, _driver);
        UIItem ExpDate = new UIItem("Routing guide>> Add Lane>> Rates>> Expiration Date", byExpDate, _driver);
        UIItem VolType = new UIItem("Routing guide>> Add Lane>> Rates>> Volume Type", byVolType, _driver);
        UIItem VolTotal = new UIItem("Routing guide>> Add Lane>> Rates>> Volume Total", byVolTotal, _driver);

        UIItem CoyoteRank = new UIItem("Routing guide>> Add Lane>> Internal>> Coyote Rank", byCoyoteRank, _driver);
        UIItem BidID = new UIItem("Routing guide>> Add Lane>> Internal>> Bid ID", byBidID, _driver);
        UIItem CustLaneID = new UIItem("Routing guide>> Add Lane>> Internal>> Customer ID", byCusLaneID, _driver);
        UIItem CustCommitment = new UIItem("Routing guide>> Add Lane>> Internal>> Customer Commitment", byCusCommitment, _driver);
        UIItem RateQuoteType = new UIItem("Routing guide>> Add Lane>> Internal>> Rate Quote Type", byRateQuoteType, _driver);
        UIItem OfferDistType = new UIItem("Routing guide>> Add Lane>> Internal>> Offer Distribution Type", byOfferDistributionType, _driver);
        UIItem PreTender = new UIItem("Routing guide>> Add Lane>> Internal>> Pretender", byPreTender, _driver);

        UIItem OriginLabel = new UIItem("Routing guide>> Edit Lane>> Origin", byOriginLbl, _driver);
        UIItem DestinationLabel = new UIItem("Routing guide>> Edit Lane>> Destination", byDestinationLbl, _driver);
        UIItem EquipModeLabel = new UIItem("Routing guide>> Edit Lane>> Equipment Mode", byEquipModeLbl, _driver);

        #endregion Add Lane objects

        //Click on the add lane button and verify the UI.
        public string AddLane_Verify()
        {

            try
            {
                //To pass !DEFAULT! for verification and actual value for adding a lane
                Assert.IsTrue(AddLane_Open());
                Assert.IsTrue(Origin.ClearAndEdit(origin));
                Thread.Sleep(2000);
                Assert.IsTrue(Origin.Edit(Keys.Enter));
                Assert.IsTrue(Destination.ClearAndEdit(destination));
                Thread.Sleep(2000);
                Assert.IsTrue(Destination.Edit(Keys.Enter));
                Assert.IsTrue(Distance.ClearAndEdit(distnace));
                Assert.IsTrue(LaneType.SelectByText(laneType));
                Assert.IsTrue(EquipMode.SelectByText(equipMode));
                Assert.IsTrue(EquipType.SelectByText(equipType));
                Assert.IsTrue(EquipLength.ClearAndEdit(equipLength));
                Assert.IsTrue(MinRate.ClearAndEdit(minRate));
                Assert.IsTrue(MaxPay.ClearAndEdit(maxPay));
                Assert.IsTrue(EffDate.ClearAndEdit(effDate));
                Assert.IsTrue(ExpDate.ClearAndEdit(expDate));
                Assert.IsTrue(VolType.SelectByText(volType));
                Assert.IsTrue(VolTotal.ClearAndEdit(volTotal));
                Assert.IsTrue(CoyoteRank.SelectByText(coyoteRank));
                Assert.IsTrue(BidID.ClearAndEdit(bidID));
                Assert.IsTrue(CustLaneID.ClearAndEdit(custLaneID));
                Assert.IsTrue(CustCommitment.ClearAndEdit(custCommitment));
                Assert.IsTrue(RateQuoteType.SelectByText(rateQuoteType));
                Assert.IsTrue(OfferDistType.SelectByText(offerDistType));
                Assert.IsTrue(PreTender.StatusCheckORPerFormAction(preTender));
                Assert.IsTrue(AddLaneSubmitButton.StatusCheckORPerFormAction(addLaneBtn));
                Assert.IsTrue(CancelAddLaneButton.StatusCheckORPerFormAction(cancelLaneBtn));
                Assert.IsTrue(WaitUtilDisplayed(byObjRtGdDD));
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
        }

        private bool AddLane_Open()
        {
            try
            {
                //select routing guides
                WaitUtilDisplayed(byObjRtGdDD);
                SelectByText(byObjRtGdDD, strRtGd);
                Thread.Sleep(Constants.Wait_Medium);
                Assert.IsTrue(WaitUtilDisplayed(byAddLaneBtn));
                Click(byAddLaneBtn);
                Thread.Sleep(Constants.Wait_Short);
                Assert.IsTrue(WaitUtilDisplayed(byOrgin));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string AddCarrier_Verify()
        {
            UIItem AddCarrierBtn = new UIItem("Routing Guide>> Add Carrier Button", byAddCarrierBtn, _driver);
            UIItem UpdateCarrierBtn = new UIItem("Routing Guide>> Update Carrier Button", byUpdateCarrierBtn, _driver);
            UIItem CarrierElement = new UIItem("Lane>> Carrier Element", byCarrierElement, _driver);
            UIItem UndoChangesButton = new UIItem("Lane>> Undo Changes Button", byUndoCangesBtn, _driver);
            try
            {
                Assert.IsTrue(AddCarrierBtn.WaitUntilDisplayed());
                Assert.IsTrue(UpdateCarrierBtn.WaitUntilDisplayed());
                Assert.IsFalse(UpdateCarrierBtn.IsEnabled());
                IList<IWebElement> carriersBeforeAdd = _driver.FindElements(byCarrierElement);

                Assert.IsTrue(AddCarrierBtn.Click());
                Assert.IsTrue(UpdateCarrierBtn.IsEnabled());
                Assert.IsTrue(UndoChangesButton.WaitUntilDisplayed());
                IList<IWebElement> carriersAfterAdd = _driver.FindElements(byCarrierElement);
                if (carriersAfterAdd.Count <= carriersBeforeAdd.Count)
                {
                    return "VerificationFailed";
                }
                Assert.IsTrue(UndoChangesButton.Click());
                Assert.IsFalse(UpdateCarrierBtn.IsEnabled());

                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
        }

        public string AddCarrier()
        {
            try
            {
                Assert.IsTrue(Open_AddCarrier());
                Assert.IsTrue(Fill_AddCarrier());
                Assert.IsTrue(Submit_AddCarrier());
                return "AddSuccess";
            }
            catch
            {
                return "AddFailed";
            }
        }

        private bool Open_AddCarrier()
        {
            UIItem AddCarrierBtn = new UIItem("Routing Guide>> Add Carrier Button", byAddCarrierBtn, _driver);
            UIItem Lane = new UIItem("Routing Guide>> Lane", byLane, _driver);

            try
            {
                Assert.IsTrue(Lane.Click());
                Assert.IsTrue(AddCarrierBtn.WaitUntilDisplayed());
                Assert.IsTrue(AddCarrierBtn.Click());

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool Fill_AddCarrier()
        {
            UIItem CarrierCode = new UIItem("Routing Guide>> Carrier Code", byCarrierCode, _driver);
            UIItem LineItems = new UIItem("Routing Guide>> LineItems", byLineItems, _driver);
            UIItem LineItemsUnit = new UIItem("Routing Guide>> LineItemsUnit", byLineItemsUnit, _driver);
            UIItem TimeAllowed = new UIItem("Routing Guide>> TimeAllowed", byTimeAllowed, _driver);
            UIItem CarrierEffDate = new UIItem("Routing Guide>> CarrierEffDate", byCarrierEffDate, _driver);
            UIItem CarrierExpDate = new UIItem("Routing Guide>> CarrierExpDate", byCarrierExpDate, _driver);

            try
            {
                int indexLineItem = LineItems.GetCountOfElements();
                int indexCarrier = CarrierEffDate.GetCountOfElements();
                Assert.IsTrue(CarrierCode.WaitUntilDisplayed());
                if (carrierCode != "!IGNORE!")
                {
                    Assert.IsTrue(CarrierCode.TypeAndSelect(carrierCode));
                    Assert.IsTrue(CarrierCode.WaitUntilDisabled());
                }
                Assert.IsTrue(LineItems.ClearAndEditByIndex(lineItems, indexLineItem));
                Assert.IsTrue(LineItemsUnit.ClearAndEditByIndex(lineItemsUnit, indexLineItem));
                Assert.IsTrue(TimeAllowed.ClearAndEditByIndex(timeAllowed, indexCarrier));
                CarrierExpDate.ScrollToElement(CarrierExpDate.ElementByIndex(indexCarrier));
                Assert.IsTrue(CarrierExpDate.WaitUntilDisplayed());
                Assert.IsTrue(CarrierExpDate.ClearAndEditByIndex(carrierExpDate, indexCarrier));
                Assert.IsTrue(CarrierEffDate.ClearAndEditByIndex(carrierEffDate, indexCarrier));

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool Submit_AddCarrier()
        {
            UIItem AddCarrierBtn = new UIItem("Routing Guide>> Add Carrier Button", byAddCarrierBtn, _driver);
            UIItem UpdateCarrierBtn = new UIItem("Routing Guide>> Update Carrier Button", byUpdateCarrierBtn, _driver);
            UIItem CarrierErrorMessage = new UIItem("Routing Guide>> Carrier Code", byCarrierErrorMessage, _driver);

            try
            {
                Assert.IsTrue(UpdateCarrierBtn.WaitUtilEnabled());
                Assert.IsTrue(UpdateCarrierBtn.Click());
                Thread.Sleep(Constants.Wait_Medium);
                Assert.IsTrue(_RoutingGuidePage.WaitUntilSaving());
                Assert.IsFalse(UpdateCarrierBtn.IsEnabled());
                Assert.IsFalse(CarrierErrorMessage.IsDisplayed());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string UIVerify()
        {
            try
            {
                if (action.ToUpper() == "ADDCARRIER")
                    Assert.IsTrue(AddCarrier_UIVerify());
                if (action.ToUpper() == "ADDEDCARRIER")
                    Assert.IsTrue(AddedCarrier_UIVerify());
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
        }

        public bool AddCarrier_UIVerify()
        {
            UIItem CarrierEffDate = new UIItem("Routing Guide>> CarrierEffDate", byCarrierEffDate, _driver);
            UIItem CarrierExpDate = new UIItem("Routing Guide>> CarrierExpDate", byCarrierExpDate, _driver);

            try
            {
                int index = CarrierEffDate.GetCountOfElements();
                CarrierExpDate.ScrollToElement(CarrierExpDate.ElementByIndex(index));
                Assert.IsTrue(carrierEffDate == Constants.Ignore ? true : CarrierEffDate.GetAllUIItems()[index-1].UIVerify(carrierEffDate));
                Assert.IsTrue(carrierExpDate == Constants.Ignore ? true : CarrierExpDate.GetAllUIItems()[index-1].UIVerify(carrierExpDate));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddedCarrier_UIVerify()
        {
            UIItem Lane = new UIItem("Routing Guide>> Lane", byLane, _driver);
            UIItem CarrierName = new UIItem("Routing Guide>> CarrierName", byCarrierName, _driver);
            UIItem CarrierEffDateDisplay = new UIItem("Routing Guide>> CarrierEffDate", byCarrierEffDateDisplay, _driver);
            UIItem CarrierExpDateDisplay = new UIItem("Routing Guide>> CarrierExpDate", byCarrierExpDateDisplay, _driver);

            try
            {
                Assert.IsTrue(Lane.Click());
                _RoutingGuidePage.WaitUntilLoading();
                Assert.IsTrue(CarrierName.WaitUntilDisplayed());
                int index = CarrierEffDateDisplay.GetCountOfElements();
                CarrierExpDateDisplay.ScrollToElement(CarrierExpDateDisplay.ElementByIndex(index));
                Assert.IsFalse(CarrierEffDateDisplay.IsEditable());
                Assert.IsFalse(CarrierExpDateDisplay.IsEditable());
                Assert.IsTrue(carrierEffDate == Constants.Ignore ? true : CarrierEffDateDisplay.GetAllUIItems()[index-1].UIVerify(carrierEffDate));
                Assert.IsTrue(carrierExpDate == Constants.Ignore ? true : CarrierExpDateDisplay.GetAllUIItems()[index-1].UIVerify(carrierExpDate));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion Functions
    }
}