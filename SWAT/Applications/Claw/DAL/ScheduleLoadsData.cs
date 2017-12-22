namespace SWAT.Applications.Claw.DAL
{
    using SWAT.Data;
    class ScheduleLoadsData
    {
        public ScheduleLoadsData(DataManager datamanager)
        {
            if(datamanager.Data("Load #") !="!IGNORE!")
            {
                LoadID = datamanager.Data("Load #");
            }
            else if(datamanager.Data("LoadID") != "!IGNORE!")
            {
                LoadID = datamanager.Data("LoadID");
            }
            else
            {
                LoadID = "!IGNORE!";
            }
            Pickup_Date = datamanager.Data("Pickup_Date");
            Pickup_Time = datamanager.Data("Pickup_Time").Replace(":00 AM","");
            Delivery_Date = datamanager.Data("Delivery_Date");
            Delivery_Time = datamanager.Data("Delivery_Time").Replace(":00 AM", "");
        }

        public string Delivery_Date { get; private set; }
        public string Delivery_Time { get; private set; }
        public string LoadID { get; private set; }
        public string Pickup_Date { get; private set; }
        public string Pickup_Time { get; private set; }
    }
}
