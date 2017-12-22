using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAT.Applications.Claw.ObjectRepository
{
    using OpenQA.Selenium;
    using SWAT.Configuration;
    using SWAT.FrameWork.Utilities;

    class ManageCustomersPage : Page
    {
        public ManageCustomersPage(IWebDriver driver)
        {
            _driver = driver;
            //url = "#managecustomers";

        }

        public ManageCustomersPage(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            //url = "#managecustomers";

        }

        // Currently check-box only settings have been covered. Need to add support for text only and check-box + text settings.
        // Will be covered as and when required.
        private By appTitle = By.CssSelector("#app-title");
        private By backButton = By.CssSelector("#customer-back-button");
        private By facilityRelationships = By.CssSelector("#field-48");
        private By enableSchedulingInbound = By.CssSelector("#inbound-checkbox");
        private By enableSchedulingOutbound = By.CssSelector("#outbound-checkbox");
        private By enableFacilityCalendar = By.CssSelector("#facility-calendar");
        private By automationAndTms = By.CssSelector("#automation-and-tms");
        private By tenderSettingsAddCarrierForTrackingOnly = By.CssSelector("#tender-setting-add-carrier-for-tracking-only-checkbox");
        private By tenderSettingsSequenceForLtlTenderingBasedOnRoutingGuideNotOnCost = By.CssSelector("#tender-setting-keep-ltl-routing-guide-sequence-checkbox");
        private By tenderSettingsHideCustomerName = By.CssSelector("#tender-setting-hide-customer-name-checkbox");
        private By tenderSettingsHideCarrierRate = By.CssSelector("#tender-setting-hide-carrier-rate-checkbox");
        private By tenderSettingsUseCustomerRepAsCarrierRep = By.CssSelector("#tender-setting-use-customer-rep-as-carrier-rep-checkbox");
        private By tenderSettingsUseCarrierCostAsCustomerRate = By.CssSelector("#tender-setting-use-carrier-cost-as-customer-rate-checkbox");
        private By tenderSettingsSendRateConfirmationToCarrier = By.CssSelector("#tender-setting-send-rate-confirmation-to-carrier-checkbox");
        private By tenderSettingsDoNotUseAutomaticCarrierRatingForManagedLtlLoads = By.CssSelector("#tms-override-automatic-ltl-workflow-checkbox");
        private By tenderSettingsSkipLtlCarriersWhoAreUnableToMakeDeliveryDate = By.CssSelector("#tms-on-time-transit-time-ltl-carriers-only-checkbox");
        private By tenderSettingsApplyFscByDefault = By.CssSelector("#tms-apply-fsc-by-default-checkbox");
        private By tenderSettingsIgnoreCarrierQualificationErrorsWhenTendering = By.CssSelector("#tms-ignore-carrier-qualification-errors-checkbox");
        private By spotTenderTimeLimitInMin = By.CssSelector("#spot-tender-time-limit-in-min");
        private By routingGuideExhaustedNotifyLoadCustomerOpsReps = By.CssSelector("#tms-notify-load-customer-ops-reps-checkbox");
        private By routingGuideExhaustedNotifyMainCustomerContact = By.CssSelector("#tms-notify-main-customer-contact-checkbox");
        private By routingGuideExhaustedSendToTheOpenBoard = By.CssSelector("#tms-send-to-open-board-checkbox");
        private By enableOrderManager = By.CssSelector("#claw-ordering");
        private By modeIntermodal = By.CssSelector("#field-3-IMDL");
        private By modeLessThanTruckload = By.CssSelector("#field-3-LTL");
        private By modeTruckload = By.CssSelector("#field-3-TL");
        private By equipmentAutoTrailer = By.CssSelector("#field-4-42");
        private By equipmentBeamTrailer = By.CssSelector("#field-4-43");
        private By equipmentBoxcar = By.CssSelector("#field-4-35");
        private By equipmentBulk = By.CssSelector("#field-4-13");
        private By equipmentCNRU = By.CssSelector("#field-4-19");
        private By equipmentConestoga = By.CssSelector("#field-4-29");
        private By equipmentContainer = By.CssSelector("#field-4-5");
        private By equipmentCPPU = By.CssSelector("#field-4-20");
        private By equipmentCSXU = By.CssSelector("#field-4-21");
        private By equipmentDeckedReefer = By.CssSelector("#field-4-30");
        private By equipmentDoubleDropLowboy = By.CssSelector("#field-4-8");
        private By equipmentDropFrame = By.CssSelector("#field-4-7");
        private By equipmentDuraplateVan = By.CssSelector("#field-4-39");
        private By equipmentEMHU = By.CssSelector("#field-4-22");
        private By equipmentEMPU = By.CssSelector("#field-4-23");
        private By equipmentEMWU = By.CssSelector("#field-4-24");
        private By equipmentEPTY = By.CssSelector("#field-4-25");
        private By equipmentEuroliner = By.CssSelector("#field-4-11");
        private By equipmentFlatCar = By.CssSelector("#field-4-38");
        private By equipmentFlatbed = By.CssSelector("#field-4-3");
        private By equipmentFlatbedStretch = By.CssSelector("#field-4-48");
        private By equipmentFlatbedWithSides = By.CssSelector("#field-4-32");
        private By equipmentFlatbedWithTarp = By.CssSelector("#field-4-10");
        private By equipmentHeatedVan = By.CssSelector("#field-4-31");
        private By equipmentHotShot = By.CssSelector("#field-4-45");
        private By equipmentLandall = By.CssSelector("field-4-40");
        private By equipmentMaxiFlatbed = By.CssSelector("#field-4-37");
        private By equipmentMaxiVan = By.CssSelector("field-4-36");
        private By equipmentMegatrailer = By.CssSelector("#field-4-16");
        private By equipmentPACU = By.CssSelector("#field-4-26");
        private By equipmentPowerOnly = By.CssSelector("#field-4-18");
        private By equipmentReefer = By.CssSelector("#field-4-2");
        private By equipmentReeferContainer = By.CssSelector("#field-4-44");
        private By equipmentRemovableGooseNeck = By.CssSelector("#field-4-28");
        private By equipmentRGNMultiAxel = By.CssSelector("#field-4-46");
        private By equipmentRGNStretch = By.CssSelector("#field-4-47");
        private By equipmentRoadTrain = By.CssSelector("#field-4-17");
        private By equipmentSiloTanker = By.CssSelector("#field-4-34");
        private By equipmentSoftShell = By.CssSelector("#field-4-9");
        private By equipmentStepDeck = By.CssSelector("#field-4-6");
        private By equipmentStepDeckStretch = By.CssSelector("#field-4-49");
        private By equipmentStepdeckWithRamps = By.CssSelector("#field-4-41");
        private By equipmentTanker = By.CssSelector("#field-4-15");
        private By equipmentTilt = By.CssSelector("#field-4-12");
        private By equipmentVan = By.CssSelector("#field-4-1");
        private By equipmentVentedVan = By.CssSelector("#field-4-27");
        private By equipmentWalkingFloor = By.CssSelector("#field-4-33");
        private By packagingTypesBag = By.CssSelector("#field-44-10");
        private By packagingTypesBale = By.CssSelector("#field-44-5");
        private By packagingTypesBarge = By.CssSelector("#field-44-15");
        private By packagingTypesBarrel = By.CssSelector("#field-44-16");
        private By packagingTypesBasketOrHamper = By.CssSelector("#field-44-17");
        private By packagingTypesBeam = By.CssSelector("#field-44-18");
        private By packagingTypesBin = By.CssSelector("#field-44-19");
        private By packagingTypesBobbin = By.CssSelector("#field-44-20");
        private By packagingTypesBottle = By.CssSelector("#field-44-21");
        private By packagingTypesBox = By.CssSelector("#field-44-3");
        private By packagingTypesBoxWithInnerContainer = By.CssSelector("#field-44-22");
        private By packagingTypesBucket = By.CssSelector("#field-44-23");
        private By packagingTypesBulk = By.CssSelector("#field-44-12");
        private By packagingTypesBulkBag = By.CssSelector("#field-44-24");
        private By packagingTypesBundle = By.CssSelector("#field-44-11");
        private By packagingTypesCabinet = By.CssSelector("#field-44-25");
        private By packagingTypesCage = By.CssSelector("#field-44-26");
        private By packagingTypesCan = By.CssSelector("#field-44-27");
        private By packagingTypesCanCase = By.CssSelector("#field-44-28");
        private By packagingTypesCarLoadRail = By.CssSelector("#field-44-29");
        private By packagingTypesCarboy = By.CssSelector("#field-44-30");
        private By packagingTypesCarrier = By.CssSelector("#field-44-31");
        private By packagingTypesCarton = By.CssSelector("#field-44-6");
        private By packagingTypesCase = By.CssSelector("#field-44-1");
        private By packagingTypesCask = By.CssSelector("#field-44-32");
        private By packagingTypesCheeses = By.CssSelector("#field-44-33");
        private By packagingTypesChest = By.CssSelector("#field-44-34");
        private By packagingTypesCoil = By.CssSelector("#field-44-7");
        private By packagingTypesCones = By.CssSelector("#field-44-35");
        private By packagingTypesContainer = By.CssSelector("#field-44-36");
        private By packagingTypesContainersOfBulkCargo = By.CssSelector("#field-44-37");
        private By packagingTypesCore = By.CssSelector("#field-44-38");
        private By packagingTypesCradle = By.CssSelector("#field-44-39");
        private By packagingTypesCrate = By.CssSelector("#field-44-40");
        private By packagingTypesCube = By.CssSelector("#field-44-41");
        private By packagingTypesCylinder = By.CssSelector("#field-44-42");
        private By packagingTypesDoubleLengthRack = By.CssSelector("#field-44-43");
        private By packagingTypesDoubleLengthSkid = By.CssSelector("#field-44-44");
        private By packagingTypesDoubleLengthToteBin = By.CssSelector("#field-44-45");
        private By packagingTypesDrum = By.CssSelector("#field-44-8");
        private By packagingTypesDryBulk = By.CssSelector("#field-44-13");
        private By packagingTypesDuffleBag = By.CssSelector("#field-44-46");
        private By packagingTypesEngineContainer = By.CssSelector("#field-44-47");
        private By packagingTypesEnvelope = By.CssSelector("#field-44-48");
        private By packagingTypesFirkin = By.CssSelector("#field-44-49");
        private By packagingTypesFlask = By.CssSelector("#field-44-50");
        private By packagingTypesFloBin = By.CssSelector("#field-44-51");
        private By packagingTypesForwardReel = By.CssSelector("#field-44-52");
        private By packagingTypesFrame = By.CssSelector("#field-44-53");
        private By packagingTypesGarmentsOnHangers = By.CssSelector("#field-44-54");
        private By packagingTypesHalfStandardRack = By.CssSelector("#field-44-55");
        private By packagingTypesHalfStandardToteBin = By.CssSelector("#field-44-56");
        private By packagingTypesHamper = By.CssSelector("#field-44-57");
        private By packagingTypesHeadsOfBeef = By.CssSelector("#field-44-58");
        private By packagingTypesHogshead = By.CssSelector("#field-44-59");
        private By packagingTypesHopperTruck = By.CssSelector("#field-44-60");
        private By packagingTypesIntermediateBulkContainers = By.CssSelector("#field-44-105");
        private By packagingTypesJar = By.CssSelector("#field-44-61");
        private By packagingTypesJug = By.CssSelector("#field-44-62");
        private By packagingTypesKeg = By.CssSelector("#field-44-63");
        private By packagingTypesKit = By.CssSelector("#field-44-64");
        private By packagingTypesKnockdownRack = By.CssSelector("#field-44-65");
        private By packagingTypesKnockdownToteBin = By.CssSelector("#field-44-66");
        private By packagingTypesLiftVan = By.CssSelector("#field-44-67");
        private By packagingTypesLifts = By.CssSelector("#field-44-68");
        private By packagingTypesLinerBagDry = By.CssSelector("#field-44-69");
        private By packagingTypesLinerBagLiquid = By.CssSelector("#field-44-70");
        private By packagingTypesLiquidBulk = By.CssSelector("#field-44-14");
        private By packagingTypesLog = By.CssSelector("#field-44-71");
        private By packagingTypesLoose = By.CssSelector("#field-44-72");
        private By packagingTypesLug = By.CssSelector("#field-44-73");
        private By packagingTypesMilitaryVan = By.CssSelector("#field-44-74");
        private By packagingTypesMixedTypePack = By.CssSelector("#field-44-75");
        private By packagingTypesMultiRollPack = By.CssSelector("#field-44-76");
        private By packagingTypesMultiwallContainerSecuredToWarehousePallet = By.CssSelector("#field-44-77");
        private By packagingTypesNoil = By.CssSelector("#field-44-78");
        private By packagingTypesOnHangerOrRackInBoxes = By.CssSelector("#field-44-79");
        private By packagingTypesOverwrap = By.CssSelector("#field-44-80");
        private By packagingTypesPackage = By.CssSelector("#field-44-81");
        private By packagingTypesPail = By.CssSelector("#field-44-82");
        private By packagingTypesPallet = By.CssSelector("#field-44-83");
        private By packagingTypesPieces = By.CssSelector("#field-44-84");
        private By packagingTypesPims = By.CssSelector("#field-44-85");
        private By packagingTypesPipeRack = By.CssSelector("#field-44-86");
        private By packagingTypesPlatform = By.CssSelector("#field-44-87");
        private By packagingTypesPrivateVehicle = By.CssSelector("#field-44-88");
        private By packagingTypesRack = By.CssSelector("#field-44-89");
        private By packagingTypesReel = By.CssSelector("#field-44-90");
        private By packagingTypesRoll = By.CssSelector("#field-44-2");
        private By packagingTypesSack = By.CssSelector("#field-44-4");
        private By packagingTypesSkid = By.CssSelector("#field-44-91");
        private By packagingTypesSleeve = By.CssSelector("#field-44-92");
        private By packagingTypesSlipSheet = By.CssSelector("#field-44-93");
        private By packagingTypesSpool = By.CssSelector("#field-44-94");
        private By packagingTypesTank = By.CssSelector("#field-44-95");
        private By packagingTypesToteBin = By.CssSelector("#field-44-9");
        private By packagingTypesToteCan = By.CssSelector("#field-44-96");
        private By packagingTypesTray = By.CssSelector("#field-44-97");
        private By packagingTypesTriwallBox = By.CssSelector("#field-44-98");
        private By packagingTypesTruck = By.CssSelector("#field-44-99");
        private By packagingTypesTub = By.CssSelector("#field-44-100");
        private By packagingTypesTube = By.CssSelector("#field-44-101");
        private By packagingTypesUnit = By.CssSelector("#field-44-102");
        private By packagingTypesVanPack = By.CssSelector("#field-44-103");
        private By packagingTypesVehicles = By.CssSelector("#field-44-104");
        private By equipmentLengths20 = By.CssSelector("#field-45-1");
        private By equipmentLengths40 = By.CssSelector("#field-45-2");
        private By equipmentLengths45 = By.CssSelector("#field-45-3");
        private By equipmentLengths48 = By.CssSelector("#field-45-4");
        private By equipmentLengths53 = By.CssSelector("#field-45-5");
        private By shippingUnitStackable = By.CssSelector("#field-1");
        private By shippingUnitOverDimension = By.CssSelector("#field-2");
        private By commoditiesThreeFieldSKU = By.CssSelector("#field-20");
        private By commoditiesShowDueDate = By.CssSelector("#field-21");
        private By referenceTypeBillOfLading = By.CssSelector("#field-19-BL");
        private By referenceTypeDelivery = By.CssSelector("#field-19-DL");
        private By referenceTypePickUp = By.CssSelector("#field-19-PU");
        private By referenceTypePurchaseOrder = By.CssSelector("#field-19-PO");
        private By referenceTypeSalesOrder = By.CssSelector("#field-19-SO");
        private By ltlReferenceTypeBillOfLading = By.CssSelector("#field-50-BOL");
        private By ltlReferenceTypeCommercialInvoice = By.CssSelector("#field-50-CI");
        private By ltlReferenceTypeDistributor = By.CssSelector("#field-50-DS");
        private By ltlReferenceTypeFranchiseId = By.CssSelector("#field-50-FI");
        private By ltlReferenceTypeKomModelNumber = By.CssSelector("#field-50-KMN");
        private By ltlReferenceTypeKomSalesOrder = By.CssSelector("#field-50-KSO");
        private By ltlReferenceTypeKomSerialNumber = By.CssSelector("#field-50-KSN");
        private By ltlReferenceTypeMasterBOLNumber = By.CssSelector("#field-50-MBN");
        private By ltlReferenceTypeOrderNumber = By.CssSelector("#field-50-ON");
        private By ltlReferenceTypePONumber = By.CssSelector("#field-50-PN");
        private By ltlReferenceTypePRONumber = By.CssSelector("#field-50-PR");
        private By ltlReferenceTypeQuoteNumber = By.CssSelector("#field-50-QN");
        private By ltlReferenceTypeShipmentNumber = By.CssSelector("#field-50-SN");
        private By ltlReferenceTypeVendorCode = By.CssSelector("#field-50-VC");
        private By myLoadsDocumentVisibility = By.CssSelector("#field-42");
        private By myLoadsHideManagedChargesFromCarrier = By.CssSelector("#hide-managed-charges-from-carrier-checkbox");
        private By myLoadsHideAllChargesFromCustomer = By.CssSelector("#hide-rates-on-my-loads-checkbox");
        private By myLoadsHideAllCarrierTrackingNotesFromCustomerAndFacilities = By.CssSelector("#hide-carrier-tracking-notes-on-my-loads-checkbox");
        private By myLoadsAllowCarriersToEditBOLAndPODNumbers = By.CssSelector("#field-47");
        private By myLoadsAllowUsersToUpdateLoadStopTimesInAnyOrder = By.CssSelector("#field-49");
        private By premiumReporting = By.CssSelector("#field-43");
        private By additionalSettingsShowFacilityHours = By.CssSelector("#field-54");
        private By additionalSettingsShowTenderHistory = By.CssSelector("#field-56");
        private By additionalSettingsShowCommodityValue = By.CssSelector("#field-57");
        private By additionalSettingsShowLoadStopNotes = By.CssSelector("#field-55");
        private By additionalSettingsShowAppointmentSchedule = By.CssSelector("#field-58");
        private By additionalSettingsShowAllCustomerChildren = By.CssSelector("#field-59");
        private By excludeWeekendsForShipmentOrder = By.CssSelector("#field-63");
        private By paddedDaysForPastDueOrders = By.CssSelector("#field-23");
        private By createLoadUsingOptimizationTool = By.CssSelector("#field-51");
        private By cutOffTimeForPastDueOrders = By.CssSelector("#field-22");
        private By cutOffTimeToSubmitOrderForShipping = By.CssSelector("#field-62");
        private By standardLeadTime = By.CssSelector("#field-60");
        private By leadTimeAfterCutOff = By.CssSelector("#field-61");
        private By standardLeadTimeLabel = By.XPath("//*[@id='view--details-itemview']/div/fieldset[7]/div/ul[2]/li[12]/label");
        private By tenderSettingText = By.CssSelector("#setting-details-region > div > div:nth-child(1) > div.panel__body > fieldset > span");
        private By actualWeightVary = By.CssSelector("#field-24");
        private By actualWeightVaryText = By.CssSelector("#field-26");
        private By actualPiecesVary = By.CssSelector("#field-27");
        private By actualPiecesVaryText = By.CssSelector("#field-28");
        private By stopUserFromSaving = By.CssSelector("#field-16");
        private By customerSummaryDetailsRegion = By.CssSelector("#customer-summary-details-region");


        public UIItem CustomerSummaryDetailsRegion { get { return new UIItem("Manage Customers>> Customer Summary Details Region", this.customerSummaryDetailsRegion, _driver); } }
        public UIItem StopUserFromSaving { get { return new UIItem("Manage Customers>> Stop User From Saving", this.stopUserFromSaving, _driver); } }
        public UIItem ActualPiecesVaryText { get { return new UIItem("Manage Customers>> Actual Pieces Vary TextBox", this.actualPiecesVaryText, _driver); } }
        public UIItem ActualPiecesVary { get { return new UIItem("Manage Customers>> Actual Pieces Vary", this.actualPiecesVary, _driver); } }
        public UIItem ActualWeightVaryText { get { return new UIItem("Manage Customers>> Actual Weight Vary TextBox", this.actualWeightVaryText, _driver); } }
        public UIItem ActualWeightVary { get { return new UIItem("Manage Customers>> Actual Weight Vary", this.actualWeightVary, _driver); } }
        public UIItem TenderSettingText { get { return new UIItem("Manage Customers>> Tender Setting Text", this.tenderSettingText, _driver); } }
        public UIItem AutomationAndTms { get { return new UIItem("Manage Customers>> Automation And Tms", this.automationAndTms, _driver); } }
        public UIItem AppTitle { get { return new UIItem("Manage Customers>> App Title", this.appTitle, _driver); } }
        public UIItem BackButton { get { return new UIItem("Manage Customers>> Back Buttom", this.backButton, _driver); } }
        public UIItem FacilityRelationships { get { return new UIItem("Manage Customers>> Facility Relationships", this.facilityRelationships, _driver); } }
        public UIItem EnableSchedulingInbound { get { return new UIItem("Manage Customers>> Enable Scheduling Inbound", this.enableSchedulingInbound, _driver); } }
        public UIItem EnableSchedulingOutbound { get { return new UIItem("Manage Customers>> Enable Scheduling Outbound", this.enableSchedulingOutbound, _driver); } }
        public UIItem EnableFacilityCalendar { get { return new UIItem("Manage Customers>> Enable Facility Calendar", this.enableFacilityCalendar, _driver); } }
        public UIItem TenderSettingsAddCarrierForTrackingOnly { get { return new UIItem("Manage Customers>> Tender Settings Add Carrier For Tracking Only", this.tenderSettingsAddCarrierForTrackingOnly, _driver); } }
        public UIItem TenderSettingsSequenceForLtlTenderingBasedOnRoutingGuideNotOnCost { get { return new UIItem("Manage Customers>> Tender SettingsSequence For Ltl Tendering Based On Routing Guide Not On Cost", this.tenderSettingsSequenceForLtlTenderingBasedOnRoutingGuideNotOnCost, _driver); } }
        public UIItem TenderSettingsHideCustomerName { get { return new UIItem("Manage Customers>> Tender Settings Hide Customer Name", this.tenderSettingsHideCustomerName, _driver); } }
        public UIItem TenderSettingsHideCarrierRate { get { return new UIItem("Manage Customers>> Tender Settings Hide Carrier Rate", this.tenderSettingsHideCarrierRate, _driver); } }
        public UIItem TenderSettingsUseCustomerRepAsCarrierRep { get { return new UIItem("Manage Customers>> Tender Settings Use Customer Rep As Carrier Rep", this.tenderSettingsUseCustomerRepAsCarrierRep, _driver); } }
        public UIItem TenderSettingsUseCarrierCostAsCustomerRate { get { return new UIItem("Manage Customers>> Tender Settings Use Carrier Cost As Customer Rate", this.tenderSettingsUseCarrierCostAsCustomerRate, _driver); } }
        public UIItem TenderSettingsSendRateConfirmationToCarrier { get { return new UIItem("Manage Customers>> Tender Settings Send Rate Confirmation To Carrier", this.tenderSettingsSendRateConfirmationToCarrier, _driver); } }
        public UIItem TenderSettingsDoNotUseAutomaticCarrierRatingForManagedLtlLoads { get { return new UIItem("Manage Customers>> Tender Settings Do Not Use Automatic Carrier Rating For Managed Ltl Loads", this.tenderSettingsDoNotUseAutomaticCarrierRatingForManagedLtlLoads, _driver); } }
        public UIItem TenderSettingsSkipLtlCarriersWhoAreUnableToMakeDeliveryDate { get { return new UIItem("Manage Customers>> Tender Settings Skip Ltl Carriers Who Are Unable To Make Delivery Date", this.tenderSettingsSkipLtlCarriersWhoAreUnableToMakeDeliveryDate, _driver); } }
        public UIItem TenderSettingsApplyFscByDefault { get { return new UIItem("Manage Customers>> Tender Settings Apply Fsc By Default", this.tenderSettingsApplyFscByDefault, _driver); } }
        public UIItem TenderSettingsIgnoreCarrierQualificationErrorsWhenTendering { get { return new UIItem("Manage Customers>> Tender Settings Ignore Carrier Qualification Errors When Tendering", this.tenderSettingsIgnoreCarrierQualificationErrorsWhenTendering, _driver); } }
        public UIItem SpotTenderTimeLimitInMin { get { return new UIItem("Manage Customers>> Spot Tender Time Limit In Min", this.spotTenderTimeLimitInMin, _driver); } }
        public UIItem RoutingGuideExhaustedNotifyLoadCustomerOpsReps { get { return new UIItem("Manage Customers>> Routing Guide Exhausted Notify Load Customer Ops Reps", this.routingGuideExhaustedNotifyLoadCustomerOpsReps, _driver); } }
        public UIItem RoutingGuideExhaustedNotifyMainCustomerContact { get { return new UIItem("Manage Customers>> Routing Guide Exhausted Notify Main Customer Contact", this.routingGuideExhaustedNotifyMainCustomerContact, _driver); } }
        public UIItem RoutingGuideExhaustedSendToTheOpenBoard { get { return new UIItem("Manage Customers>> Routing Guide Exhausted Send To The Open Board", this.routingGuideExhaustedSendToTheOpenBoard, _driver); } }
        public UIItem EnableOrderManager { get { return new UIItem("Manage Customers>> Enable Order Manager", this.enableOrderManager, _driver); } }
        public UIItem ModeIntermodal { get { return new UIItem("Manage Customers>> Mode Intermodal", this.modeIntermodal, _driver); } }
        public UIItem ModeTruckload { get { return new UIItem("Manage Customers>> Mode Truckload", this.modeTruckload, _driver); } }
        public UIItem ModeLessThanTruckload { get { return new UIItem("Manage Customers>> Mode Less Than Truckload", this.modeLessThanTruckload, _driver); } }
        public UIItem EquipmentAutoTrailer { get { return new UIItem("Manage Customers>> Equipment Auto Trailer", this.equipmentAutoTrailer, _driver); } }
        public UIItem EquipmentBeamTrailer { get { return new UIItem("Manage Customers>> Equipment Beam Trailer", this.equipmentBeamTrailer, _driver); } }
        public UIItem EquipmentBoxcar { get { return new UIItem("Manage Customers>> Equipment Boxcar", this.equipmentBoxcar, _driver); } }
        public UIItem EquipmentBulk { get { return new UIItem("Manage Customers>> Equipment Bulk", this.equipmentBulk, _driver); } }
        public UIItem EquipmentCNRU { get { return new UIItem("Manage Customers>> Equipment CNRU", this.equipmentCNRU, _driver); } }
        public UIItem EquipmentConestoga { get { return new UIItem("Manage Customers>> Equipment Conestoga", this.equipmentConestoga, _driver); } }
        public UIItem EquipmentContainer { get { return new UIItem("Manage Customers>> Equipment Container", this.equipmentContainer, _driver); } }
        public UIItem EquipmentCPPU { get { return new UIItem("Manage Customers>> Equipment CPPU", this.equipmentCPPU, _driver); } }
        public UIItem EquipmentCSXU { get { return new UIItem("Manage Customers>> Equipment CSXU", this.equipmentCSXU, _driver); } }
        public UIItem EquipmentDeckedReefer { get { return new UIItem("Manage Customers>> Equipment Decked Reefer", this.equipmentDeckedReefer, _driver); } }
        public UIItem EquipmentDoubleDropLowboy { get { return new UIItem("Manage Customers>> Equipment Double Drop Lowboy", this.equipmentDoubleDropLowboy, _driver); } }
        public UIItem EquipmentDropFrame { get { return new UIItem("Manage Customers>> Equipment Drop Frame", this.equipmentDropFrame, _driver); } }
        public UIItem EquipmentDuraplateVan { get { return new UIItem("Manage Customers>> Equipment Duraplate Van", this.equipmentDuraplateVan, _driver); } }
        public UIItem EquipmentEMHU { get { return new UIItem("Manage Customers>> Equipment EMHU", this.equipmentEMHU, _driver); } }
        public UIItem EquipmentEMPU { get { return new UIItem("Manage Customers>> Equipment EMPU", this.equipmentEMPU, _driver); } }
        public UIItem EquipmentEMWU { get { return new UIItem("Manage Customers>> Equipment EMWU", this.equipmentEMWU, _driver); } }
        public UIItem EquipmentEPTY { get { return new UIItem("Manage Customers>> Equipment EPTY", this.equipmentEPTY, _driver); } }
        public UIItem EquipmentEuroliner { get { return new UIItem("Manage Customers>> Equipment Euroliner", this.equipmentEuroliner, _driver); } }
        public UIItem EquipmentFlatCar { get { return new UIItem("Manage Customers>> Equipment Flat Car", this.equipmentFlatCar, _driver); } }
        public UIItem EquipmentFlatbed { get { return new UIItem("Manage Customers>> Equipment Flatbed", this.equipmentFlatbed, _driver); } }
        public UIItem EquipmentFlatbedStretch { get { return new UIItem("Manage Customers>> Equipment Flatbed Stretch", this.equipmentFlatbedStretch, _driver); } }
        public UIItem EquipmentFlatbedWithSides { get { return new UIItem("Manage Customers>> Equipment Flatbed With Sides", this.equipmentFlatbedWithSides, _driver); } }
        public UIItem EquipmentFlatbedWithTarp { get { return new UIItem("Manage Customers>> Equipment Flatbed With Tarp", this.equipmentFlatbedWithTarp, _driver); } }
        public UIItem EquipmentHeatedVan { get { return new UIItem("Manage Customers>> Equipment Heated Van", this.equipmentHeatedVan, _driver); } }
        public UIItem EquipmentHotShot { get { return new UIItem("Manage Customers>> Equipment Hot Shot", this.equipmentHotShot, _driver); } }
        public UIItem EquipmentLandall { get { return new UIItem("Manage Customers>> Equipment Landall", this.equipmentLandall, _driver); } }
        public UIItem EquipmentMaxiFlatbed { get { return new UIItem("Manage Customers>> Equipment Maxi Flatbed", this.equipmentMaxiFlatbed, _driver); } }
        public UIItem EquipmentMaxiVan { get { return new UIItem("Manage Customers>> Equipment Maxi Van", this.equipmentMaxiVan, _driver); } }
        public UIItem EquipmentMegatrailer { get { return new UIItem("Manage Customers>> Equipment Megatrailer", this.equipmentMegatrailer, _driver); } }
        public UIItem EquipmentPACU { get { return new UIItem("Manage Customers>> Equipment PACU", this.equipmentPACU, _driver); } }
        public UIItem EquipmentPowerOnly { get { return new UIItem("Manage Customers>> Equipment Power Only", this.equipmentPowerOnly, _driver); } }
        public UIItem EquipmentReefer { get { return new UIItem("Manage Customers>> Equipment Reefer", this.equipmentReefer, _driver); } }
        public UIItem EquipmentReeferContainer { get { return new UIItem("Manage Customers>> Equipment Reefer Container", this.equipmentReeferContainer, _driver); } }
        public UIItem EquipmentRemovableGooseNeck { get { return new UIItem("Manage Customers>> Equipment Removable Goose Neck", this.equipmentRemovableGooseNeck, _driver); } }
        public UIItem EquipmentRGNMultiAxel { get { return new UIItem("Manage Customers>> Equipment RGN Multi Axel", this.equipmentRGNMultiAxel, _driver); } }
        public UIItem EquipmentRGNStretch { get { return new UIItem("Manage Customers>> Equipment RGN Stretch", this.equipmentRGNStretch, _driver); } }
        public UIItem EquipmentRoadTrain { get { return new UIItem("Manage Customers>> Equipment Road Train", this.equipmentRoadTrain, _driver); } }
        public UIItem EquipmentSiloTanker { get { return new UIItem("Manage Customers>> Equipment Silo Tanker", this.equipmentSiloTanker, _driver); } }
        public UIItem EquipmentSoftShell { get { return new UIItem("Manage Customers>> Equipment Soft Shell", this.equipmentSoftShell, _driver); } }
        public UIItem EquipmentStepDeck { get { return new UIItem("Manage Customers>> Equipment Step Deck", this.equipmentStepDeck, _driver); } }
        public UIItem EquipmentStepDeckStretch { get { return new UIItem("Manage Customers>> Equipment Step Deck Stretch", this.equipmentStepDeckStretch, _driver); } }
        public UIItem EquipmentStepdeckWithRamps { get { return new UIItem("Manage Customers>> Equipment Stepdeck With Ramps", this.equipmentStepdeckWithRamps, _driver); } }
        public UIItem EquipmentTanker { get { return new UIItem("Manage Customers>> Equipment Tanker", this.equipmentTanker, _driver); } }
        public UIItem EquipmentTilt { get { return new UIItem("Manage Customers>> Equipment Tilt", this.equipmentTilt, _driver); } }
        public UIItem EquipmentVan { get { return new UIItem("Manage Customers>> Equipment Van", this.equipmentVan, _driver); } }
        public UIItem EquipmentVentedVan { get { return new UIItem("Manage Customers>> Equipment Vented Van", this.equipmentVentedVan, _driver); } }
        public UIItem EquipmentWalkingFloor { get { return new UIItem("Manage Customers>> Equipment Walking Floor", this.equipmentWalkingFloor, _driver); } }
        public UIItem PackagingTypesBag { get { return new UIItem("Manage Customers>> Packaging Types Bag", this.packagingTypesBag, _driver); } }
        public UIItem PackagingTypesBale { get { return new UIItem("Manage Customers>> Packaging Types Bale", this.packagingTypesBale, _driver); } }
        public UIItem PackagingTypesBarge { get { return new UIItem("Manage Customers>> Packaging Types Barge", this.packagingTypesBarge, _driver); } }
        public UIItem PackagingTypesBarrel { get { return new UIItem("Manage Customers>> Packaging Types Barrel", this.packagingTypesBarrel, _driver); } }
        public UIItem PackagingTypesBasketOrHamper { get { return new UIItem("Manage Customers>> Packaging Types Basket Or Hamper", this.packagingTypesBasketOrHamper, _driver); } }
        public UIItem PackagingTypesBeam { get { return new UIItem("Manage Customers>> Packaging Types Beam", this.packagingTypesBeam, _driver); } }
        public UIItem PackagingTypesBin { get { return new UIItem("Manage Customers>> Packaging Types Bin", this.packagingTypesBin, _driver); } }
        public UIItem PackagingTypesBobbin { get { return new UIItem("Manage Customers>> Packaging Types Bobbin", this.packagingTypesBobbin, _driver); } }
        public UIItem PackagingTypesBottle { get { return new UIItem("Manage Customers>> Packaging Types Bottle", this.packagingTypesBottle, _driver); } }
        public UIItem PackagingTypesBox { get { return new UIItem("Manage Customers>> Packaging Types Box", this.packagingTypesBox, _driver); } }
        public UIItem PackagingTypesBoxWithInnerContainer { get { return new UIItem("Manage Customers>> Packaging Types Box With Inner Container", this.packagingTypesBoxWithInnerContainer, _driver); } }
        public UIItem PackagingTypesBucket { get { return new UIItem("Manage Customers>> Packaging Types Bucket", this.packagingTypesBucket, _driver); } }
        public UIItem PackagingTypesBulk { get { return new UIItem("Manage Customers>> Packaging Types Bulk", this.packagingTypesBulk, _driver); } }
        public UIItem PackagingTypesBulkBag { get { return new UIItem("Manage Customers>> Packaging Types Bulk Bag", this.packagingTypesBulkBag, _driver); } }
        public UIItem PackagingTypesBundle { get { return new UIItem("Manage Customers>> Packaging Types Bundle", this.packagingTypesBundle, _driver); } }
        public UIItem PackagingTypesCabinet { get { return new UIItem("Manage Customers>> Packaging Types Cabinet", this.packagingTypesCabinet, _driver); } }
        public UIItem PackagingTypesCage { get { return new UIItem("Manage Customers>> Packaging Types Cage", this.packagingTypesCage, _driver); } }
        public UIItem PackagingTypesCan { get { return new UIItem("Manage Customers>> Packaging Types Can", this.packagingTypesCan, _driver); } }
        public UIItem PackagingTypesCanCase { get { return new UIItem("Manage Customers>> Packaging Types Can Case", this.packagingTypesCanCase, _driver); } }
        public UIItem PackagingTypesCarLoadRail { get { return new UIItem("Manage Customers>> Packaging Types Car Load Rail", this.packagingTypesCarLoadRail, _driver); } }
        public UIItem PackagingTypesCarboy { get { return new UIItem("Manage Customers>> Packaging Types Carboy", this.packagingTypesCarboy, _driver); } }
        public UIItem PackagingTypesCarrier { get { return new UIItem("Manage Customers>> Packaging Types Carrier", this.packagingTypesCarrier, _driver); } }
        public UIItem PackagingTypesCarton { get { return new UIItem("Manage Customers>> Packaging Types Carton", this.packagingTypesCarton, _driver); } }
        public UIItem PackagingTypesCase { get { return new UIItem("Manage Customers>> Packaging Types Case", this.packagingTypesCase, _driver); } }
        public UIItem PackagingTypesCask { get { return new UIItem("Manage Customers>> Packaging Types Cask", this.packagingTypesCask, _driver); } }
        public UIItem PackagingTypesCheeses { get { return new UIItem("Manage Customers>> Packaging Types Cheeses", this.packagingTypesCheeses, _driver); } }
        public UIItem PackagingTypesChest { get { return new UIItem("Manage Customers>> Packaging Types Chest", this.packagingTypesChest, _driver); } }
        public UIItem PackagingTypesCoil { get { return new UIItem("Manage Customers>> Packaging Types Coil", this.packagingTypesCoil, _driver); } }
        public UIItem PackagingTypesCones { get { return new UIItem("Manage Customers>> Packaging Types Cones", this.packagingTypesCones, _driver); } }
        public UIItem PackagingTypesContainer { get { return new UIItem("Manage Customers>> Packaging Types Container", this.packagingTypesContainer, _driver); } }
        public UIItem PackagingTypesContainersOfBulkCargo { get { return new UIItem("Manage Customers>> Packaging Types Containers Of Bulk Cargo", this.packagingTypesContainersOfBulkCargo, _driver); } }
        public UIItem PackagingTypesCore { get { return new UIItem("Manage Customers>> Packaging Types Core", this.packagingTypesCore, _driver); } }
        public UIItem PackagingTypesCradle { get { return new UIItem("Manage Customers>> Packaging Types Cradle", this.packagingTypesCradle, _driver); } }
        public UIItem PackagingTypesCrate { get { return new UIItem("Manage Customers>> Packaging Types Crate", this.packagingTypesCrate, _driver); } }
        public UIItem PackagingTypesCube { get { return new UIItem("Manage Customers>> Packaging Types Cube", this.packagingTypesCube, _driver); } }
        public UIItem PackagingTypesCylinder { get { return new UIItem("Manage Customers>> Packaging Types Cylinder", this.packagingTypesCylinder, _driver); } }
        public UIItem PackagingTypesDoubleLengthRack { get { return new UIItem("Manage Customers>> Packaging Types Double Length Rack", this.packagingTypesDoubleLengthRack, _driver); } }
        public UIItem PackagingTypesDoubleLengthSkid { get { return new UIItem("Manage Customers>> Packaging Types Double Length Skid", this.packagingTypesDoubleLengthSkid, _driver); } }
        public UIItem PackagingTypesDoubleLengthToteBin { get { return new UIItem("Manage Customers>> Packaging Types Double Length Tote Bin", this.packagingTypesDoubleLengthToteBin, _driver); } }
        public UIItem PackagingTypesDrum { get { return new UIItem("Manage Customers>> Packaging Types Drum", this.packagingTypesDrum, _driver); } }
        public UIItem PackagingTypesDryBulk { get { return new UIItem("Manage Customers>> Packaging Types Dry Bulk", this.packagingTypesDryBulk, _driver); } }
        public UIItem PackagingTypesDuffleBag { get { return new UIItem("Manage Customers>> Packaging Types Duffle Bag", this.packagingTypesDuffleBag, _driver); } }
        public UIItem PackagingTypesEngineContainer { get { return new UIItem("Manage Customers>> Packaging Types Engine Container", this.packagingTypesEngineContainer, _driver); } }
        public UIItem PackagingTypesEnvelope { get { return new UIItem("Manage Customers>> Packaging Types Envelope", this.packagingTypesEnvelope, _driver); } }
        public UIItem PackagingTypesFirkin { get { return new UIItem("Manage Customers>> Packaging Types Firkin", this.packagingTypesFirkin, _driver); } }
        public UIItem PackagingTypesFlask { get { return new UIItem("Manage Customers>> Packaging Types Flask", this.packagingTypesFlask, _driver); } }
        public UIItem PackagingTypesFloBin { get { return new UIItem("Manage Customers>> Packaging Types Flo Bin", this.packagingTypesFloBin, _driver); } }
        public UIItem PackagingTypesForwardReel { get { return new UIItem("Manage Customers>> Packaging Types Forward Reel", this.packagingTypesForwardReel, _driver); } }
        public UIItem PackagingTypesFrame { get { return new UIItem("Manage Customers>> Packaging Types Frame", this.packagingTypesFrame, _driver); } }
        public UIItem PackagingTypesGarmentsOnHangers { get { return new UIItem("Manage Customers>> Packaging Types Garments On Hangers", this.packagingTypesGarmentsOnHangers, _driver); } }
        public UIItem PackagingTypesHalfStandardRack { get { return new UIItem("Manage Customers>> Packaging Types Half Standard Rack", this.packagingTypesHalfStandardRack, _driver); } }
        public UIItem PackagingTypesHalfStandardToteBin { get { return new UIItem("Manage Customers>> Packaging Types Half Standard Tote Bin", this.packagingTypesHalfStandardToteBin, _driver); } }
        public UIItem PackagingTypesHamper { get { return new UIItem("Manage Customers>> Packaging Types Hamper", this.packagingTypesHamper, _driver); } }
        public UIItem PackagingTypesHeadsOfBeef { get { return new UIItem("Manage Customers>> Packaging Types Heads Of Beef", this.packagingTypesHeadsOfBeef, _driver); } }
        public UIItem PackagingTypesHogshead { get { return new UIItem("Manage Customers>> Packaging Types Hogshead", this.packagingTypesHogshead, _driver); } }
        public UIItem PackagingTypesHopperTruck { get { return new UIItem("Manage Customers>> Packaging Types Hopper Truck", this.packagingTypesHopperTruck, _driver); } }
        public UIItem PackagingTypesIntermediateBulkContainers { get { return new UIItem("Manage Customers>> Packaging Types Intermediate Bulk Containers", this.packagingTypesIntermediateBulkContainers, _driver); } }
        public UIItem PackagingTypesJar { get { return new UIItem("Manage Customers>> Packaging Types Jar", this.packagingTypesJar, _driver); } }
        public UIItem PackagingTypesJug { get { return new UIItem("Manage Customers>> Packaging Types Jug", this.packagingTypesJug, _driver); } }
        public UIItem PackagingTypesKeg { get { return new UIItem("Manage Customers>> Packaging Types Keg", this.packagingTypesKeg, _driver); } }
        public UIItem PackagingTypesKit { get { return new UIItem("Manage Customers>> Packaging Types Kit", this.packagingTypesKit, _driver); } }
        public UIItem PackagingTypesKnockdownRack { get { return new UIItem("Manage Customers>> Packaging Types Knockdown Rack", this.packagingTypesKnockdownRack, _driver); } }
        public UIItem PackagingTypesKnockdownToteBin { get { return new UIItem("Manage Customers>> Packaging Types Knockdown Tote Bin", this.packagingTypesKnockdownToteBin, _driver); } }
        public UIItem PackagingTypesLiftVan { get { return new UIItem("Manage Customers>> Packaging Types Lift Van", this.packagingTypesLiftVan, _driver); } }
        public UIItem PackagingTypesLifts { get { return new UIItem("Manage Customers>> Packaging Types Lifts", this.packagingTypesLifts, _driver); } }
        public UIItem PackagingTypesLinerBagDry { get { return new UIItem("Manage Customers>> Packaging Types Liner Bag Dry", this.packagingTypesLinerBagDry, _driver); } }
        public UIItem PackagingTypesLinerBagLiquid { get { return new UIItem("Manage Customers>> Packaging Types Liner Bag Liquid", this.packagingTypesLinerBagLiquid, _driver); } }
        public UIItem PackagingTypesLiquidBulk { get { return new UIItem("Manage Customers>> Packaging Types Liquid Bulk", this.packagingTypesLiquidBulk, _driver); } }
        public UIItem PackagingTypesLog { get { return new UIItem("Manage Customers>> Packaging Types Log", this.packagingTypesLog, _driver); } }
        public UIItem PackagingTypesLoose { get { return new UIItem("Manage Customers>> Packaging Types Loose", this.packagingTypesLoose, _driver); } }
        public UIItem PackagingTypesLug { get { return new UIItem("Manage Customers>> Packaging Types Lug", this.packagingTypesLug, _driver); } }
        public UIItem PackagingTypesMilitaryVan { get { return new UIItem("Manage Customers>> Packaging Types Military Van", this.packagingTypesMilitaryVan, _driver); } }
        public UIItem PackagingTypesMixedTypePack { get { return new UIItem("Manage Customers>> Packaging Types Mixed Type Pack", this.packagingTypesMixedTypePack, _driver); } }
        public UIItem PackagingTypesMultiRollPack { get { return new UIItem("Manage Customers>> Packaging Types Multi Roll Pack", this.packagingTypesMultiRollPack, _driver); } }
        public UIItem PackagingTypesMultiwallContainerSecuredToWarehousePallet { get { return new UIItem("Manage Customers>> Packaging Types Multiwall Container Secured To Warehouse Pallet", this.packagingTypesMultiwallContainerSecuredToWarehousePallet, _driver); } }
        public UIItem PackagingTypesNoil { get { return new UIItem("Manage Customers>> Packaging Types Noil", this.packagingTypesNoil, _driver); } }
        public UIItem PackagingTypesOnHangerOrRackInBoxes { get { return new UIItem("Manage Customers>> Packaging Types On Hanger Or Rack In Boxes", this.packagingTypesOnHangerOrRackInBoxes, _driver); } }
        public UIItem PackagingTypesOverwrap { get { return new UIItem("Manage Customers>> Packaging Types Overwrap", this.packagingTypesOverwrap, _driver); } }
        public UIItem PackagingTypesPackage { get { return new UIItem("Manage Customers>> Packaging Types Package", this.packagingTypesPackage, _driver); } }
        public UIItem PackagingTypesPail { get { return new UIItem("Manage Customers>> Packaging Types Pail", this.packagingTypesPail, _driver); } }
        public UIItem PackagingTypesPallet { get { return new UIItem("Manage Customers>> Packaging Types Pallet", this.packagingTypesPallet, _driver); } }
        public UIItem PackagingTypesPieces { get { return new UIItem("Manage Customers>> Packaging Types Pieces", this.packagingTypesPieces, _driver); } }
        public UIItem PackagingTypesPims { get { return new UIItem("Manage Customers>> Packaging Types Pims", this.packagingTypesPims, _driver); } }
        public UIItem PackagingTypesPipeRack { get { return new UIItem("Manage Customers>> Packaging Types Pipe Rack", this.packagingTypesPipeRack, _driver); } }
        public UIItem PackagingTypesPlatform { get { return new UIItem("Manage Customers>> Packaging Types Platform", this.packagingTypesPlatform, _driver); } }
        public UIItem PackagingTypesPrivateVehicle { get { return new UIItem("Manage Customers>> Packaging Types Private Vehicle", this.packagingTypesPrivateVehicle, _driver); } }
        public UIItem PackagingTypesRack { get { return new UIItem("Manage Customers>> Packaging Types Rack", this.packagingTypesRack, _driver); } }
        public UIItem PackagingTypesReel { get { return new UIItem("Manage Customers>> Packaging Types Reel", this.packagingTypesReel, _driver); } }
        public UIItem PackagingTypesRoll { get { return new UIItem("Manage Customers>> Packaging Types Roll", this.packagingTypesRoll, _driver); } }
        public UIItem PackagingTypesSack { get { return new UIItem("Manage Customers>> Packaging Types Sack", this.packagingTypesSack, _driver); } }
        public UIItem PackagingTypesSkid { get { return new UIItem("Manage Customers>> Packaging Types Skid", this.packagingTypesSkid, _driver); } }
        public UIItem PackagingTypesSleeve { get { return new UIItem("Manage Customers>> Packaging Types Sleeve", this.packagingTypesSleeve, _driver); } }
        public UIItem PackagingTypesSlipSheet { get { return new UIItem("Manage Customers>> Packaging Types Slip Sheet", this.packagingTypesSlipSheet, _driver); } }
        public UIItem PackagingTypesSpool { get { return new UIItem("Manage Customers>> Packaging Types Spool", this.packagingTypesSpool, _driver); } }
        public UIItem PackagingTypesTank { get { return new UIItem("Manage Customers>> Packaging Types Tank", this.packagingTypesTank, _driver); } }
        public UIItem PackagingTypesToteBin { get { return new UIItem("Manage Customers>> Packaging Types Tote Bin", this.packagingTypesToteBin, _driver); } }
        public UIItem PackagingTypesToteCan { get { return new UIItem("Manage Customers>> Packaging Types Tote Can", this.packagingTypesToteCan, _driver); } }
        public UIItem PackagingTypesTray { get { return new UIItem("Manage Customers>> Packaging Types Tray", this.packagingTypesTray, _driver); } }
        public UIItem PackagingTypesTriwallBox { get { return new UIItem("Manage Customers>> Packaging Types Triwall Box", this.packagingTypesTriwallBox, _driver); } }
        public UIItem PackagingTypesTruck { get { return new UIItem("Manage Customers>> Packaging Types Truck", this.packagingTypesTruck, _driver); } }
        public UIItem PackagingTypesTub { get { return new UIItem("Manage Customers>> Packaging Types Tub", this.packagingTypesTub, _driver); } }
        public UIItem PackagingTypesTube { get { return new UIItem("Manage Customers>> Packaging Types Tube", this.packagingTypesTube, _driver); } }
        public UIItem PackagingTypesUnit { get { return new UIItem("Manage Customers>> Packaging Types Unit", this.packagingTypesUnit, _driver); } }
        public UIItem PackagingTypesVanPack { get { return new UIItem("Manage Customers>> Packaging Types Van Pack", this.packagingTypesVanPack, _driver); } }
        public UIItem PackagingTypesVehicles { get { return new UIItem("Manage Customers>> Packaging Types Vehicles", this.packagingTypesVehicles, _driver); } }
        public UIItem EquipmentLengths20 { get { return new UIItem("Manage Customers>> Equipment Lengths 20", this.equipmentLengths20, _driver); } }
        public UIItem EquipmentLengths40 { get { return new UIItem("Manage Customers>> Equipment Lengths 40", this.equipmentLengths40, _driver); } }
        public UIItem EquipmentLengths45 { get { return new UIItem("Manage Customers>> Equipment Lengths 45", this.equipmentLengths45, _driver); } }
        public UIItem EquipmentLengths48 { get { return new UIItem("Manage Customers>> Equipment Lengths 48", this.equipmentLengths48, _driver); } }
        public UIItem EquipmentLengths53 { get { return new UIItem("Manage Customers>> Equipment Lengths 53", this.equipmentLengths53, _driver); } }
        public UIItem ShippingUnitStackable { get { return new UIItem("Manage Customers>> Shipping Unit Stackable", this.shippingUnitStackable, _driver); } }
        public UIItem ShippingUnitOverDimension { get { return new UIItem("Manage Customers>> Shipping Unit Over Dimension", this.shippingUnitOverDimension, _driver); } }
        public UIItem CommoditiesThreeFieldSKU { get { return new UIItem("Manage Customers>> Commodities Three Field SKU", this.commoditiesThreeFieldSKU, _driver); } }
        public UIItem CommoditiesShowDueDate { get { return new UIItem("Manage Customers>> Commodities Show Due Date", this.commoditiesShowDueDate, _driver); } }
        public UIItem ReferenceTypeBillOfLading { get { return new UIItem("Manage Customers>> Reference Type Bill Of Lading", this.referenceTypeBillOfLading, _driver); } }
        public UIItem ReferenceTypeDelivery { get { return new UIItem("Manage Customers>> Reference Type Delivery", this.referenceTypeDelivery, _driver); } }
        public UIItem ReferenceTypePickUp { get { return new UIItem("Manage Customers>> Reference Type Pick Up", this.referenceTypePickUp, _driver); } }
        public UIItem ReferenceTypePurchaseOrder { get { return new UIItem("Manage Customers>> Reference Type Purchase Order", this.referenceTypePurchaseOrder, _driver); } }
        public UIItem ReferenceTypeSalesOrder { get { return new UIItem("Manage Customers>> Reference Type Sales Order", this.referenceTypeSalesOrder, _driver); } }
        public UIItem LtlReferenceTypeBillOfLading { get { return new UIItem("Manage Customers>> Ltl Reference Type Bill Of Lading", this.ltlReferenceTypeBillOfLading, _driver); } }
        public UIItem LtlReferenceTypeCommercialInvoice { get { return new UIItem("Manage Customers>> Ltl Reference Type Commercial Invoice", this.ltlReferenceTypeCommercialInvoice, _driver); } }
        public UIItem LtlReferenceTypeDistributor { get { return new UIItem("Manage Customers>> Ltl Reference Type Distributor", this.ltlReferenceTypeDistributor, _driver); } }
        public UIItem LtlReferenceTypeFranchiseId { get { return new UIItem("Manage Customers>> Ltl Reference Type Franchise Id", this.ltlReferenceTypeFranchiseId, _driver); } }
        public UIItem LtlReferenceTypeKomModelNumber { get { return new UIItem("Manage Customers>> Ltl Reference Type Kom Model Number", this.ltlReferenceTypeKomModelNumber, _driver); } }
        public UIItem LtlReferenceTypeKomSalesOrder { get { return new UIItem("Manage Customers>> Ltl Reference Type Kom Sales Order", this.ltlReferenceTypeKomSalesOrder, _driver); } }
        public UIItem LtlReferenceTypeKomSerialNumber { get { return new UIItem("Manage Customers>> Ltl Reference Type Kom Serial Number", this.ltlReferenceTypeKomSerialNumber, _driver); } }
        public UIItem LtlReferenceTypeMasterBOLNumber { get { return new UIItem("Manage Customers>> Ltl Reference Type Master BOL Number", this.ltlReferenceTypeMasterBOLNumber, _driver); } }
        public UIItem LtlReferenceTypeOrderNumber { get { return new UIItem("Manage Customers>> Ltl Reference Type Order Number", this.ltlReferenceTypeOrderNumber, _driver); } }
        public UIItem LtlReferenceTypePONumber { get { return new UIItem("Manage Customers>> Ltl Reference Type PO Number", this.ltlReferenceTypePONumber, _driver); } }
        public UIItem LtlReferenceTypePRONumber { get { return new UIItem("Manage Customers>> Ltl Reference Type PRO Number", this.ltlReferenceTypePRONumber, _driver); } }
        public UIItem LtlReferenceTypeQuoteNumber { get { return new UIItem("Manage Customers>> Ltl Reference Type Quote Number", this.ltlReferenceTypeQuoteNumber, _driver); } }
        public UIItem LtlReferenceTypeShipmentNumber { get { return new UIItem("Manage Customers>> Ltl Reference Type Shipment Number", this.ltlReferenceTypeShipmentNumber, _driver); } }
        public UIItem LtlReferenceTypeVendorCode { get { return new UIItem("Manage Customers>> Ltl Reference Type Vendor Code", this.ltlReferenceTypeVendorCode, _driver); } }
        public UIItem MyLoadsDocumentVisibility { get { return new UIItem("Manage Customers>> My Loads Document Visibility", this.myLoadsDocumentVisibility, _driver); } }
        public UIItem MyLoadsHideManagedChargesFromCarrier { get { return new UIItem("Manage Customers>> My Loads Hide Managed Charges From Carrier", this.myLoadsHideManagedChargesFromCarrier, _driver); } }
        public UIItem MyLoadsHideAllChargesFromCustomer { get { return new UIItem("Manage Customers>> My Loads Hide All Charges From Customer", this.myLoadsHideAllChargesFromCustomer, _driver); } }
        public UIItem MyLoadsHideAllCarrierTrackingNotesFromCustomerAndFacilities { get { return new UIItem("Manage Customers>> My Loads Hide All Carrier Tracking Notes From Customer And Facilities", this.myLoadsHideAllCarrierTrackingNotesFromCustomerAndFacilities, _driver); } }
        public UIItem MyLoadsAllowCarriersToEditBOLAndPODNumbers { get { return new UIItem("Manage Customers>> My Loads Allow Carriers To Edit BOL And POD Numbers", this.myLoadsAllowCarriersToEditBOLAndPODNumbers, _driver); } }
        public UIItem MyLoadsAllowUsersToUpdateLoadStopTimesInAnyOrder { get { return new UIItem("Manage Customers>> My Loads Allow Users To Update Load Stop Times In Any Order", this.myLoadsAllowUsersToUpdateLoadStopTimesInAnyOrder, _driver); } }
        public UIItem PremiumReporting { get { return new UIItem("Manage Customers>> Premium Reporting", this.premiumReporting, _driver); } }
        public UIItem AdditionalSettingsShowFacilityHours { get { return new UIItem("Manage Customers>> Additional Settings Show Facility Hours", this.additionalSettingsShowFacilityHours, _driver); } }
        public UIItem AdditionalSettingsShowTenderHistory { get { return new UIItem("Manage Customers>> Additional Settings Show Tender History", this.additionalSettingsShowTenderHistory, _driver); } }
        public UIItem AdditionalSettingsShowCommodityValue { get { return new UIItem("Manage Customers>> Additional Settings Show Commodity Value", this.additionalSettingsShowCommodityValue, _driver); } }
        public UIItem AdditionalSettingsShowLoadStopNotes { get { return new UIItem("Manage Customers>> Additional Settings Show Load Stop Notes", this.additionalSettingsShowLoadStopNotes, _driver); } }
        public UIItem AdditionalSettingsShowAppointmentSchedule { get { return new UIItem("Manage Customers>> Additional Settings Show Appointment Schedule", this.additionalSettingsShowAppointmentSchedule, _driver); } }
        public UIItem AdditionalSettingsShowAllCustomerChildren { get { return new UIItem("Manage Customers>> Additional Settings Show All Customer Children", this.additionalSettingsShowAllCustomerChildren, _driver); } }
        public UIItem ExcludeWeekendsForShipmentOrder { get { return new UIItem("Manage Customers>> Exclude Weekends For Shipment Order", this.excludeWeekendsForShipmentOrder, _driver); } }
        public UIItem PaddedDaysForPastDueOrders { get { return new UIItem("Manage Customers>> Padded Days For Past Due Orders", this.paddedDaysForPastDueOrders, _driver); } }
        public UIItem CreateLoadUsingOptimizationTool { get { return new UIItem("Manage Customers>> Create Load Using Optimization Tool", this.createLoadUsingOptimizationTool, _driver); } }
        public UIItem CutOffTimeForPastDueOrders { get { return new UIItem("Manage Customers>> CutOff Time For Past Due Orders", this.cutOffTimeForPastDueOrders, _driver); } }
        public UIItem CutOffTimeToSubmitOrderForShipping { get { return new UIItem("Manage Customers>> cutOff Time to Submit Order For Shipping", this.cutOffTimeToSubmitOrderForShipping, _driver); } }
        public UIItem StandardLeadTime { get { return new UIItem("Manage Customers>> standard Lead Time", this.standardLeadTime, _driver); } }
        public UIItem LeadTimeAfterCutOff { get { return new UIItem("Manage Customers>> Lead Time After Cut Off", this.leadTimeAfterCutOff, _driver); } }
        public UIItem StandardLeadTimeLabel { get { return new UIItem("Manage Customers>> Standard Lead Time Label", this.standardLeadTimeLabel, _driver); } }
    }
}
