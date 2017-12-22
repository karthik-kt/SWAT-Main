using OpenQA.Selenium;
using SWAT.Configuration;
using SWAT.FrameWork.Utilities;

namespace SWAT.Applications.Claw.ObjectRepository
{
    public class CreateLTLPage :Page
    {
        private By loadid = By.CssSelector(".alert.alert--empty> p > a");
        private By errorOrSuccessMessage = By.CssSelector(".alert.alert--empty");

        public CreateLTLPage(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#ltl-freight-quote/1/get-a-quote";
        }

        public UIItem Orgin_ZipCode { get { return new UIItem("Create LTL>> Quote Information>> OrginZipCode", By.CssSelector("#origin-zip"), _driver); } }
        public UIItem StartOver { get { return new UIItem("Create LTL>> Quote Information>> StartOver", By.CssSelector("#reset-form-wizard"), _driver); } }
        //.table--selectable-rows
        public UIItem SelectCarrier { get { return new UIItem("Create LTL>> Select Carrier", By.CssSelector(".table--selectable-rows"), _driver); } }
        public UIItem LoadId { get { return new UIItem("Create LTL>> LTL Load Success>> LoadId", this.loadid, _driver); } }
        public UIItem ErrorOrSuccessMessage { get { return new UIItem("Create LTL>> LTL Load Success>> ErrorOrSuccessMessage", this.errorOrSuccessMessage, _driver); } }
    }

    public class LTLCommodity : Page
    {        
        private By commoditytype = By.CssSelector("input[id^='commodity-type-']");
        private By piececount = By.CssSelector("input[id^='piece-count-']");
        private By packagingtype = By.CssSelector("select[id^='packaging-type-select-c']");
        private By weight = By.CssSelector("input[id^='commodity-weight-c']");
        private By dimensions_l = By.CssSelector("input[id^='dimension-length-c']");
        private By dimensions_w = By.CssSelector("input[id^='dimension-width-c']");
        private By dimensions_h = By.CssSelector("input[id^='dimension-height-c']");
        private By hazmatclass = By.CssSelector("select[id^='hazmat-class-select-c']");
        private By hazmatcontactphonecode = By.CssSelector("input[id^='hazmat-contact-phone-country-code-c']");
        private By hazmatcontactphone = By.CssSelector(".text-input.one-whole.add-on__item.hook--validation.hook--tooltip-bottom.hook--hazmat-contact-phone-number.hook--hazmat-phone-input");
        private By hazmatcontactphoneextn = By.CssSelector(".text-input.one-whole.add-on__item.hook--hazmat-contact-phone-extension.hook--hazmat-phone-input");
        private By freightclass = By.CssSelector("select[id^='freight-class-select-c']");
        private By nmfc = By.CssSelector("input[id^='nmfc-number-c']");
        private By nmfcsub = By.CssSelector("input[id^='nmfc-sub-number-c']");
        private By save = By.CssSelector("#submit-commodity-button");
        private By cancel = By.CssSelector(".text-link.nudge-half--left.hook--close-modal-button.hook--cancel-update");
        private By modalTitle = By.CssSelector("#modal-title");

        public LTLCommodity(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#order/entry";
        }

