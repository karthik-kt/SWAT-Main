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

namespace SWAT.Applications.Claw
{


    internal class CreateLoad : UIObjects
    {
        private CreateLoadData _CreateLoadData;
        private CreateLoadPage _CreateLoadPage;
        private CreateLoad_StopData _StopData;
        private CreateLoad_CommodityData _CommodityData;
        private LoadDetailsPage _LoadDetailPage;
                
        public CreateLoad(TestStartInfo teststartinfo, DataManager datamanager)
            : base(teststartinfo)
        {
            testConfig = teststartinfo;
            driver = testConfig.Driver;
            _CreateLoadPage = new CreateLoadPage(teststartinfo);
            _CreateLoadData = new CreateLoadData(datamanager);
            _StopData = new CreateLoad_StopData(datamanager);
            _CommodityData = new CreateLoad_CommodityData(datamanager);
            _LoadDetailPage = new LoadDetailsPage(teststartinfo);
        }

        #region Shipper details
        private bool OpenCreateLoadPage()
        {
            try
            {
                Assert.IsTrue(_CreateLoadPage.Navigate());
                _CreateLoadPage.WaitUntilLoading();
                if (!_CreateLoadPage.CustomerName.WaitUntilDisplayed(10))
                Assert.IsTrue(_CreateLoadPage.PickUpFacility.WaitUntilDisplayed(60));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ShipperDetails()
        {
            try
            {
                _CreateLoadPage.WaitUntilLoading();
                _CreateLoadPage.PickUpFacility.WaitUntilDisplayed();
                if (_CreateLoadPage.PickUpFacilityRemove.IsDisplayed())
                    _CreateLoadPage.PickUpFacilityRemove.Click();
                Assert.IsTrue(_CreateLoadPage.PickUpFacility.TypeAndSelect(_CreateLoadData.PickupFacility));
                Assert.IsTrue(_CreateLoadPage.PickUpFacility.GetAttribute("title").IndexOf(_CreateLoadData.PickupFacility)!= -1);
                Assert.IsTrue(_CreateLoadPage.ContactName.ClearAndEdit(_CreateLoadData.ContactName));
                Assert.IsTrue(_CreateLoadPage.CityCode.ClearAndEdit(_CreateLoadData.CityCode));
                Assert.IsTrue(_CreateLoadPage.PhoneNumber.ClearAndEdit(_CreateLoadData.PhoneNumber));
                Assert.IsTrue(_CreateLoadPage.Extension.ClearAndEdit(_CreateLoadData.Extension));
                Assert.IsTrue(_CreateLoadPage.ReferenceNumber.ClearAndEdit(_CreateLoadData.ReferenceNumber));
                Assert.IsTrue(_CreateLoadPage.ReferenceNumber.Edit(Keys.Tab));
                Assert.IsTrue(_CreateLoadPage.PickUpNumber.ClearAndEdit(_CreateLoadData.PickupNumber));
                Assert.IsTrue(_CreateLoadPage.Mode.SelectRadioByValue(_CreateLoadData.Mode));
                Assert.IsTrue(_CreateLoadPage.EquipmentType.SelectByText(_CreateLoadData.EquipmentType));
                Assert.IsTrue(_CreateLoadPage.MinimumLength.SelectByText(_CreateLoadData.MinimumLength));
                Assert.IsTrue(_CreateLoadPage.TarpType.SelectByText(_CreateLoadData.TarpType));
                Assert.IsTrue(_CreateLoadPage.TarpQuantity.ClearAndEdit(_CreateLoadData.TarpQuantity));
                Assert.IsTrue(_CreateLoadPage.PickUpDateAvailable.ClearAndEdit(_CreateLoadData.PickUpDate_Available));
                Assert.IsTrue(_CreateLoadPage.PickUpDateBy.ClearAndEdit(_CreateLoadData.PickUpDate_By));
                Assert.IsTrue(_CreateLoadPage.PickUpDateBy.Edit(Keys.Tab));
                Assert.IsTrue(_CreateLoadPage.ApptTimeStart.ClearAndEdit(_CreateLoadData.AppointmentStartTime));
                Assert.IsTrue(_CreateLoadPage.ApptTimeEnd.ClearAndEdit(_CreateLoadData.AppointmentEndTime));
                Assert.IsTrue(_CreateLoadPage.ApptTimeEnd.Edit(Keys.Tab));
                if (_CreateLoadData.RushOrder == "YES")
                {
                    Assert.IsTrue(_CreateLoadPage.IsRushOrder.Click());
                }
                Assert.IsTrue(_CreateLoadPage.Notes.ClearAndEdit(_CreateLoadData.Notes));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string Fill()
        {
            try
            {
                Assert.IsTrue(OpenCreateLoadPage());
                //Few customer have this options.
                Assert.IsTrue(SelectCustomerName());
                Assert.IsTrue (ShipperDetails());
                return "CreateLoadFillSuccess";
            }
            catch
            {
                return "CraeteLoadFillDataFailed";
            }
        }

        public bool SelectCustomerName()
        {
            try
            {
                if(!_CreateLoadPage.CustomerName.WaitUntilDisplayed(10)) return true;
                if (_CreateLoadData.CustomerName == Constants.Ignore) return false;
                Assert.IsTrue(_CreateLoadPage.CustomerName.ClearAndEdit(_CreateLoadData.CustomerName));
                _CreateLoadPage.CustomerNameHighLight.WaitUntilDisplayed();
                _CreateLoadPage.CustomerNameHighLight.Click();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public string GetShipperDetails()
        {
            try
            {
                _CreateLoadData.PickupFacility = _CreateLoadPage.PickUpFacility.GetText();
                _CreateLoadData.ContactName = _CreateLoadPage.ContactName.GetText();
                _CreateLoadData.PhoneNumber = _CreateLoadPage.PhoneNumber.GetText();
                _CreateLoadData.Extension = _CreateLoadPage.Extension.GetText();
                return "ShipperDetailsCopied";
            }
            catch
            {
                return "ShipperDetailsCopiedFailed";
            }
        }
        #endregion

        #region Stops
        public string Stop_Add()
        {
            try
            {
                int expectedstopcount = _CreateLoadPage.Stops.Count()+1;
                Assert.IsTrue(_CreateLoadPage.AddAnotherStop.Click());
                Assert.IsTrue(_CreateLoadPage.Stops.Count() == expectedstopcount);
                return "AnotherStopAdded";
            }
            catch
            {
                return "AddStopFailed";
            }
        }

        public string StopDetails_Fill()
        {
            try
            {
                Stop_CreateLoad laststop = _CreateLoadPage.Stops.Last();
                Assert.IsTrue(_StopDetails_Fill(laststop));
                return "StopDetailsFilled";
            }
            catch
            {
                return "StopDetailsFailed";
            }
        }

        private bool _StopDetails_Fill(Stop_CreateLoad laststop)
        {
            try
            {
                if (laststop.DeliveryFacilityRemoveButton.IsDisplayed())
                    laststop.DeliveryFacilityRemoveButton.Click();
                Assert.IsTrue(laststop.DeliveryFacility.TypeAndSelect(_StopData.DeliveryFacility));
                Assert.IsTrue(laststop.DeliveryFacility.GetAttribute("title").IndexOf(_StopData.DeliveryFacility) != -1);
                Assert.IsTrue(laststop.ContactName.ClearAndEdit(_StopData.ContactName));
                Assert.IsTrue(laststop.CityCode.ClearAndEdit(_StopData.CityCode));
                Assert.IsTrue(laststop.PhoneNumber.ClearAndEdit(_StopData.PhoneNumber));
                Assert.IsTrue(laststop.Extension.ClearAndEdit(_StopData.Extension));

                Assert.IsTrue(laststop.DeliveryNumber.ClearAndEdit(_StopData.DeliveryNumber));
                Assert.IsTrue(laststop.DeliveryAvailable.ClearAndEdit(_StopData.DeliveryAvailable));
                Assert.IsTrue(laststop.DeliveryBy.ClearAndEdit(_StopData.DeliveryBy));
                Assert.IsTrue(laststop.DeliveryBy.Edit(Keys.Tab));
                Assert.IsTrue(laststop.AppointmentStartTime.ClearAndEdit(_StopData.AppointmentStartTime));
                Assert.IsTrue(laststop.AppointmentEndTime.ClearAndEdit(_StopData.AppointmentEndTime));
                Assert.IsTrue(laststop.AppointmentEndTime.Edit(Keys.Tab));
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Commodities 
        public string Commodity_Add()
        {
            try
            {
                Stop_CreateLoad laststop = _CreateLoadPage.Stops.Last();
                int expectedcommoditycount = laststop.Commodity.Count() + 1;
                Assert.IsTrue(laststop.AddAnotherCommodity.Click());
                Assert.IsTrue(laststop.Commodity.Count() == expectedcommoditycount);
                return "CommodityAdded";
            }
            catch
            {
                return "CommodityAddFailed";
            }
        }

        public string CommodityDetails_Fill()
        {
            try
            {
                Commodity_CreateLoad lastcommodity = _CreateLoadPage.Stops.Last().Commodity.Last();
                Assert.IsTrue(_CommodityDetails_Fill(lastcommodity));
                return "CommodityDetailsFilled";
            }
            catch
            {
                return "CommodityDetailsFailed";
            }
        }
        
        private bool _CommodityDetails_Fill(Commodity_CreateLoad lastcommodity)
        {
            try
            {
                Assert.IsTrue(lastcommodity.Description.ClearAndEdit(_CommodityData.Description));
                Assert.IsTrue(lastcommodity.Weight.ClearAndEdit(_CommodityData.Weight));
                Assert.IsTrue(lastcommodity.Quantity.ClearAndEdit(_CommodityData.Quantity));
                Assert.IsTrue(lastcommodity.PackagingTypes.SelectByText(_CommodityData.PackagingType));
                Assert.IsTrue(lastcommodity.Pallets.ClearAndEdit(_CommodityData.Pallets));
                Assert.IsTrue(lastcommodity.Length.ClearAndEdit(_CommodityData.Length));
                Assert.IsTrue(lastcommodity.Width.ClearAndEdit(_CommodityData.Width));
                Assert.IsTrue(lastcommodity.Height.ClearAndEdit(_CommodityData.Height));
                Assert.IsTrue(lastcommodity.Value.ClearAndEdit(_CommodityData.Value));
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        #endregion

        #region Submit
        public string Submit()
        {
            try
            {
               Assert.IsTrue(_CreateLoadPage.Submit.WaitUntilDisplayed());
               Assert.IsTrue(_CreateLoadPage.Submit.FindAndClickUsingJS());
               Assert.IsTrue(_CreateLoadPage.CreateAnotherLoad.WaitUntilDisplayed());
               Assert.IsTrue(_CreateLoadPage.GoToMyLoads.WaitUntilDisplayed());
               return "CreateLoadSuccess";
            }
            catch
            {
                return "CreateLoadSubmitFailed";
            }
        }
        #endregion

        #region Get LoadId
        public string GetLoadID()
        {
            try
            {
                Assert.IsTrue(_CreateLoadPage.LoadId.WaitUntilDisplayed());
                _CreateLoadData.setLoadId(_CreateLoadPage.LoadId.GetText());
               // _CreateLoadData.LoadId = _CreateLoadPage.LoadId.GetText();
                return "DataCopied";
            }
            catch
            {
                return "DataCopyFailed";
            }
        }
        #endregion 

        #region Open LoadId
        public string OpenLoadId()
        {
            try
            {
                string actualresult = null;
                Assert.IsTrue(_CreateLoadPage.LoadId.WaitUntilDisplayed());
                Assert.IsTrue(_CreateLoadPage.LoadId.Click());
                Assert.IsTrue(_LoadDetailPage.OptionBtn.WaitUntilDisplayed());
                if (_LoadDetailPage.OptionBtn.IsDisplayed())
                    actualresult = "LoadOpenSuccess";
                else
                    actualresult = "LoadOpenFailed";
                return actualresult;
            }
            catch
            {
                return "LoadOpenFailed";
            }
        }
        #endregion

        public string VerifyTarp()
        {
            try
            {
                if (_CreateLoadData.TarpQuantity.ToUpper() == "!IGNORE!")
                {
                    Submit();
                    Assert.IsTrue(_CreateLoadPage.TarpQuantity.HasClass("error"));
                }
                else
                {
                    if(!_CreateLoadPage.TarpQuantity.GetValue().Equals(string.Empty))
                    {
                        Assert.IsTrue(Convert.ToInt32(_CreateLoadPage.TarpQuantity.GetValue()) < 100);
                    }
                    string tarpQuantity = _CreateLoadPage.TarpQuantity.GetValue();
                    Assert.IsTrue(_CreateLoadPage.TarpQuantity.ClearAndEdit("Test"));
                    Assert.IsTrue(_CreateLoadPage.TarpQuantity.GetValue().Equals(string.Empty));
                    Assert.IsTrue(_CreateLoadPage.TarpQuantity.ClearAndEdit(tarpQuantity));
                }
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFail";
            }
        }

        public string VerifyForDuplicateShipmentId()
        {
            try
            {
                if (_CreateLoadData.AllowDuplicateShipmentId.ToUpper() == "FALSE")
                {
                    Assert.IsTrue(_CreateLoadPage.ReferenceNumber.HasClass("error"));
                }
                return "VerifySuccess";
            }
            catch
            {
                return "VerifyFail";
            }
        }

        public string SaveLoadTemplate()
        {
            try
            {
                int totalTemplate = _CreateLoadPage.TemplateListContainer.FindElements().Count;
                Assert.IsTrue(_CreateLoadPage.LoadTemplatePopup.IsDisplayed());
                Assert.IsTrue(_CreateLoadPage.LoadTemplatePopup.Click());
                Assert.IsTrue(_CreateLoadPage.CreateTemplateButton.Click());
                string templatePostfix = _CreateLoadData.TemplateName;
                Assert.IsTrue(_CreateLoadPage.TemplateName.WaitUntilDisplayed());
                Assert.IsTrue(_CreateLoadPage.TemplateName.ClearAndEdit("TestTemplate" + templatePostfix));
                Assert.IsTrue(_CreateLoadPage.SaveTemplateButton.Click());
                Thread.Sleep(30000);
                Assert.IsTrue(_CreateLoadPage.TemplateListContainer.GetCountOfElements().Equals(totalTemplate + 1));
                _CreateLoadData.NewlyCreatedTemplate = "TestTemplate" + templatePostfix;
                return "TemplateSaved";
            }
            catch
            {
                return "TemplateSaveFailed";
            }
        }

        public string VerifyTemplateData()
        {
            try
            {
                Assert.IsTrue(_CreateLoadPage.PickUpFacility.GetValue().Equals(_CreateLoadData.PickupFacility));
                Assert.IsTrue(_CreateLoadPage.DeliveryFacility.GetValue().Equals(_StopData.DeliveryFacility));
                Assert.IsTrue(_CreateLoadPage.EquipmentType.CompareDDMyOptions(new List<string>() { _CreateLoadData.EquipmentType }));
                Assert.IsTrue(_CreateLoadPage.MinimumLength.CompareDDMyOptions(new List<string>() { _CreateLoadData.MinimumLength }));
                Assert.IsTrue(_CreateLoadPage.Notes.GetValue().Equals(_CreateLoadData.Notes));
                Commodity_CreateLoad firstcommodity = _CreateLoadPage.Stops.First().Commodity.First();
                Assert.IsTrue(firstcommodity.Description.CompareDDMyOptions(new List<string>() { _CommodityData.Description }));
                Assert.IsTrue(firstcommodity.Weight.GetValue().Equals(_CommodityData.Weight));
                Assert.IsTrue(firstcommodity.Quantity.GetValue().Equals(_CommodityData.Quantity));
                Assert.IsTrue(firstcommodity.PackagingTypes.CompareDDMyOptions(new List<string>() { _CommodityData.PackagingType }));
                Assert.IsTrue(firstcommodity.Pallets.GetValue().Equals(_CommodityData.Pallets));
                Assert.IsTrue(firstcommodity.Length.GetValue().Equals(_CommodityData.Length));
                Assert.IsTrue(firstcommodity.Width.GetValue().Equals(_CommodityData.Width));
                Assert.IsTrue(firstcommodity.Height.GetValue().Equals(_CommodityData.Height));
                Assert.IsTrue(firstcommodity.Value.GetValue().Equals(_CommodityData.Value));
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
        }

        public string LoadNewTemplate()
        {
            try
                {
                Assert.IsTrue(OpenCreateLoadPage());
                Assert.IsTrue(_CreateLoadPage.LoadTemplatePopup.IsDisplayed());
                Assert.IsTrue(_CreateLoadPage.LoadTemplatePopup.Click());
                int index = _CreateLoadPage.TemplateListContainer.GetOneElementIndex(_CreateLoadData.NewlyCreatedTemplate);
                Assert.IsTrue(_CreateLoadPage.TemplateListContainer.Click(index));
                Thread.Sleep(15000);
                Assert.IsTrue(_CreateLoadPage.PickUpFacility.GetValue().Equals(_CreateLoadData.PickupFacility));
                return "TemplateLoadedSuccessfully";
            }
            catch
            {
                return "CouldNotLoad";
            }
        }
    }
}


//private bool DeliveryDetails()
//{
//    try
//    {
//        _CreateLoadPage.DeliveryFacility.TypeAndSelect(_CreateLoadData.DeliveryFacility);
//        _CreateLoadPage.DeliveryNumber.ClearAndEdit(_CreateLoadData.DeliveryNumber);
//        List<IWebElement> byDelDateAvi = driver.FindElements(By.CssSelector("[id*='dp']")).ToList<IWebElement>();
//        IWebElement weDelDateAvi = byDelDateAvi[0];
//        IWebElement weDelDateBy = byDelDateAvi[1];
//        setDate(weDelDateAvi, _CreateLoadData.DeliveryAvailable);
//        setDate(weDelDateBy, _CreateLoadData.DeliverBy);

//        _CreateLoadPage.DeliveryStartTime.ClearAndEdit(_CreateLoadData.Del_AppointmentStartTime);
//        _CreateLoadPage.DeliveryEndTime.ClearAndEdit( _CreateLoadData.Del_AppointmentEndTime);
//        return AddCommodities();
//    }
//    catch
//    {
//        return false;
//    }
//}

//public bool AddCommodities()
//{
//    try
//    {
//        for (var iLoop = 1; _CreateLoadData.NoCommodities > iLoop; iLoop++)
//        {
//            driver.FindElement(By.CssSelector("#add-commodity")).Click();
//        }

//        List<IWebElement> drDesc = driver.FindElements(By.CssSelector(".one-whole.hook--validation.hook--commodity-description")).ToList();
//        List<IWebElement> drWt = driver.FindElements(By.CssSelector("#load-weight")).ToList();
//        List<IWebElement> drQty = driver.FindElements(By.CssSelector(".hook--commodity-piece.text-input.one-whole.hook--validation")).ToList();
//        List<IWebElement> drVal = driver.FindElements(By.CssSelector(".hook--commodity-value.text-input.one-whole.hook--validation")).ToList();
//        List<IWebElement> drPacType = driver.FindElements(By.CssSelector(".one-whole.hook--commodity-packaging-type")).ToList();
//        List<IWebElement> drLength = driver.FindElements(By.CssSelector(".hook--commodity-length.text-input.one-whole")).ToList();
//        List<IWebElement> drWidth = driver.FindElements(By.CssSelector(".hook--commodity-width.text-input.one-whole")).ToList();
//        List<IWebElement> drHeight = driver.FindElements(By.CssSelector(".hook--commodity-height.text-input.one-whole")).ToList();
//        List<IWebElement> drPallets = driver.FindElements(By.CssSelector(".hook--commodity-pallets.text-input.one-whole")).ToList();


//        for (var iLoop = 0; _CreateLoadData.NoCommodities > iLoop; iLoop++)
//        {
//            SelectByText(drDesc[iLoop], _CreateLoadData.Desc[iLoop]);
//            ClearAndEdit(drWt[iLoop], _CreateLoadData.Wt[iLoop]);
//            ClearAndEdit(drQty[iLoop], _CreateLoadData.Qty[iLoop]);
//            ClearAndEdit(drVal[iLoop], _CreateLoadData.Value[iLoop]);
//            //ClearAndEdit(drPacType[iLoop],_CreateLoadData.PacType[iLoop]);
//            drPacType[iLoop].SendKeys(_CreateLoadData.PacType[iLoop]); // since above method is not working using this simple options
//            ClearAndEdit(drLength[iLoop], _CreateLoadData.Length[iLoop]);
//            ClearAndEdit(drWidth[iLoop], _CreateLoadData.Width[iLoop]);
//            ClearAndEdit(drHeight[iLoop], _CreateLoadData.Height[iLoop]);
//            ClearAndEdit(drPallets[iLoop], _CreateLoadData.Pallets[iLoop]);
//        }
//        MyLogger.Log("Commodities details filled successfully.");
//        return true;
//    }
//    catch
//    {
//        return false;
//    }
//}
        //private By byPickUp = By.CssSelector("#create-load-facility-search-pickup");

        ////Shipper Details
        //private By byRefNo = By.CssSelector("#reference-number");

        //private By byPickUpNo = By.CssSelector("#pickup-number");
        //private By byEqLength = By.CssSelector("#createload-equipment-length");
        //private By byEqType = By.CssSelector("#createload-equipment-type");
        //private By byPickDate = By.CssSelector("#createload-pickup-date");
        //private By byPickTime = By.CssSelector("#createload-pickup-time");
        //private By byNotes = By.CssSelector("#notes");
        //private By byPickUpDateAvi = By.CssSelector("#createload-pickup-available");
        //private By byPickUpDateBy = By.CssSelector("#createload-pickup-by");
        //private By byApptTimeStart = By.CssSelector("#createload-pickup-start-time");
        //private By byApptTimeEnd = By.CssSelector("#createload-pickup-end-time");
