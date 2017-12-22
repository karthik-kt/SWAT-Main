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


namespace SWAT.Applications.Claw.ObjectRepository
{
    using OpenQA.Selenium;
    using SWAT.FrameWork.Utilities;
    using System.Collections.Generic;
    using SWAT.Configuration;


    public class AccountingPage : Page
    {
        public AccountingPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public AccountingPage(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
        }
        private By tablehdr = By.CssSelector(".table-wrapper>table>thead>tr>th");
        private By table = By.CssSelector("#loaddetails-results-container>tr>td");
        private By accoutingtab = By.LinkText("Accounting");
        private By balanceamt = By.CssSelector(".stat__value");
        //Advn Search
        private By advfliter = By.CssSelector("#advancesearch-popup-trigger");
        private By within30days = By.LinkText("Within 30 days");
        private By fromdate = By.CssSelector("#start-date-range");
        private By todate = By.CssSelector("#end-date-range");
        private By advsearchbtn = By.CssSelector("#advanced-search-button");
        private By daterange = By.CssSelector("#date-range-select");
        private By allloads = By.CssSelector("#all-loads");
        private By onlyinvoiced = By.CssSelector("#only-invoiced");
        private By onlyuninvoiced = By.CssSelector("#only-uninvoiced");
        private By searchtype = By.CssSelector("#loadsearch-dropdown");
        private By searchval = By.CssSelector("#loadsearch-input");
        private By searchbutton = By.CssSelector("#load-search-button");
        private By searchregion = By.CssSelector("#search-region");
        private By searchresult1strow = By.CssSelector("#loaddetails-results-container>tr>td>a");
        private By searchresult1strowInvoice = By.CssSelector("#loaddetails-results-container>tr>td:nth-child(7)>div");
        private By searchResult1stRowPayDate = By.CssSelector("#loaddetails-results-container > tr:nth-child(1) > td:nth-child(8) > div");
        private By invoicestatus = By.CssSelector("#invoice-status");
        private By clearall = By.CssSelector("#clear-all");
        private By clearallforcustomer = By.CssSelector("#hook-clearall-popup"); 
        //Factoring company has below fields
        private By carriername = By.CssSelector("#carrier-search-field");
        private By loadnumbercolumn = By.XPath(".//*[@id='column-headers-container']/tr/th[1]");
        private By refnumbercolumn = By.XPath(".//*[@id='column-headers-container']/tr/th[2]");
        private By loaddatecolumn = By.XPath(".//*[@id='column-headers-container']/tr/th[3]/button");
        private By carriernamecolumn = By.XPath(".//*[@id='column-headers-container']/tr/th[4]");
        private By procolumn = By.XPath(".//*[@id='column-headers-container']/tr/th[5]");
        private By amountcolumn = By.XPath(".//*[@id='column-headers-container']/tr/th[6]");
        private By invoicenumbercolumn = By.XPath(".//*[@id='column-headers-container']/tr/th[7]");
        private By paydatecolumn = By.XPath(".//*[@id='column-headers-container']/tr/th[8]");
        private By checkcolumn = By.XPath(".//*[@id='column-headers-container']/tr/th[9]");
        private By invoicestatuscolumn = By.XPath(".//*[@id='column-headers-container']/tr/th[10]/button");
        private By balancecolumn = By.XPath(".//*[@id='column-headers-container']/tr/th[11]");

        private By loaddate1strow = By.XPath(".//*[@id='loaddetails-results-container']/tr[1]/td[3]");
        private By carriername1strow = By.XPath(".//*[@id='loaddetails-results-container']/tr[1]/td[4]");
        private By invoicestatus1strow = By.XPath(".//*[@id='loaddetails-results-container']/tr[1]/td[10]");
        private By searchresultscontainer = By.CssSelector("#loaddetails-results-container");
        private By searchloadsresultrows = By.CssSelector("#loaddetails-results-container>tr");
        private By invoicestatuses = By.CssSelector("#invoice-status");
        private By basicsearchregion = By.CssSelector("#basic-search");
        private By specificnumberbtn = By.CssSelector("#specific-number-button");
        private By advsearchnofilterbtn = By.CssSelector("#advanced-search-popup-trigger-no-filter-button");
        private By basicSearchButton = By.CssSelector("#basic-search-button");

        public UIItem BasicSearchButton { get { return new UIItem("Accounting page>> Basic Search Button", basicSearchButton, _driver); } }
        public UIItem Destination_Col_Carrier { get { return new UIItem("Accounting page>> SearchResult>> Destination_Col", By.CssSelector("#loaddetails-results-container>tr>td:nth-child(4)"), _driver); } }
        public UIItem Destination_Col_Customer { get { return new UIItem("Accounting page>> SearchResult>> Destination_Col", By.CssSelector("#loaddetails-results-container>tr>td:nth-child(5)"), _driver); } }