        public UIItem ModalTitle { get { return new UIItem("Create LTL>> Quote Information>> AddCommodity", this.modalTitle, _driver); } }
        public UIItem DeleteCommodity { get { return new UIItem("Create LTL>> Quote Information>> AddCommodity", By.CssSelector(".hook--remove-commodity-button.text-link"), _driver); } }
        public UIItem EditCommodity { get { return new UIItem("Create LTL>> Quote Information>> EditCommodity", By.CssSelector(".hook--edit-commodity-button.text-link"), _driver); } }
        public UIItem AddCommodity { get { return new UIItem("Create LTL>> Quote Information>> AddCommodity", By.CssSelector("#add-commodity"), _driver); } }
        public UIItem CommoditiesTableRow { get { return new UIItem("Create LTL>> Quote Information>> AddCommodity", By.CssSelector("#commodities-packaging-itemview-container>tr"), _driver); } }
        public UIItem CommodityType { get { return new UIItem("Create LTL>> Quote Information>> CommodityType", this.commoditytype, _driver); } }
        public UIItem PieceCount { get { return new UIItem("Create LTL>> Quote Information>> PieceCount", this.piececount, _driver); } }
        public UIItem PackagingType { get { return new UIItem("Create LTL>> Quote Information>> PackagingType", this.packagingtype, _driver); } }
        public UIItem Weight { get { return new UIItem("Create LTL>> Quote Information>> Weight", this.weight, _driver); } }
        public UIItem Dimensions_L { get { return new UIItem("Create LTL>> Quote Information>> Dimensions_L", this.dimensions_l, _driver); } }
        public UIItem Dimensions_W { get { return new UIItem("Create LTL>> Quote Information>> Dimensions_W", this.dimensions_w, _driver); } }
        public UIItem Dimensions_H { get { return new UIItem("Create LTL>> Quote Information>> Dimensions_H", this.dimensions_h, _driver); } }
        public UIItem HazmatClass { get { return new UIItem("Create LTL>> Quote Information>> HazmatClass", this.hazmatclass, _driver); } }
        public UIItem HazmatContactPhoneCode { get { return new UIItem("Create LTL>> Quote Information>> HazmatContactPhoneCode", this.hazmatcontactphonecode, _driver); } }
        public UIItem HazmatContactPhone { get { return new UIItem("Create LTL>> Quote Information>> HazmatContactPhone", this.hazmatcontactphone, _driver); } }
        public UIItem HazmatContactPhoneExtn { get { return new UIItem("Create LTL>> Quote Information>> HazmatContactPhoneExtn", this.hazmatcontactphoneextn, _driver); } }
        public UIItem FreightClass { get { return new UIItem("Create LTL>> Quote Information>> FreightClass", this.freightclass, _driver); } }
        public UIItem NMFC { get { return new UIItem("Create LTL>> Quote Information>> NMFC", this.nmfc, _driver); } }
        public UIItem NMFCSub { get { return new UIItem("Create LTL>> Quote Information>> NMFCSub", this.nmfcsub, _driver); } }
        public UIItem Save { get { return new UIItem("Create LTL>> Quote Information>> Save", this.save, _driver); } }
        public UIItem Cancel { get { return new UIItem("Create LTL>> Quote Information>> Cancel", this.cancel, _driver); } }

    }

    public class LTLQuoteInfo : Page
    {

        private By orgin_zipcode = By.CssSelector("#origin-zip");
        private By orgin_city = By.CssSelector("#origin-location-select");
        private By orgin_pickupdate = By.CssSelector("#pickup-date");
        private By orgin_accessorials = By.CssSelector("#toggle-origin-accessorials-button");
        private By orgin_accessorials_limitedaccessstop = By.CssSelector("#pickup-limited-access-stop");
        private By orgin_accessorials_inside = By.CssSelector("#InsideStop-pickup");
        private By orgin_accessorials_residential = By.CssSelector("#ResidentialStop-pickup");
        private By orgin_accessorials_tradeshow = By.CssSelector("#TradeshowStop-pickup ");
        private By orgin_accessorials_liftgate = By.CssSelector("#LiftGateStop-pickup");
        private By orgin_accessorials_appointmentrequired = By.CssSelector("#AppointmentRequired-pickup");
        private By orgin_accessorials_palletjack = By.CssSelector("#PalletJack-pickup");
        private By orgin_accessorials_sortandsegregate = By.CssSelector("#SortSegregate-pickup");
        private By orgin_accessorials_markingandtagging = By.CssSelector("#MarkingTagging-pickup");
        private By destination_zipcode = By.CssSelector("#destination-zip");
        private By destination_city = By.CssSelector("#destination-location-select");
        private By destination_accessorials = By.CssSelector("#toggle-destination-accessorials-button");
        private By destination_accessorials_limitedaccessstop = By.CssSelector("#destination-limited-access-stop");
        private By destination_accessorials_inside = By.CssSelector("#InsideStop-delivery");
        private By destination_accessorials_residential = By.CssSelector("#ResidentialStop-delivery");
        private By destination_accessorials_tradeshow = By.CssSelector("#TradeshowStop-delivery");
        private By destination_accessorials_liftgate = By.CssSelector("#LiftGateStop-delivery");
        private By destination_accessorials_appointmentrequired = By.CssSelector("#AppointmentRequired-delivery");
        private By destination_accessorials_palletjack = By.CssSelector("#PalletJack-delivery");
        private By destination_accessorials_sortandsegregate = By.CssSelector("#SortSegregate-delivery");
        private By destination_accessorials_markingandtagging = By.CssSelector("#MarkingTagging-delivery");
        private By palletcount = By.CssSelector("#pallet-count");
        private By pronumber = By.CssSelector("#pro-number");
        private By referencenumbers = By.CssSelector(".one-whole.add-on__item.hook--reference-type");
        private By referencetype = By.CssSelector(".text-input.add-on__item.one-whole.hook--reference-number");
        private By referencenumber_add = By.CssSelector("#add-reference");
        private By additionalservices_show = By.CssSelector("#toggle-additional-services-button");
        private By additionalservices_guaranteed = By.CssSelector("#Guaranteed");
        private By additionalservices_timedefinite = By.CssSelector("#TimeDefinite");
        private By additionalservices_expedite = By.CssSelector("#Expedited");
        private By additionalservices_holidaypickup = By.CssSelector("#HolidayPickup");
        private By additionalservices_holidaydelivery = By.CssSelector("#HolidayDelivery");
        private By additionalservices_weightdetermination = By.CssSelector("#WeightDetermination");
        private By additionalservices_blindshipment = By.CssSelector("#BlindShipment");
        private By additionalservices_blanketservice = By.CssSelector("#BlanketService");
        private By additionalservices_singleshipment = By.CssSelector("#SingleShipment");
        private By additionalservices_customsinbond = By.CssSelector("#CustomsInBond");
        private By additionalservices_overdimension = By.CssSelector("#OverDimension");
        private By additionalservices_stackable = By.CssSelector("#Stackable");
        private By additionalservices_turnkey = By.CssSelector("#Turnkey");
        private By additionalservices_foodgradeproducts = By.CssSelector("#FoodGradeProducts");
        private By getquotes_submit = By.CssSelector(".button.button--loud.one-whole");

