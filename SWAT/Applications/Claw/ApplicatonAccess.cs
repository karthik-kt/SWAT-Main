using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;

namespace SWAT.Applications.Claw
{
    using SWAT.Applications.Claw.DAL;
    using SWAT.Applications.Claw.ObjectRepository;
    using SWAT.Configuration;
    using SWAT.Data;
    using SWAT.Logger;
    using SWAT.FrameWork.Utilities;

    public class ApplicationAccess
    {
        ApplicationAccessData _ApplicationAccessData;
        ApplicationAccessPage _ApplicationAccessPage;
        IWebDriver _driver;
        

        public ApplicationAccess(TestStartInfo teststartinfo, DataManager datamanager)
        {
            _ApplicationAccessData = new ApplicationAccessData(datamanager);
            _ApplicationAccessPage = new ApplicationAccessPage(teststartinfo);
            _driver = teststartinfo.Driver;
        }

        public string ApplicationAccessVerify()
        {
            try
            {
                Assert.IsTrue(ApplicationAccessOpen());
                Assert.IsTrue(ApplicationAccessStatusCheck());
                return "AccessCheckSuccess";
            }
            catch
            {
                return "AccessCheckFailed";
            }
        }

        private bool ApplicationAccessStatusCheck()
        {
            try
            {
                Assert.IsTrue(_ApplicationAccessPage.SchedulerUserCanAccess.StatusCheckORPerFormAction(_ApplicationAccessData.SchedulerUserCanAccess));
                Assert.IsTrue(_ApplicationAccessPage.EdiConfiguratorUserCanAccess.StatusCheckORPerFormAction(_ApplicationAccessData.EdiConfiguratorUserCanAccess));
                Assert.IsTrue(_ApplicationAccessPage.CoyoteGOShowLoadFinder.StatusCheckORPerFormAction(_ApplicationAccessData.CoyoteGOShowLoadFinder));
                Assert.IsTrue(_ApplicationAccessPage.MyLoadsUserCanAccess.StatusCheckORPerFormAction(_ApplicationAccessData.MyLoadsUserCanAccess));
                Assert.IsTrue(_ApplicationAccessPage.MyLoadsEditLoadDetails.StatusCheckORPerFormAction(_ApplicationAccessData.MyLoadsEditLoadDetails));
                Assert.IsTrue(_ApplicationAccessPage.MyLoadsFlagHighPriorityCommodities.StatusCheckORPerFormAction(_ApplicationAccessData.MyLoadsFlagHighPriorityCommodities));
                Assert.IsTrue(_ApplicationAccessPage.MyLoadsViewTrackingNotes.StatusCheckORPerFormAction(_ApplicationAccessData.MyLoadsViewTrackingNotes));
                Assert.IsTrue(_ApplicationAccessPage.MyLoadsAllowUserToViewBillOfLading.StatusCheckORPerFormAction(_ApplicationAccessData.MyLoadsAllowUserToViewBillOfLading));
                Assert.IsTrue(_ApplicationAccessPage.MyLoadsEnableRequestAdvance.StatusCheckORPerFormAction(_ApplicationAccessData.MyLoadsEnableRequestAdvance));
                Assert.IsTrue(_ApplicationAccessPage.MyLoadsScheduleAndModifyAppointments.StatusCheckORPerFormAction(_ApplicationAccessData.MyLoadsScheduleAndModifyAppointments));
                Assert.IsTrue(_ApplicationAccessPage.MyLoadsAllowUserToReportALumper.StatusCheckORPerFormAction(_ApplicationAccessData.MyLoadsAllowUserToReportALumper));
                Assert.IsTrue(_ApplicationAccessPage.CarrierAccountingUserCanAccess.StatusCheckORPerFormAction(_ApplicationAccessData.CarrierAccountingUserCanAccess));
                Assert.IsTrue(_ApplicationAccessPage.FactoringAccountingUserCanAccess.StatusCheckORPerFormAction(_ApplicationAccessData.FactoringAccountingUserCanAccess));
                Assert.IsTrue(_ApplicationAccessPage.CarrierAccountingAllowAutoVoucher.StatusCheckORPerFormAction(_ApplicationAccessData.CarrierAccountingAllowAutoVoucher));
                Assert.IsTrue(_ApplicationAccessPage.CarrierPreferencesUserCanAccess.StatusCheckORPerFormAction(_ApplicationAccessData.CarrierPreferencesUserCanAccess));
                Assert.IsTrue(_ApplicationAccessPage.ConfirmLoadsUserCanAccess.StatusCheckORPerFormAction(_ApplicationAccessData.ConfirmLoadsUserCanAccess));
                Assert.IsTrue(_ApplicationAccessPage.AvailableLoadsUserCanAccess.StatusCheckORPerFormAction(_ApplicationAccessData.AvailableLoadsUserCanAccess));
                Assert.IsTrue(_ApplicationAccessPage.DashboardUserCanAccess.StatusCheckORPerFormAction(_ApplicationAccessData.DashboardUserCanAccess));
                Assert.IsTrue(_ApplicationAccessPage.DashboardHighPriorityShipments.StatusCheckORPerFormAction(_ApplicationAccessData.DashboardHighPriorityShipments));
                Assert.IsTrue(_ApplicationAccessPage.DashboardMichelinGadget.StatusCheckORPerFormAction(_ApplicationAccessData.DashboardMichelinGadget));
                Assert.IsTrue(_ApplicationAccessPage.DashboardRyderGadget.StatusCheckORPerFormAction(_ApplicationAccessData.DashboardRyderGadget));
                Assert.IsTrue(_ApplicationAccessPage.DashboardFuelProgram.StatusCheckORPerFormAction(_ApplicationAccessData.DashboardFuelProgram));
                Assert.IsTrue(_ApplicationAccessPage.AcceptTendersUserCanAccess.StatusCheckORPerFormAction(_ApplicationAccessData.AcceptTendersUserCanAccess));
                Assert.IsTrue(_ApplicationAccessPage.DistanceCalcUserCanAccess.StatusCheckORPerFormAction(_ApplicationAccessData.DistanceCalcUserCanAccess));
                Assert.IsTrue(_ApplicationAccessPage.ManageUsers.StatusCheckORPerFormAction(_ApplicationAccessData.ManageUsers));
                Assert.IsTrue(_ApplicationAccessPage.AllowUserToEditSetting.StatusCheckORPerFormAction(_ApplicationAccessData.AllowUserToEditSettings));
                Assert.IsTrue(_ApplicationAccessPage.ManageFacilities.StatusCheckORPerFormAction(_ApplicationAccessData.ManageFacilities));
                Assert.IsTrue(_ApplicationAccessPage.ManageCustomers.StatusCheckORPerFormAction(_ApplicationAccessData.ManageCustomers));
                Assert.IsTrue(_ApplicationAccessPage.ManageCarriers.StatusCheckORPerFormAction(_ApplicationAccessData.ManageCarriers));
                Assert.IsTrue(_ApplicationAccessPage.FacilityCalendarUserCanAccess.StatusCheckORPerFormAction(_ApplicationAccessData.FacilityCalendarUserCanAccess));
                Assert.IsTrue(_ApplicationAccessPage.OrderManagerUserCanAccess.StatusCheckORPerFormAction(_ApplicationAccessData.OrderManagerUserCanAccess));
                Assert.IsTrue(_ApplicationAccessPage.OrderManagerViewAll.StatusCheckORPerFormAction(_ApplicationAccessData.OrderManagerViewAll));
                Assert.IsTrue(_ApplicationAccessPage.OrderManagerEdit.StatusCheckORPerFormAction(_ApplicationAccessData.OrderManagerEdit));
                Assert.IsTrue(_ApplicationAccessPage.OrderManagerCreate.StatusCheckORPerFormAction(_ApplicationAccessData.OrderManagerCreate));
                Assert.IsTrue(_ApplicationAccessPage.OrderManagerCancel .StatusCheckORPerFormAction(_ApplicationAccessData.OrderManagerCancel));
                Assert.IsTrue(_ApplicationAccessPage.RoutingGuideUserCanAccess.StatusCheckORPerFormAction(_ApplicationAccessData.RoutingGuideUserCanAccess));
                Assert.IsTrue(_ApplicationAccessPage.CustomizeThemeUserCanAccess.StatusCheckORPerFormAction(_ApplicationAccessData.CustomizeThemeUserCanAccess));
                Assert.IsTrue(_ApplicationAccessPage.LtlFreightQuoteUserCanAccess.StatusCheckORPerFormAction(_ApplicationAccessData.LtlFrieghtQuoteUserCanAccess));

                return true;
            }
            catch
            {
                return false;
            }
        }

        

