using OpenQA.Selenium;
using SWAT.FrameWork.Utilities;
using System.Collections.Generic;
using SWAT.Configuration;

namespace SWAT.Applications.Claw.ObjectRepository
{
    internal class AvailableLoadPage : Page
    {
        private By searchFromDate = By.CssSelector("#search-from-date");
        private By searchToDate = By.CssSelector("#search-to-date");
        private By availableLoadsEquipment = By.CssSelector("#available-loads-equipment");
        private By availableLoadsOriginState = By.CssSelector("#available-loads-origin-state");
        private By originDeadhead = By.CssSelector("#filter-origin-deadhead-input");
        private By availableLoadsDestinationState = By.CssSelector("#available-loads-destination-state");
        private By destinationDeadhead = By.CssSelector("#filter-destination-deadhead-input");
        private By filterEquipmentType = By.CssSelector(".hook--equipmentType");
        private By filterOrigin = By.CssSelector(".hook--origin");
        private By filterDestination = By.CssSelector(".hook--destination");
        private By searchButton = By.CssSelector("#search-button");
        private By searchResultFirstRow = By.CssSelector("#search-results-container > tr > td");
        private By searchResultsContainer = By.CssSelector("#search-results-container > tr");
        private By noResultsFound = By.CssSelector("#no-results-found");
        private By clearAllSearch = By.CssSelector(".hook--clear-all-search");
        private By tableHeader = By.CssSelector("#column-headers-container > tr > th");
        private By recommendedLoadButton = By.CssSelector("#recommended-loads-button");
        private By preferredLanesContainerRows = By.CssSelector("#view--preferredlane-itemview > tr");
        private By offerSubmitMsg = By.CssSelector("#offer-submit-message");
        private By appTitle = By.CssSelector("#app-title");
        private By undoRemovedLoad = By.CssSelector("#undo-remove-load");
        private By curtainContainer = By.CssSelector("#curtain-container");
        private By emptyLocation = By.CssSelector("#empty-location");
        private By canLoadDate = By.CssSelector("#can-load-date");
        private By canLoadTime = By.CssSelector("#can-load-time");
        private By offerEquipmentType = By.CssSelector("#equipmentType");
        private By offerRate = By.CssSelector("#offer");
        private By offerSubmit = By.CssSelector(".hook--button-submit");
        private By cancelOffer = By.CssSelector(".hook--close-curtain-button");
        private By addToPreferredLanes = By.CssSelector("#add-to-preferred-lanes");
        private By removeOffer = By.CssSelector("#remove-offer");
        private By curtainLanesAlertMessage = By.CssSelector("#add-to-preferred-lanes-alert-message");
        private By browseFilterButton = By.CssSelector("#browse-filter-button");

        public AvailableLoadPage(IWebDriver driver)
        {
            _driver = driver;
            url = "#availableloads";
        }

        public AvailableLoadPage(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#availableloads";
        }

        public List<RecommendedLoad> RecommendedLoads
        {
            get
            {
                List<RecommendedLoad> Loads = new List<RecommendedLoad>();
                foreach (var element in this.SearchResults_Row.FindElements())
                {
                    Loads.Add(new RecommendedLoad(element));
                }
                return Loads;
            }
        }

        public List<PreferredLane> PreferredLanes
        {
            get
            {
                List<PreferredLane> Loads = new List<PreferredLane>();
                foreach (var element in this.PreferredLanesContainerRows.FindElements())
                {
                    Loads.Add(new PreferredLane(element));
                }
                return Loads;
            }
        }

