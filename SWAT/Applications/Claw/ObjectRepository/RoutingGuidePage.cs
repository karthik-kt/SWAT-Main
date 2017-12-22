using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SWAT.FrameWork.Utilities;
using SWAT.Configuration;

namespace SWAT.Applications.Claw.ObjectRepository
{
    class RoutingGuidePage :Page
    {
        public RoutingGuidePage(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#routingguide";
        }

        public UIItem objOriginState
        {
            get
            {
                return new UIItem("Adv search origin state", By.CssSelector("#origin-state-select"), _driver);
            }
        }
        public UIItem objOriginCity
        {
            get
            {
                return new UIItem("Adv search origin city", By.CssSelector("#origin-city-search-input"), _driver);
            }
        }
        public UIItem objDestinationState
        {
            get
            {
                return new UIItem("Adv search Destination state", By.CssSelector("#destination-state-select"), _driver);
            }
        }
        public UIItem objDestinationCity
        {
            get
            {
                return new UIItem("Adv search Destination city", By.CssSelector("#destination-city-search-input"), _driver);
            }
        }
        public UIItem RoutingGuide_DD
        {
            get
            {
                return new UIItem("Routing Guide Dropdown", By.CssSelector("#routing-guide-options"), _driver);
            }
        }

        public UIItem AdvSearch
        {
            get
            {
                return new UIItem("Adv search button", By.CssSelector("#advanced-search-options-button"), _driver);
            }
        }

        public UIItem AdvSearch_OrginFacility
        {
            get
            {
                return new UIItem("Adv search >> OrginFacility", By.CssSelector("#origin-facility-search-input"), _driver);
            }
        }

        public UIItem AdvSearch_DestinationFacility
        {
            get
            {
                return new UIItem("Adv search >> DestinationFacility", By.CssSelector("#destination-facility-search-input"), _driver);
            }
        }
    }
}