        private bool ApplicationAccessOpen()
        {
            try
            {
                _ApplicationAccessPage.ApplicationAccessTab.Click();
                Thread.Sleep(Constants.Wait_Medium);
                Assert.IsTrue(_ApplicationAccessPage.ApplicationAccessTitle.WaitUntilDisplayed());
                return true;
            }
            catch
            {
                MyLogger.Log("Opening application access page failed.");
                return false;
            }
        }

        public string VerifyAccessHeaders()
        {
            string ActualResult = string.Empty;
            try
            {
                Assert.IsTrue(ApplicationAccessOpen());
                switch (_ApplicationAccessData.UserType)
                {
                    case "Employee":
                        ActualResult = VerifyEmployeeHeaders();
                        break;
                    case "Customer":
                        ActualResult = VerifyCustomerHeaders();
                        break;
                    case "Carrier":
                        ActualResult = VerifyCarrierHeaders();
                        break;
                    case "Facility":
                        ActualResult = VerifyFacilityHeaders();
                        break;
                    case "Factoring Company":
                        ActualResult = VerifyFactoringCompanyHeaders();
                        break;
                }
                return ActualResult;
            }
            catch
            {
                return "VerificationFail";
            }
        }

        private string VerifyEmployeeHeaders()
        {
            EmployeeApplicationAccessHeaders EmployeeHeaders = new EmployeeApplicationAccessHeaders(_driver);
            try
            {
                Assert.IsTrue(EmployeeHeaders.Scheduler.IsDisplayed());
                Assert.IsTrue(EmployeeHeaders.EdiConfigurator.IsDisplayed());
                Assert.IsTrue(EmployeeHeaders.DistanceCalculator.IsDisplayed());
                Assert.IsTrue(EmployeeHeaders.Admin.IsDisplayed());
                Assert.IsTrue(EmployeeHeaders.FacilityCalendar.IsDisplayed());
                Assert.IsTrue(EmployeeHeaders.OrderManager.IsDisplayed());
                Assert.IsTrue(EmployeeHeaders.RoutingGuideManager.IsDisplayed());
                Assert.IsTrue(EmployeeHeaders.CustomizeTheme.IsDisplayed());
                Assert.IsTrue(EmployeeHeaders.LtlFrieghtQuote.IsDisplayed());
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFail";
            }
        }

