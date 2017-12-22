using OpenQA.Selenium;
using SWAT.FrameWork.Utilities;
using SWAT.Configuration;
using System.Collections.Generic;

namespace SWAT.Applications.Claw.ObjectRepository
{
    public class LoadDetailsPage : Page
    {
        //Tabs
        private By trackingtab = By.XPath(".//*[@id='tracking-container']/a");//By.CssSelector("#tracking-button");
        private By stopstab = By.XPath(".//*[@id='stops-container']/a");//By.CssSelector("#stops-button");
        private By documentstab = By.XPath(".//*[@id='documents-container']/a");//By.CssSelector("#documents-button");
        private By chargestab = By.XPath(".//*[@id='charges-container']/a");//By.CssSelector("#charges-button");
        private By tenderstatustab = By.XPath(".//*[@id='tender-history-container']/a");//By.CssSelector("#tender-history-button");
        private By toptab_factoring = By.XPath(".//*[@id='load-details-side-panel-region']/div/ul/li[1]/a");
        private By summarystab_factoring = By.XPath(".//*[@id='load-details-side-panel-region']/div/ul/li[2]/a");
        private By stopsstab_factoring = By.XPath(".//*[@id='load-details-side-panel-region']/div/ul/li[3]/a");
        private By documentstab_factoring = By.XPath(".//*[@id='load-details-side-panel-region']/div/ul/li[4]/a");
        private By chargestab_factoring = By.XPath(".//*[@id='load-details-side-panel-region']/div/ul/li[5]/a");

        private By loadinquirybtn = By.CssSelector("#load-inquiry");
        private By optionbtn = By.CssSelector("#options-button");
        private By retendersubmitbtn = By.CssSelector("#select-reason-code-submit");
        private By retenderloadbtn = By.CssSelector("#load-retender-button");
        private By modalclosebtn = By.CssSelector("#modal-header-close-modal-button");
        private By cancelloadbtn = By.CssSelector("#load-cancel");
        private By confirmcancellationbtn = By.CssSelector("#load-cancel-form>button");
        private By apptitle_loadid = By.CssSelector("#app-title");

        private By loadprogress = By.XPath(".//*[@id='summary']/div/div[3]/dl[2]/dd");

        //dispatch
        private By dispatch_drivername = By.CssSelector("#lblFirstDriverName");
        private By dispatch_driverphone = By.CssSelector("#lblFirstDriverPhone");
        private By dispatch_emptylocation = By.XPath(".//*[@id='dispatch']/div/div[2]/dl[1]/dd");
        private By dispatch_emptydatetime = By.XPath(".//*[@id='dispatch']/div/div[2]/dl[2]/dd");
        private By dispatch_distancetofirststop = By.XPath(".//*[@id='dispatch']/div/div[2]/dl[3]/dd");
        private By dispatch_nextcallback = By.XPath(".//*[@id='dispatch']/div/div[2]/dl[4]/dd");
        private By dispatch_pronumber = By.XPath(".//*[@id='pro-number']");
        private By dispatch_equipment = By.XPath(".//*[@id='dispatch']/div/div[4]/dl[1]/dd");
        private By dispatch_tractor = By.CssSelector("#lblTractorNumber");
        private By dispatch_trailer = By.CssSelector("#lblTrailerNumber");

        //Primary keys for each tabs UI
        private By trackingtab_trackingnotes = By.CssSelector("#tracking");
        private By stopstab_stops = By.CssSelector("#stops-region");
        private By documentstab_documents = By.CssSelector("#documents");
        private By chargerstab_chargers = By.CssSelector("#coyote-charges");
        private By tenderstatustab_tenderstatus = By.CssSelector("#tender-history");

        public UIItem TrackingTab_TrackingNotes { get { return new UIItem("Load details page>> Tracking Tab>> Tracking Notes", this.trackingtab_trackingnotes, _driver); } }
        public UIItem StopsTab_Stops { get { return new UIItem("Load details page>> Stops Tab>> Stops region", this.stopstab_stops, _driver); } }
        public UIItem DocumentsTab_Documents { get { return new UIItem("Load details page>> Documents Tab>> Documents Region", this.documentstab_documents, _driver); } }
        public UIItem ChargersTab_Chargers { get { return new UIItem("Load details page>> Charges Tab>> Charges Region", this.chargerstab_chargers, _driver); } }
        public UIItem TenderStatusTab_TenderStatus { get { return new UIItem("Load details page>> TenderStatus Tab>> Tender Region", this.tenderstatustab_tenderstatus, _driver); } }


        public LoadDetailsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public LoadDetailsPage(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
        }




        public UIItem LoadProgress { get { return new UIItem("Load details page>> Load progress", this.loadprogress, _driver); } }
        public UIItem DriverName { get { return new UIItem("Load details page>> Driver Name", this.dispatch_drivername, _driver); } }

