
namespace SWAT.Applications.Claw.ObjectRepository
{
    using OpenQA.Selenium;
    using SWAT.FrameWork.Utilities;
    using System.Collections.Generic;
    using SWAT.Configuration;
    public class CreateLoadPage : Page
    {
        private By byPickUp = By.CssSelector("#create-load-facility-search-pickup");

        //private IWebElement pickupContactDetails = null;

        //private By customername = By.CssSelector(".select2-selection.select2-selection--multiple");
        //.select2-search__field
        private By customername = By.CssSelector(".select2-search__field");
        private By contactName = By.CssSelector("#contact-name");
        private By cityCode = By.CssSelector(".text-input.one-whole.hook--validation.hook--tooltip-bottom.hook--contact-country-code-field.hook--input-number");
        private By phoneNumber = By.CssSelector(".text-input.one-whole.hook--validation.hook--tooltip-bottom.hook--contact-phone-field.hook--input-number");
        private By ext = By.CssSelector(".text-input.one-whole.hook--validation.hook--tooltip-bottom.hook--contact-extension-field.hook--input-number");

        private By byRefNo = By.CssSelector("#reference-number");
        private By byPickUpNo = By.CssSelector("#pickup-number");
        private By byEqLength = By.CssSelector("#createload-equipment-length");
        private By byEqType = By.CssSelector("#createload-equipment-type");
        private By byPickDate = By.CssSelector("#createload-pickup-date");
        private By byPickTime = By.CssSelector("#createload-pickup-time");
        private By byNotes = By.CssSelector("#notes");
        private By byPickUpDateAvi = By.CssSelector("#createload-pickup-available");
        private By byPickUpDateBy = By.CssSelector("#createload-pickup-by");
        private By byApptTimeStart = By.CssSelector("#createload-pickup-start-time");
        private By byApptTimeEnd = By.CssSelector("#createload-pickup-end-time");
        private By isRushOrder = By.CssSelector("#createloads-rush");
        private By searchfacility = By.CssSelector("#create-load-facility-search-delivery");
        private By deliverynumber = By.XPath("//input[starts-with(@id,'reference-number-')]");
        private By byDelApptStartTime = By.CssSelector("#createload-delivery-start-time");
        private By byDelApptEndTime = By.CssSelector("#createload-delivery-end-time");
        //private By stops_ = By.XPath("//div[@id='deliverystop-compositeview-container']/div/div/div[2]/fieldset/ul");
        private By stops_ = By.XPath("//div[@id='deliverystop-compositeview-container']/div");
        private By stops_01 = By.CssSelector(".grid.one-third.form-fields");
        private By addanotherstop = By.XPath(".//*[@id='add-load-stop-button']");
        //createbutton 
        private By submit = By.CssSelector("#create-load-button");
        //Success message
        private By createanotherload = By.CssSelector("#create-another-load");
        private By gotomyloads = By.CssSelector("#goto-myLoads");
        private By loadid = By.CssSelector(".alert.alert--empty>p>a");
        private By mode = By.CssSelector(".hook--load-detail-mode");
        private By tarpType = By.CssSelector("#createload-tarp-type");
        private By tarpQuantity = By.CssSelector("#createload-tarp-quantity");
        private By loadTemplatePopup = By.CssSelector(".hook--load-template-popup");
        private By createTemplateButton = By.CssSelector("#create-template-button");
        private By templateListContainer = By.CssSelector("#template-list > li");
        private By templateName = By.CssSelector("#template-name");
        private By saveTemplateButton = By.CssSelector("#save-template-button");


        public CreateLoadPage(IWebDriver driver)
        {
            _driver = driver;
            url = "#createloads";
        }

        public CreateLoadPage(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#createloads";
        }

