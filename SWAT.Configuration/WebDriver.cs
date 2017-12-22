using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;


namespace SWAT.Configuration
{
    public sealed class WebDriver
    {
        
        private IWebDriver driver;

        public  IWebDriver Driver
        {
            get { return driver; }
        }

        public WebDriver(string browername, string driverpath)
        {
            try
            {
                switch (browername.ToUpper().Trim())
                {
                    case "CHROME":
                        driver = new ChromeDriver(driverpath);                        
                        break;
                    case "IE":
                    case "INTERNETEXPLORER":
                        driver = new InternetExplorerDriver(driverpath);
                        break;
                    case "FIREFOX":
                    default:
                        driver = new FirefoxDriver();
                        break;
                }
                driver.Manage().Window.Maximize();
                driver.Manage().Cookies.DeleteAllCookies();
                //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            }
            catch(Exception ex)
            {
                driver = new FirefoxDriver();
                driver.Manage().Window.Maximize();
                throw ex;
            }
        }
    }
}