using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAT.Applications.Claw.DAL
{
    using SWAT.Data;

    class MyTasksData
    {
        private DataManager _datamanager;
        public MyTasksData(DataManager datamanager)
        {
            DriverName = datamanager.Data("DriverName");
            DriverPhone = datamanager.Data("DriverPhone");
            EmptyLocation = datamanager.Data("EmptyLocation");
            EmptyDate = datamanager.Data("EmptyDate");
            EmptyTime = datamanager.Data("EmptyTime");
            ExpectedConfirmLoadCountStatus = datamanager.Data("ExpectedConfirmLoadCountStatus");
            NoOfmissingDocuments = datamanager.Data("NoOfmissingDocuments");
            ReducedAmount = datamanager.Data("ReducedAmount");
            OpenLoadDetails = datamanager.Data("OpenLoadDetails");
            ComparableItem = datamanager.Data("ComparableItem");
            CallBack = datamanager.Data("CallBack");
            VerifyFor = datamanager.Data("VerifyFor");  
            _datamanager = datamanager;

        }

        public string ConfirmLoadsCount
        {
            get
            {
                return _datamanager.Data("ConfirmLoadsCount");
            }
            set
            {
                _datamanager.SetData("ConfirmLoadsCount", value);
            }
        }

        public string AccountingLoadsCount
        {
            get
            {
                return _datamanager.Data("AccountingLoadsCount");
            }
            set
            {
                _datamanager.SetData("AccountingLoadsCount", value);
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

        public string TotalMissingDocs
        {
            get
            {
                return _datamanager.Data("TotalMissingDocs");
            }
            set
            {
                _datamanager.SetData("TotalMissingDocs", value);
            }
        }
        public string VerifyFor { get; set; }
        public string CallBack { get; set; }
        public string ComparableItem { get; set; }
        public string OpenLoadDetails { get; set; }
        public string ReducedAmount { get; set; }
        public string NoOfmissingDocuments { get; set; }
        public string DriverName { get; set; }
        public string DriverPhone { get; set; }
        public string EmptyDate { get; set; }
        public string EmptyLocation { get; set; }
        public string EmptyTime { get; set; }
        public string ExpectedConfirmLoadCountStatus { get; set; }
    }
}
