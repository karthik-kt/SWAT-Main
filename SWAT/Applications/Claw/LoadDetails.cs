using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Threading;

namespace SWAT.Applications.Claw
{
    using SWAT.Applications.Claw.DAL;
    using SWAT.Data;
    using SWAT.Logger;
    using SWAT.FrameWork.Utilities;
    using SWAT.Applications.Claw.ObjectRepository;
    using SWAT.Configuration;
    using System.Windows.Forms;
    using System.Linq;
    using System;

    public class LoadDetails
    {

        private LoadDetailsData _Data;
        private LoadDetailsPage _LoadDetailsPage;
        private LoadDetails_Tracking _TrackingTab;
        private LoadDetails_Stops _StopsTab;
        private LoadDetails_Documents _LoadDetails_DocumentTab;
        private LoadDetails_Charger _ChargesTab;
        private LoadDetails_TenderStatus _TenderStatusTab;

        private LoadDetails_SummaryDetails _LoadDetails_SummaryDetails;
        private LoadDetails_DispatchPanel _LoadDetails_DispatchPanel;
        private UIItem stopsUpdateLink = null;
        IWebDriver driver = null;
        DataManager _datamanager;

        public LoadDetails(TestStartInfo teststartinfo, DataManager datamanager)
        {
            driver = teststartinfo.Driver;
            _datamanager = datamanager;
            _Data = new LoadDetailsData(datamanager);
            _LoadDetailsPage = new LoadDetailsPage(teststartinfo);
            _TrackingTab = new LoadDetails_Tracking(teststartinfo);
            _StopsTab = new LoadDetails_Stops(teststartinfo);
            _LoadDetails_DocumentTab = new LoadDetails_Documents(teststartinfo);
            _ChargesTab = new LoadDetails_Charger(teststartinfo);
            _TenderStatusTab = new LoadDetails_TenderStatus(teststartinfo);
            _LoadDetails_SummaryDetails = new LoadDetails_SummaryDetails(teststartinfo);
            _LoadDetails_DispatchPanel = new LoadDetails_DispatchPanel(teststartinfo);
        }


        #region Stops Tab
        private bool OpenStops()
        {
            try
            {
                if (!stopsUpdateLink.IsDisplayed())
                {
                    Assert.IsTrue(_LoadDetailsPage.StopsTab.WaitUntilDisplayed());
                    Assert.IsTrue(_LoadDetailsPage.StopsTab.Click());
                    Assert.IsTrue(stopsUpdateLink.WaitUntilDisplayed());
                }               
               return true;
            }
            catch
            {
                return false;
            }
        }

