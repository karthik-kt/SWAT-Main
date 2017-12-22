using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SWAT.Logger;

namespace SWAT.FrameWork.Utilities
{
    public class Common
    {
        internal void ExceptionHandler(string p)
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            MyLogger.Log("Exception Occurred :" + p);
            MyLogger.Log("Function Name  :" + sf.GetMethod().Name);
        }
    }
}
