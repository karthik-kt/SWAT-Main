using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using SWAT.Data;

namespace SWAT.Applications.Claw.DAL
{
    class CreateLTLLoadData
    {
        DataManager _datamanager;
        public CreateLTLLoadData(DataManager datamanager)
        {
            _datamanager = datamanager;
            
            CommoditiesTable_Row = datamanager.Data("CommoditiesTable_Row");
            Commodity_Type = datamanager.Data("Commodity_Type");
            Commodity_PieceCount = datamanager.Data("Commodity_PieceCount");
            Commodity_Type_Edit = datamanager.Data("Commodity_Type_Edit");
            Commodity_PieceCount_Edit = datamanager.Data("Commodity_PieceCount_Edit");
            Commodity_PackagingType = datamanager.Data("Commodity_PackagingType");
            Commodity_Weight = datamanager.Data("Commodity_Weight");
            Commodity_Dimensions_L = datamanager.Data("Commodity_Dimensions_L");
            Commodity_Dimensions_W = datamanager.Data("Commodity_Dimensions_W");
            Commodity_Dimensions_H = datamanager.Data("Commodity_Dimensions_H");
            Commodity_HazmatClass = datamanager.Data("Commodity_HazmatClass");
            Commodity_HazmatContactPhone = datamanager.Data("Commodity_HazmatContactPhone");
            Commodity_FreightClass = datamanager.Data("Commodity_FreightClass");
            Commodity_NMFC = datamanager.Data("Commodity_NMFC");
            Commodity_NMFCSub = datamanager.Data("Commodity_NMFCSub");

            Pickup_ContactName = datamanager.Data("Pickup_ContactName");
            Pickup_PhoneNumber = datamanager.Data("Pickup_PhoneNumber");
            Pickup_Email = datamanager.Data("Pickup_Email");
            Pickup_HoursOpen = datamanager.Data("Pickup_HoursOpen");
            Pickup_HoursClose = datamanager.Data("Pickup_HoursClose");
            Pickup_CompanyName = datamanager.Data("Pickup_CompanyName");
            Pickup_Address = datamanager.Data("Pickup_Address");
            Pickup_Facility = datamanager.Data("Pickup_Facility");

            Delivery_ContactName = datamanager.Data("Delivery_ContactName");
            Delivery_PhoneNumber = datamanager.Data("Delivery_PhoneNumber");
            Delivery_Email = datamanager.Data("Delivery_Email");
            Delivery_HoursOpen = datamanager.Data("Delivery_HoursOpen");
            Delivery_HoursClose = datamanager.Data("Delivery_HoursClose");
            Delivery_CompanyName = datamanager.Data("Delivery_CompanyName");
            Delivery_Address = datamanager.Data("Delivery_Address");
            Delivery_Facility = datamanager.Data("Delivery_Facility");

            QuoteInfo_Orgin_ZipCode = datamanager.Data("QuoteInfo_Orgin_ZipCode");
            QuoteInfo_Orgin_City = datamanager.Data("QuoteInfo_Orgin_City");
            QuoteInfo_Destination_ZipCode = datamanager.Data("QuoteInfo_Destination_ZipCode");
            QuoteInfo_Destination_City = datamanager.Data("QuoteInfo_Destination_City");
            QuoteInfo_PalletCount = datamanager.Data("QuoteInfo_PalletCount");
            QuoteInfo_PickupDate = datamanager.Data("QuoteInfo_PickupDate");

            Customer_Name = datamanager.Data("CustomerName");
            Customer_Action = datamanager.Data("Action");
            Entity = datamanager.Data("Entity");
            CommodityModalTitle = datamanager.Data("CommodityModalTitle");
        }
        public string CommodityModalTitle { get; set; }
        public string Entity { get; private set; }
        public string CommoditiesTable_Row { get; private set; }
        public string Commodity_Type { get; private set; }
        public string Commodity_PieceCount { get; private set; }
        public string Commodity_PackagingType { get; private set; }
        public string Commodity_Weight { get; private set; }
        public string Commodity_Dimensions_L { get; private set; }
        public string Commodity_Dimensions_W { get; private set; }
        public string Commodity_Dimensions_H { get; private set; }
        public string Commodity_HazmatClass { get; private set; }
        public string Commodity_HazmatContactPhone { get; private set; }
        public string Commodity_FreightClass { get; private set; }
        public string Commodity_NMFC { get; private set; }
        public string Commodity_NMFCSub { get; private set; }
        public string Commodity_Type_Edit { get; private set; }
        public string Commodity_PieceCount_Edit { get; private set; }

        public string Pickup_ContactName { get; private set; }
        public string Pickup_PhoneNumber { get; private set; }
        public string Pickup_Email { get; private set; }
        public string Pickup_HoursOpen { get; private set; }
        public string Pickup_HoursClose { get; private set; }
        public string Pickup_CompanyName { get; private set; }
        public string Pickup_Address { get; private set; }
        public string Pickup_Facility { get; private set; }

        public string Delivery_ContactName { get; private set; }
        public string Delivery_PhoneNumber { get; private set; }
        public string Delivery_Email { get; private set; }
        public string Delivery_HoursOpen { get; private set; }
        public string Delivery_HoursClose { get; private set; }
        public string Delivery_CompanyName { get; private set; }
        public string Delivery_Address { get; private set; }
        public string Delivery_Facility { get; private set; }

        public string QuoteInfo_Orgin_ZipCode { get; private set; }
        public string QuoteInfo_Orgin_City { get; private set; }
        public string QuoteInfo_Destination_ZipCode { get; private set; }
        public string QuoteInfo_Destination_City { get; private set; }
        public string QuoteInfo_PalletCount { get; private set; }
        public string QuoteInfo_PickupDate { get; private set; }
        public string Customer_Name { get; private set; }
        public string Customer_Action { get; private set; }

        public string LoadID
        {
            get
            {
                return _datamanager.Data("Load #");
            }
            set
            {
                _datamanager.SetData("Load #", value);
            }
        }

        public string CarrierName
        {
            get
            {
                return _datamanager.Data("CarrierName");
            }
            set
            {
                _datamanager.SetData("CarrierName", value);
            }
        }

        public string Pickup_Name
        {
            get
            {
                return _datamanager.Data("Pickup_Name");
            }
            set
            {
                _datamanager.SetData("Pickup_Name", value);
            }
        }
        public string Pickup_Street
        {
            get
            {
                return _datamanager.Data("Pickup_Street");
            }
            set
            {
                _datamanager.SetData("Pickup_Street", value);
            }
        }
        public string Delivery_Name
        {
            get
            {
                return _datamanager.Data("Delivery_Name");
            }
            set
            {
                _datamanager.SetData("Delivery_Name", value);
            }
        }
        public string Delivery_Street
        {
            get
            {
                return _datamanager.Data("Delivery_Street");
            }
            set
            {
                _datamanager.SetData("Delivery_Street", value);
            }
        }
        public string FlatRate
        {
            get
            {
                return _datamanager.Data("FlatRate");
            }
            set
            {
                _datamanager.SetData("FlatRate", value);
            }
        }
        public string FuelSurcharge
        {
            get
            {
                return _datamanager.Data("FuelSurcharge");
            }
            set
            {
                _datamanager.SetData("FuelSurcharge", value);
            }
        }
    }
}
