using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace SWAT.Applications.Claw.DAL
{
    using SWAT.Data;

    public class AvailableLoadData
    {
        private DataManager _datamanager;
        public AvailableLoadData(DataManager datamanager)
        {
            _datamanager = datamanager;
        }
        public string SearchUsing
        {
            get
            {
                return _datamanager.Data("SearchUsing");
            }
            set
            {
                _datamanager.SetData("SearchUsing", value);
            }
        }
        public string FromDate
        {
            get
            {
                return _datamanager.Data("FromDate");
            }
            set
            {
                _datamanager.SetData("FromDate", value);
            }
        }
        public string ToDate
        {
            get
            {
                return _datamanager.Data("ToDate");
            }
            set
            {
                _datamanager.SetData("ToDate", value);
            }
        }
        public string OriginState
        {
            get
            {
                return _datamanager.Data("OriginState");
            }
            set
            {
                _datamanager.SetData("OriginState", value);
            }
        }
        public string OriginCity
        {
            get
            {
                return _datamanager.Data("OriginCity");
            }
            set
            {
                _datamanager.SetData("OriginCity", value);
            }
        }
        public string OriginDeadhead
        {
            get
            {
                return _datamanager.Data("OrginDeadHead");
            }
            set
            {
                _datamanager.SetData("OrginDeadHead", value);
            }
        }
        public string DestinationState
        {
            get
            {
                return _datamanager.Data("DestinationState");
            }
            set
            {
                _datamanager.SetData("DestinationState", value);
            }
        }
        public string DestinationCity
        {
            get
            {
                return _datamanager.Data("DestinationCity");
            }
            set
            {
                _datamanager.SetData("DestinationCity", value);
            }
        }
        public string DestinationDeadhead
        {
            get
            {
                return _datamanager.Data("DestinationDeadhead");
            }
            set
            {
                _datamanager.SetData("DestinationDeadhead", value);
            }
        }
        public string EmptyLocation
        {
            get
            {
                return _datamanager.Data("EmptyLocation");
            }
            set
            {
                _datamanager.SetData("EmptyLocation", value);
            }
        }
        public string EmptyDate
        {
            get
            {
                return _datamanager.Data("EmptyDate");
            }
            set
            {
                _datamanager.SetData("EmptyDate", value);
            }
        }
        public string EmptyTime
        {
            get
            {
                return _datamanager.Data("EmptyTime");
            }
            set
            {
                _datamanager.SetData("EmptyTime", value);
            }
        }
        public string OfferEquipmentType
        {
            get
            {
                return _datamanager.Data("OfferEquipmentType");
            }
            set
            {
                _datamanager.SetData("OfferEquipmentType", value);
            }
        }
        public string OfferAmount
        {
            get
            {
                return _datamanager.Data("OfferAmount");
            }
            set
            {
                _datamanager.SetData("OfferAmount", value);
            }
        }
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
        public string Origin
        {
            get
            {
                return _datamanager.Data("Origin");
            }
            set
            {
                _datamanager.SetData("Origin", value);
            }
        }
        public string Destination
        {
            get
            {
                return _datamanager.Data("Destination");
            }
            set
            {
                _datamanager.SetData("Destination", value);
            }
        }
        public string Equipment
        {
            get
            {
                return _datamanager.Data("Equipment");
            }
            set
            {
                _datamanager.SetData("Equipment", value);
            }
        }
        public string Length
        {
            get
            {
                return _datamanager.Data("Length");
            }
            set
            {
                _datamanager.SetData("Length", value);
            }
        }
    }
    
}
