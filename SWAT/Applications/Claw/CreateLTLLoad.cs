using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SWAT.Data;
using SWAT.Logger;
using SWAT.FrameWork.Utilities;
using SWAT.Applications.Claw.DAL;
using SWAT.Configuration;
using SWAT.Applications.Claw.ObjectRepository;
using System;
using System.Text.RegularExpressions;

namespace SWAT.Applications.Claw
{
    class CreateLTLLoad
    {
        CreateLTLPage _CreateLTLPage;
        LTLCommodity _LTLCommodity;
        CreateLTLLoadData _CreateLTLLoadData;
        LTLQuoteInfo _LTLQuoteInfo;
        QuoteLoadPage _QuoteLoadPage;
        LTLSelectCarrier _LTLSelectCarrier;
        LTLPickupDetails _LTLPickupDetails;
        LTLDeliveryDetails _LTLDeliveryDetails;
        LTLNewPickupLocation _LTLNewPickupLocation;
        LTLNewDeliveryLocation _LTLNewDeliveryLocation;
        LTLConfirmAndSubmit _LTLConfirmAndSubmit;

        public CreateLTLLoad(TestStartInfo teststartinfo, DataManager datamanager)
        {
            _CreateLTLPage = new CreateLTLPage(teststartinfo);
            _LTLCommodity = new LTLCommodity(teststartinfo);
            _CreateLTLLoadData = new CreateLTLLoadData(datamanager);
            _LTLQuoteInfo = new LTLQuoteInfo(teststartinfo);
            _LTLSelectCarrier = new LTLSelectCarrier(teststartinfo);
            _LTLPickupDetails = new LTLPickupDetails(teststartinfo);
            _LTLDeliveryDetails = new LTLDeliveryDetails(teststartinfo);
            _LTLNewPickupLocation = new LTLNewPickupLocation(teststartinfo);
            _LTLNewDeliveryLocation = new LTLNewDeliveryLocation(teststartinfo);
            _LTLConfirmAndSubmit = new LTLConfirmAndSubmit(teststartinfo);
            _QuoteLoadPage = new QuoteLoadPage(teststartinfo);
        }

        public string GetQuote_Submit()
        {
            try
            {
                _LTLQuoteInfo.GetQuotes_Submit.WaitUntilDisplayed();
                Assert.IsTrue(_LTLQuoteInfo.GetQuotes_Submit.Click());
                _CreateLTLPage.WaitUntilLoading();
                Assert.IsTrue(_CreateLTLPage.SelectCarrier.WaitUntilDisplayed());
                return "GetQuotesSuccess";
            }
            catch
            {
                return "GetQuotesFailed";
            }
        }
        public string Navigate()
        {
            try
            {
                if(_CreateLTLPage.StartOver.IsDisplayed())
                    return "Success";
                _CreateLTLPage.Navigate();
                _CreateLTLPage.WaitUntilLoading();
                Assert.IsTrue(_CreateLTLPage.Orgin_ZipCode.WaitUntilDisplayed());
                return "Success";
            }
            catch
            {
                return "Failed";
            }
        }

        public string AddCommodity()
        {
            try
            {
                Assert.IsTrue(Open_AddCommodity());
                Assert.IsTrue(Fill_AddCommodity(false));
                Assert.IsTrue(Submit_AddCommodity(false));
                return "CommodityAdded";
            }
            catch
            {
                return "Failed";
            }
        }
      
        public string DeleteCommodity()
        {
            try
            {
                int itemToDelete;
                do
                {
                    itemToDelete = _LTLCommodity.CommoditiesTableRow.GetOneElementIndex(_CreateLTLLoadData.Commodity_Type + " " + _CreateLTLLoadData.Commodity_PieceCount);
                    Assert.IsTrue(_LTLCommodity.DeleteCommodity.Click(itemToDelete+1));
                }
                while (_LTLCommodity.CommoditiesTableRow.GetOneElementIndex(_CreateLTLLoadData.Commodity_Type + " " + _CreateLTLLoadData.Commodity_PieceCount) != -1);
                return "CommodityDeleted";
            }
            catch
            {
                return "Failed";
            }
        }