        public LTLQuoteInfo(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#order/entry";
        }

        public UIItem Orgin_ZipCode { get { return new UIItem("Create LTL>> Quote Information>> OrginZipCode", this.orgin_zipcode, _driver); } }
        public UIItem Orgin_City { get { return new UIItem("Create LTL>> Quote Information>> OrginCity", this.orgin_city, _driver); } }
        public UIItem Orgin_PickupDate { get { return new UIItem("Create LTL>> Quote Information>> OrginPickupDate", this.orgin_pickupdate, _driver); } }
        public UIItem Orgin_Accessorials { get { return new UIItem("Create LTL>> Quote Information>> OrginAccessorials", this.orgin_accessorials, _driver); } }
        public UIItem Orgin_Accessorials_LimitedAccessStop { get { return new UIItem("Create LTL>> Quote Information>> OrginAccessorialsLimitedAccessStop", this.orgin_accessorials_limitedaccessstop, _driver); } }
        public UIItem Orgin_Accessorials_Inside { get { return new UIItem("Create LTL>> Quote Information>> OrginAccessorialsInside", this.orgin_accessorials_inside, _driver); } }
        public UIItem Orgin_Accessorials_Residential { get { return new UIItem("Create LTL>> Quote Information>> OrginAccessorialsResidential", this.orgin_accessorials_residential, _driver); } }
        public UIItem Orgin_Accessorials_Tradeshow { get { return new UIItem("Create LTL>> Quote Information>> OrginAccessorialsTradeshow", this.orgin_accessorials_tradeshow, _driver); } }
        public UIItem Orgin_Accessorials_LiftGate { get { return new UIItem("Create LTL>> Quote Information>> OrginAccessorialsLiftGate", this.orgin_accessorials_liftgate, _driver); } }
        public UIItem Orgin_Accessorials_AppointmentRequired { get { return new UIItem("Create LTL>> Quote Information>> OrginAccessorialsAppointmentRequired", this.orgin_accessorials_appointmentrequired, _driver); } }
        public UIItem Orgin_Accessorials_PalletJack { get { return new UIItem("Create LTL>> Quote Information>> OrginAccessorialsPalletJack", this.orgin_accessorials_palletjack, _driver); } }
        public UIItem Orgin_Accessorials_SortandSegregate { get { return new UIItem("Create LTL>> Quote Information>> OrginAccessorialsSortandSegregate", this.orgin_accessorials_sortandsegregate, _driver); } }
        public UIItem Orgin_Accessorials_MarkingandTagging { get { return new UIItem("Create LTL>> Quote Information>> OrginAccessorialsMarkingandTagging ", this.orgin_accessorials_markingandtagging, _driver); } }
        public UIItem Destination_ZipCode { get { return new UIItem("Create LTL>> Quote Information>> DestinationZipCode", this.destination_zipcode, _driver); } }
        public UIItem Destination_City { get { return new UIItem("Create LTL>> Quote Information>> DestinationCity", this.destination_city, _driver); } }
        public UIItem Destination_Accessorials { get { return new UIItem("Create LTL>> Quote Information>> DestinationAccessorials", this.destination_accessorials, _driver); } }
        public UIItem Destination_Accessorials_LimitedAccessStop { get { return new UIItem("Create LTL>> Quote Information>> DestinationAccessorialsLimitedAccessStop", this.destination_accessorials_limitedaccessstop, _driver); } }
        public UIItem Destination_Accessorials_Inside { get { return new UIItem("Create LTL>> Quote Information>> DestinationAccessorialsInside", this.destination_accessorials_inside, _driver); } }
        public UIItem Destination_Accessorials_Residential { get { return new UIItem("Create LTL>> Quote Information>> DestinationAccessorialsResidential", this.destination_accessorials_residential, _driver); } }
        public UIItem Destination_Accessorials_Tradeshow { get { return new UIItem("Create LTL>> Quote Information>> DestinationAccessorialsTradeshow", this.destination_accessorials_tradeshow, _driver); } }
        public UIItem Destination_Accessorials_LiftGate { get { return new UIItem("Create LTL>> Quote Information>> DestinationAccessorialsLiftGate", this.destination_accessorials_liftgate, _driver); } }
        public UIItem Destination_Accessorials_AppointmentRequired { get { return new UIItem("Create LTL>> Quote Information>> DestinationAccessorialsAppointmentRequired", this.destination_accessorials_appointmentrequired, _driver); } }
        public UIItem Destination_Accessorials_PalletJack { get { return new UIItem("Create LTL>> Quote Information>> DestinationAccessorialsPalletJack", this.destination_accessorials_palletjack, _driver); } }
        public UIItem Destination_Accessorials_SortandSegregate { get { return new UIItem("Create LTL>> Quote Information>> DestinationAccessorialsSortandSegregate", this.destination_accessorials_sortandsegregate, _driver); } }
        public UIItem Destination_Accessorials_MarkingandTagging { get { return new UIItem("Create LTL>> Quote Information>> DestinationAccessorialsMarkingandTagging ", this.destination_accessorials_markingandtagging, _driver); } }
        public UIItem PalletCount { get { return new UIItem("Create LTL>> Quote Information>> PalletCount", this.palletcount, _driver); } }
        public UIItem PRONumber { get { return new UIItem("Create LTL>> Quote Information>> PRONumber", this.pronumber, _driver); } }
        public UIItem ReferenceNumbers { get { return new UIItem("Create LTL>> Quote Information>> ReferenceNumbers", this.referencenumbers, _driver); } }
        public UIItem ReferenceType { get { return new UIItem("Create LTL>> Quote Information>> ReferenceType", this.referencetype, _driver); } }
        public UIItem ReferenceNumber_Add { get { return new UIItem("Create LTL>> Quote Information>> ReferenceNumberAdd", this.referencenumber_add, _driver); } }
        public UIItem AdditionalServices_Show { get { return new UIItem("Create LTL>> Quote Information>> AdditionalServicesShow", this.additionalservices_show, _driver); } }
        public UIItem AdditionalServices_Guaranteed { get { return new UIItem("Create LTL>> Quote Information>> AdditionalServicesGuaranteed", this.additionalservices_guaranteed, _driver); } }
        public UIItem AdditionalServices_TimeDefinite { get { return new UIItem("Create LTL>> Quote Information>> AdditionalServicesTimeDefinite", this.additionalservices_timedefinite, _driver); } }
        public UIItem AdditionalServices_Expedite { get { return new UIItem("Create LTL>> Quote Information>> AdditionalServicesExpedite", this.additionalservices_expedite, _driver); } }
        public UIItem AdditionalServices_HolidayPickup { get { return new UIItem("Create LTL>> Quote Information>> AdditionalServicesHolidayPickup", this.additionalservices_holidaypickup, _driver); } }
        public UIItem AdditionalServices_HolidayDelivery { get { return new UIItem("Create LTL>> Quote Information>> AdditionalServicesHolidayDelivery", this.additionalservices_holidaydelivery, _driver); } }
        public UIItem AdditionalServices_WeightDetermination { get { return new UIItem("Create LTL>> Quote Information>> AdditionalServicesWeightDetermination", this.additionalservices_weightdetermination, _driver); } }
        public UIItem AdditionalServices_BlindShipment { get { return new UIItem("Create LTL>> Quote Information>> AdditionalServicesBlindShipment", this.additionalservices_blindshipment, _driver); } }
        public UIItem AdditionalServices_BlanketService { get { return new UIItem("Create LTL>> Quote Information>> AdditionalServicesBlanketService", this.additionalservices_blanketservice, _driver); } }
        public UIItem AdditionalServices_SingleShipment { get { return new UIItem("Create LTL>> Quote Information>> AdditionalServicesSingleShipment", this.additionalservices_singleshipment, _driver); } }
        public UIItem AdditionalServices_CustomsInBond { get { return new UIItem("Create LTL>> Quote Information>> AdditionalServicesCustomsInBond", this.additionalservices_customsinbond, _driver); } }
        public UIItem AdditionalServices_OverDimension { get { return new UIItem("Create LTL>> Quote Information>> AdditionalServicesOverDimension", this.additionalservices_overdimension, _driver); } }
        public UIItem AdditionalServices_Stackable { get { return new UIItem("Create LTL>> Quote Information>> AdditionalServicesStackable", this.additionalservices_stackable, _driver); } }
        public UIItem AdditionalServices_Turnkey { get { return new UIItem("Create LTL>> Quote Information>> AdditionalServicesTurnkey", this.additionalservices_turnkey, _driver); } }
        public UIItem AdditionalServices_FoodGradeProducts { get { return new UIItem("Create LTL>> Quote Information>> AdditionalServicesFoodGradeProducts", this.additionalservices_foodgradeproducts, _driver); } }
        public UIItem GetQuotes_Submit { get { return new UIItem("Create LTL>> Quote Information>> Submit", this.getquotes_submit, _driver); } }
    }