        private bool EditStops()
        {
            try
            {
                Assert.IsTrue(stopsUpdateLink.Click());
                Assert.IsTrue(_StopsTab.Stop_UpdateSave.WaitUntilDisplayed());
                Assert.IsTrue(_StopsTab.ArrivalDate.ClearAndEdit(_Data.PickUp_ArrivalDate));
                Assert.IsTrue(_StopsTab.ArrivalTime.ClearAndEdit(_Data.PickUp_ArrivalTime));
                _StopsTab.ArrivalTime.Edit(OpenQA.Selenium.Keys.Enter);
                if (_StopsTab.Latepickup_Reason.WaitUntilDisplayed(10))
                    _StopsTab.Latepickup_Continue.Click();
                Assert.IsTrue(_StopsTab.DepartureDate.ClearAndEdit(_Data.PickUp_DepartureDate));
                Assert.IsTrue(_StopsTab.DepartureTime.ClearAndEdit(_Data.PickUp_DepartureTime));
                _StopsTab.DepartureTime.Edit(OpenQA.Selenium.Keys.Enter);
                if (_StopsTab.Latepickup_Reason.WaitUntilDisplayed(10))
                    _StopsTab.Latepickup_Continue.Click();
                if (_Data.EditLoadCommodity.ToUpper() == "TRUE")
                {
                    double expWeight = Convert.ToDouble(_StopsTab.Commodities1strow_expWeight.GetText() == "--" ? "0" :
                                            _StopsTab.Commodities1strow_expWeight.GetText().Replace(" lbs", ""));
                    double expWeightPercent = Convert.ToDouble(_Data.WeightPercent == "!IGNORE!" ? "0" : _Data.WeightPercent);
                    int actualWeight = (int)Math.Ceiling(expWeight + expWeight * (expWeightPercent / 100));

                    double expPieces = Convert.ToDouble(_StopsTab.Commodities1strow_expPieces.GetText() == "--" ? "0" :
                                            _StopsTab.Commodities1strow_expPieces.GetText());
                    double expPiecesPercent = Convert.ToDouble(_Data.PiecesPercent == "!IGNORE!" ? "0" : _Data.PiecesPercent);
                    int actualPieces = (int)Math.Ceiling(expPieces + expPieces * (expPiecesPercent / 100));
                    if(_Data.WeightPercent != "!IGNORE!")
                    {
                        Assert.IsTrue(_StopsTab.Commodities1stRowActWeight.ClearAndEdit(actualWeight.ToString()));
                    }
                    if(_Data.PiecesPercent != "!IGNORE!")
                    {
                        Assert.IsTrue(_StopsTab.Commodities1stRowActPieces.ClearAndEdit(actualPieces.ToString()));
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool SaveStops()
        {
            IWebElement invalidError = null;
            try
            {
                if(_StopsTab.LoadstopConfirmCheckbox.IsDisplayed())
                    Assert.IsTrue(_StopsTab.LoadstopConfirmCheckbox.Click());
                Assert.IsTrue(_StopsTab.Stop_UpdateSave.WaitUtilEnabled());
                Assert.IsTrue(_StopsTab.Stop_UpdateSave.Click());
                try
                {
                    Thread.Sleep(Constants.Wait_Short);
                    invalidError = driver.FindElement(By.CssSelector(".tooltipster-base.tooltipster-error"));
                    if (invalidError.Text == "Invalid")
                    {
                        MyLogger.Log("Error message is shown for invalid departure time");
                        return false;
                    }
                }
                catch
                {
                    //No error occured
                }
                _StopsTab.Stop_UpdateSave.WaitUtilDisappear();
                Assert.IsTrue(_StopsTab.Stop_UpdateCancel.WaitUtilDisappear());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string PickUpDetails_Edit()
        {
            try
           {
                stopsUpdateLink = _StopsTab.Pickup_Update;
                Assert.IsTrue(OpenStops());
                Assert.IsTrue(EditStops());
                Assert.IsTrue(SaveStops());                
                return "PickupDetailUpdate";
            }
            catch
            {
                return "PickupDetailUpdateFailed";
            }

        }

        public string DeliveryDetails_Edit()
        {
            try
            {
                stopsUpdateLink = _StopsTab.Delivery_Update;
                Assert.IsTrue(OpenStops());
                Assert.IsTrue(EditStops());
                Assert.IsTrue(SaveStops());
                return "DeliveryDetailUpdate";
            }
            catch
            {
                return "DeliveryDetailUpdateFailed";
            }
        }

        public string Commodities_Verify()
        {
            try
            {
                if (_Data.VerifyUnitQtyWithFirstCommodity.ToUpper() == "TRUE")
                {
                    Assert.IsTrue(_LoadDetailsPage.StopsTab.Click());
                    Assert.IsTrue(_StopsTab.Pickup_Update.WaitUntilDisplayed());
                    List<LoadDetailsCommodity> loadCommodities = _StopsTab.PULoadDetailsCommodities.ToList();
                    string[] arrUnitQty = _Data.ActQtyForAllUnit.Split(';');
                    int totalShipment = Convert.ToInt32(_Data.TotalShipment);
                    int totalCommodities = Convert.ToInt32(_Data.TotalCommodityInShipment);
                    int visitedCommodities = 0;
                    for (int su = 0; su < totalShipment; su++)
                    {
                        bool firstComm = true;
                        totalCommodities += visitedCommodities;
                        for (int c = visitedCommodities; c < totalCommodities; c++)
                        {
                            if (firstComm)
                            {
                                Assert.IsTrue(loadCommodities[c].ExpPallets.GetText() == arrUnitQty[su]);
                                firstComm = false;
                            }
                            else
                            {
                                Assert.IsTrue(loadCommodities[c].ExpPallets.GetText() == "--");
                            }
                            visitedCommodities++;
                        }
                    }
                    return "VerificationSuccess";
                }

                while (!_StopsTab.Stop_UpdateSave.IsDisplayed())
                {
                    Assert.IsTrue(_LoadDetailsPage.StopsTab.Click());
                    Assert.IsTrue(_StopsTab.Pickup_Update.WaitUntilDisplayed());
                    Assert.IsTrue(_StopsTab.Pickup_Update.Click());
                }
                Assert.IsTrue(_StopsTab.LoadstopCommoditiesSection.WaitUntilDisplayed());
                Assert.IsTrue(_StopsTab.Commodities1stRowPackaging.IsEnabled());
                Assert.IsTrue(_StopsTab.Commodities1stRowLoadOn.IsEnabled());
                Assert.IsTrue(_StopsTab.Commodities1stRowActWeight.IsEnabled());
                Assert.IsTrue(_StopsTab.Commodities1stRowActPieces.IsEnabled());
                Assert.IsTrue(_StopsTab.Commodities1stRowActPallets.IsEnabled());
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
        }
        #endregion

        #region Other functions
        public string VerifyControls()
        {
            try
            {
                Assert.IsTrue(_TrackingTab.DispatchDriverButton.StatusCheckORPerFormAction(_Data.Dispatch_Button));
                return "VerifyControlsSuccess";
            }
            catch
            {
                return "VerifyControlsFailed";
            }
        }

        public string DispatchDriver(DataManager datamanager)
        {
            try
            {
                Assert.IsTrue(_TrackingTab.DispatchDriverButton.Click());
                Assert.IsTrue(_TrackingTab.DispatchDriverForm.WaitUntilDisplayed());
                Assert.IsTrue(_TrackingTab.FirstDriver.ClearAndEdit(_Data.FirstDriver));
                Assert.IsTrue(_TrackingTab.FirstCellPhoneNumber.ClearAndEdit(_Data.FirstDriverCellNumber));
                Assert.IsTrue(_TrackingTab.Team.StatusCheckORPerFormAction(_Data.Team));
                Assert.IsTrue(_TrackingTab.ActualEmptyLocation.TypeAndSelect(_Data.ActualEmptyLocation));

                //string pickupDateTime = _TrackingTab.PickupDate.GetText(0);
                //string[] pickupDate = pickupDateTime.Split(',')[0].Split(' ');
                //_Data.EmptyDate = pickupDate[0];

                //string equipment = _TrackingTab.Equipment.GetText(0);
                //string[] equipmentType = equipment.Split(' ')[0].Split(',');

                //switch (equipmentType[0])
                //{
                //    case "V":
                //        _Data.EquipmentType = "Van";
                //        break;
                //    case "R":
                //        _Data.EquipmentType = "Reefer";
                //        break;
                //}

                Assert.IsTrue(_TrackingTab.EmptyDate.ClearAndEdit(_Data.EmptyDate));
                Assert.IsTrue(_TrackingTab.EmptyTime.ClearAndEdit(_Data.EmptyTime));
                Assert.IsTrue(_TrackingTab.EquipmentType.SelectByText(_Data.EquipmentType));
                Assert.IsTrue(_TrackingTab.EquipmentLength.ClearAndEdit(_Data.EquipmentLength));
                Assert.IsTrue(_TrackingTab.EquipmentWidth.ClearAndEdit(_Data.EquipmentWidth));
                Assert.IsTrue(_TrackingTab.EquipmentHeight.ClearAndEdit(_Data.EquipmentHeight));
                Assert.IsTrue(_TrackingTab.TrailerNumber.ClearAndEdit(_Data.TrailerNumber));
                try
                {
                    Assert.IsTrue(_TrackingTab.Dispatch_DispatchButton.Click());
                }
                catch
                {
                    Assert.IsTrue(_TrackingTab.Dispatch_DispatchButton.FindAndClickUsingJS());
                }
                Assert.IsTrue(_TrackingTab.DispatchDriverForm.WaitUtilDisappear());
                return "DispatchDriverSuccess";
            }
            catch
            {
                return "DispatchDriverFailed";
            }
        }

        public string VerifyDispatchFields()
        {
            try
            {
                Assert.IsTrue(_TrackingTab.Dispatch_DispatchSection.WaitUntilDisplayed());

                _Data.FirstDriver = _TrackingTab.Dispatch_DriverNameText.GetText(0);
                _Data.FirstDriverCellNumber = _TrackingTab.Dispatch_DriverPhoneText.GetText(0);
                _Data.TractorName = _TrackingTab.Dispatch_TractorNameText.GetText(0);

                Assert.IsTrue(_TrackingTab.Dispatch_UpdateDispatchButton.Click());
                Assert.IsTrue(_TrackingTab.Dispatch_DriverNameInput.IsEnabled());
                Assert.IsTrue(_TrackingTab.Dispatch_DriverPhoneInput.IsEnabled());
                Assert.IsTrue(_TrackingTab.Dispatch_TractorNameInput.IsEnabled());
                Assert.IsTrue(_TrackingTab.Dispatch_CancelButton.Click());
                Assert.IsTrue(_TrackingTab.Dispatch_CancelButton.WaitUtilDisappear());

                if (_TrackingTab.Dispatch_DriverNameText.GetText(0) == _Data.FirstDriver && _TrackingTab.Dispatch_DriverPhoneText.GetText(0) == _Data.FirstDriverCellNumber && _TrackingTab.Dispatch_TractorNameText.GetText(0) == _Data.TractorName)
                {
                    return "VerifyDispatchFieldsSuccess";
                }
                return "VerifyDispatchFieldsFailed";
            }
            catch
            {
                return "VerifyDispatchFieldsFailed";
            }
        }

        public string UpdateDispatchInfo()
        {
            try
            {
                Assert.IsTrue(_TrackingTab.Dispatch_DispatchSection.WaitUntilDisplayed());
                Assert.IsTrue(_TrackingTab.Dispatch_UpdateDispatchButton.Click());
                Assert.IsTrue(_TrackingTab.Dispatch_DriverNameInput.ClearAndEdit(_Data.FirstDriver));
                Assert.IsTrue(_TrackingTab.Dispatch_DriverPhoneInput.ClearAndEdit(_Data.FirstDriverCellNumber));
                Assert.IsTrue(_TrackingTab.Dispatch_TractorNameInput.ClearAndEdit(_Data.TractorName));
                Assert.IsTrue(_TrackingTab.Dispatch_SaveDispatchButton.Click());
                Assert.IsTrue(_TrackingTab.Dispatch_SaveDispatchButton.WaitUtilDisappear());
                return "UpdateDispatchInfoSuccess";
            }
            catch
            {
                return "UpdateDispatchInfoFailed";
            }
        }

        public string UploadDocument()
        {
            string strActualRes = null;
            try
            {
                switch (_Data.EntityName)
                {
                    case "FACTORINGCOMPANY":
                        strActualRes = UploadDocument_Factoring();
                        break;
                    default:
                        strActualRes = UploadDocument_Ignore();
                        break;
                }
                return strActualRes;
            }
            catch
            {
                return "UploadDocumentFailed";
            }
        }

        public string UploadDocument_Factoring()
        {
            try
            {
                _LoadDetailsPage.DocumentsTab_Factoring.WaitUntilDisplayed();
                _LoadDetailsPage.DocumentsTab_Factoring.Click();
                _LoadDetails_DocumentTab.NotReadyForInvoiceAlert.WaitUntilDisplayed();
                if (_LoadDetails_DocumentTab.AdditionalDocsReqText.GetText(0) == "Additional documents required.")
                {
                    IList<IWebElement> documents = _LoadDetails_DocumentTab.AdditionalDocReqList.FindElements();
                    foreach (IWebElement document in documents)
                    {
                        Assert.IsTrue(_LoadDetails_DocumentTab.SelectDocument_Factoring.WaitUntilDisplayed());
                        Assert.IsTrue(_LoadDetails_DocumentTab.SelectDocument_Factoring.Click());
                        Assert.IsTrue(_LoadDetails_DocumentTab.FileTypeBtn_Factoring.ClickByText(document.Text));
                        Thread.Sleep(Constants.Wait_Short);
                        SendKeys.SendWait(_Data.FilePath);
                        Thread.Sleep(Constants.Wait_Short);
                        SendKeys.SendWait("{ENTER}");
                    }
                }
                return "UploadDocumentSuccess";
            }
            catch
            {
                return "UploadDocumentFailed";
            }
        }

        public string UploadDocument_Ignore()
        {
            try
            {
                Assert.IsTrue(_LoadDetailsPage.DocumentsTab.WaitUntilDisplayed());
                Assert.IsTrue(_LoadDetailsPage.DocumentsTab.Click());
                Assert.IsTrue(_LoadDetails_DocumentTab.AdditionalDocsReqText.WaitUntilDisplayed());
                if (_Data.EntityName == "CARRIER" && _LoadDetails_DocumentTab.NotReadyForInvoicing.IsDisplayed())
                {
                    IList<IWebElement> documents = _LoadDetails_DocumentTab.AdditionalDocReqList.FindElements();
                    foreach (IWebElement document in documents)
                    {
                        Assert.IsTrue(_LoadDetails_DocumentTab.SelectDocumentTypeBtn.WaitUntilDisplayed());
                        Assert.IsTrue(_LoadDetails_DocumentTab.SelectDocumentTypeBtn.Click());
                        Assert.IsTrue(_LoadDetails_DocumentTab.FileTypeBtn.ClickByText(document.Text));
                        Thread.Sleep(Constants.Wait_Short);
                        SendKeys.SendWait(_Data.FilePath);
                        Thread.Sleep(Constants.Wait_Short);
                        SendKeys.SendWait("{ENTER}");
                    }
                }
                else if(_Data.EntityName == "CUSTOMER")
                {
                    Assert.IsTrue(_LoadDetailsPage.DocumentsTab.WaitUntilDisplayed());
                    Assert.IsTrue(_LoadDetailsPage.DocumentsTab.Click());
                    Assert.IsTrue(_LoadDetails_DocumentTab.SelectDocumentTypeBtn.WaitUntilDisplayed());
                    Assert.IsTrue(_LoadDetails_DocumentTab.SelectDocumentTypeBtn.Click());
                    Assert.IsTrue(_LoadDetails_DocumentTab.FileTypeBtn.ClickByText(_Data.DocumentType));
                    Thread.Sleep(Constants.Wait_Short);
                    SendKeys.SendWait(_Data.FilePath);
                    Thread.Sleep(Constants.Wait_Short);
                    SendKeys.SendWait("{ENTER}");
                }
                Thread.Sleep(Constants.Wait_Short);
                _LoadDetails_DocumentTab.UploadedDocumentBtn.FindAndClickLastElement();
                Thread.Sleep(Constants.Wait_Short);
                driver.Navigate().Back();
                return "UploadDocumentSuccess";
            }
            catch
            {
                return "UploadDocumentFailed";
            }
        }

        public string VerifyLumperLink()
        {
            try
            {
                Assert.IsTrue(_LoadDetailsPage.StopsTab.Click());
                Assert.IsTrue(_StopsTab.ReportLumperLink.WaitUntilDisplayed(30));
                try
                {
                    Assert.IsTrue(_StopsTab.ReportLumperLink.IsEnabled());
                }
                catch
                {
                    return "ReportLumperLinkDisabled";
                }
                return "VerifyLumperLinkSuccess";
            }
            catch
            {
                return "VerifyLumperLinkFailed";
            }
        }

        public string SubmitLumperAmount()
        {
            try
            {
                Assert.IsTrue(_LoadDetailsPage.StopsTab.Click());
                Assert.IsTrue(_StopsTab.ReportLumperLink.WaitUntilDisplayed());
                Assert.IsTrue(_StopsTab.ReportLumperLink.Click());
                Assert.IsTrue(_StopsTab.ReportLumperForm.WaitUntilDisplayed());
                Assert.IsTrue(_StopsTab.ReportLumperInput.WaitUntilDisplayed());
                if (_StopsTab.ReportLumperTextArea.GetText(0) != _Data.LumperText)
                {
                    return "TextNotMatched";
                }
                Assert.IsTrue(_StopsTab.ReportLumperCancelBtn.Click());
                Assert.IsTrue(_StopsTab.ReportLumperForm.WaitUtilDisappear());
                Assert.IsTrue(_StopsTab.ReportLumperLink.Click());
                Assert.IsTrue(_StopsTab.ReportLumperInput.WaitUntilDisplayed());
                Assert.IsTrue(_StopsTab.ReportLumperInput.ClearAndEdit(_Data.LumperAmount));
                Assert.IsTrue(_StopsTab.ReportLumperSubmitBtn.Click());
                Assert.IsTrue(_StopsTab.ReportLumperForm.WaitUtilDisappear());
                Assert.IsTrue(_StopsTab.ReportLumperLink.WaitUntilDisabled());
                if (_StopsTab.ReportLumperLink.GetText(0) == "Lumper Reported")
                {
                    _Data.LoadId = _LoadDetailsPage.AppTitleLoadId.GetText(0);
                    return "SubmitLumperAmountSuccess";
                }
                else
                    return "SubmitLumperAmountFailed";
            }
            catch
            {
                return "SubmitLumperAmountFailed";
            }
        }

        public string VerifyLumperTicket()
        {
            string strActualRes = null;
            try
            {
                //Verify lumper ticket is displayed in required documents section
                Assert.IsTrue(_LoadDetailsPage.DocumentsTab.WaitUntilDisplayed());
                Assert.IsTrue(_LoadDetailsPage.DocumentsTab.Click());
                Assert.IsTrue(_LoadDetails_DocumentTab.AdditionalDocsReqText.WaitUntilDisplayed());
                if (_LoadDetails_DocumentTab.AdditionalDocsReqText.GetText(0) == "Additional documents required.")
                {
                    IList<IWebElement> documents = _LoadDetails_DocumentTab.AdditionalDocReqList.FindElements();
                    IEnumerable<string> documentTypes = documents.Select(i => i.Text);
                    Assert.IsTrue(documentTypes.Contains("Lumper Ticket"));
                }

                //Verify lumper charged status from charges section
                Assert.IsTrue(_LoadDetailsPage.ChargesTab.WaitUntilDisplayed());
                Assert.IsTrue(_LoadDetailsPage.ChargesTab.Click());
                Assert.IsTrue(_ChargesTab.LineItemsContainerRows.WaitUntilDisplayed());
                IList<IWebElement> lineitems = _ChargesTab.LineItemsContainerRows.FindElements();
                foreach (IWebElement lineitem in lineitems)
                {
                    IList<IWebElement> innerelements = lineitem.FindElements(By.CssSelector("td"));
                    if (innerelements[0].Text == "Lumper Charges")
                    {
                        if (innerelements[1].Text == "Pending")
                            strActualRes = "VerifyLumperChargesPendingSuccess";
                        else if (innerelements[1].Text.StartsWith("$"))
                            strActualRes = "VerifyLumperChargesAmountSuccess";
                    }
                    else
                        strActualRes = "VerifyLumperTicketFailed";
                }
                return strActualRes;
            }
            catch
            {
                return "VerifyLumperTicketFailed";
            }
        }

        public string GenerateInvoice()
        {
            try
            {
                if (_Data.EntityName.ToUpper() == "FACTORINGCOMPANY")
                {
                    Assert.IsTrue(_LoadDetailsPage.ChargesTab_Factoring.Click());
                    Assert.IsTrue(_ChargesTab.GenerateInvoiceSectionFactoring.WaitUntilDisplayed());
                    Assert.IsTrue(_ChargesTab.GenerateInvoiceButtonFactoring.WaitUntilDisplayed());
                    Assert.IsTrue(_ChargesTab.ApproveRatesCheckboxFactoring.Click());
                    Assert.IsTrue(_ChargesTab.GenerateInvoiceButtonFactoring.Click());
                    Assert.IsFalse(_ChargesTab.GenerateInvoiceButtonFactoring.WaitUtilEnabled());
                    _LoadDetailsPage.ChargesTab_Factoring.Click();
                }
                else
                {
                    Assert.IsTrue(_LoadDetailsPage.ChargesTab.Click());
                    Assert.IsTrue(_ChargesTab.GenerateInvoiceSection.WaitUntilDisplayed());
                    Assert.IsTrue(_ChargesTab.GenerateInvoiceButton.WaitUntilDisplayed());
                    Assert.IsTrue(_ChargesTab.ApproveRatesCheckbox.Click());
                    Assert.IsTrue(_ChargesTab.StdPaymentTerms.StatusCheckORPerFormAction(_Data.StdPaymentTerms));
                    Assert.IsTrue(_ChargesTab.RequestFinalAdvance.StatusCheckORPerFormAction(_Data.ReqFinalAdvance));
                    Assert.IsTrue(_ChargesTab.GenerateInvoiceButton.Click());
                    Assert.IsFalse(_ChargesTab.GenerateInvoiceButton.WaitUtilEnabled());
                    _Data.LoadId = _LoadDetailsPage.AppTitleLoadId.GetText(0);
                    _LoadDetailsPage.ChargesTab.Click();
                }
                return "GenerateInvoiceSuccess";
            }
            catch
            {
                return "GenerateInvoiceFailed";
            }
        }

        public string VerifyGenerateInvoice()
        {
            try
            {
                if (_Data.EntityName == "FACTORINGCOMPANY")
                {
                    Assert.IsTrue(_LoadDetailsPage.ChargesTab_Factoring.Click());
                    Assert.IsTrue(_ChargesTab.GenerateInvoiceSectionFactoring.WaitUntilDisplayed());
                    Assert.IsTrue(_ChargesTab.GenerateInvoiceButtonFactoring.WaitUntilDisplayed());
                    Assert.IsTrue(_ChargesTab.ApproveRatesCheckboxFactoring.WaitUntilDisplayed());
                    Assert.IsTrue(_ChargesTab.ApproveRatesCheckboxFactoring.Click());
                    Assert.IsTrue(_ChargesTab.GenerateInvoiceButtonFactoring.WaitUtilEnabled());
                }
                else
                {
                    Assert.IsTrue(_LoadDetailsPage.ChargesTab.Click());
                    Assert.IsTrue(_ChargesTab.GenerateInvoiceSection.WaitUntilDisplayed());
                    Assert.IsTrue(_ChargesTab.GenerateInvoiceButton.WaitUntilDisplayed());
                    Assert.IsTrue(_ChargesTab.ApproveRatesCheckbox.WaitUntilDisplayed());
                    Assert.IsTrue(_ChargesTab.StdPaymentTerms.WaitUntilDisplayed());
                    Assert.IsTrue(_ChargesTab.RequestFinalAdvance.WaitUntilDisplayed());
                    Assert.IsTrue(_ChargesTab.ApproveRatesCheckbox.Click());
                    Assert.IsTrue(_ChargesTab.GenerateInvoiceButton.WaitUtilEnabled());
                }
                
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
        }

        public string VerifyAlerts()
        {
            //to do: add condition or entity types
            UIItem AlertElement = null;
            UIItem AlertTextElement = null;
            try
            {
                switch (_Data.InvoiceStatus)
                {
                    case "Awaiting Approval":
                        AlertElement = _ChargesTab.AwaitingApprovalAlert;
                        AlertTextElement = _ChargesTab.AwaitingApprovalText;
                        break;
                    case "In Process":
                        AlertElement = _ChargesTab.InProcessAlert;
                        AlertTextElement = _ChargesTab.InProcessText;
                        break;
                    case "Partial Payment":
                        AlertElement = _ChargesTab.PartialPaymentAlert;
                        AlertTextElement = _ChargesTab.PartialPaymentText;
                        break;
                    case "Paid":
                        AlertElement = _ChargesTab.PaidAlert;
                        AlertTextElement = _ChargesTab.PaidText;
                        break;
                }
                Thread.Sleep(Constants.Wait_Medium);
                Assert.IsTrue(AlertElement.WaitUntilDisplayed());
                if (AlertTextElement.GetText() == _Data.AlertText)
                {
                    try
                    {
                        _LoadDetailsPage.ChargesTab_Factoring.Click();
                        _LoadDetailsPage.TopTab_Factoring.Click();
                    }
                    catch
                    {

                    }
                    return "VerifyAlertsSuccess";
                }
                else
                    return "VerifyAlertsFailed";
            }
            catch
            {
                return "VerifyAlertsFailed";
            }
        }

        public string VerifyTrackingNotes()
        {
            try
            {
                Assert.IsTrue(_LoadDetailsPage.TrackingTab.WaitUntilDisplayed());
                Assert.IsTrue(_LoadDetailsPage.TrackingTab.Click());
                Assert.IsTrue(_TrackingTab.TrackingNotesRegion.WaitUntilDisplayed());
                if (_Data.TrackingNotesAction!= "!IGNORE!" && _TrackingTab.TrackingNotes_1stRow_Action.GetText() != _Data.TrackingNotesAction)
                    return "VerificationFailed";
                if (_Data.TrackingNotesCarrier != "!IGNORE!" && !_TrackingTab.Trackingnotes_1strow_carrier.GetText().Contains(_Data.TrackingNotesCarrier))
                    return "VerificationFailed";
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
        }

        #endregion

        #region UIverify
        //This function assume that non- factorting company the load details page is open already.
        public string UIVerify()
        {
            try
            {
                LoadDetailsUIVerifyData LoadDetailsUIVerifyData = new LoadDetailsUIVerifyData(_datamanager);
                if(LoadDetailsUIVerifyData.LoadDetails_Tab.ToUpper() == "LOADDETAILS_TRACKING")
                    Assert.IsTrue(UIVerify_TrackingDetails(LoadDetailsUIVerifyData));
                if(LoadDetailsUIVerifyData.LoadDetails_Tab.ToUpper().Contains("LOADDETAILS_STOPS"))
                    Assert.IsTrue(UIVerify_Stops(LoadDetailsUIVerifyData));
                if (LoadDetailsUIVerifyData.LoadDetails_Tab.ToUpper() == "LOADDETAILS_DOCUMENTS")
                    Assert.IsTrue(UIVerify_Documents(LoadDetailsUIVerifyData));
                if (LoadDetailsUIVerifyData.LoadDetails_Tab.ToUpper() == "LOADDETAILS_CHARGES")
                    Assert.IsTrue(UIVerify_Charges(LoadDetailsUIVerifyData));
                if (LoadDetailsUIVerifyData.LoadDetails_Tab.ToUpper() == "LOADDETAILS_TENDERSTATUS")
                    Assert.IsTrue(UIVerify_TenderStatus(LoadDetailsUIVerifyData));
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
        }

        private bool UIVerify_TenderStatus(LoadDetailsUIVerifyData loadDetailsUIVerifyData)
        {
            try
            {
                NavigateTo_TenderStatusTab();
                Assert.IsTrue(_TenderStatusTab.TenderStatus_Row.UIVerify(loadDetailsUIVerifyData.TenderStatus_Row));
                Assert.IsTrue(_TenderStatusTab.TenderedLoadAlert.GetText().Contains("This is the first time") ||
                                _TenderStatusTab.TenderedLoadAlert.GetText().Contains("This lane has gone"));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool UIVerify_Stops(LoadDetailsUIVerifyData LoadDetailsUIVerifyData)
        {
            try
            {
                //Stops
                NavigateTo_StopTab();
                if (LoadDetailsUIVerifyData.LoadDetails_Tab.ToUpper() == "LOADDETAILS_STOPSPICKUP")
                    Assert.IsTrue(UIVerify_DeliveryDetails(LoadDetailsUIVerifyData));
                if (LoadDetailsUIVerifyData.LoadDetails_Tab.ToUpper() == "LOADDETAILS_STOPSDELIVERY")
                    Assert.IsTrue(UIVerify_PickupDetails(LoadDetailsUIVerifyData));
                Assert.IsTrue(UIVerify_LumberReported(LoadDetailsUIVerifyData));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool UIVerify_LumberReported(LoadDetailsUIVerifyData LoadDetailsUIVerifyData)
        {
            try
            {
                Assert.IsTrue(_StopsTab.ReportLumperLink.UIVerify(LoadDetailsUIVerifyData.Report_Lumper));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool UIVerify_DeliveryDetails(LoadDetailsUIVerifyData LoadDetailsUIVerifyData)
        {
            try
            {   
                //Delivery
                _StopsTab.Delivery_Update.Click();
                _StopsTab.WaitUntilLoading();
                if (!_StopsTab.ArrivalDate.IsDisplayed(10))
                {
                Assert.IsTrue(_StopsTab.ArrivalDate.UIVerify(LoadDetailsUIVerifyData.Pickup_ArrivalDate));
                Assert.IsTrue(_StopsTab.ArrivalTime.UIVerify(LoadDetailsUIVerifyData.Pickup_ArrivalTime));
                Assert.IsTrue(_StopsTab.DepartureDate.UIVerify(LoadDetailsUIVerifyData.Delivery_DepartureDate));
                Assert.IsTrue(_StopsTab.DepartureTime.UIVerify(LoadDetailsUIVerifyData.Delivery_DepartureTime));
                }
                // Object desciption is changing based on type of load progress = 'Pending'
                else
                {
                    Assert.IsTrue(_StopsTab.Delivery_ArrivalDate_Disabled.UIVerify(LoadDetailsUIVerifyData.Delivery_ArrivalDate));
                    Assert.IsTrue(_StopsTab.Delivery_ArrivalTime_Disabled.UIVerify(LoadDetailsUIVerifyData.Delivery_ArrivalTime));
                    Assert.IsTrue(_StopsTab.Delivery_DepartureDate_Disabled.UIVerify(LoadDetailsUIVerifyData.Delivery_DepartureDate));
                    Assert.IsTrue(_StopsTab.Delivery_DepartureTime_Disabled.UIVerify(LoadDetailsUIVerifyData.Delivery_DepartureTime));
                }
                _StopsTab.Stop_UpdateCancel.Click();
                _StopsTab.WaitUntilLoading();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool UIVerify_PickupDetails(LoadDetailsUIVerifyData LoadDetailsUIVerifyData)
        {
            try
            {
                //Pickup
                _StopsTab.Pickup_Update.Click();
                _StopsTab.WaitUntilLoading();
                if (!_StopsTab.ArrivalDate.IsDisplayed(10))
                {
                Assert.IsTrue(_StopsTab.ArrivalDate.UIVerify(LoadDetailsUIVerifyData.Pickup_ArrivalDate));
                Assert.IsTrue(_StopsTab.ArrivalTime.UIVerify(LoadDetailsUIVerifyData.Pickup_ArrivalTime));
                Assert.IsTrue(_StopsTab.DepartureDate.UIVerify(LoadDetailsUIVerifyData.Pickup_DepartureDate));
                Assert.IsTrue(_StopsTab.DepartureTime.UIVerify(LoadDetailsUIVerifyData.Pickup_DepartureTime));
                }
                // Object desciption is changing based on type of load progress = 'Pending'
                else
                {
                    Assert.IsTrue(_StopsTab.Pickup_ArrivalDate_Disabled.UIVerify(LoadDetailsUIVerifyData.Pickup_ArrivalDate));
                    Assert.IsTrue(_StopsTab.Pickup_ArrivalTime_Disabled.UIVerify(LoadDetailsUIVerifyData.Pickup_ArrivalTime));
                    Assert.IsTrue(_StopsTab.Pickup_DepartureDate_Disabled.UIVerify(LoadDetailsUIVerifyData.Pickup_DepartureDate));
                    Assert.IsTrue(_StopsTab.Pickup_DepartureTime_Disabled.UIVerify(LoadDetailsUIVerifyData.Pickup_DepartureTime));
                }
                _StopsTab.Stop_UpdateCancel.Click();
                _StopsTab.WaitUntilLoading();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool UIVerify_TrackingDetails(LoadDetailsUIVerifyData LoadDetailsUIVerifyData)
        {
            try
            {
                NavigateTo_TrackingTab();
                _TrackingTab.WaitUntilLoading();
                Assert.IsTrue(_LoadDetails_SummaryDetails.DispatchDetail_Message.UIVerify(LoadDetailsUIVerifyData.DispatchDetail_Message));
                if (LoadDetailsUIVerifyData.EntityType.ToUpper() == Constants.Entity_Customer)
                {
                    //Summary
                    Assert.IsTrue(_LoadDetails_SummaryDetails.Customer_SummaryDetails_Progress.UIVerify(LoadDetailsUIVerifyData.LoadProgress));
                    //Dispatch
                    Assert.IsTrue(_LoadDetails_DispatchPanel.Customer_Dispatch_Carrier.UIVerify(LoadDetailsUIVerifyData.Dispatch_CarrierName));
                    Assert.IsTrue(_LoadDetails_DispatchPanel.Customer_Dispatch_ProNo.UIVerify(LoadDetailsUIVerifyData.Dispatch_ProNo));
                    Assert.IsTrue(_LoadDetails_DispatchPanel.Customer_Dispatch_Tractor.UIVerify(LoadDetailsUIVerifyData.Dispatch_TractorNameText));
                    Assert.IsTrue(_LoadDetails_DispatchPanel.Customer_Dispatch_Trailer.UIVerify(LoadDetailsUIVerifyData.Dispatch_Trailer));

                }
                if (LoadDetailsUIVerifyData.EntityType.ToUpper() == Constants.Entity_Carrier)
                {
                    //Summary
                    Assert.IsTrue(_LoadDetails_SummaryDetails.Carrier_SummaryDetails_Progress.UIVerify(LoadDetailsUIVerifyData.LoadProgress));
                    //Dispatch
                    Assert.IsTrue(_TrackingTab.Dispatch_DispatchDriverButton.UIVerify(LoadDetailsUIVerifyData.Dispatch_DispatchDriverButton));
                    Assert.IsTrue(_LoadDetails_DispatchPanel.Carrier_Dispatch_DriverName.UIVerify(LoadDetailsUIVerifyData.Dispatch_DriverNameText));
                    Assert.IsTrue(_LoadDetails_DispatchPanel.Carrier_Dispatch_DriverPhone.UIVerify(LoadDetailsUIVerifyData.Dispatch_DriverPhoneText));
                    Assert.IsTrue(_LoadDetails_DispatchPanel.Carrier_Dispatch_Tractor.UIVerify(LoadDetailsUIVerifyData.Dispatch_TractorNameText));
                }
                //Tracking Notes Table
                Assert.IsTrue(_TrackingTab.TrackingNotes_1stRow_Action.UIVerify(LoadDetailsUIVerifyData.TrackingNotes_1stRow_Action));
                Assert.IsTrue(_TrackingTab.TrackingNotes_1stRow_Notes.UIVerify(LoadDetailsUIVerifyData.TrackingNotes_1stRow_Notes));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool UIVerify_Documents(LoadDetailsUIVerifyData LoadDetailsUIVerifyData)
        {
            try
            {
                NavigateTo_DocumentsTab();
                _LoadDetails_DocumentTab.RequiredDocusments.UIVerify(LoadDetailsUIVerifyData.Documents_RequiredDocs);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool UIVerify_Charges(LoadDetailsUIVerifyData LoadDetailsUIVerifyData)
        {
            try
            {
                NavigateTo_ChargesTab();
                List<int> index  = _ChargesTab.ChargesTable_ChargesCol.GetElementIndex(LoadDetailsUIVerifyData.Charges_Type);
                if(_ChargesTab.ChargesTable_AmountCol.GetText(index[0]).ToUpper() == LoadDetailsUIVerifyData.Charges_Amount.ToUpper())
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Navigation Functions
        private bool NavigateTo_TrackingTab()
        {
            try
            {
                if (_LoadDetailsPage.TrackingTab_TrackingNotes.IsDisplayed())
                    return true;
                Assert.IsTrue(_LoadDetailsPage.TrackingTab.WaitUntilDisplayed(10));
                Assert.IsTrue(_LoadDetailsPage.TrackingTab.Click());
                _LoadDetailsPage.WaitUntilLoading();
                Assert.IsTrue(_LoadDetailsPage.TrackingTab_TrackingNotes.WaitUntilDisplayed(10));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool NavigateTo_StopTab()
        {
            try
            {
                if (_LoadDetailsPage.StopsTab_Stops.IsDisplayed())
                    return true;
                Assert.IsTrue(_LoadDetailsPage.StopsTab.WaitUntilDisplayed(10));
                Assert.IsTrue(_LoadDetailsPage.StopsTab.Click());
                _LoadDetailsPage.WaitUntilLoading();
                Assert.IsTrue(_LoadDetailsPage.StopsTab_Stops.WaitUntilDisplayed(10));
                return true;
            }
            catch
            {
               return false;
            }
        }

        private bool NavigateTo_DocumentsTab()
        {
            try
            {
                Assert.IsTrue(_LoadDetailsPage.DocumentsTab.WaitUntilDisplayed(10));
                Assert.IsTrue(_LoadDetailsPage.DocumentsTab.Click());
                _LoadDetailsPage.WaitUntilLoading();
                Assert.IsTrue(_LoadDetailsPage.DocumentsTab_Documents.WaitUntilDisplayed(10));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool NavigateTo_ChargesTab()
        {
            try
            {
                Assert.IsTrue(_LoadDetailsPage.ChargesTab.WaitUntilDisplayed(10));
                Assert.IsTrue(_LoadDetailsPage.ChargesTab.Click());
                _LoadDetailsPage.WaitUntilLoading();
                Assert.IsTrue(_LoadDetailsPage.ChargersTab_Chargers.WaitUntilDisplayed(10));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool NavigateTo_TenderStatusTab()
        {
            try
            {
                Assert.IsTrue(_LoadDetailsPage.TenderStatusTab.WaitUntilDisplayed(10));
                Assert.IsTrue(_LoadDetailsPage.TenderStatusTab.Click());
                _LoadDetailsPage.WaitUntilLoading();
                Assert.IsTrue(_LoadDetailsPage.TenderStatusTab_TenderStatus.WaitUntilDisplayed(10));
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
