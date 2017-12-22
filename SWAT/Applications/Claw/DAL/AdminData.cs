using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAT.Applications.Claw.DAL
{
    using SWAT.Data;
    public class AdminData
    {
        private DataManager _datamanager;
        public AdminData(DataManager datamanager)
        {
            SearchType = datamanager.Data("SearchType");
            SearchBy = datamanager.Data("SearchBy");
            SearchVal = datamanager.Data(SearchBy);
            AdminType = datamanager.Data("AdminType");
            SearchHeaderName = datamanager.Data("SearchHeaderName");
            SearchUserName = datamanager.Data("SearchUserName");
            SearchLastLogin = datamanager.Data("SearchLastLogin");
            _datamanager = datamanager;
        }

        public string SearchLastLogin { get; set; }
        public string SearchUserName { get; set; }
        public string SearchHeaderName { get; set; }
        public string AdminType { get; set; }
        public string SearchType { get; set; }
        public string SearchVal { get; set; }
        public string SearchBy { get; set; }
    }
}
