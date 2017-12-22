
namespace SWAT.Applications.Claw.DAL
{
    using SWAT.Data;

    public class TenderData
    {
        public TenderData(DataManager DataManager)
        {
            LoadID = DataManager.Data("Load #");
            IsRushOrder = DataManager.Data("RushOrder");
            Expires = DataManager.Data("Expires");
            IsPrimary = DataManager.Data("IsPrimary");
            Accept = DataManager.Data("Accept");
            AcceptDate = DataManager.Data("AcceptDate");
            Reject = DataManager.Data("Reject");
            RejectReason = DataManager.Data("RejectReason");
            RejectReasonNotes = DataManager.Data("RejectReasonNotes");
            LoadNotes = DataManager.Data("Notes");
            SpotOfferNotes = DataManager.Data("SpotOfferNotes");
            SpotOfferRate = DataManager.Data("SpotOfferRate");
            CarrierName = DataManager.Data("CarrierName");
            LoadType = DataManager.Data("LoadType");
            VerifyCarrier = DataManager.Data("VerifyCarrier");
        }

        public string LoadType { get; set; }
        public string CarrierName { get; set; }
        public string SpotOfferRate { get; set; }
        public string SpotOfferNotes { get; set; }
        public string LoadID { get; set; }
        public string IsRushOrder { get; set; }
        public string Expires { get; set; }
        public string IsPrimary { get; set; }
        public string Accept { get; set; }
        public string AcceptDate { get; set; }
        public string Reject { get; set; }
        public string RejectReason { get; set; }
        public string RejectReasonNotes { get; set; }
        public string LoadNotes { get; set; }
        public string VerifyCarrier { get; set; }
    }
}
