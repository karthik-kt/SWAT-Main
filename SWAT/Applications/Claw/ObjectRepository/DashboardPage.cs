using OpenQA.Selenium;
using SWAT.FrameWork.Utilities;
using SWAT.Configuration;
using System.Collections.Generic;

namespace SWAT.Applications.Claw.ObjectRepository
{
    class DashboardPage : Page
    {
        //load dashboard
        private By myLoads = By.CssSelector("#my-loads-gadget-container");
        private By carrierMyLoads = By.CssSelector("#side-panel");
        private By picking_up_today = By.CssSelector(".hook--myloads-today-outbound>strong");
        private By delivering_today = By.CssSelector(".hook--myloads-today-inbound>strong");
        private By picking_up_tomorrow = By.CssSelector(".hook--myloads-tomorrow-outbound>strong");
        private By delivering_tomorrow = By.CssSelector(".hook--myloads-tomorrow-inbound>strong");
        private By picking_up_this_week = By.CssSelector(".hook--myloads-thisweek-outbound>strong");
        private By carrier_picking_up_this_week = By.CssSelector(".hook--myloads-thisweek-outbound>strong");
        private By delivering_this_week = By.CssSelector(".hook--myloads-thisweek-inbound>strong");
        private By carrier_delivering_this_week = By.CssSelector(".hook--myloads-thisweek-inbound>strong");
        private By my_loads_search_results = By.CssSelector(".hook--total");
        private By my_loads_search_results_total = By.CssSelector(".hook--ofTotal");
        private By my_loads_search_headers = By.CssSelector("#search-results-container>tr>td>a");
        private By my_loads_search_pickup_date = By.CssSelector(".hook--pickup");
        private By my_loads_search_delivery_date = By.CssSelector(".hook--delivery");

        public UIItem MyLoads { get { return new UIItem("Dashboard>> My Loads", this.myLoads, _driver); } }
        public UIItem CarrierMyLoads { get { return new UIItem("My Tasks>> My Loads", this.carrierMyLoads, _driver); } }
        public UIItem Picking_Up_Today { get { return new UIItem("Dashboard>> Picking up >> Today", this.picking_up_today, _driver); } }
        public UIItem Delivering_Today { get { return new UIItem("Dashboard>> Delivering >> Today", this.delivering_today, _driver); } }
        public UIItem Picking_Up_Tomorrow { get { return new UIItem("Dashboard>> Picking Up >> Tomorrow", this.picking_up_tomorrow, _driver); } }
        public UIItem Delivering_Tomorrow { get { return new UIItem("Dashboard>> Delivering >> Tomorrow", this.delivering_tomorrow, _driver); } }
        public UIItem Picking_Up_This_Week { get { return new UIItem("Dashboard>> Picking Up >> This week", this.picking_up_this_week, _driver); } }
        public UIItem Delivering_This_Week { get { return new UIItem("Dashboard>> Delivering >> This week", this.delivering_this_week, _driver); } }
        public UIItem My_Loads_Search_Results_Count { get { return new UIItem("My Loads>> Search results >> Count", this.my_loads_search_results, _driver); } }
        public UIItem My_Loads_Search_Results_Count_Total { get { return new UIItem("My Loads>> Search results >> Count", this.my_loads_search_results_total, _driver); } }
        public UIItem My_Loads_Search_Results_Headers { get { return new UIItem("My Loads>> Search results >> Headers", this.my_loads_search_headers, _driver); } }
        public UIItem My_Loads_Search_Pickup_Date { get { return new UIItem("My Loads>> Search results >> Pick up date", this.my_loads_search_pickup_date, _driver); } }
        public UIItem My_Loads_Search_Delivery_Date { get { return new UIItem("My Loads>> Search results >> Delivery date", this.my_loads_search_delivery_date, _driver); } }

        public DashboardPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public DashboardPage(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
        }
    }
}
