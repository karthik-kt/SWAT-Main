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
using OpenQA.Selenium;
using SWAT.FrameWork.Utilities;
using System.Collections.Generic;
using SWAT.Configuration;

namespace SWAT.Applications.Claw.ObjectRepository
{
    public class CreateOrderPage : Page
    {
        private By apptitle = By.CssSelector("#app-title");
        //Contact Information 
        private By txt_contactname = By.CssSelector("#order-contact-name");
        private By txt_contactphonecountrycode = By.CssSelector("#order-contact-phone-country-code");
        private By txt_contactphonenumber = By.CssSelector("#order-contact-phone-number");
        private By txt_contactextension = By.CssSelector("#order-contact-phone-extension");
        private By txt_contactemail = By.CssSelector("#order-contact-email");
        //Route details
        private By txt_orginfacility = By.CssSelector("#order-facility-search-origin");
        private By txt_denitationfacility = By.CssSelector("#order-facility-search-destination");
        private By rdo_directioninbound = By.CssSelector("#direction-inbound");
        private By rdo_directionoutbound = By.CssSelector("#direction-outbound");
        //Date & Time
        private By cdr_pickupdate = By.CssSelector("#order-detail-pickup-date");
        private By txt_readytime = By.CssSelector("#order-detail-pickup-time");
        //Shipper details
        private By txt_referencenumber = By.CssSelector(".text-input.add-on__item.one-whole.hook--reference-number.hook--validation.hook--tooltip-right");
        private By dd_referencenumber = By.CssSelector(".add-on__item.hook--reference-type.hook--validation.hook--tooltip-right");
        private By btn_addanother = By.CssSelector("#add-reference");
        private By rdo_modetl = By.CssSelector("#order-detail-mode-tl");
        private By rdo_modeltl = By.CssSelector("#order-detail-mode-ltl");
        private By dd_equipment = By.CssSelector("#order-detail-equipment");
        private By dd_mimlength = By.CssSelector("#order-detail-equipment-length");
        private By txt_specialinstuctions = By.CssSelector("#special-instructions");
        //Shipping Unit
        private By dd_loadon = By.CssSelector("");
        private By dd_shippinguint = By.CssSelector(".panel__body");
        private By addanothershippingunit = By.CssSelector("#add-shipping-unit-button");
        //Shipping Uint 
        private By shippinguint_ = By.XPath("//div[@id='shipping-unit-itemview-container']/div");

        private By cancel = By.CssSelector("#cancel-order-button");
        private By saveorder = By.CssSelector("#create-order-button");
        private By createorderpage = By.CssSelector("#ordermanager-createorder");
        private By shiporder = By.CssSelector("#ship-order-button");
        private By customer = By.CssSelector("#order-detail-customer");
        private By gobutton = By.CssSelector("#getstarted-button");
        private By oredersaved = By.CssSelector("#order-saved-alert");

        //Success message on ShipOrder
        private By gotomyorders = By.CssSelector("#go-to-myorders");
        private By gotomyloads = By.CssSelector("#go-to-myloads");
        private By loadid = By.CssSelector(".alert.alert--empty>p>a");

        //Error message
        private By errorMessage = By.CssSelector("#display-validation-errors");

        public CreateOrderPage(IWebDriver driver)
        {
            _driver = driver;
            url ="#order/entry";
            navigationlink = null;
        }

        public CreateOrderPage(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#order/entry";
        }

        public UIItem ContactName { get { return new UIItem("Create Order>> ContactName", this.txt_contactname, _driver); } }
        public UIItem ContactPhoneCountryCode { get { return new UIItem("Create Order>> ContactPhoneCountryCode", this.txt_contactphonecountrycode, _driver); } }
        public UIItem ContactPhoneNumber { get { return new UIItem("Create Order>> Phone Number", this.txt_contactphonenumber, _driver); } }
        public UIItem ContactExtenstion { get { return new UIItem("Create Order>> ContactExtenstion", this.txt_contactextension, _driver); } }
        public UIItem ContactEmail { get { return new UIItem("Create Order>> ContactEmail", this.txt_contactemail, _driver); } }
        public UIItem OrginFacility { get { return new UIItem("Create Order>> OrginFacility", this.txt_orginfacility, _driver); } }
        public UIItem DestinationFacility { get { return new UIItem("Create Order>> DestinationFacility", this.txt_denitationfacility, _driver); } }
        public UIItem DirectionInbound { get { return new UIItem("Create Order>> DirectionInbound", this.rdo_directioninbound, _driver); } }
        public UIItem DirectionOutbound { get { return new UIItem("Create Order>> DirectionOutbound", this.rdo_directionoutbound, _driver); } }