        public string EditCommodity()
        {
            try
            {
                int itemToEdit;
                do
                {
                    itemToEdit = _LTLCommodity.CommoditiesTableRow.GetOneElementIndex(_CreateLTLLoadData.Commodity_Type +" "+_CreateLTLLoadData.Commodity_PieceCount);
                    Assert.IsTrue(_LTLCommodity.EditCommodity.Click(itemToEdit+1));
                    _LTLCommodity.CommodityType.WaitUntilDisplayed();
                    Assert.IsTrue(Fill_AddCommodity(true));
                    Assert.IsTrue(Submit_AddCommodity(true));
                }
                while (_LTLCommodity.CommoditiesTableRow.GetOneElementIndex(_CreateLTLLoadData.Commodity_Type + " " + _CreateLTLLoadData.Commodity_PieceCount) != -1);
                return "CommodityEdited";
            }
            catch
            {
                return "Failed";
            }
        }

        public string UIVerify()
        {
            try
            {
                Assert.IsTrue(_LTLCommodity.CommoditiesTableRow.UIVerify(_CreateLTLLoadData.CommoditiesTable_Row));
                if (_CreateLTLLoadData.Entity.ToUpper() == "CREATELTLLOAD_ADDCOMMODITY")
                    Assert.IsTrue(UIVerify_AddCommodity());
                if (_CreateLTLLoadData.Entity.ToUpper() == "CREATELTLLOAD_QUOTEINFORMATION")
                    Assert.IsTrue(UIVerify_QuoteInformation());
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
        }

        public string QuoteInformation()
        {
            try
            {
                Assert.IsTrue(FillQuoteInformation());
                Assert.IsTrue(_LTLQuoteInfo.GetQuotes_Submit.Click());
                return "QuoteSuccess";
            }
            catch
            {
                return "Failed";
            }
        }

        public string SelectCarrier()
        {
            try
            {
                Assert.IsTrue(_LTLSelectCarrier.CarrierQuoteTbl.WaitUntilDisplayed());
                Assert.IsTrue(_CreateLTLPage.WaitUntilLoading());
                _CreateLTLLoadData.CarrierName = _LTLSelectCarrier.CarrierQuoteTbl.GetText();
                Assert.IsTrue(_LTLSelectCarrier.CarrierQuoteTbl.FindAndClickFirstElement());
                return "CarrierSelected";
            }
            catch
            {
                return "Failed";
            }
        }

        public string Pickup()
        {
            try
            {
                if (_CreateLTLLoadData.Pickup_Facility == "Old")
                    Assert.IsTrue(PickUpWithOldFacility());
                else
                    Assert.IsTrue(PickUpWithNewFacility());
                return "PickupSuccess";
            }
            catch
            {
                return "Failed";
            }
        }

        public string Delivery()
        {
            try
            {
                if (_CreateLTLLoadData.Delivery_Facility == "Old")
                    Assert.IsTrue(DeliverWithOldFacility());
                else
                    Assert.IsTrue(DeliverWithNewFacility());
                return "DeliverySuccess";
            }
            catch
            {
                return "Failed";
            }
        }

        public string ConfirmAndSubmit()
        {
            try
            {
                Assert.IsTrue(_LTLConfirmAndSubmit.TermsAndConditions.Click());
                Assert.IsTrue(_LTLConfirmAndSubmit.SubmitLTLLoad.Click());
                Assert.IsTrue(_CreateLTLPage.ErrorOrSuccessMessage.GetText().Equals("Submitting your LTL load. This may take a few minutes to complete."));
                Assert.IsTrue(_CreateLTLPage.WaitUntilSaving());
                Assert.IsTrue(_CreateLTLPage.LoadId.WaitUntilDisplayed());
                return "SubmitSuccess";
            }
            catch
            {
                if (_CreateLTLPage.ErrorOrSuccessMessage.GetText().Equals("Unable to process your request. We apologize for the "+
                    "inconvenience . Please contact your Coyote rep in order to resolve this matter."))
                {
                    return "ErrorWhileCreatingLoad";
                }
                return "Failed";
            }
        }


        public string NavigateToQuoteAndCreateLTL()
        {
            try
            {
                Assert.IsTrue(_QuoteLoadPage.Navigate());
                MyLogger.Log("Navigated to quote and create ltl load successfully");
                Assert.IsTrue(_QuoteLoadPage.Customer.WaitUntilDisplayed(30));
                Assert.IsTrue(_QuoteLoadPage.Customer.IsDisplayed());
                Assert.IsTrue(_QuoteLoadPage.CustomerRequired.IsDisplayed());
                MyLogger.Log("Quote & Create LTL Load successfully opened");
                return "Quote&CreateLTLLoadSuccess";
            }
            catch
            {
                MyLogger.Log("Quote & Create LTL Load failed");
                return "Quote&CreateLTLLoadFail";
            }
        }

        public string SearchOrDeleteCustomer()
        {
            try
            {
                Assert.IsTrue(_QuoteLoadPage.WaitUntilLoading());
                Assert.IsTrue(this.SearchCustomer());
                return "CustomerSearchOrDeleteSuccess";
            }
            catch
            {
                MyLogger.Log("Customer search or delete fail");
                return "CustomerSearchOrDeleteFail";
            }
        }


        public string GetLoadId()
        {
            try
            {
                Assert.IsTrue(_CreateLTLPage.LoadId.WaitUntilDisplayed());
                string loadId = Regex.Match(_CreateLTLPage.LoadId.GetText(), @"\d+").Value;
                _CreateLTLLoadData.LoadID = loadId;
                Assert.IsTrue(_CreateLTLPage.LoadId.Click());
                return "DataCopied";
            }
            catch
            {
                return "Failed";
            }
        }

        private bool UIVerify_QuoteInformation()
        {
            try
            {
                Assert.IsTrue(_LTLQuoteInfo.Orgin_ZipCode.UIVerify(_CreateLTLLoadData.QuoteInfo_Orgin_ZipCode));
                Assert.IsTrue(_LTLQuoteInfo.Orgin_City.UIVerify(_CreateLTLLoadData.QuoteInfo_Orgin_City));
                Assert.IsTrue(_LTLQuoteInfo.Destination_ZipCode.UIVerify(_CreateLTLLoadData.QuoteInfo_Destination_ZipCode));
                Assert.IsTrue(_LTLQuoteInfo.Destination_City.UIVerify(_CreateLTLLoadData.QuoteInfo_Destination_City));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool UIVerify_AddCommodity()
        {
            try
            {
                Assert.IsTrue(Open_AddCommodity());
                Assert.IsTrue(_LTLCommodity.CommodityType.UIVerify(_CreateLTLLoadData.Commodity_Type));
                Assert.IsTrue(_LTLCommodity.PieceCount.UIVerify(_CreateLTLLoadData.Commodity_PieceCount));
                Assert.IsTrue(_LTLCommodity.PackagingType.UIVerify(_CreateLTLLoadData.Commodity_PackagingType));
                Assert.IsTrue(_LTLCommodity.Weight.UIVerify(_CreateLTLLoadData.Commodity_Weight));
                Assert.IsTrue(_LTLCommodity.Dimensions_H.UIVerify(_CreateLTLLoadData.Commodity_Dimensions_H));
                Assert.IsTrue(_LTLCommodity.Dimensions_L.UIVerify(_CreateLTLLoadData.Commodity_Dimensions_L));
                Assert.IsTrue(_LTLCommodity.Dimensions_W.UIVerify(_CreateLTLLoadData.Commodity_Dimensions_W));
                Assert.IsTrue(_LTLCommodity.HazmatClass.UIVerify(_CreateLTLLoadData.Commodity_HazmatClass));

                Assert.IsTrue(_LTLCommodity.HazmatContactPhoneCode.UIVerify(_CreateLTLLoadData.Commodity_HazmatContactPhone));
                Assert.IsTrue(_LTLCommodity.HazmatContactPhone.UIVerify(_CreateLTLLoadData.Commodity_HazmatContactPhone));
                Assert.IsTrue(_LTLCommodity.HazmatContactPhoneExtn.UIVerify(_CreateLTLLoadData.Commodity_HazmatContactPhone));

                Assert.IsTrue(_LTLCommodity.HazmatClass.UIVerify(_CreateLTLLoadData.Commodity_HazmatClass));
                Assert.IsTrue(_LTLCommodity.FreightClass.UIVerify(_CreateLTLLoadData.Commodity_FreightClass));
                Assert.IsTrue(_LTLCommodity.NMFC.UIVerify(_CreateLTLLoadData.Commodity_NMFC));
                Assert.IsTrue(_LTLCommodity.NMFCSub.UIVerify(_CreateLTLLoadData.Commodity_NMFCSub));
                Assert.IsTrue(Cancel_AddCommodity());
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool Open_AddCommodity()
        {
            try
            {
                if (_LTLCommodity.CommodityType.IsDisplayed())
                    return true;
                Navigate();
                _LTLCommodity.AddCommodity.Click();
                _LTLCommodity.WaitUntilLoading();
                Assert.IsTrue(_LTLCommodity.CommodityType.WaitUntilDisplayed());
                Assert.IsTrue(_LTLCommodity.ModalTitle.GetText().Equals(_CreateLTLLoadData.CommodityModalTitle));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool Submit_AddCommodity(bool isEdit)
        {
            try
            {
                int RowsBeforeAdd = _LTLCommodity.CommoditiesTableRow.GetCountOfElements();
                if (!isEdit && _LTLCommodity.CommoditiesTableRow.GetText(1)!="No commodities")
                    RowsBeforeAdd = RowsBeforeAdd + 1;
                _LTLCommodity.Save.Click();
                _LTLCommodity.WaitUntilLoading();
                Assert.IsTrue(_LTLCommodity.CommodityType.WaitUtilDisappear());
                int RowsAfterAdd = _LTLCommodity.CommoditiesTableRow.GetCountOfElements();
                Assert.IsTrue(RowsBeforeAdd == RowsAfterAdd);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool Cancel_AddCommodity()
        {
            try
            {
                if (!_LTLCommodity.CommodityType.IsDisplayed())
                    return true;
                _LTLCommodity.Cancel.Click();
                _LTLCommodity.WaitUntilLoading();
                Assert.IsTrue(_LTLCommodity.CommodityType.WaitUtilDisappear());
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        private bool Fill_AddCommodity(bool isEdit)
        {
            try
            {
                if (isEdit)
                {
                    Assert.IsTrue(_LTLCommodity.CommodityType.ClearAndEdit(_CreateLTLLoadData.Commodity_Type_Edit));
                    Assert.IsTrue(_LTLCommodity.PieceCount.ClearAndEdit(_CreateLTLLoadData.Commodity_PieceCount_Edit));
                }
                else
                {
                    Assert.IsTrue(_LTLCommodity.CommodityType.ClearAndEdit(_CreateLTLLoadData.Commodity_Type));
                    Assert.IsTrue(_LTLCommodity.PieceCount.ClearAndEdit(_CreateLTLLoadData.Commodity_PieceCount));
                }

                Assert.IsTrue(_LTLCommodity.PackagingType.SelectByText(_CreateLTLLoadData.Commodity_PackagingType));
                Assert.IsTrue(_LTLCommodity.Weight.ClearAndEdit(_CreateLTLLoadData.Commodity_Weight));
                Assert.IsTrue(_LTLCommodity.Dimensions_L.ClearAndEdit(_CreateLTLLoadData.Commodity_Dimensions_L));
                Assert.IsTrue(_LTLCommodity.Dimensions_W.ClearAndEdit(_CreateLTLLoadData.Commodity_Dimensions_W));
                Assert.IsTrue(_LTLCommodity.Dimensions_H.ClearAndEdit(_CreateLTLLoadData.Commodity_Dimensions_H));
                Assert.IsTrue(_LTLCommodity.HazmatClass.ClearAndEdit(_CreateLTLLoadData.Commodity_HazmatClass));

                Assert.IsTrue(_LTLCommodity.HazmatContactPhoneCode.ClearAndEdit(_CreateLTLLoadData.Commodity_HazmatContactPhone));
                Assert.IsTrue(_LTLCommodity.HazmatContactPhone.ClearAndEdit(_CreateLTLLoadData.Commodity_HazmatContactPhone));
                Assert.IsTrue(_LTLCommodity.HazmatContactPhoneExtn.ClearAndEdit(_CreateLTLLoadData.Commodity_HazmatContactPhone));

                Assert.IsTrue(_LTLCommodity.HazmatClass.ClearAndEdit(_CreateLTLLoadData.Commodity_HazmatClass));
                Assert.IsTrue(_LTLCommodity.FreightClass.SelectByText(_CreateLTLLoadData.Commodity_FreightClass));
                Assert.IsTrue(_LTLCommodity.NMFC.ClearAndEdit(_CreateLTLLoadData.Commodity_NMFC));
                Assert.IsTrue(_LTLCommodity.NMFCSub.ClearAndEdit(_CreateLTLLoadData.Commodity_NMFCSub));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool SearchCustomer()
        {
            try
            {
                Assert.IsTrue(_QuoteLoadPage.Customer.Clear());
                Assert.IsTrue(_QuoteLoadPage.Customer.TypeAndEnter(_CreateLTLLoadData.Customer_Name));
                MyLogger.Log("Customer ::: " + _CreateLTLLoadData.Customer_Name + " searched");

                if (_CreateLTLLoadData.Customer_Action.Equals("Delete"))
                {
                    Assert.IsTrue(_QuoteLoadPage.CustomerDeleted.WaitUntilDisplayed(20));
                    Assert.IsTrue(_QuoteLoadPage.CustomerDeleted.Click());
                    MyLogger.Log("The selected customer is deleted");
                    return true;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool FillQuoteInformation()
        {
            try
            {
                Assert.IsTrue(_LTLQuoteInfo.Orgin_ZipCode.ClearAndEdit(_CreateLTLLoadData.QuoteInfo_Orgin_ZipCode));
                Assert.IsTrue(_LTLQuoteInfo.Orgin_PickupDate.ClearAndEdit(_CreateLTLLoadData.QuoteInfo_PickupDate));
                Assert.IsTrue(_LTLQuoteInfo.Destination_ZipCode.ClearAndEdit(_CreateLTLLoadData.QuoteInfo_Destination_ZipCode));
                Assert.IsTrue(_LTLQuoteInfo.PalletCount.ClearAndEdit(_CreateLTLLoadData.QuoteInfo_PalletCount));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool FillPickupDetails()
        {
            try
            {
                Assert.IsTrue(_LTLPickupDetails.ContactName.ClearAndEdit(_CreateLTLLoadData.Pickup_ContactName));
                Assert.IsTrue(_LTLPickupDetails.PhoneNumber.ClearAndEdit(_CreateLTLLoadData.Pickup_PhoneNumber));
                Assert.IsTrue(_LTLPickupDetails.Email.ClearAndEdit(_CreateLTLLoadData.Pickup_Email));
                Assert.IsTrue(_LTLPickupDetails.PickupHours_Open.SelectByText(Convert.ToDateTime(_CreateLTLLoadData.Pickup_HoursOpen).ToString("hh:mm tt").ToLower()));
                Assert.IsTrue(_LTLPickupDetails.PickupHours_Close.SelectByText(Convert.ToDateTime(_CreateLTLLoadData.Pickup_HoursClose).ToString("hh:mm tt").ToLower()));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool PickUpWithOldFacility()
        {
            try
            {
                Assert.IsTrue(_LTLPickupDetails.SelectFacility.WaitUntilDisplayed());
                Assert.IsTrue(_LTLPickupDetails.SelectFacility.Click());
                Assert.IsTrue(FillPickupDetails());
                Assert.IsTrue(_LTLPickupDetails.Next.Click());
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool PickUpWithNewFacility()
        {
            try
            {
                Assert.IsTrue(_LTLPickupDetails.PickupLocation.WaitUntilDisplayed());
                Assert.IsTrue(_LTLPickupDetails.PickupLocation.Click());
                Assert.IsTrue(_LTLNewPickupLocation.CompanyName.WaitUntilDisplayed());
                Assert.IsTrue(FillPickupLocationDetails());
                Assert.IsTrue(_LTLNewPickupLocation.Save.Click());
                Assert.IsTrue(_LTLNewPickupLocation.WaitUntilSaving());
                Assert.IsTrue(FillPickupDetails());
                Assert.IsTrue(_LTLPickupDetails.Next.Click());
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool FillPickupLocationDetails()
        {
            try
            {
                Assert.IsTrue(_LTLNewPickupLocation.CompanyName.ClearAndEdit(_CreateLTLLoadData.Pickup_CompanyName));
                Assert.IsTrue(_LTLNewPickupLocation.Address.ClearAndEdit(_CreateLTLLoadData.Pickup_Address));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool DeliverWithOldFacility()
        {
            try
            {
                Assert.IsTrue(_LTLDeliveryDetails.SelectFacility.WaitUntilDisplayed());
                Assert.IsTrue(_LTLDeliveryDetails.SelectFacility.Click());
                Assert.IsTrue(FillDeliveryDetails());
                Assert.IsTrue(_LTLDeliveryDetails.Next.Click());
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool DeliverWithNewFacility()
        {
            try
            {
                Assert.IsTrue(_LTLDeliveryDetails.DeliveryLocation.WaitUntilDisplayed());
                Assert.IsTrue(_LTLDeliveryDetails.DeliveryLocation.Click());
                Assert.IsTrue(_LTLNewDeliveryLocation.CompanyName.WaitUntilDisplayed());
                Assert.IsTrue(FillDeliveryLocationDetails());
                Assert.IsTrue(_LTLNewDeliveryLocation.Save.Click());
                Assert.IsTrue(_LTLNewDeliveryLocation.WaitUntilSaving());
                Assert.IsTrue(FillDeliveryDetails());
                Assert.IsTrue(_LTLDeliveryDetails.Next.Click());
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool FillDeliveryLocationDetails()
        {
            try
            {
                Assert.IsTrue(_LTLNewDeliveryLocation.CompanyName.ClearAndEdit(_CreateLTLLoadData.Delivery_CompanyName));
                Assert.IsTrue(_LTLNewDeliveryLocation.Address.ClearAndEdit(_CreateLTLLoadData.Delivery_Address));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool FillDeliveryDetails()
        {
            try
            {
                Assert.IsTrue(_LTLDeliveryDetails.ContactName.ClearAndEdit(_CreateLTLLoadData.Delivery_ContactName));
                Assert.IsTrue(_LTLDeliveryDetails.PhoneNumber.ClearAndEdit(_CreateLTLLoadData.Delivery_PhoneNumber));
                Assert.IsTrue(_LTLDeliveryDetails.Email.ClearAndEdit(_CreateLTLLoadData.Delivery_Email));
                Assert.IsTrue(_LTLDeliveryDetails.DeliveryHours_Open.SelectByText(Convert.ToDateTime(_CreateLTLLoadData.Delivery_HoursOpen).ToString("hh:mm tt").ToLower()));
                Assert.IsTrue(_LTLDeliveryDetails.DeliveryHours_Close.SelectByText(Convert.ToDateTime(_CreateLTLLoadData.Delivery_HoursClose).ToString("hh:mm tt").ToLower()));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
