namespace SWAT.Applications.Claw.ObjectRepository
{
    using OpenQA.Selenium;
    using SWAT.Configuration;
    using SWAT.FrameWork.Utilities;

    public class ApplicationAccessPage : Page
    {
        public ApplicationAccessPage(IWebDriver driver)
        {
            _driver = driver;
            //url = "#manageusers";
            //navigationlink = applicationaccesstab;

        }

        public ApplicationAccessPage(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            //url = "#manageusers";

        }

        private By backButton = By.CssSelector("#user-back-button");
        private By applicationAccessTab = By.CssSelector("#permission");
        private By schedulerUserCanAccess = By.CssSelector("#permission-128");
        private By ediConfiguratorUserCanAccess = By.CssSelector("#permission-129");
        private By distanceCalcUserCanAccess = By.CssSelector("#permission-130");
        private By manageUsers = By.CssSelector("#permission-131");
        private By facilityCalendarUserCanAccess = By.CssSelector("#permission-132");
        private By manageFacilities = By.CssSelector("#permission-133");
        private By allowUserToEditSetting = By.CssSelector("#permission-134");
        private By orderManagerUserCanAccess = By.CssSelector("#permission-136");
        private By manageCustomers = By.CssSelector("#permission-137");
        private By routingGuideUserCanAccess = By.CssSelector("#permission-139");
        private By coyoteGOShowLoadFinder = By.CssSelector("#permission-141");
        private By myLoadsUserCanAccess = By.CssSelector("#permission-142");
        private By orderManagerViewAll = By.CssSelector("#permission-145");
        private By orderManagerEdit = By.CssSelector("#permission-146");
        private By orderManagerCreate = By.CssSelector("#permission-147");
        private By orderManagerCancel = By.CssSelector("#permission-148");
        private By customizeThemeUserCanAccess = By.CssSelector("#permission-149");
        private By carrierAccountingUserCanAccess = By.CssSelector("#permission-150");
        private By carrierPreferencesUserCanAccess = By.CssSelector("#permission-152");
        private By confirmLoadsUserCanAccess = By.CssSelector("#permission-153");
        private By availableLoadsUserCanAccess = By.CssSelector("#permission-154");
        private By dashboardUserCanAccess = By.CssSelector("#permission-165");
        private By myLoadsEditLoadDetails = By.CssSelector("#permission-166");
        private By myLoadsFlagHighPriorityCommodities = By.CssSelector("#permission-167");
        private By myLoadsViewTrackingNotes = By.CssSelector("#permission-169");
        private By dashboardHighPriorityShipments = By.CssSelector("#permission-173");
        private By dashboardMichelinGadget = By.CssSelector("#permission-179");
        private By dashboardRyderGadget = By.CssSelector("#permission-180");
        private By dashboardFuelProgram = By.CssSelector("#permission-183");
        private By carrierAccountingAllowAutoVoucher = By.CssSelector("#permission-185");
        private By myLoadsAllowUserToViewBillOfLading = By.CssSelector("#permission-186");
        private By acceptTendersUserCanAccess = By.CssSelector("#permission-191");
        private By myLoadsEnableRequestAdvance = By.CssSelector("#permission-193");
        private By myLoadsScheduleAndModifyAppointments = By.CssSelector("#permission-197");
        private By manageCarriers = By.CssSelector("#permission-198");
        private By factoringAccountingUserCanAccess = By.CssSelector("#permission-259");
        private By myLoadsAllowUserToReportALumper = By.CssSelector("#permission-215");
        private By ltlFreightQuoteUserCanAccess = By.CssSelector("#permission-218");
        private By myLoadsShowChargesSectionWithoutAccountingAccess = By.CssSelector("#permission-224");
        
        public UIItem BackButton { get { return new UIItem("Applicaton Access>> Back Buttom", this.backButton, _driver); } }