        //Date & Time
        public UIItem PickUpDate { get { return new UIItem("Create Order>> PickUpDate", this.cdr_pickupdate, _driver); } }
        public UIItem ReadyTime { get { return new UIItem("Create Order>> ReadyTime", this.txt_readytime, _driver); } }

        //Shipper Details
        public UIItem ReferenceNumberPick { get { return new UIItem("Create Order>> ReferenceNumberPick", this.dd_referencenumber, _driver); } }
        public UIItem ReferenceNumber { get { return new UIItem("Create Order>> ReferenceNumber", this.txt_referencenumber, _driver); } }
        public UIItem AddAnother { get { return new UIItem("Create Order>> AddAnother", this.btn_addanother, _driver); } }
        public UIItem ModeTL { get { return new UIItem("Create Order>> ModeTL", this.rdo_modetl, _driver); } }
        public UIItem ModeLTL { get { return new UIItem("Create Order>> ModeLTL", this.rdo_modeltl, _driver); } }
        public UIItem Equipment { get { return new UIItem("Create Order>> Equipment", this.dd_equipment, _driver); } }
        public UIItem MiniLength { get { return new UIItem("Create Order>> MiniLength", this.dd_mimlength, _driver); } }
        public UIItem SpecialInstructions { get { return new UIItem("Create Order>> SpecialInstructions", this.txt_specialinstuctions, _driver); } }

        public UIItem AddAnotherShippingUnit { get { return new UIItem("Create Order>> AddAnotherShippingUnit", this.addanothershippingunit, _driver); } }

        private UIItem shippinguint { get { return new UIItem("Create Order>> shippinguint", this.shippinguint_, _driver); } }

        public List<ShippingUnit> ShippingUnits
        {
            get
            {
                List<ShippingUnit> shippingunits = new List<ShippingUnit>();
                foreach (var element in this.shippinguint.FindElements())
                {
                    shippingunits.Add(new ShippingUnit(element));
                }
                return shippingunits;
            }
        }

        public UIItem Cancel { get { return new UIItem("Create Order>> Cancel", this.cancel, _driver); } }
        public UIItem ShipOrder { get { return new UIItem("Create Order>> ShipOrder", this.shiporder, _driver); } }
        public UIItem CraeteOrderPage { get { return new UIItem("Create Order>> ", this.createorderpage, _driver); } }
        public UIItem SaveOrder { get { return new UIItem("Create Order>> SaveOrder", this.saveorder, _driver); } }
        public UIItem Customer { get { return new UIItem("Create Order>> Customer", this.customer, _driver); } }
        public UIItem GoButton { get { return new UIItem("Create Order>> GoButton", this.gobutton, _driver); } }
        public UIItem OrderSaved { get { return new UIItem("My Order>> Order Created", this.oredersaved, _driver); } }

        //Success message on ShipOrder
        public UIItem GoToMyOrders { get { return new UIItem("Ship Order Success Message>> GoToMyOrders", this.gotomyorders, _driver); } }
        public UIItem GoToMyLoads { get { return new UIItem("Ship Order Success Message>> GoToMyLoads", this.gotomyloads, _driver); } }
        public UIItem LoadId { get { return new UIItem("Ship Order Success Message>> LoadID", this.loadid, _driver); } }
        public UIItem ErrorMessage { get { return new UIItem("Ship Order>> Error Message", this.errorMessage, _driver); } }
    }

    public class ShippingUnit 
    {
        private IWebElement _driver;
        //details
        private By loadon = By.CssSelector("select[class*='load-on']");//By.XPath("./div/fieldset/ul/li/div/select");
        private By unitqty = By.CssSelector(".hook--field-unit-quantity");
        private By unitQtyLabel = By.XPath("./div/fieldset[1]/ul/li[2]");
        private By overdimensionyes = By.XPath("./div/fieldset/ul/li[3]/input");
        private By overdimensionno = By.XPath("./div/fieldset/ul/li[3]/input[2]");

