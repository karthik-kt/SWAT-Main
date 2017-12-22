using OpenQA.Selenium;
using SWAT.FrameWork.Utilities;
using SWAT.Configuration;
using System.Collections;

namespace SWAT.Applications.Claw.ObjectRepository
{
    public class ScheduleLoadsPage:Page
    {
        public ScheduleLoadsPage(IWebDriver driver)
        {
            _driver = driver;
            url = "#schedule";
        }

        public ScheduleLoadsPage(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#schedule";
        }


        private By pg_title = By.CssSelector("#app-title");
        private By btn_shorttermloads = By.CssSelector("#filter-short-term-loads");
        private By btn_allloads = By.CssSelector("#filter-all-loads");
        private By txt_loadsearchinput = By.CssSelector("label.add-in.one-whole > #load-search-input");
        
        //Search Results
        private By tbl_searchresults = By.CssSelector("#scheduler-loadstops");
        private By grp_searchresultsdisplay = By.CssSelector("#scheduler-loadroutegroups");
        private By cbx_searchresults = By.CssSelector(".soft-half--sides.ui-state-focus");
        private By pgwrp_routegroup = By.CssSelector(".cui-loadroutegroup");
        private By lbl_loadcount = By.CssSelector(".loz.box--gray.nudge--right.fl.hook-loadcount");
        private By lbl_facilityname = By.CssSelector(".soft-half--right");
        private By lbl_facilitylocation = By.CssSelector(".muted");
        private By lbl_resultspuheader = By.CssSelector("grid one-quarter text-right");
        private By lbl_resultsloadinfoheader = By.CssSelector(".grid.three-quarters");
        
        //expand a facility
        private By icn_downarrow = By.CssSelector(".icon.icon--arrow-1-down.icon--large.animate.fr.soft-half");
        //collapse a facility
        private By icn_uparrow = By.CssSelector(".icon.icon--arrow-1-down.icon--large.animate.fr.soft-half.upside-down");
       
        //Title of the loads info
        private By box_test = By.CssSelector(".hook-loadroute-details-container.islet.box--gray-light");
        private By lbl_loadid = By.CssSelector(".soft-half--right");
        private By lbl_loadmode = By.CssSelector(".soft-half--right");
        //private By lbl_facilityname = By.CssSelector(".soft-half--right");
        private By lbl_carriername = By.CssSelector(".soft-half--right");
        private By lbl_punumber = By.CssSelector(".grid.one-quarter.text-right>strong");

        //PickupInfo
        private By lbl_pulabel = By.CssSelector(".badge.box--green.uppercase.milli.hook--tooltip-trigger");
        private By lbl_puopenfacilitylabel = By.CssSelector(".badge.box--yellow.uppercase.milli.hook--tooltip-trigger");
        private By lbl_pudate = By.CssSelector(".align-middle.box--border-none");
        private By lbl_pudepartdate = By.CssSelector(".align-middle.box--border-none>span");
        private By lbl_pufacilityname = By.CssSelector(".fn.org.block");
        private By lbl_puaddress = By.CssSelector(".zeta");
        private By lbl_pudisttomt = By.CssSelector(".align-middle.box--border-none");
        private By lbl_punumberinfo = By.CssSelector(".align-middle.box--border-none");
        private By cdr_schedulerloadstops = By.CssSelector("add-in__right-item icon--calendar");
        private By txt_puopentime = By.CssSelector(".hook--chosen-schedule-open-time.text-input.width--delta");
        private By txt_puclosetime = By.CssSelector(".hook--chosen-schedule-close-time.text-input.width--delta");
        private By btn_pusaveappointmenttimeenabled = By.CssSelector(".hook--save-appointment.button.pointer.button--loud");
        private By btn_pusaveappointmenttimedisabled = By.CssSelector(".hook--save-appointment.button.pointer");
        private By btn_puremoveappointmenttime = By.CssSelector(".hook--remove-appointment.button.pointer");
        private By icn_pudetails = By.CssSelector(".hook--show-facility-information.uppercase.pointer.muted");

        //Pickup Info Details - expanded
        private By lbl_pucontactsheader = By.CssSelector(".grid.one-fifth>h4");
        private By lbl_puhourssheader = By.CssSelector(".grid.one-fifth>h4");
        private By lbl_puconfirmationnumber = By.CssSelector(".hook--confirmation-number-label");
        private By lbl_pucontactslist = By.CssSelector(".grid.one-fifth");
        private By lbl_puhoursslist = By.CssSelector(".grid.one-fifth");
        private By lbl_puremoveconfirmationnumber = By.CssSelector(".text-link.hook--remove-confirmation-number");
        private By lbl_puaddconfirmationnumber = By.CssSelector(".button.hook--add-confirmation-number");

