using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;

namespace SWAT.Applications.Claw
{
    //using SWAT.FrameWork.Utilities;
    using SWAT.Data;
    using SWAT.Logger;
    using SWAT.FrameWork.Utilities;
    using Config = SWAT.Configuration.TestStartInfo;

    internal class Facility : UIActionsAndStates
    {
        # region Facility screen UI
        private UIItem byPageTitle = new UIItem("PageTile", By.CssSelector("#app-title"));
        private UIItem byBackButton = new UIItem("BackButton", By.CssSelector("#facility-back-button"));
        private UIItem byContactDetails = new UIItem("ContactInfo", By.CssSelector("#facilitymanager-contact>div>p"));
        private UIItem byThemeButton = new UIItem("ThemeButton", By.CssSelector("#skin-button"));
        private UIItem byThemeDropDown = new UIItem("ThemeDropDown", By.CssSelector("#skin-button"));
        private UIItem byLimitScheduling = new UIItem("LimitSchedulingCheckbox", By.CssSelector("#facilitymanager-schedulesettings-enabledailylimits"));
        private UIItem byMaxDayAppt = new UIItem("Max/Day", By.CssSelector("#facilitymanager-schedulesettings-maxdailyappointments"));
        private UIItem byApptIncrMin = new UIItem("Appt. Increments", By.CssSelector("#facilitymanager-schedulesettings-increment"));
        private UIItem byShowCommodityValue = new UIItem("AdditionalSettings >> ShowCommodityValue", By.CssSelector("#show-commodity-value"));
        #endregion

        # region Admin screen UI
        private UIItem byAdminTab = new UIItem("AdminTab", By.LinkText("Admin"));
        private UIItem bySearchBtn = new UIItem("Search Button", By.CssSelector("#admin-search-button"));
        private UIItem bySearchTxt = new UIItem("Search EditBox", By.CssSelector("#search-input-box"));
        private UIItem bySearchBy = new UIItem("SearchBy DropDown", By.CssSelector("#entity-type"));
        private UIItem bySearchTbl = new UIItem("Search Result Table", By.CssSelector("#entity-search-results>tr>td"));
        private UIItem bySeachRes1Row = new UIItem("Search Result 1st Row", By.CssSelector("#entity-search-results>tr>td>a"));
        # endregion

        #region test data
        private string strCompanyName;
        private string strCode;
        private string strLocation;
        private string strSearchBy;
        #endregion

        public Facility(Config c, DataManager t)
        {
            testConfig = c;
            driver = testConfig.Driver;
            strCompanyName = t.Data("Company");
            strCode = t.Data("Code");
            strLocation = t.Data("Location");
            strSearchBy = t.Data("SearchBy");
        }

        public UIItem Facility_Hdr_Summary { get { return new UIItem("", By.XPath(".//*[@id='application']/div/div/div[2]/div[1]/div/div[1]/h2"), driver); } }
        public UIItem Facility_Hdr_Settings { get { return new UIItem("", By.XPath(".//*[@id='facilitymanager-settings']/div/div/div[1]/h2"),driver); } }
        public UIItem Facility_Hdr_AdditionalSettings { get { return new UIItem("", By.XPath(".//*[@id='facilitymanager-additional-settings']/div/div/div[1]/h2"),driver); } }