        //Maryam changed unitdimension
        private By unitdimensionslength = By.XPath(".//input[contains(@id,'length')]");
        private By unitdimensionswidth = By.XPath(".//input[contains(@id,'width')]");
        private By unitdimensionsht = By.XPath(".//input[contains(@id,'height')]");
        

        // private By unitdimensionslength = By.XPath("./div/fieldset/ul/li[4]/div/div/div/div[2]/input");
        // private By unitdimensionswidth = By.XPath("./div/fieldset/ul/li[4]/div/div[2]/div/div[2]/input");
        // private By unitdimensionsht = By.XPath("./div/fieldset/ul/li[4]/div/div[3]/div/div[2]/input");
        

        private By stackableyes = By.XPath("./div/fieldset/ul/li[5]/input");
        private By stackableno = By.XPath("./div/fieldset/ul/li[5]/input[2]");
        //weightS
        private By weight_usecommoditysumyes = By.XPath("./div/fieldset/ul/li[7]/div/div/div/input");
        private By weight_usecommoditysum = By.XPath("./div/fieldset/ul/li[7]/div/div/div[2]/div/div/input");
        private By weight_enterweightyes = By.XPath("./div/fieldset/ul/li[7]/div/div[2]/div/input");
        private By weight_enterweight = By.XPath("./div/fieldset/ul/li[7]/div/div[2]/div[2]/div/div/input");
        //commodities
        private By commodity_ = By.XPath("./div/fieldset[2]/div/table/tbody/tr");
        private By commodity_addanothercommodity = By.XPath("./div/fieldset[2]/button");
        private By commodityGearButton = By.XPath("./div/fieldset[2]/div/table/thead/tr/th[9]/button");
        private By commoditySelectAll = By.XPath("./div/fieldset[2]/div/table/thead/tr/th[9]/ul/li/ul/li/button");
        private By commoditySelectNone = By.XPath("./div/fieldset[2]/div/table/thead/tr/th[9]/ul/li/ul/li[2]/button");
        private By commodityMove = By.XPath("./div/fieldset[2]/div/table/thead/tr/th[9]/ul/li[2]/ul/li/button");
        private By commodityDelete = By.XPath("./div/fieldset[2]/div/table/thead/tr/th[9]/ul/li[2]/ul/li[2]/button");

        private By moveCommodityButton = By.XPath("./div/fieldset[2]/div[2]");

        public ShippingUnit(IWebElement element)
        {
            _driver = element;
        }

        //Details
        public UIItem UnitQtyLabel { get { return new UIItem("Create Order>> Shipping Unit>> UnitQty Label", this.unitQtyLabel, _driver); } }
        public UIItem LoadOn { get { return new UIItem("Create Order>> Shipping Unit>> LoadOn", this.loadon, _driver); } }
        public UIItem UnitQty { get { return new UIItem("Create Order>> Shipping Unit>> UnitQty", this.unitqty, _driver); } }
        public UIItem OverDimensionYes { get { return new UIItem("Create Order>> Shipping Unit>> OverDimensionYes", this.overdimensionyes, _driver); } }
        public UIItem OverDimensionNo { get { return new UIItem("Create Order>> Shipping Unit>> OverDimensionNo", this.overdimensionno, _driver); } }
        public UIItem UnitDimensionsLength { get { return new UIItem("Create Order>> Shipping Unit>> UnitDimensionsLength", this.unitdimensionslength, _driver); } }
        public UIItem UnitDimensionWidth { get { return new UIItem("Create Order>> Shipping Unit>> UnitDimensionWidth", this.unitdimensionswidth, _driver); } }
        public UIItem UnitDimensionHeight { get { return new UIItem("Create Order>> Shipping Unit>> UnitDimensionHeight", this.unitdimensionsht, _driver); } }
        public UIItem StackableYes { get { return new UIItem("Create Order>> Shipping Unit>> StackableYes", this.stackableyes, _driver); } }
        public UIItem StackableNo { get { return new UIItem("Create Order>> Shipping Unit>> StackableNo", this.stackableno, _driver); } }

