using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SWAT.FrameWork.Utilities
{
    using Config = SWAT.Configuration.TestStartInfo;

    public class WebTable
    {
        private static IWebDriver driver;//= TestSettings.driver;

        public WebTable(Config c)
        {
            driver = c.Driver;
        }

        public WebTable(IWebDriver _driver)
        {
            driver = _driver;
        }
        // Get the top result string from the table and
        // compare the row with actual table row.
        public bool RowCount_Compare(By byTableRow, By byTopResultString)
        {
            try
            {
                int intRowCount = driver.FindElements(byTableRow).Count;
                int intExpectedRowCount = int.Parse(driver.FindElement(byTopResultString).Text);
                if (intRowCount == intExpectedRowCount) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        //Compare the single col value are matching the given value
        public bool ColValues_Compare(By byTableHdr, By byTable, string strHdrName, string strValToCompare)
        {
            try
            {
                List<string> colVals = ColValues_FrmColHdr(byTableHdr, byTable, strHdrName);
                foreach (string colVal in colVals)
                    if (colVal != strValToCompare) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
       
        public bool ColValues_Compare_ForDateRange(By byTableHdr, By byTable, string strHdrName, string strFromDate, string strToDate)
        {
            DateTime fromDate = new DateTime();
            DateTime toDate = new DateTime();
            if (strFromDate != "")
            {
                fromDate = DateTime.Parse(strFromDate);
            }
            if (strToDate != "")
            {
                toDate = DateTime.Parse(strToDate);
            }
            try
            {
                List<DateTime> dates = ColValues_DatesToList(byTableHdr, byTable, strHdrName);
                foreach (DateTime date in dates)
                {
                    if (!(date > fromDate || date < toDate))
                        return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ColValues_Contains(By byTableHdr, By byTable, string strHdrName, string strValToCompare, string optionalVal = "")
        {
            try
            {
                List<string> colVals = ColValues_FrmColHdr(byTableHdr, byTable, strHdrName);
                foreach (string colVal in colVals)
                    if (!colVal.Contains(strValToCompare) && !colVal.Contains(optionalVal)) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
        //Coverst column value into list
        public List<DateTime> ColValues_DatesToList(By byTableHdr, By byTable, string strHdrName)
        {
            Regex regex = new Regex(@"\d\d\/\d\d\/\d\d\d\d");
            Regex shtDate = new Regex(@"\d\d\/\d\d");
            string strOnlyDate;
            //string strColHeader = "Pickup Date";
            List<string> colVals = ColValues_FrmColHdr(byTableHdr, byTable, strHdrName);
            List<DateTime> colDates = new List<DateTime>();
            foreach (string colVal in colVals)
            {
                //  if (colVal != "--")
                // {
                if (regex.Match(colVal).Success)
                {
                    strOnlyDate = regex.Match(colVal).Value;
                    colDates.Add(DateTime.Parse(strOnlyDate));
                }
                else if (shtDate.Match(colVal).Success)
                {
                    strOnlyDate = shtDate.Match(colVal).Value + @"/2015";
                    colDates.Add(DateTime.Parse(strOnlyDate));
                }

                // }
            }
            return colDates;
        }

        //Coverts the column header of the webtable into value
        public List<string> ColValues_FrmColHdr(By byTableHdr, By byTable, string strHdrName)
        {
            List<IWebElement> RowlValues = driver.FindElements(byTableHdr).ToList();
            int intColIntex = 1;
            List<string> colValues = new List<string>();
            foreach (IWebElement RowlValue in RowlValues)
            {
                if (RowlValue.Text == strHdrName)
                {
                    colValues = ColValues_FromColIndex(byTable, intColIntex);
                    break;
                }
                intColIntex++;
            }
            return colValues;
        }

        public List<string> ColValues_FromColIndex(By byTable, int colIndex)
        {//strTableCSS
            List<string> strcolValues = new List<string>();
            string strStringUsed = GetStringUsedToFind(byTable);
            List<IWebElement> colValues = driver.FindElements(By.CssSelector(strStringUsed + ":nth-child(" + colIndex + ")")).ToList();
            foreach (IWebElement colValue in colValues)
            {
                strcolValues.Add(colValue.Text);
            }
            return strcolValues;
        }

        //Get the elements string
        public string GetStringUsedToFind(By byElement)
        {
            string strStringUsed = byElement.ToString();
            strStringUsed = strStringUsed.Replace("By.ClassName: ", "");
            strStringUsed = strStringUsed.Replace("By.CssSelector: ", "");
            strStringUsed = strStringUsed.Replace("By.Id: ", "");
            strStringUsed = strStringUsed.Replace("By.LinkText: ", "");
            strStringUsed = strStringUsed.Replace("By.Name: ", "");
            strStringUsed = strStringUsed.Replace("By.PartialLinkText: ", "");
            strStringUsed = strStringUsed.Replace("By.TagName: ", "");
            strStringUsed = strStringUsed.Replace("By.XPath: ", "");
            return strStringUsed;
        }
    }
}