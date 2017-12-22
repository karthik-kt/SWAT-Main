using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using SWAT.Data;

namespace SWAT.Applications.Claw.DAL
{
    class DashboardData
    {
        DataManager _datamanager;

        public DashboardData(DataManager datamanager)
        {
            _datamanager = datamanager;
            Customer_Name = datamanager.Data("CustomerName");
            Customer_Keyword_Search = datamanager.Data("Keyword");
            Customer_Action = datamanager.Data("Action");
            MyLoadsGadget = datamanager.Data("MyLoads");
            MyLoadsCarrierGadget = datamanager.Data("MyCarrierLoads");
            MyLoadsPickingUpToday = datamanager.Data("PickingToday");
            MyLoadsDeliveringToday = datamanager.Data("DeliveringToday");
            MyLoadsPickingUpTomorrow = datamanager.Data("PickingTomorrow");
            MyLoadsDeliveringTomorrow = datamanager.Data("DeliveringTomorrow");
            MyLoadsPickingUpThisWeek = datamanager.Data("PickingThisWeek");
            MyLoadsDeliveringThisWeek = datamanager.Data("DeliveringThisWeek");
            MyLoadsGreenText = datamanager.Data("GreenTextClass");
            MyLoadsJustifiedText = datamanager.Data("JustifiedTextClass");
            Entity = datamanager.Data("Entity");
            Gadget = datamanager.Data("Gadget");
            My_Loads_Links = datamanager.Data("My_Loads_Links");
            My_Loads_Validation = datamanager.Data("My_Loads_Validation");
        }
        public string Gadget { get; set; }
        public string Entity { get; private set; }
        public string Customer_Name { get; private set; }
        public string Customer_Keyword_Search { get; private set; }
        public string Customer_Action { get; private set; }
        public string MyLoadsGadget { get; set; }
        public string MyLoadsCarrierGadget { get; set; }
        public string MyLoadsPickingUpToday { get; set; }
        public string MyLoadsDeliveringToday { get; set; }
        public string MyLoadsPickingUpTomorrow { get; set; }
        public string MyLoadsDeliveringTomorrow { get; set; }
        public string MyLoadsPickingUpThisWeek { get; set; }
        public string MyLoadsDeliveringThisWeek { get; set; }
        public string MyLoadsGreenText { get; set; }
        public string MyLoadsJustifiedText { get; set; }
        public string My_Loads_Links { get; set; }
        public string My_Loads_Validation { get; set; }

        public string Dashboard_Pickup_Today_Count
        {
            get
            {
                return _datamanager.Data("Dashboard_Pickup_Today_Count");
            }
            set
            {
                _datamanager.SetData("Dashboard_Pickup_Today_Count", value);
            }
        }
        public string Dashboard_Deliver_Today_Count
        {
            get
            {
                return _datamanager.Data("Dashboard_Deliver_Today_Count");
            }
            set
            {
                _datamanager.SetData("Dashboard_Deliver_Today_Count", value);
            }
        }
        public string Dashboard_Pickup_Tomorrow_Count
        {
            get
            {
                return _datamanager.Data("Dashboard_Pickup_Tomorrow_Count");
            }
            set
            {
                _datamanager.SetData("Dashboard_Pickup_Tomorrow_Count", value);
            }
        }
        public string Dashboard_Deliver_Tomorrow_Count
        {
            get
            {
                return _datamanager.Data("Dashboard_Deliver_Tomorrow_Count");
            }
            set
            {
                _datamanager.SetData("Dashboard_Deliver_Tomorrow_Count", value);
            }
        }
        public string Dashboard_Pickup_ThisWeek_Count
        {
            get
            {
                return _datamanager.Data("Dashboard_Pickup_ThisWeek_Count");
            }
            set
            {
                _datamanager.SetData("Dashboard_Pickup_ThisWeek_Count", value);
            }
        }
        public string Dashboard_Deliver_ThisWeek_Count
        {
            get
            {
                return _datamanager.Data("Dashboard_Deliver_ThisWeek_Count");
            }
            set
            {
                _datamanager.SetData("Dashboard_Deliver_ThisWeek_Count", value);
            }
        }
    }
}
