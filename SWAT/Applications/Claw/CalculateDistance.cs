using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace SWAT.Applications.Claw
{
    using SWAT.Data;
    using SWAT.FrameWork.Utilities;
    using Config = SWAT.Configuration.TestStartInfo;

    public class CalculateDistance : UIObjects
    {
        private By byCalDstPgTle = By.CssSelector("#app-title");
        private By byOrigin = By.CssSelector("#distance-calc-orig-input");
        private By byDestination = By.CssSelector("#distance-calc-dest-input");
        private By byDstSrv = By.CssSelector("#version-number");
        private By byUnitsKM = By.CssSelector("#unit-km");
        private By byUnitsMI = By.CssSelector("#unit-mi");
        private By byRouting = By.CssSelector("#routing-type");
        private By byOpnBorders = By.CssSelector("#borders");
        private By byDst = By.CssSelector("#distance-calc-result");

        private string Origin;
        private string Destination;
        private string DstSrv;
        private string Units;
        private string Routing;
        private string OpnBorders;
        private string Dst;

        public CalculateDistance(Config c, DataManager t)
            : base(c)
        {
            testConfig = c;
            driver = testConfig.Driver;
            Origin = t.Data("Origin");
            Destination = t.Data("Destination");
            DstSrv = t.Data("DstSrv");
            Units = t.Data("Units");
            Routing = t.Data("Routing");
            OpnBorders = t.Data("OpnBorders");
            Dst = t.Data("Dst");
        }

        internal string CalucateDistance()
        {
            try
            {
                Assert.IsTrue(GoToMyPage());
                Assert.IsTrue(WaitUtilEnabled(byOrigin));
                Edit(byOrigin, Origin);
                Edit(byDestination, Destination);
                SelectByText(byDstSrv, DstSrv);
                if (Units.ToUpper() == "KM")
                {
                    Click(byUnitsKM);
                }
                SelectByText(byRouting, Routing);
                if (OpnBorders.ToUpper() == "YES")
                {
                    Click(byOpnBorders);
                }
                Assert.IsTrue(WaitUtilDisplayed(byDst));

                //calculated distance and it's unit
                string calcDistance = GetText(byDst).ToUpper();
                string unit = calcDistance.Split(' ')[1];
                if (Dst.ToUpper() != calcDistance && Dst.ToUpper() != Constants.Ignore)
                {
                    return "CalculationFailed";
                }

                //Verify calculated distance has selected unit(miles/km)
                bool isUnitCorrect = Units.ToUpper() == "KM" ? unit == "KM" : unit == "MI";
                if (!isUnitCorrect)
                {
                    return "CalculationFailed";
                }
                return "CalucationSuccess";
            }
            catch
            {
                return "CalculationFailed";
            }
        }

        private bool GoToMyPage()
        {
            try
            {
                Navigate("#calculatedistance");
                Assert.IsTrue(WaitUtilDisplayed(byCalDstPgTle));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}