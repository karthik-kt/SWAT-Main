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

namespace SWAT.FrameWork.Utilities
{
    class TableHelpers
    {
        private string _name;

        private By _by;

        private ISearchContext _driver;

        public TableHelpers(string name, By by, ISearchContext driver)
        {
            _by = by;
            _name = name;
            _driver = driver;
        }

        internal bool Execute(string action)
        {
            try
            {
                bool actualResult = false;
                action = action.Replace("Table.", "");
                string pattern = @"(Verify|FindAndClick|GetData).(AllRows|AnyRow).?(Column[0-9]+)?.(ContainsText|HasText).?([a-zA-Z0-9 ,.-_]+)?";
                Match result = Regex.Match(action, pattern);
                if (!result.Success)
                {
                    MyLogger.Log(string.Concat("Incorrect keyword string : [ ", action, " ]"));
                    return false;
                }
                string testAction = result.Groups[1].Value.ToString();
                string rowType = result.Groups[2].Value.ToString();
                string temp = result.Groups[3].Value.ToString().Replace("Column","");
                int? columnIndex = (temp != "" ? Convert.ToInt32(temp) : (int?)null );
                string validationType = result.Groups[4].Value.ToString();
                string text = result.Groups[5].Value.ToString();
                if(testAction.ToUpper() == "FINDANDCLICK" && rowType.ToUpper() == "ALLROWS" && validationType.ToUpper() == "CONTAINSTEXT")
                {
                    actualResult = FindAndClickRowElement(columnIndex, text);
                }
                if (rowType.ToUpper() == "ALLROWS" && validationType.ToUpper() == "CONTAINSTEXT")
                    actualResult = AllRowContains(columnIndex, text);
                if (rowType.ToUpper() == "ALLROWS" && validationType.ToUpper() == "HASTEXT")
                    actualResult = AllRowHas(columnIndex, text);
                if (rowType.ToUpper() == "ANYROW" && validationType.ToUpper() == "CONTAINSTEXT")
                    actualResult = AnyRowContains(columnIndex, text);
                return actualResult;
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
                action = action.Replace("Table.", "");
                string actualResult = null;
                string pattern = @"(Verify|FindAndClick|GetData).(AllRows|AnyRow).?(Column[0-9]+)?.?";
                Match result = Regex.Match(action, pattern);
                if (!result.Success)
                {
                    MyLogger.Log(string.Concat("Incorrect keyword string : [ ", action, " ]"));
                    return null;
                }
                string testAction = result.Groups[1].Value.ToString();
                string rowType = result.Groups[2].Value.ToString();
                string temp = result.Groups[3].Value.ToString().Replace("Column", "");
                int? columnIndex = (temp != "" ? Convert.ToInt32(temp) : (int?)null);
                string validationType = result.Groups[4].Value.ToString();
                string text = result.Groups[5].Value.ToString();
                if (rowType.ToUpper() == "ANYROW" && testAction.ToUpper() == "GETDATA")
                    actualResult = GetData(columnIndex);
                return actualResult;
            }
            catch
            {
                return null;
            }
        }

        internal string GetData(int? columnindex)
        {
            try
            {
                IWebElement element = _driver.FindElements(BuildBy(columnindex)).FirstOrDefault();
                return element.Text;
            }
            catch
            {
                return null;
            }
        }

        private bool FindAndClickRowElement(int? columnindex, string text)
        {
            try
            {
                IWebElement element = _driver.FindElements(BuildBy(columnindex)).Where(e => e.Text.Contains(text)).FirstOrDefault();
                element.Click();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool AnyRowContains(int? columnindex, string expected)
        {
            try
            {
                MyLogger.Log("Web Table : [ " + this._name + " ]");
                if (columnindex != null)
                    MyLogger.Log("Column # : [ " + columnindex + " ]");
                IEnumerable<string> actuals = FindAColumnElements(columnindex).Select(i => i.Text);
                if (actuals.Any(e => e.Contains(expected)))
                {
                    MyLogger.Log("Some rows does contains : [ " + expected + " ]");
                    return true;
                }
                MyLogger.Log("None of the row contains : [ " + expected + " ]");
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool AllRowContains(int? columnindex,string expected)
        {
            try
            {
                MyLogger.Log("Web Table : [ " + this._name + " ]");
                if(columnindex != null)
                    MyLogger.Log("Column # : [ " + columnindex + " ]");
                IEnumerable<string> actuals = FindAColumnElements(columnindex).Select(i => i.Text);
                if (actuals.All(e => e.Contains(expected)))
                {
                    MyLogger.Log("All rows does contains : [ " + expected + " ]");
                    return true;
                }
                MyLogger.Log("All rows does NOT contains : [ " + expected + " ]");
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool AllRowHas(int? columnindex, string expected)
        {
            try
            {
                MyLogger.Log("Web Table : [ " + this._name + " ]");
                MyLogger.Log("Column # : [ " + this._name + " ]");
                IEnumerable<string> actuals = FindAColumnElements(columnindex).Select(element => element.Text);
                if (actuals.All(e => (e == expected)))
                {
                    MyLogger.Log("All rows has : [ " + expected + " ]");
                    return true;
                }
                MyLogger.Log("All rows does NOT has : [ " + expected + " ]");
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private List<IWebElement> FindAColumnElements(int? columnindex)
        {
            try
            {
               return _driver.FindElements(BuildBy(columnindex)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private By BuildBy(int? columnindex)
        {
            try
            {
                string temp = _by.ToString();
                temp = temp.Replace("By.CssSelector: ", "");
                if (columnindex != null)
                    temp = temp + ":nth-child(" + columnindex + ")";
                //else
                //    temp = temp.Replace(">td", "");
                return  By.CssSelector(temp);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