        public UIItem SaveTemplateButton { get { return new UIItem("Create Load>> Save Template Button", this.saveTemplateButton, _driver); } }
        public UIItem TemplateName { get { return new UIItem("Create Load>> Template Name text box", this.templateName, _driver); } }
        public UIItem LoadTemplatePopup { get { return new UIItem("Create Load>> Load Template Popup", this.loadTemplatePopup, _driver); } }
        public UIItem CreateTemplateButton { get { return new UIItem("Create Load>> Create Template Button", this.createTemplateButton, _driver); } }
        public UIItem TemplateListContainer { get { return new UIItem("Create Load>> Template List Container", this.templateListContainer, _driver); } }
        public UIItem TarpQuantity { get { return new UIItem("Create Load>> Tarp Quantity", this.tarpQuantity, _driver); } }
        public UIItem TarpType { get { return new UIItem("Create Load>> Tarp type", this.tarpType, _driver); } }
        public UIItem CustomerName { get { return new UIItem("Create Load>> Customer Name", this.customername, _driver); } }

        private By customernamehighlight = By.CssSelector(".select2-results__option.select2-results__option--highlighted");
        public UIItem CustomerNameHighLight { get { return new UIItem("", this.customernamehighlight, _driver); } }

        public UIItem PickUpFacilityRemove { get { return new UIItem("Create Load>> PickUp Facility Remove Button", By.XPath("html/body/div[1]/div/div[5]/div/div[2]/div/form/fieldset/ul/li[1]/div/div[1]/div[1]/button"), _driver); } }
        public UIItem PickUpFacility { get { return new UIItem("Create Load>> PickUp Facility", this.byPickUp, _driver); } }
        public UIItem ContactName { get { return new UIItem("Create Load>> Contact Name", this.contactName, _driver.FindElement(By.CssSelector("#createload-detail-pickup"))); } }
        public UIItem PhoneNumber { get { return new UIItem("Create Load>> Phone Number", this.phoneNumber, _driver.FindElement(By.CssSelector("#createload-detail-pickup"))); } }
        public UIItem CityCode { get { return new UIItem("Create Load>> Country Code", this.cityCode, _driver.FindElement(By.CssSelector("#createload-detail-pickup"))); } }
        public UIItem Extension { get { return new UIItem("Create Load>> Extenstion", this.ext, _driver.FindElement(By.CssSelector("#createload-detail-pickup"))); } }
        public UIItem ReferenceNumber { get { return new UIItem("Create Load>> Reference Number", this.byRefNo, _driver); } }
        public UIItem PickUpNumber { get { return new UIItem("Create Load>> PickUp Number", this.byPickUpNo, _driver); } }
        public UIItem MinimumLength { get { return new UIItem("Create Load>> Equipment Length", this.byEqLength, _driver); } }
        public UIItem EquipmentType { get { return new UIItem("Create Load>> Equipment Type", this.byEqType, _driver); } }
        public UIItem PickDate { get { return new UIItem("Create Load>> Pick Date", this.byPickDate, _driver); } }
        public UIItem PickTime { get { return new UIItem("Create Load>> PickTime", this.byPickTime, _driver); } }
        public UIItem Notes { get { return new UIItem("Create Load>> Notes", this.byNotes, _driver); } }
        public UIItem PickUpDateAvailable { get { return new UIItem("Create Load>> PickUpDate Available", this.byPickUpDateAvi, _driver); } }
        public UIItem PickUpDateBy { get { return new UIItem("Create Load>> PickUpDate By", this.byPickUpDateBy, _driver); } }
        public UIItem ApptTimeStart { get { return new UIItem("Create Load>> ApptTime Start", this.byApptTimeStart, _driver); } }
        public UIItem ApptTimeEnd { get { return new UIItem("Create Load>> ApptTime End", this.byApptTimeEnd, _driver); } }
        public UIItem IsRushOrder { get { return new UIItem("Create Load>> IsRushOrder", this.isRushOrder, _driver); } }
        public UIItem DeliveryFacility { get { return new UIItem("Create Load>> DeliveryFacility", this.searchfacility, _driver); } }
        public UIItem DeliveryNumber { get { return new UIItem("Create Load>> Delivery Number", this.deliverynumber, _driver); } }
        public UIItem DeliveryStartTime { get { return new UIItem("Create Load>> Delivery Start Time", this.byDelApptStartTime, _driver); } }
        public UIItem DeliveryEndTime { get { return new UIItem("Create Load>> Delivery End Time", this.byDelApptEndTime, _driver); } }

