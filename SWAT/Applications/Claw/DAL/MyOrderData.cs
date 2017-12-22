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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAT.Applications.Claw.DAL
{
    using SWAT.Data;
    class MyOrderData
    {
        DataManager _datamanager;

        public MyOrderData(DataManager datamanager)
        {
            _datamanager = datamanager;
            ReferenceType = datamanager.Data("ReferenceType");
            ReferenceNumber = datamanager.Data("ReferenceNumber");
            Mode = datamanager.Data("Mode");
            EquipmentType = datamanager.Data("EquipmentType");
            EquipmentMiniLength = datamanager.Data("EquipmenMiniLength");
            SpecialInstructions = datamanager.Data("SpecialInstructions");
            ShippingType = datamanager.Data("ShippingType");
            ExcludeWeekend = datamanager.Data("ExcludeWeekend");
            ReadyDate = datamanager.Data("ReadyDate");
            CutOffTimeToSubmitOrderForShipping = datamanager.Data("CutOffTimeToSubmitOrderForShipping");
            StandardLeadTime = datamanager.Data("StandardLeadTime");
            LeadTimeAfterCutOffTime = datamanager.Data("LeadTimeAfterCutOffTime");
            EnableProcessingLeadTime = datamanager.Data("EnableProcessingLeadTime");
            CheckForDate = datamanager.Data("CheckForDate");
            CheckForCutOffTimeSetting = datamanager.Data("CheckForCutOffTimeSetting");
            PhoneCountryCode = datamanager.Data("PhoneCountryCode");
            PhoneExt = datamanager.Data("PhoneExt");
            PhoneNumber = datamanager.Data("PhoneNumber");
            EntityType = datamanager.Data("EntityType");
            TotalLoadToConsolidate = datamanager.Data("TotalLoadToConsolidate");
            DueStartDate = datamanager.Data("DueStartDate");
            DueEndDate = datamanager.Data("DueEndDate");
            SearchOrigin = datamanager.Data("SearchOrigin");
            SearchDestination = datamanager.Data("SearchDestination");
        }
        public string DueStartDate { get; set; }
        public string DueEndDate { get; set; }
        public string SearchOrigin { get; set; }
        public string SearchDestination { get; set; }
        public string TotalLoadToConsolidate { get; set; }
        public string EntityType { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneExt { get; set; }
        public string PhoneCountryCode { get; set; }
        public string ReferenceType { get; set; }
        public string ReferenceNumber { get; set; }
        public string Mode { get; set; }
        public string EquipmentType { get; set; }
        public string EquipmentMiniLength { get; set; }
        public string SpecialInstructions { get; set; }
        public string ShippingType { get; set; }
        public string ExcludeWeekend { get; set; }
        public string ReadyDate { get; set; }
        public string CutOffTimeToSubmitOrderForShipping { get; set; }
        public string StandardLeadTime { get; set; }
        public string LeadTimeAfterCutOffTime { get; set; }
        public string EnableProcessingLeadTime { get; set; }
        public string CheckForDate { get; set; }
        public string CheckForCutOffTimeSetting { get; set; }
    }

    public class PurchaseOrderShippingUnitData
    {

        public PurchaseOrderShippingUnitData(DataManager datamanager)
        {
            //Contact Information
            LoadOn = datamanager.Data("LoadOn");
            UnitQty = datamanager.Data("UnitQty");
            Length = datamanager.Data("Length");
            Width = datamanager.Data("Width");
            Height = datamanager.Data("Height");
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

    public class PurchaseOrderCommodityData
    {
        DataManager _datamanager;
        public PurchaseOrderCommodityData(DataManager datamanager)
        {
            //Contact Information
            _datamanager = datamanager;
            Description = datamanager.Data("Description");
            ItemNumber = datamanager.Data("ItemNumber");
            Weight = datamanager.Data("Weight");
            ActQty = datamanager.Data("ActQty");
            ThreshHold = datamanager.Data("ThreshHold");
            Value = datamanager.Data("Value");
            Packaging = datamanager.Data("Packaging");
            Hazmat = datamanager.Data("Hazmat");
            UnitNmuber = datamanager.Data("UnitNmuber");
        }
        public string Description { get; set; }
        public string Weight { get; set; }
        public string ActQty { get; set; }
        public string ThreshHold { get; set; }
        public string Value { get; set; }
        public string Packaging { get; set; }
        public string Hazmat { get; set; }
        public string UnitNmuber { get; set; }
        public string ItemNumber { get; set; }
        public string RefNumber
        {
            get
            {
                return _datamanager.Data("Ref #");
            }
            set
            {
                _datamanager.SetData("Ref #", value);
            }
        }
        public string OriginalQty
        {
            get
            {
                return _datamanager.Data("OrigQty");
            }
            set
            {
                _datamanager.SetData("OrigQty", value);
            }
        }

        public string LeftOverQty
        {
            get
            {
                return _datamanager.Data("LeftOverQty");
            }
            set
            {
                _datamanager.SetData("LeftOverQty", value);
            }
        }
    }

}
