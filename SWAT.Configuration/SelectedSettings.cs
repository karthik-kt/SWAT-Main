using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAT.Configuration
{
        public struct SelectedSettings
        {
            public string Application;
            public string Environment;
            public string Browser;
            public string Suite;
            public bool isBulkExecution;
            public IEnumerable<string> BulkSuite; 
            public bool PublishResult { get; set; }
        }
}
