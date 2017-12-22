using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAT.Applications.Claw.DAL
{
    using SWAT.Data;

    class PreferencesData
    {
        private DataManager _datamanager;

        public PreferencesData(DataManager datamanager)
        {
            _datamanager = datamanager;
        }
    }

    class ModesData
    {
        public ModesData(DataManager datamanager)
        {
            Air = datamanager.Data("Air");
            Intermodal = datamanager.Data("Intermodal");
            LTL = datamanager.Data("LTL");
            Ocean = datamanager.Data("Ocean");
            Rail = datamanager.Data("Rail");
            TL = datamanager.Data("TL");
        }

        public string Air { get; set; }
        public string Intermodal { get; set; }
        public string LTL { get; set; }
        public string Ocean { get; set; }
        public string Rail { get; set; }
        public string TL { get; set; }
    }

    class AvailableEquipmentData
    {
        public AvailableEquipmentData(DataManager datamanager)
        {
            TotalPowerUnits = datamanager.Data("TotalPowerUnits");
            InputTotalDrivers = datamanager.Data("InputTotalDrivers");
        }

        public string TotalPowerUnits { get; set; }
        public string InputTotalDrivers { get; set; }
    }

    class PreferredlaneData
    {
        public PreferredlaneData(DataManager datamanager)
        {
            OriginInput = datamanager.Data("OriginInput");
            DestinationInput = datamanager.Data("DestinationInput");
            EquipmentType = datamanager.Data("EquipmentType");
            Capacity = datamanager.Data("Capacity");
            DeleteLane = datamanager.Data("DeleteLane");
        }

        public string OriginInput { get; set; }
        public string DestinationInput { get; set; }
        public string EquipmentType { get; set; }
        public string Capacity { get; set; }
        public string DeleteLane { get; set; }
    }
}
