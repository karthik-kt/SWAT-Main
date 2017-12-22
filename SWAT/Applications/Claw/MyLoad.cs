using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Net;
using System.IO;
using SWAT.Data;
using SWAT.Logger;
using SWAT.FrameWork.Utilities;
using Config = SWAT.Configuration.TestStartInfo;
using SWAT.Applications.Claw.ObjectRepository;

namespace SWAT.Applications.Claw
{


    public class MyLoad : UIObjects
    {
        private DataManager testData;
        private By bySearchResHdr = By.CssSelector("#column-headers-container>tr>th");
        private By bySearchRes = By.CssSelector("#search-results-container>tr>td");
        private string strLoadBoard = "#loadboard";
        private string Arrivaldate = null;


        //Load Search
        private string SearchType = null;//testData.Data("SearchType");

        private string SearchVal = null;//testData.Data(SearchType);

        private string Dashboard_Pickup_Today_Count = null;
        private string Dashboard_Deliver_Today_Count = null;
        private string Dashboard_Pickup_Tomorrow_Count = null;
        private string Dashboard_Deliver_Tomorrow_Count = null;
        private string Dashboard_Pickup_ThisWeek_Count = null;
        private string Dashboard_Deliver_ThisWeek_Count = null;

        private By bySearchType = By.CssSelector("#basicSearchType");
        private By bySearchVal = By.CssSelector("#basic-search-number-parameter-input");
        private By bySearchButton = By.CssSelector("#load-search-button");
        private By bySearchRegion = By.CssSelector("#search-region");
       


        #region fields

        private By bySearchRow = By.CssSelector("#search-results-container>tr");

        // By bySearchResHdr = By.CssSelector("#column-headers-container>tr>th");
        // By bySearchRes = By.CssSelector("#search-results-container>tr>td");
        private By byTopResultString = By.CssSelector("#tags-bar-region>div>span>strong");

        private By byTableHdr = By.CssSelector("#column-headers-container>tr>th");

        //Common

        private By tcReference = By.CssSelector("#reference-number");
        private By tcProgress = By.CssSelector("#progress");
        private By tcMode = By.CssSelector("#mode");
        private By tcEquipmentType = By.CssSelector("#equipment-type");
        private By tcStops = By.CssSelector("#stop-count");
        private By tcPickupDate = By.CssSelector("#pickup-date");
        private By tcPickup = By.CssSelector("#first-pickup-number");
        private By tcShipper = By.CssSelector("#origin-name");
        private By tcShipperLocation = By.CssSelector("#origin-location");
        private By tcDeliveryDate = By.CssSelector("#delivery-date");
        private By tcDelivery = By.CssSelector("#last-delivery-number");
        private By tcConsignee = By.CssSelector("#destination-name");
        private By tcConsigneeLocation = By.CssSelector("#destination-location");

        //Carrier
        private By tcDriver = By.CssSelector("#driver-name");

        private By tcTrailer = By.CssSelector("#trailer-number");
        private By tcTractor = By.CssSelector("#tractor-number");
        private By tcNextCall = By.CssSelector("#next-call-back-time");

        //Customer
        private By tcCustomer = By.CssSelector("#customer-name");

        private By tcRequestedShipDate = By.CssSelector("#requested-ship-date");
        private By tcRequestedArrivalDate = By.CssSelector("#requested-arrival-date");
        private By tcShipmentType = By.CssSelector("#shipment-type");
        private By tcCarrier = By.CssSelector("#carrier-name");
        private By tcRanking = By.CssSelector("#ranking");

        //Update pickup date 

        private By stoptab = By.XPath((".//*[@id='stops-button']"));
        private By updatebtn = By.XPath((".//[@id='stops-region']/div/div[1]/section/header/button"));
        private By arrivaldate = By.CssSelector("#dp1439285773933");
        private By arrivaltime = By.CssSelector(".hook--carrier-arrival-time.text-input.width--delta");
        private By savebtn = By.CssSelector(".button.button--loud.hook--save-update");

        private By byTotalResults = By.CssSelector(".hook--ofTotal");
        private By byCountResults = By.CssSelector(".hook--total");

        #endregion fields

        private string _entityname = "Entity Name";

        private Page _page;
        private LoadDetailsPage _LoadDetailPage;
        private DashboardPage _DashboardPage;

