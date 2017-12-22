// /////////////////////////////////////////////////////////////////////////////////////
//                           Copyright (c) 2015 - 2015
//                            Coyote Logistics L.L.C.
//                          All Rights Reserved Worldwide
// 
// WARNING:  This program (or document) is unpublished, proprietary
// property of Coyote Logistics L.L.C. and is to be maintained in strict confidence.
// Unauthorized reproduction, distribution or disclosure of this program
// (or document), or any program (or document) derived from it is
// prohibited by State and Federal law, and by local law outside of the U.S.
// /////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenQA.Selenium;
using SWAT.Logger;
using SWAT.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWAT.FrameWork.TestManager;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.Events;
namespace SWAT.FrameWork.Utilities
{
    public class UIItem
    {

        private int globalwait = 60;

        public string Name { set; get; }

        public By By { set; get; }

        public UIItem(string Name, By by)
        {
            this.By = by;
            this.Name = Name;
        }

        public UIItem(string Name, By by, IWebDriver driver)
        {
            this.By = by;
            this.Name = Name;
            this.driver = driver;
        }

        public UIItem(string Name, By by, IWebElement driver)
        {
            this.By = by;
            this.Name = Name;
            this.driver = driver;
        }

        public UIItem(string Name,IWebElement driver)
        {
            this.Name = Name;
            this.driver = driver;
            _CurrentWebElement = driver;
        }

        private EventHandler<WebElementEventArgs> test1()
        {
            throw new NotImplementedException();
        }

        internal bool Execute(string actiondata)
        {
            string[] karma = actiondata.Split('.');
            try
            {
                switch (karma[0].ToLower())
                {
                    case "click":
                        MyLogger.Log("Clicked on webelement [ " + this.Name + " ]");
                        CurrentWebElement.Click();
                        break;
                    case "edit":
                        MyLogger.Log("Editing webelement [ " + this.Name + " ] := [ "+karma[1]+" ]");
                        CurrentWebElement.SendKeys(karma[1]);
                        break;
                    case "waituntildisplayed":
                        MyLogger.Log("Wait until displayed webelement [ " + this.Name + " ]");
                        return WaitUntilDisplayed();
                }
                return true;
            }
            catch
            {
                MyLogger.Log("Exception occurred weblement [ " + this.Name + " ]");
                return false;
            }
        }

        public UIItem()
        {

        }

        private IWebElement _CurrentWebElement;

        private IWebElement CurrentWebElement
        {
            get
            {
                if (this.By != null)
                {
                    return driver.FindElement(this.By);
                }
                else
                {
                    return _CurrentWebElement;
                }
            }
        }

        // private IWebDriver driver;
        private ISearchContext driver;

        #region Action on UI Objects

        internal bool Edit(string strAction)
        {
            try
            {
                switch (strAction.ToUpper())
                {
                    case "!IGNORE!":
                        MyLogger.Log("Ignoring WebElement [ " + this.Name + " ]");
                        break;
                    case "!DEFAULT!":
                        Assert.IsTrue(IsDisplayed());
                        break;

                    case "!CLEAR!":
                        if (IsDisplayed())
                        {
                            CurrentWebElement.Clear();
                            MyLogger.Log("Clearing WebElement [ " + this.Name + " ]");
                        }
                        break;

                    default:
                        if (IsDisplayed())
                        {
                            CurrentWebElement.SendKeys(strAction);
                            MyLogger.Log(this.Name + " := [ " + strAction + " ]");
                        }
                        break;
                }
                return true;
            }
            catch
            {
                MyLogger.Log("Exception occurred on [ " + this.Name + " ]");
                return false;
            }
        }

        internal bool ClearAndEdit(string strAction)
        {
            try
            {

                switch (strAction.ToUpper())
                {
                    case "!IGNORE!":
                        MyLogger.Log("Ignoring WebElement [ " + this.Name + " ]");
                        return true;

                    case "!DEFAULT!":
                        Assert.IsTrue(IsDisplayed());
                        return true;

                    case "!CLEAR!":
                        if (IsDisplayed())
                        {
                            CurrentWebElement.Clear();
                            MyLogger.Log("Clearing WebElement [ " + this.Name + " ]");
                        }
                        return true;

                    default:
                        if (IsDisplayed())
                        {
                            CurrentWebElement.Clear();
                            CurrentWebElement.SendKeys(strAction);
                            MyLogger.Log(this.Name + " := [ " + strAction + " ]");
                            return true;
                        }
                        return false;
                }
            }
            catch
            {
                MyLogger.Log("Exception occurred on  [ " + this.Name + " ]");
                return false;
            }
        }

        internal bool ClearAndEditByIndex(string strAction, int index)
        {
            try
            {
                IList<IWebElement> elements = driver.FindElements(this.By);
                switch (strAction.ToUpper())
                {
                    case "!IGNORE!":
                        MyLogger.Log("Ignoring WebElement [ " + this.Name + " ]");
                        return true;

                    case "!DEFAULT!":
                        Assert.IsTrue(elements[index - 1].Displayed);
                        return true;

                    case "!CLEAR!":
                        if (elements[index - 1].Displayed)
                        {
                            elements[index - 1].Clear();
                            MyLogger.Log("Clearing WebElement [ " + this.Name + " ]");
                        }
                        return true;

                    default:
                        if (elements[index - 1].Displayed)
                        {
                            elements[index - 1].Clear();
                            elements[index - 1].SendKeys(strAction);
                            MyLogger.Log(this.Name + " := [ " + strAction + " ]");
                            return true;
                        }
                        return false;
                }
            }
            catch
            {
                MyLogger.Log("Exception occurred on  [ " + this.Name + " ]");
                return false;
            }
        }

