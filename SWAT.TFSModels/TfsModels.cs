using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAT.TFSModels
{

    public class Plan
    {
        public string id { get; set; }
    }

    public class TestRun
    {
        public string name { get; set; }
        public Plan plan { get; set; }
        public List<string> pointIds { get; set; }
        //public string iteration { get; set; }
        //public Build build { get; set; }
        //public TestRunState state { get; set; }
        //public DateTime dueDate { get; set; }
        //public bool isAutomated { get; set; }
        //public string controller { get; set; }
        //public string errorMessage { get; set; }
        //public string comment { get; set; }
        //public TestSettings testSettings { get; set; }
        //public string testEnvironmentId { get; set; }
        //public DateTime startedDate { get; set; }
        //public DateTime completedDate { get; set; }
        //public Owner owner { get; set; }
        //public string buildDropLocation { get; set; }
        //public string buildPlatform { get; set; }
        //public string buildFlavor { get; set; }
        //public List<int> configIds { get; set; }
        //public string releaseUri { get; set; }
        //public string releaseEnvironmentUri { get; set; }
    }

    public class TestResult
    {
        public int id { get; set; }
    }

    public class TestResultDetail
    {
        public TestResult testResult { get; set; }
        public string state { get; set; }
        public string comment { get; set; }
        public string outcome { get; set; }
    }
}