        public MyLoad(Config c, DataManager t)
            : base(c)
        {
            testConfig = c;
            driver = testConfig.Driver;
            testData = t;
            SearchType = testData.Data("SearchType");
            SearchVal = testData.Data(SearchType);
            _entityname = testData.Data(_entityname);
            _page = new Page(c.Driver);
            _LoadDetailPage = new LoadDetailsPage(c);
            _DashboardPage = new DashboardPage(c);
            Dashboard_Pickup_Today_Count = testData.Data("Dashboard_Pickup_Today_Count");
            Dashboard_Deliver_Today_Count = testData.Data("Dashboard_Deliver_Today_Count");
            Dashboard_Pickup_Tomorrow_Count = testData.Data("Dashboard_Pickup_Tomorrow_Count");
            Dashboard_Deliver_Tomorrow_Count = testData.Data("Dashboard_Deliver_Tomorrow_Count");
            Dashboard_Pickup_ThisWeek_Count = testData.Data("Dashboard_Pickup_ThisWeek_Count");
            Dashboard_Deliver_ThisWeek_Count = testData.Data("Dashboard_Deliver_ThisWeek_Count");

        }

        // Update Pickup Stop Arrival date and time
        public string Pickuparrival_Edit()
        {
            ClickOnUpdate();
            Updatefield();
            Savethedetails();

            return ("");
        }