        //weight
        public UIItem Weight_UseCommoditySumYes { get { return new UIItem("Create Order>> Shipping Unit>> ", this.weight_usecommoditysumyes, _driver); } }
        public UIItem Weight_UseCommoditySum { get { return new UIItem("Create Order>> Shipping Unit>> ", this.weight_usecommoditysum, _driver); } }
        public UIItem Weight_EnterWeightYes { get { return new UIItem("Create Order>> Shipping Unit>> ", this.weight_enterweightyes,_driver); } }
        public UIItem Weight_EnterWeight { get { return new UIItem("Create Order>> Shipping Unit>> ", this.weight_enterweight, _driver); } }


        public UIItem Commodities_AddAnotherCommodity { get { return new UIItem("Create Order>> Shipping Unit>> ", this.commodity_addanothercommodity,_driver); } }
        public UIItem CommodityGearButton { get { return new UIItem("Create Order>> Shipping Unit>> Commodity Gear Button", this.commodityGearButton, _driver); } }
        public UIItem CommoditySelectAll { get { return new UIItem("Create Order>> Shipping Unit>> Commodity Select All", this.commoditySelectAll, _driver); } }
        public UIItem CommoditySelectNone { get { return new UIItem("Create Order>> Shipping Unit>> Commodity Select None", this.commoditySelectNone, _driver); } }
        public UIItem CommodityMove { get { return new UIItem("Create Order>> Shipping Unit>> Commodity Move", this.commodityMove, _driver); } }
        public UIItem CommodityDelete { get { return new UIItem("Create Order>> Shipping Unit>> Commodity Delete", this.commodityDelete, _driver); } }
        public UIItem MoveCommodityButton { get { return new UIItem("Create Order>> Shipping Unit>> Move Commodity Button", this.moveCommodityButton, _driver); } }

        private UIItem commodity { get { return new UIItem("Create Order>> Shipping Unit>> ", this.commodity_, _driver); } }
        
        public List<Commodity> Commodities
        {
            get
            {
                List<Commodity> commodities = new List<Commodity>();
                foreach (var element in this.commodity.FindElements())
                {
                    commodities.Add(new Commodity(element));
                }
                return commodities;
            }
        } 
    }

    public class Commodity
    {
        private IWebElement _driver;

        //commodities
        private By description = By.CssSelector(".hook--field-description"); //By.XPath("./td[1]/input");
        private By ponumber = By.CssSelector(".hook--field-po-number"); //By.XPath("./td[2]/input");
        private By linenumber = By.CssSelector(".hook--field-line-number"); //By.XPath("./td[3]/input");
        private By schedlinenumber = By.CssSelector(".hook--field-schedule-line-number");  //By.XPath("./td[4]/input");
        private By weight = By.CssSelector(".hook--field-weight");  //By.XPath("./td[5]/div/div[1]/input");
        private By qty = By.CssSelector(".hook--field-piece-count"); //By.XPath("./td[6]/input");
        private By value = By.CssSelector(".hook--field-purchase-order-commodity-value");  //By.XPath("./td[7]/div/div[2]/input"); 
        private By packaging = By.CssSelector(".hook--field-packaging");  //By.XPath("./td[8]/select");
        private By hazmat = By.CssSelector(".hook--field-hazmat");  //By.XPath("./td[9]/select");
        private By unitnumber = By.CssSelector(".hook--field-un-number");  //By.XPath("./td[10]/input");
        private By duedate = By.CssSelector(".hook--field-due-date");  //By.XPath("./td[11]/label/input");
        private By options = By.CssSelector(".hook--commodity-modifier");  //By.XPath("./td[12]/button[2]");
        private By item = By.CssSelector(".hook--field-item-number");

        public Commodity(IWebElement element)
        {
            _driver = element;
        }
        