        public UIItem LoadInquireBtn { get { return new UIItem("Load details page>> Load inquire button", this.loadinquirybtn, _driver); } }
        public UIItem OptionBtn { get { return new UIItem("Load details page>> Option Button", this.optionbtn, _driver); } }
        public UIItem RetenderSubmitBtn { get { return new UIItem("Load details page>> RetenderSubmit Button", this.retendersubmitbtn, _driver); } }
        public UIItem RetenderLoadBtn { get { return new UIItem("Load details page>> Retender this load Button", this.retenderloadbtn, _driver); } }
        public UIItem ModalCloseBtn { get { return new UIItem("Load details page>> Modal close Button", this.modalclosebtn, _driver); } }
        public UIItem CancelLoadBtn { get { return new UIItem("Load details page>> Cancel Load Button", this.cancelloadbtn, _driver); } }
        public UIItem ConfirmCancellationBtn { get { return new UIItem("Load details page>> Cancel Confirmation Button", this.confirmcancellationbtn, _driver); } }
        //Tabs
        public UIItem TrackingTab { get { return new UIItem("Load details page>> Tracking Tab", this.trackingtab, _driver); } }
        public UIItem StopsTab { get { return new UIItem("Load details page>> Stop Tab", this.stopstab, _driver); } }
        public UIItem DocumentsTab { get { return new UIItem("Load details page>> Document Tab", this.documentstab, _driver); } }
        public UIItem ChargesTab { get { return new UIItem("Load details page>> Chargers Tab", this.chargestab, _driver); } }
        public UIItem TenderStatusTab { get { return new UIItem("Load details page>> TenderStatus Tab", this.tenderstatustab, _driver); } }
        public UIItem TopTab_Factoring { get { return new UIItem("Load details page for Factoring Company>> Top Tab", this.toptab_factoring, _driver); } }
        public UIItem SummaryTab_Factoring { get { return new UIItem("Load details page for Factoring Company>> Summary Tab", this.summarystab_factoring, _driver); } }
        public UIItem StopsTab_Factoring { get { return new UIItem("Load details page for Factoring Company>> Stops Tab", this.stopsstab_factoring, _driver); } }
        public UIItem DocumentsTab_Factoring { get { return new UIItem("Load details page for Factoring Company>> Documents Tab", this.documentstab_factoring, _driver); } }
        public UIItem ChargesTab_Factoring { get { return new UIItem("Load details page for Factoring Company>> Charges Tab", this.chargestab_factoring, _driver); } }
        public UIItem AppTitleLoadId { get { return new UIItem("Load details page>> App Title LoadId", this.apptitle_loadid, _driver); } }
    }

    public class LoadDetails_Tracking : Page
    {
        private OpenQA.Selenium.IWebDriver webDriver;
        private By dispatchdriverbutton = By.CssSelector("#dispatch-driver");
        private By dispatchdriverform = By.CssSelector("#load-dispatch-driver-form");
        private By firstdriver = By.CssSelector("#firstDriver");
        private By firstcellphonenumber = By.CssSelector("#firstCellPhoneNumber");
        private By team = By.CssSelector("#team");
        private By actualemptylocation = By.CssSelector("#actual-empty-location");
        private By emptydate = By.CssSelector("#emptyDate");
        private By emptytime = By.CssSelector("#emptyTime");
        private By equipmenttype = By.CssSelector("#equipmentType");
        private By equipmentlength = By.CssSelector("#equipmentLength");
        private By equipmentwidth = By.CssSelector("#equipmentWidth");
        private By equipmentheight = By.CssSelector("#equipmentHeight");
        private By trailernumber = By.CssSelector("#trailerNumber");

        private By dispatch_dispatchbutton = By.CssSelector(".hook--save-dispatch-driver");
        private By dispatch_cancelbutton = By.CssSelector(".hook--cancel-carrier-update");
        private By dispatch_updatedispatchbutton = By.CssSelector(".hook--edit-carrier-button");
        private By dispatch_dispatchsection = By.CssSelector("#dispatch");
        private By dispatch_drivernameinput = By.XPath("//*[@id='dispatch']/div/div[1]/dl[1]/dd/input");
        private By dispatch_drivernametext = By.XPath("//*[@id='dispatch']/div/div[1]/dl[1]/dd/div");
        private By dispatch_driverphoneinput = By.XPath("//*[@id='dispatch']/div/div[1]/dl[2]/dd/input");
        private By dispatch_driverphonetext = By.XPath("//*[@id='dispatch']/div/div[1]/dl[2]/dd/div");
        private By dispatch_tractornameinput = By.XPath("//*[@id='dispatch']/div/div[4]/dl[2]/dd/input");
        private By dispatch_tractornametext = By.XPath("//*[@id='dispatch']/div/div[4]/dl[2]/dd/div");
        private By dispatch_savedispatchbutton = By.CssSelector(".hook--save-carrier");
        private By dispatch_dispatchdriver = By.CssSelector("#dispatch-driver");


        private By pickupdate = By.XPath("//*[@id='summary']/div/div[2]/div[1]/div[2]");
        private By equipment = By.XPath("//*[@id='summary']/div/div[3]/dl[5]/dd");
        private By trackingnotes_region = By.CssSelector("#tracking-notes-region");
        private By trackingnotes_1strow_action = By.XPath(".//*[@id='trackingnotes-results-container']/tr[1]/td[2]");
        private By trackingnotes_1strow_notes = By.XPath(".//*[@id='trackingnotes-results-container']/tr[1]/td[4]");
        private By trackingnotes_1strow_carrier = By.XPath(".//*[@id='trackingnotes-results-container']/tr[1]/td[5]");


        public LoadDetails_Tracking(OpenQA.Selenium.IWebDriver webDriver)
        {
            // TODO: Complete member initialization
            this.webDriver = webDriver;
        }

        public LoadDetails_Tracking(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
        }

