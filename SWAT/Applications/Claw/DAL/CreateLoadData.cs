using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace SWAT.Applications.Claw.DAL
{
    using SWAT.Data;

    public class CreateLoadData
    {
        private DataManager _datamanager;
        //private string pickupdateAvailable;
        //private string pickupdateBy;
        public CreateLoadData(DataManager datamanager)
        {
            _datamanager = datamanager;
            LoadId = datamanager.Data("Load #");

            PickupFacility = datamanager.Data("Shipper");
            ContactName = datamanager.Data("ContactName");
            CityCode = datamanager.Data("CityCode");
            PhoneNumber = datamanager.Data("PhoneNumber");
            Extension = datamanager.Data("Extension");
            ReferenceNumber = datamanager.Data("Reference #");
            Mode = datamanager.Data("Mode").ToUpper();
            MinimumLength = datamanager.Data("Minimum Length");
            EquipmentType = datamanager.Data("Equipment Type");
            PickupTime = datamanager.Data("Pickup Time").Replace(':', '\0');
            PickupDate = datamanager.Data("Pickup Date");
            RushOrder = datamanager.Data("Rush Order");
            Notes = datamanager.Data("Notes");
            AppointmentStartTime = datamanager.Data("PickUp_ApptStartTime");
            AppointmentEndTime = datamanager.Data("PickUp_ApptEndTime");
            PickUpDate_Available = datamanager.Data("PickupDate_Avilable");
            PickUpDate_By = datamanager.Data("PickupDate_By");
            PickupNumber = datamanager.Data("Pickup #");
            TarpType = datamanager.Data("TarpType");
            TarpQuantity = datamanager.Data("TarpQuantity");
            AllowDuplicateShipmentId = datamanager.Data("AllowDuplicateShipment");
            TemplateName = datamanager.Data("TemplateName");

            //StopDetails
            Del_AppointmentStartTime = datamanager.Data("Delivery_ApptStartTime");
            Del_AppointmentEndTime = datamanager.Data("Delivery_ApptEndTime");
            DeliveryAvailable = datamanager.Data("DeliveryDate_Avilable");
            DeliverBy = datamanager.Data("DeliveryDate_By");
            DeliveryFacility = datamanager.Data("Consignee");
            DeliveryNumber = datamanager.Data("Delivery #");
            Desc = datamanager.Data("Description").Split(';');
            Wt = datamanager.Data("Weight").Split(';');
            Qty = datamanager.Data("Quantity").Split(';');
            Value = datamanager.Data("Value").Split(';');
            PacType = datamanager.Data("PackageType").Split(';');
            Length = datamanager.Data("Length").Split(';');
            Width = datamanager.Data("Width").Split(';');
            Height = datamanager.Data("Height").Split(';');
            Pallets = datamanager.Data("Pallets").Split(';');
            CustomerName = datamanager.Data("CustomerName");

        }

        private string _loadid = null;

        public void setLoadId(string value)
        {
            _datamanager.SetData("Load #", value);
            _datamanager.SetData("LoadID", value);
            _loadid = value;
        }


        public string LoadId {             
            get
            {
                return _loadid;
            }        
            set
            {
                _loadid = value;
            }
        }

        public string PickupFacility
        {
            get
            {
                return _datamanager.Data("Shipper");
            }
            set
            {
                _datamanager.SetData("Shipper", value);
            }
        }

        public string NewlyCreatedTemplate
        {
            get
            {
                return _datamanager.Data("NewlyCreatedTemplate");
            }
            set
            {
                _datamanager.SetData("NewlyCreatedTemplate", value);
            }
        }

        public string ContactName {
            get
            {
               return _datamanager.Data("ContactName");
            }
            set
            {
                _datamanager.SetData("ContactName", value);
            }
        }

        public string CityCode { get; set; }

        public string PhoneNumber {
            get
            {
                return _datamanager.Data("PhoneNumber");
            }
            set
            {
                _datamanager.SetData("PhoneNumber", value);
            }
        }

        public string Extension {
            get
            {
                return _datamanager.Data("Extension");
            }
            set
            {
                _datamanager.SetData("Extension", value);
            }
        }

        public string AllowDuplicateShipmentId { get; set; }
        public string ReferenceNumber { get; set; }

        public string PickupNumber { get; set; }

        public string Mode { get; set; }        

        public string EquipmentType { get; set; }
  
        public string MinimumLength { get; set; }

        public string PickUpDate_Available {
            get; set;
        }

        public string PickUpDate_By {
            get; set;
        }

        public string PickupTime { get; set; }

        public string PickupDate { get; set; }

        public string AppointmentStartTime { get; set; }

        public string AppointmentEndTime { get; set; }

        public string RushOrder { get; set; }

        public string Notes { get; set; }


        //Stop Details
        public string Del_AppointmentStartTime { get; set; }

        public string Del_AppointmentEndTime { get; set; }

        public string DeliveryAvailable { get; set; }

        public string DeliverBy { get; set; }

        public string DeliveryFacility { get; set; }

        public string DeliveryNumber { get; set; }

        public string[] Desc { get; set; }

        public string[] Wt { get; set; }

        public string[] Qty { get; set; }

        public string[] Value { get; set; }

        public string[] PacType { get; set; }

        public string[] Length { get; set; }

        public string[] Width { get; set; }

        public string[] Height { get; set; }

        public string[] Pallets { get; set; }

        public int NoCommodities { get; set; }
        public string TarpType { get; set; }
        public string TarpQuantity { get; set; }
        public string TemplateName { get; set; }

        public string CustomerName { get; private set; }
    }

    public class CreateLoad_StopData
    {
        private DataManager _datamanager;
        //private string deliveryAvailable;
        //private string deliveryeBy;
        public CreateLoad_StopData(DataManager datamanager)
        {
            _datamanager = datamanager;
            DeliveryFacility = datamanager.Data("DeliveryFacility");
            ContactName = datamanager.Data("ContactName");
            CityCode = datamanager.Data("CityCode");
            PhoneNumber = datamanager.Data("PhoneNumber");
            Extension = datamanager.Data("Extension");
            DeliveryNumber = datamanager.Data("DeliveryNumber");
            DeliveryAvailable = datamanager.Data("DeliveryAvailable");
            DeliveryBy = datamanager.Data("DeliveryBy");
            AppointmentStartTime = datamanager.Data("AppointmentStartTime");
            AppointmentEndTime = datamanager.Data("AppointmentEndTime");
        }

        public string DeliveryFacility { get; set; }
        public string ContactName { get; set; }
        public string CityCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Extension { get; set; }
        public string DeliveryNumber { get; set; }
        public string DeliveryAvailable {
            get; set;
        }
        public string DeliveryBy {
            get; set;
        }
        public string AppointmentStartTime { get; set; }
        public string AppointmentEndTime { get; set; }
        
    }

    public class CreateLoad_CommodityData
    {
        public CreateLoad_CommodityData(DataManager datamanager)
        {
            Description = datamanager.Data("Description");
            Weight = datamanager.Data("Weight");
            Quantity = datamanager.Data("Quantity");
            Value = datamanager.Data("Value");
            PackagingType = datamanager.Data("PackageType");
            Length = datamanager.Data("Length");
            Width = datamanager.Data("Width");
            Height = datamanager.Data("Height");
            Pallets = datamanager.Data("Pallets");
        }
        public string Description { get; set; }

        public string Weight { get; set; }

        public string Quantity { get; set; }

        public string Value { get; set; }

        public string PackagingType { get; set; }

        public string Length { get; set; }

        public string Width { get; set; }

        public string Height { get; set; }

        public string Pallets { get; set; }
    }
}
