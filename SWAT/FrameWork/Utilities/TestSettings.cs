using OpenQA.Selenium;
using System;

namespace SWAT.FrameWork.Utilities
{
    using SWAT.Data;

    public static class TestSettings
    {
        public static IWebDriver driver { set; get; }

        public static string baseURL { set; get; }

        public static DataManager testData { set; get; }

        public static string strTestPath = @"\\gxstorage\gxdata\SmokeTests\Claw\TestSuite\Full\TestSuite.xls";

        //public static string strResultPath = @"C:\SmokeTestResults\";

        public static string strResultPath = @"\\gxstorage\gxdata\SmokeTests\Claw\Results\" + TestSettings.TestEnv + "\\" + TestSettings.TestSuite;

        public static string strDriversPath = @"\\gxstorage\gxdata\SmokeTests\Claw\Common\Drivers\";

        public static int intHowManyTry = 2;

        public static DateTime StartTime;

        public static DateTime StopTime;

        public static TimeSpan Duration()
        {
            return StartTime - StopTime;
        }

        public static string Browser { get; set; }

        public static string TestEnv { get; set; }

        public static string TestApp { get; set; }

        public static string TestSuite { get; set; }
    }
}