        public UIItem Destination_Col_Hdr_Carrier { get { return new UIItem("Accounting page>> SearchResult>> Destination_Col", By.XPath("//button[@value='Destination']"), _driver); } }
        public UIItem Destination_Col_Hdr_Customer { get { return new UIItem("Accounting page>> SearchResult>> Destination_Col", By.XPath("//button[@value='Destination']"), _driver); } }

        public UIItem SearchResult1stRowPayDate { get { return new UIItem("Accounting page>> SearchResult>> Pay date", this.searchResult1stRowPayDate, _driver); } }
        public UIItem CarrierName { get { return new UIItem("Accounting page>> Adv Search>> Carrier Name", this.carriername, _driver); } }
        public UIItem CarrierName_IntelliSense { get { return new UIItem("Accounting page>> Adv Search>> Carrier Name", By.CssSelector(".box--white.box--border-bottom.ui-menu-item"), _driver); } }
        public UIItem SearchTableHeader { get { return new UIItem("Accounting page>> Search Table", this.tablehdr,_driver); } }
        public UIItem SearchTable { get { return new UIItem("Accounting  page>> Search table", this.table,_driver); } }
        public UIItem AccoutingTab { get { return new UIItem("Accounting  page>> Accounting tab", this.accoutingtab,_driver); } }
        public UIItem BalanceAmt { get { return new UIItem("Accounting  page>> Balance amount text", this.balanceamt,_driver); } }
        public UIItem AdvFliter { get { return new UIItem("Accounting  page>> AdvFilter", this.advfliter,_driver); } }
        public UIItem Within30Days { get { return new UIItem("Accounting  page>> AdvFilter>> Within30dayslink", this.within30days,_driver); } }
        public UIItem FromDate { get { return new UIItem("Accounting  page>> AdvFilter>> From Date", this.fromdate,_driver); } }
        public UIItem ToDate { get { return new UIItem("Accounting  page>> AdvFilter>> To Date", this.todate,_driver); } }
        public UIItem AdvSearchBtn { get { return new UIItem("Accounting  page>> AdvFilter>> Search Button", this.advsearchbtn,_driver); } }
        public UIItem DateRange { get { return new UIItem("Accounting  page>> AdvFilter>> DateRange", this.daterange,_driver); } }
        public UIItem AllLoads { get { return new UIItem("Accounting  page>> AdvFilter>> AllLoad link", this.allloads,_driver); } }
        public UIItem OnlyInvoiced { get { return new UIItem("Accounting  page>> AdvFilter>> OnlyInvoices link", this.onlyinvoiced,_driver); } }
        public UIItem OnlyUninvoiced { get { return new UIItem("Accounting  page>> AdvFilter>> OnlyUninvoiced link", this.onlyuninvoiced,_driver); } }
        public UIItem SearchType { get { return new UIItem("Accounting  page>> AdvFilter>> SeachType DD", this.searchtype,_driver); } }
        public UIItem SearchVal { get { return new UIItem("Accounting  page>> AdvFilter>> Search Edit box", this.searchval,_driver); } }
        public UIItem SearchButton { get { return new UIItem("Accounting  page>> Search Button", this.searchbutton,_driver); } }
        public UIItem SearchRegion { get { return new UIItem("Accounting  page>> Search Region", this.searchregion,_driver); } }
        public UIItem SearchResult1stRow { get { return new UIItem("Search result 1st row", this.searchresult1strow, _driver); } }
        public UIItem InvoiceStatus { get { return new UIItem("Accounting  page>> AdvFilter>> Invoice Status", this.invoicestatus, _driver); } }
        public UIItem ClearAll { get { return new UIItem("Accounting  page>> AdvFilter>> Clear All", this.clearall, _driver); } }
        public UIItem ClearAllForCustomer { get { return new UIItem("Accounting  page>> AdvFilter>> Clear All", this.clearallforcustomer, _driver); } }
        public UIItem LoadNumberColumn { get { return new UIItem("Accounting  page>> Advance Search Results>> Load # Column Header", this.loadnumbercolumn, _driver); } }
        public UIItem RefNumberColumn { get { return new UIItem("Accounting  page>> Advance Search Results>> Ref # Column Header", this.refnumbercolumn, _driver); } }
        public UIItem LoadDateColumn { get { return new UIItem("Accounting  page>> Advance Search Results>> Load Date Column Header", this.loaddatecolumn, _driver); } }
        public UIItem CarrierNameColumn { get { return new UIItem("Accounting  page>> Advance Search Results>> Carrier Name Column Header", this.carriernamecolumn, _driver); } }
        public UIItem ProColumn { get { return new UIItem("Accounting  page>> Advance Search Results>> Pro # Column Header", this.procolumn, _driver); } }
        public UIItem AmountColumn { get { return new UIItem("Accounting  page>> Advance Search Results>> Amount Column Header", this.amountcolumn, _driver); } }
        public UIItem InvoiceNumberColumn { get { return new UIItem("Accounting  page>> Advance Search Results>> Invoice # Column Header", this.invoicenumbercolumn, _driver); } }
        public UIItem PayDateColumn { get { return new UIItem("Accounting  page>> Advance Search Results>> Pay Date Column Header", this.paydatecolumn, _driver); } }
        public UIItem CheckColumn { get { return new UIItem("Accounting  page>> Advance Search Results>> Check Column Header", this.checkcolumn, _driver); } }
        public UIItem InvoiceStatusColumn { get { return new UIItem("Accounting  page>> Advance Search Results>> Carrier Name Column Header", this.invoicestatuscolumn, _driver); } }
        public UIItem BalanceColumn { get { return new UIItem("Accounting  page>> Advance Search Results>> Balance Column Header", this.balancecolumn, _driver); } }
        public UIItem LoadDate1stRow { get { return new UIItem("Accounting  page>> Advance Search Results>> Carrier Name 1st Row", this.loaddate1strow, _driver); } }
        public UIItem CarrierName1stRow { get { return new UIItem("Accounting  page>> Advance Search Results>> Carrier Name 1st Row", this.carriername1strow, _driver); } }
        public UIItem InvoiceStatus1stRow { get { return new UIItem("Accounting  page>> Advance Search Results>> Carrier Name 1st Row", this.invoicestatus1strow, _driver); } }
        public UIItem SearchResult1stRowInvoice { get { return new UIItem("Search result 1st row", this.searchresult1strowInvoice, _driver); } }
        public UIItem SearchResultsContainer { get { return new UIItem("Accounting Search result table", this.searchresultscontainer, _driver); } }
        public UIItem SearchLoadsResultRows { get { return new UIItem("Accounting Search result table rows", this.searchloadsresultrows, _driver); } }
        public UIItem InvoiceStatuses { get { return new UIItem("Accounting advance search invoice status", this.invoicestatuses, _driver); } }
        public UIItem BasicSearchRegion { get { return new UIItem("Accounting Search region", this.basicsearchregion, _driver); } }
        public UIItem SpecificNumberBtn { get { return new UIItem("Accounting Page Factoring>> Specific Number button", this.specificnumberbtn, _driver); } }
        public UIItem AdvSearchNoFilterBtn { get { return new UIItem("Accounting Page Factoring>> Advance Search Without Filter button", this.advsearchnofilterbtn, _driver); } }
        public UIItem SearchResultTableHeader_Factoring { get { return new UIItem("Accounting Page Factoring>> Table Header", By.CssSelector("#column-headers-container>tr>th"), _driver); } }
        public List<SearchResult_Rows> rows
        {
            get
            {
                List<SearchResult_Rows> searchResultRows = new List<SearchResult_Rows>();
                foreach (var element in this.SearchLoadsResultRows.FindElements())
                {
                    searchResultRows.Add(new SearchResult_Rows(element));
                }
                return searchResultRows;
            }
        }

    }

