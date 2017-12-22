using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;

namespace SWAT.Applications.Claw
{
    using SWAT.Applications.Claw.DAL;
    using SWAT.Applications.Claw.ObjectRepository;
    using SWAT.Configuration;
    using SWAT.Data;
    using SWAT.Logger;
    using SWAT.FrameWork.Utilities;
    using System;   

    class ManageCustomers
    {
        ManageCustomersData _ManageCustomersData;
        ManageCustomersPage _ManageCustomersPage;


        public ManageCustomers(TestStartInfo teststartinfo, DataManager datamanager)
        {
            _ManageCustomersData = new ManageCustomersData(datamanager);
            _ManageCustomersPage = new ManageCustomersPage(teststartinfo);
        }

        public string FacilityRelationshipsStatusCheckAndUpdate()
        {
            try
            {
                Assert.IsTrue(_ManageCustomersPage.AppTitle.GetText().Trim().Equals("Manage Customers"));
                Assert.IsTrue(_ManageCustomersPage.FacilityRelationships.StatusCheckORPerFormAction(_ManageCustomersData.FacilityRelationships));
                return "ManageCustomersFacilityRelationshipsStatusCheckAndUpdateSuccess";
            }
            catch
            {
                return "ManageCustomersFacilityRelationshipsStatusCheckAndUpdateFailed";
            }
        }

        public string ScheduleLoadsStatusCheckAndUpdate()
        {
            try
            {
                Assert.IsTrue(_ManageCustomersPage.AppTitle.GetText().Trim().Equals("Manage Customers"));
                Assert.IsTrue(_ManageCustomersPage.EnableSchedulingInbound.StatusCheckORPerFormAction(_ManageCustomersData.EnableSchedulingInbound));
                Assert.IsTrue(_ManageCustomersPage.EnableSchedulingOutbound.StatusCheckORPerFormAction(_ManageCustomersData.EnableSchedulingOutbound));
                return "ManageCustomersScheduleLoadsStatusCheckAndUpdateSuccess";
            }
            catch
            {
                return "ManageCustomersScheduleLoadsStatusCheckAndUpdateFailed";
            }
        }

        public string FacilityCalendarStatusCheckAndUpdate()
        {
            try
            {
                Assert.IsTrue(_ManageCustomersPage.AppTitle.GetText().Trim().Equals("Manage Customers"));
                Assert.IsTrue(_ManageCustomersPage.EnableFacilityCalendar.StatusCheckORPerFormAction(_ManageCustomersData.EnableFacilityCalendar));
                return "ManageCustomersFacilityCalendarStatusCheckAndUpdateSuccess";
            }
            catch
            {
                return "ManageCustomersFacilityCalendarStatusCheckAndUpdateFailed";
            }
        }