        public bool ClickOnUpdate()
        {
            try
            {
                Click(stoptab);
                Click(updatebtn);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Updatefield()
        {
            try
            {
                //Click on textbox so that datepicker will come
                Click(arrivaldate);
                SetDate(arrivaldate, Arrivaldate);
                Click(arrivaltime);
                driver.FindElement(By.CssSelector(".hook--carrier-arrival-time.text-input.width--delta")).SendKeys("22:00");
                //Edit
               
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Savethedetails()
        {
            try
            {
                Click(savebtn);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Update Delivery Stop Arrival date and time
        public string deliveryarrival_edit()
        {
            return "Not Implemented";
        }

        #region Load Search

        public string Search()
        {
            try
            {
                if(SearchVal != "!IGNORE!")
                {
                    Navigate(strLoadBoard);
                    WaitUtilDisplayed(bySearchRegion, 60);
                    SelectByText(bySearchType, SearchType);
                    MyLogger.Log("Search using =[" + SearchType + "]");

                    Clear(bySearchVal);
                    Edit(bySearchVal, SearchVal);
                    Edit(bySearchVal, Keys.Tab);
                    MyLogger.Log("Search Value =[" + SearchVal + "]");

                    //Thread.Sleep(Constants.intShortWait);
                    try { Edit(bySearchVal, Keys.Enter); }
                    catch { Click(bySearchButton); }

                    //Thread.Sleep(Constants.intMedWait);
                    //Search result table displayed.
                    WaitUtilDisplayed(bySearchResHdr);
                    if (_page.NoResultsFound())
                        return "NoResultsFound";
                    return "SearchSuccess";
                }
                else
                {
                    MyLogger.Log("Ignoring the search as there is no data input given to search");
                    return "SearchFailed";
                }


            }
            catch
            {
                return "SearchFailed";
            }
        }

        public string ValidateResult()
        {
            // DataTable ds = Import_ExcelWorkSheet(strSheetName);
            string TableName = testData.Data("TableName");
            string ColName = testData.Data("ColName");
            string Validation = testData.Data("Validation");
            string Value1 = testData.Data("Value1");
            string Value2 = testData.Data("Value2");
            bool check = false;
            WebTable w = new WebTable(testConfig);
            check = DateRangeCheckInList(Value1, Value2, w.ColValues_DatesToList(bySearchResHdr, bySearchRes, ColName));
            w = null;
            return "ResultsMatching";
        }

        //Search and open the first row on the load result page.
        //expectation is search result only one row.
        public string SearchAndOpenLoad()
        {
            try
            {
                string strActualResult;

                //search for the load
                strActualResult = Search();
                if (strActualResult == "SearchSuccess")
                {
                    strActualResult = OpenFromSearchResult();
                }
                return strActualResult;
            }
            catch
            {
                return "LoadOpenedFailed";
            }
        }

        public string AdvancedSearch()
        {
            try
            {
                string date = testData.Data("Date");                
                string fromdate = testData.Data("From_Date");
                string todate = testData.Data("To_Date");
                string commodity = testData.Data("Commodity");
                string tractor = testData.Data("Tractor");
                string progress = testData.Data("Progress");
                string mode = testData.Data("Mode");
                string carrier = testData.Data("Carrier");
                string customer = testData.Data("Customer");
                string facility = testData.Data("Facility");
                string highpriorityonly = testData.Data("HighPriorityOnly");

                By _date = By.XPath(".//*[@id='date-range-inputs-container']/select");//testData.Data("Date");
                By _fromdate = By.CssSelector("#search-from-date");//testData.Data("From_Date");
                By _todate = By.CssSelector("#search-to-date");//testData.Data("To_Date");
                By _commodity = By.CssSelector("#search-commodity");//testData.Data("Commodity");
                By _tractor = By.CssSelector("#search-trailer");//testData.Data("Tractor");
                By _progress = By.CssSelector("#search-progress");//testData.Data("Progress");
                By _mode = By.CssSelector("#search-mode");//testData.Data("Mode");
                By _carrier = By.CssSelector("#search-carrier-name");//testData.Data("Carrier");
                By _customer = By.CssSelector("#search-customer-name");//testData.Data("Customer");
                By _facility = By.CssSelector("#search-facility-location-name");//testData.Data("Facility");
                By _highpriorityonly = By.CssSelector("#high-priority-commodities");//testData.Data("HighPriorityOnly");

                By _search = By.CssSelector("#advanced-search-submit-button");
                By _searchregion = By.CssSelector("#search-region");
                By _searchinput = By.CssSelector("#basic-search-number-parameter-input");
                By _advancedsearch = By.Id("advanced-search-submit-button");
                By _advancedsearchpopup = By.CssSelector("#advancesearch-popup-trigger");
                By _searchresultrow = By.XPath(".//*[@id='search-results-container']/tr");

                
                Navigate("#loadboard");
                WaitUtilDisplayed(_searchregion,60);
                int intTry = 2;
                while (!IsEnabled(_searchinput) && intTry == 2)
                {
                    Navigate("#loadboard");
                    intTry--;
                }
                Click(_advancedsearchpopup);
                SelectByText(_date, date);
                //SetDate(_fromdate, fromdate);
                //SetDate(_todate, todate);
                ClearAndEdit(_fromdate, fromdate);
                ClearAndEdit(_todate, todate);
                ClearAndEdit(_commodity, commodity);
                ClearAndEdit(_tractor, tractor);
                SelectByText(_progress, progress);
                SelectByText(_mode, mode);
                ClearAndEdit(_carrier, carrier);
                ClearAndEdit(_customer, customer);
                ClearAndEdit(_facility, facility);
                if(highpriorityonly == "YES")
                {
                    Click(_highpriorityonly);
                }
                Click(_advancedsearch);
                WaitUntilLoading();
                WaitUtilDisplayed(_searchresultrow);
                return "AdvSearchSuccess";
            }
            catch
            {
                return "AdvSearchFailed";
            }
        }
        public string OpenFromSearchResult()
        {
            try
            {
                string actualresult = null;
                string strObjFirstRow = ".//*[@id='search-results-container']/tr";

                Assert.IsTrue(WaitUtilDisplayed(By.XPath(strObjFirstRow)));
                Assert.IsTrue(Click(By.CssSelector(".nowrap>a")));

                Assert.IsTrue(_LoadDetailPage.OptionBtn.WaitUntilDisplayed());
                if (_LoadDetailPage.OptionBtn.IsDisplayed())
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

        public string SearchLoadFromTopNav()
        {
            string actualresult = null;
            string searchType = testData.Data("SearchType");
            string searchVal = testData.Data(searchType);
            By byLoadSearchInput = By.CssSelector("#load-search-input");
            try
            {
                ClearAndEdit(byLoadSearchInput, searchVal);
                MyLogger.Log("Entered value "+searchVal+" in load search input");
                Edit(byLoadSearchInput, Keys.Enter);
                Assert.IsTrue(_LoadDetailPage.OptionBtn.WaitUntilDisplayed());
                if (_LoadDetailPage.OptionBtn.IsDisplayed())
                    actualresult = "SearchLoadSuccess";
                else
                    actualresult = "SeacrhLoadFailed";
                return actualresult;
            }
            catch
            {
                return "SearchLoadFailed";
            }
        }
        #endregion Load Search

        #region Data Setup Get Load Details

        public string GetLoadDetails(DataManager testData)
        {
            By bytogglecheckboxes = By.ClassName("hook--select-column-checkbox");
            
            By bytogglebutton = By.CssSelector(".button.flyout__title.fr.hook--popup-trigger");            
            try
            {
                WaitUntilLoading();
                Assert.IsTrue(AtleastARowDisplayed());
                Open_ToggleCol();
                List<bool> checkboxesstatuses = GetCheckBoxesStatus(bytogglecheckboxes);
                SelectOnly_LoadDetailCol();
                SaveAndClose_ToggleCol();
                GetLoadDetails_FromSearchResult(testData);
                Open_ToggleCol();
                Assert.IsTrue(SetCheckBoxesStatus(bytogglecheckboxes, checkboxesstatuses));
                SaveAndClose_ToggleCol();

                return "DataCopied";                
            }
            catch
            {
                return "Failed";
            }
        }

        private bool AtleastARowDisplayed()
        {
            By by1stRow = By.XPath(".//*[@id='search-results-container']/tr[1]/td[1]");
            try
            {
                Assert.IsTrue(WaitUtilDisplayed(by1stRow, 10));
                return true;
            }
            catch
            {
                MyLogger.Log("No result displayed under search result");
                return false;
            }
        }
        
        private List<bool> GetCheckBoxesStatus(By byCheckboxes)
        {            
            Thread.Sleep(Constants.Wait_Short);
            List<bool> checkboxstatus = new List<bool>();
            List<IWebElement> elements = driver.FindElements(byCheckboxes).ToList<IWebElement>();
            foreach (IWebElement element in elements)
            {
                checkboxstatus.Add(element.Selected);
            }
            return checkboxstatus;
        }

        private bool SetCheckBoxesStatus(By bycheckboxes, List<bool> statuses)
        {
            try
            {
                List<IWebElement> elements = driver.FindElements(bycheckboxes).ToList<IWebElement>();

                if (elements.Count != statuses.Count)
                {
                    return false;
                }
                int iLoop = 0;
                foreach (var element in elements)
                {
                    if (element.Selected != statuses[iLoop])
                    {
                        element.Click();
                    }
                    iLoop++;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void SelectOnly_LoadDetailCol()
        {
            WaitUtilDisplayed(By.XPath(".//*[@id='customize-columns-form']/button"));
            List<IWebElement> cols = driver.FindElements(By.ClassName("hook--select-column-checkbox")).ToList<IWebElement>();
            foreach (IWebElement col in cols)
            {
                if (col.Selected)
                {
                    col.Click();
                }
            }
            driver.FindElement(tcReference).Click();
            driver.FindElement(tcDelivery).Click();
            driver.FindElement(tcPickup).Click();
        }

        private void GetLoadDetails_FromSearchResult(DataManager testData)
        {
            Assert.IsTrue(WaitUtilDisplayed(By.XPath(".//*[@id='search-results-container']/tr/td"), 60));
            List<IWebElement> TblElements = driver.FindElements(By.XPath(".//*[@id='search-results-container']/tr/td")).ToList<IWebElement>();
            List<IWebElement> RowElements = driver.FindElements(By.XPath(".//*[@id='search-results-container']/tr")).ToList<IWebElement>();
            int iLoop = 1;
            string strRef = Constants.Option_Ignore;
            string strLoad = Constants.Option_Ignore;
            string strPick = Constants.Option_Ignore;
            string strDelivery = Constants.Option_Ignore;
            // bool blGotIt = false;
            try
            {
                //Pull the date from search result table.
                foreach (IWebElement Row in RowElements)
                {
                    strRef = driver.FindElement(By.XPath(".//*[@id='search-results-container']/tr[" + iLoop + "]/td[1]")).Text;
                    strLoad = driver.FindElement(By.XPath(".//*[@id='search-results-container']/tr[" + iLoop + "]/td[2]")).Text;
                    strPick = driver.FindElement(By.XPath(".//*[@id='search-results-container']/tr[" + iLoop + "]/td[4]")).Text;
                    strDelivery = driver.FindElement(By.XPath(".//*[@id='search-results-container']/tr[" + iLoop + "]/td[5]")).Text;
                    if (strRef != "--" && strLoad != "--" && strPick != "--" && strDelivery != "--")
                    {
                        if (strRef != "" && strLoad != "" && strPick != "" && strDelivery != "")
                        {
                            // blGotIt = true;
                            break;
                        }
                    }
                    iLoop++;
                }
                //Update the data in data table
                testData.SetData("Reference #", strRef);
                testData.SetData("Load #", strLoad);
                testData.SetData("Pickup #", strPick);
                testData.SetData("Delivery #", strDelivery);
            }
            catch
            {
            }
        }

        #endregion Data Setup Get Load Details

        #region functions for column selection and verifications.

        private Dictionary<string, bool> listOfColHeaders(string entityName)
        {
            Dictionary<string, bool> columnheader = new Dictionary<string, bool>();
            //Customer
            if (entityName.ToUpper() == "CUSTOMER")
            {
                columnheader.Add("CUSTOMER", false);
                columnheader.Add("REFERENCE #", false);
                columnheader.Add("LOAD #", false);
                columnheader.Add("PROGRESS", false);
                columnheader.Add("MODE", false);
                columnheader.Add("EQPT", false);
                columnheader.Add("STOPS", false);
                columnheader.Add("PICKUP DATE", false);
                columnheader.Add("REQUESTED SHIP DATE", false);
                columnheader.Add("PICK #", false);
                columnheader.Add("SHIPPER", false);
                columnheader.Add("SHIPPER LOCATION", false);
                columnheader.Add("DELIVERY DATE", false);
                columnheader.Add("REQUESTED ARRIVAL DATE", false);
                columnheader.Add("DELIVERY #", false);
                columnheader.Add("CONSIGNEE", false);
                columnheader.Add("CONSIGNEE LOCATION", false);
                columnheader.Add("SHIPMENT TYPE", false);
                columnheader.Add("CARRIER", false);
                columnheader.Add("RANKING", false);
                columnheader.Add("BOL", false);
            }
            //Carrier
            if (entityName.ToUpper() == "CARRIER")
            {
                columnheader.Add("REFERENCE #", false);
                columnheader.Add("LOAD #", false);
                columnheader.Add("PROGRESS", false);
                columnheader.Add("MODE", false);
                columnheader.Add("EQPT", false);
                columnheader.Add("STOPS", false);
                columnheader.Add("PICKUP DATE", false);
                columnheader.Add("PICKUP #", false);
                columnheader.Add("SHIPPER", false);
                columnheader.Add("SHIPPER LOCATION", false);
                columnheader.Add("DELIVERY DATE", false);
                columnheader.Add("DELIVERY #", false);
                columnheader.Add("CONSIGNEE", false);
                columnheader.Add("CONSIGNEE LOCATION", false);
                columnheader.Add("DRIVER", false);
                columnheader.Add("TRAILER #", false);
                columnheader.Add("TRACTOR #", false);
                columnheader.Add("NEXT CALL BACK TIME", false);
                columnheader.Add("BOL", false);
            }
            return columnheader;
        }

        private Dictionary<string, bool> DisplayedColHeaders()
        {
            By byDisplayedColumn = By.CssSelector("#column-headers-container>tr>th");
            Dictionary<string, bool> headersDic = listOfColHeaders(_entityname);
            try
            {
                List<IWebElement> hrdElements = driver.FindElements(byDisplayedColumn).ToList();
                foreach (IWebElement hrdElement in hrdElements)
                {
                    headersDic[hrdElement.Text.ToString().ToUpper()] = true;
                }
                return headersDic;
            }
            catch (Exception)
            {
                return headersDic;
            }
        }

        private Dictionary<string, By> objOfHeaders(string entityName)
        {
            Dictionary<string, By> headersDic = new Dictionary<string, By>();
            //Customer
            if (entityName.ToUpper() == "CUSTOMER")
            {
                headersDic.Add("REFERENCE #", By.CssSelector("#reference-number"));
                headersDic.Add("PROGRESS", By.CssSelector("#progress"));
                headersDic.Add("MODE", By.CssSelector("#mode"));
                headersDic.Add("EQUIPMENT TYPE", By.CssSelector("#equipment-type"));
                headersDic.Add("STOPS", By.CssSelector("#stop-count"));
                headersDic.Add("PICKUP DATE", By.CssSelector("#pickup-date"));
                headersDic.Add("SHIPPER", By.CssSelector("#origin-name"));
                headersDic.Add("SHIPPER LOCATION", By.CssSelector("#origin-location"));
                headersDic.Add("DELIVERY DATE", By.CssSelector("#delivery-date"));
                headersDic.Add("DELIVERY #", By.CssSelector("#last-delivery-number"));
                headersDic.Add("CONSIGNEE", By.CssSelector("#destination-name"));
                headersDic.Add("CONSIGNEE LOCATION", By.CssSelector("#destination-location"));
                headersDic.Add("PICKUP #", By.CssSelector("#first-pickup-number"));
                headersDic.Add("CUSTOMER", By.CssSelector("#customer-name"));
                headersDic.Add("REQUESTED SHIP DATE", By.CssSelector("#requested-ship-date"));
                headersDic.Add("REQUESTED ARRIVAL DATE", By.CssSelector("#requested-arrival-date"));
                headersDic.Add("SHIPMENT TYPE", By.CssSelector("#shipment-type"));
                headersDic.Add("CARRIER", By.CssSelector("#carrier-name"));
                headersDic.Add("RANKING", By.CssSelector("#ranking"));
            }
            //Carrier
            if (entityName.ToUpper() == "CARRIER")
            {
                headersDic.Add("REFERENCE #", By.CssSelector("#reference-number"));
                headersDic.Add("PROGRESS", By.CssSelector("#progress"));
                headersDic.Add("MODE", By.CssSelector("#mode"));
                headersDic.Add("EQUIPMENT TYPE", By.CssSelector("#equipment-type"));
                headersDic.Add("STOPS", By.CssSelector("#stop-count"));
                headersDic.Add("PICKUP DATE", By.CssSelector("#pickup-date"));
                headersDic.Add("SHIPPER", By.CssSelector("#origin-name"));
                headersDic.Add("SHIPPER LOCATION", By.CssSelector("#origin-location"));
                headersDic.Add("DELIVERY DATE", By.CssSelector("#delivery-date"));
                headersDic.Add("DELIVERY #", By.CssSelector("#last-delivery-number"));
                headersDic.Add("CONSIGNEE", By.CssSelector("#destination-name"));
                headersDic.Add("CONSIGNEE LOCATION", By.CssSelector("#destination-location"));
                headersDic.Add("PICKUP #", By.CssSelector("#first-pickup-number"));
                headersDic.Add("DRIVER", By.CssSelector("#driver-name"));
                headersDic.Add("TRAILER #", By.CssSelector("#trailer-number"));
                headersDic.Add("TRACTOR #", By.CssSelector("#tractor-number"));
                headersDic.Add("NEXT CALL", By.CssSelector("#next-call-back-time"));
            }
            return headersDic;
        }

        public string ToggleColumn()
        {
            Dictionary<string, By> columnheaders = objOfHeaders(_entityname);
            Dictionary<string, string> expectedcolumnheader = testData.ToDictionary();
            try
            {
                expectedcolumnheader.Remove("Entity Name");
                expectedcolumnheader.Remove("LOAD_#");
                expectedcolumnheader.Remove("BOL");
                expectedcolumnheader.Remove("ID");
            }
            catch
            {
            }
            bool isallpass = true;
            Open_ToggleCol();
            try
            {
                foreach (KeyValuePair<string, string> kvp in expectedcolumnheader)
                {
                    if (!Check(columnheaders[kvp.Key.Replace('_', ' ')], kvp.Value))
                        isallpass = false;
                }
                Save_ToggleCol();
                Close_ToggleCol();
                if (isallpass)
                    return "SelectionSuccess";
                else
                    return "SelectionFailed";
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex.Message, GetCurrentMethod());
                return "SelectionFailed";
            }
        }

        private bool Open_ToggleCol()
        {
            try
            {
                if (!WaitUtilDisplayed(By.CssSelector("#reference-number"), 5))
                {
                    Click(By.CssSelector("#column-settings-button"));
                    Assert.IsTrue(WaitUtilDisplayed(By.CssSelector("#reference-number"), 5));
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool SaveAndClose_ToggleCol()
        {
            try
            {
                Assert.IsTrue(Save_ToggleCol());
                Assert.IsTrue(Close_ToggleCol());
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool Close_ToggleCol()
        {
            try
            {
                Click(By.CssSelector("#search-region"));
                Assert.IsFalse(WaitUtilDisplayed(By.CssSelector("#reference-number"), 5));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Save_ToggleCol()
        {
            try
            {
                Click(By.XPath(".//*[@id='customize-columns-form']/button"));
                Assert.IsTrue(WaitUntilLoading());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private Dictionary<string, string> AdjustDic(Dictionary<string, string> dic)
        {
            if (dic.ContainsKey("Entity Name"))
            {
                dic.Remove("Entity Name");
            }
            if (dic.ContainsKey("EQUIPMENT_TYPE"))
            {
                dic.Add("EQPT", dic["EQUIPMENT_TYPE"]);
                dic.Remove("EQUIPMENT_TYPE");
            }
            return dic;
        }

        public string DisplayedColVerification()
        {
            bool isPassed = true;
            try
            {
                Dictionary<string, bool> displayedcols = DisplayedColHeaders();
                Dictionary<string, string> expectedcols = testData.ToDictionary();
                expectedcols = AdjustDic(expectedcols);
                foreach (KeyValuePair<string, string> kvp in expectedcols)
                {
                    if (kvp.Value == "!COL_DISPLAYED!")
                    {
                        if (displayedcols[kvp.Key.Replace('_', ' ')])// Column is displayed and expected to be displayed.
                            continue;
                        else
                        {
                            MyLogger.Log("Column <" + kvp.Key + ">is not displayed.");
                            isPassed = false;
                        }
                    }
                    else
                    {
                        if (!displayedcols[kvp.Key])// Column is not displayed and expected not to be displayed.
                            continue;
                        else
                        {
                            MyLogger.Log("Column <" + kvp.Key + ">is displayed.");
                            isPassed = false;
                        }
                    }
                }
                if (isPassed) return "VerificationSuccess"; // one of the col verification failed.
                else return "VerificationFailed";
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex.Message, GetCurrentMethod());
                return "VerificationFailed";
            }
        }

        public Dictionary<By, string> MatchObjectWithExcelValues(Dictionary<By, string> UIObjects, Dictionary<string, string> ExcelVals)
        {
            foreach (var ExcelVal in ExcelVals)
            {
                for (int UIObjectCount = 0; UIObjects.Count > UIObjectCount; UIObjectCount++)
                {
                    if (ExcelVal.Key == UIObjects[UIObjects.ElementAt(UIObjectCount).Key])
                    {
                        UIObjects[UIObjects.ElementAt(UIObjectCount).Key] = ExcelVal.Value;
                    }
                }
            }
            return UIObjects;
        }

        #endregion functions for column selection and verifications.

        #region Download Files
        internal string DownloadBOL()
        {
            try
            {
                By Result1stBOL = By.CssSelector(".text-link.hook--tooltip-trigger.hook--bol-report"); //By.XPath(".//*[@id='search-results-container']/tr[1]/td[21]/button");
                Assert.IsTrue(WaitUtilDisplayed(Result1stBOL));
                Assert.IsTrue(Click(Result1stBOL));
                return "DownloadSuccess";
            }
            catch
            {
                return "DownloadFailed";
            }
        }

        private bool VerifyFile()
        {
            try
            {
                var directoryInfo = new DirectoryInfo(@"C:\Users\manivas.murugaiah\Downloads");  
                foreach (var file in directoryInfo.GetFiles())
                {
                    string filename = file.Name;
                    filename = filename.Replace(".xls", "");
                    filename = filename.Replace("TestSuite_", "");
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal string DownloadPOD()
        {
            try
            {
                NavigateToDocuments();
                WebClient myWebClient = new WebClient();
                string myStringWebResource = GetUrlOfDownloadable();
                string filename = "clawtesting.pdf";
                
                //Console.WriteLine("Successfully Downloaded File \"{0}\" from \"{1}\"", fileName, myStringWebResource);
                try
                {
                    MyLogger.Log(string.Format("Downloading File \"{0}\" from \"{1}\" .......\n\n", filename, myStringWebResource));
                    myWebClient.DownloadFile(myStringWebResource, @"C:\temp\" + filename);
                    MyLogger.Log("\nDownloaded file saved in the following file system folder:\n\t"+ @"C:\temp\" + filename);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Execption " + ex.Message);
                }
                return "DownloadSuccess";
            }
            catch
            {
                return "DownloadFailed";
            }
        }

        private bool NavigateToDocuments()
        {
            try
            {
                By documenttab = By.XPath(".//*[@id='documents-container']/a");
                By document1st = By.XPath(".//*[@id='document-list-container']/tr/td[1]/button");
                WaitUtilDisplayed(documenttab,10);
                Click(documenttab);
                WaitUtilDisplayed(document1st, 30);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        private string GetUrlOfDownloadable()
        {
            try
            {                
                string baseUrl = @"https://connect-test.coyote.com/";
                string documentUrl = @"Coyote.Client.WebApi/v1/documents/";
                string documentID = null;
                try
                {
                    documentID = driver.FindElement(By.XPath(".//*[@id='document-list-container']/tr[1]/td[1]/button")).GetAttribute("docid");
                }
                catch
                {
                    documentID = null;
                }
                string accesskey = @"?";
                string cookie = "CoyoteAccess=" + driver.Manage().Cookies.AllCookies[0].Value;
                foreach(var cookie1 in driver.Manage().Cookies.AllCookies)
                {
                   if( cookie1.Name == "CoyoteAccess")
                    {
                        cookie = cookie1.Value.ToString();
                        break;
                    }
                }
                string remoteUri = baseUrl + documentUrl + documentID + accesskey + "CoyoteAccess=" + cookie;
                return remoteUri;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        public string VerifySearchResult()
        {
            try
            {
                string searchType = testData.Data("SearchType");
                string searchVal = testData.Data(searchType);
                Assert.IsTrue(SearchResult_Verify_TextCol(searchVal, searchType));
                return "VerifySearchResultSuccess";
            }
            catch
            {
                return "VerifySearchResultFailed";
            }
        }

        public bool SearchResult_Verify_TextCol(string strValToCompare, string strVerifyColName)
        {
            try
            {

                WebTable w = new WebTable(testConfig);

                if (strVerifyColName == "Pickup Date" || strVerifyColName == "Delivery Date")
                {
                    string fromdate = testData.Data("From_Date");
                    string todate = testData.Data("To_Date");
                    Assert.IsTrue(w.ColValues_Compare_ForDateRange(bySearchResHdr, bySearchRes, strVerifyColName, fromdate, todate));
                }
                else
                {
                    Assert.IsTrue(w.ColValues_Compare(bySearchResHdr, bySearchRes, strVerifyColName, strValToCompare));
                }
                w = null;

                //action successs
                return true;
            }
            catch
            {
                //action falied
                return false;
            }
        }

        public string UIVerify()
        {
            
            try
            {
                Assert.IsTrue(VerifyMyLoadsCount(_DashboardPage.Picking_Up_Today, Dashboard_Pickup_Today_Count));
                Assert.IsTrue(VerifyMyLoadsCount(_DashboardPage.Delivering_Today, Dashboard_Deliver_Today_Count));
                Assert.IsTrue(VerifyMyLoadsCount(_DashboardPage.Picking_Up_This_Week, Dashboard_Pickup_ThisWeek_Count));
                Assert.IsTrue(VerifyMyLoadsCount(_DashboardPage.Delivering_This_Week, Dashboard_Deliver_ThisWeek_Count));

                if (_entityname != Constants.Entity_Carrier)
                {
                    Assert.IsTrue(VerifyMyLoadsCount(_DashboardPage.Picking_Up_Tomorrow, Dashboard_Pickup_Tomorrow_Count));
                    Assert.IsTrue(VerifyMyLoadsCount(_DashboardPage.Delivering_Tomorrow, Dashboard_Deliver_Tomorrow_Count));
                }

                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
        }

        private bool VerifyMyLoadsCount(UIItem item, string count)
        {
            try
            {
                Assert.IsTrue(item.Click());
                _DashboardPage.My_Loads_Search_Results_Headers.WaitUntilDisplayed(30);

                UIItem TotalResults = new UIItem("MyLoads>> Total results>>", byTotalResults, driver);
                UIItem CountResults = new UIItem("MyLoads>> Total results>>", byCountResults, driver);

                if (TotalResults.IsDisplayed())
                {
                    Assert.IsTrue(TotalResults.UIVerify("HasText." + count));
                }
                else
                {
                    Assert.IsTrue(CountResults.UIVerify("HasText." + count));
                }

                if (_entityname == Constants.Entity_Carrier)
                {
                    MyTasksPage objToNavigate = new MyTasksPage(this.testConfig);
                    objToNavigate.Navigate();
                }
                else
                {
                    CLAW objToNavigate = new CLAW(testConfig, testData);
                    objToNavigate.NavigateTo("HOME");
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}