        public UIItem ApplicationAccessTitle { get { return new UIItem("", By.XPath(".//*[@id='permission-details']/div[1]/h2"), _driver); } }
        public UIItem ApplicationAccessTab { get { return new UIItem("Application Access>> Application Access Tab", this.applicationAccessTab, _driver); } }
        public UIItem SchedulerUserCanAccess { get { return new UIItem("Application Access>> Scheduler User Can Access", this.schedulerUserCanAccess, _driver); } }
        public UIItem CoyoteGOShowLoadFinder { get { return new UIItem("Application Access>> CoyoteGO Show Load Finder", this.coyoteGOShowLoadFinder, _driver); } }
        public UIItem MyLoadsUserCanAccess { get { return new UIItem("Application Access>> MyLoads User Can Access", this.myLoadsUserCanAccess, _driver); } }
        public UIItem MyLoadsEditLoadDetails { get { return new UIItem("Application Access>> MyLoads Edit Load Details", this.myLoadsEditLoadDetails, _driver); } }
        public UIItem MyLoadsFlagHighPriorityCommodities { get { return new UIItem("Application Access>> MyLoads Flag High Priority Commodities", this.myLoadsFlagHighPriorityCommodities, _driver); } }
        public UIItem MyLoadsViewTrackingNotes { get { return new UIItem("Application Access>> MyLoads View Tracking Notes", this.myLoadsViewTrackingNotes, _driver); } }
        public UIItem MyLoadsAllowUserToViewBillOfLading { get { return new UIItem("Application Access>> MyLoads Allow User To View Bill Of Lading", this.myLoadsAllowUserToViewBillOfLading, _driver); } }
        public UIItem MyLoadsEnableRequestAdvance { get { return new UIItem("Application Access>> MyLoads Enable Request Advance", this.myLoadsEnableRequestAdvance, _driver); } }
        public UIItem MyLoadsScheduleAndModifyAppointments { get { return new UIItem("Application Access>> MyLoads Schedule And Modify Appointments", this.myLoadsScheduleAndModifyAppointments, _driver); } }
        public UIItem MyLoadsAllowUserToReportALumper { get { return new UIItem("Application Access>> MyLoads Allow User To Report A Lumper", this.myLoadsAllowUserToReportALumper, _driver); } }
        public UIItem MyLoadsShowChargesSectionWithoutAccountingAccess { get { return new UIItem("Application Access>> MyLoads Show Charges Section Without Accounting Access", this.myLoadsShowChargesSectionWithoutAccountingAccess, _driver); } }
        public UIItem CarrierAccountingUserCanAccess { get { return new UIItem("Application Access>> CarrierAccounting User Can Access", this.carrierAccountingUserCanAccess, _driver); } }
        public UIItem FactoringAccountingUserCanAccess { get { return new UIItem("Application Access>> FactoringAccounting User Can Access", this.factoringAccountingUserCanAccess, _driver); } }
        public UIItem CarrierAccountingAllowAutoVoucher { get { return new UIItem("Application Access>> CarrierAccounting Allow Auto Voucher", this.carrierAccountingAllowAutoVoucher, _driver); } }
        public UIItem CarrierPreferencesUserCanAccess { get { return new UIItem("Application Access>> CarrierPreferences User Can Access", this.carrierPreferencesUserCanAccess, _driver); } }
        public UIItem ConfirmLoadsUserCanAccess { get { return new UIItem("Application Access>> ConfirmLoads User Can Access", this.confirmLoadsUserCanAccess, _driver); } }
        public UIItem AvailableLoadsUserCanAccess { get { return new UIItem("Application Access>> AvailableLoads User Can Access", this.availableLoadsUserCanAccess, _driver); } }
        public UIItem DashboardUserCanAccess { get { return new UIItem("Application Access>> Dashboard User Can Access", this.dashboardUserCanAccess, _driver); } }
        public UIItem DashboardHighPriorityShipments { get { return new UIItem("Application Access>> Dashboard High Priority Shipments", this.dashboardHighPriorityShipments, _driver); } }
        public UIItem DashboardMichelinGadget { get { return new UIItem("Application Access>> Dashboard Michelin Gadget", this.dashboardMichelinGadget, _driver); } }
        public UIItem DashboardRyderGadget { get { return new UIItem("Application Access>> Dashboard Ryder Gadget", this.dashboardRyderGadget, _driver); } }
        public UIItem DashboardFuelProgram { get { return new UIItem("Application Access>> Dashboard Fuel Program", this.dashboardFuelProgram, _driver); } }
        public UIItem AcceptTendersUserCanAccess { get { return new UIItem("Application Access>> AcceptTenders User Can Access", this.acceptTendersUserCanAccess, _driver); } }
        public UIItem EdiConfiguratorUserCanAccess { get { return new UIItem("Application Access>> EDI Configurator User Can Access", this.ediConfiguratorUserCanAccess, _driver); } }
        public UIItem DistanceCalcUserCanAccess { get { return new UIItem("Application Access>> Distance Calculator User Can Access", this.distanceCalcUserCanAccess, _driver); } }
        public UIItem ManageUsers { get { return new UIItem("Application Access>> Manage Users", this.manageUsers, _driver); } }
        public UIItem ManageFacilities { get { return new UIItem("Application Access>> Manage Facilities", this.manageFacilities, _driver); } }
        public UIItem AllowUserToEditSetting { get { return new UIItem("Application Access>> Allow User To Edit Setting", this.allowUserToEditSetting, _driver); } }
        public UIItem ManageCustomers { get { return new UIItem("Application Access>> Allow User To Edit Setting", this.manageCustomers, _driver); } }
        public UIItem ManageCarriers { get { return new UIItem("Application Access>> Allow User To Edit Setting", this.manageCarriers, _driver); } }
        public UIItem FacilityCalendarUserCanAccess { get { return new UIItem("Application Access>> Facility user can access", this.facilityCalendarUserCanAccess, _driver); } }
        public UIItem OrderManagerUserCanAccess { get { return new UIItem("Application Access>> Order Manager user can access", this.orderManagerUserCanAccess, _driver); } }
        public UIItem OrderManagerViewAll { get { return new UIItem("Application Access>> Order Manager View all", this.orderManagerViewAll, _driver); } }
        public UIItem OrderManagerEdit { get { return new UIItem("Application Access>> Order Manager edit", this.orderManagerEdit, _driver); } }
        public UIItem OrderManagerCreate { get { return new UIItem("Application Access>> Order Manager create", this.orderManagerCreate, _driver); } }
        public UIItem OrderManagerCancel { get { return new UIItem("Application Access>> Order Manager cancel", this.orderManagerCancel, _driver); } }
        public UIItem RoutingGuideUserCanAccess { get { return new UIItem("Application Access>> Routing Guide User Can Access", this.routingGuideUserCanAccess, _driver); } }
        public UIItem CustomizeThemeUserCanAccess { get { return new UIItem("Application Access>> Customize theme User Can Access", this.customizeThemeUserCanAccess, _driver); } }
        public UIItem LtlFreightQuoteUserCanAccess { get { return new UIItem("Application Access>> LTL Freight Quote User Can Access", this.ltlFreightQuoteUserCanAccess, _driver); } }
    }

