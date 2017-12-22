// /////////////////////////////////////////////////////////////////////////////////////
//                           Copyright (c) 2015 - 2015
//                            Coyote Logistics L.L.C.
//                          All Rights Reserved Worldwide
// 
// WARNING:  This program (or document) is unpublished, proprietary
// property of Coyote Logistics L.L.C. and is to be maintained in strict confidence.
// Unauthorized reproduction, distribution or disclosure of this program
// (or document), or any program (or document) derived from it is
// prohibited by State and Federal law, and by local law outside of the U.S.
// /////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


namespace SWAT.Applications.Claw.DAL
{
    using SWAT.Data;

    public class CreateOrderData 
        {
            DataManager _datamanager;
           
            public CreateOrderData(DataManager datamanager)
            {

                CustomerName = datamanager.Data("CustomerName");

                //Contact Information
                ContactName = datamanager.Data("ContactName");
                ContactPhnCountryCode = datamanager.Data("CountryCode");
                ContactPhnNumber = datamanager.Data("PhoneNumber");
                ContactExtenstion = datamanager.Data("Extenstion");
                ContactEmail = datamanager.Data("Email");

                //Route Details
                OrginFacility = datamanager.Data("OrginFacility");
                DestinationFacility = datamanager.Data("DestinationFacility");
                Direction = datamanager.Data("Direction");

                //Data & Time
                //PickUpDate = datamanager.Data("PickUpDate");
                ReadyTime = datamanager.Data("ReadyTime");

                //Shipper Details
                ReferenceNumberPick = datamanager.Data("ReferenceNumberPick");
                ReferenceNumber = datamanager.Data("ReferenceNumber");
                Mode = datamanager.Data("Mode");
                Equipment = datamanager.Data("Equipment");
                MiniLength = datamanager.Data("MiniLength");
                SpecialInstructions = datamanager.Data("SpecialInstructions");
                NumberOfShippingUnit = datamanager.Data("NumberOfShippingUnit");
                ShippingUnit = datamanager.Data("ShippingUnit");

                //Error message
                ErrorMessage = datamanager.Data("ErrorMessage");
                OrginFacilityAddress = datamanager.Data("OrginFacilityAddress");
                OriginCityAndState = datamanager.Data("OriginCityAndState");
                OrginZip = datamanager.Data("OrginZip");
                DestinationZip = datamanager.Data("DestinationZip");
                DestinationCityAndState = datamanager.Data("DestinationCityAndState");
                DestinationFacilityAddress = datamanager.Data("DestinationFacilityAddress");

            //ShppingUnits = GetShppingUnitData();
            //CommoditiesData = new CommodityData(datamanager);
            _datamanager = datamanager;
            }

            private string Today()
            {
                DateTime today = DateTime.Today;
                return today.ToString("MM/dd/yyyy");
            }

            public string OrginZip { get; set; }
            public string OriginCityAndState { get; set; }
            public string OrginFacilityAddress { get; set; }
            public string DestinationZip { get; set; }
            public string DestinationCityAndState { get; set; }
            public string DestinationFacilityAddress { get; set; }
            public string  CustomerName {get;set;}
            public CommodityData CommoditiesData { get; set; }
            public string ContactName { get; set; }

            public string ContactPhnCountryCode { get; set; }

            public string ContactExtenstion { get; set; }

            public string ContactEmail { get; set; }

            public string OrginFacility { get; set; }

            public string DestinationFacility { get; set; }

            public string Direction { get; set; }

            public string ReferenceNumberPick { get; set; }

            public string ReferenceNumber { get; set; }

            public string Mode { get; set; }

            public string Equipment { get; set; }

            public string MiniLength { get; set; }

            public string SpecialInstructions { get; set; }

            public string PickUpDate
            {
                get
                {
                    if (_datamanager.Data("PickUpDate").Equals("!TODAY!"))
                    {
                        return Today();
                    }
                    return _datamanager.Data("PickUpDate");
                }
                set
                {
                    _datamanager.SetData("PickUpDate", value);
                }
            }

            public string ReadyTime { get; set; }

            public string NumberOfShippingUnit { get; set; }

            public string ShippingUnit { get; set; }

            public string ContactPhnNumber { get; private set; }

            public string ErrorMessage { get; set; }

            public string LoadId
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
    }
    
    public class ShippingUnitData 
    {

        public ShippingUnitData(DataManager datamanager)
        {
            //Contact Information
            LoadOn = datamanager.Data("LoadOn");
            UnitQty = datamanager.Data("UnitQty");
            OverDimension = datamanager.Data("OverDimension");
            Length = datamanager.Data("Length");
            Width = datamanager.Data("Width");
            Height = datamanager.Data("Height");
            Stackable = datamanager.Data("Stackable");
            Weight = datamanager.Data("Weight");

        }
        
        public string LoadOn { get; set; }

        public string UnitQty { get; set; }

        public string Length { get; set; }

        public string Width { get; set; }

        public string Height { get; set; }

        public string Stackable { get; set; }

        public string OverDimension { get; set; }

        public string Weight { get; set; }
    }

    public class CommodityData 
    {

        public CommodityData(DataManager datamanager)
        {
            Description = datamanager.Data("Description");
            Item = datamanager.Data("Item");
            PONumber = datamanager.Data("PONumber");
            LineNumber = datamanager.Data("LineNumber");
            SchedLineNumber = datamanager.Data("SchedLineNumber");
            Weight = datamanager.Data("Weight");
            Qty = datamanager.Data("Qty");
            Value = datamanager.Data("Value");
            Packaging = datamanager.Data("Packaging");
            Hazmat = datamanager.Data("Hazmat");
            UnitNmuber = datamanager.Data("UnitNmuber");
            DueDate = datamanager.Data("DueDate");
        }


        public string Description { get; set; }

        public string Item { get; set; }

        public string PONumber { get; set; }

        public string LineNumber { get; set; }

        public string SchedLineNumber { get; set; }

        public string Weight { get; set; }

        public string Qty { get; set; }

        public string Value { get; set; }

        public string Packaging { get; set; }

        public string Hazmat { get; set; }

        public string UnitNmuber { get; set; }

        public string DueDate { get; set; }

    }

    public class UIVerifyData
    {
        public UIVerifyData(DataManager datamanager)
        {
            VerificationSection = datamanager.Data("VerificationSection");
            VerificationElement = datamanager.Data("VerificationElement");
            VerifyFor = datamanager.Data("VerifyFor");
        }
        public string VerificationSection { get; set; }
        public string VerificationElement { get; set; }
        public string VerifyFor { get; set; }
    }
}
