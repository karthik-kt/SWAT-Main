using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;

namespace SWAT.Applications.Claw
{
    using SWAT.Data;
    using SWAT.Logger;
    using SWAT.FrameWork.Utilities;
    using Config = SWAT.Configuration.TestStartInfo;


    public class CLAW : UIObjects //partial class TestManager//class Class_Claw
    {
        private By byProfile = By.CssSelector("#show-account-options");
        private By bySignOut = By.CssSelector("#sign-out");
        private By byUserName = By.CssSelector("#UserName");
        private By byPwd = By.Id("Password");
        private By byLoadSearchInput = By.CssSelector("#load-search-input");

        private By bySubmit = By.Id("saveForm");

        private By byAdmin = By.XPath("//a[contains(@href, '#admin')]");
        private By byHeaderLogo = By.CssSelector(".fl.nudge-half.flush--left");

        private string UserName;
        private string PassWord;
        private string baseURL;
        public CLAW(Config c, DataManager t)
            : base(c)
        {
            testConfig = c;
            driver = testConfig.Driver;
            baseURL = c.BaseURL;
            UserName = t.Data("UserName");
            PassWord = t.Data("PassWord");
            //MyLogger = c.Logger;
        }

        #region functions

        //Navigate to claw URL
        public string NavigateToURL()
        {
            try
            {
                driver.Navigate().GoToUrl(baseURL);
                MyLogger.Log("URL Opened >> " + baseURL);
                Thread.Sleep(Constants.Wait_Medium);
                return "NavigationSuccess";
            }
            catch
            {
                MyLogger.Log("Unable to open");
                return "NavigationFailed";
            }
        }

        //Logout of Claw
        public string LogOut()
        {
            try
            {
                Assert.IsTrue(WaitUtilDisplayed(byProfile));
                Click(byProfile);
                Click(bySignOut);
                Assert.IsTrue(WaitUtilDisplayed(byUserName));
                return "LogoutSuccess";
            }
            catch
            {
                return "LogoutFailed";
            }
        }

        //Login into claw
        public string Login()
        {
            try
            {
                driver.Url = baseURL;
                WaitUtilDisplayed(byUserName);
                Assert.IsTrue(WaitUtilDisplayed(byUserName));
                Clear(byUserName);
                Edit(byUserName, UserName);
                MyLogger.Log("User name entered := [" + UserName + "]");
                Clear(byPwd);
                Edit(byPwd, PassWord);
                MyLogger.Log("Password entered :=  []");
                Click(bySubmit);
                MyLogger.Log("Clicked on the [Sign in] button");
                Assert.IsTrue(WaitUtilDisplayed(byHeaderLogo));
                MyLogger.Log("Home page displayed");
                return "LoginSuccess";
            }
            catch
            {
                MyLogger.Log("Login Failed");
                return "LOGINFAILED";
            }
        }

        #endregion functions

        internal string NavigateTo(string page)
        {
            try
            {
                if (Navigate(page))
                    return "NavigationSuccess";
                else
                    return "NavigationFailed";
            }
            catch
            {
                return "NavigationFailed";
            }
        }

        private bool Navigate(string page)
        {
            try
            {
                By byHomeNav = By.CssSelector("#header-logo");
                By byHomeVer = By.CssSelector("#dashboard-view>div");
                switch (page)
                {
                    case "HOME":
                        Click(byHomeNav);
                        return WaitUtilDisplayed(byHomeVer);
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}