        //Commodities_
        public UIItem Description { get { return new UIItem("Create Order>> Shipping Unit>> Commodity>> Description", this.description, _driver); } }
        public UIItem PONumber { get { return new UIItem("Create Order>> Shipping Unit>> Commodity>> PONumber", this.ponumber, _driver); } }
        public UIItem Item { get { return new UIItem("Create Order>> Shipping Unit>> Commodity>> Item", this.item, _driver); } }
        public UIItem LineNumber { get { return new UIItem("Create Order>> Shipping Unit>> Commodity>> LineNumber", this.linenumber, _driver); } }
        public UIItem SchedLineNumber { get { return new UIItem("Create Order>> Shipping Unit>> Commodity>> SchedLineNumber", this.schedlinenumber, _driver); } }
        public UIItem Weight { get { return new UIItem("Create Order>> Shipping Unit>> Commodity>> Weight", this.weight, _driver); } }
        public UIItem Qty { get { return new UIItem("Create Order>> Shipping Unit>> Commodity>> Qty", this.qty, _driver); } }
        public UIItem Value { get { return new UIItem("Create Order>> Shipping Unit>> Commodity>> Value", this.value, _driver); } }
        public UIItem Packaging { get { return new UIItem("Create Order>> Shipping Unit>> Commodity>> Packaging", this.packaging, _driver); } }
        public UIItem Hazmat { get { return new UIItem("Create Order>> Shipping Unit>> Commodity>> Hazmat", this.hazmat, _driver); } }
        public UIItem UnitNumber { get { return new UIItem("Create Order>> Shipping Unit>> Commodity>> UnitNumber", this.unitnumber, _driver); } }
        public UIItem DueDate { get { return new UIItem("Create Order>> Shipping Unit>> Commodity>> DueDate", this.duedate, _driver); } }
        public UIItem Option { get { return new UIItem("Create Order>> Shipping Unit>> Commodity>> Option", this.options, _driver); } }
    }
}




//Commodities_        
//public UIItem Commodities_PONumber { get { return new UIItem("", this.commodities_ponumber,_driver); } }
//public UIItem Commodities_LineNumber { get { return new UIItem("", this.commodities_linenumber,_driver); } }
//public UIItem Commodities_SchedLineNumber { get { return new UIItem("", this.commodities_schedlinenumber,_driver); } }
//public UIItem Commodities_Weight { get { return new UIItem("", this.commodities_weight,_driver); } }
//public UIItem Commodities_Qty { get { return new UIItem("", this.commodities_qty,_driver); } }
//public UIItem Commodities_Value { get { return new UIItem("", this.commodities_value,_driver); } }
//public UIItem Commodities_Packaging { get { return new UIItem("", this.commodities_packaging,_driver); } }
//public UIItem Commodities_Hazmat { get { return new UIItem("", this.commodities_hazmat,_driver); } }
//public UIItem Commodities_UnitNumber { get { return new UIItem("", this.commodities_unitnumber,_driver); } }
//public UIItem Commodities_DueDate { get { return new UIItem("", this.commodities_duedate); } }
//public UIItem Commodities_Option { get { return new UIItem("", this.commodities_options,_driver); } }
//private By commodities_ponumber = By.XPath("./div/fieldset[2]/div/table/tbody/tr/td[2]/input");
//private By commodities_linenumber = By.XPath("./div/fieldset[2]/div/table/tbody/tr/td[3]/input");
//private By commodities_schedlinenumber = By.XPath("./div/fieldset[2]/div/table/tbody/tr/td[4]/input");
//private By commodities_weight = By.XPath("./div/fieldset[2]/div/table/tbody/tr/td[5]/div/div/input");
//private By commodities_qty = By.XPath("./div/fieldset[2]/div/table/tbody/tr/td[6]/input");
//private By commodities_value = By.XPath("./div/fieldset[2]/div/table/tbody/tr/td[7]/div/div[2]/input");
//private By commodities_packaging = By.XPath("./div/fieldset[2]/div/table/tbody/tr/td[8]/div/select");
//private By commodities_hazmat = By.XPath("./div/fieldset[2]/div/table/tbody/tr/td[9]/div/select");
//private By commodities_unitnumber = By.XPath("./div/fieldset[2]/div/table/tbody/tr/td[10]/input");
//private By commodities_duedate = By.XPath("./div/fieldset[2]/div/table/tbody/tr/td[11]/label/input");
//private By commodities_options = By.XPath("./div/fieldset[2]/div/table/tbody/tr/td[12]/button[2]");