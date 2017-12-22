using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace SWAT.Applications.Claw
{
    using SWAT.Applications.Claw.DAL;
    using SWAT.Data;
    using SWAT.Logger;
    using SWAT.FrameWork.Utilities;
    using SWAT.Configuration;
    using SWAT.Applications.Claw.ObjectRepository;

    public class Tender 
    {
        private TenderData _Data;
        private TenderPage _TenderPage;

        public Tender(TestStartInfo teststartinfo, DataManager datamanager)
        {
            _Data = new TenderData(datamanager);
            _TenderPage = new TenderPage(teststartinfo);
        }

        public string AcceptTender()
        {
            string actualresult = null;
            try
            {
                actualresult = GoToAcceptTender();
                if (actualresult == "NavigationFailed") 
                    return actualresult;
                actualresult = VerifyTenderedLoadPresent();
                if (actualresult != "TenderedLoadPresent")
                    return actualresult;
                int LoadIDIndex = _TenderPage.LoadIDs.GetOneElementIndex("#"+_Data.LoadID);
                if (LoadIDIndex == -1)
                {
                    return "FailedToAcceptTender";
                }
                if (_Data.AcceptDate != "!IGNORE!")
                {
                    Assert.IsTrue(_TenderPage.AcceptByDateBtns.Click(LoadIDIndex));
                    _TenderPage.WaitUntilSaving();
                    _TenderPage.WaitUntilLoading();
                    //Verify the acceptload success
                    LoadIDIndex = _TenderPage.LoadIDs.GetOneElementIndex("#" + _Data.LoadID);
                    if (LoadIDIndex != -1)
                    {
                        return "FailedToAcceptTender";
                    }
                }
                else
                {
                    if(_Data.LoadType.ToUpper() == "SPOT")
                    {
                        Assert.IsTrue(_TenderPage.SpotOfferNotes.ClearAndEditByIndex(_Data.SpotOfferNotes, LoadIDIndex));
                        Assert.IsTrue(_TenderPage.SpotOfferRate.ClearAndEditByIndex(_Data.SpotOfferRate, LoadIDIndex));
                    }
                    Assert.IsTrue(_TenderPage.AcceptBtn.Click(LoadIDIndex));                    
                    _TenderPage.WaitUntilSaving();
                    _TenderPage.WaitUntilLoading();
                    //Verify the acceptload success
                    LoadIDIndex = _TenderPage.LoadIDs.GetOneElementIndex("#" + _Data.LoadID);
                    if (LoadIDIndex != -1)
                    {
                        return "FailedToAcceptTender";
                    }
                }
                return "TenderAccepted";
            }
            catch
            {
                return "UnableToAccept";
            }
        }

        public string AcceptTender_01()
        {
            string actualresult = null;
            try
            {
                actualresult = GoToAcceptTender();
                if (actualresult == "NavigationFailed")
                    return actualresult;
                Assert.IsTrue(_TenderPage.AcceptBtn.WaitUntilDisplayed());
                Assert.IsTrue(_Data.LoadNotes == _TenderPage.Notes.GetText());
                Assert.IsTrue(_TenderPage.AcceptBtn.Click());
                System.Threading.Thread.Sleep(60000);
                return "TenderAccepted";
            }
            catch
            {
                return "UnableToAccept";
            }
        }

        private int GetTenderdedLoadIndex()
        {
            try
            {
                int index = _TenderPage.LoadIDs.GetOneElementIndex(_Data.LoadID);
                return 0;
            }
            catch
            {
                return 0;
            }
        }
        
        private string VerifyTenderedLoadPresent()
        {
            try
            {
                if (_TenderPage.NoLoadAvilableMsg.WaitUntilDisplayed(10))
                {
                    //ToDo: Refresh and wait for 2 mins.
                    //ToDo: Can be changed based on expected result
                    return "NoTenderedLoads";
                }
                if (!_TenderPage.LoadIDs.WaitUntilDisplayed())
                {
                    //ToDo: Refresh and wait for 2 mins.
                    //ToDo: Can be changed based on expected result
                    return "NoTenderedLoads";
                }
                return "TenderedLoadPresent";
            }
            catch
            {
                return null;
            }
        }

        public string GoToAcceptTender()
        {
            try
            {
                if(_TenderPage.Navigate())
                {
                    if (!_TenderPage.AppTile.WaitUntilDisplayed(20))
                    {
                        return "NavigationFailed";
                    }
                    return "NavigationSuccess";
                }
                return "NavigationFailed";
            }
            catch
            {
                return "NavigationFailed";
            }
        }

        public string AcceptSpotOffer()
        {
            try
            {
                Assert.IsTrue(_TenderPage.LoadSummary.WaitUntilDisplayed());
                Assert.IsTrue(_TenderPage.TenderHistoryContainer.Click());
                Assert.IsTrue(_TenderPage.SpotQuotes.WaitUntilDisplayed());
                int index = _TenderPage.SpotQuotesResultsRows.GetOneElementIndex(_Data.CarrierName);
                if (_Data.VerifyCarrier.ToUpper() == "TRUE")
                {
                    if (index == -1)
                    {
                        return "AcceptOfferFailed";
                    }
                    else
                    {
                        return "AcceptOfferSuccess";
                    }
                }
                Assert.IsTrue(_TenderPage.BookLoad.Click(index+1));
                return "AcceptOfferSuccess";
            }
            catch
            {
                return "AcceptOfferFailed";
            }
        }
    }
}
