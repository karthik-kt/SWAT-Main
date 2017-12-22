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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SWAT.Applications.Claw
{
    using SWAT.Applications.Claw.DAL;
    using SWAT.Data;
    using SWAT.Logger;
    using SWAT.FrameWork.Utilities;
    using SWAT.Applications.Claw.ObjectRepository;
    using SWAT.Configuration;
    using System;
    using System.Collections.Generic;
    using OpenQA.Selenium;
    using System.Threading;
    using System.Linq;
    using System.Globalization;

    class MyOrder
    {
        private MyOrderPage _MyOrderPage;
        private MyOrderData _MyOrderData;
        private PurchaseOrderShippingUnitData _PurchaseOrderShippingUnitData;
        private PurchaseOrderCommodityData _PurchaseOrderCommodityData;

        public MyOrder(TestStartInfo teststartinfo, DataManager datamanager)
        {
            _MyOrderPage = new MyOrderPage(teststartinfo);
            _MyOrderData = new MyOrderData(datamanager);
            _PurchaseOrderShippingUnitData = new PurchaseOrderShippingUnitData(datamanager);
            _PurchaseOrderCommodityData = new PurchaseOrderCommodityData(datamanager);
        }

        public string Navigate()
        {
            try
            {
                Assert.IsTrue(_MyOrderPage.Navigate());
                _MyOrderPage.WaitUntilLoading();
                Assert.IsTrue(_MyOrderPage.OrderRefSearch.WaitUtilEnabled());
                return "NavigationSuccess";
            }
            catch
            {
                return "NavigationFailed";
            }
        }

        public string OpenOrderToShip()
        {
            string ActualResult = "";
            if (_MyOrderData.ShippingType.ToUpper() == "MULTI")
            {
                ActualResult = OpenMultiCommoditiesOrderToShip();
            }
            if (_MyOrderData.ShippingType.ToUpper() == "ORPHAN")
            {
                ActualResult = OpenOrphanedOrderToShip();
            }
            if (_MyOrderData.ShippingType.ToUpper() == "SINGLE")
            {
                ActualResult = OpenSingleOrderToShip();
            }
            return ActualResult;
        }

        private string OpenMultiCommoditiesOrderToShip()
        {
            try
            {
                Assert.IsTrue(_MyOrderPage.FirstOrderHeader.WaitUntilDisplayed());
                List<Order> orders = _MyOrderPage.Orders;
                foreach (Order order in orders)
                {
                    if (order.OrderCommodities.Count > 1 && order.OrderCommodities.First().Shipper.GetText(0) == "--")
                    {
                        Assert.IsTrue(order.PrepareToShip.Click());
                        Assert.IsTrue(_MyOrderPage.OrderContactName.WaitUntilDisplayed());
                        return "OrderOpenSuccess";
                    }
                }
                return "OrderOpenFail";
            }
            catch (Exception)
            {
                return "OrderOpenFail";
            }
        }

        private string OpenOrphanedOrderToShip()
        {
            try
            {
                Assert.IsTrue(_MyOrderPage.FirstOrderHeader.WaitUntilDisplayed());
                Order order = _MyOrderPage.Orders.Last();
                if (order.OrderCommodities.First().Shipper.GetText(0) == "--")
                {
                    Assert.IsTrue(order.PrepareToShip.Click());
                    Assert.IsTrue(_MyOrderPage.OrderContactName.WaitUntilDisplayed());
                    return "OrderOpenSuccess";
                }
                return "OrderOpenFail";
            }
            catch (Exception)
            {
                return "OrderOpenFail";
            }

        }

        private string OpenSingleOrderToShip()
        {
            try
            {
                Assert.IsTrue(_MyOrderPage.FirstOrderHeader.WaitUntilDisplayed());
                Order order = _MyOrderPage.Orders.First();
                Assert.IsTrue(order.PrepareToShip.Click());
                Assert.IsTrue(_MyOrderPage.OrderContactName.WaitUntilDisplayed());
                return "OrderOpenSuccess";
            }
            catch
            {
                return "OrderOpenFail";
            }
        }

        public string FillShipperDetails()
        {
            try
            {
                Assert.IsTrue(_MyOrderPage.ReferenceType.WaitUntilDisplayed());
                Assert.IsTrue(_MyOrderPage.ReferenceType.SelectByText(_MyOrderData.ReferenceType));
                Assert.IsTrue(_MyOrderPage.ReferenceNumber.ClearAndEdit(_MyOrderData.ReferenceNumber));
                if (_MyOrderData.Mode == "TL")
                    Assert.IsTrue(_MyOrderPage.LtlMode.Click());
                if (_MyOrderData.Mode == "LTL")
                    Assert.IsTrue(_MyOrderPage.TlMode.Click());
                Assert.IsTrue(_MyOrderPage.EquipmentType.SelectByText(_MyOrderData.EquipmentType));
                Assert.IsTrue(_MyOrderPage.EquipmentLength.SelectByText(_MyOrderData.EquipmentMiniLength));
                Assert.IsTrue(_MyOrderPage.SpecialInstructions.ClearAndEdit(_MyOrderData.SpecialInstructions));
                return "FillShipperDetailsSuccess";
            }
            catch
            {
                return "FillShipperDetailsFail";
            }
        }

        public string FillShippingsAndCommoditiesDetails()
        {
            try
            {
                Assert.IsTrue(_MyOrderPage.ShippingUnitContainer.WaitUntilDisplayed());
                List<PurchaseOrderShippingUnit> shippingUnits = _MyOrderPage.ShippingUnits;
                bool fielEditedWithLessValue = false;
                foreach (PurchaseOrderShippingUnit shippingUnit in shippingUnits)
                {
                    Assert.IsTrue(ShippingUnit_Edit(shippingUnit));
                    List<PurchaseOrderCommodity> commodities = shippingUnit.Commodities;
                    foreach (PurchaseOrderCommodity commodity in commodities)
                    {
                        if (_PurchaseOrderCommodityData.ThreshHold == "True" && commodity.ExpQty.GetText(0) != "--" && 
                            Convert.ToInt32(commodity.ExpQty.GetText(0)) > 1 && !fielEditedWithLessValue)
                        {
                            Assert.IsTrue(FillCommodity(commodity, true, shippingUnit));
                            Assert.IsFalse(commodity.ExpQty.IsEditable());
                            Assert.IsTrue(SetRefNumber(commodity).Equals("Copied"));
                            Assert.IsTrue(SetOriginalQty(commodity));
                            fielEditedWithLessValue = true;
                        }
                        else
                        {
                            Assert.IsTrue(FillCommodity(commodity, false, shippingUnit));
                            Assert.IsFalse(commodity.ExpQty.IsEditable());
                        }
                    }
                }
                return "FillShippingDetailsSuccess";
            }
            catch
            {
                return "FillShippingDetailsFail";
            }
        }

        public string SetRefNumber(PurchaseOrderCommodity commodity)
        {
            try
            {   if (commodity == null)
                {
                    Order order = _MyOrderPage.Orders.First();
                    OrderCommodity orderCommodity = order.OrderCommodities.First();
                    _PurchaseOrderCommodityData.RefNumber = orderCommodity.Reference.GetText().Trim();
                    return "Copied";
                }
                Assert.IsTrue(commodity.ItemNumber.WaitUntilDisplayed());
                string itemNumber = commodity.ItemNumber.GetValue();
                _PurchaseOrderCommodityData.RefNumber = itemNumber;
                return "Copied";
            }
            catch
            {
                return "Failed";
            }
        }

        private bool SetOriginalQty(PurchaseOrderCommodity commodity)
        {
            try
            {
                Assert.IsTrue(commodity.OrigQty.WaitUntilDisplayed());
                string origQty = commodity.OrigQty.GetText(0);
                _PurchaseOrderCommodityData.OriginalQty = origQty;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool FillCommodity(PurchaseOrderCommodity commodity, bool threshHold, PurchaseOrderShippingUnit shippingUnit)
        {
            try
            {
                string actQty;
                if (_MyOrderData.EntityType.ToUpper() == "FACILITY")
                {
                    if (shippingUnit.OrigQtyHeader.GetText().Trim().Equals("Orig Qty"))
                    {
                        actQty = ((commodity.ExpQtyWhenOriginal.GetText() == "--" || commodity.ExpQtyWhenOriginal.GetText() == null) ? "1" : commodity.ExpQtyWhenOriginal.GetText());
                    }
                    else
                    {
                        actQty = ((commodity.ExpQtyWhenNoOriginal.GetText() == "--" || commodity.ExpQtyWhenNoOriginal.GetText() == null) ? "1" : commodity.ExpQtyWhenNoOriginal.GetText());
                    }
                    Assert.IsTrue(commodity.Weight.ClearAndEdit(_PurchaseOrderCommodityData.Weight));
                    Assert.IsTrue(commodity.ActQty.ClearAndEdit(actQty));
                    Assert.IsTrue(commodity.ActQty.Edit(Keys.Tab));
                }
                else
                {
                    if (threshHold)
                    {
                        actQty = (Convert.ToInt32(commodity.ExpQty.GetText(0)) / 2).ToString();
                        int leftOverQty = (Convert.ToInt32(commodity.ExpQty.GetText(0)) - (Convert.ToInt32(commodity.ExpQty.GetText(0)) / 2));
                        SetLeftOverQty(commodity, leftOverQty);
                        Random random = new Random();
                        int randomNumber = random.Next(100, 700);
                        string refNo = commodity.ItemNumber.GetValue() + "_Test-ref-" + randomNumber;
                        Assert.IsTrue(commodity.ItemNumber.ClearAndEdit(refNo));
                    }
                    else
                    {
                        actQty = (commodity.ExpQty.GetText(0) == "--" ? "10" : commodity.ExpQty.GetText(0));
                    }
                    Assert.IsTrue(commodity.Description.ClearAndEdit(_PurchaseOrderCommodityData.Description));
                    Assert.IsTrue(commodity.ItemNumber.ClearAndEdit(_PurchaseOrderCommodityData.ItemNumber));
                    Assert.IsTrue(commodity.Weight.ClearAndEdit(_PurchaseOrderCommodityData.Weight));
                    Assert.IsTrue(commodity.ActQty.ClearAndEdit(actQty));
                    Assert.IsTrue(commodity.ActQty.Edit(Keys.Tab));
                    Assert.IsTrue(commodity.Value.ClearAndEdit(_PurchaseOrderCommodityData.Value));
                    Assert.IsTrue(commodity.Packaging.SelectByText(_PurchaseOrderCommodityData.Packaging));
                    Assert.IsTrue(commodity.Hazmat.SelectByText(_PurchaseOrderCommodityData.Hazmat));
                    Assert.IsTrue(commodity.UnitNumber.ClearAndEdit(_PurchaseOrderCommodityData.UnitNmuber));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool SetLeftOverQty(PurchaseOrderCommodity commodity, int leftOverQty)
        {
            try
            {
                Assert.IsTrue(commodity.OrigQty.WaitUntilDisplayed());
                _PurchaseOrderCommodityData.LeftOverQty = leftOverQty.ToString();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ShippingUnit_Edit(PurchaseOrderShippingUnit shippingunit)
        {
            try
            {
                Assert.IsTrue(shippingunit.LoadOn.SelectByText(_PurchaseOrderShippingUnitData.LoadOn));
                Assert.IsTrue(shippingunit.UnitQty.ClearAndEdit(_PurchaseOrderShippingUnitData.UnitQty));
                Assert.IsTrue(shippingunit.UnitDimensionsLength.ClearAndEdit(_PurchaseOrderShippingUnitData.Length));
                Assert.IsTrue(shippingunit.UnitDimensionWidth.ClearAndEdit(_PurchaseOrderShippingUnitData.Width));
                Assert.IsTrue(shippingunit.UnitDimensionHeight.ClearAndEdit(_PurchaseOrderShippingUnitData.Height));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string SaveOrder()
        {
            try
            {
                System.Threading.Thread.Sleep(30);
                Assert.IsTrue(_MyOrderPage.SaveOrder.Click());
                if (_PurchaseOrderCommodityData.ThreshHold == "True")
                {
                    Assert.IsTrue(_MyOrderPage.OrderModelSave.WaitUntilDisplayed());
                    Assert.IsTrue(_MyOrderPage.OrderModelSave.Click());
                }
                if (_MyOrderPage.OrderModelSave.IsDisplayed())
                {
                    Assert.IsTrue(_MyOrderPage.OrderModelSave.Click());
                }
                Assert.IsTrue(_MyOrderPage.OrderSaved.WaitUntilDisplayed(30));
                Assert.IsTrue(_MyOrderPage.OrderSaved.GetText(0).Equals("Your order has been saved. You may adjust orders up to 3 days prior to the shipment date, after which point, Coyote will send an automated email. Please contact us with any questions."));
                return "OrderSaved";
            }
            catch
            {
                return "OrderNotSaved";
            }
        }

        public string SearchOrder()
        {
            try
            {
                Assert.IsTrue(_MyOrderPage.OrderRefSearch.WaitUntilDisplayed());
                Assert.IsTrue(_MyOrderPage.OrderRefSearch.WaitUtilEnabled());
                Assert.IsTrue(_MyOrderPage.OrderRefSearch.Clear());
                Assert.IsTrue(_MyOrderPage.OrderRefSearch.Edit(_PurchaseOrderCommodityData.RefNumber));
                Assert.IsTrue(_MyOrderPage.OrderRefSearch.Edit(Keys.Tab));
                MyLogger.Log("Search Value =[ " + _PurchaseOrderCommodityData.RefNumber + " ]");
                Thread.Sleep(Constants.Wait_Short);
                try { _MyOrderPage.OrderRefSearch.Edit(Keys.Enter); }
                catch { _MyOrderPage.SearchButton.Click(); }
                Thread.Sleep(Constants.Wait_Medium);
                //Search result table displayed.
                Assert.IsTrue(_MyOrderPage.FirstOrderHeader.WaitUntilDisplayed());
                return "SearchSuccess";
            }
            catch
            {
                return "SearchFailed";
            }
        }

        public string Verify()
        {
            try
            {
                Assert.IsTrue(_MyOrderPage.FirstOrderHeader.WaitUntilDisplayed());
                List<Order> orders = _MyOrderPage.Orders;
                Order lastorder = orders.Last();
                List<OrderCommodity> orderCommodities = lastorder.OrderCommodities;
                OrderCommodity orderCommodity = orderCommodities.Last();
                Assert.IsTrue(orderCommodity.OrigQty.IsDisplayed());
                Assert.IsTrue(MatchValue(_PurchaseOrderCommodityData.OriginalQty, orderCommodity.OrigQty.GetText(0)));
                Assert.IsTrue(MatchValue(_PurchaseOrderCommodityData.LeftOverQty, orderCommodity.ExpQty.GetText(0)));
                Assert.IsTrue(MatchValue(orderCommodity.ActQty.GetText(0), "--"));
                Assert.IsFalse(orderCommodity.OrigQty.IsEditable());
                Assert.IsTrue(lastorder.ShortShippedIcon.IsDisplayed());
                //Assert.IsTrue(lastorder.ShortShippedIcon.MouseHover());
                //Assert.IsTrue(MatchValue(lastorder.ToolTipsterContent.GetText(0), "Item Short Shipped"));
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFail";
            }
        }

        private bool MatchValue(string firstValue, string secondValue)
        {
            return (firstValue == secondValue);
        }

        public string VerifyOrder()
        {
            try
            {
                Assert.IsTrue(_MyOrderPage.HeaderCustomerName.WaitUntilDisplayed());
                Assert.IsTrue(VerifyLoadReadyDate());
                if (_MyOrderData.CheckForCutOffTimeSetting == "True")
                {
                    Assert.IsTrue(VerifyLoadReadyDateWithManageCustomerSettings());
                }
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFail";
            }

        }

        private bool VerifyLoadReadyDate()
        {
            try
            {
                string readyDate = DataManager.GetSpecificWeekDay(6);
                Assert.IsTrue(_MyOrderPage.OrderDetailReadyDate.ClearAndEdit(readyDate));
                Assert.IsTrue(_MyOrderPage.OrderDetailReadyDate.Edit(Keys.Tab));
                if (_MyOrderData.ExcludeWeekend == "True")
                {
                    Assert.IsTrue(_MyOrderPage.OrderDetailReadyDate.HasClass("error"));
                    return true;
                }
                else
                    if (_MyOrderData.ExcludeWeekend == "False")
                {
                    Assert.IsFalse(_MyOrderPage.OrderDetailReadyDate.HasClass("error"));
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private bool VerifyLoadReadyDateWithManageCustomerSettings()
        {
            try
            {
                int day = (int)DateTime.Now.DayOfWeek;
                string dateToTest = string.Empty;
                bool actualValue = false;
                string monday = DateTime.Today.AddDays(1 - day).ToString();
                string friday = DateTime.Today.AddDays(5 - day).ToString();
                Assert.IsTrue(_MyOrderPage.OrderDetailPickupDate.ClearAndEdit(monday));
                Assert.IsTrue(_MyOrderPage.OrderDetailPickupEndDate.ClearAndEdit(friday));
                Assert.IsTrue(_MyOrderPage.OrderDetailPickupEndDate.Edit(Keys.Tab));
                List<string> allowableDateRange = GetAllowableReadyDateRange();
                if (_MyOrderData.CheckForDate == "Last")
                {
                    dateToTest = allowableDateRange.Last();
                    actualValue = IsValidReadyDate(dateToTest, "Last");
                }
                if (_MyOrderData.CheckForDate == "First")
                {
                    dateToTest = allowableDateRange.First();
                    actualValue = IsValidReadyDate(dateToTest, "First");
                }
                if (_MyOrderData.CheckForDate == "Range")
                {
                    dateToTest = allowableDateRange.First();
                    actualValue = IsValidReadyDate(dateToTest, "First");
                    if (actualValue)
                    {
                        dateToTest = allowableDateRange.Last();
                        actualValue = IsValidReadyDate(dateToTest, "Last");
                    }
                }
                return actualValue;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidReadyDate(string date, string rangeEnd)
        {
            try
            {
                string outOfRangeDate = string.Empty;
                if (rangeEnd == "Last")
                {
                    outOfRangeDate = Convert.ToDateTime(date).AddDays(1).ToString();
                }
                if (rangeEnd == "First")
                {
                    outOfRangeDate = Convert.ToDateTime(date).AddDays(-1).ToString();
                }
                Assert.IsTrue(_MyOrderPage.OrderDetailReadyDate.ClearAndEdit(outOfRangeDate));
                Assert.IsTrue(_MyOrderPage.OrderDetailReadyDate.Edit(Keys.Tab));
                Assert.IsTrue(_MyOrderPage.OrderDetailReadyDate.HasClass("error"));
                Thread.Sleep(Constants.Wait_Short);
                Assert.IsTrue(_MyOrderPage.OrderDetailReadyDate.ClearAndEdit(date));
                Assert.IsTrue(_MyOrderPage.OrderDetailReadyDate.Edit(Keys.Tab));
                Assert.IsFalse(_MyOrderPage.OrderDetailReadyDate.HasClass("error"));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private List<string> GetAllowableReadyDateRange()
        {
            string excludeWeekend = _MyOrderData.ExcludeWeekend,
                   pickupDate = _MyOrderPage.OrderDetailPickupDate.GetValue(),
                   pickupEndDate = _MyOrderPage.OrderDetailPickupEndDate.GetValue();
            string endDate = "";
            string startDate = GetReadyDateStartRange();
            List<string> readyDateRange = new List<string>();

            if (pickupDate != string.Empty && pickupEndDate != string.Empty)
            {
                if (Convert.ToDateTime(startDate) < Convert.ToDateTime(pickupDate))
                {
                    startDate = pickupDate;
                }
                if (Convert.ToDateTime(startDate) >= Convert.ToDateTime(pickupDate) && Convert.ToDateTime(startDate) <= Convert.ToDateTime(pickupEndDate))
                {
                    endDate = pickupEndDate;
                }
                else
                {
                    endDate = AddDaysToMomentDate(startDate, 6, excludeWeekend);
                }
            }
            else
            {
                startDate = pickupDate;
                endDate = pickupEndDate;
            }
            readyDateRange = GetReadyDateRange(startDate, endDate, excludeWeekend);
            return readyDateRange;
        }

        private string GetReadyDateStartRange()
        {
            string cutOffTimeToSubmitOrderForShipping = _MyOrderData.CutOffTimeToSubmitOrderForShipping,
                standardLeadTime = _MyOrderData.StandardLeadTime,
                leadTimeAfterCutOffTime = _MyOrderData.LeadTimeAfterCutOffTime,
                excludeWeekend = _MyOrderData.ExcludeWeekend, leadTimeInDays;
            string startDate = "";
            DateTime today;
            if ((cutOffTimeToSubmitOrderForShipping != "!IGNORE!" || standardLeadTime != "!IGNORE!" || leadTimeAfterCutOffTime != "!IGNORE!"))
            {
                today = DateTime.Now;
                if (today.TimeOfDay < Convert.ToDateTime(cutOffTimeToSubmitOrderForShipping).TimeOfDay)
                {
                    leadTimeInDays = standardLeadTime != "!IGNORE!" ? standardLeadTime : "!IGNORE!";
                }
                else
                {
                    leadTimeInDays = leadTimeAfterCutOffTime != "!IGNORE!" ? leadTimeAfterCutOffTime : "!IGNORE!";
                }
                startDate = AddDaysToMomentDate(today.ToString("MM/dd/yyyy"), Convert.ToInt32(leadTimeInDays), excludeWeekend);
            }
            return startDate;
        }

        private string AddDaysToMomentDate(string startDate, int days, string excludeWeekend)
        {
            DateTime readyDate = Convert.ToDateTime(startDate);
            int day = (int)readyDate.DayOfWeek;
            if (excludeWeekend == "True")
            {
                for (int i = 0; i < days; i++)
                {
                    do
                    {
                        readyDate = readyDate.AddDays(1);
                        day = (int)readyDate.DayOfWeek;
                    }
                    while (day == 0 || day == 6);
                }
            }
            else
            {
                readyDate = readyDate.AddDays(days);
            }
            return readyDate.ToString("MM/dd/yyyy");
        }

        private List<string> GetReadyDateRange(string startDate, string endDate, string excludeWeekend)
        {
            int totalDays = (int)(Convert.ToDateTime(endDate) - Convert.ToDateTime(startDate)).TotalDays;
            List<string> readyDateRange = new List<string>();
            for (int i = 0; i <= totalDays; i++)
            {
                if (excludeWeekend == "True")
                {
                    if (Convert.ToDateTime(startDate).DayOfWeek == 0 || (int)Convert.ToDateTime(startDate).DayOfWeek == 6)
                    {
                        startDate = Convert.ToDateTime(startDate).AddDays(1).ToString("MM/dd/yyyy");
                        continue;
                    }
                    readyDateRange.Add(startDate);
                    startDate = Convert.ToDateTime(startDate).AddDays(1).ToString("MM/dd/yyyy");
                }
                else
                {
                    readyDateRange.Add(startDate);
                    startDate = Convert.ToDateTime(startDate).AddDays(1).ToString("MM/dd/yyyy");
                }
            }
            return readyDateRange;
        }

        public string FillContactDetails()
        {
            try
            {
                Assert.IsTrue(_MyOrderPage.PhoneCountryCode.WaitUntilDisplayed());
                Assert.IsTrue(_MyOrderPage.PhoneCountryCode.ClearAndEdit(_MyOrderData.PhoneCountryCode));
                Assert.IsTrue(_MyOrderPage.PhoneExt.ClearAndEdit(_MyOrderData.PhoneExt));
                Assert.IsTrue(_MyOrderPage.PhoneNumber.ClearAndEdit(_MyOrderData.PhoneNumber));
                return "FillContactDetailsSuccess";
            }
            catch
            {
                return "FillContactDetailsFail";
            }
        }

        public string ConsolidateOrders()
        {
            try
            {
                int totalCommodities = 0;
                Assert.IsTrue(_MyOrderPage.FirstOrderHeader.WaitUntilDisplayed());
                Order order = _MyOrderPage.Orders.First();
                totalCommodities = order.OrderCommodities.Count;
                Assert.IsTrue(order.ConsolidateButton.Click());
                List<Order> orders = _MyOrderPage.Orders.ToList();
                bool firstOrder = false;
                int totalConsolidatedOrder = 1;
                while (totalConsolidatedOrder < Convert.ToInt32(_MyOrderData.TotalLoadToConsolidate))
                {
                    foreach (Order ord in orders)
                    {
                        if (!firstOrder)
                        {
                            firstOrder = true;
                            continue;
                        }
                        if (totalConsolidatedOrder >= Convert.ToInt32(_MyOrderData.TotalLoadToConsolidate))
                        {
                            break;
                        }
                        if (totalConsolidatedOrder < Convert.ToInt32(_MyOrderData.TotalLoadToConsolidate) && !ord.ConsolidateButton.IsAttribtuePresent("disabled"))
                        {
                            Assert.IsTrue(ord.ConsolidateButton.Click());
                            totalCommodities += ord.OrderCommodities.Count;
                            totalConsolidatedOrder++;
                        }
                    }
                    if(totalConsolidatedOrder < Convert.ToInt32(_MyOrderData.TotalLoadToConsolidate))
                    {
                        Assert.IsTrue(_MyOrderPage.NextPage.Click());
                        Thread.Sleep(Constants.Wait_Medium);
                    }
                }
                Assert.IsTrue(_MyOrderPage.ToBeConsolidatedFlyOut.Click());
                Assert.IsTrue(_MyOrderPage.ConsolidationCartList.GetCountOfElements().Equals(Convert.ToInt32(_MyOrderData.TotalLoadToConsolidate)));
                Assert.IsTrue(_MyOrderPage.ConsolidateButton.Click());
                Thread.Sleep(Constants.Wait_Medium);
                Assert.IsTrue(_MyOrderPage.OrderContactName.WaitUntilDisplayed());
                int countOfCommodities = _MyOrderPage.ShippingUnits.First().Commodities.Count;
                Assert.IsTrue(totalCommodities.Equals(countOfCommodities));
                return "OrderConsolidationSuccess";
            }
            catch
            {
                return "OrderConsolidationFail";
            }
        }

        public string AdvanceSearch()
        {
            try
            {
                Assert.IsTrue(_MyOrderPage.OrderRefSearch.WaitUntilDisplayed());
                Assert.IsTrue(_MyOrderPage.OrderRefSearch.WaitUtilEnabled());
                Assert.IsTrue(_MyOrderPage.DuedateStartSearch.ClearAndEdit(_MyOrderData.DueStartDate));
                Assert.IsTrue(_MyOrderPage.DuedateEndSearch.ClearAndEdit(_MyOrderData.DueEndDate));
                Assert.IsTrue(_MyOrderPage.FacilitySearchOrigin.TypeAndSelect(_MyOrderData.SearchOrigin));
                Assert.IsTrue(_MyOrderPage.FacilitySearchDestination.TypeAndSelect(_MyOrderData.SearchDestination));
                Assert.IsTrue(_MyOrderPage.AdvancedSearchButton.Click());
                Assert.IsTrue(_MyOrderPage.ShipmentList.WaitUntilDisplayed());
                return "SearchSuccess";
            }
            catch
            {
                return "SearchFailed";
            }
        }
    }
}