    public class EmployeeApplicationAccessHeaders
    {
        private IWebDriver _driver;
        private By scheduler = By.XPath("//*[@id='security-access-form']/div/fieldset[1]/legend");
        private By ediConfigurator = By.XPath("//*[@id='security-access-form']/div/fieldset[2]/legend");
        private By distanceCalculator = By.XPath("//*[@id='security-access-form']/div/fieldset[3]/legend");
        private By admin = By.XPath("//*[@id='security-access-form']/div/fieldset[4]/legend");
        private By facilityCalendar = By.XPath("//*[@id='security-access-form']/div/fieldset[5]/legend");
        private By orderManager = By.XPath("//*[@id='security-access-form']/div/fieldset[6]/legend");
        private By routingGuideManager = By.XPath("//*[@id='security-access-form']/div/fieldset[7]/legend");
        private By customizeTheme = By.XPath("//*[@id='security-access-form']/div/fieldset[8]/legend");
        private By ltlFrieghtQuote = By.XPath("//*[@id='security-access-form']/div/fieldset[9]/legend");

        public EmployeeApplicationAccessHeaders(IWebDriver driver)
        {
            _driver = driver;
        }

        public UIItem Scheduler { get { return new UIItem("Admin >> Application Access >> Scheduler", this.scheduler, _driver); } }
        public UIItem EdiConfigurator { get { return new UIItem("Admin >> Application Access >> EDI Configurator", this.ediConfigurator, _driver); } }
        public UIItem DistanceCalculator { get { return new UIItem("Admin >> Application Access >> DistanceCalculator", this.distanceCalculator, _driver); } }
        public UIItem Admin { get { return new UIItem("Admin >> Application Access >> Admin", this.admin, _driver); } }
        public UIItem FacilityCalendar { get { return new UIItem("Admin >> Application Access >> FacilityCalendar", this.facilityCalendar, _driver); } }
        public UIItem OrderManager { get { return new UIItem("Admin >> Application Access >> OrderManager", this.orderManager, _driver); } }
        public UIItem RoutingGuideManager { get { return new UIItem("Admin >> Application Access >> RoutingGuideManager", this.routingGuideManager, _driver); } }
        public UIItem CustomizeTheme { get { return new UIItem("Admin >> Application Access >> Scheduler", this.customizeTheme, _driver); } }
        public UIItem LtlFrieghtQuote { get { return new UIItem("Admin >> Application Access >> Scheduler", this.ltlFrieghtQuote, _driver); } }
    }

