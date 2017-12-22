using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAT.FrameWork.TestManager
{
    public class TestSteps
    {
        public int Index { get; set; }
        public string Action { get; set; } // #UserName.SetValue
        public string DataReference { get; set; } // manivas.murugaiah
        public string Option { get; set; } // ExitOnError
        public string ExpectedResult { get; set; } // Passed
        public string ActualResult { get; set; }
        public List<string> TestStepsStatus;
        public int TCStartIndex { get; set; }
    }
}
