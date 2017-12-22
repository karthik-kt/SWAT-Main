using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAT.Applications.Claw.DAL
{
    using SWAT.Data;

    public class ManageCustomersData
    {
        private DataManager _datamanager;

        // Currently check-box only settings have been covered. Need to add support for text only and check-box + text settings.
        // Will be covered as and when required.
        public ManageCustomersData(DataManager datamanager)
        {
            _datamanager = datamanager;

            //FACILITY RELATIONSHIPS
            FacilityRelationships = datamanager.Data("FacilityRelationships");

            //SCHEDULE LOADS
            EnableSchedulingInbound = datamanager.Data("EnableScheduling_Inbound");
            EnableSchedulingOutbound = datamanager.Data("EnableScheduling_Outbound");

            //FACILITY CALENDAR
            EnableFacilityCalendar = datamanager.Data("EnableFacilityCalendar");

            //TMS
            //Tender Settings
            TenderSettingsAddCarrierForTrackingOnly = datamanager.Data("TenderSettings_AddCarrierForTrackingOnly");
            TenderSettingsSequenceForLtlTenderingBasedOnRoutingGuideNotOnCost = datamanager.Data("TenderSettings_SequenceForLtlTenderingBasedOnRoutingGuideNotOnCost");
            TenderSettingsHideCustomerName = datamanager.Data("TenderSettings_HideCustomerName");
            TenderSettingsHideCarrierRate = datamanager.Data("Scheduler_UserCanAccess");
            TenderSettingsUseCustomerRepAsCarrierRep = datamanager.Data("TenderSettings_HideCarrierRate");
            TenderSettingsUseCarrierCostAsCustomerRate = datamanager.Data("TenderSettings_UseCarrierCostAsCustomerRate");
            TenderSettingsSendRateConfirmationToCarrier = datamanager.Data("TenderSettings_SendRateConfirmationToCarrier");
            TenderSettingsDoNotUseAutomaticCarrierRatingForManagedLtlLoads = datamanager.Data("TenderSettings_DoNotUseAutomaticCarrierRatingForManagedLtlLoads");
            TenderSettingsSkipLtlCarriersWhoAreUnableToMakeDeliveryDate = datamanager.Data("TenderSettings_SkipLtlCarriersWhoAreUnableToMakeDeliveryDate");
            TenderSettingsApplyFscByDefault = datamanager.Data("TenderSettings_ApplyFscByDefault");
            TenderSettingsIgnoreCarrierQualificationErrorsWhenTendering = datamanager.Data("TenderSettings_IgnoreCarrierQualificationErrorsWhenTendering");
            SpotTenderTimeLimitInMin = datamanager.Data("SpotTenderTimeLimitInMin");

            //Routing Guide Exhausted
            RoutingGuideExhaustedNotifyLoadCustomerOpsReps = datamanager.Data("RoutingGuideExhausted_NotifyLoadCustomerOpsReps");
            RoutingGuideExhaustedNotifyMainCustomerContact = datamanager.Data("RoutingGuideExhausted_NotifyMainCustomerContact");
            RoutingGuideExhaustedSendToTheOpenBoard = datamanager.Data("RoutingGuideExhausted_SendToTheOpenBoard");

            //ORDER MANAGER
            EnableOrderManager = datamanager.Data("EnableOrderManager");

            //LOADS AND ORDERS
            //Mode
            ModeIntermodal = datamanager.Data("Mode_Intermodal");
            ModeTruckload = datamanager.Data("Mode_Truckload");
            ModeLessThanTruckload = datamanager.Data("Mode_LessThanTruckload");

            //Equipment
            EquipmentAutoTrailer = datamanager.Data("Equipment_AutoTrailer");
            EquipmentBeamTrailer = datamanager.Data("Equipment_BeamTrailer");
            EquipmentBoxcar = datamanager.Data("Equipment_Boxcar");
            EquipmentBulk = datamanager.Data("Equipment_Bulk");
            EquipmentCNRU = datamanager.Data("Equipment_CNRU");
            EquipmentConestoga = datamanager.Data("Equipment_Conestoga");
            EquipmentContainer = datamanager.Data("Equipment_Container");
            EquipmentCPPU = datamanager.Data("Equipment_CPPU");
            EquipmentCSXU = datamanager.Data("Equipment_CSXU");
            EquipmentDeckedReefer = datamanager.Data("Equipment_DeckedReefer");
            EquipmentDoubleDropLowboy = datamanager.Data("Equipment_DoubleDropLowboy");
            EquipmentDropFrame = datamanager.Data("Equipment_DropFrame");
            EquipmentDuraplateVan = datamanager.Data("Equipment_DuraplateVan");
            EquipmentEMHU = datamanager.Data("Equipment_EMHU");
            EquipmentEMPU = datamanager.Data("Equipment_EMPU");
            EquipmentEMWU = datamanager.Data("Equipment_EMWU");
            EquipmentEPTY = datamanager.Data("Equipment_EPTY");
            EquipmentEuroliner = datamanager.Data("Equipment_Euroliner");
            EquipmentFlatCar = datamanager.Data("Equipment_FlatCar");
            EquipmentFlatbed = datamanager.Data("Equipment_Flatbed");
            EquipmentFlatbedStretch = datamanager.Data("Equipment_FlatbedStretch");
            EquipmentFlatbedWithSides = datamanager.Data("Equipment_FlatbedWithSides");
            EquipmentFlatbedWithTarp = datamanager.Data("Equipment_FlatbedWithTarp");
            EquipmentHeatedVan = datamanager.Data("Equipment_HeatedVan");
            EquipmentHotShot = datamanager.Data("Equipment_HotShot");
            EquipmentLandall = datamanager.Data("Equipment_Landall");
            EquipmentMaxiFlatbed = datamanager.Data("Equipment_MaxiFlatbed");
            EquipmentMaxiVan = datamanager.Data("Equipment_MaxiVan");
            EquipmentMegatrailer = datamanager.Data("Equipment_Megatrailer");
            EquipmentPACU = datamanager.Data("Equipment_PACU");
            EquipmentPowerOnly = datamanager.Data("Equipment_PowerOnly");
            EquipmentReefer = datamanager.Data("Equipment_Reefer");
            EquipmentReeferContainer = datamanager.Data("Equipment_ReeferContainer");
            EquipmentRemovableGooseNeck = datamanager.Data("Equipment_RemovableGooseNeck");
            EquipmentRGNMultiAxel = datamanager.Data("Equipment_RGNMultiAxel");
            EquipmentRGNStretch = datamanager.Data("Equipment_RGNStretch");
            EquipmentRoadTrain = datamanager.Data("Equipment_RoadTrain");
            EquipmentSiloTanker = datamanager.Data("Equipment_SiloTanker");
            EquipmentSoftShell = datamanager.Data("Equipment_SoftShell");
            EquipmentStepDeck = datamanager.Data("Equipment_StepDeck");
            EquipmentStepDeckStretch = datamanager.Data("Equipment_StepDeckStretch");
            EquipmentStepdeckWithRamps = datamanager.Data("Equipment_StepdeckWithRamps");
            EquipmentTanker = datamanager.Data("Equipment_Tanker");
            EquipmentTilt = datamanager.Data("Equipment_Tilt");
            EquipmentVan = datamanager.Data("Equipment_Van");
            EquipmentVentedVan = datamanager.Data("Equipment_VentedVan");
            EquipmentWalkingFloor = datamanager.Data("Equipment_WalkingFloor");

            //Packaging Types
            PackagingTypesBag = datamanager.Data("PackagingTypes_Bag");
            PackagingTypesBale = datamanager.Data("PackagingTypes_Bale");
            PackagingTypesBarge = datamanager.Data("PackagingTypes_Barge");
            PackagingTypesBarrel = datamanager.Data("PackagingTypes_Barrel");
            PackagingTypesBasketOrHamper = datamanager.Data("PackagingTypes_BasketOrHamper");
            PackagingTypesBeam = datamanager.Data("PackagingTypes_Beam");
            PackagingTypesBin = datamanager.Data("PackagingTypes_Bin");
            PackagingTypesBobbin = datamanager.Data("PackagingTypes_Bobbin");
            PackagingTypesBottle = datamanager.Data("PackagingTypes_Bottle");
            PackagingTypesBox = datamanager.Data("PackagingTypes_Box");
            PackagingTypesBoxWithInnerContainer = datamanager.Data("PackagingTypes_BoxWithInnerContainer");
            PackagingTypesBucket = datamanager.Data("PackagingTypes_Bucket");
            PackagingTypesBulk = datamanager.Data("PackagingTypes_Bulk");
            PackagingTypesBulkBag = datamanager.Data("PackagingTypes_BulkBag");
            PackagingTypesBundle = datamanager.Data("PackagingTypes_Bundle");
            PackagingTypesCabinet = datamanager.Data("PackagingTypes_Cabinet");
            PackagingTypesCage = datamanager.Data("PackagingTypes_Cage");
            PackagingTypesCan = datamanager.Data("PackagingTypes_Can");
            PackagingTypesCanCase = datamanager.Data("PackagingTypes_CanCase");
            PackagingTypesCarLoadRail = datamanager.Data("PackagingTypes_CarLoadRail");
            PackagingTypesCarboy = datamanager.Data("PackagingTypes_Carboy");
            PackagingTypesCarrier = datamanager.Data("PackagingTypes_Carrier");
            PackagingTypesCarton = datamanager.Data("PackagingTypes_Carton");
            PackagingTypesCase = datamanager.Data("PackagingTypes_Case");
            PackagingTypesCask = datamanager.Data("PackagingTypes_Cask");
            PackagingTypesCheeses = datamanager.Data("PackagingTypes_Cheeses");
            PackagingTypesChest = datamanager.Data("PackagingTypes_Chest");
            PackagingTypesCoil = datamanager.Data("PackagingTypes_Coil");
            PackagingTypesCones = datamanager.Data("PackagingTypes_Cones");
            PackagingTypesContainer = datamanager.Data("PackagingTypes_Container");
            PackagingTypesContainersOfBulkCargo = datamanager.Data("PackagingTypes_ContainersOfBulkCargo");
            PackagingTypesCore = datamanager.Data("PackagingTypes_Core");
            PackagingTypesCradle = datamanager.Data("PackagingTypes_Cradle");
            PackagingTypesCrate = datamanager.Data("PackagingTypes_Crate");
            PackagingTypesCube = datamanager.Data("PackagingTypes_Cube");
            PackagingTypesCylinder = datamanager.Data("PackagingTypes_Cylinder");
            PackagingTypesDoubleLengthRack = datamanager.Data("PackagingTypes_DoubleLengthRack");
            PackagingTypesDoubleLengthSkid = datamanager.Data("PackagingTypes_DoubleLengthSkid");
            PackagingTypesDoubleLengthToteBin = datamanager.Data("PackagingTypes_DoubleLengthToteBin");
            PackagingTypesDrum = datamanager.Data("PackagingTypes_Drum");
            PackagingTypesDryBulk = datamanager.Data("PackagingTypes_DryBulk");
            PackagingTypesDuffleBag = datamanager.Data("PackagingTypes_DuffleBag");
            PackagingTypesEngineContainer = datamanager.Data("PackagingTypes_EngineContainer");
            PackagingTypesEnvelope = datamanager.Data("PackagingTypes_Envelope");
            PackagingTypesFirkin = datamanager.Data("PackagingTypes_Firkin");
            PackagingTypesFlask = datamanager.Data("PackagingTypes_Flask");
            PackagingTypesFloBin = datamanager.Data("PackagingTypes_FloBin");
            PackagingTypesForwardReel = datamanager.Data("PackagingTypes_ForwardReel");
            PackagingTypesFrame = datamanager.Data("PackagingTypes_Frame");
            PackagingTypesGarmentsOnHangers = datamanager.Data("PackagingTypes_GarmentsOnHangers");
            PackagingTypesHalfStandardRack = datamanager.Data("PackagingTypes_HalfStandardRack");
            PackagingTypesHalfStandardToteBin = datamanager.Data("PackagingTypes_HalfStandardToteBin");
            PackagingTypesHamper = datamanager.Data("PackagingTypes_Hamper");
            PackagingTypesHeadsOfBeef = datamanager.Data("PackagingTypes_HeadsOfBeef");
            PackagingTypesHogshead = datamanager.Data("PackagingTypes_Hogshead");
            PackagingTypesHopperTruck = datamanager.Data("PackagingTypes_HopperTruck");
            PackagingTypesIntermediateBulkContainers = datamanager.Data("PackagingTypes_IntermediateBulkContainers");
            PackagingTypesJar = datamanager.Data("PackagingTypes_Jar");
            PackagingTypesJug = datamanager.Data("PackagingTypes_Jug");
            PackagingTypesKeg = datamanager.Data("PackagingTypes_Keg");
            PackagingTypesKit = datamanager.Data("PackagingTypes_Kit");
            PackagingTypesKnockdownRack = datamanager.Data("PackagingTypes_KnockdownRack");
            PackagingTypesKnockdownToteBin = datamanager.Data("PackagingTypes_KnockdownToteBin");
            PackagingTypesLiftVan = datamanager.Data("PackagingTypes_LiftVan");
            PackagingTypesLifts = datamanager.Data("PackagingTypes_Lifts");
            PackagingTypesLinerBagDry = datamanager.Data("PackagingTypes_LinerBagDry");
            PackagingTypesLinerBagLiquid = datamanager.Data("PackagingTypes_LinerBagLiquid");
            PackagingTypesLiquidBulk = datamanager.Data("PackagingTypes_LiquidBulk");
            PackagingTypesLog = datamanager.Data("PackagingTypes_Log");
            PackagingTypesLoose = datamanager.Data("PackagingTypes_Loose");
            PackagingTypesLug = datamanager.Data("PackagingTypes_Lug");
            PackagingTypesMilitaryVan = datamanager.Data("PackagingTypes_MilitaryVan");
            PackagingTypesMixedTypePack = datamanager.Data("PackagingTypes_MixedTypePack");
            PackagingTypesMultiRollPack = datamanager.Data("PackagingTypes_MultiRollPack");
            PackagingTypesMultiwallContainerSecuredToWarehousePallet = datamanager.Data("PackagingTypes_MultiwallContainerSecuredToWarehousePallet");
            PackagingTypesNoil = datamanager.Data("PackagingTypes_Noil");
            PackagingTypesOnHangerOrRackInBoxes = datamanager.Data("PackagingTypes_OnHangerOrRackInBoxes");
            PackagingTypesOverwrap = datamanager.Data("PackagingTypes_Overwrap");
            PackagingTypesPackage = datamanager.Data("PackagingTypes_Package");
            PackagingTypesPail = datamanager.Data("PackagingTypes_Pail");
            PackagingTypesPallet = datamanager.Data("PackagingTypes_Pallet");
            PackagingTypesPieces = datamanager.Data("PackagingTypes_Pieces");
            PackagingTypesPims = datamanager.Data("PackagingTypes_Pims");
            PackagingTypesPipeRack = datamanager.Data("PackagingTypes_PipeRack");
            PackagingTypesPlatform = datamanager.Data("PackagingTypes_Platform");
            PackagingTypesPrivateVehicle = datamanager.Data("PackagingTypes_PrivateVehicle");
            PackagingTypesRack = datamanager.Data("PackagingTypes_Rack");
            PackagingTypesReel = datamanager.Data("PackagingTypes_Reel");
            PackagingTypesRoll = datamanager.Data("PackagingTypes_Roll");
            PackagingTypesSack = datamanager.Data("PackagingTypes_Sack");
            PackagingTypesSkid = datamanager.Data("PackagingTypes_Skid");
            PackagingTypesSleeve = datamanager.Data("PackagingTypes_Sleeve");
            PackagingTypesSlipSheet = datamanager.Data("PackagingTypes_SlipSheet");
            PackagingTypesSpool = datamanager.Data("PackagingTypes_Spool");
            PackagingTypesTank = datamanager.Data("PackagingTypes_Tank");
            PackagingTypesToteBin = datamanager.Data("PackagingTypes_ToteBin");
            PackagingTypesToteCan = datamanager.Data("PackagingTypes_ToteCan");
            PackagingTypesTray = datamanager.Data("PackagingTypes_Tray");
            PackagingTypesTriwallBox = datamanager.Data("PackagingTypes_TriwallBox");
            PackagingTypesTruck = datamanager.Data("PackagingTypes_Truck");
            PackagingTypesTub = datamanager.Data("PackagingTypes_Tub");
            PackagingTypesTube = datamanager.Data("PackagingTypes_Tube");
            PackagingTypesUnit = datamanager.Data("PackagingTypes_Unit");
            PackagingTypesVanPack = datamanager.Data("PackagingTypes_VanPack");
            PackagingTypesVehicles = datamanager.Data("PackagingTypes_Vehicles");

            //Equipment Lengths
            EquipmentLengths20 = datamanager.Data("EquipmentLengths20");
            EquipmentLengths40 = datamanager.Data("EquipmentLengths40");
            EquipmentLengths45 = datamanager.Data("EquipmentLengths45");
            EquipmentLengths48 = datamanager.Data("EquipmentLengths48");
            EquipmentLengths53 = datamanager.Data("EquipmentLengths53");

            //Shipping Unit
            ShippingUnitStackable = datamanager.Data("ShippingUnit_Stackable");
            ShippingUnitOverDimension = datamanager.Data("ShippingUnit_OverDimension");

            //Commodities
            CommoditiesThreeFieldSKU = datamanager.Data("Commodities_ThreeFieldSKU");
            CommoditiesShowDueDate = datamanager.Data("Commodities_ShowDueDate");

            //Reference Type
            ReferenceTypeBillOfLading = datamanager.Data("ReferenceType_BillOfLading");
            ReferenceTypeDelivery = datamanager.Data("ReferenceType_Delivery");
            ReferenceTypePickUp = datamanager.Data("ReferenceType_PickUp");
            ReferenceTypePurchaseOrder = datamanager.Data("ReferenceType_PurchaseOrder");
            ReferenceTypeSalesOrder = datamanager.Data("ReferenceType_SalesOrder");

            //LTL Reference Type
            LTLReferenceTypeBillOfLading = datamanager.Data("LTLReferenceType_BillOfLading");
            LTLReferenceTypeCommercialInvoice = datamanager.Data("LTLReferenceType_CommercialInvoice");
            LTLReferenceTypeDistributor = datamanager.Data("LTLReferenceType_Distributor");
            LTLReferenceTypeFranchiseId = datamanager.Data("LTLReferenceType_FranchiseId");
            LTLReferenceTypeKomModelNumber = datamanager.Data("LTLReferenceType_KomModelNumber");
            LTLReferenceTypeKomSalesOrder = datamanager.Data("LTLReferenceType_KomSalesOrder");
            LTLReferenceTypeKomSerialNumber = datamanager.Data("LTLReferenceType_KomSerialNumber");
            LTLReferenceTypeMasterBOLNumber = datamanager.Data("LTLReferenceType_MasterBOLNumber");
            LTLReferenceTypeOrderNumber = datamanager.Data("LTLReferenceType_OrderNumber");
            LTLReferenceTypePONumber = datamanager.Data("LTLReferenceType_PONumber");
            LTLReferenceTypePRONumber = datamanager.Data("LTLReferenceType_PRONumber");
            LTLReferenceTypeQuoteNumber = datamanager.Data("LTLReferenceType_QuoteNumber");
            LTLReferenceTypeShipmentNumber = datamanager.Data("LTLReferenceType_ShipmentNumber");
            LTLReferenceTypeVendorCode = datamanager.Data("LTLReferenceType_VendorCode");

            //MY LOADS
            MyLoadsDocumentVisibility = datamanager.Data("MyLoads_DocumentVisibility");
            MyLoadsHideManagedChargesFromCarrier = datamanager.Data("MyLoads_HideManagedChargesFromCarrier");
            MyLoadsHideAllChargesFromCustomer = datamanager.Data("MyLoads_HideAllChargesFromCustomer");
            MyLoadsHideAllCarrierTrackingNotesFromCustomerAndFacilities = datamanager.Data("MyLoads_HideAllCarrierTrackingNotesFromCustomerAndFacilities");
            MyLoadsAllowCarriersToEditBOLAndPODNumbers = datamanager.Data("MyLoads_AllowCarriersToEditBOLAndPODNumbers");
            MyLoadsAllowUsersToUpdateLoadStopTimesInAnyOrder = datamanager.Data("MyLoads_AllowUsersToUpdateLoadStopTimesInAnyOrder");

            //REPORTING
            PremiumReporting = datamanager.Data("PremiumReporting");

            //ADDITIONAL SETTINGS
            AdditionalSettingsShowFacilityHours = datamanager.Data("AdditionalSettings_ShowFacilityHours");
            AdditionalSettingsShowTenderHistory = datamanager.Data("AdditionalSettings_ShowTenderHistory");
            AdditionalSettingsShowCommodityValue = datamanager.Data("AdditionalSettings_ShowCommodityValue");
            AdditionalSettingsShowLoadStopNotes = datamanager.Data("AdditionalSettings_ShowLoadStopNotes");
            AdditionalSettingsShowAppointmentSchedule = datamanager.Data("AdditionalSettings_ShowAppointmentSchedule");
            AdditionalSettingsShowAllCustomerChildren = datamanager.Data("AdditionalSettings_ShowAllCustomerChildren");

            //CUT-OFF TIME
            ExcludeWeekendsForShipmentOrder = datamanager.Data("ExcludeWeekendsForShipmentOrder");
            PaddedDaysForPastDueOrders = datamanager.Data("PaddedDaysForPastDueOrders");
            CreateLoadUsingOptimizationTool = datamanager.Data("CreateLoadUsingOptimizationTool");
            CutOffTimeForPastDueOrders = datamanager.Data("CutOffTimeForPastDueOrders");
            CutOffTimeToSubmitOrderForShipping = datamanager.Data("CutOffTimeToSubmitOrderForShipping");
            StandardLeadTime = datamanager.Data("StandardLeadTime");
            LeadTimeAfterCutOff = datamanager.Data("LeadTimeAfterCutOff");
            StandardLeadTimeLabel = datamanager.Data("StandardLeadTimeLabel");

            //CommodityQuantityValidation
            ActualPiecesVaryPercent = datamanager.Data("ActualPiecesVaryPercent");
            ActualPiecesVaryCheckBox = datamanager.Data("ActualPiecesVaryCheckBox");
            ActualWeightVaryPercent = datamanager.Data("ActualWeightVaryPercent");
            ActualWeightVaryCheckBox = datamanager.Data("ActualWeightVaryCheckBox");
            StopUserFromSaving = datamanager.Data("StopUserFromSaving");
        }

        public string StopUserFromSaving { get; set; }
        public string ActualPiecesVaryCheckBox { get; set; }
        public string ActualPiecesVaryPercent { get; set; }
        public string ActualWeightVaryPercent { get; set; }
        public string ActualWeightVaryCheckBox { get; set; }
        public string FacilityRelationships { get; set; }
        public string EnableSchedulingInbound { get; set; }
        public string EnableSchedulingOutbound { get; set; }
        public string EnableFacilityCalendar { get; set; }
        public string TenderSettingsAddCarrierForTrackingOnly { get; set; }
        public string TenderSettingsSequenceForLtlTenderingBasedOnRoutingGuideNotOnCost { get; set; }
        public string TenderSettingsHideCustomerName { get; set; }
        public string TenderSettingsHideCarrierRate { get; set; }
        public string TenderSettingsUseCustomerRepAsCarrierRep { get; set; }
        public string TenderSettingsUseCarrierCostAsCustomerRate { get; set; }
        public string TenderSettingsSendRateConfirmationToCarrier { get; set; }
        public string TenderSettingsDoNotUseAutomaticCarrierRatingForManagedLtlLoads { get; set; }
        public string TenderSettingsSkipLtlCarriersWhoAreUnableToMakeDeliveryDate { get; set; }
        public string TenderSettingsApplyFscByDefault { get; set; }
        public string TenderSettingsIgnoreCarrierQualificationErrorsWhenTendering { get; set; }
        public string SpotTenderTimeLimitInMin { get; set; }
        public string RoutingGuideExhaustedNotifyLoadCustomerOpsReps { get; set; }
        public string RoutingGuideExhaustedNotifyMainCustomerContact { get; set; }
        public string RoutingGuideExhaustedSendToTheOpenBoard { get; set; }
        public string EnableOrderManager { get; set; }
        public string ModeIntermodal { get; set; }
        public string ModeTruckload { get; set; }
        public string ModeLessThanTruckload { get; set; }
        public string EquipmentAutoTrailer { get; set; }
        public string EquipmentBeamTrailer { get; set; }
        public string EquipmentBoxcar { get; set; }
        public string EquipmentBulk { get; set; }
        public string EquipmentCNRU { get; set; }
        public string EquipmentConestoga { get; set; }
        public string EquipmentContainer { get; set; }
        public string EquipmentCPPU { get; set; }
        public string EquipmentCSXU { get; set; }
        public string EquipmentDeckedReefer { get; set; }
        public string EquipmentDoubleDropLowboy { get; set; }
        public string EquipmentDropFrame { get; set; }
        public string EquipmentDuraplateVan { get; set; }
        public string EquipmentEMHU { get; set; }
        public string EquipmentEMPU { get; set; }
        public string EquipmentEMWU { get; set; }
        public string EquipmentEPTY { get; set; }
        public string EquipmentEuroliner { get; set; }
        public string EquipmentFlatCar { get; set; }
        public string EquipmentFlatbed { get; set; }
        public string EquipmentFlatbedStretch { get; set; }
        public string EquipmentFlatbedWithSides { get; set; }
        public string EquipmentFlatbedWithTarp { get; set; }
        public string EquipmentHeatedVan { get; set; }
        public string EquipmentHotShot { get; set; }
        public string EquipmentLandall { get; set; }
        public string EquipmentMaxiFlatbed { get; set; }
        public string EquipmentMaxiVan { get; set; }
        public string EquipmentMegatrailer { get; set; }
        public string EquipmentPACU { get; set; }
        public string EquipmentPowerOnly { get; set; }
        public string EquipmentReefer { get; set; }
        public string EquipmentReeferContainer { get; set; }
        public string EquipmentRemovableGooseNeck { get; set; }
        public string EquipmentRGNMultiAxel { get; set; }
        public string EquipmentRGNStretch { get; set; }
        public string EquipmentRoadTrain { get; set; }
        public string EquipmentSiloTanker { get; set; }
        public string EquipmentSoftShell { get; set; }
        public string EquipmentStepDeck { get; set; }
        public string EquipmentStepDeckStretch { get; set; }
        public string EquipmentStepdeckWithRamps { get; set; }
        public string EquipmentTanker { get; set; }
        public string EquipmentTilt { get; set; }
        public string EquipmentVan { get; set; }
        public string EquipmentVentedVan { get; set; }
        public string EquipmentWalkingFloor { get; set; }
        public string PackagingTypesBag { get; set; }
        public string PackagingTypesBale { get; set; }
        public string PackagingTypesBarge { get; set; }
        public string PackagingTypesBarrel { get; set; }
        public string PackagingTypesBasketOrHamper { get; set; }
        public string PackagingTypesBeam { get; set; }
        public string PackagingTypesBin { get; set; }
        public string PackagingTypesBobbin { get; set; }
        public string PackagingTypesBottle { get; set; }
        public string PackagingTypesBox { get; set; }
        public string PackagingTypesBoxWithInnerContainer { get; set; }
        public string PackagingTypesBucket { get; set; }
        public string PackagingTypesBulk { get; set; }
        public string PackagingTypesBulkBag { get; set; }
        public string PackagingTypesBundle { get; set; }
        public string PackagingTypesCabinet { get; set; }
        public string PackagingTypesCage { get; set; }
        public string PackagingTypesCan { get; set; }
        public string PackagingTypesCanCase { get; set; }
        public string PackagingTypesCarLoadRail { get; set; }
        public string PackagingTypesCarboy { get; set; }
        public string PackagingTypesCarrier { get; set; }
        public string PackagingTypesCarton { get; set; }
        public string PackagingTypesCase { get; set; }
        public string PackagingTypesCask { get; set; }
        public string PackagingTypesCheeses { get; set; }
        public string PackagingTypesChest { get; set; }
        public string PackagingTypesCoil { get; set; }
        public string PackagingTypesCones { get; set; }
        public string PackagingTypesContainer { get; set; }
        public string PackagingTypesContainersOfBulkCargo { get; set; }
        public string PackagingTypesCore { get; set; }
        public string PackagingTypesCradle { get; set; }
        public string PackagingTypesCrate { get; set; }
        public string PackagingTypesCube { get; set; }
        public string PackagingTypesCylinder { get; set; }
        public string PackagingTypesDoubleLengthRack { get; set; }
        public string PackagingTypesDoubleLengthSkid { get; set; }
        public string PackagingTypesDoubleLengthToteBin { get; set; }
        public string PackagingTypesDrum { get; set; }
        public string PackagingTypesDryBulk { get; set; }
        public string PackagingTypesDuffleBag { get; set; }
        public string PackagingTypesEngineContainer { get; set; }
        public string PackagingTypesEnvelope { get; set; }
        public string PackagingTypesFirkin { get; set; }
        public string PackagingTypesFlask { get; set; }
        public string PackagingTypesFloBin { get; set; }
        public string PackagingTypesForwardReel { get; set; }
        public string PackagingTypesFrame { get; set; }
        public string PackagingTypesGarmentsOnHangers { get; set; }
        public string PackagingTypesHalfStandardRack { get; set; }
        public string PackagingTypesHalfStandardToteBin { get; set; }
        public string PackagingTypesHamper { get; set; }
        public string PackagingTypesHeadsOfBeef { get; set; }
        public string PackagingTypesHogshead { get; set; }
        public string PackagingTypesHopperTruck { get; set; }
        public string PackagingTypesIntermediateBulkContainers { get; set; }
        public string PackagingTypesJar { get; set; }
        public string PackagingTypesJug { get; set; }
        public string PackagingTypesKeg { get; set; }
        public string PackagingTypesKit { get; set; }
        public string PackagingTypesKnockdownRack { get; set; }
        public string PackagingTypesKnockdownToteBin { get; set; }
        public string PackagingTypesLiftVan { get; set; }
        public string PackagingTypesLifts { get; set; }
        public string PackagingTypesLinerBagDry { get; set; }
        public string PackagingTypesLinerBagLiquid { get; set; }
        public string PackagingTypesLiquidBulk { get; set; }
        public string PackagingTypesLog { get; set; }
        public string PackagingTypesLoose { get; set; }
        public string PackagingTypesLug { get; set; }
        public string PackagingTypesMilitaryVan { get; set; }
        public string PackagingTypesMixedTypePack { get; set; }
        public string PackagingTypesMultiRollPack { get; set; }
        public string PackagingTypesMultiwallContainerSecuredToWarehousePallet { get; set; }
        public string PackagingTypesNoil { get; set; }
        public string PackagingTypesOnHangerOrRackInBoxes { get; set; }
        public string PackagingTypesOverwrap { get; set; }
        public string PackagingTypesPackage { get; set; }
        public string PackagingTypesPail { get; set; }
        public string PackagingTypesPallet { get; set; }
        public string PackagingTypesPieces { get; set; }
        public string PackagingTypesPims { get; set; }
        public string PackagingTypesPipeRack { get; set; }
        public string PackagingTypesPlatform { get; set; }
        public string PackagingTypesPrivateVehicle { get; set; }
        public string PackagingTypesRack { get; set; }
        public string PackagingTypesReel { get; set; }
        public string PackagingTypesRoll { get; set; }
        public string PackagingTypesSack { get; set; }
        public string PackagingTypesSkid { get; set; }
        public string PackagingTypesSleeve { get; set; }
        public string PackagingTypesSlipSheet { get; set; }
        public string PackagingTypesSpool { get; set; }
        public string PackagingTypesTank { get; set; }
        public string PackagingTypesToteBin { get; set; }
        public string PackagingTypesToteCan { get; set; }
        public string PackagingTypesTray { get; set; }
        public string PackagingTypesTriwallBox { get; set; }
        public string PackagingTypesTruck { get; set; }
        public string PackagingTypesTub { get; set; }
        public string PackagingTypesTube { get; set; }
        public string PackagingTypesUnit { get; set; }
        public string PackagingTypesVanPack { get; set; }
        public string PackagingTypesVehicles { get; set; }
        public string EquipmentLengths20 { get; set; }
        public string EquipmentLengths40 { get; set; }
        public string EquipmentLengths45 { get; set; }
        public string EquipmentLengths48 { get; set; }
        public string EquipmentLengths53 { get; set; }
        public string ShippingUnitStackable { get; set; }
        public string ShippingUnitOverDimension { get; set; }
        public string CommoditiesThreeFieldSKU { get; set; }
        public string CommoditiesShowDueDate { get; set; }
        public string ReferenceTypeBillOfLading { get; set; }
        public string ReferenceTypeDelivery { get; set; }
        public string ReferenceTypePickUp { get; set; }
        public string ReferenceTypePurchaseOrder { get; set; }
        public string ReferenceTypeSalesOrder { get; set; }
        public string LTLReferenceTypeBillOfLading { get; set; }
        public string LTLReferenceTypeCommercialInvoice { get; set; }
        public string LTLReferenceTypeDistributor { get; set; }
        public string LTLReferenceTypeFranchiseId { get; set; }
        public string LTLReferenceTypeKomModelNumber { get; set; }
        public string LTLReferenceTypeKomSalesOrder { get; set; }
        public string LTLReferenceTypeKomSerialNumber { get; set; }
        public string LTLReferenceTypeMasterBOLNumber { get; set; }
        public string LTLReferenceTypeOrderNumber { get; set; }
        public string LTLReferenceTypePONumber { get; set; }
        public string LTLReferenceTypePRONumber { get; set; }
        public string LTLReferenceTypeQuoteNumber { get; set; }
        public string LTLReferenceTypeShipmentNumber { get; set; }
        public string LTLReferenceTypeVendorCode { get; set; }
        public string MyLoadsDocumentVisibility { get; set; }
        public string MyLoadsHideManagedChargesFromCarrier { get; set; }
        public string MyLoadsHideAllChargesFromCustomer { get; set; }
        public string MyLoadsHideAllCarrierTrackingNotesFromCustomerAndFacilities { get; set; }
        public string MyLoadsAllowCarriersToEditBOLAndPODNumbers { get; set; }
        public string MyLoadsAllowUsersToUpdateLoadStopTimesInAnyOrder { get; set; }
        public string PremiumReporting { get; set; }
        public string AdditionalSettingsShowFacilityHours { get; set; }
        public string AdditionalSettingsShowTenderHistory { get; set; }
        public string AdditionalSettingsShowCommodityValue { get; set; }
        public string AdditionalSettingsShowLoadStopNotes { get; set; }
        public string AdditionalSettingsShowAppointmentSchedule { get; set; }
        public string AdditionalSettingsShowAllCustomerChildren { get; set; }
        public string ExcludeWeekendsForShipmentOrder { get; set; }
        public string PaddedDaysForPastDueOrders { get; set; }
        public string CreateLoadUsingOptimizationTool { get; set; }
        public string CutOffTimeForPastDueOrders { get; set; }
        public string CutOffTimeToSubmitOrderForShipping { get; set; }
        public string StandardLeadTime { get; set; }
        public string LeadTimeAfterCutOff { get; set; }
        public string StandardLeadTimeLabel { get; set; }

    }
}