        public UIItem AddAnotherStop { get { return new UIItem("Create Load>> Add Another Stop", this.addanotherstop, _driver); } }
        public UIItem Mode { get { return new UIItem("Create Load>> Mode", this.mode, _driver); } }
                
        public UIItem stops{get{return new UIItem("",this.stops_,_driver);}}
        public List<Stop_CreateLoad> Stops
        {
            get
            {
                List<Stop_CreateLoad> stops = new List<Stop_CreateLoad>();
                foreach (var element in this.stops.FindElements())
                {
                    stops.Add(new Stop_CreateLoad(element));
                }
                return stops;
            }
        }

        public UIItem Submit { get { return new UIItem("", this.submit, _driver); } }
        //Success message
        public UIItem CreateAnotherLoad { get { return new UIItem("", this.createanotherload, _driver); } }
        public UIItem GoToMyLoads { get { return new UIItem("", this.gotomyloads, _driver); } }
        public UIItem LoadId { get { return new UIItem("Create Load Success Message >> LoadID", this.loadid, _driver); } }
    }

    public class Stop_CreateLoad 
    {
        private IWebElement _driver;
        private By deliveryfacility_01 = By.XPath("");

        private By deliveryfacility = By.CssSelector("#create-load-facility-search-delivery");

        private By contactName = By.CssSelector("#contact-name");
        private By cityCode = By.CssSelector(".text-input.one-whole.hook--validation.hook--tooltip-bottom.hook--contact-country-code-field.hook--input-number");
        private By phoneNumber = By.CssSelector(".text-input.one-whole.hook--validation.hook--tooltip-bottom.hook--contact-phone-field.hook--input-number");
        private By ext = By.CssSelector(".text-input.one-whole.hook--validation.hook--tooltip-bottom.hook--contact-extension-field.hook--input-number");

        private By deliverynumber = By.XPath("./div[2]/fieldset/ul/li[2]/input");

        private By deliveryavailable = By.XPath("./div[2]/fieldset/ul/li[3]/div[1]/label[2]/input");
        private By deliveryby = By.XPath("./div[2]/fieldset/ul/li[3]/div[2]/label[2]/input");

        private By appointmentstarttime = By.CssSelector("#createload-delivery-start-time");
        private By appointmentendtime = By.CssSelector("#createload-delivery-end-time");

        private By addanothercommodity = By.XPath(".//*[@id='add-commodity']");
        private By removestop = By.CssSelector(".text-link.fr.hook--delete-stop");
        //private By commodity_ = By.XPath(".//*[@id='container-deliverystop-commodity-itemview']/tr");
        private By commodity_ = By.XPath(".//tbody[@id='container-deliverystop-commodity-itemview']/tr");

