using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SWAT.Applications.Claw.DAL;
using SWAT.Applications.Claw.ObjectRepository;


namespace SWAT.Applications.Claw
{
    using Logger;
    using SWAT.Data;
    using SWAT.FrameWork.Utilities;
    using Config = SWAT.Configuration.TestStartInfo;

    public class DashBoard : UIObjects
    {
        private DataManager testData;
        private string strObjDashBrdURL = "#";
        private string strObjloadcal = "#calendar-gadget";
        private string strObjmyreports = "#reports";
        private string strObjdailyFacAct = "#daily-facility-activity";
        private string strObjInvoicedAccBal = "#my-account-balance";
        private string strObjhipriship = "#high-priority-shipments";
        private string strObjCostPerCase = "#cost-per-units";
        private string strObjLeadDayStats = "#lead-time";
        private string strObjFuelSurcharge = "#fuel-surcharge";
        private string strObjCarrierAccp = "#acceptance-gadget";

        private string strLoadCal;
        private string strMyReports;
        private string strDailyFacActivity;
        private string strMyTeam;
        private string strInvoicedAccountBalance;
        private string strHighPriShipment;
        private string strCostPerCase;
        private string strLeadDayStats;
        private string strFuelSurcharge;
        private string strBaseURL;
        private string strOpenReport;

        private By byOpenReport = null;

        private Page _page;

        DashboardData _DashboardData;
        DashboardPage _DashboardPage;
        public DashBoard(Config c, DataManager t)
            : base(c)
        {
            testConfig = c;
            driver = testConfig.Driver;
            strBaseURL = t.Data("Link");
            strLoadCal = t.Data("LoadCal");
            strMyReports = t.Data("MyReports");
            strDailyFacActivity = t.Data("DailyFacActivity");
            strMyTeam = t.Data("MyTeam");
            strInvoicedAccountBalance = t.Data("InvoicedAccountBalance");
            strHighPriShipment = t.Data("HighPriShipment");
            strCostPerCase = t.Data("CostPerCase");
            strLeadDayStats = t.Data("LeadDayStats");
            strFuelSurcharge = t.Data("FuelSurcharge");
            strOpenReport = t.Data("OpenReport");
            By byOpenReport = By.LinkText(strOpenReport);
            _page = new Page(c.Driver);
            testData = t;
            _DashboardData = new DashboardData(t);
            _DashboardPage = new DashboardPage(c);
        }

        //Open the dashboard and verify the gadgets displayed.
        public string Gadgets_Verify()
        {
            try
            {
                //driver.Navigate().GoToUrl(testBaseURL + strObjDashBrdURL);
                Navigate(strObjDashBrdURL);
                Assert.IsTrue(WaitUtilDisplayed(By.CssSelector(strObjloadcal)));
                IsDisplayed(By.CssSelector(strObjloadcal), strLoadCal);
                IsDisplayed(By.CssSelector(strObjmyreports), strMyReports);
                IsDisplayed(By.CssSelector(strObjdailyFacAct), strDailyFacActivity);
                IsDisplayed(By.CssSelector(strObjInvoicedAccBal), strMyTeam);
                IsDisplayed(By.CssSelector(strObjhipriship), strInvoicedAccountBalance);
                IsDisplayed(By.CssSelector(strObjCostPerCase), strHighPriShipment);
                IsDisplayed(By.CssSelector(strObjLeadDayStats), strCostPerCase);
                IsDisplayed(By.CssSelector(strObjFuelSurcharge), strLeadDayStats);
                IsDisplayed(By.CssSelector(strObjCarrierAccp), strFuelSurcharge);
                return "GadgetVerificationSuccess";
            }
            catch
            {
                return "GadgetVerificationFailed";
            }
        }

        public string Report_Open()
        {
            byOpenReport = By.XPath(".//*[@id='report-list']/li[1]/a");
            try
            {
                //driver.Navigate().GoToUrl(testBaseURL + strObjDashBrdURL);
                Navigate(strObjDashBrdURL);
                WaitUtilDisplayed(By.CssSelector(strObjloadcal));
                WaitUtilDisplayed(byOpenReport);
                Click(byOpenReport);
                string mainWindow = driver.CurrentWindowHandle;
                foreach (var handle in driver.WindowHandles)
                {
                    if (!handle.Equals(mainWindow))
                    {
                        driver.SwitchTo();
                        // Not sure how to assert
                        break;
                    }
                }
                if (driver.WindowHandles.Count == 1)
                {
                    return "ReportOpenedFailed";
                }
                return "ReportOpenedSuccess";
            }
            catch
            {
                return "ReportOpenedFailed";
            }
        }

