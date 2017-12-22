using ClosedXML.Excel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Xml;

namespace SWAT.Applications.Claw
{    
    using SWAT.Applications.Claw.DAL;
    using SWAT.Applications.Claw.ObjectRepository;
    using SWAT.Configuration;
    using SWAT.Data;
    using SWAT.Logger;
    using SWAT.FrameWork.Utilities;

    class FacilityCalendar
    {
        FacilityCalendarData _FacilityCalendarData;
        FacilityCalendarPage _FacilityCalendarPage;

        public FacilityCalendar(TestStartInfo teststartinfo, DataManager datamanager)
        {
            _FacilityCalendarData = new FacilityCalendarData(datamanager);
            _FacilityCalendarPage = new FacilityCalendarPage(teststartinfo);
        }

        //Navigation to Facility Calendra page 

        public string Navigate()
        {
            try
            {
                Assert.IsTrue(_FacilityCalendarPage.Navigate());
                Assert.IsTrue(_FacilityCalendarPage.FcAgendaSlots.WaitUntilDisplayed());
                Assert.IsTrue(_FacilityCalendarPage.AppTitle.GetText().Trim().Equals("Facility Calendar"));
                return "NavigationSuccess";
            }
            catch
            {
                return "NavigationFailed";
            }

        }

        public string OpenEvent()
        {
            try
            {
                Assert.IsTrue(_FacilityCalendarPage.FcAgendaSlots.WaitUntilDisplayed());
                Assert.IsTrue(_FacilityCalendarPage.AppTitle.GetText().Trim().Equals("Facility Calendar"));
                Assert.IsNotNull(_FacilityCalendarData.ReferenceNumber);
                Assert.IsTrue(_FacilityCalendarPage.FcEventInner.ClickByText(_FacilityCalendarData.ReferenceNumber));
                Assert.IsTrue(_FacilityCalendarPage.EventContainer.WaitUntilDisplayed());
                return "OpenEventSuccess";
            }
            catch
            {
                return "OpenEventFailed";
            }
            
        }

        public string GetLoadDetails()
        {
            try
            {
                Assert.IsTrue(_FacilityCalendarPage.EventContainer.WaitUntilDisplayed());
                _FacilityCalendarData.CarrierName = _FacilityCalendarPage.EventTitle.GetText();
                _FacilityCalendarData.ReferenceNumber = _FacilityCalendarPage.ReferenceNumber.GetText();
                _FacilityCalendarData.FromTime = _FacilityCalendarPage.EventTimeInput.GetText();
                _FacilityCalendarData.ToTime = _FacilityCalendarPage.EventEndTimeInput.GetText();
                _FacilityCalendarData.EquipmentType = _FacilityCalendarPage.EquipmentType.GetText();
                _FacilityCalendarData.EquipmentLength = _FacilityCalendarPage.EquipmentLength.GetText();
                _FacilityCalendarData.LoadNumber = _FacilityCalendarPage.LoadNumber.GetText();
                return "GetLoadDetailsSuccess";
            }
            catch
            {
                return "GetLoadDetailsFailed";
            }            
        }

        public string OpenLoad()
        {
            try
            {
                Assert.IsTrue(_FacilityCalendarPage.EventContainer.WaitUntilDisplayed());
                Assert.IsTrue(_FacilityCalendarPage.LoadNumber.IsDisplayed());
                Assert.IsTrue(_FacilityCalendarPage.LoadNumber.IsEnabled());
                Assert.IsTrue(_FacilityCalendarPage.LoadNumber.Click());
                Assert.IsTrue(_FacilityCalendarPage.SummaryTab.WaitUntilDisplayed());
                return "OpenLoadSuccess";
            }
            catch
            {
                return "OpenLoadFailed";
            }
        }

        // Events should be load to Schedulercalendar 

        public string dropdown()
        {
            return "Not Implemented";

        }

        // Event Creations

        public string createEvent()
        {
            return "Not Implemented";

        }

        // Export PDF as report

        public string export()
        {
            return "Not Implemented";

        }

    }
}