    public class LTLSelectCarrier: Page
    {
        private By gatheringcarrierquotesmsg = By.CssSelector(".panel__body.alert.alert--empty.weight--semibold>h2");
        private By carrierquotestbl = By.XPath(".//*[@id='ltl-freight-quotes-list']/tr/td");
        public LTLSelectCarrier(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#order/entry";
        }
        public UIItem GatheringCarrierQuotesMsg { get { return new UIItem("Create LTL>> Select Carrier Quote>> GatheringCarrierQuotesMsg", this.gatheringcarrierquotesmsg, _driver); } }
        public UIItem CarrierQuoteTbl { get { return new UIItem("Create LTL>> Select Carrier Quote>> CarrierQuoteTable", this.carrierquotestbl, _driver); } }
    }

    public class LTLPickupDetails : Page
    {
        private By pickuplocation = By.CssSelector("#new-location");
        private By contactname = By.CssSelector("#contact-name-input");
        private By phonenumbercode = By.CssSelector("#phone-country-code");
        private By phonenumber = By.CssSelector("#phone-number-input");
        private By phonenumberextn = By.CssSelector("#phone-ext");
        private By email = By.CssSelector("#email-input");
        private By pickupdate = By.CssSelector("#pickup-date-input");
        private By pickuphours_open = By.CssSelector("#pickup-open-time");
        private By pickuphours_close = By.CssSelector("#pickup-close-time");
        private By pickupnumber = By.CssSelector("#pickup-number-input");
        private By specialinstructions = By.CssSelector("#special-instruction-textarea");
        private By next = By.CssSelector(".button.button--loud.one-whole");
        private By selectFacility = By.CssSelector("#selectFacility");

