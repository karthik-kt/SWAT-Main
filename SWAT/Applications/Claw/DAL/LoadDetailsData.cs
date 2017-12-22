using SWAT.Data;
using System;
namespace SWAT.Applications.Claw
{
    class LoadDetailsUIVerifyData
    {
        

        public LoadDetailsUIVerifyData(DataManager datamanager)
        {
            LoadProgress = datamanager.Data("LoadProgress");            
            Dispatch_DriverPhoneText = datamanager.Data("Dispatch_DriverPhone");            
            Dispatch_DriverNameText = datamanager.Data("Dispatch_DriverName");            
            Dispatch_TractorNameText = datamanager.Data("Dispatch_Tractor");
            Dispatch_DispatchDriverButton = datamanager.Data("Dispatch_DriverButton");
            Dispatch_CarrierName = datamanager.Data("Dispatch_CarrierName");
            Dispatch_ProNo = datamanager.Data("Dispatch_ProNo");
            //Dispatch_Tractor = datamanager.Data("Dispatch_Tractor");
            Dispatch_Trailer = datamanager.Data("Dispatch_Trailer");
            TrackingNotes_1stRow_Action = datamanager.Data("TrackingNotes_Action");
            TrackingNotes_1stRow_Notes = datamanager.Data("TrackingNotes_Notes");
            EntityType = datamanager.Data("EntityType");

            Pickup_ArrivalDate = datamanager.Data("Pickup_ArrivalDate");
            Pickup_ArrivalTime = datamanager.Data("Pickup_ArrivalTime");
            Pickup_DepartureDate = datamanager.Data("Pickup_DepartureDate");
            Pickup_DepartureTime = datamanager.Data("Pickup_DepartureTime");

            Delivery_ArrivalDate = datamanager.Data("Delivery_ArrivalDate");
            Delivery_ArrivalTime = datamanager.Data("Delivery_ArrivalTime");
            Delivery_DepartureDate = datamanager.Data("Delivery_DepartureDate");
            Delivery_DepartureTime = datamanager.Data("Delivery_DepartureTime");
            LoadDetails_Tab = datamanager.Data("Page");

            Documents_RequiredDocs = datamanager.Data("Documents_RequiredDocs");
            Charges_Type = datamanager.Data("Charges_Type");
            Charges_Amount = datamanager.Data("Charges_Amount");
            Report_Lumper = datamanager.Data("Report_Lumper");

            TenderStatus_Row = datamanager.Data("TenderStatus_Row");
            TenderStatus_CarrierName = datamanager.Data("TenderStatus_CarrierName");
            TenderStatus_Rank = datamanager.Data("TenderStatus_Rank");
            TenderStatus_OfferStatus = datamanager.Data("TenderStatus_OfferStatus");
            TenderStatus_OfferTime = datamanager.Data("TenderStatus_OfferTime");
            TenderStatus_ResponseTime = datamanager.Data("TenderStatus_ResponseTime");
            TenderStatus_ResponseBy = datamanager.Data("TenderStatus_ResponseBy");
            TenderStatus_ResponseNotes = datamanager.Data("TenderStatus_ResponseNotes");
            DispatchDetail_Message = datamanager.Data("DispatchDetail_Message");
        }

        public string EntityType { get; internal set; }
        public string LoadProgress { get; set; }
        public string Dispatch_DispatchDriverButton { get; set; }
        public string Dispatch_DriverNameText { get; set; }
        public string Dispatch_DriverPhoneText { get;set;}
        public string Dispatch_TractorNameText { get;set;}
        public string Dispatch_CarrierName { get; internal set; }
        public string Dispatch_ProNo { get; internal set; }
        //public string Dispatch_Tractor { get; internal set; }
        public string Dispatch_Trailer { get; internal set; }

        public string TrackingNotes_1stRow_Action { get; internal set; }
        public string TrackingNotes_1stRow_Notes { get; internal set; }
        public string Pickup_ArrivalDate { get; internal set; }
        public string Pickup_ArrivalTime { get; internal set; }
        public string Pickup_DepartureDate { get; internal set; }
        public string Pickup_DepartureTime { get; internal set; }

        public string Delivery_ArrivalDate { get; internal set; }
        public string Delivery_ArrivalTime { get; internal set; }
        public string Delivery_DepartureDate { get; internal set; }
        public string Delivery_DepartureTime { get; internal set; }
        public string LoadDetails_Tab { get; private set; }
        public string Documents_RequiredDocs { get; private set; }
        public string Charges_Type{ get; internal set; }
        public string Charges_Amount { get; private set; }
        public string Report_Lumper { get; private set; }
        public string TenderStatus_CarrierName { get; private set; }
        public string TenderStatus_Rank { get; private set; }
        public string TenderStatus_OfferStatus { get; private set; }
        public string TenderStatus_OfferTime { get; private set; }
        public string TenderStatus_ResponseTime { get; private set; }
        public string TenderStatus_ResponseBy { get; private set; }
        public string TenderStatus_ResponseNotes { get; private set; }
        public string TenderStatus_Row { get; private set; }
        public string DispatchDetail_Message { get; internal set; }
    }