        internal bool ClearAndEditBox(string strAction)
        {
            try
            {

                switch (strAction.ToUpper())
                {
                    case "!IGNORE!":
                        MyLogger.Log("Ignoring WebElement [ " + this.Name + " ]");
                        return true;

                    case "!DEFAULT!":
                        Assert.IsTrue(IsDisplayed());
                        return true;

                    case "!CLEAR!":
                        if (IsDisplayed())
                        {
                            CurrentWebElement.Clear();
                            MyLogger.Log("Clearing WebElement [ " + this.Name + " ]");
                        }
                        return true;

                    default:
                        if (IsDisplayed())
                        {
                            CurrentWebElement.Clear();
                            if (CurrentWebElement.GetAttribute("value").ToString() != "")
                            {
                                CurrentWebElement.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                                CurrentWebElement.SendKeys(OpenQA.Selenium.Keys.Delete);
                            }
                            CurrentWebElement.SendKeys(strAction);
                            MyLogger.Log(this.Name + " := [ " + strAction + " ]");
                            return true;
                        }
                        return false;
                }
            }
            catch
            {
                MyLogger.Log("Exception occurred on  [ " + this.Name + " ]");
                return false;
            }
        }

        internal bool IsEditable()
        {
            try
            {
                string testString = "TestXYZ";
                IWebElement fieldName = CurrentWebElement;
                if (IsDisplayed())
                {
                    string previousFieldValue = fieldName.Text.ToString();
                    fieldName.Clear();
                    fieldName.SendKeys(testString);
                    string fieldValue = fieldName.Text.ToString();
                    if (fieldValue == testString)
                    {
                        fieldName.SendKeys(previousFieldValue);
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        internal bool HasClass(string strAction)
        {
            try
            {

                switch (strAction.ToUpper())
                {
                    case "!IGNORE!":
                        MyLogger.Log("Ignoring WebElement [ " + this.Name + " ]");
                        return true;

                    default:
                        if (IsDisplayed())
                        {

                            if (driver.FindElement(this.By).GetAttribute("class").Contains(strAction))
                            {
                                MyLogger.Log("Found class " + strAction + " in [ " + this.Name + " ]");
                                return true;
                            }
                            else
                            {
                                MyLogger.Log("Not found class " + strAction + " in [ " + this.Name + " ]");
                                return false;
                            }
                        }
                        return false;
                }
            }
            catch
            {
                MyLogger.Log("Exception occurred on  [ " + this.Name + " ]");
                return false;
            }
        }

        internal bool SelectByText(string strAction)
        {
            try
            {
                switch (strAction.ToUpper())
                {
                    case "!IGNORE!":
                        MyLogger.Log("Ignoring WebElement [ " + this.Name + " ]");
                        break;

                    case "!DEFAULT!":
                        Assert.IsTrue(CurrentWebElement.Displayed);
                        MyLogger.Log("Checking [ " + this.Name + " ]");
                        break;

                    default:
                        new SelectElement(CurrentWebElement).SelectByText(strAction);
                        MyLogger.Log(this.Name + " := [ " + strAction + " ]");
                        break;
                }
                return true;
            }
            catch
            {
                MyLogger.Log("Exception occurred on  [ " + this.Name + " ]");
                return false;
            }
        }

        internal bool SelectByValue(string strAction)
        {
            try
            {
                switch (strAction.ToUpper())
                {
                    case "!IGNORE!":
                        MyLogger.Log("Ignoring WebElement [ " + this.Name + " ]");
                        break;

                    case "!DEFAULT!":
                        Assert.IsTrue(CurrentWebElement.Displayed);
                        MyLogger.Log("Checking [ " + this.Name + " ]");
                        break;

                    default:
                        new SelectElement(CurrentWebElement).SelectByValue(strAction);
                        MyLogger.Log(this.Name + " := [ " + strAction + " ]");
                        break;
                }
                return true;
            }
            catch
            {
                MyLogger.Log("Exception occurred on  [ " + this.Name + " ]");
                return false;
            }
        }

        internal bool SelectRadioByValue(string strAction)
        {
            try
            {
                switch (strAction.ToUpper())
                {
                    case "!IGNORE!":
                        MyLogger.Log("Ignoring WebElement [ " + this.Name + " ]");
                        break;

                    default:
                        List<IWebElement> elements = new List<IWebElement>(driver.FindElements(this.By));
                        //new SelectElement(CurrentWebElement).SelectByValue(strAction);
                        foreach (IWebElement element in elements)
                        {
                            if (element.GetAttribute("value") == strAction)
                            {
                                element.Click();
                                MyLogger.Log(this.Name + " := [ " + strAction + " ]");
                            }
                        }
                        
                        break;
                }
                return true;
            }
            catch
            {
                MyLogger.Log("Exception occurred on  [ " + this.Name + " ]");
                return false;
            }
        }

        internal bool Clear()
        {
            try
            {
                if (IsDisplayed())
                {
                    CurrentWebElement.Clear();
                }
                return true;
            }
            catch
            {
                MyLogger.Log("Failed to clear web element [ " + this.Name + " ]");
                return false;
            }
        }

        internal bool Click()
        {
            try
            {
                Assert.IsTrue(WaitUntilDisplayed());
                Assert.IsTrue(WaitUtilEnabled());
                CurrentWebElement.Click();
                MyLogger.Log("Clicked on web element [ " + this.Name + " ]");
                return true;

            }
            catch
            {
                MyLogger.Log("Unable to click on web element [ " + this.Name + " ]");
                return false;
            }
        }

        public void ResetMouseCursor()
        {
            IWebDriver _driver = (IWebDriver)driver;            
            new Actions(_driver).MoveToElement(CurrentWebElement, 0, 0).Perform();           
        }

        public string MouseCurrentPosition()
        {
            IWebDriver _driver = (IWebDriver)driver;
            return Cursor.Position.ToString();
        }

        public bool IsFocused()
        {
            try
            {
                IWebDriver _driver = (IWebDriver)driver;
                IWebElement element = CurrentWebElement;
                return element.Equals(_driver.SwitchTo().ActiveElement());
            }
            catch
            {
                return false;
            }
        }

        public string ElementCurrentPosition()
        {
            return CurrentWebElement.Location.ToString();
        }

        public bool CompareDDMyOptions_UIVerify(string expectedOption)
        {
            try
            {
                expectedOption = expectedOption.Replace("{", "");
                expectedOption = expectedOption.Replace("}", "");
                IEnumerable<string> expectedOptions = expectedOption.Split(';');
                return CompareDDMyOptions(expectedOptions);
            }
            catch
            {
                return false;
            }

        }

        public bool CompareDDAllOptions(IEnumerable<string> expectedOptions)
        {
            try
            {
                IEnumerable<string> actualOptions = GetDropDownOptions();
                Assert.IsTrue(actualOptions.All(e => expectedOptions.Contains(e)));
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool CompareDDMyOptions(IEnumerable<string> expectedOptions)
        {
            try
            {
                IEnumerable<string> actualOptions = GetDropDownOptions();
                actualOptions = Clean(actualOptions);
                expectedOptions = Clean(expectedOptions);
                Assert.IsTrue(expectedOptions.All(e => actualOptions.Contains(e)));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private IEnumerable<string> Clean(IEnumerable<string> input)
        {
            input = input.Select(e => e.Trim());
            input = input.Select(e => e.Replace("\r", ""));
            input = input.Select(e => e.Replace("\n", ""));
            return input;
        }

        public IEnumerable<string> GetDropDownOptions()
        {
            SelectElement searchType = new SelectElement(CurrentWebElement);
            IEnumerable<string> actualEntityTypes = searchType.Options.Select(i => i.Text);
            return actualEntityTypes;
        }

        #endregion Action on this Objects

        #region State of this Objects
         
        internal bool WaitUtilDisappear()
        {
            int second = 1;
            for (; second <= this.globalwait; second++)
            {
                try
                {
                    if (!CurrentWebElement.Displayed)
                    {
                        if (second > 1)
                            MyLogger.Log("[ " + this.Name + " ] is not displayed, after [" + second + "] secs");
                        return true;
                    }
                }
                catch (NoSuchElementException)
                {
                    if (second > 1)
                        MyLogger.Log("[ " + this.Name + " ] is not displayed, after [" + second + "] secs");
                    return true;
                }
                Thread.Sleep(1000);
            }
            MyLogger.Log("Waited for [ " + second + " ] secs, but [ " + this.Name + " ] is still displayed.");
            return false;
        }

        internal bool IsDisplayed()
        {
            try
            {
                return CurrentWebElement.Displayed;
            }
            catch (NoSuchElementException)
            {
                MyLogger.Log("The webElement is not displayed := [" + this.Name + "]");
                return false;
            }
        }

        //Wait for 30 sec for the object to exist.
        internal bool WaitUntilDisplayed()
        {
            int second = 1;
            for (; second <= this.globalwait; second++)
            {
                try
                {
                    if (CurrentWebElement.Displayed)
                    {
                        if (second > 1)
                            MyLogger.Log("[ " + this.Name + " ] is displayed, after [" + second + "] secs");
                        return true;
                    }
                }
                catch
                { }
                Thread.Sleep(1000);
            }
            MyLogger.Log("Waited for [ " + second + " ] secs, but [ " + this.Name + " ] is not displayed.");
            return false;
        }

        internal bool WaitUntilDisplayed(int iWait)
        {
            int iTemp = this.globalwait;
            this.globalwait = iWait;
            bool bResult = WaitUntilDisplayed();
            this.globalwait = iTemp;
            return bResult;
        }

        public bool FindAndClickUsingJS()
        {
            IWebElement element = CurrentWebElement;

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

        public string GetText()
        {
            try
            {
                if (IsDisplayed())
                {
                    string text = CurrentWebElement.Text.ToString();
                    MyLogger.Log("[ " + this.Name + " ]'s having [ " + text + " ]");
                    return text;
                }
                return null;
            }
            catch
            {
                MyLogger.Log("Unable to get text from [" + this.Name + "]");
                return null;
            }
        }

        public string GetAttribute1(string attributeName)
        {
            try
            {
                if (IsDisplayed())
                {
                    string text = CurrentWebElement.GetAttribute(attributeName);
                    MyLogger.Log("[ " + this.Name + " ]'s [ "+attributeName+" ] having [ " + text + " ]");
                    return text;
                }
                return null;
            }
            catch
            {
                MyLogger.Log("Unable to get text from [" + this.Name + "]");
                return null;
            }
        }

        public List<string> GetAllText()
        {
            try
            {
                List<string> values = new List<string>();
                IEnumerable<IWebElement> elements =  driver.FindElements(this.By);
                foreach (IWebElement element in elements)
                {
                    values.Add(element.Text);
                }

                return values;
            }
            catch
            {
                return null;
            }
        }


        public void ValidateText1(string text)
        {
            string[] ValidationTypes = new string[] { Constants.TestData_HasText,
                                                      Constants.TestData_Contains,
                                                      Constants.TestData_HasDate,
                                                      "Table.OneRow.Contains."};
            string ValidationType = ValidationTypes.FirstOrDefault<string>(s => text.Contains(s));
            switch (ValidationType)
            {
                case "Table.OneRow.Contains.":
                    Table(text.Replace("Table.", ""));
                    break;
            }
        }

        public string GetAttribute(string attribute)
        {
            try
            {
                if (IsDisplayed())
                {
                    string attrValue = CurrentWebElement.GetAttribute(attribute).ToString();
                    MyLogger.Log("[ " + this.Name + " attribute " + attribute +" ]'s having [ " + attrValue + " ]");
                    return attrValue;
                }
                return null;
            }
            catch
            {
                MyLogger.Log("Unable to get text from [" + this.Name + " attribute " + attribute + "]");
                return null;
            }
        }

        public string GetValue()
        {
            try
            {
                if (IsDisplayed())
                {
                    string value = CurrentWebElement.GetAttribute("value").ToString();
                    MyLogger.Log("[ " + this.Name + " ]'s having [ " + value + " ]");
                    return value;
                }
                return null;
            }
            catch
            {
                MyLogger.Log("Unable to get value from [" + this.Name + "]");
                return null;
            }
        }

        internal IList<UIItem> GetAllUIItems()
        {
            try
            {
                IList<UIItem> uiitems = new List<UIItem>();
                IEnumerable<IWebElement> webelements = FindElements();
                foreach(var element in webelements)
                {
                    uiitems.Add(new UIItem(this.Name + "Child", element));
                }
                return uiitems;
            }
            catch 
            {
                return null;
            }
        }
        
        public string GetSelectedItemText()
        {
            try
            {
                if (IsDisplayed())
                {
                    string selectedItem = new SelectElement(CurrentWebElement).SelectedOption.Text;
                    MyLogger.Log("[ " + this.Name + " ]'s having [ " + selectedItem + " ]");
                    return selectedItem;
                }
                return null;
            }
            catch
            {
                MyLogger.Log("Unable to get value from [" + this.Name + "]");
                return null;
            }
        }

        internal bool WaitUtilEnabled()
        {
            return WaitUtilEnabled(this.By);
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
                catch
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
                catch
                { }
                Thread.Sleep(1000);
            }
            return false;
        }

        internal bool WaitUntilDisabled()
        {
            return WaitUntilDisabled(this.By);
        }

        internal bool WaitUntilDisabled(By by)
        {
            for (int second = 1; second <= 30; second++)
            {
                try
                {
                    if (!driver.FindElement(by).Enabled)
                    {
                        if (second > 1)
                            return true;
                    }
                }
                catch
                { }
                Thread.Sleep(1000);
            }
            return false;
        }

        internal bool IsEnabled()
        {
            try
            {
                Assert.IsTrue(CurrentWebElement.Enabled);
                MyLogger.Log(this.Name + "web Element is enabled.");
                return true;
            }
            catch
            {
                MyLogger.Log(this.Name + "web Element is not enabled.");
                return false;
            }
        }

        #endregion State of this Objects

        #region Get Attribute by index
        internal int GetOneElementIndex(string searchString)
        {
            try
            {
                List<int> index = GetElementIndex(searchString);
                if (index == null)
                {
                    MyLogger.Log("[" + this.Name + "] element not found on this page");
                    return -1;
                }
                if (index.Count == 0)
                {
                    //return element not found.
                    MyLogger.Log("[" + this.Name + "] element not found on this page");
                    return -1;
                }
                if (index.Count > 1)
                {
                    //more than one elements are found.
                    MyLogger.Log("[" + this.Name + "] more than one elements are found.");
                    return -1;
                }
                if (index.Count == 1)
                {
                    MyLogger.Log("[" + this.Name + "] element found on this page");
                    return index[0];
                }
                return index[0];
            }
            catch (Exception ex)
            {
                MyLogger.Log(ex.Message);
                return -1;
            }
        }

        internal List<int> GetElementIndex(string searchstring)
        {
            try
            {
                List<int> index = new List<int>();
                IList<IWebElement> elements = driver.FindElements(this.By);
                for (int iloop = 0; iloop < elements.Count; iloop++)
                {
                    if (Regex.IsMatch(elements[iloop].Text.ToUpper().Replace(" ",""), searchstring.ToUpper().Replace(" ", "")))
                    {
                        index.Add(iloop);
                       // MyLogger.Log("[" + searchstring + "] is displayed on [" + iloop + "] the data set");
                    }
                }
                if (index.Count == 0)
                {
                    MyLogger.Log("Given string is not found on the rows.");
                }
                return index;
            }
            catch (Exception ex)
            {
                MyLogger.Log(ex.Message);
                return null;
            }
        }

        internal List<int> GetElementIndex_NotContains(string searchstring)
        {
            try
            {
                List<int> index = new List<int>();
                IList<IWebElement> elements = driver.FindElements(this.By);
                for (int iloop = 0; iloop < elements.Count; iloop++)
                {
                    if (!Regex.IsMatch(elements[iloop].Text, searchstring))
                    {
                        index.Add(iloop);
                        MyLogger.Log("[" + searchstring + "] is NOT displayed on [" + iloop + "] the data set");
                    }
                }
                if (index.Count == 0)
                {
                    MyLogger.Log("Given string is not found on the rows.");
                }
                return index;
            }
            catch (Exception ex)
            {
                MyLogger.Log(ex.Message);
                return null;
            }
        }
        
        internal bool ClickByText(string searchstring)
        {
            try
            {
                IList<IWebElement> elements = driver.FindElements(this.By);
                for (int iloop = 0; iloop < elements.Count; iloop++)
                {
                    if (Regex.IsMatch(elements[iloop].Text, searchstring))
                    {
                        elements[iloop].Click();
                        MyLogger.Log("Clicked on web-element containing text [" + searchstring + "]");
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MyLogger.Log(ex.Message);
                return false;
            }
        }
        
        internal IList<IWebElement> FindElements()
        {
            try
            {
                IList<IWebElement> elements = driver.FindElements(this.By);
                return elements;
            }
            catch (EntryPointNotFoundException)
            {
                MyLogger.Log("Elements are not found :" + this.Name);
                return null;
            }
            catch (Exception ex)
            {
                MyLogger.Log(ex.Message);
                return null;
            }
        }

        internal bool ClickInside(int index, UIItem button)
        {
            try
            {
                IWebElement webelement = FindElementInside(index, button.By);
                Assert.IsTrue(webelement.Displayed);
                Assert.IsTrue(webelement.Enabled);
                webelement.Click();
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal IWebElement FindElementInside(int index,By by)
        {
            try
            {
                return ElementByIndex(index).FindElement(by);
            }
            catch
            {
                return null;
            }
        }

        internal IWebElement ElementByIndex(int index)
        {
            try
            {
                return driver.FindElements(this.By)[index-1];
            }
            catch
            {
                return null;
            }
        }

        internal bool IsDisplayed(int index)
        {
            try
            {
                return ElementByIndex(index).Displayed;
            }
            catch
            {
                return false;
            }
        }

        internal bool IsEnabled(int index)
        {
            try
            {
                Assert.IsTrue(ElementByIndex(index).Enabled);
                MyLogger.Log(this.Name + "web Element is enabled.");
                return true;
            }
            catch
            {
                MyLogger.Log(this.Name + "web Element is not enabled.");
                return false;
            }
        }

        internal bool Click(int index)
        {
            try
            {
                ElementByIndex(index).Click();
                return true;
            }
            catch (Exception ex)
            {
                MyLogger.Log(ex.Message);
                return false;
            }
        }

        internal string GetText(int index)
        {
            try
            {
                return ElementByIndex(index).Text;
            }
            catch (Exception ex)
            {
                MyLogger.Log(ex.Message);
                return null;
            }
        }


        #endregion

        #region custom actions

        internal bool SetDate(string p)
        {
            try
            {
                p = p.Replace(" 12:00:00 AM", "");
                string From_Date = p.Replace('/', '\0');
                //IWebElement fromDate = driver.FindElement(byFromDate);
                CurrentWebElement.SendKeys(OpenQA.Selenium.Keys.Backspace);
                CurrentWebElement.SendKeys(OpenQA.Selenium.Keys.Backspace);
                CurrentWebElement.SendKeys(OpenQA.Selenium.Keys.Backspace);
                CurrentWebElement.SendKeys(OpenQA.Selenium.Keys.Backspace);
                CurrentWebElement.SendKeys(OpenQA.Selenium.Keys.Backspace);
                CurrentWebElement.SendKeys(OpenQA.Selenium.Keys.Backspace);
                CurrentWebElement.SendKeys(OpenQA.Selenium.Keys.Backspace);
                CurrentWebElement.SendKeys(OpenQA.Selenium.Keys.Backspace);
                if (driver.ToString() == "OpenQA.Selenium.IE.InternetExplorerDriver")
                {
                    string[] arDate = From_Date.Split('\0');
                    CurrentWebElement.SendKeys(arDate[0]);
                    CurrentWebElement.SendKeys(arDate[1]);
                    CurrentWebElement.SendKeys(arDate[2]);
                }
                else
                {
                    CurrentWebElement.SendKeys(From_Date);
                }
                CurrentWebElement.SendKeys(OpenQA.Selenium.Keys.Tab);
                MyLogger.Log(" " + this.Name + "  is set to > [ " + p + " ]");
                return true;
            }
            catch
            {
                MyLogger.Log("Unable to click on web element [ " + this.Name + " ]");
                return false;
            }

        }

        internal bool TypeAndSelect(string value)
        {
            try
            {
                switch (value.ToUpper())
                {
                    case "!IGNORE!":
                        return true;
                    default:
                        Assert.IsTrue(this.TypeCharByChar(value));
                        Thread.Sleep(800);
                        CurrentWebElement.SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                        Thread.Sleep(800);
                        CurrentWebElement.SendKeys(OpenQA.Selenium.Keys.Enter);
                    break;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal bool TypeAndEnter(string value)
        {
            try
            {
                Assert.IsTrue(this.TypeCharByChar(value));
                Thread.Sleep(800);
                CurrentWebElement.SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                Thread.Sleep(800);
                CurrentWebElement.SendKeys(OpenQA.Selenium.Keys.ArrowUp);
                Thread.Sleep(800);
                CurrentWebElement.SendKeys(OpenQA.Selenium.Keys.Enter);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool TypeCharByChar(string value)
        {
            try
            {
                foreach (char val in value)
                {
                    CurrentWebElement.SendKeys(val.ToString());
                    Thread.Sleep(500);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        internal bool IsSelected()
        {
            try
            {
                Assert.IsTrue(CurrentWebElement.Selected);
                MyLogger.Log(this.Name + "web Element is in selected state");
                return true;
            }
            catch
            {
                MyLogger.Log(this.Name + "web Element is not in selected state.");
                return false;
            }
        }

        internal bool UIVerify(string text)
        {
            try
            {
                if (text.StartsWith("Table."))
                    return UIVerify_Table(text);
                switch (text.ToUpper())
                {
                    case "!IGNORE!":
                        //MyLogger.Log("Ignoring WebElement [ " + this.Name + " ]");
                        return true;
                    case "!ENABLED!":
                        if(IsEnabled())
                        {
                            MyLogger.Log("WebElement [ " + this.Name + " ] is enabled.");
                            return true;
                        }
                        else
                        {
                            MyLogger.Log("WebElement [ " + this.Name + " ] is disabled.");
                            return false;
                        }
                    case "!DISABLED!":
                        if(!IsEnabled())
                        {
                            MyLogger.Log("WebElement [ " + this.Name + " ] is disabled.");
                            return true;
                        }
                        else
                        {
                            MyLogger.Log("WebElement [ " + this.Name + " ] is enabled.");
                            return false;
                        }
                    case "!DISPLAYED!":
                        if (IsDisplayed())
                        {
                            MyLogger.Log("WebElement [ " + this.Name + " ] is displayed.");
                            return true;
                        }
                        else 
                        {
                            MyLogger.Log("WebElement [ " + this.Name + " ] is not displayed.");
                            return false;
                        }
                    case "!NOTDISPLAYED!":
                        if (!IsDisplayed())
                        {
                            MyLogger.Log("WebElement [ " + this.Name + " ] is not displayed.");
                            return true;
                        }
                        else
                        {
                            MyLogger.Log("WebElement [ " + this.Name + " ] is displayed.");
                            return false;
                        }
                    default:
                        return UIVerify_Details(text);
                }
            }
            catch
            {
                MyLogger.Log("Error in verification of webelement [ " + this.Name + " ].");
                return false;
            }
        }

        private bool UIVerify_Details(string actionData)
        {
            try
            {
                string pattern = @"(HasClass|HasText|HasDate|HasText|Edit|DDOptionsContains|Contains).([a-zA-Z0-9, ._/{}!;-]+)";
                Match result = Regex.Match(actionData, pattern);
                if (!result.Success)
                {
                    MyLogger.Log(string.Concat("Incorrect keyword string : [ ", actionData, " ]"));
                    return false;
                }
                string action = result.Groups[1].Value.ToString();
                string data = result.Groups[2].Value.ToString();
                if (action == Constants.TestData_HasText)
                    return HasText(data);
                if (action == Constants.TestData_Contains)
                    return ContainsText(data);
                if (action == Constants.TestData_HasDate)
                    return HasDate(data);
                if (action == Constants.TestData_Edit)
                    return Edit(data);
                if (action == Constants.TestData_DDOptionsContains)
                    return CompareDDMyOptions_UIVerify(data);
                if (action == Constants.TestData_DDOptionSelected)
                    return CompareDDOptionSelected(data);
                if (action == Constants.HasClass)
                    return HasClass(data);
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal string GetData(string action)
        {
            try
            {
                if (action.StartsWith("Table."))
                {
                    TableHelpers Table = new TableHelpers(this.Name,this.By,this.driver);
                    return Table.GetData(action.Replace("Table.",""));
                }
                return null;
            }
            catch
            {
                return null;
            }
            
        }

        public bool CompareDDOptionSelected_UIVerify(string expectedOption)
        {
            try
            {
                expectedOption = expectedOption.Replace("{", "");
                expectedOption = expectedOption.Replace("}", "");
                IEnumerable<string> expectedOptions = expectedOption.Split(';');
                return CompareDDMyOptions(expectedOptions);
            }
            catch
            {
                return false;
            }

        }

        internal bool CompareDDOptionSelected(string data)
        {
            try
            {
                return data == DDOptionSelected();
            }
            catch
            {
                return false;
            }
        }

        internal string DDOptionSelected()
        {
            try
            {
                SelectElement dropdown = new SelectElement(CurrentWebElement);
                string selectedoption = dropdown.SelectedOption.Text.Trim();
                selectedoption = selectedoption.Replace("\r", "");
                selectedoption = selectedoption.Replace("\n", "");
                return selectedoption;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ValidateText(string text)
        {
            string[] ValidationTypes = new string[] { Constants.TestData_HasText,
                                                      Constants.TestData_Contains,
                                                      Constants.TestData_HasDate,
                                                      "Table.OneRow.Contains."};
            string ValidationType = ValidationTypes.FirstOrDefault<string>(s => text.Contains(s));
            switch (ValidationType)
            {
                case "Table.OneRow.Contains.":
                    Table(text.Replace("Table.", ""));
                    break;
            }
        }

        public void Table(string text)
        {
            try
            {
                List<IWebElement> Rows = driver.FindElements(this.By).ToList();
                IEnumerable<IWebElement> contains = Rows.Where(s => s.Text.Contains(text));
                IEnumerable<IWebElement> has = Rows.Where(s => s.Text == text);
                if(contains.Count() == 0)
                {

                }
                if(has.Count()== 0)
                {

                }
            }
            catch
            {

            }
        }

        internal bool UIVerify(string text,int index)
        {
            try
            {
                if (text.StartsWith("Table."))
                    return UIVerify_Table(text);
                switch (text.ToUpper())
                {
                    case "!IGNORE!":
                        //MyLogger.Log("Ignoring WebElement [ " + this.Name + " ]");
                        return true;
                    case "!ENABLED!":
                        if (IsEnabled(index))
                        {
                            MyLogger.Log("WebElement [ " + this.Name + " ] is enabled.");
                            return true;
                        }
                        else
                        {
                            MyLogger.Log("WebElement [ " + this.Name + " ] is disabled.");
                            return false;
                        }
                    case "!DISABLED!":
                        if (!IsDisplayed(index))
                        {
                            MyLogger.Log("WebElement [ " + this.Name + " ] is disabled.");
                            return true;
                        }
                        else
                        {
                            MyLogger.Log("WebElement [ " + this.Name + " ] is enabled.");
                            return false;
                        }
                    case "!DISPLAYED!":
                        if (IsDisplayed(index))
                        {
                            MyLogger.Log("WebElement [ " + this.Name + " ] is displayed.");
                            return true;
                        }
                        else
                        {
                            MyLogger.Log("WebElement [ " + this.Name + " ] is not displayed.");
                            return false;
                        }
                    case "!NOTDISPLAYED!":
                        if (!IsDisplayed(index))
                        {
                            MyLogger.Log("WebElement [ " + this.Name + " ] is not displayed.");
                            return true;
                        }
                        else
                        {
                            MyLogger.Log("WebElement [ " + this.Name + " ] is displayed.");
                            return false;
                        }                        
                    default:
                        if (Regex.IsMatch(text, Constants.TestData_HasText) && HasText(text.Replace(Constants.TestData_HasText, "")))
                        {
                            return true;
                        }
                        if (Regex.IsMatch(text, Constants.TestData_HasDate) && HasDate(text.Replace(Constants.TestData_HasDate, "")))
                        {
                            return true;
                        }
                        break;
                }
                return false;
            }
            catch
            {
                MyLogger.Log("Error in verification of webelement [ " + this.Name + " ].");
                return false;
            }
        }

        internal bool UIVerify_Table(string text)
        {
            try
            {
                TableHelpers tablehelper = new TableHelpers(this.Name,this.By,this.driver);
                return tablehelper.Execute(text);
            }
            catch 
            {
                return false;
            }
        }

        internal bool HasText(string text, int index)
        {
            try
            {

                switch (text.ToUpper())
                {
                    case "!IGNORE!":
                        MyLogger.Log("Ignoring WebElement [ " + this.Name + " ]");
                        return true;

                    default:
                        if (IsDisplayed(index))
                        {
                            string text1 = ElementByIndex(index).Text.ToUpper().Trim();
                            string value = ElementByIndex(index).GetAttribute("value");
                            if (text1 == text.ToUpper().Trim())
                            {
                                MyLogger.Log("WebElement [ " + this.Name + " ] contains text = [" + text + "]");
                                return true;
                            }
                            else if (value == text.ToUpper().Trim())
                            {
                                MyLogger.Log("WebElement [ " + this.Name + " ] contains text = [" + text + "]");
                                return true;
                            }
                            else
                            {
                                MyLogger.Log("WebElement [ " + this.Name + " ] does not contains text = [" + text + "]");
                                return false;
                            }
                        }
                        MyLogger.Log("WebElement [ " + this.Name + " ] is not displayed");
                        return false;
                }
            }
            catch
            {
                MyLogger.Log("Exception occurred on  [ " + this.Name + " ]");
                return false;
            }
        }

        internal bool HasDate(string text)
        {
            try
            {

                switch (text.ToUpper())
                {
                    case "!IGNORE!":
                        MyLogger.Log("Ignoring WebElement [ " + this.Name + " ]");
                        return true;

                    default:
                        if (IsDisplayed())
                        {
                            string actualVal = CurrentWebElement.GetAttribute("value").ToUpper().Trim();
                            string expectedVal = text.ToUpper().Trim();
                            // if the date field is empty or doesnot need date conversion.
                            if (actualVal == expectedVal)
                            {
                                return true;
                            }
                            DateTime actual = Convert.ToDateTime(actualVal);
                            DateTime expected = Convert.ToDateTime(expectedVal);
                            if (DateTime.Equals(actual, expected))
                            {
                                MyLogger.Log("WebElement [ " + this.Name + " ] contains text = [" + text + "]");
                                return true;
                            }
                            else
                                return false;
                }
                return false;
            }
            }
            catch
            {
                return false;
            }
        }

        internal bool HasText(string text)
        {
            try
            {

                switch (text.ToUpper())
                {
                    case "!IGNORE!":
                        MyLogger.Log("Ignoring [ " + this.Name + " ]");
                        return true;

                    default:
                        if (IsDisplayed())
                        {
                            string text1 = CurrentWebElement.Text.ToUpper().Trim();
                            string value = CurrentWebElement.GetAttribute("value");
                            if (text1 == text.ToUpper().Trim())
                            {
                                MyLogger.Log("[ " + this.Name + " ] has text = [" + text + "]");
                                return true;
                            }
                            else if (value == text.ToUpper().Trim())
                            {
                                MyLogger.Log("[ " + this.Name + " ] has text = [" + text + "]");
                                return true;
                            }
                            else
                            {
                                MyLogger.Log("[ " + this.Name + " ] does not has text = [" + text1 + "]");
                                MyLogger.Log("[ " + this.Name + " ] has text = [" + text + "]");
                                return false;
                        }
                        }
                        MyLogger.Log("[ " + this.Name + " ] is not displayed");
                        return false;
                }
            }
            catch
            {
                MyLogger.Log("Exception occurred on  [ " + this.Name + " ]");
                return false;
            }
        }

        internal bool ContainsText(string text)
        {
            try
            {

                switch (text.ToUpper())
                {
                    case "!IGNORE!":
                        MyLogger.Log("Ignoring [ " + this.Name + " ]");
                        return true;

                    default:
                        if (IsDisplayed())
                        {
                            string text1 = CurrentWebElement.Text.ToUpper().Trim();
                            string value = CurrentWebElement.GetAttribute("value");
                            if (text1.Contains(text.ToUpper().Trim()))
                            {
                                MyLogger.Log("[ " + this.Name + " ] contains text = [ " + text + " ]");
                                return true;
                            }
                            else if (value.Contains(text.ToUpper().Trim()))
                            {
                                MyLogger.Log("[ " + this.Name + " ] contains text = [ " + text + " ]");
                                return true;
                            }
                            else
                            {
                                MyLogger.Log("[ " + this.Name + " ] does not contains text = [" + text + "]");
                                MyLogger.Log("[ " + this.Name + " ] has text = [" + text + "]");
                                return false;
                            }
                        }
                        MyLogger.Log("[ " + this.Name + " ] is not displayed");
                        return false;
                }
            }
            catch
            {
                MyLogger.Log("Exception occurred on  [ " + this.Name + " ]");
                return false;
            }
        }

        public bool StatusCheckORPerFormAction(string stAction)
        {
            try
            {
                switch (stAction.ToUpper().Trim())
                {
                    case "!IGNORE!":
                        MyLogger.Log("Ignoring WebElement [ " + this.Name + " ]");
                        break;

                    case "!DEFAULT!":
                        Assert.IsTrue(IsDisplayed());
                        break;

                    case "!DISPLAYED!":
                        Assert.IsTrue(IsDisplayed());
                        break;

                    case "!CHECKED!":
                        Assert.IsTrue(IsDisplayed());
                        Assert.IsTrue(IsSelected());
                        break;

                    case "!UNCHECKED!":
                        Assert.IsTrue(IsDisplayed());
                        Assert.IsFalse(IsSelected());
                        break;

                    case "!UNCHECK!":
                        Assert.IsTrue(IsDisplayed());
                        if (IsSelected())
                        {
                            Click();
                        }
                        Assert.IsFalse(IsSelected());
                        break;

                    case "!CHECK!":
                        Assert.IsTrue(IsDisplayed());
                        if (!IsSelected())
                        {
                            Click();
                        }
                        Assert.IsTrue(IsSelected());
                        break;
                    case "!CLICK!":
                        Assert.IsTrue(IsDisplayed());
                        Assert.IsTrue(Click());
                        break;
                }
                return true;
            }
            catch
            {
                MyLogger.Log("Failed to Check Status or Perform Action for WebElement [ " + this.Name + " ]");
                return false;
            }

        }

        public void FindAndClickLastElement()
        {
            try
            {
                IList<IWebElement> elements = driver.FindElements(this.By);
                if (elements != null)
                {
                    elements[elements.Count - 1].Click();
                }
            }
            catch (Exception ex)
            {
                MyLogger.Log(ex.Message);
            }
        }

        public bool FindAndClickFirstElement()
        {
            try
            {
                IList<IWebElement> elements = FindElements();
                if (elements != null)
                {
                    elements[0].Click();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MyLogger.Log(ex.Message);
                return false;
            }
        }

        public int GetCountOfElements()
        {
            try
            {
                return driver.FindElements(this.By).Count;
            }
            catch
            {
                return 0;
            }
        }

        internal bool IsAttribtuePresent(string attribute)
        {
            bool result = false;
            try
            {
                string value = CurrentWebElement.GetAttribute(attribute);
                if (value != null)
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
            }
            return result;
        }

    }
}