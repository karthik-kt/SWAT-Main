using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAT.Applications.Claw.DAL
{
    using SWAT.Data;

    class FacilityCalendarData
    {
        private DataManager _datamanager;

        public FacilityCalendarData(DataManager dataManager)
        {
            _datamanager = dataManager;
                        
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
        public string EquipmentLength
        {
            get
            {
                return _datamanager.Data("EquipmentLength");
            }
            set
            {
                _datamanager.SetData("EquipmentLength", value);
            }
        }
        public string EquipmentType
        {
            get
            {
                return _datamanager.Data("EquipmentType");
            }
            set
            {
                _datamanager.SetData("EquipmentType", value);
            }
        }
        public string EventDate
        {
            get
            {
                return _datamanager.Data("EventDate");
            }
            set
            {
                _datamanager.SetData("EventDate", value);
            }
        }
        public string EventNotes
        {
            get
            {
                return _datamanager.Data("EventNotes");
            }
            set
            {
                _datamanager.SetData("EventNotes", value);
            }
        }
        public string EventTime
        {
            get
            {
                return _datamanager.Data("EventTime");
            }
            set
            {
                _datamanager.SetData("EventTime", value);
            }
        }
        public string EventTitle
        {
            get
            {
                return _datamanager.Data(" EventTitle");
            }
            set
            {
                _datamanager.SetData("EventTitle", value);
            }
        }
        public string FromTime
        {
            get
            {
                return _datamanager.Data("FromTime");
            }
            set
            {
                _datamanager.SetData("FromTime", value);
            }
        }
        public string LoadNumber
        {
            get
            {
                return _datamanager.Data("LoadNumber");
            }
            set
            {
                _datamanager.SetData("LoadNumber", value);
            }
        }
        public string ReferenceNumber
        {
            get
            {
                return _datamanager.Data("ReferenceNumber");
            }
            set
            {
                _datamanager.SetData("ReferenceNumber", value);
            }
        }
        public string ToTime
        {
            get
            {
                return _datamanager.Data("ToTime");
            }
            set
            {
                _datamanager.SetData("ToTime", value);
            }
        }
    }

}
