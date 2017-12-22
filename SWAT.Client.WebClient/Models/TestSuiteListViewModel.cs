using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SWAT.Domain.Entities;

namespace SWAT.Client.WebClient.Models
{
    public class TestSuiteListViewModel
    {
        public IEnumerable<TestSuite> TestSuites { get; set; }
        public IEnumerable<TestProject> TestProjects { get; set; }
        public PagingInfo PageInfo { get; set; }
        public string CurrentStatus { get; set; }
    }
}