    public class CustomerApplicationAccessHeaders
    {
        private IWebDriver _driver;
        private By scheduler = By.XPath("//*[@id='security-access-form']/div/fieldset[1]/legend");
        private By facilityCalendar = By.XPath("//*[@id='security-access-form']/div/fieldset[2]/legend");
        private By orderManager = By.XPath("//*[@id='security-access-form']/div/fieldset[3]/legend");
        private By routingGuideManager = By.XPath("//*[@id='security-access-form']/div/fieldset[4]/legend");
        private By myLoad = By.XPath("//*[@id='security-access-form']/div/fieldset[5]/legend");
        private By customerAccounting = By.XPath("//*[@id='security-access-form']/div/fieldset[6]/legend");
        private By dashboard = By.XPath("//*[@id='security-access-form']/div/fieldset[7]/legend");
        private By reporting = By.XPath("//*[@id='security-access-form']/div/fieldset[8]/legend");
        private By ltlFrieghtQuote = By.XPath("//*[@id='security-access-form']/div/fieldset[9]/legend");
        private By admin = By.XPath("//*[@id='security-access-form']/div/fieldset[10]/legend");
        private By additionalSettings = By.XPath("//*[@id='security-access-form']/div/fieldset[11]/legend");
        private By customSSRSReports = By.XPath("//*[@id='reports-container']/div/legend");
        private By premiumReporting = By.XPath("//*[@id='tableau-reports-container']/div/legend");

        public CustomerApplicationAccessHeaders(IWebDriver driver)
        {
            _driver = driver;
        }

        public UIItem Scheduler { get { return new UIItem("Admin >> Application Access >> Scheduler", this.scheduler, _driver); } }
        public UIItem FacilityCalendar { get { return new UIItem("Admin >> Application Access >> FacilityCalendar", this.facilityCalendar, _driver); } }
        public UIItem OrderManager { get { return new UIItem("Admin >> Application Access >> OrderManager", this.orderManager, _driver); } }
        public UIItem RoutingGuideManager { get { return new UIItem("Admin >> Application Access >> RoutingGuideManager", this.routingGuideManager, _driver); } }
        public UIItem MyLoad { get { return new UIItem("Admin >> Application Access >> My Load", this.myLoad, _driver); } }
        public UIItem CustomerAccounting { get { return new UIItem("Admin >> Application Access >> DistanceCalculator", this.customerAccounting, _driver); } }
        public UIItem Dashboard { get { return new UIItem("Admin >> Application Access >> Scheduler", this.dashboard, _driver); } }
        public UIItem Reporting { get { return new UIItem("Admin >> Application Access >> Admin", this.reporting, _driver); } }
        public UIItem LtlFrieghtQuote { get { return new UIItem("Admin >> Application Access >> Scheduler", this.ltlFrieghtQuote, _driver); } }
        public UIItem Admin { get { return new UIItem("Admin >> Application Access >> Admin", this.admin, _driver); } }
        public UIItem AdditionalSettings { get { return new UIItem("Admin >> Application Access >> Admin", this.additionalSettings, _driver); } }
        public UIItem CustomSSRSReports { get { return new UIItem("Admin >> Application Access >> Admin", this.customSSRSReports, _driver); } }
        public UIItem PremiumReporting { get { return new UIItem("Admin >> Application Access >> Admin", this.premiumReporting, _driver); } }
    }

