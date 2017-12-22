using OpenQA.Selenium;
using System.Text.RegularExpressions;
using SWAT.Configuration;

namespace SWAT.Applications.Claw
{
    using SWAT.Logger;
    using Config = SWAT.Configuration.TestStartInfo;
    using SWAT.FrameWork.Utilities;

    public class Page 
    {
        internal IWebDriver _driver;
        internal string _baseurl;

        protected string url = null;
        protected By navigationlink = null;
        public string Url { get { return this.url; } }

        public Page(IWebDriver driver)
        {
            _driver = driver;
        }

        public Page()
        {

        }

        public bool NoResultsFound()
        {
            if (Regex.IsMatch(_driver.FindElement(By.TagName("BODY")).Text, "No results found."))
            {
                MyLogger.Log("No Results were displayed.");
                return true;
            }
            else
                return false;
        }

        public bool AnErrorOccured()
        {
            if (Regex.IsMatch(_driver.FindElement(By.TagName("body")).Text, "An error has occurred"))
            {
                MyLogger.Log("An error has occurred");
                return true;
            }
            else
                return false;
        }

        public string isAt()
        {
            By pagetitle = By.CssSelector("#app-title");
            return _driver.FindElement(pagetitle).Text.ToString().ToUpper();            
        }

        public bool Navigate()
        {
            try
            {
                if (url != null)
                {
                    _driver.Navigate().GoToUrl(_baseurl + this.Url);
                    return true;
                }
                if (navigationlink != null)
                {
                    _driver.FindElement(navigationlink).Click();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool Navigate(string url)
        {
            try
            {
                _driver.Navigate().GoToUrl(_baseurl + url);
                return true;
            }
            catch
            {
                return false;
            }
        }

        By byGlobalMessage = By.CssSelector("#global-notification__message");

        public bool WaitUntilLoading()
        {
            try
            {                
                while (IsDisplayed(byGlobalMessage))
                {
                    if (GetText(byGlobalMessage) == "Loading...")
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
            catch
            {
                return false;
            }
        }

        public bool WaitUntilSaving()
        {
            try
            {
                while (IsDisplayed(byGlobalMessage))
                {
                    if (GetText(byGlobalMessage) == "Saving...")
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
            catch
            {
                return false;
            }
        }


        private bool IsDisplayed(By by)
        {
            try
            {
                if (_driver.FindElement(by).Displayed)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        private string GetText(By by)
        {
            try
            {
                return  _driver.FindElement(by).Text;
            }
            catch
            {
                return null;
            }
        }
    }
}