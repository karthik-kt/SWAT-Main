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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Threading;
using SWAT.Applications.Claw.DAL;
using SWAT.Data;
using SWAT.Logger;
using SWAT.FrameWork.Utilities;
using SWAT.Applications.Claw.ObjectRepository;
using SWAT.Configuration;
using System;

namespace SWAT.Applications.Claw
{
    
    public class Accouting 
    {              
        private Page _Page;        
        private AccoutingData _Data;
        private WebTable _Webtable;
        private AccountingPage _AccoutingPage;
        private LoadDetailsPage _LoadDetailPage;
        private TestStartInfo testConfig;
        private IWebDriver driver;

        public Accouting(TestStartInfo info, DataManager datamanager)
        {
            testConfig = info;
            driver = info.Driver;
            _Page = new Page(info.Driver);
            _Data = new AccoutingData(datamanager);
            _Webtable = new WebTable(info.Driver);
            _AccoutingPage = new AccountingPage(info);
            _LoadDetailPage = new LoadDetailsPage(info);
        }
        
        public string fileter()
        {
            _AccoutingPage.AdvSearchBtn.WaitUntilDisplayed();
            return "";
        }

        public string SortColumn()
        {
            try
            {
                _AccoutingPage.WaitUntilLoading();
                Assert.IsTrue(_AccoutingPage.SearchTableHeader.WaitUntilDisplayed());
                List<string> actual = new List<string>();
                if (_Data.EntityName.ToUpper() == "CUSTOMER")
                {
                    Assert.IsTrue(_AccoutingPage.Destination_Col_Customer.WaitUntilDisplayed());
                    Assert.IsTrue(_AccoutingPage.Destination_Col_Hdr_Customer.Click());
                    _AccoutingPage.WaitUntilLoading();
                    actual = _AccoutingPage.Destination_Col_Customer.GetAllText();
                }
                else
                {
                    Assert.IsTrue(_AccoutingPage.Destination_Col_Carrier.WaitUntilDisplayed());
                    Assert.IsTrue(_AccoutingPage.Destination_Col_Hdr_Carrier.Click());
                    _AccoutingPage.WaitUntilLoading();
                    actual = _AccoutingPage.Destination_Col_Carrier.GetAllText();
                }
                Assert.IsTrue(_AccoutingPage.SearchTableHeader.WaitUntilDisplayed());
                Assert.IsTrue(_AccoutingPage.SearchTable.WaitUntilDisplayed());
                List<string> expected = new List<string>();
                expected = actual;
                actual.Sort();
                CollectionAssert.AreEqual(actual, expected);
                return "SortSuccess";
            }
            catch
            {
                return "SortFailed";
            }
        }

        //public string SortColumn_Old()
        //{
        //    //try
        //    //{
        //    //    UIItem ColumnToSort = new UIItem("Column to sort", By.XPath(_Data.ColToSort),driver);
        //    //    _AccoutingPage.WaitUntilLoading();
        //    //    Assert.IsTrue(_AccoutingPage.SearchTableHeader.WaitUntilDisplayed());
        //    //   // List<string> liActual = _AccoutingPage.Destination_Col.GetAllText();
        //    //    //Assert.IsTrue(_AccoutingPage.SearchTable.WaitUntilDisplayed(40));
        //    //    //List<string> liActual = _Webtable.ColValues_FrmColHdr(_AccoutingPage.SearchTableHeader.By, _AccoutingPage.SearchTable.By, _Data.SortByColHeader);
        //    //   // if (liActual.Count == 0)
        //    //        return "NoRowsDisplayed";
        //    //    Assert.IsTrue(ColumnToSort.WaitUntilDisplayed());
        //    //    Assert.IsTrue(ColumnToSort.Click());
        //    //    Assert.IsTrue(_AccoutingPage.SearchTableHeader.WaitUntilDisplayed());
        //    //    Assert.IsTrue(_AccoutingPage.SearchTable.WaitUntilDisplayed());
        //    //    List<string> liExec = _Webtable.ColValues_FrmColHdr(_AccoutingPage.SearchTableHeader.By, _AccoutingPage.SearchTable.By, _Data.SortByColHeader);
        //    //   // liActual.Sort();
        //    //   // CollectionAssert.AreEqual(liActual, liExec);
        //    //    return "SortSuccess";
        //    //}
        //    //catch
        //    //{
        //        return "SortFailed";
        //    //}
        //}