        private string VerifyCustomerHeaders()
        {
            CustomerApplicationAccessHeaders CustomerHeaders = new CustomerApplicationAccessHeaders(_driver);
            try
            {
                Assert.IsTrue(CustomerHeaders.Scheduler.IsDisplayed());
                Assert.IsTrue(CustomerHeaders.FacilityCalendar.IsDisplayed());
                Assert.IsTrue(CustomerHeaders.OrderManager.IsDisplayed());
                Assert.IsTrue(CustomerHeaders.RoutingGuideManager.IsDisplayed());
                Assert.IsTrue(CustomerHeaders.MyLoad.IsDisplayed());
                Assert.IsTrue(CustomerHeaders.CustomerAccounting.IsDisplayed());
                Assert.IsTrue(CustomerHeaders.Dashboard.IsDisplayed());
                Assert.IsTrue(CustomerHeaders.Reporting.IsDisplayed());
                Assert.IsTrue(CustomerHeaders.LtlFrieghtQuote.IsDisplayed());
                Assert.IsTrue(CustomerHeaders.Admin.IsDisplayed());
                Assert.IsTrue(CustomerHeaders.AdditionalSettings.IsDisplayed());
                Assert.IsTrue(CustomerHeaders.CustomSSRSReports.IsDisplayed());
                Assert.IsTrue(CustomerHeaders.PremiumReporting.IsDisplayed());
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFail";
            }
        }

        private string VerifyCarrierHeaders()
        {
            CarrierApplicationAccessHeaders CarrierHeaders = new CarrierApplicationAccessHeaders(_driver);
            try
            {
                Assert.IsTrue(CarrierHeaders.Scheduler.IsDisplayed());
                Assert.IsTrue(CarrierHeaders.CoyoteGo.IsDisplayed());
                Assert.IsTrue(CarrierHeaders.MyLoad.IsDisplayed());
                Assert.IsTrue(CarrierHeaders.CarrierAccounting.IsDisplayed());
                Assert.IsTrue(CarrierHeaders.CarrierPreferences.IsDisplayed());
                Assert.IsTrue(CarrierHeaders.ConfirmLoads.IsDisplayed());
                Assert.IsTrue(CarrierHeaders.AvailableLoads.IsDisplayed());
                Assert.IsTrue(CarrierHeaders.Dashboard.IsDisplayed());
                Assert.IsTrue(CarrierHeaders.AcceptTender.IsDisplayed());
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFail";
            }
        }

        private string VerifyFacilityHeaders()
        {
            FacilityApplicationAccessHeaders FacilityHeaders = new FacilityApplicationAccessHeaders(_driver);
            try
            {
                Assert.IsTrue(FacilityHeaders.Scheduler.IsDisplayed());
                Assert.IsTrue(FacilityHeaders.FacilityCalendar.IsDisplayed());
                Assert.IsTrue(FacilityHeaders.OrderManager.IsDisplayed());
                Assert.IsTrue(FacilityHeaders.MyLoads.IsDisplayed());
                Assert.IsTrue(FacilityHeaders.Dashboard.IsDisplayed());
                Assert.IsTrue(FacilityHeaders.AdditionalSettings.IsDisplayed());
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFail";
            }
        }

        private string VerifyFactoringCompanyHeaders()
        {
            FactoringCompanyApplicationAccessHeaders FactoringCompanyHeaders = new FactoringCompanyApplicationAccessHeaders(_driver);
            try
            {
                Assert.IsTrue(FactoringCompanyHeaders.Accounting.IsDisplayed());
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFail";
            }
        }
    }
}