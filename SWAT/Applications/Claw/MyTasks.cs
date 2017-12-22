using ClosedXML.Excel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Xml;

namespace SWAT.Applications.Claw
{
    using SWAT.Applications.Claw.DAL;
    using SWAT.Applications.Claw.ObjectRepository;
    using SWAT.Configuration;
    using SWAT.Data;
    using SWAT.Logger;
    using SWAT.FrameWork.Utilities;

    class MyTasks
    {
        MyTasksData _MyTasksData;
        MyTasksPage _MyTasksPage;

        public MyTasks(TestStartInfo teststartinfo, DataManager datamanager)
        {
            _MyTasksData = new MyTasksData(datamanager);
            _MyTasksPage = new MyTasksPage(teststartinfo);
        }

        public string NavigateToConfirmLoads()
        {
            try
            {
                Assert.IsTrue(_MyTasksPage.ConfirmLoadsTab.Click());
                Assert.IsTrue(_MyTasksPage.ConfirmLoadsPanel.WaitUntilDisplayed());
                return "NavigationSuccess";
            }
            catch
            {
                return "NavigationFailed";
            }

        }

        public string NavigateToAccounting()
        {
            try
            {
                Assert.IsTrue(_MyTasksPage.Navigate());
                _MyTasksPage.WaitUntilLoading();
                Assert.IsTrue(_MyTasksPage.AccountingTab.WaitUntilDisplayed());
                Assert.IsTrue(_MyTasksPage.AccountingTab.Click());
                Assert.IsTrue(_MyTasksPage.AccountingPanel.WaitUntilDisplayed());
                return "NavigationSuccess";
            }
            catch
            {
                return "NavigationFailed";
            }
        }

        public string NavigateToTracking()
        {
            try
            {
                Assert.IsTrue(_MyTasksPage.Navigate());
                _MyTasksPage.WaitUntilLoading();
                Assert.IsTrue(_MyTasksPage.TrackingTab.WaitUntilDisplayed());
                Assert.IsTrue(_MyTasksPage.TrackingTab.Click());
                Assert.IsTrue(_MyTasksPage.LoadsTracking.WaitUntilDisplayed());
                return "NavigationSuccess";
            }
            catch
            {
                return "NavigationFailed";
            }
        }

        public string GetAccountingLoadsCount()
        {
            try
            {
                Assert.IsTrue(_MyTasksPage.AccountingTotalLoads.WaitUntilDisplayed());
                _MyTasksData.AccountingLoadsCount = _MyTasksPage.AccountingTotalLoads.GetText();
                return "DataCopied";
            }
            catch
            {
                return "DataCopyFailed";
            }
        }

        public string CompareAccountingLoadsCount()
        {
            try
            {
                int earlierAmount = Convert.ToInt32(_MyTasksData.AccountingLoadsCount);
                int reducedBy = Convert.ToInt32(_MyTasksData.ReducedAmount);
                if (Convert.ToInt32(_MyTasksPage.AccountingTotalLoads.GetText()).Equals(earlierAmount - reducedBy))
                {
                    return "Success";
                }
                return "Fails";
            }
            catch
            {
                return "Success";
            }
        }

        public string VerifyReducedMissingDocuments()
        {
            try
            {
                AccountingRow accountingRows = _MyTasksPage.AccountingRows.Where(x => x.LoadID.GetText().Equals(_MyTasksData.LoadId)).FirstOrDefault();
                Assert.IsTrue(accountingRows.LoadID.Click());
                Thread.Sleep(Constants.Wait_Short);
                if (_MyTasksPage.MissingDocumentsList.FindElements().Count == (Convert.ToInt32(_MyTasksData.NoOfmissingDocuments) - Convert.ToInt32(_MyTasksData.ReducedAmount)))
                {
                    return "Success";
                }
                return "Fails";
            }
            catch
            {
                return "Fails";
            }
        }

        public string OpenLoadWithMissingDocument()
        {
            try
            {
                List<AccountingRow> accountingRows = _MyTasksPage.AccountingRows.ToList();
                bool openLoadDetails = (_MyTasksData.OpenLoadDetails.ToUpper() == "TRUE") ? true : false;
                foreach (AccountingRow ar in accountingRows)
                {
                    if (_MyTasksData.NoOfmissingDocuments.ToUpper() != "!IGNORE!" && Convert.ToInt32(_MyTasksData.NoOfmissingDocuments) > 1)
                    {
                        if (ar.Remark.GetText().Contains("and"))
                        {
                            Assert.IsTrue(OpenLoadWithMissingDocuments(ar, openLoadDetails));
                            return "LoadOpenSuccess";
                        }
                    }
                    else if(ar.Remark.GetText().Contains("missing"))
                    {
                        Assert.IsTrue(OpenLoadWithMissingDocuments(ar, openLoadDetails));
                        return "LoadOpenSuccess";
                    }
                }
                return "LoadOpenFails";
            }
            catch
            {
                return "LoadOpenFails";
            }
        }
        
        public string GetConfirmLoadsCount()
        {
            try
            {
                Assert.IsTrue(_MyTasksPage.ConfirmLoadsCount.WaitUntilDisplayed());
                _MyTasksData.ConfirmLoadsCount = _MyTasksPage.ConfirmLoadsCount.GetText();
                return "DataCopied";
            }
            catch
            {
                return "DataCopyFailed";
            }
        }

        public string OpenConfirmLoad()
        {
            try
            {
                Assert.IsTrue(_MyTasksPage.ConfirmLoadsList.WaitUntilDisplayed());
                Assert.IsTrue(_MyTasksPage.ConfirmLoadsList.FindAndClickFirstElement());
                Assert.IsTrue(_MyTasksPage.ConfirmLoadDriverName.WaitUntilDisplayed());
                Assert.IsTrue(_MyTasksPage.SidePanelHeader.GetText().Equals("Confirm Load"));
                return "ConfirmLoadOpenSuccess";
            }
            catch
            {
                return "ConfirmLoadOpenFailed";
            }
        }