        public Stop_CreateLoad(IWebElement driver)
        {
            _driver = driver;
        }
        public UIItem DeliveryFacilityRemoveButton { get { return new UIItem("Create Load>> Stops>> DeliveryFacility >> Remove Button", 
                                                        By.XPath("./div[2]/fieldset[1]/ul/li[1]/div/div[1]/div[1]/button"), _driver); } }
        public UIItem DeliveryFacility{get{return new UIItem("Create Load>> Stops>> DeliveryFacility",this.deliveryfacility,_driver);}}
        public UIItem ContactName { get { return new UIItem("Create Load>> Stops>> Contact Name", this.contactName, _driver); } }
        public UIItem PhoneNumber { get { return new UIItem("Create Load>> Stops>> Phone Number", this.phoneNumber, _driver); } }
        public UIItem CityCode { get { return new UIItem("Create Load>> Stops>> Country Code", this.cityCode, _driver); } }
        public UIItem Extension { get { return new UIItem("Create Load>> Stops>> Extenstion", this.ext, _driver); } }
        public UIItem DeliveryNumber{get{return new UIItem("Create Load>> Stops>> DeliveryNumber",this.deliverynumber,_driver);}}
        public UIItem DeliveryAvailable{get{return new UIItem("Create Load>> Stops>> DeliveryAvailable",this.deliveryavailable,_driver);}}
        public UIItem DeliveryBy { get { return new UIItem("Create Load>> Stops>> DeliverBy", this.deliveryby, _driver); } }
        public UIItem AppointmentStartTime { get { return new UIItem("Create Load>> Stops>> AppointmentStartTime", this.appointmentstarttime, _driver); } }
        public UIItem AppointmentEndTime { get { return new UIItem("Create Load>> Stops>> AppointmentStartTime", this.appointmentendtime, _driver); } }
        public UIItem AddAnotherCommodity { get { return new UIItem("Create Load>> Stops>> AddAnotherCommodity", this.addanothercommodity, _driver); } }
        public UIItem RemoveStop { get { return new UIItem("Create Load>> Stops>> RemoveStop", this.removestop, _driver); } }
        public UIItem commodity { get { return new UIItem("Create Load>> Stops>> Commodity", this.commodity_, _driver); } }
        public List<Commodity_CreateLoad> Commodity
        {
            get
            {
                List<Commodity_CreateLoad> commodities = new List<Commodity_CreateLoad>();
                foreach (var element in this.commodity.FindElements())
                {
                    commodities.Add(new Commodity_CreateLoad(element));
                }
                return commodities;
            }
        }
    }

    public class Commodity_CreateLoad 
    {
        private IWebElement _driver;

        //private By description = By.CssSelector(".one-whole.hook--validation.hook--commodity-description");
        private By description = By.CssSelector(".text-input.one-whole.hook--commodity-description");
        private By weight = By.CssSelector("#load-weight");
        private By quantity = By.CssSelector(".hook--commodity-piece.text-input.one-whole.hook--validation");
        private By packagingtyes = By.CssSelector(".one-whole.hook--commodity-packaging-type");
        private By pallets = By.CssSelector(".hook--commodity-pallets.text-input.one-whole.hook--validation");
        private By dimentsionsl = By.CssSelector(".hook--commodity-length.text-input.one-whole");
        private By dimentsionsW = By.CssSelector(".hook--commodity-width.text-input.one-whole");
        private By dimentsionsH = By.CssSelector(".hook--commodity-height.text-input.one-whole");
        private By value = By.CssSelector(".hook--commodity-value.text-input.one-whole.hook--validation");
        private By remove = By.CssSelector(".text-link.hook--tooltip-trigger.hook--delete-commodity");
        

        public  Commodity_CreateLoad(IWebElement driver)
        {
            _driver = driver;
        }

        public UIItem Description { get { return new UIItem("Create Load>> Stops>> Commodity>> Description", this.description, _driver); } }
        public UIItem Weight { get { return new UIItem("Create Load>> Stops>> Commodity>> Weight", this.weight, _driver); } }
        public UIItem Quantity { get { return new UIItem("Create Load>> Stops>> Commodity>> Quantity", this.quantity, _driver); } }
        public UIItem PackagingTypes { get { return new UIItem("Create Load>> Stops>> Commodity>> PackaginTypes", this.packagingtyes, _driver); } }
        public UIItem Pallets { get { return new UIItem("Create Load>> Stops>> Commodity>> Pallets", this.pallets, _driver); } }
        public UIItem Length { get { return new UIItem("Create Load>> Stops>> Commodity>> Length", this.dimentsionsl, _driver); } }
        public UIItem Width { get { return new UIItem("Create Load>> Stops>> Commodity>> Width", this.dimentsionsW, _driver); } }
        public UIItem Height { get { return new UIItem("Create Load>> Stops>> Commodity>> Height", this.dimentsionsH, _driver); } }
        public UIItem Value { get { return new UIItem("Create Load>> Stops>> Commodity>> Value", this.value, _driver); } }
        public UIItem Remove { get { return new UIItem("Create Load>> Stops>> Commodity>> Remove", this.remove, _driver); } }

    }
}
