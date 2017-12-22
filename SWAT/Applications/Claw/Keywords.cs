using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAT.Applications.Claw
{
    using SWAT.FrameWork.Utilities;
    using SWAT.Data;
    using SWAT.Logger;
    using TestStartInfo = SWAT.Configuration.TestStartInfo;

    public class Keywords
    {
        public TestStartInfo _teststartinfo { get; set; }
        private string _datacolumn;

        public Keywords(TestStartInfo teststartinfo, string datacolumn)
        {
            this._teststartinfo = teststartinfo;
            _datacolumn = datacolumn;
        }

        public string ExecuteTestStep(string action, DataManager datamanager)
        {
            string[] arrAction = action.Split('.');
            string ActualResults = "StepNotImplemented";
            switch (arrAction[0])
            {                
                case "TESTCASE":
                    ActualResults = "";
                    break;
                case "SERVICE":
                    Service Service = new Service(_teststartinfo, datamanager);
                    switch (arrAction[1])
                    {
                        case "COPYROW":
                            ActualResults = Service.CopyRow();
                            break;
                        case "COPYCOLUMN":
                            ActualResults = Service.CopyColoumn(arrAction[2]);
                            break;
                        case "IMPORTEXCEL":
                            ActualResults = Service.ImportExcel();
                            break;
                        case "OPENBROWSER":
                            ActualResults = Service.OpenBrower();
                            break;
                        case "COPYCELL":
                            ActualResults = Service.CopyCell();
                            break;
                        case "WAIT":
                            ActualResults = Service.Wait(arrAction[2]);
                            break;
                    }
                    Service = null;
                    break;
                case "CLAW":
                    CLAW Claw = new CLAW(_teststartinfo, datamanager);
                    switch (arrAction[1])
                    {
                        case "PASSTEST":
                            ActualResults = "Pass";
                            break;
                        case "OPEN":
                            ActualResults = Claw.NavigateToURL();
                            break;

                        case "LOGOUT":
                            ActualResults = Claw.LogOut();
                            break;

                        case "LOGIN":
                            ActualResults = Claw.Login();
                            break;

                        case "NAVIGATETO":
                            ActualResults = Claw.NavigateTo(arrAction[2]);
                            break;

                        case "CREATELTLLOAD":
                            CreateLTLLoad CreateLTLLoad = new CreateLTLLoad(_teststartinfo, datamanager);
                            switch (arrAction[2])
                            {
                                case "GETQUOTES":
                                    ActualResults = CreateLTLLoad.GetQuote_Submit();
                                    break;
                                case "UIVERIFY":
                                    ActualResults = CreateLTLLoad.UIVerify();
                                    break;
                                case "ADDCOMMODITY":
                                    ActualResults = CreateLTLLoad.AddCommodity();
                                    break;
                                case "DELETECOMMODITY":
                                    ActualResults = CreateLTLLoad.DeleteCommodity();
                                    break;
                                case "EDITCOMMODITY":
                                    ActualResults = CreateLTLLoad.EditCommodity();
                                    break;
                                case "GETQUOTE":
                                    ActualResults = CreateLTLLoad.QuoteInformation();
                                    break;
                                case "SELECTCARRIER":
                                    ActualResults = CreateLTLLoad.SelectCarrier();
                                    break;
                                case "PICKUP":
                                    ActualResults = CreateLTLLoad.Pickup();
                                    break;
                                case "DELIVERY":
                                    ActualResults = CreateLTLLoad.Delivery();
                                    break;
                                case "CONFIRMANDSUBMIT":
                                    ActualResults = CreateLTLLoad.ConfirmAndSubmit();
                                    break;
                                case "OPENQUOTECREATELTL":
                                    //ActualResults = CreateLTLLoad.NavigateToQuoteAndCreateLTL();
                                    break;
                                case "SEARCHORDELETECUSTOMER":
                                    //ActualResults = CreateLTLLoad.SearchOrDeleteCustomer();
                                    break;
                                case "GETLOADID":
                                    ActualResults = CreateLTLLoad.GetLoadId();
                                    break;
                            }
                            break;

                        case "TENDER":
                            Tender Tender = new Tender(_teststartinfo, datamanager);
                            switch (arrAction[2])
                            {
                                case "ACCEPTTENDER":
                                    ActualResults = Tender.AcceptTender();
                                    break;
                                case "ACCEPTSPOTOFFER":
                                    ActualResults = Tender.AcceptSpotOffer();
                                    break;
                                default:
                                    ActualResults = Tender.AcceptTender();
                                    break;
                            }
                            break;

                        case "DASHBOARD":
                            DashBoard DashBoard = new DashBoard(_teststartinfo, datamanager);
                            switch (arrAction[2])
                            {
                                case "GADGETS":
                                    switch (arrAction[3])
                                    {
                                        case "VERIFY":
                                            ActualResults = DashBoard.Gadgets_Verify();
                                            break;
                                    }
                                    break;
                                case "REPORT":
                                    switch (arrAction[3])
                                    {
                                        case "OPEN":
                                            ActualResults = DashBoard.Report_Open();
                                            break;
                                    }
                                    break;
                                case "CALENDAR":
                                    switch (arrAction[3])
                                    {
                                        case "TOMYLOAD":
                                            ActualResults = DashBoard.Calendar();
                                            break;
                                    }
                                    break;
                                case "UIVERIFY":
                                   // ActualResults = DashBoard.UIVerify();
                                    break;
                                case "GETLOADCOUNT":
                                   // ActualResults = DashBoard.GetMyLoadsCounts();
                                    break;
                            }
                            DashBoard = null;
                            break;

                        case "SCHEDULELOADS":
                            ScheduleLoads ScheduleLoads = new ScheduleLoads(_teststartinfo, datamanager);
                            switch (arrAction[2])
                            {
                                case "SEARCH":
                                    ActualResults = ScheduleLoads.Search();
                                    break;
                                case "UPDATEDATE":
                                    ActualResults = ScheduleLoads.UpdateDate();
                                    break;
                            }
                            break;

                        case "ROUTINGGUIDE":
                            RouringGuide RouringGuide = new RouringGuide(_teststartinfo, datamanager);
                            switch (arrAction[2])
                            {
                                case "SELECT":
                                    ActualResults = RouringGuide.VerifyLane();
                                    break;
                                case "ADVSEARCH":
                                    ActualResults = RouringGuide.AdvSearch();
                                    break;
                                case "LANE":
                                    switch (arrAction[3])
                                    {
                                        case "VERIFY":
                                            ActualResults = RouringGuide.VerifyLane();
                                            break;
                                        case "VERIFYDETAILS":
                                            ActualResults = RouringGuide.VerifyLaneDetails();
                                            break;
                                    }
                                    break;
                                case "NAVIGATE":
                                    ActualResults = RouringGuide.Navigate();
                                    break;
                                case "SEARCHCUSTOMER":
                                    ActualResults = RouringGuide.Customer_Search();
                                    break;
                                case "ADD":
                                    switch (arrAction[3])
                                    {
                                        case "VERIFY":
                                            ActualResults = RouringGuide.Add_Verify();
                                            break;
                                    }
                                    break;
                                case "ADDLANE":
                                    switch (arrAction[3])
                                    {
                                        case "VERIFY":
                                            ActualResults = RouringGuide.AddLane_Verify();
                                            break;
                                    }
                                    break;
                                case "ADDCARRIER":
                                    switch (arrAction[3])
                                    {
                                        case "VERIFY":
                                            ActualResults = RouringGuide.AddCarrier_Verify();
                                            break;
                                        case "ADD":
                                            ActualResults = RouringGuide.AddCarrier();
                                            break;
                                        case "UIVERIFY":
                                            ActualResults = RouringGuide.UIVerify();
                                            break;
                                    }
                                    break;
                            }
                            RouringGuide = null;
                            break;

                        case "CALCULATEDISTANCE":
                            CalculateDistance cd = new CalculateDistance(_teststartinfo, datamanager);
                            switch (arrAction[2])
                            {
                                case "VERIFY":
                                    ActualResults = cd.CalucateDistance();
                                    break;    
                            }
                            cd = null;
                            break;

                        #region Accounting Keyword
                        case "ACCOUNTING":
                            Accouting Accouting = new Accouting(_teststartinfo, datamanager);
                            switch (arrAction[2])
                            {
                                case "FILTER":
                                    ActualResults = Accouting.AccFilter();
                                    break;
                                case "SORT":
                                    ActualResults = Accouting.SortColumn();
                                    break;
                                case "OPENLOAD":
                                    ActualResults = Accouting.OpenLoad();
                                    break;
                                case "GETLOADID":
                                    ActualResults = Accouting.GetLoadID();
                                    break;
                                case "VERIFYCONTROLS":
                                    ActualResults = Accouting.VerifyControls();
                                    break;
                                case "FILTERANDVERIFY":
                                    ActualResults = Accouting.FilterAndVerify();
                                    break;
                                case "COLVERIFY":
                                    ActualResults = Accouting.FactoringColVerify();
                                    break;
                                case "GETINVOICEID":
                                    ActualResults = Accouting.GetInvoiceID();
                                    break;
                                case "SEARCHLOAD":
                                    ActualResults = Accouting.SearchLoad();
                                    break;
                                case "VERIFYSEARCHRESULTS":
                                    ActualResults = Accouting.VerifySearchResults(datamanager);
                                    break;
                                case "VERIFYBASICSEARCH":
                                    ActualResults = Accouting.VerifyBasicSearch();
                                    break;
                                case "DEFAULTSEARCH":
                                    ActualResults = Accouting.DefaultSearch();
                                    break;
                                case "VERIFYDEFAULTVIEW":
                                    ActualResults = Accouting.VerifyDefaultView();
                                    break;
                                case "GETPAYDATE":
                                    ActualResults = Accouting.GetPayDate();
                                    break;
                            }
                            Accouting = null;
                            break;
                        #endregion

                        case "ADMIN":
                            Admin Admin = new Admin(_teststartinfo, datamanager);
                            switch (arrAction[2])
                            {
                                case "ADMINTABVISIBLE":
                                    ActualResults = Admin.IsAdminTabVisible();
                                    break;
                                case "VERIFY":
                                    ActualResults = Admin.VerifyAdminPage();
                                    break;
                                case "SEARCH":
                                    ActualResults = Admin.Search();
                                    break;
                                case "SEARCHANDOPEN":
                                     ActualResults = Admin.SearchAndOpen();
                                    break;
                                case "NAVTOHOMEPAGE":
                                    ActualResults = Admin.NavigateToHomePage();
                                    break;
                                case "UIVERIFY":
                                    ActualResults = Admin.UIVerify();
                                    break;
                            }
                            break;

                        #region User Keyword
                        case "USER":
                            User User = new User(_teststartinfo, datamanager);
                            switch (arrAction[2])
                            {
                                case "SEARCH":
                                    ActualResults = User.Search();
                                    break;
                                case "NAVTOALL":
                                    ActualResults = User.NavigateToAllPage();
                                    break;
                                case "OPEN":
                                    ActualResults = "test";
                                    break;
                                case "EMULATE":
                                    ActualResults = User.Emulate();
                                    break;
                                case "SEARCHANDOPEN":
                                    ActualResults = User.SearchAndOpen();
                                    break;
                                case "APPACCESS":
                                    ApplicationAccess ApplicationAccess = new ApplicationAccess(_teststartinfo, datamanager);
                                    switch (arrAction[3])
                                    {
                                        case "VERIFY":
                                            ActualResults = ApplicationAccess.ApplicationAccessVerify();
                                            break;
                                    }
                                    break;
                                case "CHANGEPASSWORD":
                                    switch (arrAction[3])
                                    {
                                        case "VERIFY":
                                            ActualResults = User.ChangePassword();
                                            break;
                                        case "CHANGE":
                                            ActualResults = User.ChangeUserPassword();
                                            break;
                                    }
                                    break;

                                case "GENERALINFO":
                                    switch (arrAction[3])
                                    {
                                        case "VERIFY":
                                            ActualResults = User.GenralInfo_Verify();
                                            break;
                                        case "UPDATEANDVERIFY":
                                            ActualResults = User.GenralInfo_UpdateAndVerify();
                                            break;
                                    }
                                    break;
                                case "PROFILE":
                                    switch (arrAction[3])
                                    {
                                        case "NAVIGATE":
                                            ActualResults = User.Profile_Nav();
                                            break;
                                        case "ADDFACILITY":
                                            ActualResults = User.AddFacility();
                                            break;
                                        case "REMOVEFACILITY":
                                            ActualResults = User.RemoveFacility();
                                            break;
                                        case "VERIFYDETAILS":
                                            ActualResults = User.VerifySettingsDetails();
                                            break;
                                        case "CANEDIT":
                                            ActualResults = User.CanEditProfile();
                                            break;
                                    }
                                    break;
                                case "BACKTOSEARCH":
                                    ActualResults = User.VerifyBackToSearch();
                                    break;
                                case "VERIFYSYSTEMMESSAGE":
                                    ActualResults = User.VerifySystemMessage();
                                    break;
                            }
                            User = null;
                            break;
                        #endregion

                        case "FACILITY":
                            Facility Facility = new Facility(_teststartinfo, datamanager);
                            switch (arrAction[2])
                            {
                                case "SEARCHANDOPEN":
                                    ActualResults = "Use New Keyword Claw.Admin.SearchAndOpen";
                                    break;
                                case "VERIFYDETAILS":
                                    ActualResults = Facility.VerifyInfo();
                                    break;
                            }
                            Facility = null;
                            break;

                        case "FACILITYCALENDAR":
                            FacilityCalendar FacilityCalendar = new FacilityCalendar(_teststartinfo, datamanager);
                            switch (arrAction[2])
                            {
                                case "NAVIGATION":
                                    ActualResults = FacilityCalendar.Navigate();
                                    break;
                                case "OPENEVENT":
                                    ActualResults = FacilityCalendar.OpenEvent();
                                    break;
                                case "GETLOADDETAILS":
                                    ActualResults = FacilityCalendar.GetLoadDetails();
                                    break;
                                case "OPENLOAD":
                                    ActualResults = FacilityCalendar.OpenLoad();
                                    break;
                            }
                            break;
                        case "CUSTOMER":
                            Customer Customer = new Customer(_teststartinfo, datamanager);
                            switch (arrAction[2])
                            {
                                case "SEARCHANDOPEN":
                                    ActualResults = "Use New Keyword Claw.Admin.SearchAndOpen";
                                    break;
                                case "VERIFYDETAILS":
                                    ActualResults = Customer.VerifyInfo();
                                    break;
                                case "APPACCESS":
                                    switch (arrAction[3])
                                    {
                                        case "VERIFY":
                                            ActualResults = Customer.AppAccess_Verify();
                                            break;
                                    }
                                    break;
                                case "MANAGECUSTOMERS":
                                    ManageCustomers ManageCustomers = new ManageCustomers(_teststartinfo, datamanager);
                                    switch (arrAction[3])
                                    {
                                        case "MYLOADSSTATUSCHECKANDUPDATE":
                                            ActualResults = ManageCustomers.MyLoadsStatusCheckAndUpdate();
                                            break;
                                        case "CUTOFFTIMESTATUSCHECKANDUPDATE":
                                            ActualResults = ManageCustomers.CutOffTimeStatusCheckAndUpdate(datamanager);
                                            break;
                                        case "TENDERSETTINGSSTATUSCHECKANDUPDATE":
                                            ActualResults = ManageCustomers.TenderSettingsStatusCheckAndUpdate();
                                            break;
                                        case "COMMODITYQUANTITYVALIDATIONSTATUSCHECKANDUPDATED":
                                            ActualResults = ManageCustomers.CommodityQuantityValidationStatusCheckAndUpdated();
                                            break;
                                    }
                                    break;
                            }
                            Customer = null;
                            break;
                        case "CARRIER":
                            Carriers Carriers = new Carriers(_teststartinfo, datamanager);
                            switch (arrAction[2])
                            {
                                case "SEARCHANDOPEN":
                                    ActualResults = "Use New Keyword Claw.Admin.SearchAndOpen";
                                    break;
                                case "VERIFYDETAILS":
                                    ActualResults = Carriers.VerifyInfo();
                                    break;
                                case "USERAPPACCESS":
                                    switch (arrAction[3])
                                    {
                                        case "VERIFY":
                                            ActualResults = Carriers.UserAppAccess_Verify();
                                            break;
                                    }
                                    break;
                                case "APPACCESS":
                                    ApplicationAccess ApplicationAccess = new ApplicationAccess(_teststartinfo, datamanager);
                                    switch (arrAction[3])
                                    {
                                        case "VERIFY":
                                            ActualResults = ApplicationAccess.ApplicationAccessVerify();
                                            break;
                                        case "VERIFYACCESSHEADERS":
                                            ActualResults = ApplicationAccess.VerifyAccessHeaders();
                                            break;
                                    }
                                    break;
                            }
                            Facility = null;
                            break;
                        case "FACTORING":
                            Factoring Factoring = new Factoring(_teststartinfo, datamanager);
                            switch (arrAction[2])
                            {
                                case "APPACCESS":
                                    switch (arrAction[3])
                                    {
                                        case "VERIFY":
                                            ApplicationAccess ApplicationAccess = new ApplicationAccess(_teststartinfo, datamanager);
                                            ActualResults = ApplicationAccess.ApplicationAccessVerify();
                                            break;
                                    }
                                    break;
                                case "UIVERIFY":                                   
                                    ActualResults = Factoring.UIVerify();
                                    break;
                            }
                            break;

                        #region AVILLOAD Keyword
                        case "AVILLOAD":
                            AvailableLoad AviLoad = new AvailableLoad(_teststartinfo, datamanager);
                            switch (arrAction[2])
                            {
                                case "OPEN":
                                    ActualResults = AviLoad.BrowseAndFilter_NavigateTo();
                                    break;
                                case "ADDTRUCK":
                                    ActualResults = AviLoad.PostAndMatch_Add_Truck();
                                    break;
                                case "MAKEOFFER":
                                    switch (arrAction[3])
                                    {
                                        case "OPEN":
                                            ActualResults = AviLoad.BrowseAndFilter_MakeOffer_Open();
                                            break;
                                        case "CLOSE":
                                            ActualResults = AviLoad.BrowseAndFilter_MakeOffer_Close();
                                            break;
                                        case "SUBMITOFFER":
                                            ActualResults = AviLoad.BrowseAndFilter_MakeOffer();
                                            break;
                                    }
                                    break;
                                case "ADVFILTER":
                                    ActualResults = AviLoad.BrowseAndFilter_Filter();
                                    break;
                                case "RESULT":
                                    switch (arrAction[3])
                                    {
                                        case "VERIFY":
                                            ActualResults = AviLoad.BrowseAndFilter_SearchResult_Verify();
                                            break;
                                    }
                                    break;
                                case "MATCHTRUCKS":
                                    ActualResults = AviLoad.PostAndMatch_Match_Trucks();
                                    break;
                                case "REMOVETRUCK":
                                    ActualResults = AviLoad.PostAndMatch_Remove_Truck();
                                    break;
                                case "GETLOAD_WO_OFFER":
                                    ActualResults = AviLoad.BrowseAndFilter_GetAvilableLoadDetails();
                                    break;
                                case "VERIFYFILTERSANDSEARCH":
                                    ActualResults = AviLoad.BrowseAndFilter_VerifyFiltersAndSearch();
                                    break;
                                case "CLEARFILTERANDVERIFY":
                                    ActualResults = AviLoad.BrowseAndFilter_ClearFilterAndVerify();
                                    break;
                                case "VERIFYSEARCHRESULT":
                                    ActualResults = AviLoad.BrowseAndFilter_VerifySearchResult();
                                    break;
                                case "VERIFYRECOMMENDEDLOADS":
                                    ActualResults = AviLoad.VerifyRecommendedLoads();
                                    break;
                                case "ADDTOPREFERREDLANE":
                                    ActualResults = AviLoad.AddToPreferredLane();
                                    break;
                                case "VERIFYNEWLANEONPREFERENCEPAGE":
                                    ActualResults = AviLoad.VerifyNewLaneOnPreferencePage();
                                    break;
                                case "REMOVEANDUNDORECOMMENDEDLOAD":
                                    ActualResults = AviLoad.RemoveRecommendedLoad();
                                    break;
                                case "UNDO_REMOVERECOMMENDEDLOAD":
                                    ActualResults = AviLoad.UnDoRemoveRecommendedLoad();
                                    break;
                                case "REMOVEOFFER":
                                    ActualResults = AviLoad.BrowseAndFilter_RemoveOffer();
                                    break;
                                case "ADDTOPREFERREDLANEFROMCURTAIN":
                                    ActualResults = AviLoad.AddToPreferredLaneFromCurtain();
                                    break;
                            }
                            AviLoad = null;
                            break;
                        #endregion

                        case "CREATELOAD":
                            CreateLoad CreateLoad = new CreateLoad(_teststartinfo, datamanager);
                            switch (arrAction[2])
                            {
                                case "COMMODITYDETAILSFILL":
                                    ActualResults = CreateLoad.CommodityDetails_Fill();
                                    break;
                                case "COMMODITYADD":
                                    ActualResults = CreateLoad.Commodity_Add();
                                    break;
                                case "STOPDETAILSFILL":
                                    ActualResults = CreateLoad.StopDetails_Fill();
                                    break;
                                case "STOPADD":
                                    ActualResults = CreateLoad.Stop_Add();
                                    break;
                                case "FILL":
                                    ActualResults = CreateLoad.Fill();
                                    break;
                                case "SUBMIT":
                                    ActualResults = CreateLoad.Submit();
                                    break;
                                case "GETLOADID":
                                    ActualResults = CreateLoad.GetLoadID();
                                    break;
                                case "GETSHIPPERDETAILS":
                                    ActualResults = CreateLoad.GetShipperDetails();
                                    break;
                                case "OPENLOADID":
                                    ActualResults = CreateLoad.OpenLoadId();
                                    break;
                                case "VERIFYTARP":
                                    ActualResults = CreateLoad.VerifyTarp();
                                    break;
                                case "VERIFYDUPLICATESHIPMENT":
                                    ActualResults = CreateLoad.VerifyForDuplicateShipmentId();
                                    break;
                                case "SAVELOADTEMPLATE":
                                    ActualResults = CreateLoad.SaveLoadTemplate();
                                    break;
                                case "VERIFYTEMPLATEDATA":
                                    ActualResults = CreateLoad.VerifyTemplateData();
                                    break;
                                case "LOADNEWTEMPLATE":
                                    ActualResults = CreateLoad.LoadNewTemplate();
                                    break;
                            }
                            CreateLoad = null;
                            break;

                        case "MYLOAD":
                            MyLoad MyLoad = new MyLoad(_teststartinfo, datamanager);
                            switch (arrAction[2])
                            {
                                case "SEARCH":
                                    ActualResults = MyLoad.Search();
                                    break;
                                case "VERIFYSEARCH":
                                    ActualResults = MyLoad.ValidateResult();
                                    break;
                                case "ADVSEARCH":
                                    ActualResults = MyLoad.AdvancedSearch();
                                    break;
                                case "OPENLOAD":
                                    ActualResults = MyLoad.SearchAndOpenLoad();
                                    break;
                                case "TOGGLECOL":
                                    switch (arrAction[3])
                                    {
                                        case "SELECT":
                                            ActualResults = MyLoad.ToggleColumn();
                                            break;
                                        case "VERIFY":
                                            ActualResults = MyLoad.DisplayedColVerification();
                                            break;
                                    }
                                    break;
                                case "GETDETAILS":
                                    ActualResults = MyLoad.GetLoadDetails(datamanager);
                                    break;
                                case "DOWNLOADPOD":
                                    ActualResults = MyLoad.DownloadPOD();
                                    break;
                                case "DOWNLOADBOL":
                                    ActualResults = MyLoad.DownloadBOL();
                                    break;
                                case "OPENSEARCHEDLOAD":
                                    ActualResults = MyLoad.OpenFromSearchResult();
                                    break;
                                case "SEARCHLOADFROMTOPNAV":
                                    ActualResults = MyLoad.SearchLoadFromTopNav();
                                    break;
                                case "VERIFYSEARCHRESULT":
                                    ActualResults = MyLoad.VerifySearchResult();
                                    break;
                                case "UIVERIFY":
                                   // ActualResults = MyLoad.UIVerify();
                                    break;
                            }
                            MyLoad = null;
                            break;

                        case "MYLOAD2":
                            MyLoad2 MyLoad2 = new MyLoad2(_teststartinfo, datamanager);
                            switch (arrAction[2])
                            {
                                case "EXECUTE":
                                    ActualResults = MyLoad2.Execute();
                                    break;
                                case "UIVERIFY":
                                    ActualResults = MyLoad2.UIVerify();
                                    break;
                                case "NAVIGATETO":
                                    ActualResults = MyLoad2.NavigateTo();
                                    break;
                            }
                            break;
                        #region CREATEORDER Keyword
                        case "CREATEORDER":
                            {
                                CreateOrder CreateOrder = new CreateOrder(_teststartinfo, datamanager);
                                switch (arrAction[2])
                                {
                                    case "FILL":
                                        ActualResults = CreateOrder.CreateOrderFill();
                                        break;
                                    case "NAVIGATE":
                                        ActualResults = CreateOrder.Navigate();
                                        break;
                                    case "CANCEL":
                                        ActualResults = CreateOrder.Cancel();
                                        break;
                                    case "SAVE":
                                        ActualResults = CreateOrder.SaveOrder();
                                        break;
                                    case "SHIPORDER":
                                        ActualResults = CreateOrder.ShipOrder();
                                        break;
                                    case "VERIFY":
                                        ActualResults = CreateOrder.UIVerify();
                                        break;
                                    case "SHIPPINGUINT":
                                        switch (arrAction[3])
                                        {
                                            case "ADD":
                                                ActualResults = CreateOrder.AddAnotherShippingUnit();
                                                break;
                                            case "FILL":
                                                ActualResults = CreateOrder.ShippingUnit_Fill();
                                                break;
                                            case "ADDANDFILL":
                                                ActualResults = CreateOrder.ShippingUnit_AddAndFill();
                                                break;
                                        }
                                        break;
                                    case "COMMODITY":
                                        switch (arrAction[3])
                                        {
                                            case "ADD":
                                                ActualResults = CreateOrder.AddAnotherCommodity();
                                                break;
                                            case "FILL":
                                                ActualResults = CreateOrder.CommodityDetails_Fill();
                                                break;
                                            case "SELECTALL":
                                                ActualResults = CreateOrder.CommoditySelectAll();
                                                break;
                                            case "SELECTNONE":
                                                ActualResults = CreateOrder.CommoditySelectNone();
                                                break;
                                            case "SELECTMOVE":
                                                ActualResults = CreateOrder.CommodityMove();
                                                break;
                                            case "SELECTDELETE":
                                                ActualResults = CreateOrder.CommodityDelete();
                                                break;
                                        }
                                        break;                       
                                    case "VERIFYDEFAULTVALUES":
                                        ActualResults = CreateOrder.VerifyDefaultValues();
                                        break;
                                    case "VERIFYFACILITY":
                                        ActualResults = CreateOrder.VerifyFacilityNameAndAddress();
                                        break;
                                    case "VERIFYERROR":
                                        ActualResults = CreateOrder.VerifyError();
                                        break;
                                    case "GETLOADID":
                                        ActualResults = CreateOrder.GetLoadId();
                                        break;
                                }
                                CreateOrder = null;
                            }
                            break;
                        #endregion

                        case "MYORDER":
                            {
                                MyOrder MyOrder = new MyOrder(_teststartinfo, datamanager);
                                switch (arrAction[2])
                                {
                                    case "NAVIGATE":
                                        ActualResults = MyOrder.Navigate();
                                        break;
                                    case "OPENORDERTOSHIP":
                                        ActualResults = MyOrder.OpenOrderToShip();
                                        break;
                                    case "FILLSHIPPERDETAILS":
                                        ActualResults = MyOrder.FillShipperDetails();
                                        break;
                                    case "FILLSHIPPINGANDCOMMODITIESDETAILS":
                                        ActualResults = MyOrder.FillShippingsAndCommoditiesDetails();
                                        break;
                                    case "SAVEORDER":
                                        ActualResults = MyOrder.SaveOrder();
                                        break;
                                    case "SEARCHORDER":
                                        ActualResults = MyOrder.SearchOrder();
                                        break;
                                    case "VERIFY":
                                        ActualResults = MyOrder.Verify();
                                        break;
                                    case "VERIFYORDER":
                                        ActualResults = MyOrder.VerifyOrder();
                                        break;
                                    case "FILLCONTACTDETAILS":
                                        ActualResults = MyOrder.FillContactDetails();
                                        break;
                                    case "CONSOLIDATEORDERS":
                                        ActualResults = MyOrder.ConsolidateOrders();
                                        break;
                                    case "ADVANCESEARCH":
                                        ActualResults = MyOrder.AdvanceSearch();
                                        break;
                                    case "SETREFNUMBER":
                                        ActualResults = MyOrder.SetRefNumber(null);
                                        break;
                                }
                            }
                            break;
                        case "LOAD":
                            Load Load = new Load(_teststartinfo, datamanager);
                            switch (arrAction[2])                            
                            {    
                                case "VERIFY":                                    
                                    ActualResults = Load.VerifyDetails();
                                    break;
                                case "INQUIRE":
                                    ActualResults = Load.Inquire();
                                    break;
                                case "VERIFYBOLEDITSTATUS":
                                    ActualResults = Load.VerifyBolEditStatus();
                                    break;
                                case "VERIFYPODEDITSTATUS":
                                    ActualResults = Load.VerifyPodEditStatus();
                                    break;
                                case "VERIFYCHARGES":
                                    ActualResults = Load.VerifyCharges();
                                    break;
                                case "VERIFYPICKUPCOMMODITIES":
                                    ActualResults = Load.VerifyPickUpCommodities();
                                    break;
                                case "RETENDER":
                                    ActualResults = Load.ReTender();
                                    break;
                                case "CANCELLOAD":
                                    ActualResults = Load.CancelLoad();
                                    break;
                                case "GETPRICINGDETAILS":
                                    ActualResults = Load.GetPricingDetails();
                                    break;
                                case "GETFACILITYDETAILS":
                                    ActualResults = Load.GetFacilityDetails();
                                    break;
                            }
                            Load = null;
                            break;

                        #region LOADDETAILS Keyword
                        case "LOADDETAILS":
                            LoadDetails LoadDetails = new LoadDetails(_teststartinfo, datamanager);
                            switch (arrAction[2])
                            {
                                case "UIVERIFY":
                                    ActualResults = LoadDetails.UIVerify();
                                    break;
                                case "EDITPICKUP":
                                    ActualResults = LoadDetails.PickUpDetails_Edit();
                                    break;
                                case "EDITDELIVERY":
                                    ActualResults = LoadDetails.DeliveryDetails_Edit();
                                    break;
                                case "VERIFYTRACKINGNOTES":
                                    ActualResults = LoadDetails.VerifyTrackingNotes();
                                    break;
                                case "COMMODITIES":
                                    switch (arrAction[3])
                                    {
                                        case "VERIFY":
                                            ActualResults = LoadDetails.Commodities_Verify();
                                            break;
                                    }
                                    break;
                                case "INQUIRE":
                                    //ActualResults = LoadDetails.Inquire();
                                    break;
                                case "VERIFYCONTROLS":
                                    ActualResults = LoadDetails.VerifyControls();
                                    break;
                                case "DISPATCHDRIVER":
                                    ActualResults = LoadDetails.DispatchDriver(datamanager);
                                    break;
                                case "VERIFYDISPATCHFIELDS":
                                    ActualResults = LoadDetails.VerifyDispatchFields();
                                    break;
                                case "UPDATEDISPATCHINFO":
                                    ActualResults = LoadDetails.UpdateDispatchInfo();
                                    break;
                                case "UPLOADDOCUMENT":
                                    ActualResults = LoadDetails.UploadDocument();
                                    break;
                                case "REPORTLUMPER":
                                    switch (arrAction[3])
                                    {
                                        case "VERIFY":
                                            ActualResults = LoadDetails.VerifyLumperLink();
                                            break;
                                        case "SUBMIT":
                                            ActualResults = LoadDetails.SubmitLumperAmount();
                                            break;
                                        case "LUMPERTICKET":
                                            ActualResults = LoadDetails.VerifyLumperTicket();
                                            break;
                            }
                                    break;
                                case "INVOICE":
                                    switch (arrAction[3])
                                    {
                                        case "GENERATE":
                                            ActualResults = LoadDetails.GenerateInvoice();
                                            break;
                                        case "VERIFY":
                                            ActualResults = LoadDetails.VerifyGenerateInvoice();
                                            break;
                                    }
                                    break;
                                case "VERIFYALERTS":
                                    ActualResults = LoadDetails.VerifyAlerts();
                                    break;
                            }
                            Load = null;
                            break;
                        #endregion

                        case "MYTASKS":
                            MyTasks MyTasks = new MyTasks(_teststartinfo, datamanager);
                            switch (arrAction[2])
                            {
                                case "NAVIGATE":
                                    switch (arrAction[3])
                                    {
                                        case "CONFIRMLOADS":
                                            ActualResults = MyTasks.NavigateToConfirmLoads();
                                            break;
                                        case "ACCOUNTING":
                                            ActualResults = MyTasks.NavigateToAccounting();
                                            break;
                                        case "TRACKING":
                                            ActualResults = MyTasks.NavigateToTracking();
                                            break;
                                    }
                                    break;
                                case "CONFIRMLOAD":
                                    switch (arrAction[3])
                                    {
                                        case "GETCOUNT":
                                            ActualResults = MyTasks.GetConfirmLoadsCount();
                                            break;
                                        case "OPEN":
                                            ActualResults = MyTasks.OpenConfirmLoad();
                                            break;
                                        case "FILL":
                                            ActualResults = MyTasks.ConfirmLoadFill();
                                            break;
                                        case "SUBMIT":
                                            ActualResults = MyTasks.ConfirmLoadSubmit();
                                            break;
                                        case "CANCEL":
                                            ActualResults = MyTasks.ConfirmLoadCancel();
                                            break;
                                        case "VERIFY":
                                            switch (arrAction[4])
                                            {
                                                case "COUNTDECREASE":
                                                    ActualResults = MyTasks.VerifyConfirmLoadCountDecrease();
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case "ACCOUNTING":
                                    switch (arrAction[3])
                                    {
                                        case "OPENLOADWITHMISSINGDOCUMENT":
                                            ActualResults = MyTasks.OpenLoadWithMissingDocument();
                                            break;
                                        case "GETCOUNT":
                                            ActualResults = MyTasks.GetAccountingLoadsCount();
                                            break;
                                        case "COMPARELOADSCOUNT":
                                            ActualResults = MyTasks.CompareAccountingLoadsCount();
                                                break;
                                        case "TOTALMISSINGDOCUMENTS":
                                            ActualResults = MyTasks.GetTotalMissingDocuments();
                                            break;
                                        case "COMPAREMISSINGDOCUMENTS":
                                            ActualResults = MyTasks.VerifyReducedMissingDocuments();
                                            break;
                                    }
                                    break;
                                case "TRACKING":
                                    switch (arrAction[3])
                                    {
                                        case "GETCALLBACK":
                                            ActualResults = MyTasks.GetCallBackOfLoad();
                                            break;
                                        case "VERIFYCALLBACK":
                                            ActualResults = MyTasks.VerifyCallBackTime();
                                            break;
                                        case "OPENLOAD":
                                            ActualResults = MyTasks.OpenTrackingLoadDetails();
                                            break; 
                                    }
                                    break;
                            }
                            MyTasks = null;
                            break;
                        case "PREFERENCES":
                            Preferences Preferences = new Preferences(_teststartinfo, datamanager);
                            switch (arrAction[2])
                            {
                                case "NAVIGATE":
                                    ActualResults = Preferences.Navigate();
                                    break;
                                case "VERIFYOPTIONS":
                                    ActualResults = Preferences.VerifyPreferencesOptions();
                                    break;
                                case "UPDATE":
                                    switch (arrAction[3])
                                    {
                                        case "MODES":
                                            ActualResults = Preferences.UpdateModes();
                                            break;
                                        case "AVAILABLEEQUIPMENT":
                                            ActualResults = Preferences.UpdateAvailableEquipment();
                                            break;
                                        case "ADDANOTHERLANE":
                                            ActualResults = Preferences.AddAnotherLane();
                                            break;
                                        case "DELETELANE":
                                            ActualResults = Preferences.DeleteLane();
                                            break;
                                    }
                                    break;
                            }
                            Preferences = null;
                            break;
                    }
                    Claw = null;
                    break;
            }
            datamanager = null;
            return ActualResults;
        }

    }
}
