using OpenQA.Selenium;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SWAT.Applications.Claw
{
    using SWAT.Data;
    using SWAT.FrameWork.Utilities;
    using SWAT.Applications.Claw.ObjectRepository;
    using SWAT.Configuration;
    using SWAT.Applications.Claw.DAL;
    using OpenQA.Selenium.Support.UI;
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using System.Globalization;
    public class Admin 
    {
        //private By bySrchRes1stRow = By.CssSelector("#user-search-results>tr>td>a");
        //private By bySearchVal = By.CssSelector("#search-input-box");
        //private By SearchButton = By.CssSelector("#admin-search-button");
        //private By SearchHyperlink = By.CssSelector("#specific-search-button");
        //private By AdminTab = By.LinkText("Admin");

        AdminPage _AdminPage;
        AdminData _AdminData;
        IWebDriver driver;

        public Admin(TestStartInfo teststartinfo, DataManager datamanager)
        {
            driver = teststartinfo.Driver;
            _AdminPage = new AdminPage(teststartinfo);
            _AdminData = new AdminData(datamanager);
        }

        public string IsAdminTabVisible()
        {
            try
            {
                Assert.IsTrue(_AdminPage.AdminTab.IsDisplayed());
                return "TabVisible";
            }
            catch
            {
                return "TabNotVisible";
            }
        }

        public string Search()
        {
            try
            {
                int retry = 0;
                do
                {
                    do
                    {
                        OpenAdminPage();
                    }
                    while (!_AdminPage.ValueToSearch.WaitUntilDisplayed(10));
                    Assert.IsTrue(_AdminPage.SearchType.WaitUntilDisplayed());
                    Assert.IsTrue(_AdminPage.SearchType.SelectByText(_AdminData.SearchType));
                    Assert.IsTrue(_AdminPage.ValueToSearch.Clear());
                    Assert.IsTrue(_AdminPage.ValueToSearch.ClearAndEdit(_AdminData.SearchVal));
                    try 
                    { 
                        _AdminPage.ValueToSearch.Edit(Keys.Enter); 
                    }
                    catch 
                    { 
                        try 
                        { 
                            _AdminPage.SearchButton.Click(); 
                        } 
                        catch 
                        { 
                        } 
                    }
                    retry++;
                }
                while (!_AdminPage.SearchResult1stRow.WaitUntilDisplayed(30) && retry > 2);
                return "UserSearchSuccess";
            }
            catch
            {
                return "UserSearchFailed";
            }
        }

        private string OpenAdminPage()
        {
            try
            {
                Assert.IsTrue(_AdminPage.AdminTab.WaitUntilDisplayed());
                if (!_AdminPage.Navigate())
                {
                    Assert.IsTrue(_AdminPage.AdminTab.Click());
                }
                Assert.IsTrue(_AdminPage.SearchButton.WaitUntilDisplayed());
                Assert.IsTrue(_AdminPage.SearchHyperLink.WaitUntilDisplayed());
                return "AdminPageOpened";
            }
            catch
            {
                return "AdminPageOpenFailed";
            }
        }

        public string VerifyAdminPage()
        {
            try
            {
                while (!_AdminPage.ValueToSearch.WaitUntilDisplayed(10))
                {
                    OpenAdminPage();
                }
                
                


                Assert.IsTrue(_AdminPage.SearchButton.WaitUntilDisplayed());                
                Assert.IsTrue(_AdminPage.SearchHyperLink.WaitUntilDisplayed());
                Assert.IsTrue(_AdminPage.ValueToSearch.WaitUntilDisplayed());
                Assert.IsTrue(_AdminPage.ValueToSearch.WaitUtilEnabled());
                //Verify that on clicking the 'search' hyperlink user control is moved to the Search text box                
                //Assert.IsTrue(driver.SwitchTo().ActiveElement().GetAttribute("id").Equals("search-input-box"));
                Assert.IsTrue(_AdminPage.ValueToSearch.IsFocused());
                //Verify default text on admin page
                if (_AdminPage.DefaultText.GetText() != "Manage users by using the search above.")
                {
                    return "AdminPageDefaultTextNotMatched";
                }
                //Verify that the dropdown contains all 4 entity types
                string[] expectedEntityTypes = { "Users", "Facilities", "Carriers", "Customers" };
                Assert.IsTrue(_AdminPage.SearchType.CompareDDAllOptions(expectedEntityTypes));
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
        }

        public string NavigateToHomePage()
        {
            try
            {
                Assert.IsTrue(_AdminPage.AdminTab.WaitUntilDisplayed());
                Assert.IsTrue(_AdminPage.AdminTab.Click());
                _AdminPage.WaitUntilLoading();
                Assert.IsTrue(_AdminPage.CoyoteSymbol.WaitUntilDisplayed());
                Assert.IsTrue(_AdminPage.CoyoteSymbol.Click());
                Assert.IsTrue(_AdminPage.HomePage.WaitUntilDisplayed());
                if(!driver.Url.Contains("#"))
                {
                    return "NavigationFailed";
                }
                return "NavigationSuccess";
            }
            catch
            {
                return "NavigationFailed";
            }
        }

        public string SearchAndOpen()
        {
            try
            {
                string actualresult = Search();
                actualresult = actualresult == "UserSearchSuccess" ? OpenFirstSearchResult() : "UserSearchFailed";
                return actualresult;
            }
            catch
            {
                return "SearchAndOpenFailed";
            }
        }

        public string OpenFirstSearchResult()
        {
            try
            {
                _AdminPage.SearchResult1stRow.WaitUntilDisplayed(40);
                _AdminPage.SearchResult1stRow.Click();
                _AdminPage.WaitUntilLoading();
                //if (_AdminData.SearchType.ToUpper() == Constants.Entity_People)
                //{
                //    Assert.IsTrue(_AdminPage.byAccountInfo.WaitUntilDisplayed());
                //}
                //else if (_AdminData.SearchType.ToUpper() == Constants.Entity_Customer)
                //{
                //    Assert.IsTrue(_AdminPage.CustomerSummaryDetailsRegion.WaitUntilDisplayed());
                //}
                //else
                //{
                //    Assert.IsTrue(_AdminPage.BackToSearchButton.WaitUntilDisplayed());
                //}              
                return "SearchAndOpenSuccess";
            }
            catch
            {
                return "SearchAndOpenFailed";
            }
                
        }

        public string UIVerify()
        {
            try
            {
                if (_AdminData.AdminType.ToUpper() == "EXTERNAL")
                {
                    Assert.IsTrue(VerifyExternalAdmin());
                }
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
        }

        private bool VerifyExternalAdmin()
        {
            try
            {
                Assert.IsTrue(_AdminPage.SearchHeaderName.UIVerify(_AdminData.SearchHeaderName));
                Assert.IsTrue(_AdminPage.SearchHeaderUserName.UIVerify(_AdminData.SearchUserName));
                Assert.IsTrue(_AdminPage.SearchHeaderLastLogin.UIVerify(_AdminData.SearchLastLogin));
                Assert.IsTrue(ValidateDateAndTimeFormatForLastLoginColumn(_AdminPage.SearchResultLastLogin1stRow.GetText()));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ValidateDateAndTimeFormatForLastLoginColumn(string dateTime)
        {
            try
            {
                if (dateTime == "--")
                {
                    return true;
                }
                else
                {
                    string[] dateTimeFormat = dateTime.Split(',');
                    bool validDate = ValidateDateFormat(dateTimeFormat[0].Trim(), "MM/dd/yyyy");

                    bool validTime = ValidateDateFormat(dateTimeFormat[1].Trim(), "hh:mm");
                    if (validDate && validTime)
                    {
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

        private bool ValidateDateFormat(string dateTime, string format)
        {
            if (dateTime != null && dateTime != string.Empty)
            {
                DateTime parsed;
                return (DateTime.TryParseExact(dateTime.Trim(), format,
                                                    CultureInfo.InvariantCulture,
                                                    DateTimeStyles.None,
                                                    out parsed));
            }
            else
            {
                return false;
            }
        }
    }
}