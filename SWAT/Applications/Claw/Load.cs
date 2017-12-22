using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SWAT.Applications.Claw
{
    using SWAT.Data;
    using SWAT.FrameWork.Utilities;
    using Config = SWAT.Configuration.TestStartInfo;
    using SWAT.Applications.Claw.ObjectRepository;
    using DAL;

    public class Load : UIActionsAndStates
    {
        private DataManager testData;
        private LoadDetailsPage _LoadDetailsPage;
        private LoadDetails_Charger _LoadDetails_Charger;
        private CreateLTLLoadData _CreateLTLLoadData;
        private LoadDetails_Stops _LoadDetails_Stops;

        public Load(Config c, DataManager t) //: base(c)
        {
            testConfig = c;
            driver = testConfig.Driver;
            testData = t;
            _LoadDetailsPage = new LoadDetailsPage(c);
            _LoadDetails_Charger = new LoadDetails_Charger(c);
            _LoadDetails_Stops = new LoadDetails_Stops(c);
            _CreateLTLLoadData = new CreateLTLLoadData(t);
        }

        public string VerifyDetails()
        {
            //UIItem LoadInquireBtn = new UIItem("Load details page>> Load inquire button", By.CssSelector("#load-inquiry"), driver);
            //UIItem OptionBtn = new UIItem("Load details page>> Option Button", By.CssSelector("#options-button"), driver);
            string result = testData.Data("EntityName") == "FACTORINGCOMPANY" ? AFactoringCompany() : NotAFactortingCompany();
            return result;
        }

        public string NotAFactortingCompany()
        {
            UIItem byTrackingTab = new UIItem("Tracking Tab", By.XPath(".//*[@id='tracking-container']/a"), driver);
            UIItem byStopsTab = new UIItem("Stop Tab", By.XPath(".//*[@id='stops-container']/a"), driver);
            UIItem byDocumentsTab = new UIItem("Documents Tab", By.XPath(".//*[@id='documents-container']/a"), driver);
            UIItem byChargesTab = new UIItem("Charges Tab", By.XPath(".//*[@id='charges-container']/a"), driver);
            UIItem byTenderStatusTab = new UIItem("Tender Status Tab", By.CssSelector("#tender-history-button"), driver);
            UIItem bySummaryTab = new UIItem("Summary Tab", By.XPath(".//*[@id='summary']/header"), driver);

            try
            {
                Assert.IsTrue(byTrackingTab.WaitUntilDisplayed());
                Assert.IsTrue(byStopsTab.WaitUntilDisplayed());
                Assert.IsTrue(bySummaryTab.WaitUntilDisplayed());
                //Assert.IsTrue(WaitUtilDisplayed(byChargesTab));
                Assert.IsTrue(byDocumentsTab.WaitUntilDisplayed());
                Assert.IsTrue(VerifyLoadDetailsSummary());
                return "LoadVerificationSuccess";

            }
            catch
            {
                return "LoadVerificationFailed";
            }
        }

        public string AFactoringCompany()
        {
            UIItem Print = new UIItem("Print button", By.CssSelector("#load-summary-print"), driver);
            UIItem LoadInquireBtn = new UIItem("Load details page>> Load inquire button", By.CssSelector("#load-inquiry"), driver);
            UIItem SummaryTab = new UIItem("Summary Tab", By.XPath(".//*[@id='summary']/header"), driver);
            UIItem StopsTab = new UIItem("Stop Tab", By.XPath(".//*[@id='stops-region']/div"), driver);
            UIItem DocumentsTab = new UIItem("Documents Tab", By.XPath(".//*[@id='documents']/header"), driver);
            UIItem ChargesTab = new UIItem("Charges Tab", By.XPath(".//*[@id='coyote-charges']/div"), driver);


            try
            {
                Assert.IsTrue(Print.WaitUntilDisplayed());
                Assert.IsTrue(LoadInquireBtn.WaitUntilDisplayed());
                Assert.IsTrue(SummaryTab.IsDisplayed());
                Assert.IsTrue(StopsTab.IsDisplayed());
                //ToDo: Need to fix this object issue.
                //Assert.IsTrue(DocumentsTab.IsDisplayed());
                Assert.IsTrue(ChargesTab.IsDisplayed());
                return "LoadVerificationSuccess";

            }
            catch
            {
                return "LoadVerificationFailed";
            }
        }


        private bool VerifyLoadDetailsSummary()
        {
            try
            {
                UIItem summaryRegion = new UIItem("Summary Region", By.XPath(".//*[@id='summary']"), driver);
                String summaryRegionData = GetText(summaryRegion);
                Assert.IsTrue(ValidateData(summaryRegionData, "ReferenceNumber"));
                Assert.IsTrue(ValidateData(summaryRegionData, "LoadNumber"));
                Assert.IsTrue(ValidateData(summaryRegionData, "EquipmentLength"));
                Assert.IsTrue(ValidateData(summaryRegionData, "EquipmentType"));
                return true;
            }
            catch
            {
                return false;
            }

        }

        private bool ValidateData(String data, String colounmName)
        {
            try
            {
                if (!testData.Data(colounmName).Equals("!IGNORE!"))
                {
                    Assert.IsTrue(Regex.IsMatch(data, testData.Data(colounmName)));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string VerifyPickUpCommodities()
        {
            try
            {
                if (!testData.Data("RowNumber").Equals("!IGNORE!"))
                {
                    int rowNumber = Convert.ToInt32(testData.Data("RowNumber"));
                    UIItem stopsTab = new UIItem("Stop Tab", By.XPath(".//*[@id='stops-container']/a"), driver);
                    UIItem stopsRegionPanel = new UIItem("Stops Region Panel", By.Id("stops-region"), driver);
                    UIItem commodityRows = new UIItem("Commodity Rows", By.XPath(".//*[@id='stops-region']/div/div[1]/section/div/div[2]/table/tbody/tr"), driver);
                    Assert.IsTrue(Click(stopsTab));
                    Assert.IsTrue(WaitUtilDisplayed(stopsRegionPanel));
                    IList<IWebElement> commodityDataRows = FindElements(commodityRows);
                    Assert.IsTrue(commodityDataRows.Count > 0);
                    IWebElement commodityDataRow = commodityDataRows[rowNumber - 1];
                    IList<IWebElement> commodityColumns = commodityDataRow.FindElements(By.CssSelector("tr>td"));

                    Assert.IsTrue(ValidateData(commodityColumns[1].Text.Trim(), "Item"));
                    Assert.IsTrue(ValidateData(commodityColumns[2].Text.Trim(), "Packaging"));
                    Assert.IsTrue(ValidateData(commodityColumns[3].Text.Trim(), "LoadOn"));
                    Assert.IsTrue(ValidateData(commodityColumns[4].Text.Trim(), "ExpWeight"));
                    Assert.IsTrue(ValidateData(commodityColumns[5].Text.Trim(), "ActWeight"));
                    Assert.IsTrue(ValidateData(commodityColumns[6].Text.Trim(), "ExpPcs"));
                    Assert.IsTrue(ValidateData(commodityColumns[7].Text.Trim(), "ActPcs"));
                    Assert.IsTrue(ValidateData(commodityColumns[8].Text.Trim(), "ExpPallets"));
                    Assert.IsTrue(ValidateData(commodityColumns[9].Text.Trim(), "ActPallets"));
                    Assert.IsTrue(ValidateData(commodityColumns[10].Text.Trim(), "Hazmat"));
                    Assert.IsTrue(ValidateData(commodityColumns[11].Text.Trim(), "HazmatClass"));
                    Assert.IsTrue(ValidateData(commodityColumns[12].Text.Trim(), "UnitNmuber"));

                }

                return "PickUpCommoditiesVerificationSuccess";
            }
            catch
            {
                return "PickUpCommoditiesVerificationFailed";
            }

        }

        public string VerifyBolEditStatus()
        {
            UIItem stopsTab = new UIItem("Stops Tab", By.XPath(".//*[@id='stops-container']/a"), driver);
            UIItem stopsRegionPanel = new UIItem("Stops Region Panel", By.Id("stops-region"), driver);
            UIItem updateButton = new UIItem("Update Button", By.CssSelector(".text-link.nudge--left.fr.hook--edit-stop"), driver);
            UIItem saveButton = new UIItem("Save Button", By.CssSelector(".button.button--loud.hook--save-update"), driver);
            UIItem billOfLanding = new UIItem("Bill Of Landing", By.CssSelector(".text-input.hook--bol-number-input.nudge-half--bottom"), driver);

            try
            {
                Assert.IsTrue(Click(stopsTab));
                Assert.IsTrue(WaitUtilDisplayed(stopsRegionPanel));
                IList<IWebElement> elements = FindElements(updateButton);
                Assert.IsNotNull(elements);
                foreach (IWebElement element in elements)
                {
                    if (element.GetAttribute("data-edit-field").Trim().Equals("bol"))
                    {
                        element.Click();
                        Assert.IsTrue(WaitUtilDisplayed(saveButton));
                        Assert.IsTrue(WaitUtilDisplayed(billOfLanding));
                        try
                        {
                            Assert.IsTrue(IsEnabled(billOfLanding));
                            return "Editable";
                        }
                        catch
                        {
                            return "NonEditable";
                        }
                    }
                }
                return "Failed";
            }
            catch
            {
                return "Failed";
            }

        }

        public string VerifyPodEditStatus()
        {
            UIItem stopsTab = new UIItem("Stops Tab", By.XPath(".//*[@id='stops-container']/a"), driver);
            UIItem stopsRegionPanel = new UIItem("Stops Region Panel", By.Id("stops-region"), driver);
            UIItem updateButton = new UIItem("Update Button", By.CssSelector(".text-link.nudge--left.fr.hook--edit-stop"), driver);
            UIItem saveButton = new UIItem("Save Button", By.CssSelector(".button.button--loud.hook--save-update"), driver);
            UIItem profOfDelivery = new UIItem("Prof Of Delivery", By.CssSelector(".text-input.hook--pod-name-input.nudge-half--bottom"), driver);

            try
            {
                Assert.IsTrue(Click(stopsTab));
                Assert.IsTrue(WaitUtilDisplayed(stopsRegionPanel));
                IList<IWebElement> elements = FindElements(updateButton);
                Assert.IsNotNull(elements);
                foreach (IWebElement element in elements)
                {
                    if (element.GetAttribute("data-edit-field").Trim().Equals("pod"))
                    {
                        element.Click();
                        Assert.IsTrue(WaitUtilDisplayed(saveButton));
                        Assert.IsTrue(WaitUtilDisplayed(profOfDelivery));
                        try
                        {
                            Assert.IsTrue(IsEnabled(profOfDelivery));
                            return "Editable";
                        }
                        catch
                        {
                            return "NonEditable";
                        }
                    }
                }
                return "Failed";
            }
            catch
            {
                return "Failed";
            }

        }

        public string VerifyCharges()
        {
            UIItem chargesTab = new UIItem("Stops Tab", By.XPath(".//*[@id='charges-container']/a"), driver);
            UIItem chargesRegionPanel = new UIItem("Charges Panel", By.Id("charges-region"), driver);
            UIItem panelTitle = new UIItem("Panel Title", By.ClassName("panel__title"), driver);

            try
            {
                Assert.IsTrue(Click(chargesTab));
                Assert.IsTrue(WaitUtilDisplayed(chargesRegionPanel));
                IList<IWebElement> elements = FindElements(panelTitle);
                Assert.IsNotNull(elements);

                if (testData.Data("ManagedCharges").Equals("!DISPLAYED!"))
                {
                    Assert.IsTrue(VerifyChargesData(elements, testData.Data("ManagedCarrier"), "!DISPLAYED!"));
                }

                if (testData.Data("ManagedCharges").Equals("!NOTDISPLAYED!"))
                {
                    Assert.IsTrue(VerifyChargesData(elements, testData.Data("ManagedCarrier"), "!NOTDISPLAYED!"));
                }

                if (testData.Data("NonManagedCharges").Equals("!DISPLAYED!"))
                {
                    Assert.IsTrue(VerifyChargesData(elements, testData.Data("NonManagedCarrier"), "!DISPLAYED!"));
                }

                if (testData.Data("NonManagedCharges").Equals("!NOTDISPLAYED!"))
                {
                    Assert.IsTrue(VerifyChargesData(elements, testData.Data("NonManagedCarrier"), "!NOTDISPLAYED!"));
                }
                return "VerifyChargesSuccess";

            }
            catch
            {
                return "VerifyChargesFailed";
            }

        }

        private bool VerifyChargesData(IList<IWebElement> elements, String title, String status)
        {
            try
            {
                foreach (IWebElement element in elements)
                {
                    if (element.Text.Trim().Equals(title) && status.Equals("!DISPLAYED!"))
                    {
                        Assert.IsTrue(element.Displayed);
                    }
                    else if (element.Text.Trim().Equals(title) && status.Equals("!NOTDISPLAYED!"))
                    {
                        Assert.IsFalse(element.Displayed);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }

        }



        public string Inquire()
        {
            UIItem AppTitile = new UIItem("App title", By.CssSelector("#app-title"), driver);
            UIItem InquireButton = new UIItem("Inquiry About This Load", By.CssSelector("#load-inquiry"), driver);
            UIItem SendInquireButton = new UIItem("Send Inquiry", By.CssSelector("#send-inquiry"), driver);
            UIItem OptionButton = new UIItem("Option button", By.CssSelector("#options-button"), driver);
            UIItem CloseButton = new UIItem("Close Button", By.CssSelector("#modal-header-close-modal-button"), driver);
            UIItem PageTitle = new UIItem("Page Title - Load detail", By.CssSelector("#app-title"), driver);
            try
            {
                if (testData.Data("EntityName") != "FACTORINGCOMPANY")
                {
                    AppTitile.Click();
                    Assert.IsTrue(OptionButton.WaitUntilDisplayed());
                    OptionButton.Click();
                    Assert.IsTrue(InquireButton.WaitUntilDisplayed());
                }
                Assert.IsTrue(InquireButton.FindAndClickUsingJS());
                Assert.IsTrue(SendInquireButton.WaitUntilDisplayed());
                Assert.IsTrue(CloseButton.FindAndClickUsingJS());
                Assert.IsTrue(SendInquireButton.WaitUtilDisappear());
                return "LoadInquireSuccess";
            }
            catch
            {
                return "LoadInquireFailed";
            }
        }

        public string ReTender()
        {

            try
            {
                try
                {
                    Assert.IsTrue(_LoadDetailsPage.OptionBtn.WaitUntilDisplayed());
                    _LoadDetailsPage.OptionBtn.Click();
                }
                catch
                {
                    return "NoOptionButtonForFactoring";
                }
                Assert.IsTrue(_LoadDetailsPage.RetenderLoadBtn.WaitUntilDisplayed());
                try
                {
                    Assert.IsTrue(FindAndClickUsingJS(_LoadDetailsPage.RetenderLoadBtn));
                }
                catch
                {
                    Assert.IsTrue(_LoadDetailsPage.RetenderLoadBtn.Click());
                }
                Assert.IsTrue(_LoadDetailsPage.RetenderSubmitBtn.WaitUntilDisplayed());
                try
                {
                    Assert.IsTrue(FindAndClickUsingJS(_LoadDetailsPage.ModalCloseBtn));
                    Assert.IsTrue(_LoadDetailsPage.RetenderSubmitBtn.WaitUtilDisappear());
                }
                catch
                {
                    Edit(_LoadDetailsPage.RetenderLoadBtn, Keys.Escape);
                    Assert.IsFalse(_LoadDetailsPage.RetenderSubmitBtn.WaitUntilDisplayed());
                }
                return "LoadRetenderSuccess";
            }
            catch
            {
                return "LoadRetenderFailed";
            }
        }
        public string CancelLoad()
        {

            try
            {
                try
                {
                    Assert.IsTrue(_LoadDetailsPage.OptionBtn.WaitUntilDisplayed());
                    _LoadDetailsPage.OptionBtn.Click();
                }
                catch
                {
                    return "NoOptionButtonForFactoring";
                }
                Assert.IsTrue(_LoadDetailsPage.CancelLoadBtn.WaitUntilDisplayed());
                try
                {
                    Assert.IsTrue(FindAndClickUsingJS(_LoadDetailsPage.CancelLoadBtn));
                }
                catch
                {
                    Assert.IsTrue(_LoadDetailsPage.CancelLoadBtn.Click());
                }
                Assert.IsTrue(_LoadDetailsPage.ConfirmCancellationBtn.WaitUntilDisplayed());
                try
                {
                    Assert.IsTrue(FindAndClickUsingJS(_LoadDetailsPage.ModalCloseBtn));
                    Assert.IsTrue(_LoadDetailsPage.ConfirmCancellationBtn.WaitUtilDisappear());
                }
                catch
                {
                    Edit(_LoadDetailsPage.CancelLoadBtn, Keys.Escape);
                    Assert.IsFalse(_LoadDetailsPage.ConfirmCancellationBtn.WaitUntilDisplayed());
                }
                return "CancelLoadSuccess";
            }
            catch
            {
                return "CancelLoadFailed";
            }
        }

        public string GetPricingDetails()
        {
            try
            {
                Assert.IsTrue(_LoadDetailsPage.WaitUntilLoading());
                Assert.IsTrue(_LoadDetailsPage.ChargesTab.Click());
                Assert.IsTrue(_LoadDetailsPage.ChargersTab_Chargers.WaitUntilDisplayed());

                if (_LoadDetails_Charger.ChargesTable_ChargesCol.ElementByIndex(1).Text.Contains("Flat Rate"))
                {
                    string flatRate = _LoadDetails_Charger.ChargesTable_AmountCol.ElementByIndex(1).Text;
                    flatRate = flatRate.Replace("$", "");
                    flatRate = flatRate.Insert(flatRate.Length, " CAD");
                    _CreateLTLLoadData.FlatRate = flatRate;

                    if (_LoadDetails_Charger.ChargesTable_ChargesCol.ElementByIndex(2).Text.Contains("Fuel Surcharge"))
                    {
                        string fuelSurcharge = _LoadDetails_Charger.ChargesTable_AmountCol.ElementByIndex(2).Text;
                        fuelSurcharge = fuelSurcharge.Replace("$", "");
                        fuelSurcharge = fuelSurcharge.Insert(fuelSurcharge.Length, " CAD");
                        _CreateLTLLoadData.FuelSurcharge = fuelSurcharge;
                    }

                    return "DataCopied";
                }
                else
                {
                    return "Failed";
                }
            }
            catch
            {
                return "Failed";
            }
        }
        
        public string GetFacilityDetails()
        {
            try
            {
                Assert.IsTrue(_LoadDetailsPage.StopsTab.Click());
                Assert.IsTrue(_LoadDetailsPage.StopsTab_Stops.WaitUntilDisplayed());

                string pickupAddress = _LoadDetails_Stops.Pickup_Address.GetText();
                string deliveryAddress = _LoadDetails_Stops.Delivery_Address.GetText();

                _CreateLTLLoadData.Pickup_Name = _LoadDetails_Stops.Pickup_Name.GetText();
                _CreateLTLLoadData.Pickup_Street = pickupAddress.Substring(0, pickupAddress.IndexOf("\r\n"));
                _CreateLTLLoadData.Delivery_Name= _LoadDetails_Stops.Delivery_Name.GetText();
                _CreateLTLLoadData.Delivery_Street = deliveryAddress.Substring(0, deliveryAddress.IndexOf("\r\n"));

                return "DataCopied";
            }
            catch
            {
                return "Failed";
            }
        }
    }
}