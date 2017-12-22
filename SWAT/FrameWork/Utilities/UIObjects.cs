using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace SWAT.FrameWork.Utilities
{
    using SWAT.Logger;
    using Config = SWAT.Configuration.TestStartInfo;

    public class UIObjects
    {

        public int iGlobalWait = 60;

        internal IWebDriver driver;

        public bool onceFailed = true;

        public Config testConfig;// = new Config();

        public string testSuitePath { get; set; }

        public string testDataPath { get; set; }

        public string testEnv { get; set; }

        public string testBrowser { get; set; }

        public string testApplication { get; set; }

        public string testSuite { get; set; }

        public IWebDriver testDriver { get; set; }

        public string testBaseURL { get; set; }

        public UIObjects(Config c)
        {
            this.testSuitePath = c.SuitePath;
            this.testDataPath = c.DataPath;
            this.testEnv = c.Environment;
            this.testBrowser = c.Browser;
            this.testApplication = c.Application;
            this.testSuite = c.Suite;
            this.testDriver = c.Driver;
            this.testBaseURL = c.BaseURL;
            this.driver = c.Driver;
        }

        public UIObjects()
        {

        }

        internal bool AllPass()
        {
            if (onceFailed) return false;
            else return true;
        }

        #region State of UI Objects

        //Wait for 30 sec for the object to exist.
        internal bool WaitUtilDisplayed(By by)
        {
            for (int second = 1; second <= iGlobalWait; second++)
            {
                try
                {
                    if (driver.FindElement(by).Displayed)
                    {
                        if (second > 1)
                            return true;
                    }
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
            return false;
        }

        internal bool WaitUtilDisplayed(By by, int iWait)
        {
            for (int second = 1; second <= iWait; second++)
            {
                try
                {
                    if (driver.FindElement(by).Displayed)
                    {
                        if (second > 1)
                            return true;
                    }
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
            return false;
        }

        //Validate if the object displayed
        internal bool IsDisplayed(By by)
        {
            try
            {
                //Mylogger.Log("The WebElement is displayed := [" + by.ToString() + "]");
                return driver.FindElement(by).Displayed;
            }
            catch (NoSuchElementException)
            {
                //Mylogger.Log("The WebElement is not displayed := [" + by.ToString() + "]");
                return false;
            }
        }

        //Validate if the object enabled
        internal bool IsEnabled(By by)
        {
            try
            {
                //Mylogger.Log("The WebElement is displayed := [" + by.ToString() + "]");
                return driver.FindElement(by).Enabled;
            }
            catch (NoSuchElementException)
            {
                //Mylogger.Log("The WebElement is not displayed := [" + by.ToString() + "]");
                return false;
            }
        }

        //Validate if the object disabled
        internal bool IsDisabled(By by)
        {
            try
            {
                return !(driver.FindElement(by).Enabled);
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        //Wait for 30 sec for the object to enabled.
        internal bool WaitUtilEnabled(By by)
        {
            for (int second = 1; second <= iGlobalWait; second++)
            {
                try
                {
                    if (driver.FindElement(by).Enabled)
                    {
                        if (second > 1)
                            return true;
                    }
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
            return false;
        }

        internal bool WaitUtilEnabled(By by, int iWait)
        {
            for (int second = 1; second <= iWait; second++)
            {
                try
                {
                    if (driver.FindElement(by).Enabled)
                    {
                        if (second > 1)
                            return true;
                    }
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
            return false;
        }

        //Validate if the element displayed based on the strAction
        internal bool IsDisplayed(By by, string strAction)
        {
            if (strAction == "!DISPLAYED!")
            {
                //IWebDriver driver = WebTable.driver;
                try
                {
                    //Mylogger.Log("The WebElement is displayed := [" + by.ToString() + "]");
                    return driver.FindElement(by).Displayed;
                }
                catch (NoSuchElementException)
                {
                    //Mylogger.Log("The WebElement is not displayed := [" + by.ToString() + "]");
                    return false;
                }
            }
            return true;
        }

        #endregion State of UI Objects

        #region Action on UI Objects

        internal void Edit(By by, string strAction)
        {
            try
            {
                switch (strAction.ToUpper())
                {
                    case "!IGNORE!":
                        //Mylogger.Log("Ignoring WebElement := [" + by.ToString() +"]");
                        break;

                    case "!DEFAULT!":
                        if (!IsDisplayed(by))
                            onceFailed = true;
                        break;

                    case "!CLEAR!":
                        if (IsDisplayed(by))
                        {
                            driver.FindElement(by).Clear();
                            //Mylogger.Log("Editing WebElement := [ " + by.ToString() + "] = " + strAction);
                        }
                        else onceFailed = true;
                        break;

                    default:
                        if (IsDisplayed(by))
                        {
                            driver.FindElement(by).SendKeys(strAction);
                            //Mylogger.Log("Editing WebElement := [ " + by.ToString() + "] = " + strAction);
                        }
                        else onceFailed = true;
                        break;
                }
            }
            catch
            {
                //Mylogger.Log("Exception occured on  := [ " + by.ToString() + "] = " + strAction);
                onceFailed = false;
            }
        }

        //For Create load page - add commodities
        internal void ClearAndEdit(IWebElement iWE, string strAction)
        {
            try
            {
                switch (strAction.ToUpper())
                {
                    case "!IGNORE!":
                        break;

                    case "!DEFAULT!": // Sine we are passing the webelement is should have been already displayed.
                        break;

                    case "!CLEAR!":
                        iWE.Clear();
                        break;

                    default:
                        iWE.Clear();
                        iWE.SendKeys(strAction);
                        break;
                    //InvalidElementStateException
                }
            }
            catch
            {
                onceFailed = false;
            }
        }

        internal void ClearAndEdit(By by, string strAction)
        {
            try
            {
                switch (strAction.ToUpper())
                {
                    case "!IGNORE!":
                        break;

                    case "!DEFAULT!": // Sine we are passing the webelement is should have been already displayed.
                        break;

                    case "!CLEAR!":
                        driver.FindElement(by).Clear();
                        break;

                    default:
                        driver.FindElement(by).Clear();
                        driver.FindElement(by).SendKeys(strAction);
                        break;
                    //InvalidElementStateException
                }
            }
            catch
            {
                onceFailed = false;
            }
        }

        internal void Clear(By by)
        {
            try
            {
                if (IsDisplayed(by))
                {
                    driver.FindElement(by).Clear();
                    //Mylogger.Log("Editing WebElement := [ " + by.ToString() + "] = " + strAction);
                }
                else onceFailed = true;
            }
            catch
            {
                //Mylogger.Log("Exception occured on  := [ " + by.ToString() + "] = " + strAction);
                onceFailed = false;
            }
        }

        //Select values from the dropdown.
        internal void Select(By by, string strAction)
        {
            try
            {
                switch (strAction.ToUpper())
                {
                    case "!IGNORE!":
                        //Mylogger.Log("Ignoring " + by.ToString());
                        break;

                    case "!DEFAULT!":
                        Assert.IsTrue(driver.FindElement(by).Displayed); //Mylogger.Log("Chekcing " + by.ToString());
                        break;

                    default:
                        try { new SelectElement(driver.FindElement(by)).SelectByText(strAction); }
                        catch { }
                        try { new SelectElement(driver.FindElement(by)).SelectByValue(strAction); }
                        catch { }
                        try { new SelectElement(driver.FindElement(by)).SelectByIndex(int.Parse(strAction)); }
                        catch { }
                        //Mylogger.Log("Selecting " + by.ToString());
                        break;
                }
            }
            catch
            {
                //Mylogger.Log("Exception occured on " + by.ToString());
                onceFailed = false;
            }
        }

        internal bool SelectByText(By by, string strAction)
        {
            try
            {
                switch (strAction.ToUpper())
                {
                    case "!IGNORE!":
                        break;

                    case "!DEFAULT!":
                        Assert.IsTrue(driver.FindElement(by).Displayed);
                        break;

                    default:
                        var select = new SelectElement(driver.FindElement(by));
                        select.SelectByText(strAction);
                        //for (int iloop = 0; iloop < select.Options.Count;iloop++ )
                        //{
                        //    driver.FindElement(by).SendKeys(Keys.ArrowDown);
                        //    if(select.SelectedOption.Text == strAction)
                        //    {
                        //        break;
                        //    }
                        //}
                        break;
                }
                return true;
            }
            catch
            {
                MyLogger.Log("Unable to select the value from" + strAction);
                return false;
            }
        }

        //For Create load page - add commodities
        internal void SelectByText(IWebElement iWE, string strAction)
        {
            try
            {
                switch (strAction.ToUpper())
                {
                    case "!IGNORE!":
                        break;

                    case "!DEFAULT!":
                        break;

                    default:
                        new SelectElement(iWE).SelectByText(strAction);
                        break;
                }
            }
            catch
            {
                onceFailed = false;
            }
        }

        internal bool SelectByValue(By by, string strAction)
        {
            try
            {
                switch (strAction.ToUpper())
                {
                    case "!IGNORE!":
                        break;

                    case "!DEFAULT!":
                        Assert.IsTrue(driver.FindElement(by).Displayed);
                        break;

                    default:
                        var select = new SelectElement(driver.FindElement(by));
                        select.SelectByValue(strAction);
                        break;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool FindAndClickUsingJS(By by)
        {
            IWebElement element = driver.FindElement(by);

            if (null == element || !element.Enabled)
            {
                return false;
            }

            ScrollToElement(element);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
            return true;
        }

        public void ScrollToElement(IWebElement element)
        {
            ILocatable locatableElement = (ILocatable)element;
            System.Drawing.Point p = locatableElement.Coordinates.LocationInViewport;
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            js.ExecuteScript("window.scrollTo(" + p.X + "," + p.Y + ");"); 
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,-100);");
        }

        public void _ScrollToElement(IWebElement element)
        {

            ILocatable locatableElement = (ILocatable)element;
            System.Drawing.Point p = locatableElement.Coordinates.LocationInViewport;
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            js.ExecuteScript("window.scrollTo(" + p.X + "," + p.Y + ");");
        }

        internal bool Click(By by)
        {
            try
            {
                if (WaitUtilDisplayed(by))
                    if (WaitUtilEnabled(by))
                        driver.FindElement(by).Click();
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal bool Check(By by, string stAction)
        {
            try
            {
                switch (stAction.ToUpper()) // DoTo : Need to convert to uppper case, trim
                {
                    case "!IGNORE!": // DoTo : Logically this wont work.Since selenium find element would have thrown error.
                        break;

                    case "!DEFAULT!":
                        Assert.IsTrue(driver.FindElement(by).Displayed);
                        break;

                    case "!CHECKED!":
                        Assert.IsTrue(driver.FindElement(by).Displayed);
                        Assert.IsTrue(driver.FindElement(by).Selected);
                        break;

                    case "!UNCHECKED!":
                        Assert.IsTrue(driver.FindElement(by).Displayed);
                        Assert.IsFalse(driver.FindElement(by).Selected);
                        break;

                    case "!UNCHECK!":
                        Assert.IsTrue(driver.FindElement(by).Displayed);
                        if (driver.FindElement(by).Selected)
                        {
                            driver.FindElement(by).Click();
                        }
                        Assert.IsFalse(driver.FindElement(by).Selected);
                        break;

                    case "!CHECK!":
                        Assert.IsTrue(driver.FindElement(by).Displayed);
                        if (!driver.FindElement(by).Selected)
                        {
                            driver.FindElement(by).Click();
                        }
                        Assert.IsTrue(driver.FindElement(by).Selected);
                        break;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal void SetDate(By byFromDate, string p)
        {
            string From_Date = p.Replace('/', '\0');
            IWebElement fromDate = driver.FindElement(byFromDate);
            fromDate.SendKeys(Keys.Backspace);
            fromDate.SendKeys(Keys.Backspace);
            fromDate.SendKeys(Keys.Backspace);
            fromDate.SendKeys(Keys.Backspace);
            fromDate.SendKeys(Keys.Backspace);
            fromDate.SendKeys(Keys.Backspace);
            fromDate.SendKeys(Keys.Backspace);
            fromDate.SendKeys(Keys.Backspace);
            if (driver.ToString() == "OpenQA.Selenium.IE.InternetExplorerDriver")
            {
                string[] arDate = From_Date.Split('\0');
                fromDate.SendKeys(arDate[0]);
                fromDate.SendKeys(arDate[1]);
                fromDate.SendKeys(arDate[2]);
            }
            else
            {
                fromDate.SendKeys(From_Date);
            }
            fromDate.SendKeys(Keys.Tab);
        }

        //Create new page change
        internal void setDate(IWebElement fromDate, string p)
        {
            string From_Date = p.Replace('/', '\0');
            //IWebElement fromDate = driver.FindElement(byFromDate);
            fromDate.SendKeys(Keys.Backspace);
            fromDate.SendKeys(Keys.Backspace);
            fromDate.SendKeys(Keys.Backspace);
            fromDate.SendKeys(Keys.Backspace);
            fromDate.SendKeys(Keys.Backspace);
            fromDate.SendKeys(Keys.Backspace);
            fromDate.SendKeys(Keys.Backspace);
            fromDate.SendKeys(Keys.Backspace);
            if (driver.ToString() == "OpenQA.Selenium.IE.InternetExplorerDriver")
            {
                string[] arDate = From_Date.Split('\0');
                fromDate.SendKeys(arDate[0]);
                fromDate.SendKeys(arDate[1]);
                fromDate.SendKeys(arDate[2]);
            }
            else
            {
                fromDate.SendKeys(From_Date);
            }
            fromDate.SendKeys(Keys.Tab);
        }

        internal void Navigate(string strPage)
        {
            try
            {
                //string strbaseURL = TestSettings.baseURL;
                if(testBaseURL != null)
                driver.Navigate().GoToUrl(testBaseURL + strPage);
            }
            catch
            {
            }
        }

        #endregion Action on UI Objects

        internal string GetText(By by)
        {
            return driver.FindElement(by).Text;
        }

        public bool DateRangeCheckInList(string strFromDate, string strToDate, List<DateTime> dates)
        {
            //return colValues;
            DateTime fromDate = DateTime.Parse(strFromDate);
            DateTime toDate = DateTime.Parse(strToDate);
            bool InRange = true;
            foreach (DateTime date in dates)
            {
                if (fromDate < date || date < toDate)
                {
                    InRange = false;
                    break;
                }
            }
            return InRange;
        }

        internal void ExceptionHandler(string p, string GetCurrentMethod)
        {
            MyLogger.Log("Exception Occurred :" + p);
            MyLogger.Log("Function Name  :" + GetCurrentMethod);
        }

        internal string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            return sf.GetMethod().Name;
        }

        internal bool WaitUntilLoading()
        {
            By byLoading = By.ClassName("busy");
            string message = null;       
            try
            {
                if (driver.FindElement(byLoading).Displayed)
                {
                    do
                    {
                        message = driver.FindElement(byLoading).Text.ToUpper();
                        var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(30.00));
                        wait.Until(driver1 => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                        if (message == "AN ERROR HAS OCCURRED")
                        {
                            MyLogger.Log("<<AN ERROR HAS OCCURRED>> message displayed.");
                            return false;
                        }
                        Thread.Sleep(10);
                    }
                    while (message == "LOADING...");
                }
                return true;
            }
            catch
            {
                return true;
            }
        }

        internal List<IWebElement> GetElements(By by)
        {
            return driver.FindElements(by).ToList<IWebElement>();
        }
        
        internal void Wait(int time){
            Thread.Sleep(time);
        }
    }
}