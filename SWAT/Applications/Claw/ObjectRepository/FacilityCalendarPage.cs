using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAT.Applications.Claw.ObjectRepository
{
    using OpenQA.Selenium;
    using SWAT.Configuration;
    using SWAT.FrameWork.Utilities;

    class FacilityCalendarPage : Page
    {
        public FacilityCalendarPage(IWebDriver driver)
        {
            _driver = driver;
            url = "#facilitycalendar";
        }

        public FacilityCalendarPage(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#facilitycalendar";
        }

        private By appTitle = By.CssSelector("#app-title");
        private By facilitySelectorInput = By.CssSelector("#facilityselector-input");
        private By schedulerCalendarReportLink = By.CssSelector("#schedulercalendar-report-link");
        private By blockAllButton = By.CssSelector("#block-all");
        private By unblockAllButton = By.CssSelector("#unblock-all");
        private By calendarDateSelect = By.CssSelector("#calendar-date-select");
        private By todayDateSelect = By.XPath("//div[@id='schedulercalendar-fc-container']/div/table/tbody/tr/td[3]/span[2]");
        private By previousDateSelect = By.XPath("//div[@id='schedulercalendar-fc-container']/div/table/tbody/tr/td[3]/span[4]");
        private By nextDateSelect = By.XPath("//div[@id='schedulercalendar-fc-container']/div/table/tbody/tr/td[3]/span[5]");
        private By fcAgendaSlots = By.ClassName("fc-agenda-slots");
        private By fcEventInner = By.ClassName("fc-event-inner");
        private By eventContainer = By.Id("modal-inner-container");
        private By eventTitle = By.CssSelector("#modal-title");
        private By referenceNumber = By.CssSelector("#schedulercalendar-display-number-input");
        private By trailerOrContainerNumber = By.CssSelector("#scheduler-calendar-equipment-input");
        private By equipmentType = By.XPath("//div[@id='modal-content-region']/div/dl[2]/dd[2]");
        private By equipmentLength = By.XPath("//div[@id='modal-content-region']/div/dl[2]/dd[3]");
        private By loadNumber = By.XPath("//div[@id='modal-content-region']/div/div[1]/div/dl/dd/a");
        private By schedularName = By.CssSelector("#display-scheduled-by-name");
        private By eventTitleInput = By.CssSelector("schedulercalendar-title-input");
        private By eventCalendarInput = By.Id("schedulercalendar-date-input");
        private By eventTimeInput = By.Id("schedulercalendar-time-input");
        private By eventEndTimeInput = By.Id("schedulercalendar-endtime-input");
        private By eventNotesInput = By.Id("schedulercalendar-notes-input");
        private By eventCreateButton = By.XPath("//form[@id='schedulercalendar-form']/ul/li[4]/button");
        private By eventCancelButton = By.XPath("//form[@id='schedulercalendar-form']/ul/li[4]/button[2]");
        private By eventDeleteButton = By.CssSelector("sc-event-delete");
        private By summaryTab = By.XPath(".//*[@id='summary']/header");


        public UIItem AppTitle { get { return new UIItem("Facility Calendar>> App Title", this.appTitle, _driver); } }
        public UIItem FacilitySelectorInput { get { return new UIItem("Facility Calendar>> Facility Selector Dropdown", this.facilitySelectorInput, _driver); } }
        public UIItem SchedulerCanlendarReportLink { get { return new UIItem("Facility Calendar>> Report Link", this.schedulerCalendarReportLink, _driver); } }
        public UIItem BlockAllButton { get { return new UIItem("Facility Calendar>> Block All Button", this.blockAllButton, _driver); } }
        public UIItem UnblockAllButton { get { return new UIItem("Facility Calendar>> Unblock All Button", this.unblockAllButton, _driver); } }
        public UIItem CalendarDateSelect { get { return new UIItem("Facility Calendar>> Calendar Date Select", this.calendarDateSelect, _driver); } }
        public UIItem TodayDateSelect { get { return new UIItem("Facility Calendar>> Today Date Select", this.todayDateSelect, _driver); } }
        public UIItem PreviousDateSelect { get { return new UIItem("Facility Calendar>> Previous Date Select", this.previousDateSelect, _driver); } }
        public UIItem NextDateSelect { get { return new UIItem("Facility Calendar>> Next Date Select", this.nextDateSelect, _driver); } }
        public UIItem FcAgendaSlots { get { return new UIItem("Facility Calendar>> Agenda Slots", this.fcAgendaSlots, _driver); } }
        public UIItem FcEventInner { get { return new UIItem("Facility Calendar>> New Event Select", this.fcEventInner, _driver); } }
        public UIItem EventContainer { get { return new UIItem("Facility Calendat>> Event Container", this.eventContainer, _driver); } }
        public UIItem EventTitle { get { return new UIItem("Facility Calendar>> Title Carrier Name", this.eventTitle, _driver); } }
        public UIItem ReferenceNumber { get { return new UIItem("Facility Calendar>> Reference Number", this.referenceNumber, _driver); } }
        public UIItem TrailerOrContainerNumber { get { return new UIItem("Facility Calendar>> Trailer or Container Number", this.trailerOrContainerNumber, _driver); } }
        public UIItem EquipmentType { get { return new UIItem("Facility Calendar>> Equipment Type", this.equipmentType, _driver); } }
        public UIItem EquipmentLength { get { return new UIItem("Facility Calendar>> Equipment Length", this.equipmentLength, _driver); } }
        public UIItem LoadNumber { get { return new UIItem("Facility Calendar>> Load Number", this.loadNumber, _driver); } }
        public UIItem SchedularName { get { return new UIItem("Facility Calendar>> Schedular Name", this.schedularName, _driver); } }
        public UIItem EventTitleInput { get { return new UIItem("Facility Calendar>> Event Title Input", this.eventTitleInput, _driver); } }
        public UIItem EventCalendarInput { get { return new UIItem("Facility Calendar>> Event Calendar Input", this.eventCalendarInput, _driver); } }
        public UIItem EventTimeInput { get { return new UIItem("Facility Calendar>> Event Time Input", this.eventTimeInput, _driver); } }
        public UIItem EventEndTimeInput { get { return new UIItem("Facility Calendar>> Event End Time Input", this.eventEndTimeInput, _driver); } }
        public UIItem EventNotesInput { get { return new UIItem("Facility Calendar>> Event Notes Input", this.eventNotesInput, _driver); } }
        public UIItem EventCreateButton { get { return new UIItem("Facility Calendar>> Event Create Button", this.eventCreateButton, _driver); } }
        public UIItem EventCancelButton { get { return new UIItem("Facility Calendar>> Event Cancel Button", this.eventCancelButton, _driver); } }
        public UIItem EventDeleteButton { get { return new UIItem("Facility Calendar>> Event Delete Button", this.eventDeleteButton, _driver); } }
        public UIItem SummaryTab { get { return new UIItem("Load Details>> Summary Tab", this.summaryTab, _driver); } }

    }
}