        //Verify facility page details
        public string VerifyInfo()
        {
            try
            {
                WaitUtilDisplayed(byPageTitle);
                ////Assert.IsTrue(IsDisplayed(byBackButton));
                //Assert.IsTrue(IsDisplayed(byContactDetails));
                ////below field is removed.
                //// Assert.IsTrue(IsDisplayed(byThemeButton));
                ////Assert.IsTrue(IsDisplayed(byThemeDropDown));
                //Assert.IsTrue(IsDisplayed(byLimitScheduling));
                //Assert.IsTrue(IsDisplayed(byMaxDayAppt));
                //Assert.IsTrue(IsDisplayed(byShowCommodityValue));
                Assert.IsTrue(Facility_Hdr_Summary.GetText().ToString().ToUpper() == "FACILITY");
                Assert.IsTrue(Facility_Hdr_Settings.GetText().ToString().ToUpper() == "SETTINGS");
                Assert.IsTrue(Facility_Hdr_AdditionalSettings.GetText().ToString().ToUpper() == "ADDITIONAL SETTINGS");
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
                Edit(bySearchBy, strSearchBy);
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

        #endregion

        #region Application Access Data and UI

        private UIItem byScheduler_Access = new UIItem("Scheduler_Access", By.CssSelector("#permission-128"));
        private UIItem byFacilityCal_Access = new UIItem("FacilityCal_Access", By.CssSelector("#permission-132"));
        private UIItem byOrderMngr_Access = new UIItem("OrderMngr_Access", By.CssSelector("#permission-136"));
        private UIItem byOrderMngr_ViewAll = new UIItem("OrderMngr_ViewAll", By.CssSelector("#permission-145"));
        private UIItem byOrderMngr_Edit = new UIItem("OrderMngr_Edit", By.CssSelector("#permission-146"));
        private UIItem byOrderMngr_Create = new UIItem("OrderMngr_Create", By.CssSelector("#permission-147"));
        private UIItem byOrderMngr_Cancel = new UIItem("OrderMngr_Cancel", By.CssSelector("#permission-148"));
        private UIItem byRGMngr_Access = new UIItem("RGMngr_Access", By.CssSelector("#permission-139"));
        private UIItem byMyLds_Access = new UIItem("MyLds_Access", By.CssSelector("#permission-142"));
        private UIItem byMyLds_EditLdDetails = new UIItem("MyLds_EditLdDetails", By.CssSelector("#permission-166"));
        private UIItem byMyLds_FlagHPC = new UIItem("MyLds_FlagHPC", By.CssSelector("#permission-167"));
        private UIItem byMyLds_ViewTrackingNotes = new UIItem("MyLds_ViewTrackingNotes", By.CssSelector("#permission-169"));
        private UIItem byMyLds_ViewBOL = new UIItem("MyLds_ViewBOL", By.CssSelector("#permission-186"));
        private UIItem byMyLds_HideMoney = new UIItem("MyLds_HideMoney", By.CssSelector("#permission-190"));
        private UIItem byMyLds_CreateLds = new UIItem("MyLds_CreateLds", By.CssSelector("#permission-192"));
        private UIItem byMyLds_RollLds = new UIItem("MyLds_RollLds", By.CssSelector("#permission-194"));
        private UIItem byMyLds_CancelLds = new UIItem("MyLds_CancelLds", By.CssSelector("#permission-195"));
        private UIItem byMyLds_Appointments = new UIItem("MyLds_Appointments", By.CssSelector("#permission-197"));
        private UIItem byAccouting_Access = new UIItem("Accouting_Access", By.CssSelector("#permission-151"));
        private UIItem byDB_Access = new UIItem("DB_Access", By.CssSelector("#permission-165"));
        private UIItem byDB_FaciltiyAcitivity = new UIItem("DB_FaciltiyAcitivity", By.CssSelector("#permission-170"));
        private UIItem byDB_AccountBalance = new UIItem("DB_AccountBalance", By.CssSelector("#permission-171"));
        private UIItem byDB_HPShipements = new UIItem("DB_HPShipements", By.CssSelector("#permission-171"));
        private UIItem byDB_CostPerUnitGadget = new UIItem("DB_CostPerUnitGadget", By.CssSelector("#permission-175"));
        private UIItem byDB_CustGadget = new UIItem("DB_CustGadget", By.CssSelector("#permission-181"));
        private UIItem byDB_LeadTimeGadget = new UIItem("DB_LeadTimeGadget", By.CssSelector("#permission-182"));
        private UIItem byDB_FuelProgram = new UIItem("DB_FuelProgram", By.CssSelector("#permission-183"));
        private UIItem byDB_AcceptanceGadget = new UIItem("DB_AcceptanceGadget", By.CssSelector("#permission-188"));
        private UIItem byDB_MyReports = new UIItem("DB_MyReports", By.CssSelector("#permission-189"));
        private UIItem byReporting_Access = new UIItem("Reporting_Access", By.CssSelector("#permission-202"));
        private UIItem byCarrierSpotMngt_Access = new UIItem("CarrierSpotMngt_Access", By.CssSelector("#permission-210"));
        private UIItem byCutom_SSRS_Reports = new UIItem("Cutom_SSRS_Reports", By.CssSelector("#my-reports"));
        private UIItem byPremium_Reporting = new UIItem("Premium_Reporting", By.CssSelector("#tableau-workbooks-container"));

        //private string Scheduler_Access;
        //private string FacilityCal_Access;
        //private string OrderMngr_Access;
        //private string OrderMngr_ViewAll;
        //private string OrderMngr_Edit;
        //private string OrderMngr_Create;
        //private string OrderMngr_Cancel;
        //private string RGMngr_Access;
        //private string MyLds_Access;
        //private string MyLds_EditLdDetails;
        //private string MyLds_FlagHPC;
        //private string MyLds_ViewTrackingNotes;
        //private string MyLds_ViewBOL;
        //private string MyLds_HideMoney;
        //private string MyLds_CreateLds;
        //private string MyLds_RollLds;
        //private string MyLds_CancelLds;
        //private string MyLds_Appointments;
        //private string Accouting_Access;
        //private string DB_Access;
        //private string DB_FaciltiyAcitivity;
        //private string DB_AccountBalance;
        //private string DB_HPShipements;
        //private string DB_CostPerUnitGadget;
        //private string DB_CustGadget;
        //private string DB_LeadTimeGadget;
        //private string DB_FuelProgram;
        //private string DB_AcceptanceGadget;
        //private string DB_MyReports;
        //private string Reporting_Access;
        //private string CarrierSpotMngt_Access;
        //private string Cutom_SSRS_Reports;
        //private string Premium_Reporting;

        #endregion

        #region application access

        private bool AppAccess()
        {
            try
            {
                //Edit(byScheduler_Access, Scheduler_Access);
                //Edit(byFacilityCal_Access, FacilityCal_Access);
                //Edit(byOrderMngr_Access, OrderMngr_Access);
                //Edit(byOrderMngr_ViewAll, OrderMngr_ViewAll);
                //Edit(byOrderMngr_Edit, OrderMngr_Edit);
                //Edit(byOrderMngr_Create, OrderMngr_Create);
                //Edit(byOrderMngr_Cancel, OrderMngr_Cancel);
                //Edit(byRGMngr_Access, RGMngr_Access);
                //Edit(byMyLds_Access, MyLds_Access);
                //Edit(byMyLds_EditLdDetails, MyLds_EditLdDetails);
                //Edit(byMyLds_FlagHPC, MyLds_FlagHPC);
                //Edit(byMyLds_ViewTrackingNotes, MyLds_ViewTrackingNotes);
                //Edit(byMyLds_ViewBOL, MyLds_ViewBOL);
                //Edit(byMyLds_HideMoney, MyLds_HideMoney);
                //Edit(byMyLds_CreateLds, MyLds_CreateLds);
                //Edit(byMyLds_RollLds, MyLds_RollLds);
                //Edit(byMyLds_CancelLds, MyLds_CancelLds);
                //Edit(byMyLds_Appointments, MyLds_Appointments);
                //Edit(byAccouting_Access, Accouting_Access);
                //Edit(byDB_Access, DB_Access);
                //Edit(byDB_FaciltiyAcitivity, DB_FaciltiyAcitivity);
                //Edit(byDB_AccountBalance, DB_AccountBalance);
                //Edit(byDB_HPShipements, DB_HPShipements);
                //Edit(byDB_CostPerUnitGadget, DB_CostPerUnitGadget);
                //Edit(byDB_CustGadget, DB_CustGadget);
                //Edit(byDB_LeadTimeGadget, DB_LeadTimeGadget);
                //Edit(byDB_FuelProgram, DB_FuelProgram);
                //Edit(byDB_AcceptanceGadget, DB_AcceptanceGadget);
                //Edit(byDB_MyReports, DB_MyReports);
                //Edit(byReporting_Access, Reporting_Access);
                //Edit(byCarrierSpotMngt_Access, CarrierSpotMngt_Access);
                //Edit(byCutom_SSRS_Reports, Cutom_SSRS_Reports);
                //Edit(byPremium_Reporting, Premium_Reporting);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string AppAccess_Verify()
        {
            try
            {
                Assert.IsTrue(AppAccess_Open());
                Assert.IsTrue(AppAccess());
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
        }

        private bool AppAccess_Open()
        {
            UIItem byBackButton = new UIItem("Manage user page back button.", By.CssSelector("#user-back-button"));
            UIItem byAppAccess = new UIItem("Application Acees link", By.CssSelector("#permission"));
            try
            {
                Assert.IsTrue(WaitUtilDisplayed(byBackButton));
                Assert.IsTrue(WaitUtilDisplayed(byAppAccess));
                Click(byAppAccess);
                return true;
            }
            catch
            {
                MyLogger.Log("Openinging application access page failed.");
                return false;
            }
        }

        #endregion
    }
}