        //step 7 - verify are we in my load page.
        //step 8 - get the total number of rows into number of totalrows.
        //step 9 - compare loadcount == totalrows.  
        public string Calendar()
        {
            By myloadpageloads = By.CssSelector(".hook--total");
            By clearall = By.CssSelector(".text-link.hook--clear-all-search");
            string loadcount = null;
            string myldpageloads = null;
            try
            {
                NavigateTo();
                Thread.Sleep(Constants.Wait_Short);
                loadcount = GetLoadCountAndClick();                
                WaitUntilLoading();
                WaitUtilDisplayed(clearall, 30);
                myldpageloads =  GetText(myloadpageloads);
                if (loadcount == myldpageloads)
                {
                    return "NavigationSuccess";
                }
                else
                {
                    return "LoadCountMismatch";
                }
            }
            catch
            {
                return "NavigationFailed";
            }         
        }


        //step 1 - Get list of elements with load count.
        //step 2 - If above doesn't have any value navigate to next month
        //step 3 - repeat step 1 and 2 if needed.
        //step 4 - select the 1st load count.
        //step 5 - get the load count into loadcount.
        //step 6 - click on the load count element.
        private string GetLoadCountAndClick()
        {            
            By loadcount = By.CssSelector(".hook--loadcount");
            By nextmonth = By.CssSelector(".flexbox__item.one-eighth.text-right.clndr-next-button");
            By previousmonth = By.CssSelector(".flexbox__item.one-eighth.clndr-previous-button");
            bool foundloadcount = true;
            string ldcount = null;
            while (foundloadcount)
            {
                try
                {
                    Thread.Sleep(Constants.Wait_Short);
                    List<IWebElement> loadcountlist = GetElements(loadcount);
                    ldcount = loadcountlist[0].Text;
                    loadcountlist[0].Click();
                    return ldcount;
                }
                catch (NoSuchElementException)
                {
                    Click(previousmonth);
                    WaitUntilLoading();                
                }
                catch (ArgumentOutOfRangeException)
                {
                    Click(previousmonth);
                    WaitUntilLoading();
                }
            }
            return GetText(loadcount);
        }

        private void NavigateTo()
        {
           while ( _page.isAt() != "DASHBOARD")
            {
              Navigate(strObjDashBrdURL);
              WaitUtilDisplayed(By.CssSelector(strObjloadcal));         
            }
        }

