using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualBasic;
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

    public class ScheduleLoads
    {
        private ScheduleLoadsData _Data;
        private ScheduleLoadsPage _ScheduleLoadsPage;

        public ScheduleLoads(TestStartInfo info, DataManager datamanager)
        {
            _Data = new ScheduleLoadsData(datamanager);
            //_ScheduleLoadsPage = new ScheduleLoadsPage(info.Driver);
            _ScheduleLoadsPage = new ScheduleLoadsPage(info);
        }

        private bool Navigate()
        {
            try
            {
                Assert.IsTrue(_ScheduleLoadsPage.Navigate());
                _ScheduleLoadsPage.txt_LoadSearchInput.WaitUntilDisplayed();
                Assert.IsTrue(_ScheduleLoadsPage.txt_LoadSearchInput.WaitUntilDisplayed(10));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string Search()
        {
            Navigate();
            // Enter the text into the field
            _ScheduleLoadsPage.txt_LoadSearchInput.Edit(_Data.LoadID);

            // Click on the search result
            _ScheduleLoadsPage.cbx_SearchResults.Click();

            // Wait until displayed
            _ScheduleLoadsPage.grp_SearchResultsDisplay.WaitUntilDisplayed();

            if (_ScheduleLoadsPage.grp_SearchResultsDisplay.IsDisplayed())
            {
                string getLoadinfo;
                int intCompare = -1;
                

                if (_ScheduleLoadsPage.lbl_ResultsLoadInfoHeader.WaitUntilDisplayed())
                {
                    getLoadinfo = _ScheduleLoadsPage.lbl_ResultsLoadInfoHeader.GetText();

                    string[] aryLoadInfoHeader = getLoadinfo.Split(' ');
                    int intVerifyAnchorElementsCount = 0;
                    // Anchor element #1
                    foreach (string strItem in aryLoadInfoHeader)
                    {
                        intCompare = strItem.IndexOf(_Data.LoadID);
                        if (intCompare == 0)
                        {
                            intVerifyAnchorElementsCount = +1;
                            break;
                        }
                        else
                        { //Unable to match the text
                            MyLogger.Log("Verification failed for loadID" + _Data.LoadID + ".");
                            return "VerifyTextFailed";
                        }
                    }

                }
                else
                {
                    return "VerifyElementFailed";
                } //lbl_ResultsLoadInfoHeader failed

                // Anchor element #2
                if (!_ScheduleLoadsPage.tbl_SearchResults.IsDisplayed())
                { return "VerifyElementFailed"; } //tbl_SearchResults failed

                MyLogger.Log("Verify anchor elements successful");
                return "SearchSuccess";


            }

            MyLogger.Log("Search for load ID failed. Verification of anchor elements failed for the load ID" + _Data.LoadID + ". Verify Test Data or Screenshot for more details.");
            return "SearchFailed";

        }

        public string UpdateDate()
        {
            try
            {
                Assert.IsTrue(_ScheduleLoadsPage.PickupDate.WaitUntilDisplayed());
                Assert.IsTrue(_ScheduleLoadsPage.PickupDate.ClearAndEdit(_Data.Pickup_Date));
                _ScheduleLoadsPage.Outside.Click();
                Assert.IsTrue(_ScheduleLoadsPage.PickTime.ClearAndEdit(_Data.Pickup_Time));
                _ScheduleLoadsPage.Outside.Click();
                Assert.IsTrue(_ScheduleLoadsPage.PickUpdateSave.Click());
                SelectReasonForChange();
                Assert.IsTrue(_ScheduleLoadsPage.DeliveryDate.ClearAndEdit(_Data.Delivery_Date));
                _ScheduleLoadsPage.Outside.Click();
                Assert.IsTrue(_ScheduleLoadsPage.DeliveryTime.ClearAndEdit(_Data.Delivery_Time));
                _ScheduleLoadsPage.Outside.Click();
                Assert.IsTrue(_ScheduleLoadsPage.DeliveryUpdateSave.Click());
                SelectReasonForChange();
                _ScheduleLoadsPage.SaveMessage.WaitUtilDisappear();   
                return "DateUpdated";
            }
            catch
            {
                return "UpdateFailed";
            }
        }

        public void SelectReasonForChange()
        {
            try
            {
                Assert.IsTrue(_ScheduleLoadsPage.ChangeReasonPopup.WaitUntilDisplayed(15));
                Assert.IsTrue(_ScheduleLoadsPage.PopContinue.Click());
            }
            catch
            {

            }
        }
        public string UpdateDeliveryDate()
        {
            try
            {

                return "DateUpdated";
            }
            catch
            {
                return "UpdateFailed";
            }
        }

    }


}