        public string GetLoadID()
        {
            try
            {
                Assert.IsTrue(_AccoutingPage.SearchResult1stRow.WaitUntilDisplayed());
                string value = _AccoutingPage.SearchResult1stRow.GetText();
                _Data.LoadId = value;
                return "Done";
            }
            catch
            {
                return "Failed";
            }
        }

        public string GetPayDate()
        {
            try
            {
                Assert.IsTrue(_AccoutingPage.SearchResult1stRow.WaitUntilDisplayed());
                string value = _AccoutingPage.SearchResult1stRowPayDate.GetText();
                value = Convert.ToDateTime(value.Replace("Expected ", "") + "/" + DateTime.Now.Year).ToString(_Data.DateFormat);
                _Data.PayDate = value;
                return "Done";
            }
            catch
            {
                return "Failed";
            }
        }

        public string GetInvoiceID()
        {
            try
            {
                Assert.IsTrue(_AccoutingPage.SearchResult1stRow.WaitUntilDisplayed());
                string value = _AccoutingPage.SearchResult1stRowInvoice.GetText();
                _Data.InvoiceId = value;
                return "Done";
            }
            catch
            {
                return "Failed";
            }
        }
        private bool SelectInvoiceStatus()
        {
            try
            {
                switch (_Data.InvoiceStatus)
                {
                    case "All Loads":
                        _AccoutingPage.AllLoads.Click();
                        break;

                    case "Only invoiced":
                        _AccoutingPage.OnlyInvoiced.Click();
                        break;

                    case "Only un-invoiced":
                        _AccoutingPage.OnlyUninvoiced.Click();
                        break;

                    case "!IGNORE!":
                        break;
                }
                _AccoutingPage.InvoiceStatuses.SelectByText(_Data.InvoiceStatuses);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool NavAndOpenAdvnFilter()
        {
            try
            {
                Assert.IsTrue(_AccoutingPage.AccoutingTab.Click());
                Assert.IsTrue(_AccoutingPage.SearchTableHeader.WaitUntilDisplayed(60));
                Assert.IsTrue(_AccoutingPage.BalanceAmt.WaitUntilDisplayed());
                Assert.IsTrue(_AccoutingPage.AdvFliter.WaitUntilDisplayed());
                Assert.IsTrue(_AccoutingPage.SearchVal.WaitUtilEnabled());
                while (!_AccoutingPage.AdvSearchBtn.WaitUntilDisplayed(2)) 
                {
                    _AccoutingPage.AdvFliter.Click();
                    Thread.Sleep(Constants.Wait_Short);
                }
                _AccoutingPage.FromDate.WaitUtilEnabled();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public string SearchAndOpenLoad()
        {
            try
            {
                string actualresult = null;
                actualresult = SearchLoad();
                if (actualresult == "SearchSuccess")
                {
                   // Thread.Sleep(Constants.intShortWait);
                    Assert.IsTrue(_AccoutingPage.SearchResult1stRow.WaitUntilDisplayed());
                    try
                    {
                        Assert.IsTrue(_AccoutingPage.SearchResult1stRow.Click());
                    }
                    catch
                    {
                        Assert.IsTrue(_AccoutingPage.SearchResult1stRow.FindAndClickUsingJS());
                    }                    
                    Assert.IsTrue(_LoadDetailPage.OptionBtn.WaitUntilDisplayed());
                    if (_LoadDetailPage.OptionBtn.IsDisplayed())
                        actualresult = "LoadOpenSuccess";
                    else
                        actualresult = "LoadOpenFailed";
                }
                return actualresult;
            }
            catch
            {
                return "LoadOpenedFailed";
            }
        }

        public string OpenLoad()
        {
            try
            {
                string actualresult = null;
                UIItem optionButton = null;
                Assert.IsTrue(_AccoutingPage.SearchResult1stRow.WaitUntilDisplayed());
                try
                {
                    Assert.IsTrue(_AccoutingPage.SearchResult1stRow.Click());
                }
                catch
                {
                    Assert.IsTrue(_AccoutingPage.SearchResult1stRow.FindAndClickUsingJS());
                }
                optionButton = _Data.EntityName == "FACTORINGCOMPANY" ? _LoadDetailPage.LoadInquireBtn : _LoadDetailPage.OptionBtn;

                Assert.IsTrue(optionButton.WaitUntilDisplayed());
                if (optionButton.IsDisplayed())
                    actualresult = "LoadOpenSuccess";
                else
                    actualresult = "LoadOpenFailed";
                return actualresult;
            }
            catch
            {
                return "LoadOpenFailed";
            }
        }

        public string SearchLoad()
        {
            try
            {
                Assert.IsTrue(_AccoutingPage.AccoutingTab.Click());
                _AccoutingPage.WaitUntilLoading();
                Assert.IsTrue(_AccoutingPage.BasicSearchButton.WaitUntilDisplayed());
                Assert.IsTrue(_AccoutingPage.BasicSearchButton.Click());
                Assert.IsTrue(_AccoutingPage.BasicSearchRegion.WaitUntilDisplayed());
                if (_Data.EntityName.ToLower() != "factoringcompany")
                {
                    Assert.IsTrue(_AccoutingPage.SearchType.WaitUntilDisplayed());
                    Assert.IsTrue(_AccoutingPage.SearchType.WaitUtilEnabled());
                    Assert.IsTrue(_AccoutingPage.SearchType.SelectByText(_Data.SearchType));
                    MyLogger.Log("Search using =[ " + _Data.SearchType + " ]");
                }
                Assert.IsTrue(_AccoutingPage.SearchVal.ClearAndEdit(_Data.SearchVal));
                Assert.IsTrue(_AccoutingPage.SearchVal.Edit(Keys.Tab));
                try
                {
                    _AccoutingPage.SearchVal.Edit(Keys.Enter);
                }
                catch
                {
                    _AccoutingPage.SearchButton.Click();
                }
                _AccoutingPage.WaitUntilLoading();
                //Search result table displayed.
                Assert.IsTrue(_AccoutingPage.SearchTableHeader.WaitUntilDisplayed());
                Assert.IsTrue(_AccoutingPage.SearchResult1stRow.WaitUntilDisplayed());
                if (_Page.NoResultsFound())
                    return "NoResultsFound";
                return "SearchSuccess";
            }
            catch
            {
                return "SearchFailed";
            }
        }

        public string VerifySearchResults(DataManager testData)
        {
            try
            {
                Dictionary<string, string> accSearchResult = new Dictionary<string, string>();
                bool loadResultStatus = false;
                bool invoiceResultStatus = false;
                bool carrierResultStatus = false;
                Assert.IsTrue(_AccoutingPage.SearchResult1stRow.WaitUntilDisplayed());
                List<SearchResult_Rows> rows = _AccoutingPage.rows;

                foreach (SearchResult_Rows row in rows)
                {
                    accSearchResult["Load #"] = row.LoadId.GetText(0);
                    accSearchResult["Invoice #"] = row.Invoice.GetText(0);
                    accSearchResult["CarrierName"] = row.Carrier.GetText(0);
                    loadResultStatus = testData.CompareColValues(accSearchResult, "Load #", _Data.LoadId);
                    invoiceResultStatus = testData.CompareColValues(accSearchResult, "Invoice #", _Data.InvoiceId);
                    carrierResultStatus = testData.CompareColValues(accSearchResult, "CarrierName", _Data.CarrierName);
                    if (loadResultStatus && invoiceResultStatus && carrierResultStatus)
                    {
                        return "VerificationSuccess";
                    }
                }
                return "VerificationFail";
            }
            catch
            {
                return "VerificationFail";
            }
        }
        public string AccFilter()
        {
            string strActualRes = null;
            switch (_Data.EntityName)
            {
                case "FACTORINGCOMPANY":
                    strActualRes = Filter_FactortingCompany();
                    break;

                default:
                    strActualRes = Filter_Ignore();
                    break;
            }
            return strActualRes;
        }

        private string Filter_Ignore()
        {
            try
            {
                Assert.IsTrue(NavAndOpenAdvnFilter());
                if (_Data.SearchBy == "Within 30 days" || _Data.SearchBy == "31 - 60 days" || _Data.SearchBy == "61 - 90 days" || _Data.SearchBy == "Beyond 90 days")
                {
                    SelectInvoiceStatus();
                    Assert.IsTrue(_AccoutingPage.Within30Days.WaitUntilDisplayed());
                    Assert.IsTrue(_AccoutingPage.Within30Days.Click());
                    MyLogger.Log("Selected option [" + _Data.InvoiceStatus + "]");
                }
                else
                {
                    Assert.IsTrue(_AccoutingPage.DateRange.SelectByText(_Data.SearchBy));
                    SelectInvoiceStatus();
                    Assert.IsTrue(_AccoutingPage.FromDate.WaitUntilDisplayed());
                    Assert.IsTrue(_AccoutingPage.FromDate.ClearAndEdit(_Data.FromDate));
                    MyLogger.Log("From date [" + _Data.FromDate + "]");
                    Assert.IsTrue(_AccoutingPage.ToDate.WaitUntilDisplayed());
                    Assert.IsTrue(_AccoutingPage.ToDate.ClearAndEdit(_Data.ToDate));
                    MyLogger.Log("To date [" + _Data.ToDate + "]");
                    Assert.IsTrue(_AccoutingPage.AdvSearchBtn.WaitUntilDisplayed());
                    Assert.IsTrue(_AccoutingPage.AdvSearchBtn.Click());
                }
                _AccoutingPage.SearchTableHeader.WaitUntilDisplayed();
                return "FilterSuccess";
            }
            catch
            {
                return "FilteringFailed";
            }
        }

        private string Filter_FactortingCompany()
        {
            try
            {               
                Assert.IsTrue(_AccoutingPage.AccoutingTab.Click());
                Assert.IsTrue(_AccoutingPage.AdvFliter.WaitUntilDisplayed());
                Assert.IsTrue(_AccoutingPage.AdvFliter.Click());
                Assert.IsTrue(_AccoutingPage.FromDate.ClearAndEdit(_Data.FromDate));
                _AccoutingPage.ToDate.Edit(Keys.Tab);
                Assert.IsTrue(_AccoutingPage.ToDate.ClearAndEdit(_Data.ToDate));
                _AccoutingPage.ToDate.Edit(Keys.Tab);
                Assert.IsTrue(_AccoutingPage.InvoiceStatus.SelectByText(_Data.InvoiceStatus));
                Assert.IsTrue(_AccoutingPage.CarrierName.TypeAndSelect(_Data.CarrierName));
                _AccoutingPage.ToDate.Edit(Keys.Tab);
                Assert.IsTrue(_AccoutingPage.AdvSearchBtn.Click());
                _AccoutingPage.WaitUntilLoading();
                if (!_AccoutingPage.SearchResult1stRow.WaitUntilDisplayed())
                    return "NoRowsDisplayed";
                return "FilterSuccess";
            }
            catch
            {
                return "FilteringFailed";
            }
        }

        public string VerifyControls()
        {
            try
            {
                if (_Data.EntityName == "FACTORINGCOMPANY")
                {
                    Assert.IsTrue(_AccoutingPage.AccoutingTab.Click());
                    Assert.IsTrue(_AccoutingPage.AdvFliter.WaitUntilDisplayed());
                    Assert.IsTrue(_AccoutingPage.AdvFliter.Click());
                    Assert.IsTrue(_AccoutingPage.FromDate.StatusCheckORPerFormAction(_Data.FromDate));
                    Assert.IsTrue(_AccoutingPage.ToDate.StatusCheckORPerFormAction(_Data.ToDate));
                    Assert.IsTrue(_AccoutingPage.InvoiceStatus.StatusCheckORPerFormAction(_Data.InvoiceStatus));
                    Assert.IsTrue(_AccoutingPage.CarrierName.StatusCheckORPerFormAction(_Data.CarrierName));
                    Assert.IsTrue(_AccoutingPage.AdvSearchBtn.StatusCheckORPerFormAction(_Data.AdvSearchButton));
                    Assert.IsTrue(_AccoutingPage.ClearAll.StatusCheckORPerFormAction(_Data.ClearAllButton));
                    return "VerifyControlsSuccess";
                }
                return "VerifyControlsFailed";
            }
            catch
            {
                return "VerifyControlsFailed";
            }
        }

        public string FilterAndVerify()
        {
            string strActualRes = null;
            try
            {
                if (_Data.SearchType == "!IGNORE!")
                {
                    strActualRes = AccFilter();
                  if (strActualRes != "FilterSuccess")
                    {
                        return strActualRes;
                    }
                }
                if (_Data.EntityName == "FACTORINGCOMPANY")
                {
                    Assert.IsTrue(FilterResultVerify_Factoring());
                }
                else if (_Data.EntityName.ToUpper() == "CARRIER")
                {
                    Assert.IsTrue(FilterResultVerify_Carrier());
                }
                else
                {
                    Assert.IsTrue(FilterResultVerify_Customer());
                }
                strActualRes = "FilterAndVerifySuccess";
            }
            catch
            {
                strActualRes = "FilterAndVerifyFailed";
            }
            return strActualRes;
        }

        private bool FilterResultVerify_Factoring()
        {
            try
            {
                //for advance search options set value of column verifycolumn to ignore
                if (_Data.VerifyColumn == "!IGNORE!")
                {
                    Assert.IsTrue(Verify("LoadDate") && Verify("InvoiceStatus") && Verify("CarrierName"));
                    if (OpenLoad() == "LoadOpenFailed")
                    {
                        return false;
                    }
                }
                else
                {
                    //to verify columns other than advance search options set value verifycolumn to Column Name
                    Assert.IsTrue(Verify(_Data.VerifyColumn));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool FilterResultVerify_Customer()
        {
            try
            {
                Assert.IsTrue(Verify(_Data.SearchBy));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool FilterResultVerify_Carrier()
        {
            try
            {
                Assert.IsTrue(Verify(_Data.VerifyColumn));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool Verify(string fieldName)
        {
            bool result = false;
            try
            {
                switch (fieldName)
                {
                    case "LoadDate":                                //for factoring company's load date column
                        if (_Data.FromDate == "!IGNORE!")
                        {
                            return true;
                        }
                        else
                        {
                            Assert.IsTrue(VerifyColumn(_AccoutingPage.LoadDateColumn, "Load Date"));
                            result = SearchResult_Verify_TextCol(_Data.FromDate, "Load Date");
                        }
                        break;

                    case "InvoiceStatus":
                        if (_Data.InvoiceStatus == "!IGNORE!")
                        {
                            return true;
                        }
                        else
                        {
                            Assert.IsTrue(VerifyColumn(_AccoutingPage.InvoiceStatusColumn, "Invoice Status"));
                            result = SearchResult_Verify_TextCol(_Data.InvoiceStatus, "Invoice Status");
                        }
                        break;

                    case "CarrierName":
                        if (_Data.CarrierName == "!IGNORE!")
                        {
                            return true;
                        }
                        else
                        {
                            Assert.IsTrue(VerifyColumn(_AccoutingPage.CarrierNameColumn, "Carrier"));
                            result = SearchResult_Verify_TextCol(_Data.CarrierName, "Carrier");
                        }
                        break;
                    case "Invoice Date":
                        result = SearchResult_Verify_TextCol(_Data.FromDate, _Data.SearchBy);
                        break;
                    case "Load Date":                                           //for customer's load date column
                        result = SearchResult_Verify_TextCol(_Data.FromDate, _Data.SearchBy); 
                        break;
                    case "Reference #":
                        result = SearchResult_Verify_TextCol(_Data.SearchVal, "Ref #");
                        break;
                    case "Check #":
                        if (_Data.EntityName == "CUSTOMER")
                        {
                            result = SearchResult_Verify_TextCol(_Data.SearchVal, "Check");
                        }
                        else
                        {
                            result = SearchResult_Verify_TextCol(_Data.SearchVal, "Check(s)");
                        }
                        break;
                    case "Pay Date(s)":
                        WebTable w = new WebTable(testConfig);
                        switch (_Data.InvoiceStatus)
                        {
                            case "In Process":
                                Assert.IsTrue(w.ColValues_Contains(_AccoutingPage.SearchTableHeader.By, _AccoutingPage.SearchTable.By, "Pay Date(s)", "Expected", "--"));
                                result = SearchResult_Verify_TextCol("--", "Check(s)");
                                break;
                            case "Paid":
                                Assert.IsFalse(w.ColValues_Contains(_AccoutingPage.SearchTableHeader.By, _AccoutingPage.SearchTable.By, "Pay Date(s)", "Expected", "--"));
                                Assert.IsFalse(SearchResult_Verify_TextCol("--", "Check(s)"));
                                result = true;
                                break;
                        }
                        break;
                    case "Invoice #":
                        result = SearchResult_Verify_TextCol(_Data.SearchVal, "Invoice #(s)");
                        break;
                    case "PRO #":
                        result = SearchResult_Verify_TextCol(_Data.SearchVal, "PRO #");
                        break;
                }
                return result;
            }
            catch
            {
                return false;
            }
        }

        public bool SearchResult_Verify_TextCol(string strValToCompare, string strVerifyColName)
        {
            try
            {
                Assert.IsTrue(_AccoutingPage.SearchTable.WaitUntilDisplayed());

                WebTable w = new WebTable(testConfig);
                
                //Verify the col value matches the given string
                if (strVerifyColName == "Invoice Date" || strVerifyColName == "Load Date")
                {
                    Assert.IsTrue(w.ColValues_Compare_ForDateRange(_AccoutingPage.SearchTableHeader.By, _AccoutingPage.SearchTable.By, strVerifyColName, _Data.FromDate, _Data.ToDate));
                }
                else
                {
                    Assert.IsTrue(w.ColValues_Compare(_AccoutingPage.SearchTableHeader.By, _AccoutingPage.SearchTable.By, strVerifyColName, strValToCompare));
                }

                w = null;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string FactoringColVerify()
        {
            try
            {
                Assert.IsTrue(VerifyColumn(_AccoutingPage.LoadNumberColumn, "Load #"));
                Assert.IsTrue(VerifyColumn(_AccoutingPage.RefNumberColumn, "Ref #"));
                Assert.IsTrue(VerifyColumn(_AccoutingPage.LoadDateColumn, "Load Date"));
                Assert.IsTrue(VerifyColumn(_AccoutingPage.CarrierNameColumn, "Carrier"));
                Assert.IsTrue(VerifyColumn(_AccoutingPage.ProColumn, "PRO #"));
                Assert.IsTrue(VerifyColumn(_AccoutingPage.AmountColumn, "Amount"));
                Assert.IsTrue(VerifyColumn(_AccoutingPage.InvoiceNumberColumn, "Invoice #(s)"));
                Assert.IsTrue(VerifyColumn(_AccoutingPage.PayDateColumn, "Pay Date(s)"));
                Assert.IsTrue(VerifyColumn(_AccoutingPage.CheckColumn, "Check(s)"));
                Assert.IsTrue(VerifyColumn(_AccoutingPage.InvoiceStatusColumn, "Invoice Status"));
                Assert.IsTrue(VerifyColumn(_AccoutingPage.BalanceColumn, "Balance"));
                return "FactoringColVerifySuccess";
            }
            catch
            {
                return "FactoringColVerifyFailed";
            }
        }

        private bool VerifyColumn(UIItem column, string columnName)
        {
            if (column.GetText() == columnName)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public string VerifyBasicSearch()
        {
            try
            {
                string strActualRes = null;
                strActualRes = SearchLoad();
                if (strActualRes == "SearchFailed")
                {
                    return "SearchFailed";
                }
                Assert.IsTrue(Verify(_Data.SearchType));
                return "VerifyBasicSearchSuccess";
            }
            catch
            {
                return "VerifyBasicSearchFailed";
            }
        }

        public string DefaultSearch()
        {
            try
            {
                Assert.IsTrue(NavAndOpenAdvnFilter());
                Assert.IsTrue(_AccoutingPage.ClearAllForCustomer.WaitUntilDisplayed());
                Assert.IsTrue(_AccoutingPage.ClearAllForCustomer.Click());
                Assert.IsTrue(_AccoutingPage.SearchTableHeader.WaitUntilDisplayed());
                if (!_AccoutingPage.SearchResult1stRow.WaitUntilDisplayed())
                    return "NoRowsDisplayed";
                return "DefaultSearchSuccess";
            }
            catch
            {
                return "DefaultSearchFailed";
            }
        }

        public string VerifyDefaultView()
        {
            try
            {
                Assert.IsTrue(_AccoutingPage.AccoutingTab.WaitUntilDisplayed());
                try
                {
                    Assert.IsTrue(_AccoutingPage.AccoutingTab.Click());
                }
                catch
                {
                    Assert.IsTrue(_AccoutingPage.AccoutingTab.FindAndClickUsingJS());
                }
                
                Assert.IsTrue(_AccoutingPage.SpecificNumberBtn.WaitUntilDisplayed());
                Assert.IsTrue(_AccoutingPage.AdvSearchNoFilterBtn.WaitUntilDisplayed());
                return "VerifyDefaultViewSuccess";
            }
            catch
            {
                return "VerifyDefaultViewSuccess";
            }
        }
    }
        
}