        public UIItem BrowseFilterButton { get { return new UIItem("Available Load >> Browse Filter Button", this.browseFilterButton, _driver); } }
        public UIItem CurtainLanesAlertMessage { get { return new UIItem("Available Load >> Curtain Lanes Alert Message", this.curtainLanesAlertMessage, _driver); } }
        public UIItem OfferSubmit { get { return new UIItem("Available Load >> Offer Submit", this.offerSubmit, _driver); } }
        public UIItem CancelOffer { get { return new UIItem("Available Load >> Add To Preferred Lanes", this.cancelOffer, _driver); } }
        public UIItem AddToPreferredLanes { get { return new UIItem("Available Load >> Add To Preferred Lanes", this.addToPreferredLanes, _driver); } }
        public UIItem RemoveOffer { get { return new UIItem("Available Load >> Remove Offer", this.removeOffer, _driver); } }
        public UIItem EmptyLocation { get { return new UIItem("Available Load >> Empty Location", this.emptyLocation, _driver); } }
        public UIItem CanLoadDate { get { return new UIItem("Available Load >> Can Load Date", this.canLoadDate, _driver); } }
        public UIItem CanLoadTime { get { return new UIItem("Available Load >> Can Load Time", this.canLoadTime, _driver); } }
        public UIItem OfferEquipmentType { get { return new UIItem("Available Load >> Offer Equipment Type", this.offerEquipmentType, _driver); } }
        public UIItem OfferRate { get { return new UIItem("Available Load >> Offer Rate", this.offerRate, _driver); } }
        public UIItem CurtainContainer { get { return new UIItem("Available Load >> Curtain Container", this.curtainContainer, _driver); } }
        public UIItem UndoRemovedLoad { get { return new UIItem("App Title", this.undoRemovedLoad, _driver); } }
        public UIItem AppTitle { get { return new UIItem("App Title", this.appTitle, _driver); } }
        public UIItem OfferSubmitMsg { get { return new UIItem("Available Load>> Offer Submit Msg", this.offerSubmitMsg, _driver); } }
        public UIItem PreferredLanesContainerRows { get { return new UIItem("Available Load>> Preferred Lanes Container Rows", this.preferredLanesContainerRows, _driver); } }
        public UIItem SearchResults_Row { get { return new UIItem("Available Load>> Search Results Container", By.CssSelector("#search-results-container > tr"), _driver); } }
        public UIItem RecommendedLoadButton { get { return new UIItem("Available Load>> Recommended Loads Button", this.recommendedLoadButton, _driver); } }
        public UIItem TableHeader { get { return new UIItem("Available Load>> Table Header", this.tableHeader, _driver); } }
        public UIItem ClearAllSearch { get { return new UIItem("Available Load>> Clear All Search", this.clearAllSearch, _driver); } }
        public UIItem NoResultsFound { get { return new UIItem("Available Load>> Search Result First Row", this.noResultsFound, _driver); } }
        public UIItem SearchResults_Table { get { return new UIItem("Available Load>> Search Result First Row", By.CssSelector("#search-results-container > tr > td"), _driver); } }
        public UIItem SearchButton { get { return new UIItem("Available Load>> Search Button", this.searchButton, _driver); } }
        public UIItem FilterDestination { get { return new UIItem("Available Load>> Filter Destination", this.filterDestination, _driver); } }
        public UIItem FilterOrigin { get { return new UIItem("Available Load>> Filter Origin", this.filterOrigin, _driver); } }
        public UIItem FilterEquipmentType { get { return new UIItem("Available Load>> Filter Equipment Type", this.filterEquipmentType, _driver); } }
        public UIItem SearchFromDate { get { return new UIItem("Available Load>> Search From Date", By.Name("daterangepicker_start"), _driver); } }
        public UIItem SearchToDate { get { return new UIItem("Available Load>> Search To Date", By.Name("daterangepicker_end"), _driver); } }
        public UIItem AvailableLoadsEquipment { get { return new UIItem("Available Load>> Available Loads Equipment", this.availableLoadsEquipment, _driver); } }
        public UIItem AvailableLoadsOriginState { get { return new UIItem("Available Load>> Available Loads Origin State", this.availableLoadsOriginState, _driver); } }
        public UIItem OriginDeadhead { get { return new UIItem("Available Load>> Origin Deadhead", By.Id("odh-slider"), _driver); } }
        public UIItem AvailableLoadsDestinationState { get { return new UIItem("Available Load>> Available Loads Destination State", this.availableLoadsDestinationState, _driver); } }
        public UIItem DestinationDeadhead { get { return new UIItem("Available Load>> Destination Deadhead", By.Id("ddh-slider"), _driver); } }
        public UIItem PickupDateRange_Apply { get { return new UIItem("", By.CssSelector(".applyBtn.button.button--loud.fl"), _driver); } }
        public UIItem PickupDateRange_CustomRange { get { return new UIItem("", By.XPath("html/body/div[2]/div[3]/ul/li[4]"), _driver); } }
        //Recommended Load page table columns
        public UIItem LoadNumber { get { return new UIItem("", By.XPath(".//*[@id='column-headers-container']/tr/th[1]"), _driver); } }
        public UIItem Mode { get { return new UIItem("", By.XPath(".//*[@id='column-headers-container']/tr/th[2]"), _driver); } }
        public UIItem Equipment { get { return new UIItem("", By.XPath(".//*[@id='column-headers-container']/tr/th[3]"), _driver); } }
        public UIItem Stops { get { return new UIItem("", By.XPath(".//*[@id='column-headers-container']/tr/th[4]"), _driver); } }
        public UIItem Origin { get { return new UIItem("", By.XPath(".//*[@id='column-headers-container']/tr/th[5]"), _driver); } }
        public UIItem PickupDateAndTime { get { return new UIItem("", By.XPath(".//*[@id='column-headers-container']/tr/th[6]"), _driver); } }
        public UIItem Destination { get { return new UIItem("", By.XPath(".//*[@id='column-headers-container']/tr/th[7]"), _driver); } }
        public UIItem DeliveryDateAndTime { get { return new UIItem("", By.XPath(".//*[@id='column-headers-container']/tr/th[8]"), _driver); } }
        public UIItem Distance { get { return new UIItem("", By.XPath(".//*[@id='column-headers-container']/tr/th[9]"), _driver); } }
        public UIItem RemoveRecommendationsLoad { get { return new UIItem("Available Load>> Recommended>> Remove Recommendation", By.CssSelector("#remove-load"), _driver); } }
        public UIItem Undo_RemoveRecommendationsLoad { get { return new UIItem("Available Load>> Recommended>> Undo Remove Recommendation", By.CssSelector("#undo-remove-load"), _driver); } }
        public UIItem RemovedRecommendations_Message { get { return new UIItem("Available Load>> Recommended>> Remove Recommendation Message", By.CssSelector("#offer-submit-message"), _driver); } }
    }
    
