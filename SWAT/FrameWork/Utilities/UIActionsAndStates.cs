using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Internal;
using System;
using System.Threading;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace SWAT.FrameWork.Utilities
{
    using SWAT.Logger;
    using Config = SWAT.Configuration.TestStartInfo;

    public class UIActionsAndStates : UIObjects
    {
        //internal IWebDriver driver;       

        //public bool onceFailed = true;

        //public Config testConfig; 

        public UIActionsAndStates()
        {
        }

        //internal bool AllPass()
        //{
        //    if (onceFailed) return false;
        //    else return true;
        //}

        #region State of UI Objects

        internal bool IsDisplayed(UIItem UI)
        {
            try
            {
                return driver.FindElement(UI.By).Displayed;
            }
            catch (NoSuchElementException)
            {
                MyLogger.Log("The webElement is not displayed := [" + UI.Name + "]");
                return false;
            }
        }

        //Validate if the object enabled
        internal bool IsEnabled(UIItem UI)
        {
            try
            {
                return driver.FindElement(UI.By).Enabled;
            }
            catch (NoSuchElementException)
            {
                MyLogger.Log("The WebElement is not displayed := [" + UI.Name + "]");
                return false;
            }
        }

        //Wait for 30 sec for the object to exist.
        internal bool WaitUtilDisplayed(UIItem UI)
        {
            int second = 1;
            for (; second <= this.iGlobalWait; second++)
            {
                try
                {
                    if (driver.FindElement(UI.By).Displayed)
                    {
                        if (second > 1)
                            MyLogger.Log("[ " + UI.Name + " ] is displayed, after [" + second + "] secs");
                        return true;
                    }
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
            MyLogger.Log("Waited for [ " + second + " ] secs, but [ " + UI.Name + " ] is not displayed.");
            return false;
        }

        internal bool WaitUtilDisappear(UIItem UI)
        {
            int second = 1;
            for (; second <= this.iGlobalWait; second++)
            {
                try
                {
                    if (!driver.FindElement(UI.By).Displayed)
                    {
                        if (second > 1)
                            MyLogger.Log("[ " + UI.Name + " ] is not displayed, after [" + second + "] secs");
                        return true;
                    }
                }
                catch (NoSuchElementException)
                {
                    if (second > 1)
                        MyLogger.Log("[ " + UI.Name + " ] is not displayed, after [" + second + "] secs");
                    return true;
                }
                Thread.Sleep(1000);
            }
            MyLogger.Log("Waited for [ " + second + " ] secs, but [ " + UI.Name + " ] is still displayed.");
            return false;
        }

        internal bool WaitUtilDisplayed(UIItem UI, int iWait)
        {
            int iTemp = this.iGlobalWait;
            this.iGlobalWait = iWait;
            bool bResult = WaitUtilDisplayed(UI);
            this.iGlobalWait = iTemp;
            return bResult;
        }

        public bool FindAndClickUsingJS(UIItem ui)
        {            
            IWebElement element = driver.FindElement(ui.By);

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
            _ScrollToElement(element);
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,-100);");
        }

        public void _ScrollToElement(IWebElement element)
        {

            ILocatable locatableElement = (ILocatable)element;
            System.Drawing.Point p = locatableElement.Coordinates.LocationInViewport;
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            js.ExecuteScript("window.scrollTo(" + p.X + "," + p.Y + ");");
        }

        internal bool WaitUtilBusy()
        {
            By byGlobalMessage = By.CssSelector("#global-notification__message");
            UIItem uiGlobalMessage = new UIItem("Loading...", byGlobalMessage);
            while (IsDisplayed(uiGlobalMessage))
            {
                if (GetText(uiGlobalMessage) == "Loading...")
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        internal string GetText(UIItem ui)
        {
            if(IsDisplayed(ui))
            {
                return driver.FindElement(ui.By).Text.ToString();
            }
            return null;
        }

        internal bool WaitUtilEnabled(By by)
        {
            for (int second = 1; second <= 30; second++)
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

        internal int GetOneElementIndex(UIItem ui,string searchString)
        {
            try
            {
                List<int> index = GetElementIndex(ui, searchString);

                if (index.Count == 1)
                {
                    MyLogger.Log("<<" + ui.Name + ">> element found on this page");
                    return index[0];
                }
                if (index.Count == 0)
                {
                    //return element not found.
                    MyLogger.Log("<<"+ui.Name+">> element not found on this page");
                    return -1;
                }
                if (index.Count > 1)
                {
                    //more than one elements are found.
                    MyLogger.Log("<<" + ui.Name + ">> more than one elements are found.");
                    return -1;
                }
                return index[0];
            }
            catch(Exception ex)
            {
                MyLogger.Log(ex.Message);
                return -1;
            }
        }

        internal List<int> GetElementIndex(UIItem ui,string searchstring)
        {
            try
            {
                List<int> index = null;
                IList<IWebElement> elements = driver.FindElements(ui.By);
                for (int iloop = 0; iloop < elements.Count;iloop++)
                {
                    if (Regex.IsMatch(elements[iloop].Text, searchstring))
                    {
                        index.Add(iloop);
                        MyLogger.Log("<<"+ui.Name+">> is displayed on <<"+iloop+">> the data set");
                    }
                }
                if (index.Count == 0)
                {
                    MyLogger.Log("Given string is not found on the rows.");
                }
                return index;
            }
            catch(Exception ex)
            {
                MyLogger.Log(ex.Message);
                return null;
            }
        }

        internal bool IsDisplayedByIndex(UIItem uIItem, int LoadIDIndex)
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal bool ClickByIndex(UIItem uiitem, int index)
        {
            try
            {
                IList<IWebElement> elements = driver.FindElements(uiitem.By);
                elements[index].Click();
                return true;
            }
            catch(Exception ex)
            {
                MyLogger.Log(ex.Message);
                return false;
            }
        }

        private IWebElement FindElementByIndex(UIItem ui,int index)
        {
            try
            {
                IList<IWebElement> elements = FindElements(ui);
                if(elements != null)
                {
                    return elements[index];
                }
                return null;
            }
            catch (Exception ex)
            {
                MyLogger.Log(ex.Message);
                return null;
            }
        }


        internal IList<IWebElement> FindElements(UIItem ui)
        {
            try
            {
                IList<IWebElement> elements = driver.FindElements(ui.By);
                return elements;
            }
            catch(EntryPointNotFoundException)
            {
                MyLogger.Log("Elements are not found :" + ui.Name);
                return null;
            }
            catch(Exception ex)
            {
                MyLogger.Log(ex.Message);
                return null;
            }
        }


        #endregion State of UI Objects

        #region Action on UI Objects

        internal void Edit(UIItem UI, string strAction)
        {
            try
            {
                switch (strAction.ToUpper())
                {
                    case "!IGNORE!":
                        MyLogger.Log("Ignoring WebElement <" + UI.Name + ">");
                        break;

                    case "!DEFAULT!":
                        if (!IsDisplayed(UI))
                            onceFailed = true;
                        break;

                    case "!CLEAR!":
                        if (IsDisplayed(UI))
                        {
                            driver.FindElement(UI.By).Clear();
                            MyLogger.Log("Clearing WebElement <" + UI.Name + " >");
                        }
                        else onceFailed = true;
                        break;

                    default:
                        if (IsDisplayed(UI))
                        {
                            driver.FindElement(UI.By).SendKeys(strAction);
                            MyLogger.Log(UI.Name + " := [ " + strAction + " ]");
                        }
                        else onceFailed = true;
                        break;
                }
            }
            catch
            {
                MyLogger.Log("Exception occured on <" + UI.Name + ">");
                onceFailed = false;
            }
        }

        internal void ClearAndEdit(UIItem UI, string strAction)
        {
            try
            {
                switch (strAction.ToUpper())
                {
                    case "!IGNORE!":
                        MyLogger.Log("Ignoring WebElement < " + UI.Name + ">");
                        break;

                    case "!DEFAULT!":
                        if (!IsDisplayed(UI))
                            onceFailed = true;
                        break;

                    case "!CLEAR!":
                        if (IsDisplayed(UI))
                        {
                            driver.FindElement(UI.By).Clear();
                            MyLogger.Log("Clearing WebElement <" + UI.Name + ">");
                        }
                        else onceFailed = true;
                        break;

                    default:
                        if (IsDisplayed(UI))
                        {
                            driver.FindElement(UI.By).Clear();
                            driver.FindElement(UI.By).SendKeys(strAction);
                            MyLogger.Log(UI.Name + " := [ " + strAction + " ]");
                        }
                        else onceFailed = true;
                        break;
                }
            }
            catch
            {
                MyLogger.Log("Exception occured on  <" + UI.Name + ">");
                onceFailed = false;
            }
        }

        internal void SelectByText(UIItem UI, string strAction)
        {
            try
            {
                switch (strAction.ToUpper())
                {
                    case "!IGNORE!":
                        MyLogger.Log("Ignoring WebElement < " + UI.Name + ">");
                        break;

                    case "!DEFAULT!":
                        Assert.IsTrue(driver.FindElement(UI.By).Displayed);
                        MyLogger.Log("Chekcing < " + UI.Name + ">");
                        break;

                    default:
                        new SelectElement(driver.FindElement(UI.By)).SelectByText(strAction);
                        MyLogger.Log(UI.Name + " := [ " + strAction + " ]");
                        break;
                }
            }
            catch
            {
                MyLogger.Log("Exception occured on  <" + UI.Name + ">");
                onceFailed = false;
            }
        }

        internal void SelectByValue(UIItem UI, string strAction)
        {
            try
            {
                switch (strAction.ToUpper())
                {
                    case "!IGNORE!":
                        MyLogger.Log("Ignoring WebElement < " + UI.Name + ">");
                        break;

                    case "!DEFAULT!":
                        Assert.IsTrue(driver.FindElement(UI.By).Displayed);
                        MyLogger.Log("Chekcing < " + UI.Name + ">");
                        break;

                    default:
                        new SelectElement(driver.FindElement(UI.By)).SelectByValue(strAction);
                        MyLogger.Log(UI.Name + " := [ " + strAction + " ]");
                        break;
                }
            }
            catch
            {
                MyLogger.Log("Exception occured on  <" + UI.Name + ">");
                onceFailed = false;
            }
        }

        internal void Clear(UIItem UI)
        {
            try
            {
                if (IsDisplayed(UI))
                {
                    driver.FindElement(UI.By).Clear();
                }
                else onceFailed = true;
            }
            catch
            {
                MyLogger.Log("Failed to clear webelement <" + UI.Name + ">");
                onceFailed = false;
            }
        }

        internal bool Click(UIItem UI)
        {
            try
            {
                Assert.IsTrue(WaitUtilDisplayed(UI));
                Assert.IsTrue(WaitUtilEnabled(UI.By));
                driver.FindElement(UI.By).Click();
                MyLogger.Log("Clicked on webelement <" + UI.Name + ">");
                return true;
            }
            catch
            {
                MyLogger.Log("Unable to click on webelement <" + UI.Name + ">");
                onceFailed = false;
                return false;
            }
        }

        internal void Navigate(string strPage)
        {
            string baseurl = driver.Url;
            try
            {
                driver.Navigate().GoToUrl(baseurl + strPage);
            }
            catch
            {
            }
        }

        #endregion Action on UI Objects
    }
}