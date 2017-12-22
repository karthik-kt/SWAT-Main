using System;
using System.Xml;
using System.Text;
using System.Linq;
using System.Data;
using System.Timers;
using System.Collections.Generic;
using System.Threading;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SWAT.Applications.Claw
{

    using SWAT.Data;
    using SWAT.Logger;
    using SWAT.FrameWork.Utilities;
    using Config = SWAT.Configuration.TestStartInfo;


    class CustomerReporting
    {

        public CustomerReporting(Config c, DataManager t)
        {

        }

        //Navigation to Reporting pge  

        public string Navigation()
        {
            return "Not Implemented";

        }


        // Verification of General Information available on page 

        public string generalinfo()
        {
            return "Not Implemented";

        }

        // Veification of Premium Report 

        public string premimureport()
        {
            return "Not Implemented";

        }



    }
}
