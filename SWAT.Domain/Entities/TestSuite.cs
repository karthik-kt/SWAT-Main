using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAT.Domain.Entities
{
    public class TestSuite
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string TestCase { get; set; }
        public string AutomationStatus { get; set; }
        public string AnalyzedStatus { get; set; }
        public int PlannedTestCases { get; set; }
        public int ExecutedTestCases { get; set; }
        public string MachineName { get; set; }
        public string Tester { get; set; } 
        public bool isSelected { get; set; }
    }
}