        public LTLPickupDetails(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#order/entry";
        }

        public UIItem PickupLocation { get { return new UIItem("Create LTL>> Quote Information>> PickupLocation", this.pickuplocation, _driver); } }
        public UIItem ContactName { get { return new UIItem("Create LTL>> Quote Information>> ContactName", this.contactname, _driver); } }
        public UIItem PhoneNumberCode { get { return new UIItem("Create LTL>> Quote Information>> PhoneNumberCode", this.phonenumbercode, _driver); } }
        public UIItem PhoneNumber { get { return new UIItem("Create LTL>> Quote Information>> PhoneNumber", this.phonenumber, _driver); } }
        public UIItem PhoneNumberExtn { get { return new UIItem("Create LTL>> Quote Information>> PhoneNumberExtn", this.phonenumberextn, _driver); } }
        public UIItem Email { get { return new UIItem("Create LTL>> Quote Information>> Email", this.email, _driver); } }
        public UIItem PickupDate { get { return new UIItem("Create LTL>> Quote Information>> PickupDate", this.pickupdate, _driver); } }
        public UIItem PickupHours_Open { get { return new UIItem("Create LTL>> Quote Information>> PickupHours_Open", this.pickuphours_open, _driver); } }
        public UIItem PickupHours_Close { get { return new UIItem("Create LTL>> Quote Information>> PickupHours_Close", this.pickuphours_close, _driver); } }
        public UIItem PickupNumber { get { return new UIItem("Create LTL>> Quote Information>> PickupNumber", this.pickupnumber, _driver); } }
        public UIItem SpecialInstructions { get { return new UIItem("Create LTL>> Quote Information>> SpecialInstructions", this.specialinstructions, _driver); } }
        public UIItem Next { get { return new UIItem("Create LTL>> Quote Information>> Next", this.next, _driver); } }
        public UIItem SelectFacility { get { return new UIItem("Create LTL >> Quote Information >> SelectFacility", this.selectFacility, _driver); } }

    }

