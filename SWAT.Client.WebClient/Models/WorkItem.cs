using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWAT.Client.WebClient.Models
{
    public class IWorkItem
    {
        public string id { get; set; }
        public string name { get; set; }
        public Uri url { get; set; }
        public string state { get; set; }
        public int revision { get; set; }
    }

    public class TFSProject : IWorkItem
    {

    }

    public class ITFSTestPlan : IWorkItem
    {
        public TFSProject project { get; set; }
        public IWorkItem area { get; set; }
        public string iteration { get; set; }
        public string owner { get; set; }
        public IWorkItem rootSuite { get; set; }
        public Uri clientUrl { get; set; }
    }

    public class TFSTestPlan : ITFSTestPlan
    {

    }

    public class TFSProjectResults
    {
        public int count { get; set; }        
        public List<TFSProject> value { get; set; }
    }


}