        private By pickupdate = By.XPath("//table[@id='scheduler-loadstops']/tbody/tr/td/table/tbody/tr/td[6]/div/span/div/label/input");//By.XPath("(//input[@type='text'])[3]");
        private By picktime = By.XPath("//table[@id='scheduler-loadstops']/tbody/tr/td/table/tbody/tr/td[6]/div/span/input");// By.XPath("(//input[@type='text'])[4]");
        private By pickupdatesave = By.XPath(".//*[@id='scheduler-loadstops']/tbody/tr[1]/td/table/tbody/tr[1]/td[6]/div[1]/span/button[1]");
        private By deliverydate = By.XPath("//table[@id='scheduler-loadstops']/tbody/tr[2]/td/table/tbody/tr/td[6]/div/span/div/label/input"); // By.XPath("(//input[@type='text'])[6]");
        private By deliverytime = By.XPath("//table[@id='scheduler-loadstops']/tbody/tr[2]/td/table/tbody/tr/td[6]/div/span/input");//By.XPath("(//input[@type='text'])[7]");
        private By deliveryupdatesave = By.XPath(".//*[@id='scheduler-loadstops']/tbody/tr[2]/td/table/tbody/tr[1]/td[6]/div[1]/span/button[1]");

        public UIItem pg_Title { get { return new UIItem("Schedule Loads Page >>  Page Title", this.pg_title, _driver); } }
        public UIItem btn_ShortTermLoads { get { return new UIItem("Schedule Loads Page >>  Short Term Loads button", this.btn_shorttermloads, _driver); } }
        public UIItem btn_AllLoads { get { return new UIItem("Schedule Loads Page >>  All Loads button", this.btn_allloads, _driver); } }
        public UIItem txt_LoadSearchInput { get { return new UIItem("Schedule Loads Page >>  Load Search Textbox", this.txt_loadsearchinput, _driver); } }
        public UIItem cbx_SearchResults { get { return new UIItem("Schedule Loads Page >>  Load Search results Intellisense ", this.cbx_searchresults, _driver); } }
        public UIItem grp_SearchResultsDisplay { get { return new UIItem("Schedule Loads Page >>  Load Search group display", this.grp_searchresultsdisplay, _driver); } }
        public UIItem lbl_ResultsPUHeader { get { return new UIItem("Schedule Loads Page >>  PU number header ribbon", this.lbl_resultspuheader, _driver); } }
        public UIItem lbl_ResultsLoadInfoHeader { get { return new UIItem("Schedule Loads Page >>  Load info header ribbon", this.lbl_resultsloadinfoheader, _driver); } }
        public UIItem tbl_SearchResults { get { return new UIItem("Schedule Loads Page >>  Load info header ribbon", this.tbl_searchresults, _driver); } }

        public UIItem PickupDate { get { return new UIItem("Schedule Loads Page >> Pickup Date", this.pickupdate, _driver); } }
        public UIItem PickTime { get { return new UIItem("Schedule Loads Page >> Pickup Time", this.picktime, _driver); } }
        public UIItem PickUpdateSave { get { return new UIItem("Schedule Loads Page >> Pickup Facility Save", this.pickupdatesave, _driver); } } 
        public UIItem DeliveryTime { get { return new UIItem("Schedule Loads Page >> Delivery Time", this.deliverytime, _driver); } }
        public UIItem DeliveryDate { get { return new UIItem("Schedule Loads Page >> Delivery Date", this.deliverydate, _driver); } }
        public UIItem DeliveryUpdateSave { get { return new UIItem("Schedule Loads Page >> Delivery Facility Save", this.deliveryupdatesave, _driver); } }
        public UIItem SaveMessage { get { return new UIItem("Saving in progress", By.CssSelector(".hook--schedule-service-message.busy"), _driver); } }

        public UIItem Outside { get { return new UIItem("Outside", By.CssSelector("#scheduler-widget-container>div"), _driver); } }
        By changereasonpopu = By.CssSelector("#modal-title");
        By crpopupcontinue = By.CssSelector("#select-reason-code-submit");

        public UIItem ChangeReasonPopup
        {
            get { return new UIItem("Schedule Loads Page >> Change Reason popup", this.changereasonpopu, _driver); }
        }

        public UIItem PopContinue
        {
            get { return new UIItem("Schedule Loads Page >> Popup Continue", this.crpopupcontinue, _driver); }
        }

    }

}