    public class LTLNewPickupLocation :Page
    {
        private By companyname = By.CssSelector("#company-name-input");
        private By address = By.CssSelector("#primary-street-address-input");
        private By addressline2 = By.CssSelector("#secondary-street-address-input");
        private By citystate = By.CssSelector("#city-input");
        private By zipcode = By.CssSelector("#zip-input");
        private By save = By.CssSelector("#submit-button");
        private By cancel = By.CssSelector("#cancel-button");
        public LTLNewPickupLocation(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#order/entry";
        }

        public UIItem CompanyName { get { return new UIItem("Create LTL>> Quote Information>> CompanyName", this.companyname, _driver); } }
        public UIItem Address { get { return new UIItem("Create LTL>> Quote Information>> Address", this.address, _driver); } }
        public UIItem AddressLine2 { get { return new UIItem("Create LTL>> Quote Information>> AddressLine2", this.addressline2, _driver); } }
        public UIItem CityState{ get{ return new UIItem("Create LTL>> Quote Information>> City/State", this.citystate, _driver); }}
        public UIItem ZIPCode { get { return new UIItem("Create LTL>> Quote Information>> ZIPCode", this.zipcode, _driver); } }
        public UIItem Save { get { return new UIItem("Create LTL>> Quote Information>> Save", this.save, _driver); } }
        public UIItem Cancel { get { return new UIItem("Create LTL>> Quote Information>> Cancel", this.cancel, _driver); } }

    }

    public class LTLDeliveryDetails : Page
    {

        private By contactname = By.CssSelector("#contact-name-input");
        private By phonenumbercode = By.CssSelector("#phone-country-code");
        private By phonenumber = By.CssSelector("#phone-number-input");
        private By phonenumberextn = By.CssSelector("#phone-ext");
        private By email = By.CssSelector("#email-input");
        private By deliverydate = By.CssSelector("#delivery-date-input");
        private By deliveryhours_open = By.CssSelector("#delivery-open-time");
        private By deliveryhours_close = By.CssSelector("#delivery-close-time");
        private By deliverynumber = By.CssSelector("#delivery-number-input");
        private By specialinstructions = By.CssSelector("#special-instruction-textarea");
        private By next = By.CssSelector("#next-facility-button");
        private By selectFacility = By.CssSelector("#selectFacility");
        private By deliverylocation = By.CssSelector("#new-location");

        public LTLDeliveryDetails(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#order/entry";
        }