    public class CarrierApplicationAccessHeaders
    {
        private IWebDriver _driver;
        private By scheduler = By.XPath("//*[@id='security-access-form']/div/fieldset[1]/legend");
        private By coyoteGo = By.XPath("//*[@id='security-access-form']/div/fieldset[2]/legend");
        private By myLoad = By.XPath("//*[@id='security-access-form']/div/fieldset[3]/legend");
        private By carrierAccounting = By.XPath("//*[@id='security-access-form']/div/fieldset[4]/legend");
        private By carrierPreferences = By.XPath("//*[@id='security-access-form']/div/fieldset[5]/legend");
        private By confirmLoads = By.XPath("//*[@id='security-access-form']/div/fieldset[6]/legend");
        private By availableLoads = By.XPath("//*[@id='security-access-form']/div/fieldset[7]/legend");
        private By dashboard = By.XPath("//*[@id='security-access-form']/div/fieldset[7]/legend");
        private By acceptTender = By.XPath("//*[@id='security-access-form']/div/fieldset[8]/legend");

        public CarrierApplicationAccessHeaders(IWebDriver driver)
        {
            _driver = driver;
        }

        public UIItem Scheduler { get { return new UIItem("Admin >> Application Access >> Scheduler", this.scheduler, _driver); } }
        public UIItem CoyoteGo { get { return new UIItem("Admin >> Application Access >> CoyoteGo", this.coyoteGo, _driver); } }
        public UIItem MyLoad { get { return new UIItem("Admin >> Application Access >> My Load", this.myLoad, _driver); } }
        public UIItem CarrierAccounting { get { return new UIItem("Admin >> Application Access >> CarrierAccounting", this.carrierAccounting, _driver); } }
        public UIItem CarrierPreferences { get { return new UIItem("Admin >> Application Access >> CarrierPreferences", this.carrierPreferences, _driver); } }
        public UIItem ConfirmLoads { get { return new UIItem("Admin >> Application Access >> ConfirmLoads", this.confirmLoads, _driver); } }
        public UIItem AvailableLoads { get { return new UIItem("Admin >> Application Access >> AvailableLoads", this.availableLoads, _driver); } }

        public UIItem Dashboard { get { return new UIItem("Admin >> Application Access >> Scheduler", this.dashboard, _driver); } }

        public UIItem AcceptTender { get { return new UIItem("Admin >> Application Access >> Scheduler", this.acceptTender, _driver); } }
    }

    public class FacilityApplicationAccessHeaders
    {
        private IWebDriver _driver;
        private By scheduler = By.XPath("//*[@id='security-access-form']/div/fieldset[1]/legend");
        private By facilityCalendar = By.XPath("//*[@id='security-access-form']/div/fieldset[2]/legend");
        private By orderManager = By.XPath("//*[@id='security-access-form']/div/fieldset[3]/legend");
        private By myLoads = By.XPath("//*[@id='security-access-form']/div/fieldset[4]/legend");
        private By dashboard = By.XPath("//*[@id='security-access-form']/div/fieldset[5]/legend");
        private By additionalSettings = By.XPath("//*[@id='security-access-form']/div/fieldset[6]/legend");

        public FacilityApplicationAccessHeaders(IWebDriver driver)
        {
            _driver = driver;
        }

        public UIItem Scheduler { get { return new UIItem("Admin >> Application Access >> Scheduler", this.scheduler, _driver); } }
        public UIItem FacilityCalendar { get { return new UIItem("Admin >> Application Access >> FacilityCalendar", this.facilityCalendar, _driver); } }
        public UIItem OrderManager { get { return new UIItem("Admin >> Application Access >> OrderManager", this.orderManager, _driver); } }
        public UIItem MyLoads { get { return new UIItem("Admin >> Application Access >> MyLoads", this.myLoads, _driver); } }
        public UIItem Dashboard { get { return new UIItem("Admin >> Application Access >> Scheduler", this.dashboard, _driver); } }
        public UIItem AdditionalSettings { get { return new UIItem("Admin >> Application Access >> Scheduler", this.additionalSettings, _driver); } }
    }

    public class FactoringCompanyApplicationAccessHeaders
    {
        private IWebDriver _driver;
        private By accounting = By.XPath("//*[@id='security-access-form']/div/fieldset[1]/legend");
        

        public FactoringCompanyApplicationAccessHeaders(IWebDriver driver)
        {
            _driver = driver;
        }

        public UIItem Accounting { get { return new UIItem("Admin >> Application Access >> Scheduler", this.accounting, _driver); } }
        
    }


}