    internal class RecommendedLoad
    {
        private IWebElement _driver;
        private By loadID = By.XPath(".//td[2]");
        private By mode = By.XPath(".//td[3]");
        private By equipment = By.XPath(".//td[4]");
        private By stops = By.XPath(".//td[5]");
        private By origin = By.XPath(".//td[6]");
        private By pickupDateTime = By.XPath(".//td[7]");
        private By destination = By.XPath(".//td[8]");
        private By deliveryDateTime = By.XPath(".//td[9]");
        private By distance = By.XPath(".//td[10]");
        private By moreInfoButton = By.CssSelector(".hook--open-offer");
        private By moreInfoPopUp = By.CssSelector(".hook--popup-trigger.add-on__item");
        private By addToPreferedLane = By.CssSelector("#add-to-preffered-lanes");
        private By removeLoad = By.CssSelector("#remove-load");
        private By offerDisplayAmount = By.CssSelector(".hook--offer-display-amount");

        public RecommendedLoad(IWebElement driver)
        {
            _driver = driver;
        }

        public UIItem OfferDisplayAmount { get { return new UIItem("Available Load >> Offer Display Amount", this.offerDisplayAmount, _driver); } }
        public UIItem RemoveLoad { get { return new UIItem("Recommended Load >> Remove Load", this.removeLoad, _driver); } }
        public UIItem AddToPreferedLane { get { return new UIItem("Recommended Load >> Add To Prefered Lane", this.addToPreferedLane, _driver); } }
        public UIItem LoadID { get { return new UIItem("Recommended Load >> LoadID", this.loadID, _driver); } }
        public UIItem Mode { get { return new UIItem("Recommended Load >> Mode", this.mode, _driver); } }
        public UIItem Equipment { get { return new UIItem("Recommended Load >> Equipment", this.equipment, _driver); } }
        public UIItem Stops { get { return new UIItem("Recommended Load >> Stops", this.stops, _driver); } }
        public UIItem Origin { get { return new UIItem("Recommended Load >> Origin", this.origin, _driver); } }
        public UIItem PickupDateTime { get { return new UIItem("Recommended Load >> PickupDateTime", this.pickupDateTime, _driver); } }
        public UIItem Destination { get { return new UIItem("Recommended Load >> Destination", this.destination, _driver); } }
        public UIItem DeliveryDateTime { get { return new UIItem("Recommended Load >> DeliveryDateTime", this.deliveryDateTime, _driver); } }
        public UIItem Distance { get { return new UIItem("Recommended Load >> Distance", this.distance, _driver); } }
        public UIItem MoreInfoButton { get { return new UIItem("Recommended Load >> MoreInfoButton", this.moreInfoButton, _driver); } }
        public UIItem MoreInfoPopUp { get { return new UIItem("Recommended Load >> MoreInfoPopUp", this.moreInfoPopUp, _driver); } }
    }