    public class SearchResult_Rows
    {
        private IWebElement _driver;
        private By loadid = By.XPath(".//td[1]/a");
        private By referenceid = By.XPath(".//td[2]");
        private By loaddate = By.XPath(".//td[3]");
        private By carrier = By.XPath(".//td[4]");
        private By pro = By.XPath(".//td[5]");
        private By amount = By.XPath(".//td[6]");
        private By invoice = By.XPath(".//td[7]/div");
        private By paydate = By.XPath(".//td[8]/div");
        private By check = By.XPath(".//td[9]/div");
        private By invoicestatus = By.XPath(".//td[10]");
        private By balance = By.XPath(".//td[11]");

        public SearchResult_Rows(IWebElement driver)
        {
            _driver = driver;
        }
        public UIItem LoadId { get { return new UIItem("", this.loadid, _driver); } }
        public UIItem ReferenceId { get { return new UIItem("", this.referenceid, _driver); } }
        public UIItem LoadDate { get { return new UIItem("", this.loaddate, _driver); } }
        public UIItem Carrier { get { return new UIItem("", this.carrier, _driver); } }
        public UIItem Pro { get { return new UIItem("", this.pro, _driver); } }
        public UIItem Amount { get { return new UIItem("", this.amount, _driver); } }
        public UIItem Invoice { get { return new UIItem("", this.invoice, _driver); } }
        public UIItem PayDate { get { return new UIItem("", this.paydate, _driver); } }
        public UIItem Check { get { return new UIItem("", this.check, _driver); } }
        public UIItem InvoiceStatus { get { return new UIItem("", this.invoicestatus, _driver); } }
        public UIItem Balance { get { return new UIItem("", this.balance, _driver); } }

    }

}
