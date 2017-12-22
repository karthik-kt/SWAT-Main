using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SWAT.Data;
using SWAT.Logger;
using SWAT.FrameWork.Utilities;
using Config = SWAT.Configuration.TestStartInfo;

namespace SWAT.Applications.Claw
{


    internal class Carriers : UIActionsAndStates
    {
        # region Facility screen UI
        private UIItem byPageTitle = new UIItem("PageTile", By.CssSelector("#app-title"));
        private UIItem byBackButton = new UIItem("BackButton", By.CssSelector("#carrier-back-button"));
        private UIItem byContactDetails = new UIItem("ContactInfo", By.CssSelector("#view--details-itemview>div>fieldset"));
        private UIItem byTenderEmailCnbx = new UIItem("Tender email checkbox", By.CssSelector("#tms-redirect-to-claw"));
        private UIItem byHideCustNmChbx = new UIItem("Hide Customer Name Checkbox", By.CssSelector("#hide-customer-name"));
        private UIItem byAdditionalSettingsShowFacilityHours = new UIItem("AdditionalSettings >> ShowFacilityHours", By.CssSelector("#show-facility-hours"));
        private UIItem byAdditionalSettingsShowAppointmentSchedule = new UIItem("AdditionalSettings >> ShowAppointmentSchedule", By.CssSelector("#show-appointment-schedule"));
        #endregion

        # region Admin screen UI
        private UIItem byAdminTab = new UIItem("AdminTab", By.LinkText("Admin"));
        private UIItem bySearchBtn = new UIItem("Search Button", By.CssSelector("#admin-search-button"));
        private UIItem bySearchTxt = new UIItem("Search EditBox", By.CssSelector("#search-input-box"));
        private UIItem bySearchBy = new UIItem("SearchBy DropDown", By.CssSelector("#entity-type"));
        private UIItem bySearchTbl = new UIItem("Search Result Table", By.CssSelector("#entity-search-results>tr>td"));
        private UIItem bySeachRes1Row = new UIItem("Search Result 1st Row", By.CssSelector("#entity-search-results>tr>td>a"));
        #endregion



        #region test data
        private string strCompanyName;
        private string strCode;
        private string strLocation;
        private string strSearchBy;
        private string strShowChargesSectionWithoutAccoutingAccess;
        #endregion

        public Carriers(Config c, DataManager t)
        {
            testConfig = c;
            driver = testConfig.Driver;
            strCompanyName = t.Data("Company");
            strCode = t.Data("Code");
            strLocation = t.Data("Location");
            strSearchBy = t.Data("SearchBy");
            strShowChargesSectionWithoutAccoutingAccess = t.Data("ShowChargesSectionWithoutAccoutingAccess");
        }

        public UIItem Carrier_Summary_Hdr { get { return new UIItem("Carrier Profile>> Summary Headers", By.XPath(".//*[@id='view--summary-region']/div/div/div[1]/h2"), driver); } }
        public UIItem TMS_Tender_Settings_Hdr { get { return new UIItem("Carrier Profile>> TMS Tender Settings Headers", By.XPath(".//*[@id='view--setting-region']/div/div[1]/div[1]/h2"), driver); } }
        public UIItem Additional_Settings_Hdr { get { return new UIItem("Carrier Profile>> Additional Settings Headers", By.XPath(".//*[@id='view--setting-region']/div/div[2]/div[1]/h2"),driver); } }

        //Verify facility page details
        public string VerifyInfo()
        {
            try
            {
                WaitUtilDisplayed(byPageTitle);
                //Assert.IsTrue(IsDisplayed(byBackButton));
                //Assert.IsTrue(IsDisplayed(byContactDetails));
                //Assert.IsTrue(IsDisplayed(byTenderEmailCnbx));
                //Assert.IsTrue(IsDisplayed(byHideCustNmChbx));
                //Assert.IsTrue(IsDisplayed(byAdditionalSettingsShowFacilityHours));
                //Assert.IsTrue(IsDisplayed(byAdditionalSettingsShowAppointmentSchedule));
                Assert.IsTrue(Carrier_Summary_Hdr.GetText().ToString().ToUpper() == "CARRIER");
                Assert.IsTrue(TMS_Tender_Settings_Hdr.GetText().ToString().ToUpper() == "TMS TENDER SETTINGS");
                Assert.IsTrue(Additional_Settings_Hdr.GetText().ToString().ToUpper() == "ADDITIONAL SETTINGS");
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
        }

        #region Search and open facility

        //Search for the user and open the user displayed on the 1st
        public string SearchAndOpen()
        {
            try
            {
                string strActResult = null;
                strActResult = Search();
                if ("SearchSuccess" == strActResult)
                {
                    strActResult = Open();
                    if (strActResult == "ManagePageOpened")
                        strActResult = "SearchAndOpenSuccess";
                }
                return strActResult;
            }
            catch
            {
                return "SearchAndOpenFailed";
            }
        }

        public string Search()
        {
            try
            {
                Assert.IsTrue(WaitUtilDisplayed(byAdminTab));
                Click(byAdminTab);
                Assert.IsTrue(WaitUtilDisplayed(bySearchBtn));
                SelectByText(bySearchBy, strSearchBy);
                ClearAndEdit(bySearchTxt, strCompanyName);
                try { Click(bySearchBtn); }
                catch
                {
                    MyLogger.Log("Failed to click on search button using work around.");
                    Edit(bySearchTxt, Keys.Enter);
                }

                Assert.IsTrue(WaitUtilDisplayed(bySeachRes1Row, 60));
                return "SearchSuccess";
            }
            catch
            {
                return "SearchFailed";
            }
        }

        //Open the user details page from the search result page.
        public string Open()
        {
            try
            {
                WaitUtilDisplayed(bySeachRes1Row);
                Click(bySeachRes1Row);
                WaitUtilDisplayed(byPageTitle);
                return "ManagePageOpened";
            }
            catch
            {
                return "ManagePageOpenFailed";
            }
        }

        public string UserAppAccess_Verify()
        {
            try
            {
                UIItem ShowChargesSectionWithoutAccoutingAccess = new UIItem("User Profile >> Applications >> Show charges section without accounting access", By.CssSelector("#permission-224"),driver);
                Assert.IsTrue(ApplicationAccessOpen());
                Assert.IsTrue(ShowChargesSectionWithoutAccoutingAccess.StatusCheckORPerFormAction(strShowChargesSectionWithoutAccoutingAccess));
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
        }

        private bool ApplicationAccessOpen()
        {
            try
            {
                Page _Page= new Page(driver);
                UIItem ApplicationAccessTab= new UIItem("Application Access>> Application Access Tab", By.CssSelector("#permission"), driver);
                Assert.IsTrue(ApplicationAccessTab.Click());
                _Page.WaitUntilLoading();
                return true;
            }
            catch
            {
                MyLogger.Log("Opening application access page failed.");
                return false;
            }
        }
        #endregion
    }
}