        public string TenderSettingsStatusCheckAndUpdate()
        {
            try
            {
                Assert.IsTrue(_ManageCustomersPage.CustomerSummaryDetailsRegion.WaitUntilDisplayed());
                Assert.IsTrue(_ManageCustomersPage.AppTitle.GetText().Trim().Equals("Manage Customers"));
                Assert.IsTrue(_ManageCustomersPage.AutomationAndTms.Click());
                Thread.Sleep(Constants.Wait_Medium);
                Assert.IsTrue(_ManageCustomersPage.TenderSettingText.WaitUntilDisplayed());
                Assert.IsTrue(_ManageCustomersPage.TenderSettingText.GetText().Trim().Equals("Tender Settings"));
                Assert.IsTrue(_ManageCustomersPage.TenderSettingsAddCarrierForTrackingOnly.StatusCheckORPerFormAction(_ManageCustomersData.TenderSettingsAddCarrierForTrackingOnly));
                Assert.IsTrue(_ManageCustomersPage.TenderSettingsSequenceForLtlTenderingBasedOnRoutingGuideNotOnCost.StatusCheckORPerFormAction(_ManageCustomersData.TenderSettingsSequenceForLtlTenderingBasedOnRoutingGuideNotOnCost));
                Assert.IsTrue(_ManageCustomersPage.TenderSettingsHideCustomerName.StatusCheckORPerFormAction(_ManageCustomersData.TenderSettingsHideCustomerName));
                Assert.IsTrue(_ManageCustomersPage.TenderSettingsHideCarrierRate.StatusCheckORPerFormAction(_ManageCustomersData.TenderSettingsHideCarrierRate));
                Assert.IsTrue(_ManageCustomersPage.TenderSettingsUseCustomerRepAsCarrierRep.StatusCheckORPerFormAction(_ManageCustomersData.TenderSettingsUseCustomerRepAsCarrierRep));
                Assert.IsTrue(_ManageCustomersPage.TenderSettingsUseCarrierCostAsCustomerRate.StatusCheckORPerFormAction(_ManageCustomersData.TenderSettingsUseCarrierCostAsCustomerRate));
                Assert.IsTrue(_ManageCustomersPage.TenderSettingsSendRateConfirmationToCarrier.StatusCheckORPerFormAction(_ManageCustomersData.TenderSettingsSendRateConfirmationToCarrier));
                Assert.IsTrue(_ManageCustomersPage.TenderSettingsDoNotUseAutomaticCarrierRatingForManagedLtlLoads.StatusCheckORPerFormAction(_ManageCustomersData.TenderSettingsDoNotUseAutomaticCarrierRatingForManagedLtlLoads));
                Assert.IsTrue(_ManageCustomersPage.TenderSettingsSkipLtlCarriersWhoAreUnableToMakeDeliveryDate.StatusCheckORPerFormAction(_ManageCustomersData.TenderSettingsSkipLtlCarriersWhoAreUnableToMakeDeliveryDate));
                Assert.IsTrue(_ManageCustomersPage.TenderSettingsApplyFscByDefault.StatusCheckORPerFormAction(_ManageCustomersData.TenderSettingsApplyFscByDefault));
                Assert.IsTrue(_ManageCustomersPage.TenderSettingsIgnoreCarrierQualificationErrorsWhenTendering.StatusCheckORPerFormAction(_ManageCustomersData.TenderSettingsIgnoreCarrierQualificationErrorsWhenTendering));
                Assert.IsTrue(_ManageCustomersPage.SpotTenderTimeLimitInMin.ClearAndEdit(_ManageCustomersData.SpotTenderTimeLimitInMin));
                return "ManageCustomersTenderSettingsStatusCheckAndUpdateSuccess";
            }
            catch
            {
                return "ManageCustomersTenderSettingsStatusCheckAndUpdateFailed";
            }
        }

        public string RoutingGuideExhaustedStatusCheckAndUpdate()
        {
            try
            {
                Assert.IsTrue(_ManageCustomersPage.AppTitle.GetText().Trim().Equals("Manage Customers"));
                Assert.IsTrue(_ManageCustomersPage.RoutingGuideExhaustedNotifyLoadCustomerOpsReps.StatusCheckORPerFormAction(_ManageCustomersData.RoutingGuideExhaustedNotifyLoadCustomerOpsReps));
                Assert.IsTrue(_ManageCustomersPage.RoutingGuideExhaustedNotifyMainCustomerContact.StatusCheckORPerFormAction(_ManageCustomersData.RoutingGuideExhaustedNotifyMainCustomerContact));
                Assert.IsTrue(_ManageCustomersPage.RoutingGuideExhaustedSendToTheOpenBoard.StatusCheckORPerFormAction(_ManageCustomersData.RoutingGuideExhaustedSendToTheOpenBoard));
                return "ManageCustomersRoutingGuideExhaustedStatusCheckAndUpdateSuccess";
            }
            catch
            {
                return "ManageCustomersRoutingGuideExhaustedStatusCheckAndUpdateFailed";
            }
        }

        public string OrderManagerStatusCheckAndUpdate()
        {
            try
            {
                Assert.IsTrue(_ManageCustomersPage.AppTitle.GetText().Trim().Equals("Manage Customers"));
                Assert.IsTrue(_ManageCustomersPage.EnableOrderManager.StatusCheckORPerFormAction(_ManageCustomersData.EnableOrderManager));
                return "ManageCustomersOrderManagerStatusCheckAndUpdateSuccess";
            }
            catch
            {
                return "ManageCustomersOrderManagerStatusCheckAndUpdateFailed";
            }
        }

        public string ModeStatusCheckAndUpdate()
        {
            try
            {
                Assert.IsTrue(_ManageCustomersPage.AppTitle.GetText().Trim().Equals("Manage Customers"));
                Assert.IsTrue(_ManageCustomersPage.ModeIntermodal.StatusCheckORPerFormAction(_ManageCustomersData.ModeIntermodal));
                Assert.IsTrue(_ManageCustomersPage.ModeTruckload.StatusCheckORPerFormAction(_ManageCustomersData.ModeTruckload));
                Assert.IsTrue(_ManageCustomersPage.ModeLessThanTruckload.StatusCheckORPerFormAction(_ManageCustomersData.ModeLessThanTruckload));
                return "ManageCustomersModeStatusCheckAndUpdateSuccess";
            }
            catch
            {
                return "ManageCustomersModeStatusCheckAndUpdateFailed";
            }
        }