    internal class PreferredLane
    {
        private IWebElement _driver;
        private By origin = By.CssSelector(".hook--preferred-lane-orig-input");
        private By destination = By.CssSelector(".hook--preferred-lane-dest-input");
        private By equipmentType = By.CssSelector(".hook--type");

        public PreferredLane(IWebElement driver)
        {
            _driver = driver;
        }

        public UIItem Origin { get { return new UIItem("Preferences >> origin", this.origin, _driver); } }
        public UIItem Destination { get { return new UIItem("Preferences >> Destination", this.destination, _driver); } }
        public UIItem EquipmentType { get { return new UIItem("Preferences >> Equipment Type", this.equipmentType, _driver); } }
    }

    internal class AvilableLoad_BrowseAndFliter : Page
    {
        public AvilableLoad_BrowseAndFliter(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#availableloads";
        }

        public UIItem RecommendedLoad_Tab { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> Recommaded Tab", By.CssSelector("#recommended-loads-button"), _driver); } }
        public UIItem BrowseAndFliter_Tab { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> BrowseAndFilter Tab", By.CssSelector("#recommended-loads-button"), _driver); } }
        public UIItem PostAndMatch_Tab { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> PostAndMatch Tab", By.CssSelector("#recommended-loads-button"), _driver); } }
        public UIItem Result_Row { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> Result Row", By.CssSelector("#search-results-container>tr"), _driver); } }
        public UIItem Result_Table { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> Table", By.CssSelector("#search-results-container>tr>td"), _driver); } }
        public UIItem MoreInfo_Button { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> MoreInfo Button", By.CssSelector(".button.align-top.hook--open-offer"), _driver); } }
        public UIItem Column_LoadNumber { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> Load Number Col", By.CssSelector("#search-results-container>tr>td:nth-child(1)"), _driver); } }
        public UIItem Column_Mode { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> Column_Mode", By.CssSelector("#search-results-container>tr>td:nth-child(2)"), _driver); } }
        public UIItem Column_Equipment { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> Column_Equipment", By.CssSelector("#search-results-container>tr>td:nth-child(3)"), _driver); } }
        public UIItem Column_Stops { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> Column_Stops", By.CssSelector("#search-results-container>tr>td:nth-child(4)"), _driver); } }
        public UIItem Column_Origin { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> Column_Origin", By.CssSelector("#search-results-container>tr>td:nth-child(5)"), _driver); } }
        public UIItem Column_PickupDateAndTime { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> Column_PickupDateAndTime", By.CssSelector("#search-results-container>tr>td:nth-child(6)"), _driver); } }
        public UIItem Column_Destination { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> Column_Destination", By.CssSelector("#search-results-container>tr>td:nth-child(7)"), _driver); } }
        public UIItem Column_DeliveryDateAndTime { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> Column_DeliveryDateAndTime", By.CssSelector("#search-results-container>tr>td:nth-child(8)"), _driver); } }
        public UIItem Column_Distance { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> Column_Distance", By.CssSelector("#search-results-container>tr>td:nth-child(9)"), _driver); } }
        public UIItem Column_MoreInfo { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> Column_MoreInfo", By.CssSelector("#search-results-container>tr>td:nth-child(10)"), _driver); } }
        public UIItem Button_MoreInfo { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> Button_MoreInfo", By.CssSelector(".button.align-top.hook--open-offer"), _driver); } }

        public UIItem OfferSumitAlert { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> OfferSumitAlert", By.CssSelector("#offer-submit-alert"), _driver); } }
        public UIItem MakeOffer_EmptyLocation { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> MakeOffer_EmptyLocation", By.CssSelector("#empty-location"), _driver); } }
        public UIItem MakeOffer_EmptyDate { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> MakeOffer_EmptyDate", By.CssSelector("#can-load-date"), _driver); } }
        public UIItem MakeOffer_EmptyTime { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> MakeOffer_EmptyTime", By.CssSelector("#can-load-time"), _driver); } }
        public UIItem MakeOffer_EquipmentType { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> MakeOffer_EquipmentType", By.CssSelector("#equipmentType"), _driver); } }
        public UIItem MakeOffer_OfferAmount { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> MakeOffer_OfferAmount", By.CssSelector("#offer"), _driver); } }
        public UIItem MakeOffer_OfferSubmit { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> MakeOffer_OfferSubmit", By.CssSelector(".button.button--loud.hook--button-submit"), _driver); } }
        public UIItem MakeOffer_OfferCancel { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> MakeOffer_OfferCancel", By.CssSelector(".hook--close-curtain-button.hook--close-offer.text-link.nudge-half--left"), _driver); } }
        public UIItem MakeOffer_RemoveOffer { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> MakeOffer_EmptyLocation", By.CssSelector("#remove-offer"), _driver); } }
        public UIItem MakeOffer_Title { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> MakeOffer_Title", By.CssSelector("#search-summary-region>div>h4"), _driver); } }
        public UIItem MakeOffer_EmptyLocationDisabled { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> MakeOffer_Title", By.CssSelector("#location-control-region"), _driver); } }
        public UIItem MakeOffer_NoLoadSelected { get { return new UIItem("Avilable Load>>  BrowseAndFilter>> No Load Selected Msg", By.CssSelector("#search-summary-region>h3"), _driver); } }


    }

    internal class AvilableLoad_PostAndMatch : Page
    {
        public AvilableLoad_PostAndMatch(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#availableloads";
        }
        //Post and match
        private By byLinkPstAndMth = By.CssSelector("#post-match-button");
        private By byAddTruck = By.CssSelector(".button.hook--add-truck.fr");

        //Add Truck
        private By byTruckOrgin = By.CssSelector("#empty-location");
        private By byTruckDest = By.CssSelector("#destination-location");
        private By byTruckEmpDate = By.CssSelector("#can-load-date");
        private By byTruckEmpTime = By.CssSelector("#can-load-time");
        private By byTruckEquType = By.CssSelector("#equipmentType");
        private By byTruckLength = By.CssSelector("#length");
        private By byTruckAdd = By.CssSelector(".button.button--loud");
        private By byTruckAddCancel = By.CssSelector(".hook--close-curtain-button.button.button--quiet.nudge-half--sides.hook--slideout-cancel");
        private By byTruckAddClose = By.CssSelector(".hook--close-curtain-button.curtain-header__close-button");
        private By byTruckSuccessMsg = By.CssSelector("#add-to-truck-alert");

        //Match Truck
        private By byViewTrucks = By.CssSelector("#view-trucks-button");
        private By byViewTrucksODH = By.Id("ODH");
        private By ByViewTrucksDDH = By.Id("DDH");
        private By ByVireTrucksMatchTrucks = By.CssSelector("#matchSingleTruck");

        public UIItem PostAndMatch_Tab { get { return new UIItem("Avilable Load>>  PostAndMatchTab ", this.byLinkPstAndMth, _driver); } }
        public UIItem AddTruck_Button { get { return new UIItem("Avilable Load>>  PostAndMatch>> AddTruck Button", this.byAddTruck, _driver); } }
        public UIItem AddTruck_Orgin { get { return new UIItem("Avilable Load>>  PostAndMatch>> AddTruck>> Orgin", this.byTruckOrgin, _driver); } }
        public UIItem AddTruck_Destination { get { return new UIItem("Avilable Load>>  PostAndMatch>> AddTruck>> Destination", this.byTruckDest, _driver); } }
        public UIItem AddTruck_EmptyDate { get { return new UIItem("Avilable Load>>  PostAndMatch>> AddTruck>> EmptyDate", this.byTruckEmpDate, _driver); } }
        public UIItem AddTruck_EmptyTime { get { return new UIItem("Avilable Load>>  PostAndMatch>> AddTruck>> EmptyTime", this.byTruckEmpTime, _driver); } }
        public UIItem AddTruck_EquipmentType { get { return new UIItem("Avilable Load>>  PostAndMatch>> AddTruck>> EquipmentType", this.byTruckEquType, _driver); } }
        public UIItem AddTruck_Length { get { return new UIItem("Avilable Load>>  PostAndMatch>> AddTruck>> Length", this.byTruckLength, _driver); } }
        public UIItem AddTruck_AddButton { get { return new UIItem("Avilable Load>>  PostAndMatch>> AddTruck>> Add Button", this.byTruckAdd, _driver); } }
        public UIItem AddTruck_CancelButton { get { return new UIItem("Avilable Load>>  PostAndMatch>> AddTruck>> Cancel Button", this.byTruckAddCancel, _driver); } }
        public UIItem AddTruck_CloseButton { get { return new UIItem("Avilable Load>>  PostAndMatch>> AddTruck>> Close Button", this.byTruckAddClose, _driver); } }
        public UIItem AddTruck_SuccessMsg { get { return new UIItem("Avilable Load>>  PostAndMatch>> AddTruck>> SuccessMessage", this.byTruckSuccessMsg, _driver); } }

        public UIItem ViewTrucks { get { return new UIItem("Avilable Load>>  PostAndMatch>> ViewTrucks", this.byViewTrucks, _driver); } }
        public UIItem ViewTrucks_Results { get { return new UIItem("Avilable Load>>  PostAndMatch>> ViewTrucksResults", By.CssSelector("#viewtrucks-results-container>tr>td"), _driver); } }
        public UIItem ViewTrucks_ResultsRows { get { return new UIItem("Avilable Load>>  PostAndMatch>> ViewTrucksResultsRow", By.CssSelector("#viewtrucks-results-container>tr"), _driver); } }
        public UIItem ViewTrucks_Results_ODH { get { return new UIItem("Avilable Load>>  PostAndMatch>> ViewTrucks>> ODH", By.CssSelector("#add-truck-origin-deadhead-input"), _driver); } }
        public UIItem ViewTrucks_Results_DDH { get { return new UIItem("Avilable Load>>  PostAndMatch>> ViewTrucks>> DDH", By.CssSelector("#add-truck-destination-deadhead-input"), _driver); } }
        public UIItem ViewTrucks_Results_MatchTrcukButton { get { return new UIItem("Avilable Load>>  PostAndMatch>> ViewTrucks>> MatchTruck Button", this.ByVireTrucksMatchTrucks, _driver); } }
        public UIItem NoResultFound { get { return new UIItem("Avilable Load>>  PostAndMatch>> No Result Found", By.CssSelector("#no-results-found"), _driver); } }
        public UIItem PostAndMatch_ResultsTable { get { return new UIItem("Avilable Load>>  PostAndMatch>> Results", By.CssSelector("#search-results-container>tr"), _driver); } }
        public UIItem ViewTrucks_Delete { get { return new UIItem("Avilable Load>>  PostAndMatch>> ViewTrucks>> Delete", By.CssSelector(".text-link.hook--remove-truck.hook--tooltip-trigger"), _driver); } }
    }
}
