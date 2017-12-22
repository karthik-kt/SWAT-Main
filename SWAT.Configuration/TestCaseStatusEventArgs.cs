using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAT.Configuration
{
    public class TestCaseStatusEventArgs:EventArgs
    {
        public TestCaseStatusEventArgs(int testCaseId, string testCaseStatus,string testResultPath)
        {
            TestCaseStatus = testCaseStatus;
            TestCaseId = testCaseId;
            TestResultPath = testResultPath;
        }

        public string TestResultPath { get; set; }
        public string TestCaseStatus { get; set; }
        public int TestCaseId { get; set; }
    }
}