        public string ConfirmLoadFill()
        {
            try
            {
                Assert.IsTrue(_MyTasksPage.SidePanelHeader.GetText().Equals("Confirm Load"));
                Assert.IsTrue(_MyTasksPage.ConfirmLoadDriverName.WaitUntilDisplayed());
                Assert.IsTrue(_MyTasksPage.ConfirmLoadDriverName.ClearAndEdit(_MyTasksData.DriverName));
                Assert.IsTrue(_MyTasksPage.ConfirmLoadDriverPhone.ClearAndEdit(_MyTasksData.DriverPhone));
                Assert.IsTrue(_MyTasksPage.ConfirmLoadEmptyLocation.TypeAndSelect(_MyTasksData.EmptyLocation));
                Assert.IsTrue(_MyTasksPage.ConfirmLoadEmptyDate.ClearAndEdit(_MyTasksData.EmptyDate));
                Assert.IsTrue(_MyTasksPage.ConfirmLoadEmptyTime.ClearAndEdit(_MyTasksData.EmptyTime));
                return "ConfirmLoadFillSuccess";
            }
            catch
            {
                return "ConfirmLoadFillFailed";
            }
        }

        public string ConfirmLoadSubmit()
        {
            try
            {
                Assert.IsTrue(_MyTasksPage.SidePanelHeader.GetText().Equals("Confirm Load"));
                Assert.IsTrue(_MyTasksPage.ConfirmLoadDriverName.WaitUntilDisplayed());
                Assert.IsTrue(_MyTasksPage.ConfirmLoadConfirmButton.Click());
                return "ConfirmLoadSubmitSuccess";
            }
            catch
            {
                return "ConfirmLoadSubmitFailed";
            }
        }

        public string ConfirmLoadCancel()
        {
            try
            {
                Assert.IsTrue(_MyTasksPage.SidePanelHeader.GetText().Equals("Confirm Load"));
                Assert.IsTrue(_MyTasksPage.ConfirmLoadDriverName.WaitUntilDisplayed());
                Assert.IsTrue(_MyTasksPage.ConfirmLoadCancelButton.Click());
                return "ConfirmLoadCancelSuccess";
            }
            catch
            {
                return "ConfirmLoadCancelFailed";
            }
        }


        public string VerifyConfirmLoadCountDecrease()
        {
            try
            {
                Assert.IsTrue(_MyTasksPage.ConfirmLoadsCount.WaitUntilDisplayed());
                Assert.IsTrue(Convert.ToInt32(_MyTasksData.ConfirmLoadsCount) < Convert.ToInt32(_MyTasksPage.ConfirmLoadsCount.GetText()));
                return "CountDecreaseVerificationSuccess";
            }
            catch
            {
                return "CountDecreaseVerificationFailed";
            }
        }

        public string GetTotalMissingDocuments()
        {
            try
            {
                _MyTasksData.TotalMissingDocs = _MyTasksPage.MissingDocumentsList.FindElements().ToString();
                return "Copied";
            }
            catch
            {
                return "Failed";
            }
            
        }

        private bool OpenLoadWithMissingDocuments(AccountingRow ar, bool openLoadDetails)
        {
            try
            {
                _MyTasksData.LoadId = ar.LoadID.GetText();
                Assert.IsTrue(ar.LoadID.Click());
                Assert.IsTrue(_MyTasksPage.LoadIdInSummary.WaitUntilDisplayed());
                if(openLoadDetails)
                {
                    Assert.IsTrue(_MyTasksPage.LoadIdInSummary.Click());
                    Assert.IsTrue(_MyTasksPage.LoadDetailsDocumentsPanel.WaitUntilDisplayed());
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetCallBackOfLoad()
        {
            try
            {
                TrackingRow trackingRow = _MyTasksPage.TrackingRows.Where(x => x.LoadID.GetText().Equals(_MyTasksData.LoadId)).FirstOrDefault();
                _MyTasksData.CallBack = trackingRow.CallBackTime.GetText();
                return "DataCopied";
            }
            catch
            {
                return "CopyFailed";
            }
        }

        public string VerifyCallBackTime()
        {
            try
            {
                TrackingRow trackingRow = _MyTasksPage.TrackingRows.Where(x => x.LoadID.GetText().Equals(_MyTasksData.LoadId)).FirstOrDefault();
                if (_MyTasksData.VerifyFor.ToUpper() == "EQUALITY")
                {
                    if (trackingRow.CallBackTime.GetText() == _MyTasksData.CallBack)
                    {
                        return "VerifySuccess";
                    }
                }
                else
                {
                    if (_MyTasksData.CallBack != "!IGNORE!" && trackingRow.CallBackTime.GetText() != _MyTasksData.CallBack)
                    {
                        return "VerifySuccess";
                    }
                }
                return "VerifyFails";
            }
            catch
            {
                return "VerifyFails";
            } 
        }

        public string OpenTrackingLoadDetails()
        {
            try
            {
                TrackingRow trackingRow = _MyTasksPage.TrackingRows.Where(x => x.LoadID.GetText().Equals(_MyTasksData.LoadId)).FirstOrDefault();
                Assert.IsTrue(trackingRow.LoadID.Click());
                Assert.IsTrue(_MyTasksPage.LoadIdInStopSummary.Click());
                Assert.IsTrue(_MyTasksPage.SummaryRegion.WaitUntilDisplayed());
                return "Success";
            }
            catch
            {
                return "Fails";
            }
        }
    }
}
