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


namespace SWAT.Applications.Claw.DAL
{
    using SWAT.Data;
    public class AccoutingData
    {
        private DataManager _datamanager;

        public AccoutingData(DataManager datamanager)
        {
            ColToSort = "//button[@value='" + datamanager.Data("SortBy") + "']";
            SearchBy = datamanager.Data("SearchBy");
            InvoiceStatus = datamanager.Data("Invoiced_Status");
            InvoiceStatuses = datamanager.Data("Invoice_Status");
            FromDate = datamanager.Data("From_Date");
            ToDate = datamanager.Data("To_Date");
            CarrierName = datamanager.Data("CarrierName");
            SearchType = datamanager.Data("SearchType");            
            EntityName = datamanager.Data("EntityName");
            SearchVal = EntityName.ToUpper() == "FACTORINGCOMPANY" ?  datamanager.Data("SearchVal") : datamanager.Data(SearchType);
            Type = datamanager.Data("Type").ToUpper().Trim();
            AdvSearchButton = datamanager.Data("Adv_Search_Button");
            ClearAllButton = datamanager.Data("Clear_All_Button");
            SortByColHeader = datamanager.Data("SortByColHeader");
            VerifyColumn = datamanager.Data("VerifyColumn");
            _datamanager = datamanager;
            DateFormat = datamanager.Data("DateFormat");
            Table = datamanager.Data("Table");
        }

        public string Table { get; set; }
        public string DateFormat { get; set; }
        public string ColToSort { set; get; }
        public string SearchBy { get; set; }
        public string InvoiceStatus { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CarrierName { get; set; }
        public string SearchType { get; set; }
        public string SearchVal { get; set; }
        public string EntityName { get; set; }
        public string Type { get; set; }
        public string InvoiceStatuses { get; set; }
        public string LoadId
        {         
            get
            {
                return _datamanager.Data("Load #");
            }
            set
            {
                _datamanager.SetData("Load #", value);
                _datamanager.SetData("LoadID", value);
            }
        }

        public string PayDate
        {
            get
            {
                return _datamanager.Data("PayDate");
            }
            set
            {
                _datamanager.SetData("PayDate", value);
            }
        }

        public string InvoiceId
        {
            get
            {
                return _datamanager.Data("Invoice #");
            }
            set
            {
                _datamanager.SetData("Invoice #", value);
            }
        }
        public string AdvSearchButton { get; set; }
        public string ClearAllButton { get; set; }
        public string SortByColHeader { get; set; }
        public string VerifyColumn { get; set; }
    }
}