    class LoadDetailsData
    {
        private DataManager _datamanager;
        private string pickup_arrivaldate = null;
        private string pickup_departuredate = null;
        private string pickup_arrivaltime = null;
        private string pickup_departuretime = null;
        public LoadDetailsData(DataManager datamanager)
        {
            pickup_arrivaldate = datamanager.Data("pickup_arrivaldate");
            pickup_departuredate = datamanager.Data("pickup_departuredate");
            pickup_arrivaltime = datamanager.Data("pickup_arrivaltime");
            pickup_departuretime = datamanager.Data("pickup_departuretime");
            Dispatch_Button = datamanager.Data("DispatchButton");
            FirstDriver = datamanager.Data("FirstDriver");
            FirstDriverCellNumber = datamanager.Data("FirstDriverCellNumber");
            Team = datamanager.Data("Team");
            ActualEmptyLocation = datamanager.Data("ActualEmptyLocation");
            EmptyDate = datamanager.Data("EmptyDate");
            EmptyTime = datamanager.Data("EmptyTime");
            EquipmentType = datamanager.Data("EquipmentType");
            EquipmentLength = datamanager.Data("EquipmentLength");
            EquipmentWidth = datamanager.Data("EquipmentWidth");
            EquipmentHeight = datamanager.Data("EquipmentHeight");
            TractorName = datamanager.Data("Tractor#");
            TrailerNumber = datamanager.Data("Trailer#");
            DocumentType = datamanager.Data("DocumentType");
            FilePath = datamanager.Data("FilePath");
            LumperAmount = datamanager.Data("LumperAmount");
            LumperText = datamanager.Data("ReportLumperText");
            Progress = datamanager.Data("Progress");
            EntityName = datamanager.Data("EntityName");
            InvoiceStatus = datamanager.Data("Invoiced_Status");
            AlertText = datamanager.Data("AlertText");
            TrackingNotesAction = datamanager.Data("Action");
            StdPaymentTerms = datamanager.Data("StdPaymentTerms");
            ReqFinalAdvance = datamanager.Data("ReqFinalAdvance");
            VerifyUnitQtyWithFirstCommodity = datamanager.Data("VerifyUnitQtyWithFirstCommodity");
            TotalShipment = datamanager.Data("TotalShipment");
            TotalCommodityInShipment = datamanager.Data("TotalCommodityInShipment");
            ActQtyForAllUnit = datamanager.Data("ActQtyForAllUnit");
            EditLoadCommodity = datamanager.Data("EditLoadCommodity");
            WeightPercent = datamanager.Data("WeightPercent");
            PiecesPercent = datamanager.Data("PiecesPercent");
            TrackingNotesCarrier = datamanager.Data("CarrierName");
            _datamanager = datamanager;
        }

        private string Yesterday()
        {
            TimeSpan day = TimeSpan.FromDays(1);
            DateTime yesterday = DateTime.Today.Subtract(day);
            return yesterday.ToString("MM-dd-yyyy");
        }
        
        public string PickUp_ArrivalDate
        {
            get
            {
                if (pickup_arrivaldate == "!IGNORE!")
                {
                    return Yesterday();
                }
                return pickup_arrivaldate;
            }
            set
            {
                pickup_arrivaldate = value; 
            }
        }

        public string PickUp_DepartureDate
        {
            get
            {
                //if (pickup_departuredate == "!IGNORE!")
                //{
                //    return Yesterday();
                //}
                return pickup_departuredate;
            }
            set
            {
                pickup_departuredate = value;
            }
        }
        public string PickUp_ArrivalTime
        {
            get
            {
                return pickup_arrivaltime; //PickUp_ArrivalTime
            }
            set
            {
                pickup_arrivaltime = value; //PickUp_ArrivalTime

            }
        }
        

        public string PickUp_DepartureTime
        {
            get
            {
                return pickup_departuretime;
            }
            set
            {
                pickup_departuretime = value;
            }
        }
        public string WeightPercent { get; set; }
        public string PiecesPercent { get; set; }
        public string EditLoadCommodity { get; set; }
        public string ActQtyForAllUnit { get; set; }
        public string TotalCommodityInShipment { get; set; }
        public string TotalShipment { get; set; }
        public string VerifyUnitQtyWithFirstCommodity { get; set; }
        public string FirstDriver { get; set; }
        public string FirstDriverCellNumber { get; set; }
        public string Team { get; set; }
        public string ActualEmptyLocation { get; set; }
        public string EmptyDate { get; set; }
        public string EmptyTime { get; set; }
        public string EquipmentType { get; set; }
        public string EquipmentLength { get; set; }
        public string EquipmentWidth { get; set; }
        public string EquipmentHeight { get; set; }
        public string TractorName { get; set; }
        public string TrailerNumber { get; set; }
        public string Dispatch_Button { get; set; }
        public string DocumentType { get; set; }
        public string FilePath { get; set; }
        public string LumperAmount { get; set; }
        public string Progress { get; set; }
        public string LumperText { get; set; }
        public string EntityName { get; set; }
        public string InvoiceStatus { get; set; }
        public string AlertText { get; set; }
        public string TrackingNotesAction { get; set; }
        public string TrackingNotesCarrier { get; set; }
        public string StdPaymentTerms { get; set; }
        public string ReqFinalAdvance { get; set; }
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
}