        public string EquipmentStatusCheckAndUpdate()
        {
            try
            {
                Assert.IsTrue(_ManageCustomersPage.AppTitle.GetText().Trim().Equals("Manage Customers"));
                Assert.IsTrue(_ManageCustomersPage.EquipmentAutoTrailer.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentAutoTrailer));
                Assert.IsTrue(_ManageCustomersPage.EquipmentBeamTrailer.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentBeamTrailer));
                Assert.IsTrue(_ManageCustomersPage.EquipmentBoxcar.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentBoxcar));
                Assert.IsTrue(_ManageCustomersPage.EquipmentBulk.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentBulk));
                Assert.IsTrue(_ManageCustomersPage.EquipmentCNRU.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentCNRU));
                Assert.IsTrue(_ManageCustomersPage.EquipmentConestoga.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentConestoga));
                Assert.IsTrue(_ManageCustomersPage.EquipmentContainer.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentContainer));
                Assert.IsTrue(_ManageCustomersPage.EquipmentCPPU.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentCPPU));
                Assert.IsTrue(_ManageCustomersPage.EquipmentCSXU.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentCSXU));
                Assert.IsTrue(_ManageCustomersPage.EquipmentDeckedReefer.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentDeckedReefer));
                Assert.IsTrue(_ManageCustomersPage.EquipmentDoubleDropLowboy.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentDoubleDropLowboy));
                Assert.IsTrue(_ManageCustomersPage.EquipmentDropFrame.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentDropFrame));
                Assert.IsTrue(_ManageCustomersPage.EquipmentDuraplateVan.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentDuraplateVan));
                Assert.IsTrue(_ManageCustomersPage.EquipmentEMHU.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentEMHU));
                Assert.IsTrue(_ManageCustomersPage.EquipmentEMPU.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentEMPU));
                Assert.IsTrue(_ManageCustomersPage.EquipmentEMWU.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentEMWU));
                Assert.IsTrue(_ManageCustomersPage.EquipmentEPTY.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentEPTY));
                Assert.IsTrue(_ManageCustomersPage.EquipmentEuroliner.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentEuroliner));
                Assert.IsTrue(_ManageCustomersPage.EquipmentFlatCar.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentFlatCar));
                Assert.IsTrue(_ManageCustomersPage.EquipmentFlatbed.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentFlatbed));
                Assert.IsTrue(_ManageCustomersPage.EquipmentFlatbedStretch.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentFlatbedStretch));
                Assert.IsTrue(_ManageCustomersPage.EquipmentFlatbedWithSides.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentFlatbedWithSides));
                Assert.IsTrue(_ManageCustomersPage.EquipmentFlatbedWithTarp.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentFlatbedWithTarp));
                Assert.IsTrue(_ManageCustomersPage.EquipmentHeatedVan.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentHeatedVan));
                Assert.IsTrue(_ManageCustomersPage.EquipmentHotShot.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentHotShot));
                Assert.IsTrue(_ManageCustomersPage.EquipmentLandall.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentLandall));
                Assert.IsTrue(_ManageCustomersPage.EquipmentMaxiFlatbed.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentMaxiFlatbed));
                Assert.IsTrue(_ManageCustomersPage.EquipmentMaxiVan.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentMaxiVan));
                Assert.IsTrue(_ManageCustomersPage.EquipmentMegatrailer.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentMegatrailer));
                Assert.IsTrue(_ManageCustomersPage.EquipmentPACU.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentPACU));
                Assert.IsTrue(_ManageCustomersPage.EquipmentPowerOnly.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentPowerOnly));
                Assert.IsTrue(_ManageCustomersPage.EquipmentReefer.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentReefer));
                Assert.IsTrue(_ManageCustomersPage.EquipmentReeferContainer.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentReeferContainer));
                Assert.IsTrue(_ManageCustomersPage.EquipmentRemovableGooseNeck.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentRemovableGooseNeck));
                Assert.IsTrue(_ManageCustomersPage.EquipmentRGNMultiAxel.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentRGNMultiAxel));
                Assert.IsTrue(_ManageCustomersPage.EquipmentRGNStretch.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentRGNStretch));
                Assert.IsTrue(_ManageCustomersPage.EquipmentRoadTrain.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentRoadTrain));
                Assert.IsTrue(_ManageCustomersPage.EquipmentSiloTanker.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentSiloTanker));
                Assert.IsTrue(_ManageCustomersPage.EquipmentSoftShell.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentSoftShell));
                Assert.IsTrue(_ManageCustomersPage.EquipmentStepDeck.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentStepDeck));
                Assert.IsTrue(_ManageCustomersPage.EquipmentStepDeckStretch.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentStepDeckStretch));
                Assert.IsTrue(_ManageCustomersPage.EquipmentStepdeckWithRamps.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentStepdeckWithRamps));
                Assert.IsTrue(_ManageCustomersPage.EquipmentTanker.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentTanker));
                Assert.IsTrue(_ManageCustomersPage.EquipmentTilt.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentTilt));
                Assert.IsTrue(_ManageCustomersPage.EquipmentVan.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentVan));
                Assert.IsTrue(_ManageCustomersPage.EquipmentVentedVan.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentVentedVan));
                Assert.IsTrue(_ManageCustomersPage.EquipmentWalkingFloor.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentWalkingFloor));
                return "ManageCustomersEquipmentStatusCheckAndUpdateSuccess";
            }
            catch
            {
                return "ManageCustomersEquipmentStatusCheckAndUpdateFailed";
            }
        }

        public string PackagingTypesStatusCheckAndUpdate()
        {
            try
            {
                Assert.IsTrue(_ManageCustomersPage.AppTitle.GetText().Trim().Equals("Manage Customers"));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesBag.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesBag));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesBale.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesBale));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesBarge.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesBarge));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesBarrel.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesBarrel));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesBasketOrHamper.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesBasketOrHamper));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesBeam.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesBeam));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesBin.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesBin));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesBobbin.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesBobbin));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesBottle.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesBottle));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesBox.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesBox));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesBoxWithInnerContainer.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesBoxWithInnerContainer));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesBucket.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesBucket));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesBulk.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesBulk));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesBulkBag.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesBulkBag));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesBundle.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesBundle));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesCabinet.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesCabinet));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesCage.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesCage));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesCan.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesCan));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesCanCase.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesCanCase));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesCarLoadRail.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesCarLoadRail));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesCarboy.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesCarboy));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesCarrier.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesCarrier));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesCarton.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesCarton));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesCase.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesCase));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesCask.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesCask));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesCheeses.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesCheeses));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesChest.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesChest));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesCoil.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesCoil));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesCones.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesCones));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesContainer.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesContainer));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesContainersOfBulkCargo.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesContainersOfBulkCargo));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesCore.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesCore));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesCradle.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesCradle));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesCrate.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesCrate));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesCube.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesCube));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesCylinder.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesCylinder));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesDoubleLengthRack.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesDoubleLengthRack));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesDoubleLengthSkid.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesDoubleLengthSkid));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesDoubleLengthToteBin.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesDoubleLengthToteBin));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesDrum.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesDrum));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesDryBulk.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesDryBulk));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesDuffleBag.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesDuffleBag));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesEngineContainer.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesEngineContainer));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesEnvelope.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesEnvelope));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesFirkin.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesFirkin));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesFlask.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesFlask));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesFloBin.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesFloBin));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesForwardReel.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesForwardReel));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesFrame.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesFrame));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesGarmentsOnHangers.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesGarmentsOnHangers));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesHalfStandardRack.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesHalfStandardRack));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesHalfStandardToteBin.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesHalfStandardToteBin));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesHamper.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesHamper));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesHeadsOfBeef.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesHeadsOfBeef));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesHogshead.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesHogshead));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesHopperTruck.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesHopperTruck));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesIntermediateBulkContainers.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesIntermediateBulkContainers));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesJar.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesJar));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesJug.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesJug));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesKeg.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesKeg));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesKit.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesKit));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesKnockdownRack.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesKnockdownRack));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesKnockdownToteBin.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesKnockdownToteBin));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesLiftVan.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesLiftVan));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesLifts.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesLifts));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesLinerBagDry.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesLinerBagDry));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesLinerBagLiquid.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesLinerBagLiquid));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesLiquidBulk.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesLiquidBulk));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesLog.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesLog));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesLoose.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesLoose));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesLug.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesLug));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesMilitaryVan.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesMilitaryVan));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesMixedTypePack.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesMixedTypePack));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesMultiRollPack.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesMultiRollPack));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesMultiwallContainerSecuredToWarehousePallet.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesMultiwallContainerSecuredToWarehousePallet));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesNoil.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesNoil));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesOnHangerOrRackInBoxes.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesOnHangerOrRackInBoxes));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesOverwrap.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesOverwrap));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesPackage.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesPackage));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesPail.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesPail));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesPallet.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesPallet));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesPieces.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesPieces));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesPims.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesPims));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesPipeRack.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesPipeRack));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesPlatform.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesPlatform));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesPrivateVehicle.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesPrivateVehicle));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesRack.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesRack));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesReel.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesReel));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesRoll.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesRoll));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesSack.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesSack));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesSkid.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesSkid));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesSleeve.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesSleeve));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesSlipSheet.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesSlipSheet));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesSpool.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesSpool));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesTank.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesTank));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesToteBin.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesToteBin));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesToteCan.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesToteCan));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesTray.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesTray));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesTriwallBox.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesTriwallBox));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesTruck.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesTruck));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesTub.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesTub));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesTube.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesTube));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesUnit.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesUnit));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesVanPack.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesVanPack));
                Assert.IsTrue(_ManageCustomersPage.PackagingTypesVehicles.StatusCheckORPerFormAction(_ManageCustomersData.PackagingTypesVehicles));
                return "ManageCustomersPackagingTypesStatusCheckAndUpdateSuccess";
            }
            catch
            {
                return "ManageCustomersPackagingTypesStatusCheckAndUpdateFailed";
            }
        }

        public string EquipmentLengthsStatusCheckAndUpdate()
        {
            try
            {
                Assert.IsTrue(_ManageCustomersPage.AppTitle.GetText().Trim().Equals("Manage Customers"));
                Assert.IsTrue(_ManageCustomersPage.EquipmentLengths20.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentLengths20));
                Assert.IsTrue(_ManageCustomersPage.EquipmentLengths40.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentLengths40));
                Assert.IsTrue(_ManageCustomersPage.EquipmentLengths45.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentLengths45));
                Assert.IsTrue(_ManageCustomersPage.EquipmentLengths48.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentLengths48));
                Assert.IsTrue(_ManageCustomersPage.EquipmentLengths53.StatusCheckORPerFormAction(_ManageCustomersData.EquipmentLengths53));
                return "ManageCustomersEquipmentLengthsStatusCheckAndUpdateSuccess";
            }
            catch
            {
                return "ManageCustomersEquipmentLengthsStatusCheckAndUpdateFailed";
            }
        }

        public string ShippingUnitStatusCheckAndUpdate()
        {
            try
            {
                Assert.IsTrue(_ManageCustomersPage.AppTitle.GetText().Trim().Equals("Manage Customers"));
                Assert.IsTrue(_ManageCustomersPage.ShippingUnitStackable.StatusCheckORPerFormAction(_ManageCustomersData.ShippingUnitStackable));
                Assert.IsTrue(_ManageCustomersPage.ShippingUnitOverDimension.StatusCheckORPerFormAction(_ManageCustomersData.ShippingUnitOverDimension));
                return "ManageCustomersShippingUnitStatusCheckAndUpdateSuccess";
            }
            catch
            {
                return "ManageCustomersShippingUnitStatusCheckAndUpdateFailed";
            }
        }

        public string CommoditiesStatusCheckAndUpdate()
        {
            try
            {
                Assert.IsTrue(_ManageCustomersPage.AppTitle.GetText().Trim().Equals("Manage Customers"));
                Assert.IsTrue(_ManageCustomersPage.CommoditiesThreeFieldSKU.StatusCheckORPerFormAction(_ManageCustomersData.CommoditiesThreeFieldSKU));
                Assert.IsTrue(_ManageCustomersPage.CommoditiesShowDueDate.StatusCheckORPerFormAction(_ManageCustomersData.CommoditiesShowDueDate));
                return "ManageCustomersCommoditiesStatusCheckAndUpdateSuccess";
            }
            catch
            {
                return "ManageCustomersCommoditiesStatusCheckAndUpdateFailed";
            }
        }

        public string ReferenceTypeStatusCheckAndUpdate()
        {
            try
            {
                Assert.IsTrue(_ManageCustomersPage.AppTitle.GetText().Trim().Equals("Manage Customers"));
                Assert.IsTrue(_ManageCustomersPage.ReferenceTypeBillOfLading.StatusCheckORPerFormAction(_ManageCustomersData.ReferenceTypeBillOfLading));
                Assert.IsTrue(_ManageCustomersPage.ReferenceTypeDelivery.StatusCheckORPerFormAction(_ManageCustomersData.ReferenceTypeDelivery));
                Assert.IsTrue(_ManageCustomersPage.ReferenceTypePickUp.StatusCheckORPerFormAction(_ManageCustomersData.ReferenceTypePickUp));
                Assert.IsTrue(_ManageCustomersPage.ReferenceTypePurchaseOrder.StatusCheckORPerFormAction(_ManageCustomersData.ReferenceTypePurchaseOrder));
                Assert.IsTrue(_ManageCustomersPage.ReferenceTypeSalesOrder.StatusCheckORPerFormAction(_ManageCustomersData.ReferenceTypeSalesOrder));
                return "ManageCustomersReferenceTypeStatusCheckAndUpdateSuccess";
            }
            catch
            {
                return "ManageCustomersReferenceTypeStatusCheckAndUpdateFailed";
            }
        }

        public string LtlReferenceTypeStatusCheckAndUpdate()
        {
            try
            {
                Assert.IsTrue(_ManageCustomersPage.AppTitle.GetText().Trim().Equals("Manage Customers"));
                Assert.IsTrue(_ManageCustomersPage.LtlReferenceTypeBillOfLading.StatusCheckORPerFormAction(_ManageCustomersData.LTLReferenceTypeBillOfLading));
                Assert.IsTrue(_ManageCustomersPage.LtlReferenceTypeCommercialInvoice.StatusCheckORPerFormAction(_ManageCustomersData.LTLReferenceTypeCommercialInvoice));
                Assert.IsTrue(_ManageCustomersPage.LtlReferenceTypeDistributor.StatusCheckORPerFormAction(_ManageCustomersData.LTLReferenceTypeDistributor));
                Assert.IsTrue(_ManageCustomersPage.LtlReferenceTypeFranchiseId.StatusCheckORPerFormAction(_ManageCustomersData.LTLReferenceTypeFranchiseId));
                Assert.IsTrue(_ManageCustomersPage.LtlReferenceTypeKomModelNumber.StatusCheckORPerFormAction(_ManageCustomersData.LTLReferenceTypeKomModelNumber));
                Assert.IsTrue(_ManageCustomersPage.LtlReferenceTypeKomSalesOrder.StatusCheckORPerFormAction(_ManageCustomersData.LTLReferenceTypeKomSalesOrder));
                Assert.IsTrue(_ManageCustomersPage.LtlReferenceTypeKomSerialNumber.StatusCheckORPerFormAction(_ManageCustomersData.LTLReferenceTypeKomSerialNumber));
                Assert.IsTrue(_ManageCustomersPage.LtlReferenceTypeMasterBOLNumber.StatusCheckORPerFormAction(_ManageCustomersData.LTLReferenceTypeMasterBOLNumber));
                Assert.IsTrue(_ManageCustomersPage.LtlReferenceTypeOrderNumber.StatusCheckORPerFormAction(_ManageCustomersData.LTLReferenceTypeOrderNumber));
                Assert.IsTrue(_ManageCustomersPage.LtlReferenceTypePONumber.StatusCheckORPerFormAction(_ManageCustomersData.LTLReferenceTypePONumber));
                Assert.IsTrue(_ManageCustomersPage.LtlReferenceTypePRONumber.StatusCheckORPerFormAction(_ManageCustomersData.LTLReferenceTypePRONumber));
                Assert.IsTrue(_ManageCustomersPage.LtlReferenceTypeQuoteNumber.StatusCheckORPerFormAction(_ManageCustomersData.LTLReferenceTypeQuoteNumber));
                Assert.IsTrue(_ManageCustomersPage.LtlReferenceTypeShipmentNumber.StatusCheckORPerFormAction(_ManageCustomersData.LTLReferenceTypeShipmentNumber));
                Assert.IsTrue(_ManageCustomersPage.LtlReferenceTypeVendorCode.StatusCheckORPerFormAction(_ManageCustomersData.LTLReferenceTypeVendorCode));
                return "ManageCustomersLtlReferenceTypeStatusCheckAndUpdateSuccess";
            }
            catch
            {
                return "ManageCustomersLtlReferenceTypeStatusCheckAndUpdateFailed";
            }
        }

        public string MyLoadsStatusCheckAndUpdate()
        {
            try
            {
                Assert.IsTrue(_ManageCustomersPage.AppTitle.GetText().Trim().Equals("Manage Customers"));
                Assert.IsTrue(_ManageCustomersPage.MyLoadsDocumentVisibility.StatusCheckORPerFormAction(_ManageCustomersData.MyLoadsDocumentVisibility));
                Assert.IsTrue(_ManageCustomersPage.MyLoadsHideManagedChargesFromCarrier.StatusCheckORPerFormAction(_ManageCustomersData.MyLoadsHideManagedChargesFromCarrier));
                Assert.IsTrue(_ManageCustomersPage.MyLoadsHideAllChargesFromCustomer.StatusCheckORPerFormAction(_ManageCustomersData.MyLoadsHideAllChargesFromCustomer));
                Assert.IsTrue(_ManageCustomersPage.MyLoadsHideAllCarrierTrackingNotesFromCustomerAndFacilities.StatusCheckORPerFormAction(_ManageCustomersData.MyLoadsHideAllCarrierTrackingNotesFromCustomerAndFacilities));
                Assert.IsTrue(_ManageCustomersPage.MyLoadsAllowCarriersToEditBOLAndPODNumbers.StatusCheckORPerFormAction(_ManageCustomersData.MyLoadsAllowCarriersToEditBOLAndPODNumbers));
                Assert.IsTrue(_ManageCustomersPage.MyLoadsAllowUsersToUpdateLoadStopTimesInAnyOrder.StatusCheckORPerFormAction(_ManageCustomersData.MyLoadsAllowUsersToUpdateLoadStopTimesInAnyOrder));
                return "ManageCustomersMyLoadsStatusCheckAndUpdateSuccess";
            }
            catch
            {
                return "ManageCustomersMyLoadsStatusCheckAndUpdateFailed";
            }
        }

        public string PremiumReportingStatusCheckAndUpdate()
        {
            try
            {
                Assert.IsTrue(_ManageCustomersPage.AppTitle.GetText().Trim().Equals("Manage Customers"));
                Assert.IsTrue(_ManageCustomersPage.PremiumReporting.StatusCheckORPerFormAction(_ManageCustomersData.PremiumReporting));
                return "ManageCustomersPremiumReportingStatusCheckAndUpdateSuccess";
            }
            catch
            {
                return "ManageCustomersPremiumReportingStatusCheckAndUpdateFailed";
            }
        }

        public string AdditionalSettingsStatusCheckAndUpdate()
        {
            try
            {
                Assert.IsTrue(_ManageCustomersPage.AppTitle.GetText().Trim().Equals("Manage Customers"));
                Assert.IsTrue(_ManageCustomersPage.AdditionalSettingsShowFacilityHours.StatusCheckORPerFormAction(_ManageCustomersData.AdditionalSettingsShowFacilityHours));
                Assert.IsTrue(_ManageCustomersPage.AdditionalSettingsShowTenderHistory.StatusCheckORPerFormAction(_ManageCustomersData.AdditionalSettingsShowTenderHistory));
                Assert.IsTrue(_ManageCustomersPage.AdditionalSettingsShowCommodityValue.StatusCheckORPerFormAction(_ManageCustomersData.AdditionalSettingsShowCommodityValue));
                Assert.IsTrue(_ManageCustomersPage.AdditionalSettingsShowLoadStopNotes.StatusCheckORPerFormAction(_ManageCustomersData.AdditionalSettingsShowLoadStopNotes));
                Assert.IsTrue(_ManageCustomersPage.AdditionalSettingsShowAppointmentSchedule.StatusCheckORPerFormAction(_ManageCustomersData.AdditionalSettingsShowAppointmentSchedule));
                Assert.IsTrue(_ManageCustomersPage.AdditionalSettingsShowAllCustomerChildren.StatusCheckORPerFormAction(_ManageCustomersData.AdditionalSettingsShowAllCustomerChildren));
                return "ManageCustomersAdditionalSettingsStatusCheckAndUpdateSuccess";
            }
            catch
            {
                return "ManageCustomersAdditionalSettingsStatusCheckAndUpdateFailed";
            }
        }

        public string CutOffTimeStatusCheckAndUpdate(DataManager datamanager)
        {
            try
            {
                Assert.IsTrue(_ManageCustomersPage.AppTitle.GetText().Trim().Equals("Manage Customers"));
                Assert.IsTrue(_ManageCustomersPage.ExcludeWeekendsForShipmentOrder.StatusCheckORPerFormAction(_ManageCustomersData.ExcludeWeekendsForShipmentOrder));

                Assert.IsTrue(_ManageCustomersPage.PaddedDaysForPastDueOrders.ClearAndEdit(_ManageCustomersData.PaddedDaysForPastDueOrders));
                Assert.IsTrue(_ManageCustomersPage.PaddedDaysForPastDueOrders.Edit(Keys.Tab));
                Thread.Sleep(Constants.Wait_Short);
                Assert.IsTrue(_ManageCustomersPage.CreateLoadUsingOptimizationTool.StatusCheckORPerFormAction(_ManageCustomersData.CreateLoadUsingOptimizationTool));
                Assert.IsTrue(_ManageCustomersPage.CutOffTimeForPastDueOrders.ClearAndEditBox(datamanager.GetFormattedTime(_ManageCustomersData.CutOffTimeForPastDueOrders, "HH:mm")));
                Assert.IsTrue(_ManageCustomersPage.CutOffTimeForPastDueOrders.Edit(Keys.Tab));
                Thread.Sleep(Constants.Wait_Short);
                Assert.IsTrue(_ManageCustomersPage.CutOffTimeToSubmitOrderForShipping.ClearAndEditBox(datamanager.GetFormattedTime(_ManageCustomersData.CutOffTimeToSubmitOrderForShipping, "HH:mm")));
                Assert.IsTrue(_ManageCustomersPage.CutOffTimeToSubmitOrderForShipping.Edit(Keys.Tab));
                Thread.Sleep(Constants.Wait_Short);
                Assert.IsTrue(_ManageCustomersPage.StandardLeadTime.ClearAndEdit(_ManageCustomersData.StandardLeadTime));
                Assert.IsTrue(_ManageCustomersPage.StandardLeadTime.Edit(Keys.Tab));
                Thread.Sleep(Constants.Wait_Short);
                Assert.IsTrue(_ManageCustomersPage.LeadTimeAfterCutOff.ClearAndEdit(_ManageCustomersData.LeadTimeAfterCutOff));
                Assert.IsTrue(_ManageCustomersPage.LeadTimeAfterCutOff.Edit(Keys.Tab));
                Thread.Sleep(Constants.Wait_Short);
                Assert.IsTrue(_ManageCustomersPage.StandardLeadTimeLabel.StatusCheckORPerFormAction(_ManageCustomersData.StandardLeadTimeLabel));
                return "ManageCustomersCutOffTimeStatusCheckAndUpdateSuccess";
            }
            catch
            {
                return "ManageCustomersCutOffTimeStatusCheckAndUpdateFail";
            }
        }

        public string CommodityQuantityValidationStatusCheckAndUpdated()
        {
            try
            {
                Assert.IsTrue(_ManageCustomersPage.CustomerSummaryDetailsRegion.WaitUntilDisplayed());
                Assert.IsTrue(_ManageCustomersPage.AppTitle.GetText().Trim().Equals("Manage Customers"));
                Assert.IsTrue(_ManageCustomersPage.ActualPiecesVary.StatusCheckORPerFormAction(_ManageCustomersData.ActualPiecesVaryCheckBox));
                Assert.IsTrue(_ManageCustomersPage.ActualPiecesVaryText.ClearAndEdit(_ManageCustomersData.ActualPiecesVaryPercent));
                if(_ManageCustomersData.ActualPiecesVaryCheckBox.ToUpper() == "!CHECK!")
                {
                    Assert.IsTrue(_ManageCustomersPage.ActualPiecesVaryText.Edit(Keys.Tab));
                }
                Assert.IsTrue(_ManageCustomersPage.ActualWeightVary.StatusCheckORPerFormAction(_ManageCustomersData.ActualWeightVaryCheckBox));
                Assert.IsTrue(_ManageCustomersPage.ActualWeightVaryText.ClearAndEdit(_ManageCustomersData.ActualWeightVaryPercent));
                if (_ManageCustomersData.ActualWeightVaryCheckBox.ToUpper() == "!CHECK!")
                {
                    Assert.IsTrue(_ManageCustomersPage.ActualWeightVaryText.Edit(Keys.Tab));
                }
                Assert.IsTrue(_ManageCustomersPage.StopUserFromSaving.StatusCheckORPerFormAction(_ManageCustomersData.StopUserFromSaving));
                Thread.Sleep(Constants.Wait_Medium);
                return "CommodityQuantityValidationStatusCheckAndUpdatedSuccess";
            }
            catch
            {
                return "CommodityQuantityValidationStatusCheckAndUpdatedFails";
            }
        }
    }
}