        public string UIVerify()
        {
            string result = string.Empty;
            try
            {
                if (_DashboardData.Gadget.ToLower().Equals("myloads"))
                {
                    Assert.IsTrue(UIVerifyMyLoadsGadget());
                    if (_DashboardData.My_Loads_Links.ToLower().Equals("yes"))
                        Assert.IsTrue(UIVerifyMyLoadsLinks());
                    if (_DashboardData.My_Loads_Validation.ToLower().Equals("yes"))
                        Assert.IsTrue(MyLoadsValidations());
                }
                result = "UIVerificationSuccess";
            }
            catch (Exception)
            {
                result = "UIVerificationFail";
            }
            return result;
        }
        public bool UIVerifyMyLoadsGadget()
        {
            try
            {
                if (_DashboardData.Entity == Constants.Entity_Carrier)
                {
                    Assert.IsTrue(_DashboardPage.CarrierMyLoads.UIVerify(_DashboardData.MyLoadsCarrierGadget));
                }
                else
                {
                    Assert.IsTrue(_DashboardPage.MyLoads.UIVerify(_DashboardData.MyLoadsGadget));
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UIVerifyMyLoadsLinks()
        {
            try
            {
                if (_DashboardData.Entity == Constants.Entity_Carrier)
                {
                    Assert.IsTrue(_DashboardPage.CarrierMyLoads.WaitUntilDisplayed(30));
                    Assert.IsTrue(_DashboardPage.CarrierMyLoads.UIVerify(_DashboardData.MyLoadsCarrierGadget));
                    Assert.IsTrue(_DashboardPage.Picking_Up_Today.UIVerify(_DashboardData.MyLoadsPickingUpToday));
                    Assert.IsTrue(_DashboardPage.Delivering_Today.UIVerify(_DashboardData.MyLoadsDeliveringToday));
                    Assert.IsTrue(_DashboardPage.Picking_Up_Tomorrow.UIVerify(_DashboardData.MyLoadsPickingUpTomorrow));
                    Assert.IsTrue(_DashboardPage.Delivering_Tomorrow.UIVerify(_DashboardData.MyLoadsDeliveringTomorrow));
                    Assert.IsTrue(_DashboardPage.Picking_Up_This_Week.UIVerify(_DashboardData.MyLoadsPickingUpThisWeek));
                    Assert.IsTrue(_DashboardPage.Delivering_This_Week.UIVerify(_DashboardData.MyLoadsDeliveringThisWeek));
                }
                else
                {
                    Assert.IsTrue(_DashboardPage.MyLoads.WaitUntilDisplayed(30));
                    Assert.IsTrue(_DashboardPage.MyLoads.UIVerify(_DashboardData.MyLoadsGadget));
                    Assert.IsTrue(_DashboardPage.Picking_Up_Today.UIVerify(_DashboardData.MyLoadsPickingUpToday));
                    Assert.IsTrue(_DashboardPage.Delivering_Today.UIVerify(_DashboardData.MyLoadsDeliveringToday));
                    Assert.IsTrue(_DashboardPage.Picking_Up_Tomorrow.UIVerify(_DashboardData.MyLoadsPickingUpTomorrow));
                    Assert.IsTrue(_DashboardPage.Delivering_Tomorrow.UIVerify(_DashboardData.MyLoadsDeliveringTomorrow));
                    Assert.IsTrue(_DashboardPage.Picking_Up_This_Week.UIVerify(_DashboardData.MyLoadsPickingUpThisWeek));
                    Assert.IsTrue(_DashboardPage.Delivering_This_Week.UIVerify(_DashboardData.MyLoadsDeliveringThisWeek));
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool MyLoadsValidations()
        {
            try
            {
                Assert.IsTrue(VerifyTextClass());
                if (_DashboardData.Entity == Constants.Entity_Carrier)
                {
                    MyTasksPage objMyTasksPage = new MyTasksPage(this.testConfig);
                    Assert.IsTrue(CheckPickupWeekData());
                    objMyTasksPage.Navigate();
                    Assert.IsTrue(CheckDeliveryWeekData());
                    objMyTasksPage.Navigate();
                }
                else
                {
                    Assert.IsTrue(CheckPickupWeekData());
                    CLAW objClaw = new CLAW(testConfig, testData);
                    objClaw.NavigateTo("HOME");
                    Assert.IsTrue(CheckDeliveryWeekData());
                    objClaw.NavigateTo("HOME");

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool VerifyTextClass()
        {
            try
            {
                _DashboardPage.Picking_Up_Today.WaitUntilDisplayed(30);
                Assert.IsTrue(_DashboardPage.Picking_Up_Today.HasClass(_DashboardData.MyLoadsGreenText));
                Assert.IsTrue(_DashboardPage.Picking_Up_Today.HasClass(_DashboardData.MyLoadsJustifiedText));
                Assert.IsTrue(_DashboardPage.Delivering_Today.HasClass(_DashboardData.MyLoadsGreenText));
                Assert.IsTrue(_DashboardPage.Delivering_Today.HasClass(_DashboardData.MyLoadsJustifiedText));
                if (_DashboardData.Entity != Constants.Entity_Carrier)
                {
                    Assert.IsTrue(_DashboardPage.Picking_Up_Tomorrow.HasClass(_DashboardData.MyLoadsGreenText));
                    Assert.IsTrue(_DashboardPage.Picking_Up_Tomorrow.HasClass(_DashboardData.MyLoadsJustifiedText));
                    Assert.IsTrue(_DashboardPage.Delivering_Tomorrow.HasClass(_DashboardData.MyLoadsGreenText));
                    Assert.IsTrue(_DashboardPage.Delivering_Tomorrow.HasClass(_DashboardData.MyLoadsJustifiedText));
                }
                Assert.IsTrue(_DashboardPage.Picking_Up_This_Week.HasClass(_DashboardData.MyLoadsGreenText));
                Assert.IsTrue(_DashboardPage.Picking_Up_This_Week.HasClass(_DashboardData.MyLoadsJustifiedText));
                Assert.IsTrue(_DashboardPage.Delivering_This_Week.HasClass(_DashboardData.MyLoadsGreenText));
                Assert.IsTrue(_DashboardPage.Delivering_This_Week.HasClass(_DashboardData.MyLoadsJustifiedText));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetMyLoadsCounts()
        {
            try
            {
                Assert.IsTrue(_DashboardPage.Picking_Up_Today.WaitUntilDisplayed(30));

                _DashboardData.Dashboard_Pickup_Today_Count = _DashboardPage.Picking_Up_Today.GetText().Trim();
                _DashboardData.Dashboard_Deliver_Today_Count = _DashboardPage.Delivering_Today.GetText().Trim();
                _DashboardData.Dashboard_Pickup_ThisWeek_Count = _DashboardPage.Picking_Up_This_Week.GetText().Trim();
                _DashboardData.Dashboard_Deliver_ThisWeek_Count = _DashboardPage.Delivering_This_Week.GetText().Trim();

                if (_DashboardData.Entity != Constants.Entity_Carrier)
                {
                    _DashboardData.Dashboard_Pickup_Tomorrow_Count = _DashboardPage.Picking_Up_Tomorrow.GetText().Trim();
                    _DashboardData.Dashboard_Deliver_Tomorrow_Count = _DashboardPage.Delivering_Tomorrow.GetText().Trim();
                }
                return "CountSuccess";
            }
            catch 
            {
                return "CountFailure";
            }
        }

        private bool CheckEquality(string string1, string string2)
        {
            return (string1.Trim().Equals(string2.Trim()));
        }

        private bool CheckPickupWeekData()
        {
            DateTime fromDate;
            DateTime toDate;
            string dateRange = string.Empty;
            bool result = false;
            CLAW objClaw = new CLAW(testConfig, testData);
            Assert.IsTrue(_DashboardPage.Picking_Up_This_Week.Click());
            _DashboardPage.My_Loads_Search_Results_Headers.WaitUntilDisplayed(30);
            try
            {
                dateRange = _DashboardPage.My_Loads_Search_Pickup_Date.GetAllText()[0];
                if (!string.IsNullOrWhiteSpace(dateRange))
                {
                    dateRange = dateRange.ToLower().Replace("pickup is", string.Empty).Trim();
                    MyLogger.Log("Date range is ::: " + dateRange);
                }
                string[] arrDates = dateRange.Split(new string[] { " – " }, StringSplitOptions.None);
                if (arrDates.Length > 0)
                {
                    fromDate = Convert.ToDateTime(arrDates[0]);
                    toDate = Convert.ToDateTime(arrDates[1]);

                    Assert.IsTrue(fromDate.DayOfWeek.Equals(DayOfWeek.Sunday));
                    Assert.IsTrue(toDate.DayOfWeek.Equals(DayOfWeek.Saturday));
                    Assert.IsTrue(GetDateDiffInDays(fromDate, toDate).Equals(6));
                    MyLogger.Log("First day of date range is ::: " + fromDate.DayOfWeek);
                    MyLogger.Log("Last day of date range is ::: " + toDate.DayOfWeek);
                    MyLogger.Log("Difference of date range is ::: " + Convert.ToString(GetDateDiffInDays(fromDate, toDate)));
                    result = true;
                }
                else {
                    result = false;
                }
            }
            catch (Exception)
            {
                MyLogger.Log("Exception in date range");
                result = false;
            }

            return result;

        }

        private bool CheckDeliveryWeekData()
        {
            DateTime fromDate;
            DateTime toDate;
            string dateRange = string.Empty;
            bool result = false;
            CLAW objClaw = new CLAW(testConfig, testData);
            Assert.IsTrue(_DashboardPage.Delivering_This_Week.Click());
            _DashboardPage.My_Loads_Search_Results_Headers.WaitUntilDisplayed(30);
            try
            {
                dateRange = _DashboardPage.My_Loads_Search_Delivery_Date.GetAllText()[0];
                if (!string.IsNullOrWhiteSpace(dateRange))
                {
                    dateRange = dateRange.ToLower().Replace("delivery is", string.Empty).Trim();
                    MyLogger.Log("Date range is ::: " + dateRange);
                }
                string[] arrDates = dateRange.Split(new string[] { " – " }, StringSplitOptions.None);
                if (arrDates.Length > 0)
                {
                    fromDate = Convert.ToDateTime(arrDates[0]);
                    toDate = Convert.ToDateTime(arrDates[1]);

                    Assert.IsTrue(fromDate.DayOfWeek.Equals(DayOfWeek.Sunday));
                    Assert.IsTrue(toDate.DayOfWeek.Equals(DayOfWeek.Saturday));
                    Assert.IsTrue(GetDateDiffInDays(fromDate, toDate).Equals(6));
                    MyLogger.Log("First day of date range is ::: " + fromDate.DayOfWeek);
                    MyLogger.Log("Last day of date range is ::: " + toDate.DayOfWeek);
                    MyLogger.Log("Difference of date range is ::: " + Convert.ToString(GetDateDiffInDays(fromDate, toDate)));
                    result = true;
                }
                else {
                    result = false;
                }

            }
            catch (Exception)
            {
                MyLogger.Log("Exception in date range");
                result = false;
            }
            return result;
        }
        private double GetDateDiffInDays(DateTime firstDate, DateTime lastDate)
        {
            return (lastDate - firstDate).TotalDays;
        }
    }
}