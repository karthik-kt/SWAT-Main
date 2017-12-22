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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAT.Applications.Claw.ObjectRepository
{
    using OpenQA.Selenium;
    using SWAT.FrameWork.Utilities;
    using System.Collections.Generic;
    using SWAT.Configuration;
    class MyOrderPage : Page
    {
        private By appTitle = By.CssSelector("#app-title");
        private By shipmentList = By.CssSelector("#shipments-list > li");
        private By firstOrderHeader = By.CssSelector("#shipments-list");
        private By shippingUnitContainer = By.XPath("//div[@id='shipping-unit-itemview-container']/div");
        private By saveOrder = By.CssSelector("#create-order-button");
        private By orederSaved = By.CssSelector("#order-saved-alert");
        private By orderRefSearch = By.CssSelector("#order-ref-search");
        private By searchButton = By.CssSelector("#order-search");
        private By referenceType = By.CssSelector(".hook--reference-type");
        private By referenceNumber = By.CssSelector(".hook--reference-number");
        private By tlMode = By.CssSelector("#order-detail-mode-tl");
        private By ltlMode = By.CssSelector("#order-detail-mode-ltl");
        private By equipmentType = By.CssSelector("#order-detail-equipment");
        private By equipmentLength = By.CssSelector("#order-detail-equipment-length");
        private By specialInstructions = By.CssSelector("#special-instructions");
        private By orderModelSave = By.CssSelector("#order-modal-save");
        private By orderDetailReadyDate = By.CssSelector("#order-detail-ready-date");
        private By orderDetailPickupDate = By.CssSelector("#order-detail-pickup-date");
        private By orderDetailPickupEndDate = By.CssSelector("#order-detail-pickup-date-end");
        private By headerCustomerName = By.CssSelector(".hook--display-header-customername");
        private By phoneCountryCode = By.CssSelector("#order-contact-phone-country-code");
        private By phoneNumber = By.CssSelector("#order-contact-phone-number");
        private By phoneExt = By.CssSelector("#order-contact-phone-extension");
        private By consolidationCartList = By.CssSelector(".consolidation-cart__list > li");
        private By consolidateButton = By.CssSelector(".hook--consolidate");
        private By toBeConsolidatedFlyOut = By.CssSelector(".button.hook--popup-trigger");
        private By orderContactName = By.CssSelector("#order-contact-name");
        private By expandSearch = By.CssSelector("#expand-search");
        private By duedateStartSearch = By.CssSelector("#duedate-start-search");
        private By duedateEndSearch = By.CssSelector("#duedate-end-search");
        private By facilitySearchOrigin = By.CssSelector("#order-facility-search-origin");
        private By facilitySearchDestination = By.CssSelector("#order-facility-search-destination");
        private By advancedSearchButton = By.CssSelector("#advanced-search-button");
        private By nextPage = By.CssSelector("#next-page");

        public MyOrderPage(IWebDriver driver)
        {
            _driver = driver;
            url = "#order/page/1";
            navigationlink = null;
        }

        public MyOrderPage(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#order/page/1";
        }

        public UIItem NextPage { get { return new UIItem("My Order >> nextPage", this.nextPage, _driver); } }
        public UIItem DuedateStartSearch { get { return new UIItem("My Order >> duedateStartSearch", this.duedateStartSearch, _driver); } }
        public UIItem DuedateEndSearch { get { return new UIItem("My Order >> duedateEndSearch", this.duedateEndSearch, _driver); } }
        public UIItem FacilitySearchOrigin { get { return new UIItem("My Order >> facilitySearchOrigin", this.facilitySearchOrigin, _driver); } }
        public UIItem FacilitySearchDestination { get { return new UIItem("My Order >> facilitySearchDestination", this.facilitySearchDestination, _driver); } }
        public UIItem AdvancedSearchButton { get { return new UIItem("My Order >> advancedSearchButton", this.advancedSearchButton, _driver); } }
        public UIItem ExpandSearch { get { return new UIItem("My Order >> Expand Search", this.expandSearch, _driver); } }
        public UIItem OrderContactName { get { return new UIItem("My Order >> Order Contact Name", this.orderContactName, _driver); } }
        public UIItem ToBeConsolidatedFlyOut { get { return new UIItem("My Order >> To Be Consolidated FlyOut", this.toBeConsolidatedFlyOut, _driver); } }
        public UIItem ConsolidateButton { get { return new UIItem("My Order >> Consolidation Button", this.consolidateButton, _driver); } }
        public UIItem ConsolidationCartList { get { return new UIItem("My Order >> Consolidation CartList", this.consolidationCartList, _driver); } }
        public UIItem PhoneCountryCode { get { return new UIItem("My Order >> phone Country Code", this.phoneCountryCode, _driver); } }
        public UIItem PhoneNumber { get { return new UIItem("My Order >> phone Number", this.phoneNumber, _driver); } }
        public UIItem PhoneExt { get { return new UIItem("My Order >> phone Ext", this.phoneExt, _driver); } }
        public UIItem AppTitle { get { return new UIItem("My Order >> AppTitle", this.appTitle, _driver); } }
        public UIItem ShipmentList { get { return new UIItem("My Order >> Order", this.shipmentList, _driver); } }
        public UIItem FirstOrderHeader { get { return new UIItem("My Order >> FirstOrderHeader", this.firstOrderHeader, _driver); } }
        public UIItem ShippingUnitContainer { get { return new UIItem("My Order >> Purchase Order >> Shipping Units", this.shippingUnitContainer, _driver); } }
        public UIItem OrderSaved { get { return new UIItem("My Order>> Order Saved", this.orederSaved, _driver); } }
        public UIItem SaveOrder { get { return new UIItem("My Order>> SaveOrder", this.saveOrder, _driver); } }
        public UIItem OrderRefSearch { get { return new UIItem("My Order>> Search Order By Reference", this.orderRefSearch, _driver); } }
        public UIItem SearchButton { get { return new UIItem("My Order>> Search Button", this.searchButton, _driver); } }
        public UIItem ReferenceType { get { return new UIItem("My Order >> PurchaseOrder >> Reference Type", this.referenceType, _driver); } }
        public UIItem ReferenceNumber { get { return new UIItem("My Order >> PurchaseOrder >> Reference Number", this.referenceNumber, _driver); } }
        public UIItem TlMode { get { return new UIItem("My Order >> PurchaseOrder >> TL Mode", this.tlMode, _driver); } }
        public UIItem LtlMode { get { return new UIItem("My Order >> PurchaseOrder >> LTL Mode", this.ltlMode, _driver); } }
        public UIItem EquipmentType { get { return new UIItem("My Order >> PurchaseOrder >> EquipmentType", this.equipmentType, _driver); } }
        public UIItem EquipmentLength { get { return new UIItem("My Order >> PurchaseOrder >> EquipmentLength", this.equipmentLength, _driver); } }
        public UIItem SpecialInstructions { get { return new UIItem("My Order >> PurchaseOrder >> Special Instructions", this.specialInstructions, _driver); } }
        public UIItem OrderModelSave { get { return new UIItem("My Order >> PurchaseOrder >> Notification for short shipped button", this.orderModelSave, _driver); } }
        public UIItem OrderDetailReadyDate { get { return new UIItem("My Order >> PurchaseOrder >> OrderDetail Ready Date", this.orderDetailReadyDate, _driver); } }
        public UIItem OrderDetailPickupDate { get { return new UIItem("My Order >> PurchaseOrder >> Order Detail Pickup Date", this.orderDetailPickupDate, _driver); } }
        public UIItem OrderDetailPickupEndDate { get { return new UIItem("My Order >> PurchaseOrder >> Order Detail Pickup End Date", this.orderDetailPickupEndDate, _driver); } }
        public UIItem HeaderCustomerName { get { return new UIItem("My Order >> PurchaseOrder >> Header Customer Name", this.headerCustomerName, _driver); } }
        public List<Order> Orders
        {
            get
            {
                List<Order> orders = new List<Order>();
                foreach (var element in this.ShipmentList.FindElements())
                {
                    orders.Add(new Order(element));
                }
                return orders;
            }
        }

        public List<PurchaseOrderShippingUnit> ShippingUnits
        {
            get
            {
                List<PurchaseOrderShippingUnit> shippingUnits = new List<PurchaseOrderShippingUnit>();
                foreach (var element in this.ShippingUnitContainer.FindElements())
                {
                    shippingUnits.Add(new PurchaseOrderShippingUnit(element));
                }
                return shippingUnits;
            }
        }


    }

    public class Order
    {
        private IWebElement _driver;
        private By shortShippedIcon = By.XPath(".//header/button");
        private By prepareToShip = By.XPath(".//header/div/button[2]");
        private By commodityRow = By.XPath(".//div/table/tbody/tr");
        private By toolTipsterContent = By.CssSelector(".tooltipster-content");
        private By consolidateButton = By.CssSelector(".hook--consolidate-button");
        public Order(IWebElement driver)
        {
            _driver = driver;
        }

        public UIItem ConsolidateButton { get { return new UIItem("My Order >> Order >> Header >> Consolidate Button", this.consolidateButton, _driver); } }
        public UIItem ShortShippedIcon { get { return new UIItem("My Order >> Order >> Header >> ShortShippedIcon", this.shortShippedIcon, _driver); } }
        public UIItem PrepareToShip { get { return new UIItem("My Order >> Order >> Header >> PrepareToShip", this.prepareToShip, _driver); } }
        public UIItem CommodityRow { get { return new UIItem("My Order >> Order >> Commodities >> CommodityRow", this.commodityRow, _driver); } }
        public UIItem ToolTipsterContent { get { return new UIItem("My Order >> Tooltipster", this.toolTipsterContent, _driver); } }

        public List<OrderCommodity> OrderCommodities
        {
            get
            {
                List<OrderCommodity> orderCommodities = new List<OrderCommodity>();
                foreach (var element in this.CommodityRow.FindElements())
                {
                    orderCommodities.Add(new OrderCommodity(element));
                }
                return orderCommodities;
            }

        }
    }

    public class OrderCommodity
    {
        private IWebElement _driver;
        private By reference = By.XPath(".//td[1]");
        private By origQty = By.XPath(".//td[2]");
        private By expQty = By.XPath(".//td[3]");
        private By actQty = By.XPath(".//td[4]");
        private By description = By.XPath(".//td[5]");
        private By shipper = By.XPath(".//td[7]");
        public OrderCommodity(IWebElement driver)
        {
            _driver = driver;
        }
        public UIItem Reference { get { return new UIItem("My Order >> Reference", this.reference, _driver); } }
        public UIItem OrigQty { get { return new UIItem("My Order >> OrigQty", this.origQty, _driver); } }
        public UIItem ExpQty { get { return new UIItem("My Order >> ExpQty", this.expQty, _driver); } }
        public UIItem ActQty { get { return new UIItem("My Order >> Reference", this.actQty, _driver); } }
        public UIItem Description { get { return new UIItem("My Order >> Reference", this.description, _driver); } }
        public UIItem Shipper { get { return new UIItem("My Order >> Reference", this.shipper, _driver); } }
    }

    public class PurchaseOrderShippingUnit
    {
        private IWebElement _driver;
        //details
        private By loadon = By.CssSelector("select[class*='load-on']");//By.XPath("./div/fieldset/ul/li/div/select");
        private By unitqty = By.CssSelector(".hook--field-unit-quantity");

        private By unitdimensionslength = By.CssSelector(".hook--field-length");
        private By unitdimensionswidth = By.CssSelector(".hook--field-width");
        private By unitdimensionsht = By.CssSelector(".hook--field-height");

        //commodities
        private By commodity_ = By.XPath("./div/fieldset[2]/div/table/tbody/tr");
        private By commodity_addanothercommodity = By.CssSelector(".hook--button-add-commodity");
        private By origQtyHeader = By.XPath("./div/fieldset[2]/div/table/thead/tr/th[6]");

        public PurchaseOrderShippingUnit(IWebElement element)
        {
            _driver = element;
        }

        //Details
        public UIItem OrigQtyHeader { get { return new UIItem("Create Order>> Shipping Unit>> origQtyHeader", this.origQtyHeader, _driver); } }
        public UIItem LoadOn { get { return new UIItem("Create Order>> Shipping Unit>> LoadOn", this.loadon, _driver); } }
        public UIItem UnitQty { get { return new UIItem("Create Order>> Shipping Unit>> UnitQty", this.unitqty, _driver); } }
        public UIItem UnitDimensionsLength { get { return new UIItem("Create Order>> Shipping Unit>> UnitDimensionsLength", this.unitdimensionslength, _driver); } }
        public UIItem UnitDimensionWidth { get { return new UIItem("Create Order>> Shipping Unit>> UnitDimensionWidth", this.unitdimensionswidth, _driver); } }
        public UIItem UnitDimensionHeight { get { return new UIItem("Create Order>> Shipping Unit>> UnitDimensionHeight", this.unitdimensionsht, _driver); } }

        public UIItem Commodities_AddAnotherCommodity { get { return new UIItem("Create Order>> Shipping Unit>> ", this.commodity_addanothercommodity, _driver); } }

        private UIItem Commodity { get { return new UIItem("Create Order>> Shipping Unit>> ", this.commodity_, _driver); } }

        public List<PurchaseOrderCommodity> Commodities
        {
            get
            {
                List<PurchaseOrderCommodity> commodities = new List<PurchaseOrderCommodity>();
                foreach (var element in this.Commodity.FindElements())
                {
                    commodities.Add(new PurchaseOrderCommodity(element));
                }
                return commodities;
            }
        }
    }

    public class PurchaseOrderCommodity
    {
        private IWebElement _driver;

        //commodities
        private By description = By.CssSelector(".hook--field-description");
        private By ponumber = By.CssSelector(".hook--field-po-number");
        private By linenumber = By.CssSelector(".hook--field-line-number");
        private By schedlinenumber = By.CssSelector(".hook--field-schedule-line-number");
        private By itemnumber = By.CssSelector(".hook--field-item-number");
        private By weight = By.CssSelector(".hook--field-weight");
        private By actQty = By.CssSelector(".hook--field-piece-count");
        private By value = By.CssSelector(".hook--field-purchase-order-commodity-value");
        private By packaging = By.CssSelector(".hook--field-packaging");
        private By hazmat = By.CssSelector(".hook--field-hazmat");
        private By unitnumber = By.CssSelector(".hook--field-un-number");
        private By duedate = By.CssSelector(".hook--field-due-date");
        private By options = By.CssSelector(".hook--commodity-modifier");
        private By expQty = By.XPath(".//td[5]");
        private By origQty = By.XPath(".//td[4]");
        private By expQtyWhenNoOriginal = By.XPath(".//td[6]");
        private By expQtyWhenOriginal = By.XPath(".//td[7]");
        public PurchaseOrderCommodity(IWebElement element)
        {
            _driver = element;
        }

        //Commodities_
        public UIItem ExpQtyWhenOriginal { get { return new UIItem("My Order>> Purchase Order >> Shipping Unit>> Commodity>> facility Exp Qty", this.expQtyWhenOriginal, _driver); } }
        public UIItem ExpQtyWhenNoOriginal { get { return new UIItem("My Order>> Purchase Order >> Shipping Unit>> Commodity>> facility Exp Qty", this.expQtyWhenNoOriginal, _driver); } }
        public UIItem Description { get { return new UIItem("My Order>> Purchase Order >> Shipping Unit>> Commodity>> Description", this.description, _driver); } }
        public UIItem PONumber { get { return new UIItem("My Order>> Purchase Order >> Shipping Unit>> Commodity>> PONumber", this.ponumber, _driver); } }
        public UIItem LineNumber { get { return new UIItem("My Order>> Purchase Order >> Shipping Unit>> Commodity>> LineNumber", this.linenumber, _driver); } }
        public UIItem SchedLineNumber { get { return new UIItem("My Order>> Purchase Order >> Shipping Unit>> Commodity>> SchedLineNumber", this.schedlinenumber, _driver); } }
        public UIItem ItemNumber { get { return new UIItem("My Order>> Purchase Order >> Shipping Unit>> Commodity>> ItemNumber", this.itemnumber, _driver); } }
        public UIItem Weight { get { return new UIItem("My Order>> Purchase Order >> Shipping Unit>> Commodity>> Weight", this.weight, _driver); } }
        public UIItem ActQty { get { return new UIItem("My Order>> Purchase Order >> Shipping Unit>> Commodity>> Qty", this.actQty, _driver); } }
        public UIItem Value { get { return new UIItem("My Order>> Purchase Order >> Shipping Unit>> Commodity>> Value", this.value, _driver); } }
        public UIItem Packaging { get { return new UIItem("My Order>> Purchase Order >> Shipping Unit>> Commodity>> Packaging", this.packaging, _driver); } }
        public UIItem Hazmat { get { return new UIItem("My Order>> Purchase Order >> Shipping Unit>> Commodity>> Hazmat", this.hazmat, _driver); } }
        public UIItem UnitNumber { get { return new UIItem("My Order>> Purchase Order >> Shipping Unit>> Commodity>> UnitNumber", this.unitnumber, _driver); } }
        public UIItem DueDate { get { return new UIItem("My Order>> Purchase Order >> Shipping Unit>> Commodity>> DueDate", this.duedate, _driver); } }
        public UIItem Option { get { return new UIItem("My Order>> Purchase Order >> Shipping Unit>> Commodity>> Option", this.options, _driver); } }
        public UIItem ExpQty { get { return new UIItem("My Order>> Purchase Order >> Shipping Unit>> Commodity>> Option", this.expQty, _driver); } }
        public UIItem OrigQty { get { return new UIItem("My Order>> Purchase Order >> Shipping Unit>> Commodity>> Option", this.origQty, _driver); } }
    }
}