        public UIItem ContactName { get { return new UIItem("Create LTL>> Quote Information>> ContactName", this.contactname, _driver); } }
        public UIItem PhoneNumberCode { get { return new UIItem("Create LTL>> Quote Information>> PhoneNumberCode", this.phonenumbercode, _driver); } }
        public UIItem PhoneNumber { get { return new UIItem("Create LTL>> Quote Information>> PhoneNumber", this.phonenumber, _driver); } }
        public UIItem PhoneNumberExtn { get { return new UIItem("Create LTL>> Quote Information>> PhoneNumberExtn", this.phonenumberextn, _driver); } }
        public UIItem Email { get { return new UIItem("Create LTL>> Quote Information>> Email", this.email, _driver); } }
        public UIItem DeliveryDate { get { return new UIItem("Create LTL>> Quote Information>> DeliveryDate", this.deliverydate, _driver); } }
        public UIItem DeliveryHours_Open { get { return new UIItem("Create LTL>> Quote Information>> DeliveryHours_Open", this.deliveryhours_open, _driver); } }
        public UIItem DeliveryHours_Close { get { return new UIItem("Create LTL>> Quote Information>> DeliveryHours_Close", this.deliveryhours_close, _driver); } }
        public UIItem DeliveryNumber { get { return new UIItem("Create LTL>> Quote Information>> DeliveryNumber", this.deliverynumber, _driver); } }
        public UIItem SpecialInstructions { get { return new UIItem("Create LTL>> Quote Information>> SpecialInstructions", this.specialinstructions, _driver); } }
        public UIItem Next { get { return new UIItem("Create LTL>> Quote Information>> Next", this.next, _driver); } }
        public UIItem SelectFacility { get { return new UIItem("Create LTL >> Quote Information >> SelectFacility", this.selectFacility, _driver); } }
        public UIItem DeliveryLocation { get { return new UIItem("Create LTL>> Quote Information>> DeliveryLocation", this.deliverylocation, _driver); } }
    }

    public class LTLNewDeliveryLocation : Page
    {
        private By companyname = By.CssSelector("#company-name-input");
        private By address = By.CssSelector("#primary-street-address-input");
        private By addressline2 = By.CssSelector("#secondary-street-address-input");
        private By citystate = By.CssSelector("#city-input");
        private By zipcode = By.CssSelector("#zip-input");
        private By save = By.CssSelector("#submit-button");
        private By cancel = By.CssSelector("#cancel-button");
        public LTLNewDeliveryLocation(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#order/entry";
        }

        public UIItem CompanyName { get { return new UIItem("Create LTL>> Delivery Location>> CompanyName", this.companyname, _driver); } }
        public UIItem Address { get { return new UIItem("Create LTL>> Delivery Location>> Address", this.address, _driver); } }
        public UIItem AddressLine2 { get { return new UIItem("Create LTL>> Delivery Location>> AddressLine2", this.addressline2, _driver); } }
        public UIItem CityState { get { return new UIItem("Create LTL>> Delivery Location>> City/State", this.citystate, _driver); } }
        public UIItem ZIPCode { get { return new UIItem("Create LTL>> Delivery Location>> ZIPCode", this.zipcode, _driver); } }
        public UIItem Save { get { return new UIItem("Create LTL>> Delivery Location>> Save", this.save, _driver); } }
        public UIItem Cancel { get { return new UIItem("Create LTL>> Delivery Location>> Cancel", this.cancel, _driver); } }
    }

    public class LTLConfirmAndSubmit : Page
    {
        private By termsandconditions = By.CssSelector("#accept-ltl-terms-and-conditions");
        private By submitltlload = By.CssSelector("#submit-ltl-load");

        public LTLConfirmAndSubmit(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#order/entry";
        }

        public UIItem TermsAndConditions { get { return new UIItem("Create LTL>> Confirm and Submit>> TermsAndConditions", this.termsandconditions, _driver); } }
        public UIItem SubmitLTLLoad { get { return new UIItem("Create LTL>> Confirm and Submit>> Submit", this.submitltlload, _driver); } }
    }

    public class QuoteLoadPage : Page
    {
        private By customer = By.CssSelector("#customer-search-input");
        private By customerRequired = By.CssSelector(".required");
        private By customerDelete = By.CssSelector("#clear-selected-customer-button");

        public QuoteLoadPage(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#ltl-freight-quote/1/get-a-quote";
        }

        public UIItem Customer { get { return new UIItem("Quote & Create LTL Load>> Customer>>", customer, _driver); } }
        public UIItem CustomerRequired { get { return new UIItem("Quote & Create LTL Load>> Customer Required>>", customerRequired, _driver); } }
        public UIItem CustomerDeleted { get { return new UIItem("Quote & Create LTL Load>> Search Customer>>Delete", customerDelete, _driver); } }
    }
}