        public UIItem DispatchDriverButton { get { return new UIItem("Load details page>> Dispatch Driver Button", this.dispatchdriverbutton, _driver); } }
        public UIItem DispatchDriverForm { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Driver Form", this.dispatchdriverform, _driver); } }
        public UIItem FirstDriver { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Driver Form>> Driver Name", this.firstdriver, _driver); } }
        public UIItem FirstCellPhoneNumber { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Driver Form>> Cell Phone Number", this.firstcellphonenumber, _driver); } }
        public UIItem Team { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Driver Form>> Team Checkbox", this.team, _driver); } }
        public UIItem ActualEmptyLocation { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Driver Form>> Actual Empty Location", this.actualemptylocation, _driver); } }
        public UIItem EmptyDate { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Driver Form>> Empty Date", this.emptydate, _driver); } }
        public UIItem EmptyTime { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Driver Form>> Empty Time", this.emptytime, _driver); } }
        public UIItem EquipmentType { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Driver Form>> Equipment Type", this.equipmenttype, _driver); } }
        public UIItem EquipmentLength { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Driver Form>> Equipment Length", this.equipmentlength, _driver); } }
        public UIItem EquipmentWidth { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Driver Form>> Equipment Width", this.equipmentwidth, _driver); } }
        public UIItem EquipmentHeight { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Driver Form>> Equipment Height", this.equipmentheight, _driver); } }
        public UIItem TrailerNumber { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Driver Form>> Trailer Number", this.trailernumber, _driver); } }


        public UIItem Dispatch_DispatchDriverButton { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Driver Form>> Dispatch Driver button", this.dispatch_dispatchdriver, _driver); } }
        public UIItem Dispatch_DispatchButton { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Driver Form>> Dispatch Button", this.dispatch_dispatchbutton, _driver); } }
        public UIItem Dispatch_CancelButton { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Driver Form>> Cancel Button", this.dispatch_cancelbutton, _driver); } }
        public UIItem Dispatch_UpdateDispatchButton { get { return new UIItem("Load details page>> Tracking Tab>> Update Dispatch Info Button", this.dispatch_updatedispatchbutton, _driver); } }
        public UIItem Dispatch_DispatchSection { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Section", this.dispatch_dispatchsection, _driver); } }
        public UIItem Dispatch_DriverNameInput { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Section>> Driver Name Input", this.dispatch_drivernameinput, _driver); } }
        public UIItem Dispatch_DriverNameText { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Section>> Driver Name Text", this.dispatch_drivernametext, _driver); } }
        public UIItem Dispatch_DriverPhoneInput { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Section>> Driver Phone Input", this.dispatch_driverphoneinput, _driver); } }
        public UIItem Dispatch_DriverPhoneText { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Section>> Driver Phone Text", this.dispatch_driverphonetext, _driver); } }
        public UIItem Dispatch_TractorNameInput { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Section>> Tractor Name Input", this.dispatch_tractornameinput, _driver); } }
        public UIItem Dispatch_TractorNameText { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Section>> Tractor Name Text", this.dispatch_tractornametext, _driver); } }
        public UIItem Dispatch_SaveDispatchButton { get { return new UIItem("Load details page>> Tracking Tab>> Dispatch Section>> Save Dispatch Info Button", this.dispatch_savedispatchbutton, _driver); } }


        public UIItem PickupDate { get { return new UIItem("Load details page>> Tracking Tab>> Summary Region>> Pickup Date", this.pickupdate, _driver); } }
        public UIItem Equipment { get { return new UIItem("Load details page>> Tracking Tab>> Summary Region>> Equipment", this.equipment, _driver); } }
        public UIItem TrackingNotesRegion { get { return new UIItem("Load details page>> Tracking Tab>> Tracking Notes Region", this.trackingnotes_region, _driver); } }
        public UIItem TrackingNotes_1stRow_Action { get { return new UIItem("Load details page>> Tracking Tab>> Tracking Notes>> Action", this.trackingnotes_1strow_action, _driver); } }
        public UIItem TrackingNotes_1stRow_Notes { get { return new UIItem("Load details page>> Tracking Tab>> Tracking Notes>> Notes ", this.trackingnotes_1strow_notes, _driver); } }
        public UIItem Trackingnotes_1strow_carrier { get { return new UIItem("Load details page>> Tracking Tab>> Tracking Notes>> Carrier ", this.trackingnotes_1strow_carrier, _driver); } }
    }

    public class LoadDetails_SummaryDetails:Page
    {
        public LoadDetails_SummaryDetails(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
        }

        private By customer_summarydetails_buyer = By.XPath(".//*[@id='summary']/div/div[3]/dl[1]/dd");
        private By customer_summarydetails_referenceid = By.XPath(".//*[@id='summary']/div/div[3]/dl[2]/dd");
        private By customer_summarydetails_progress = By.XPath(".//*[@id='summary']/div/div[3]/dl[3]/dd");
        private By customer_summarydetails_loadid = By.XPath(".//*[@id='summary']/div/div[3]/dl[4]/dd");
        private By customer_summarydetails_mode = By.XPath(".//*[@id='summary']/div/div[3]/dl[5]/dd");
        private By customer_summarydetails_equipment = By.XPath(".//*[@id='summary']/div/div[3]/dl[6]/dd");
        private By customer_summarydetails_distance = By.XPath(".//*[@id='summary']/div/div[3]/dl[7]/dd");
        private By customer_summarydetails_shipmenttype = By.XPath(".//*[@id='summary']/div/div[3]/dl[8]/dd");

        public UIItem Customer_SummaryDetails_Buyer { get { return new UIItem("", this.customer_summarydetails_buyer, _driver); } }
        public UIItem Customer_SummaryDetails_ReferenceID { get { return new UIItem("", this.customer_summarydetails_referenceid, _driver); } }
        public UIItem Customer_SummaryDetails_Progress { get { return new UIItem("", this.customer_summarydetails_progress, _driver); } }
        public UIItem Customer_SummaryDetails_LoadID { get { return new UIItem("", this.customer_summarydetails_loadid, _driver); } }
        public UIItem Customer_SummaryDetails_Mode { get { return new UIItem("", this.customer_summarydetails_mode, _driver); } }
        public UIItem Customer_SummaryDetails_Equipment { get { return new UIItem("", this.customer_summarydetails_equipment, _driver); } }
        public UIItem Customer_SummaryDetails_Distance { get { return new UIItem("", this.customer_summarydetails_distance, _driver); } }
        public UIItem Customer_SummaryDetails_ShipmentType { get { return new UIItem("", this.customer_summarydetails_shipmenttype, _driver); } }
        public UIItem DispatchDetail_Message { get { return new UIItem("", By.CssSelector(".panel__body>p"), _driver); } }

        private By carrier_summarydetails_referenceid = By.XPath(".//*[@id='summary']/div/div[3]/dl[1]/dd");
        private By carrier_summarydetails_progress = By.XPath(".//*[@id='summary']/div/div[3]/dl[2]/dd");
        private By carrier_summarydetails_loadid = By.XPath(".//*[@id='summary']/div/div[3]/dl[3]/dd");
        private By carrier_summarydetails_mode = By.XPath(".//*[@id='summary']/div/div[3]/dl[4]/dd");
        private By carrier_summarydetails_equipment = By.XPath(".//*[@id='summary']/div/div[3]/dl[5]/dd");
        private By carrier_summarydetails_distance = By.XPath(".//*[@id='summary']/div/div[3]/dl[6]/dd");

        public UIItem Carrier_SummaryDetails_ReferenceID { get { return new UIItem("", this.carrier_summarydetails_referenceid, _driver); } }
        public UIItem Carrier_SummaryDetails_Progress { get { return new UIItem("", this.carrier_summarydetails_progress, _driver); } }
        public UIItem Carrier_SummaryDetails_LoadID { get { return new UIItem("", this.carrier_summarydetails_loadid, _driver); } }
        public UIItem Carrier_SummaryDetails_Mode { get { return new UIItem("", this.carrier_summarydetails_mode, _driver); } }
        public UIItem Carrier_SummaryDetails_Equipment { get { return new UIItem("", this.carrier_summarydetails_equipment, _driver); } }
        public UIItem Carrier_SummaryDetails_Distance { get { return new UIItem("", this.carrier_summarydetails_distance, _driver); } }
    }

    public class LoadDetails_DispatchPanel: Page
    {
        public LoadDetails_DispatchPanel(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
        }
        private By customer_dispatch_carrier = By.XPath(".//*[@id='dispatch']/div/div[1]/dl/dd");
        private By customer_dispatch_prono = By.XPath(".//*[@id='lblProNumber']");
        private By customer_dispatch_equipment = By.XPath(".//*[@id='dispatch']/div/div[3]/dl[1]/dd");
        private By customer_dispatch_tractor = By.XPath(".//*[@id='lblTractorNumber']");
        private By customer_dispatch_trailer = By.XPath(".//*[@id='lblTrailerNumber']");

        public UIItem Customer_Dispatch_Carrier { get { return new UIItem("Load Details>> Tracking Tab>> Dispatch>> Carrier Name", this.customer_dispatch_carrier, _driver); } }
        public UIItem Customer_Dispatch_ProNo { get { return new UIItem("Load Details>> Tracking Tab>> Dispatch>> ProNo", this.customer_dispatch_prono, _driver); } }
        public UIItem Customer_Dispatch_Equipment { get { return new UIItem("Load Details>> Tracking Tab>> Dispatch>> Equipment", this.customer_dispatch_equipment, _driver); } }
        public UIItem Customer_Dispatch_Tractor { get { return new UIItem("Load Details>> Tracking Tab>> Dispatch>> Tractor", this.customer_dispatch_tractor, _driver); } }
        public UIItem Customer_Dispatch_Trailer { get { return new UIItem("Load Details>> Tracking Tab>> Dispatch>> Trailer", this.customer_dispatch_trailer, _driver); } }

        private By carrier_dispatch_drivername = By.XPath(".//*[@id='lblFirstDriverName']");
        private By carrier_dispatch_driverphone = By.XPath(".//*[@id='lblFirstDriverPhone']");
        private By carrier_dispatch_emptylocation = By.XPath(".//*[@id='dispatch']/div/div[2]/dl[1]/dd");
        private By carrier_dispatch_emptydatetime = By.XPath(".//*[@id='dispatch']/div/div[2]/dl[2]/dd");
        private By carrier_dispatch_distancetofirststop = By.XPath(".//*[@id='dispatch']/div/div[2]/dl[3]/dd");
        private By carrier_dispatch_nextcallback = By.XPath(".//*[@id='dispatch']/div/div[2]/dl[4]/dd");
        private By carrier_dispatch_prono = By.XPath(".//*[@id='lblProNumber']");
        private By carrier_dispatch_equipment = By.XPath(".//*[@id='dispatch']/div/div[4]/dl[1]/dd");
        private By carrier_dispatch_tractor = By.XPath(".//*[@id='lblTractorNumber']");
        private By carrier_dispatch_traile = By.XPath(".//*[@id='lblTrailerNumber']");

        public UIItem Carrier_Dispatch_DriverName { get { return new UIItem("Load Details>> Tracking Tab>> Dispatch>> Driver Name", this.carrier_dispatch_drivername, _driver); } }
        public UIItem Carrier_Dispatch_DriverPhone { get { return new UIItem("Load Details>> Tracking Tab>> Dispatch>> Driver Phone", this.carrier_dispatch_driverphone, _driver); } }
        public UIItem Carrier_Dispatch_EmptyLocation { get { return new UIItem("Load Details>> Tracking Tab>> Dispatch>> Empty Location", this.carrier_dispatch_emptylocation, _driver); } }
        public UIItem Carrier_Dispatch_EmptyDateTime{ get { return new UIItem("Load Details>> Tracking Tab>> Dispatch>> Empty Date time", this.carrier_dispatch_emptydatetime, _driver);}}
        public UIItem Carrier_Dispatch_DistancetoFirstStop { get { return new UIItem("Load Details>> Tracking Tab>> Dispatch>> Distance To First Stop", this.carrier_dispatch_distancetofirststop, _driver); } }
        public UIItem Carrier_Dispatch_NextCallBack { get { return new UIItem("Load Details>> Tracking Tab>> Dispatch>> Next Call Back", this.carrier_dispatch_nextcallback, _driver); } }
        public UIItem Carrier_Dispatch_ProNo { get { return new UIItem("Load Details>> Tracking Tab>> Dispatch>> Pro No", this.carrier_dispatch_prono, _driver); } }
        public UIItem Carrier_Dispatch_Equipment { get { return new UIItem("Load Details>> Tracking Tab>> Dispatch>> Equipment", this.carrier_dispatch_equipment, _driver); } }
        public UIItem Carrier_Dispatch_Tractor { get { return new UIItem("Load Details>> Tracking Tab>> Dispatch>> Tractor", this.carrier_dispatch_tractor, _driver); } }
        public UIItem Carrier_Dispatch_Trailer { get { return new UIItem("Load Details>> Tracking Tab>> Dispatch>> Trailer", this.carrier_dispatch_traile, _driver); } }




    }

    public class LoadDetails_Stops : Page
    {

        private By arrivaldate = By.CssSelector("input[class*='carrier-arrival-date']");
        private By arrivaltime = By.CssSelector("input[class*='carrier-arrival-time']");
        private By departuredate = By.CssSelector("input[class*='carrier-departure-date']");
        private By departuretime = By.CssSelector("input[class*='carrier-departure-time']");
        private By pickup_update = By.XPath(".//*[@id='stops-region']/div/div[1]/section/header/button");
        private By stop_save = By.CssSelector(".button.button--loud.hook--save-update");
        private By stop_savecancel = By.CssSelector(".text-link.nudge-half--sides.hook--cancel-update");
        private By latepickup_reason = By.CssSelector("#reason-codes");
        private By latepickup_continue = By.CssSelector("#select-reason-code-submit");
        private By delivery_update = By.XPath(".//*[@id='stops-region']/div/div[2]/section/header/button");
        private By reportlumperlink = By.XPath(".//*[@id='stops-region']/div/div/section/header/div/button");
        private By reportlumperform = By.CssSelector(".hook--loadstop-lumper-form");
        private By reportlumperinput = By.CssSelector(".hook--amount");
        private By reportlumpertextarea = By.XPath(".//*[@id='stops-region']/div/div[1]/section/header/div/form/ul/li[2]/span");
        private By reportlumpercancelbtn = By.CssSelector(".hook--cancel-lumper");
        private By reportlumpersubmitbtn = By.CssSelector(".hook--submit-lumper-button");
        private By loadstop_comfirm_checkbox = By.CssSelector("#load-stop-update-confirm-checkbox");
        private By loadstop_commoditiessection = By.XPath(".//*[@id='stops-region']/div/div[1]/section/div/div/div[2]");
        private By commodities1strow_packcging = By.XPath(".//*[@id='stops-region']/div/div[1]/section/div/div/div[2]/table/tbody/tr/td[3]/select");
        private By commodities1strow_loadon = By.XPath(".//*[@id='stops-region']/div/div[1]/section/div/div/div[2]/table/tbody/tr/td[4]/select");
        private By commodities1strow_actweight = By.XPath(".//*[@id='stops-region']/div/div[1]/section/div/div/div[2]/table/tbody/tr/td[7]/input");
        private By commodities1strow_actpieces = By.XPath(".//*[@id='stops-region']/div/div[1]/section/div/div/div[2]/table/tbody/tr/td[9]/input");
        private By commodities1strow_actpallets = By.XPath(".//*[@id='stops-region']/div/div[1]/section/div/div/div[2]/table/tbody/tr/td[11]/input");
        private By puCommidityRows = By.XPath(".//*[@id='stops-region']/div/div[1]/section/div/div[2]/table/tbody/tr");
        private By commodities1strow_expWeight = By.XPath(".//*[@id='stops-region']/div/div[1]/section/div/div/div[2]/table/tbody/tr/td[6]");
        private By commodities1strow_expPieces = By.XPath(".//*[@id='stops-region']/div/div[1]/section/div/div/div[2]/table/tbody/tr/td[8]");
        private By pickup_facilityname = By.XPath(".//*[@id='stops-region']/div/div[1]/section/div/div[1]/div[1]/dl[4]/dd[1]");
        private By pickup_facilityaddress = By.XPath(".//*[@id='stops-region']/div/div[1]/section/div/div[1]/div[1]/dl[4]/dd[2]");
        private By delivery_facilityname = By.XPath(".//*[@id='stops-region']/div/div[2]/section/div/div[1]/div[1]/dl[4]/dd[1]");
        private By delivery_facilityaddress = By.XPath(".//*[@id='stops-region']/div/div[2]/section/div/div[1]/div[1]/dl[4]/dd[2]");

        public LoadDetails_Stops(IWebDriver driver)
        {
            _driver = driver;
        }

        public LoadDetails_Stops(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
        }

        public List<LoadDetailsCommodity> PULoadDetailsCommodities
        {
            get
            {
                List<LoadDetailsCommodity> loadCommodities = new List<LoadDetailsCommodity>();
                foreach (var element in this.PUCommidityRows.FindElements())
                {
                    loadCommodities.Add(new LoadDetailsCommodity(element));
                }
                return loadCommodities;
            }

        }

        public UIItem Commodities1strow_expPieces { get { return new UIItem("Load details page>> Stop Tab>> 1st Row expected pieces", this.commodities1strow_expPieces, _driver); } }
        public UIItem Commodities1strow_expWeight { get { return new UIItem("Load details page>> Stop Tab>> 1st Row expected weight", this.commodities1strow_expWeight, _driver); } }
        public UIItem PUCommidityRows { get { return new UIItem("Load details page>> Stop Tab>> puCommidityRows", this.puCommidityRows, _driver); } }
        public UIItem Delivery_ArrivalDate_Disabled { get { return new UIItem("Load details page>> Stop Tab>> ArrivalDate", By.XPath(".//*[@id='stops-region']/div/div[2]/section/div/div/div[1]/div[1]/dl[5]/dd[4]/span"), _driver); } }
        public UIItem Delivery_ArrivalTime_Disabled { get { return new UIItem("Load details page>> Stop Tab>> ArrivalTime", By.XPath(".//*[@id='stops-region']/div/div[2]/section/div/div/div[1]/div[1]/dl[5]/dd[4]/span"), _driver); } }
        public UIItem Delivery_DepartureDate_Disabled { get { return new UIItem("Load details page>> Stop Tab>> DepartureDate", By.XPath(".//*[@id='stops-region']/div/div[2]/section/div/div/div[1]/div[1]/dl[5]/dd[5]/span"), _driver); } }
        public UIItem Delivery_DepartureTime_Disabled { get { return new UIItem("Load details page>> Stop Tab>> DepartureTime", By.XPath(".//*[@id='stops-region']/div/div[2]/section/div/div/div[1]/div[1]/dl[5]/dd[5]/span"), _driver); } }

        public UIItem Pickup_ArrivalDate_Disabled { get { return new UIItem("Load details page>> Stop Tab>> ArrivalDate", By.XPath(".//*[@id='stops-region']/div/div[1]/section/div/div/div[1]/div[1]/dl[5]/dd[4]/span"), _driver); } }
        public UIItem Pickup_ArrivalTime_Disabled { get { return new UIItem("Load details page>> Stop Tab>> ArrivalTime", By.XPath(".//*[@id='stops-region']/div/div[1]/section/div/div/div[1]/div[1]/dl[5]/dd[4]/span"), _driver); } }
        public UIItem Pickup_DepartureDate_Disabled { get { return new UIItem("Load details page>> Stop Tab>> DepartureDate", By.XPath(".//*[@id='stops-region']/div/div[1]/section/div/div/div[1]/div[1]/dl[5]/dd[5]/span"), _driver); } }
        public UIItem Pickup_DepartureTime_Disabled { get { return new UIItem("Load details page>> Stop Tab>> DepartureTime", By.XPath(".//*[@id='stops-region']/div/div[1]/section/div/div/div[1]/div[1]/dl[5]/dd[5]/span"), _driver); } }


        public UIItem ArrivalDate { get { return new UIItem("Load details page>> Stop Tab>> ArrivalDate", this.arrivaldate, _driver); } }
        public UIItem ArrivalTime { get { return new UIItem("Load details page>> Stop Tab>> ArrivalTime", this.arrivaltime, _driver); } }
        public UIItem DepartureDate { get { return new UIItem("Load details page>> Stop Tab>> DepartureDate", this.departuredate, _driver); } }
        public UIItem DepartureTime { get { return new UIItem("Load details page>> Stop Tab>> DepartureTime", this.departuretime, _driver); } }
        public UIItem Pickup_Update { get { return new UIItem("Load details page>> Stop Tab>> Pickup_Update", this.pickup_update, _driver); } }
        public UIItem Stop_UpdateSave { get { return new UIItem("Load details page>> Stop Tab>> Pickup_Save", this.stop_save, _driver); } }
        public UIItem Stop_UpdateCancel { get { return new UIItem("Load details page>> Stop Tab>> Pickup_SaveCancel", this.stop_savecancel, _driver); } }
        public UIItem Latepickup_Reason { get { return new UIItem("Load details page>> Stop Tab>> Latepickup_Reason", this.latepickup_reason, _driver); } }
        public UIItem Latepickup_Continue { get { return new UIItem("Load details page>> Stop Tab>> Latepickup_Continue", this.latepickup_continue, _driver); } }
        public UIItem Delivery_Update { get { return new UIItem("Load details page>> Stop Tab>> Delivery_Update", this.delivery_update, _driver); } }
        public UIItem ReportLumperLink { get { return new UIItem("Load details page>> Stop Tab>> Report Lumper Link", this.reportlumperlink, _driver); } }
        public UIItem ReportLumperForm { get { return new UIItem("Load details page>> Stop Tab>> Report Lumper Form", this.reportlumperform, _driver); } }
        public UIItem ReportLumperInput { get { return new UIItem("Load details page>> Stop Tab>> Report Lumper Input", this.reportlumperinput, _driver); } }
        public UIItem ReportLumperTextArea { get { return new UIItem("Load details page>> Stop Tab>> Report Lumper Text Area", this.reportlumpertextarea, _driver); } }
        public UIItem ReportLumperCancelBtn { get { return new UIItem("Load details page>> Stop Tab>> Report Lumper Pop up Cancel Button", this.reportlumpercancelbtn, _driver); } }
        public UIItem ReportLumperSubmitBtn { get { return new UIItem("Load details page>> Stop Tab>> Report Lumper Pop up Submit Button", this.reportlumpersubmitbtn, _driver); } }
        public UIItem LoadstopConfirmCheckbox { get { return new UIItem("Load details page>> Stop Tab>> Load stop confirmation checkbox", this.loadstop_comfirm_checkbox, _driver); } }
        public UIItem LoadstopCommoditiesSection { get { return new UIItem("Load details page>> Stop Tab>> Load stop>> commodities section", this.loadstop_commoditiessection, _driver); } }
        public UIItem Commodities1stRowPackaging { get { return new UIItem("Load details page>> Stop Tab>> Load stop>> Commodities 1st row packaging", this.commodities1strow_packcging, _driver); } }
        public UIItem Commodities1stRowLoadOn { get { return new UIItem("Load details page>> Stop Tab>> Load stop>> Commodities 1st row load on field", this.commodities1strow_loadon, _driver); } }
        public UIItem Commodities1stRowActWeight { get { return new UIItem("Load details page>> Stop Tab>> Load stop>> Commodities 1st row actual weight", this.commodities1strow_actweight, _driver); } }
        public UIItem Commodities1stRowActPieces { get { return new UIItem("Load details page>> Stop Tab>> Load stop>> Commodities 1st row actual pieces", this.commodities1strow_actpieces, _driver); } }
        public UIItem Commodities1stRowActPallets { get { return new UIItem("Load details page>> Stop Tab>> Load stop>> Commodities 1st row actual pallets", this.commodities1strow_actpallets, _driver); } }
        public UIItem Pickup_Name { get { return new UIItem("Load details page>> Stop Tab>> Load stop>> Pickup Name", this.pickup_facilityname, _driver); } }
        public UIItem Pickup_Address { get { return new UIItem("Load details page>> Stop Tab>> Load stop>> Pickup Address", this.pickup_facilityaddress, _driver); } }
        public UIItem Delivery_Name { get { return new UIItem("Load details page>> Stop Tab>> Load stop>> Delivery Name", this.delivery_facilityname, _driver); } }
        public UIItem Delivery_Address { get { return new UIItem("Load details page>> Stop Tab>> Load stop>> Delivery Address", this.delivery_facilityaddress, _driver); } }
    }

    public class LoadDetailsCommodity
    {
        private IWebElement _driver;
        private By item = By.XPath(".//td[2]");
        private By packaging = By.XPath(".//td[3]");
        private By loadOn = By.XPath(".//td[4]");
        private By expWeight = By.XPath(".//td[5]");
        private By actWeight = By.XPath(".//td[6]");
        private By expPallets = By.XPath(".//td[9]");
        public LoadDetailsCommodity(IWebElement driver)
        {
            _driver = driver;
        }
        public UIItem Item { get { return new UIItem("Load details >> Item", this.item, _driver); } }
        public UIItem Packaging { get { return new UIItem("Load details >> Packaging", this.packaging, _driver); } }
        public UIItem LoadOn { get { return new UIItem("Load details >> LoadOn", this.loadOn, _driver); } }
        public UIItem ExpWeight { get { return new UIItem("Load details >> ExpWeight", this.expWeight, _driver); } }
        public UIItem ActWeight { get { return new UIItem("Load details >> ActWeight", this.actWeight, _driver); } }
        public UIItem ExpPallets { get { return new UIItem("Load details >> ExpPallets", this.expPallets, _driver); } }
    }

    public class LoadDetails_Documents : Page
    {
        private OpenQA.Selenium.IWebDriver webDriver;
        private By notready_for_invoice_alert = By.CssSelector(".hook--not-ready-for-vendorinvoice");
        private By additional_docs_required_text = By.XPath(".//*[@id='documents-region']/div/section/div[2]/div[1]/p[1]");
        private By additional_docs_required_list = By.XPath(".//*[@id='documents-region']/div/section/div[2]/div[1]/ul/li");
        private By selectdocument_factoring = By.CssSelector("#select-document-type");
        private By filetypebtn_factoring = By.CssSelector(".hook--file-types");
        private By selectdocumentbtn = By.CssSelector("#select-document-type");
        private By filetypebtn = By.CssSelector(".hook--fileTypes");
        private By uploadeddocument = By.CssSelector(".hook--document");
        private By notReadyForInvoicing = By.CssSelector(".hook--not-ready-for-invoicing");
        public LoadDetails_Documents(OpenQA.Selenium.IWebDriver webDriver)
        {
            // TODO: Complete member initialization
            this.webDriver = webDriver;
        }

                public LoadDetails_Documents(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
        }

        public UIItem NotReadyForInvoicing { get { return new UIItem("Load details page>> Documents Tab>> Not ready for invoice alert box", this.notReadyForInvoicing, _driver); } }
        public UIItem NotReadyForInvoiceAlert { get { return new UIItem("Load details page>> Documents Tab>> Not ready for invoice alert box", this.notready_for_invoice_alert, _driver); } }
        public UIItem AdditionalDocsReqText { get { return new UIItem("Load details page>> Documents Tab>> Alert box text", this.additional_docs_required_text, _driver); } }
        public UIItem AdditionalDocReqList { get { return new UIItem("Load details page>> Documents Tab>> Alert box text", this.additional_docs_required_list, _driver); } }
        public UIItem SelectDocument_Factoring { get { return new UIItem("Load details page>> Documents Tab>> Select Document Button for Factoring", this.selectdocument_factoring, _driver); } }
        public UIItem FileTypeBtn_Factoring { get { return new UIItem("Load details page>> Documents Tab>> File Type Button for Factoring", this.filetypebtn_factoring, _driver); } }
        public UIItem SelectDocumentTypeBtn { get { return new UIItem("Load details page>> Cancel Load Button", this.selectdocumentbtn, _driver); } }
        public UIItem FileTypeBtn { get { return new UIItem("Load details page>> File Type Button", this.filetypebtn, _driver); } }
        public UIItem UploadedDocumentBtn { get { return new UIItem("Load details page>> Uploaded Document Button", this.uploadeddocument, _driver); } }

        public UIItem RequiredDocusments { get { return new UIItem("", By.CssSelector("#documents-status-container>li"), _driver); } }

    }

    public class LoadDetails_Charger : Page
    {
        private OpenQA.Selenium.IWebDriver webDriver;
        private By generate_invoice_section_factoring = By.CssSelector("#generate-invoice-div");
        private By invoice_number_input_factoring = By.CssSelector("#invoice-number");
        private By invoice_notes_input_factoring = By.CssSelector("#invoice-notes");
        private By approve_rates_checkbox_factoring = By.CssSelector("#approve-rates");
        private By generate_invoice_button_factoring = By.CssSelector("#generate-invoice-button");
        private By generate_invoice_section = By.CssSelector("#generate-invoice");
        private By invoice_number_input = By.CssSelector("#invoiceNumber");
        private By invoice_notes_input = By.CssSelector("#invoiceNotes");
        private By approve_rates_checkbox = By.CssSelector("#approveRates");
        private By generate_invoice_button = By.CssSelector("#generateInvoice");
        private By std_payment_terms = By.CssSelector("#standardPaymentTerms");
        private By req_final_advance = By.CssSelector("#requestFinalAdvance"); 
        private By awaiting_approval_alert = By.CssSelector("#awaiting-approval");
        private By awaiting_approval_text = By.XPath(".//*[@id='awaiting-approval']/p[2]");   
        private By in_process_alert = By.CssSelector("#inprocess-vendorinvoice");
        private By in_process_text = By.XPath(".//*[@id='inprocess-vendorinvoice']/p[2]");
        private By partial_payment_alert = By.CssSelector("#partial-payment");
        private By partial_payment_text = By.XPath(".//*[@id='partial-payment']/p[2]");
        private By paid_alert = By.XPath(".//*[@id='charges-region']/div/div/div[2]/div");
        private By paid_text = By.XPath(".//*[@id='charges-region']/div/div/div[2]/div/p[2]");
        private By lineitems_container_rows = By.XPath(".//*[@id='lineitems-container']/tr");
        public LoadDetails_Charger(OpenQA.Selenium.IWebDriver webDriver)
        {
            // TODO: Complete member initialization
            this.webDriver = webDriver;
        }

                public LoadDetails_Charger(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
        }
        public UIItem GenerateInvoiceSectionFactoring { get { return new UIItem("Load details page>> Charges Tab>> Generate Invoice Section", this.generate_invoice_section_factoring, _driver); } }
        public UIItem InvoiceNumberInputFactoring { get { return new UIItem("Load details page>> Charges Tab>> Invoice Number Input", this.invoice_number_input_factoring, _driver); } }
        public UIItem InvoiceNotesInputFactoring { get { return new UIItem("Load details page>> Charges Tab>> Invoice Notes Inut", this.invoice_notes_input_factoring, _driver); } }
        public UIItem ApproveRatesCheckboxFactoring { get { return new UIItem("Load details page>> Charges Tab>> Approve rates checkbox", this.approve_rates_checkbox_factoring, _driver); } }
        public UIItem GenerateInvoiceButtonFactoring { get { return new UIItem("Load details page>> Charges Tab>> Generate invoice button", this.generate_invoice_button_factoring, _driver); } }
        public UIItem GenerateInvoiceSection { get { return new UIItem("Load details page>> Charges Tab>> Generate Invoice Section", this.generate_invoice_section, _driver); } }
        public UIItem InvoiceNumberInput { get { return new UIItem("Load details page>> Charges Tab>> Invoice Number Input", this.invoice_number_input, _driver); } }
        public UIItem InvoiceNotesInput { get { return new UIItem("Load details page>> Charges Tab>> Invoice Notes Inut", this.invoice_notes_input, _driver); } }
        public UIItem ApproveRatesCheckbox { get { return new UIItem("Load details page>> Charges Tab>> Approve rates checkbox", this.approve_rates_checkbox, _driver); } }
        public UIItem GenerateInvoiceButton { get { return new UIItem("Load details page>> Charges Tab>> Generate invoice button", this.generate_invoice_button, _driver); } }
        public UIItem StdPaymentTerms { get { return new UIItem("Load details page>> Charges Tab>> Standard payment terms radio", this.std_payment_terms, _driver); } }
        public UIItem RequestFinalAdvance { get { return new UIItem("Load details page>> Charges Tab>> Request final advance radio", this.req_final_advance, _driver); } }
        public UIItem AwaitingApprovalAlert { get { return new UIItem("Load details page>> Charges Tab>> Awaiting Approval Alert", this.awaiting_approval_alert, _driver); } }
        public UIItem AwaitingApprovalText { get { return new UIItem("Load details page>> Charges Tab>> Awaiting Approval Text", this.awaiting_approval_text, _driver); } }
        public UIItem InProcessAlert { get { return new UIItem("Load details page>> Charges Tab>> In Process Alert", this.in_process_alert, _driver); } }
        public UIItem InProcessText { get { return new UIItem("Load details page>> Charges Tab>> In Process Text", this.in_process_text, _driver); } }
        public UIItem PartialPaymentAlert { get { return new UIItem("Load details page>> Charges Tab>> Partial Payment Alert", this.partial_payment_alert, _driver); } }
        public UIItem PartialPaymentText { get { return new UIItem("Load details page>> Charges Tab>> Partial Payment Text", this.partial_payment_text, _driver); } }
        public UIItem PaidAlert { get { return new UIItem("Load details page>> Charges Tab>> Paid Approval Alert", this.paid_alert, _driver); } }
        public UIItem PaidText { get { return new UIItem("Load details page>> Charges Tab>> Paid Approval Text", this.paid_text, _driver); } }
        public UIItem LineItemsContainerRows { get { return new UIItem("Load details page>> Charges Tab>> Line Items Container", this.lineitems_container_rows, _driver); } }
        public UIItem ChargesTable_ChargesCol { get { return new UIItem("Load details page>> Charges Tab>> Charges Table>> Charges Coloumn", By.CssSelector("#lineitems-container>tr>td:nth-child(1)"), _driver); } }
        public UIItem ChargesTable_AmountCol { get { return new UIItem("Load details page>> Charges Tab>> Charges Table>> Amount Coloumn", By.CssSelector("#lineitems-container>tr>td:nth-child(2)"), _driver); } }
    }

    public class LoadDetails_TenderStatus : Page
    {
        private OpenQA.Selenium.IWebDriver webDriver;

        public LoadDetails_TenderStatus(OpenQA.Selenium.IWebDriver webDriver)
        {
            // TODO: Complete member initialization
            this.webDriver = webDriver;
        }

        public LoadDetails_TenderStatus(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
        }

        public UIItem TenderedLoadAlert { get { return new UIItem("Tendered Load Alert", By.CssSelector("#spot-quote-section > p"), _driver); } }
        public UIItem TenderStatus_Row { get { return new UIItem("Load details page>> Charges Tab>> Invoice Number Input", By.CssSelector("#tender-history-results-container>tr"), _driver); } }
        public UIItem Col_Carrier { get { return new UIItem("Load details page>> Charges Tab>> Invoice Number Input", By.CssSelector("#tender-history-results-container>tr>td:nth-child(1)"), _driver); } }
        public UIItem Col_Rank { get { return new UIItem("Load details page>> Charges Tab>> Invoice Number Input", By.CssSelector("#tender-history-results-container>tr>td:nth-child(2)"), _driver); } }
        public UIItem Col_OfferStatus { get { return new UIItem("Load details page>> Charges Tab>> Invoice Number Input", By.CssSelector("#tender-history-results-container>tr>td:nth-child(3)"), _driver); } }
        public UIItem Col_OfferTime { get { return new UIItem("Load details page>> Charges Tab>> Invoice Number Input", By.CssSelector("#tender-history-results-container>tr>td:nth-child(4)"), _driver); } }
        public UIItem Col_ResponseTime { get { return new UIItem("Load details page>> Charges Tab>> Invoice Number Input", By.CssSelector("#tender-history-results-container>tr>td:nth-child(5)"), _driver); } }
        public UIItem Col_ResponseBy { get { return new UIItem("Load details page>> Charges Tab>> Invoice Number Input", By.CssSelector("#tender-history-results-container>tr>td:nth-child(6)"), _driver); } }
        public UIItem Col_ResponseNotes { get { return new UIItem("Load details page>> Charges Tab>> Invoice Number Input", By.CssSelector("#tender-history-results-container>tr>td:nth-child(7)"), _driver); } }

    }

}
