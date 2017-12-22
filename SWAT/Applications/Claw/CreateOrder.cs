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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace SWAT.Applications.Claw
{
    using SWAT.Applications.Claw.DAL;
    using SWAT.Data;
    using SWAT.Logger;
    using SWAT.FrameWork.Utilities;
    using SWAT.Applications.Claw.ObjectRepository;
    using SWAT.Configuration;


    public class CreateOrder
    {
        private CreateOrderPage _CreateOrderPage;
        private CreateOrderData _CreateOrderData;
        private ShippingUnitData _ShippingUintData;
        private CommodityData _CommodityData;
        private DataManager _datamanager;
        public CreateOrder(TestStartInfo teststartinfo, DataManager datamanager)
        {
            _CreateOrderPage = new CreateOrderPage(teststartinfo);
            _CreateOrderData = new CreateOrderData(datamanager);
            _ShippingUintData = new ShippingUnitData(datamanager);
            _CommodityData = new CommodityData(datamanager);
            _datamanager = datamanager;
        }

        #region Navigation and other page
        public string Navigate()
        {           
            try
            {
                Assert.IsTrue(_CreateOrderPage.Navigate());
                if (_CreateOrderData.CustomerName == "!IGNORE!")
                {                    
                    Assert.IsTrue(_CreateOrderPage.ContactName.WaitUntilDisplayed());
                }
                else
                {
                    Assert.IsTrue(NavigationForAdmin());
                }
                return "NavigationSuccess";
            }
            catch
            {
                return "NavigationFailed";
            }
        }
        private bool NavigationForAdmin()
        {
            try
            {
                _CreateOrderPage.Customer.WaitUntilDisplayed();
                Assert.IsTrue(_CreateOrderPage.Customer.WaitUntilDisplayed());
                Assert.IsTrue(_CreateOrderPage.Customer.SelectByText(_CreateOrderData.CustomerName));
                Assert.IsTrue(_CreateOrderPage.GoButton.Click());
                Assert.IsTrue(_CreateOrderPage.GoButton.WaitUtilDisappear());
                Assert.IsTrue(_CreateOrderPage.ContactName.WaitUntilDisplayed());
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string Cancel()
        {
            try
            {
                Assert.IsTrue(_CreateOrderPage.Cancel.Click());
                Assert.IsTrue(_CreateOrderPage.Cancel.WaitUtilDisappear());
                return "OrderCancelled";
            }
            catch
            {
                return "OrderCancelFailed";
            }
        }
        public string ShipOrder()
        {
            try
            {
                Assert.IsTrue(_CreateOrderPage.ShipOrder.Click());
                Assert.IsTrue(_CreateOrderPage.GoToMyOrders.WaitUntilDisplayed());
                Assert.IsTrue(_CreateOrderPage.GoToMyLoads.WaitUntilDisplayed());
                Assert.IsTrue(_CreateOrderPage.LoadId.WaitUntilDisplayed());
                return "OrderPlaced";
            }
            catch
            {
                return "OrderNotPlaced";
            }
        }
        public string SaveOrder()
        {
            try
            {
                Assert.IsTrue(_CreateOrderPage.SaveOrder.Click());
                _CreateOrderPage.OrderSaved.WaitUntilDisplayed();
                Assert.IsTrue(_CreateOrderPage.OrderSaved.WaitUntilDisplayed(30));
                return "OrderSaved";
            }
            catch
            {
                return "OrderNotSaved";
            }
        }
        #endregion
        
       
        #region CreateOrderFill
        public string CreateOrderFill()
        {
            try
            {
                Assert.IsTrue(ContactInfo_Fill());
                Assert.IsTrue(RouteDetails_Fill());
                Assert.IsTrue(DateAndTime_Fill());
                Assert.IsTrue(ShipperDetails_Fill());
                return "CreateOrderFillSuccess";
            }
            catch
            {
                return "CreateOrderFillFailed";
            }
        }

        private bool ContactInfo_Fill()
        {
            try
            {
                Assert.IsTrue(_CreateOrderPage.ContactName.ClearAndEdit(_CreateOrderData.ContactName));
                Assert.IsTrue(_CreateOrderPage.ContactPhoneCountryCode.ClearAndEdit(_CreateOrderData.ContactPhnCountryCode));
                Assert.IsTrue(_CreateOrderPage.ContactPhoneNumber.ClearAndEdit(_CreateOrderData.ContactPhnNumber));
                Assert.IsTrue(_CreateOrderPage.ContactExtenstion.ClearAndEdit(_CreateOrderData.ContactExtenstion));
                Assert.IsTrue(_CreateOrderPage.ContactEmail.ClearAndEdit(_CreateOrderData.ContactEmail));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool RouteDetails_Fill()
        {
            try
            {
                Assert.IsTrue(_CreateOrderPage.OrginFacility.TypeAndSelect(_CreateOrderData.OrginFacility));
                Assert.IsTrue(_CreateOrderPage.DestinationFacility.TypeAndSelect(_CreateOrderData.DestinationFacility));
                if(_CreateOrderData.Direction.ToUpper() == "INBOUND")  
                    Assert.IsTrue(_CreateOrderPage.DirectionInbound.Click());
                if (_CreateOrderData.Direction.ToUpper() == "OUTBOUND") 
                    Assert.IsTrue(_CreateOrderPage.DirectionOutbound.Click());
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool DateAndTime_Fill()
        {
            try
            {
            Assert.IsTrue(_CreateOrderPage.PickUpDate.ClearAndEdit(_CreateOrderData.PickUpDate));
            _CreateOrderPage.CraeteOrderPage.Click();
            Assert.IsTrue(_CreateOrderPage.ReadyTime.ClearAndEdit(_CreateOrderData.ReadyTime));
            return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ShipperDetails_Fill()
        {
            try
            {
            Assert.IsTrue(_CreateOrderPage.ReferenceNumberPick.SelectByText(_CreateOrderData.ReferenceNumberPick));            
            Assert.IsTrue(_CreateOrderPage.ReferenceNumber.Edit(_CreateOrderData.ReferenceNumber));
            if (_CreateOrderData.Mode == "TL") 
                Assert.IsTrue(_CreateOrderPage.ModeLTL.Click());
            if(_CreateOrderData.Mode == "LTL") 
                Assert.IsTrue(_CreateOrderPage.ModeTL.Click());
            Assert.IsTrue(_CreateOrderPage.Equipment.SelectByText(_CreateOrderData.Equipment));
            Assert.IsTrue(_CreateOrderPage.MiniLength.SelectByText(_CreateOrderData.MiniLength));
            Assert.IsTrue(_CreateOrderPage.SpecialInstructions.ClearAndEdit(_CreateOrderData.SpecialInstructions));
            return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion CreateOrderFill

        
        #region shipping unit

        public string ShippingUnit_AddAndFill()
        {
            string actualresult = AddAnotherShippingUnit();
            if (actualresult == "ShippingUintAdded")
            {
                actualresult = ShippingUnit_Fill();
                if(actualresult=="ShippingUintFillSuccess")
                {
                    return "ShippingUintAdded";
                }
            }
            return actualresult;
        }

        public string AddAnotherShippingUnit()
        {
            try
            {
                int expectednoofshippingunit = _CreateOrderPage.ShippingUnits.Count() + 1;
                Assert.IsTrue(_CreateOrderPage.AddAnotherShippingUnit.FindAndClickUsingJS());
                Assert.IsTrue(_CreateOrderPage.ShippingUnits.Count == expectednoofshippingunit);
                return "ShippingUintAdded";
            }
            catch
            {
                return "ShippingUintAddFailled";
            }

        }

        public string ShippingUnit_Fill()
        {
            try
            {
                ShippingUnit lastshippingunit = _CreateOrderPage.ShippingUnits.Last();
                Assert.IsTrue(ShippingUnit_Edit(lastshippingunit));
                return "ShippingUintFillSuccess";
            }
            catch
            {
                return "ShippingUintFillFailed";
            }
        }

        private bool ShippingUnit_Edit(ShippingUnit shippingunit)
        {
            try
            {
                Assert.IsTrue(shippingunit.LoadOn.SelectByText(_ShippingUintData.LoadOn));
                Assert.IsTrue(shippingunit.UnitQty.ClearAndEdit(_ShippingUintData.UnitQty));

                if (_ShippingUintData.OverDimension == "YES")
                    Assert.IsTrue(shippingunit.OverDimensionYes.Click());
                if (_ShippingUintData.OverDimension == "NO")
                    Assert.IsTrue(shippingunit.OverDimensionNo.Click());

                Assert.IsTrue(shippingunit.UnitDimensionsLength.ClearAndEdit(_ShippingUintData.Length));
                Assert.IsTrue(shippingunit.UnitDimensionWidth.ClearAndEdit(_ShippingUintData.Width));
                Assert.IsTrue(shippingunit.UnitDimensionHeight.ClearAndEdit(_ShippingUintData.Height));

                if (_ShippingUintData.Stackable == "YES")
                    Assert.IsTrue(shippingunit.StackableYes.Click());
                if (_ShippingUintData.Stackable == "NO")
                    Assert.IsTrue(shippingunit.StackableNo.Click());

                if (_ShippingUintData.Weight == "!IGNORE!")
                {
                    Assert.IsTrue(shippingunit.Weight_UseCommoditySumYes.Click());
                    Assert.IsTrue(shippingunit.Weight_UseCommoditySum.IsDisplayed());
                }
                else
                {
                    Assert.IsTrue(shippingunit.Weight_EnterWeightYes.Click());
                    Assert.IsTrue(shippingunit.Weight_EnterWeight.ClearAndEdit(_ShippingUintData.Weight));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion


        #region commodity
        public string AddAnotherCommodity()
        {
            try
            {
                ShippingUnit lastshippingunit = _CreateOrderPage.ShippingUnits.Last();
                int expectednoofcommodities = lastshippingunit.Commodities.Count() + 1;
                Assert.IsTrue(lastshippingunit.Commodities_AddAnotherCommodity.Click());
                Assert.IsTrue(lastshippingunit.Commodities.Count() == expectednoofcommodities);
                return "CommodoityAdded";
            }
            catch
            {
                return "CommodoityAddedFailed";
            }
        }
        
        public string CommodityDetails_Fill()
        {
            try
            {
                
                List<ShippingUnit> ShippingUnits = _CreateOrderPage.ShippingUnits;
                ShippingUnit lastshippingunit = ShippingUnits.Last();
                List<Commodity> Commodities = lastshippingunit.Commodities;
                Commodity Commodity = Commodities.Last();
                Assert.IsTrue(CommodityDetails_Edit(Commodity));
                return "CommodoityFillSuccess";
            }
            catch
            {
                return "CommodoityFillFailed";
            }
        }

        private bool CommodityDetails_Edit(Commodity commodity)
        {
            try
            {                
                Assert.IsTrue(commodity.Description.ClearAndEdit(_CommodityData.Description));
                Assert.IsTrue(commodity.Item.ClearAndEdit(_CommodityData.Item));
                Assert.IsTrue(commodity.PONumber.ClearAndEdit(_CommodityData.PONumber));
                Assert.IsTrue(commodity.LineNumber.ClearAndEdit(_CommodityData.LineNumber));
                Assert.IsTrue(commodity.SchedLineNumber.ClearAndEdit(_CommodityData.SchedLineNumber));
                Assert.IsTrue(commodity.Weight.ClearAndEdit(_CommodityData.Weight));
                Assert.IsTrue(commodity.Qty.ClearAndEdit(_CommodityData.Qty));
                Assert.IsTrue(commodity.Value.ClearAndEdit(_CommodityData.Value));
                Assert.IsTrue(commodity.Packaging.SelectByText(_CommodityData.Packaging));
                Assert.IsTrue(commodity.Hazmat.SelectByText(_CommodityData.Hazmat));
                Assert.IsTrue(commodity.UnitNumber.ClearAndEdit(_CommodityData.UnitNmuber));
                System.Threading.Thread.Sleep(10);
                Assert.IsTrue(commodity.DueDate.ClearAndEdit(_CommodityData.DueDate));
                Assert.IsTrue(commodity.Option.Click());
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        public string CommoditySelectAll()
        {
            try
            {
                ShippingUnit lastshippingunit = _CreateOrderPage.ShippingUnits.Last();
                Assert.IsTrue(lastshippingunit.CommodityGearButton.Click());
                Assert.IsTrue(lastshippingunit.CommoditySelectAll.Click());
                List<Commodity> commodities = lastshippingunit.Commodities;
                foreach (Commodity comodity in commodities)
                {
                    Assert.IsTrue(comodity.Option.IsSelected());
                }

                return "CommoditySelectAllSuccess";
            }
            catch
            {
                return "CommoditySelectAllFailed";
            }
        }

        public string CommoditySelectNone()
        {
            try
            {
                ShippingUnit lastshippingunit = _CreateOrderPage.ShippingUnits.Last();
                Assert.IsTrue(lastshippingunit.CommodityGearButton.Click());
                Assert.IsTrue(lastshippingunit.CommoditySelectNone.Click());
                List<Commodity> commodities = lastshippingunit.Commodities;
                foreach (Commodity comodity in commodities)
                {
                    Assert.IsFalse(comodity.Option.IsSelected());
                }
                return "CommoditySelectNoneSuccess";
            }
            catch
            {
                return "CommoditySelectNoneFailed";
            }
        }

        public string CommodityMove()
        {
            try
            {
                int expectednoofshippingunit = _CreateOrderPage.ShippingUnits.Count();
                if (_CreateOrderPage.ShippingUnits.Count() > 1)
                {
                    int count = 0;
                    ShippingUnit lastshippingunit = _CreateOrderPage.ShippingUnits.Last();
                    List<Commodity> commodities = lastshippingunit.Commodities;
                    count = GetNumberOfSelectedComodities(commodities);
                    if (count > 0)
                    {
                        Assert.IsTrue(lastshippingunit.CommodityGearButton.Click());
                        Assert.IsTrue(lastshippingunit.CommodityMove.Click());
                        Assert.IsTrue(_CreateOrderPage.ShippingUnits.First().MoveCommodityButton.Click());
                        var elements = commodities[0].Option.FindElements();
                        if (elements != null)
                        {
                            Assert.IsTrue(elements.Count < commodities.Count);
                        }
                    }

                }
                
                return "CommodityMoveSuccess";
            }
            catch
            {
                return "CommodityMoveFailed";
            }
        }

        public string CommodityDelete()
        {
            try
            {
                int count = 0;
                ShippingUnit lastshippingunit = _CreateOrderPage.ShippingUnits.Last();
                List<Commodity> commodities = lastshippingunit.Commodities;
                count = GetNumberOfSelectedComodities(commodities);
                Assert.IsTrue(lastshippingunit.CommodityGearButton.Click());
                Assert.IsTrue(lastshippingunit.CommodityDelete.Click());
                if (count > 0)
                {
                    var elements = commodities[0].Option.FindElements();
                    if(elements != null)
                    {
                        Assert.IsTrue(elements.Count < commodities.Count);
                    }
                }
                return "CommodityDeleteSuccess";
            }
            catch
            {
                return "CommodityDeleteFailed";
            }
        }

        private int GetNumberOfSelectedComodities(List<Commodity> commodities)
        {
            int count = 0;
            foreach (Commodity comodity in commodities)
            {
                if (comodity.Option.IsSelected())
                {
                    count++;
                }
            }
            return count;
        }

        public string GetLoadId() {
            try
            {
                Assert.IsTrue(_CreateOrderPage.LoadId.WaitUntilDisplayed());
                string loadId = _CreateOrderPage.LoadId.GetText();
                _CreateOrderData.LoadId = loadId;
                return "DataCopied";
            }
            catch
            {
                return "DataCopyFailed";
            }
        }

        public string VerifyDefaultValues()
        {
            try
            {
                Assert.IsTrue(_CreateOrderPage.ContactName.WaitUntilDisplayed());
                Assert.IsTrue(_CreateOrderPage.ContactName.GetText().Trim().Equals(_CreateOrderData.ContactName));
                Assert.IsTrue(_CreateOrderPage.ContactPhoneCountryCode.GetText().Trim().Equals(_CreateOrderData.ContactPhnCountryCode));
                Assert.IsTrue(_CreateOrderPage.ContactPhoneNumber.GetText().Trim().Equals(_CreateOrderData.ContactPhnNumber));
                Assert.IsTrue(_CreateOrderPage.ContactExtenstion.GetText().Trim().Equals(_CreateOrderData.ContactExtenstion));
                Assert.IsTrue(_CreateOrderPage.ContactEmail.GetText().Trim().Equals(_CreateOrderData.ContactEmail));
                ShippingUnit shippingunit = _CreateOrderPage.ShippingUnits.Last();
                Assert.IsTrue(shippingunit.UnitQty.GetText().Trim().Equals(_ShippingUintData.UnitQty));
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
            
        }

        public string VerifyError()
        {
            try
            {
                Assert.IsTrue(_CreateOrderPage.ErrorMessage.GetText().Trim().Equals(_CreateOrderData.ErrorMessage));
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }

        }

        public string VerifyFacilityNameAndAddress()
        {
            try
            {
                if (_CreateOrderData.OrginFacility.ToUpper().Trim() != "!IGNORE!")
                {
                    Assert.IsTrue(VerifyOriginFacility());
                }
                if (_CreateOrderData.DestinationFacility.ToUpper().Trim() != "!IGNORE!")
                {
                    Assert.IsTrue(VerifyDestinationFacility());
                }
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
        }

        private bool VerifyOriginFacility()
        {
            try
            {
                string completeAddress = _CreateOrderData.OrginFacility + ", " + _CreateOrderData.OrginFacilityAddress + ", " +
                                        _CreateOrderData.OriginCityAndState + ", " + _CreateOrderData.OrginZip;
                Assert.IsTrue(_CreateOrderPage.OrginFacility.GetValue().Equals(_CreateOrderData.OrginFacility));
                Assert.IsTrue(_CreateOrderPage.OrginFacility.GetAttribute("title").Equals(completeAddress));
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool VerifyDestinationFacility()
        {
            try
            {
                string completeAddress = _CreateOrderData.DestinationFacility + ", " + _CreateOrderData.DestinationFacilityAddress + ", " +
                                        _CreateOrderData.DestinationCityAndState + ", " + _CreateOrderData.DestinationZip;
                Assert.IsTrue(_CreateOrderPage.DestinationFacility.GetValue().Equals(_CreateOrderData.DestinationFacility));
                Assert.IsTrue(_CreateOrderPage.DestinationFacility.GetAttribute("title").Equals(completeAddress));
            }
            catch
            {
                return false;
            }
            return true;
        }

        public string UIVerify()
        {
            try
            {
                UIVerifyData verifyData = new UIVerifyData(_datamanager);
                if (verifyData.VerificationSection.ToUpper() == "SHIPPINGUNIT")
                {
                    Assert.IsTrue(VerifyShippingUnit(verifyData));
                }
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFails";
            }
        }

        private bool VerifyShippingUnit(UIVerifyData verificationData)
        {
            try
            {
                if (verificationData.VerificationElement.ToUpper() == "UNITQTY" && verificationData.VerifyFor.ToUpper() == "ERROR")
                {
                    ShippingUnit shippingUnit = _CreateOrderPage.ShippingUnits.First();
                    Assert.IsTrue(shippingUnit.UnitQty.GetValue().Equals("1"));
                    Assert.IsTrue(shippingUnit.UnitQty.Clear());
                    Assert.IsTrue(shippingUnit.UnitQty.Edit(Keys.Tab));
                    Assert.IsTrue(_CreateOrderPage.SaveOrder.Click());
                    Assert.IsTrue(shippingUnit.UnitQty.HasClass("error"));
                    Assert.IsTrue(shippingUnit.UnitQty.ClearAndEdit("0"));
                    Assert.IsTrue(shippingUnit.UnitQty.Edit(Keys.Tab));
                    Assert.IsTrue(_CreateOrderPage.SaveOrder.Click()); 
                    Assert.IsTrue(shippingUnit.UnitQty.HasClass("error"));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
