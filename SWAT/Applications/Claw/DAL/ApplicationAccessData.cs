using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAT.Applications.Claw.DAL
{
    using SWAT.Data;

    public class ApplicationAccessData
    {
        public string UserType;
        public string SchedulerUserCanAccess;
        public string CoyoteGOShowLoadFinder;
        public string MyLoadsUserCanAccess;
        public string MyLoadsEditLoadDetails;
        public string MyLoadsFlagHighPriorityCommodities;
        public string MyLoadsViewTrackingNotes;
        public string MyLoadsAllowUserToViewBillOfLading;
        public string MyLoadsEnableRequestAdvance;
        public string MyLoadsScheduleAndModifyAppointments;
        public string MyLoadsAllowUserToReportALumper;
        public string MyLoadsShowChargesSectionWithoutAccountingAccess;
        public string CarrierAccountingUserCanAccess;
        public string FactoringAccountingUserCanAccess;
        public string CarrierAccountingAllowAutoVoucher;
        public string CarrierPreferencesUserCanAccess;
        public string ConfirmLoadsUserCanAccess;
        public string AvailableLoadsUserCanAccess;
        public string DashboardUserCanAccess;
        public string DashboardHighPriorityShipments;
        public string DashboardMichelinGadget;
        public string DashboardRyderGadget;
        public string DashboardFuelProgram;
        public string AcceptTendersUserCanAccess;
        public string EdiConfiguratorUserCanAccess;
        public string DistanceCalcUserCanAccess;
        public string ManageUsers;
        public string AllowUserToEditSettings;
        public string ManageFacilities;
        public string ManageCustomers;
        public string ManageCarriers;
        public string FacilityCalendarUserCanAccess;
        public string OrderManagerUserCanAccess;
        public string OrderManagerViewAll;
        public string OrderManagerEdit;
        public string OrderManagerCreate;
        public string OrderManagerCancel;
        public string RoutingGuideUserCanAccess;
        public string CustomizeThemeUserCanAccess;
        public string LtlFrieghtQuoteUserCanAccess;

        public ApplicationAccessData(DataManager datamanager)
        {
            SchedulerUserCanAccess = datamanager.Data("Scheduler_UserCanAccess");
            EdiConfiguratorUserCanAccess = datamanager.Data("EdiConfigurator_UserCanAccess");
            DistanceCalcUserCanAccess = datamanager.Data("DistanceCalc_UserCanAccess");
            CoyoteGOShowLoadFinder = datamanager.Data("CoyoteGO_ShowLoadFinder");
            MyLoadsUserCanAccess = datamanager.Data("MyLoads_UserCanAccess");
            MyLoadsEditLoadDetails = datamanager.Data("MyLoads_EditLoadDetails");
            MyLoadsFlagHighPriorityCommodities = datamanager.Data("MyLoads_FlagHighPriorityCommodities");
            MyLoadsViewTrackingNotes = datamanager.Data("MyLoads_ViewTrackingNotes");
            MyLoadsAllowUserToViewBillOfLading = datamanager.Data("MyLoads_AllowUserToViewBillOfLading");
            MyLoadsEnableRequestAdvance = datamanager.Data("MyLoads_EnableRequestAdvance");
            MyLoadsScheduleAndModifyAppointments = datamanager.Data("MyLoads_ScheduleAndModifyAppointments");
            MyLoadsAllowUserToReportALumper = datamanager.Data("MyLoads_AllowUserToReportALumper");
            MyLoadsShowChargesSectionWithoutAccountingAccess = datamanager.Data("MyLoads_MyLoadsShowChargesSectionWithoutAccountingAccess");
            CarrierAccountingUserCanAccess = datamanager.Data("CarrierAccounting_UserCanAccess");
            FactoringAccountingUserCanAccess = datamanager.Data("FactoringAccounting_UserCanAccess");
            CarrierAccountingAllowAutoVoucher = datamanager.Data("CarrierAccounting_AllowAutoVoucher");
            CarrierPreferencesUserCanAccess = datamanager.Data("CarrierPreferences_UserCanAccess");
            ConfirmLoadsUserCanAccess = datamanager.Data("ConfirmLoads_UserCanAccess");
            AvailableLoadsUserCanAccess = datamanager.Data("AvailableLoads_UserCanAccess");
            DashboardUserCanAccess = datamanager.Data("Dashboard_UserCanAccess");
            DashboardHighPriorityShipments = datamanager.Data("Dashboard_HighPriorityShipments");
            DashboardMichelinGadget = datamanager.Data("Dashboard_MichelinGadget");
            DashboardRyderGadget = datamanager.Data("Dashboard_RyderGadget");
            DashboardFuelProgram = datamanager.Data("Dashboard_FuelProgram");
            AcceptTendersUserCanAccess = datamanager.Data("AcceptTenders_UserCanAccess");
            ManageUsers = datamanager.Data("ManageUsers");
            AllowUserToEditSettings = datamanager.Data("AllowUserToEditSettings");
            ManageFacilities = datamanager.Data("ManageFacilities");
            ManageCustomers = datamanager.Data("ManageCustomers");
            ManageCarriers = datamanager.Data("ManageCarriers");
            FacilityCalendarUserCanAccess = datamanager.Data("FacilityCalendar_UserCanAccess");
            OrderManagerUserCanAccess = datamanager.Data("OrderManager_UserCanAccess");
            OrderManagerViewAll = datamanager.Data("OrderManager_ViewAll");
            OrderManagerEdit = datamanager.Data("OrderManager_Edit");
            OrderManagerCreate = datamanager.Data("OrderManager_Create");
            OrderManagerCancel= datamanager.Data("OrderManager_Cancel");
            RoutingGuideUserCanAccess = datamanager.Data("RoutingGuide_UserCanAccess");
            CustomizeThemeUserCanAccess = datamanager.Data("CustomizeTheme_UserCanAccess");
            LtlFrieghtQuoteUserCanAccess = datamanager.Data("LtlFrieghtQuote_UserCanAccess");
            UserType = datamanager.Data("UserType");
    }
    }
}