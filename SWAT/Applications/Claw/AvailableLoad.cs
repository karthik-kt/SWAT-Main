using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using SWAT.Applications.Claw.ObjectRepository;
using SWAT.Data;
using SWAT.Logger;
using SWAT.Configuration;
using SWAT.Applications.Claw.DAL;

namespace SWAT.Applications.Claw
{
    public class AvailableLoad 
    {
        private Dictionary<string, string> EquipmentType = new Dictionary<string, string>();
        private AvailableLoadData _AvailableLoadData = null;
        private AvailableLoadPage _AvailableLoadPage = null;
        private AvilableLoad_BrowseAndFliter _AvilableLoad_BrowseAndFliter = null;
        private AvilableLoad_PostAndMatch _AvilableLoad_PostAndMatch = null;

        public AvailableLoad(TestStartInfo teststartinfo, DataManager datamanager)
        {
            _AvilableLoad_BrowseAndFliter = new AvilableLoad_BrowseAndFliter(teststartinfo);
            _AvilableLoad_PostAndMatch = new AvilableLoad_PostAndMatch(teststartinfo);     
            _AvailableLoadPage = new AvailableLoadPage(teststartinfo);
            _AvailableLoadData = new AvailableLoadData(datamanager);
            LoadEquipementType();
        }

        private void LoadEquipementType()
        {
            EquipmentType.Add("", "--");
            EquipmentType.Add("V", "Van");
            EquipmentType.Add("R", "Reefer");
            EquipmentType.Add("F", "Flatbed");
            EquipmentType.Add("C", "Container");
            EquipmentType.Add("SD", "Step Deck");
            EquipmentType.Add("DF", "Drop Frame");
            EquipmentType.Add("DD", "Double Drop/Lowboy");
            EquipmentType.Add("SS", "Soft Shell");
            EquipmentType.Add("FT", "Flatbed with Tarp");
            EquipmentType.Add("E", "Euroliner");
            EquipmentType.Add("T", "Tilt");
            EquipmentType.Add("B", "Bulk");
            EquipmentType.Add("Z", "Tanker");
            EquipmentType.Add("M", "Megatrailer");
            EquipmentType.Add("Q", "Road Train");
            EquipmentType.Add("P", "Power Only");
            EquipmentType.Add("CNRU", "CNRU");
            EquipmentType.Add("CPPU", "CPPU");
            EquipmentType.Add("CSXU", "CSXU");
            EquipmentType.Add("EMHU", "EMHU");
            EquipmentType.Add("EMPU", "EMPU");
            EquipmentType.Add("EMWU", "EMWU");
            EquipmentType.Add("EPTY", "EPTY");
            EquipmentType.Add("PACU", "PACU");
            EquipmentType.Add("W", "Vented Van");
            EquipmentType.Add("G", "Removable Goose Neck");
            EquipmentType.Add("K", "Conestoga");
            EquipmentType.Add("DR", "Decked Reefer");
            EquipmentType.Add("H", "Heated Van");
            EquipmentType.Add("FWS", "Flatbed with Sides");
            EquipmentType.Add("BT", "Walking floor");
            EquipmentType.Add("ZS", "Silo Tanker");
            EquipmentType.Add("XM", "Boxcar");
            EquipmentType.Add("MV", "Maxi Van");
            EquipmentType.Add("MF", "Maxi Flatbed");
            EquipmentType.Add("FM", "Flat Car");
            EquipmentType.Add("DV", "Duraplate Van");
            EquipmentType.Add("LALL", "Landall");
            EquipmentType.Add("SDL", "Stepdeck With Ramps");
            EquipmentType.Add("A", "Auto Trailer");
            EquipmentType.Add(Constants.Option_Ignore, Constants.Option_Ignore);
        }

        #region PostAndMatch

        public string PostAndMatch_Remove_Truck()
        {
            try
            {
                Assert.IsTrue(PostAndMatch_NavigateTo());
                Assert.IsTrue(_AvilableLoad_PostAndMatch.ViewTrucks.Click());
                string truckdetails = _AvailableLoadData.Origin + " "
                                                + _AvailableLoadData.Destination 
                                                + _AvailableLoadData.EmptyDate.Replace("/"+DateTime.Today.Year.ToString(), "") + ","
                                                + _AvailableLoadData.EmptyTime 
                                                + _AvailableLoadData.Equipment.Substring(0, 1) + ","
                                                + _AvailableLoadData.Length;
                List<int> index = _AvilableLoad_PostAndMatch.ViewTrucks_ResultsRows.GetElementIndex(truckdetails);
                if (index.Count == 0)
                    return "NoTrucksAvilable";
                Assert.IsTrue(_AvilableLoad_PostAndMatch.ViewTrucks_Delete.Click(index[index.Count - 1] + 1));
                return "TruckRemoved";
            }
            catch
            {
                return "TruckRemoveFailed";
            }
        }

        public string PostAndMatch_Match_Trucks()
        {
            try
            {
                Assert.IsTrue(PostAndMatch_NavigateTo());
                Assert.IsTrue(_AvilableLoad_PostAndMatch.ViewTrucks.Click());
                string truckdetails = _AvailableLoadData.Origin + " "
                                                + _AvailableLoadData.Destination 
                                                + _AvailableLoadData.EmptyDate.Replace("/"+DateTime.Today.Year.ToString(), "") + ","
                                                + _AvailableLoadData.EmptyTime 
                                                + _AvailableLoadData.Equipment.Substring(0, 1) + ","
                                                + _AvailableLoadData.Length;
                List<int> index = _AvilableLoad_PostAndMatch.ViewTrucks_ResultsRows.GetElementIndex(truckdetails);
                if (index.Count == 0)  return "NoTrucksAvilable";
                Assert.IsTrue(_AvilableLoad_PostAndMatch.ViewTrucks_ResultsRows.Click(index[index.Count - 1] + 1));
                Assert.IsTrue(_AvilableLoad_PostAndMatch.ViewTrucks_Results_ODH.ClearAndEdit("0"));
                Assert.IsTrue(_AvilableLoad_PostAndMatch.ViewTrucks_Results_DDH.ClearAndEdit("0"));
                Assert.IsTrue(_AvilableLoad_PostAndMatch.ViewTrucks_Results_MatchTrcukButton.Click());
                _AvilableLoad_PostAndMatch.WaitUntilLoading();
                if (_AvilableLoad_PostAndMatch.NoResultFound.WaitUntilDisplayed(10))  return "NoLoadsMatching";
                _AvilableLoad_PostAndMatch.PostAndMatch_ResultsTable.WaitUntilDisplayed();
                return "MatchFound";
            }
            catch
            {
                return "MatchNotFound";
            }
        }

        public string PostAndMatch_Add_Truck()
        {
            try
            {
                Assert.IsTrue(PostAndMatch_NavigateTo());
                Assert.IsTrue(_AvilableLoad_PostAndMatch.AddTruck_Button.Click());
                _AvilableLoad_PostAndMatch.WaitUntilLoading();
                Assert.IsTrue(_AvilableLoad_PostAndMatch.AddTruck_Orgin.WaitUntilDisplayed());
                Assert.IsTrue(_AvilableLoad_PostAndMatch.AddTruck_Orgin.ClearAndEdit(_AvailableLoadData.Origin));
                Assert.IsTrue(_AvilableLoad_PostAndMatch.AddTruck_Destination.ClearAndEdit(_AvailableLoadData.Destination));
                Assert.IsTrue(_AvilableLoad_PostAndMatch.AddTruck_EmptyDate.ClearAndEdit(_AvailableLoadData.EmptyDate));
                _AvilableLoad_PostAndMatch.AddTruck_EmptyDate.Edit(Keys.Tab);
                _AvilableLoad_PostAndMatch.AddTruck_EmptyTime.Click();
                Assert.IsTrue(_AvilableLoad_PostAndMatch.AddTruck_EmptyTime.ClearAndEdit(_AvailableLoadData.EmptyTime));
                Assert.IsTrue(_AvilableLoad_PostAndMatch.AddTruck_EquipmentType.SelectByText(_AvailableLoadData.Equipment));
                Assert.IsTrue(_AvilableLoad_PostAndMatch.AddTruck_Length.ClearAndEdit(_AvailableLoadData.Length));
                Assert.IsTrue(_AvilableLoad_PostAndMatch.AddTruck_AddButton.Click());
                _AvilableLoad_PostAndMatch.WaitUntilSaving();
                _AvilableLoad_PostAndMatch.WaitUntilLoading();
                Assert.IsTrue(_AvilableLoad_PostAndMatch.AddTruck_SuccessMsg.WaitUntilDisplayed());
                _AvilableLoad_PostAndMatch.AddTruck_AddButton.Edit(Keys.Escape);
                return "TruckAdded";
            }
            catch
            {
                return "AddingTruckFailed";
            }
        }

        public bool PostAndMatch_NavigateTo()
        {
            try
            {
                if (_AvilableLoad_PostAndMatch.ViewTrucks.WaitUntilDisplayed(10))
                    return true;
                Assert.IsTrue(_AvilableLoad_PostAndMatch.PostAndMatch_Tab.Click());
                _AvilableLoad_PostAndMatch.WaitUntilLoading();
                Assert.IsTrue(_AvilableLoad_PostAndMatch.ViewTrucks.WaitUntilDisplayed());
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region BrowseAndFilter

        public string BrowseAndFilter_GetAvilableLoadDetails()
        {
            try
            {
                _AvilableLoad_BrowseAndFliter.WaitUntilLoading();
                _AvilableLoad_BrowseAndFliter.Result_Table.WaitUntilDisplayed();
                _AvailableLoadData.LoadId = _AvilableLoad_BrowseAndFliter.Result_Table.GetData(_AvailableLoadData.LoadId);
                string originDetails = _AvilableLoad_BrowseAndFliter.Result_Table.GetData(_AvailableLoadData.Origin);
                string destinationDetails = _AvilableLoad_BrowseAndFliter.Result_Table.GetData(_AvailableLoadData.Destination);
                string equipmentDetails = _AvilableLoad_BrowseAndFliter.Result_Table.GetData(_AvailableLoadData.Equipment);
                string orgin_destination_pattern = @"(([A-Z])\w+ )?([A-Z])\w+, ([A-Z])\w+";
                string date_pattern = @"([0-9][0-9]\/[0-9][0-9])+";
                string length_pattern = @"([0-9][0-9]+')";
                _AvailableLoadData.Origin = Regex.Match(originDetails, orgin_destination_pattern).Value.ToString();
                _AvailableLoadData.Destination = Regex.Match(destinationDetails, orgin_destination_pattern).Value.ToString();
                _AvailableLoadData.EmptyDate = Regex.Match(originDetails, date_pattern).Value.ToString() + @"/" + DateTime.Today.Year.ToString();
                _AvailableLoadData.EmptyTime = "00:01";
                _AvailableLoadData.Length = Regex.Match(equipmentDetails, length_pattern).Value.ToString();
                _AvailableLoadData.Equipment = EquipmentType.Where(p => Regex.Match(equipmentDetails, p.Key).Success).ToList()[1].Value;
                return "DataUpdated";
            }
            catch
            {
                return "DataUpdateFailed";
            }
        }
       
        public string BrowseAndFilter_SearchResult_Verify()
        {
            try
            {
                Assert.IsTrue(_AvailableLoadPage.SearchResults_Table.UIVerify(_AvailableLoadData.Destination));
                Assert.IsTrue(_AvailableLoadPage.SearchResults_Table.UIVerify(_AvailableLoadData.Origin));
                return "ResultsMatching";
            }
            catch
            {
                return "ResultsNotMatching";
            }
        }

        public string BrowseAndFilter_Filter()
        {
            try
            {
                Assert.IsTrue(_AvailableLoadPage.AvailableLoadsOriginState.ClearAndEdit(_AvailableLoadData.Origin));
                Assert.IsTrue(_AvailableLoadPage.AvailableLoadsDestinationState.ClearAndEdit(_AvailableLoadData.Destination));
                _AvailableLoadPage.AvailableLoadsDestinationState.Edit(Keys.Tab);
                _AvailableLoadPage.AvailableLoadsOriginState.Click();                
                Assert.IsTrue(_AvailableLoadPage.OriginDeadhead.Edit(_AvailableLoadData.DestinationDeadhead));
                Assert.IsTrue(_AvailableLoadPage.DestinationDeadhead.Edit(_AvailableLoadData.OriginDeadhead));
                _AvailableLoadPage.WaitUntilLoading();
                Assert.IsFalse(_AvailableLoadPage.NoResultsFound.IsDisplayed(10));
                return "SearchResultDisplayed";
            }
            catch
            {
                return "SearchFailed";
            }
        }

        public string BrowseAndFilter_MakeOffer_Close()
        {
            try
            {
                Assert.IsTrue(_AvilableLoad_BrowseAndFliter.MakeOffer_Title.WaitUntilDisplayed());
                Assert.IsTrue(_AvilableLoad_BrowseAndFliter.MakeOffer_OfferCancel.Click());
                _AvilableLoad_BrowseAndFliter.WaitUntilLoading();
                Assert.IsTrue(_AvilableLoad_BrowseAndFliter.MakeOffer_NoLoadSelected.WaitUntilDisplayed());
                return "MakeOfferClosed";
            }
            catch
            {
                return "MakeOfferCloseFailed";
            }
        }

        public string BrowseAndFilter_MakeOffer_Open()
        {
            try
            {
                string actualresult = "MakeOfferOpened";
                int numberofloads = _AvilableLoad_BrowseAndFliter.Result_Row.GetCountOfElements();
                foreach(var eachRow in _AvilableLoad_BrowseAndFliter.Result_Row.GetAllUIItems())
                {
                    eachRow.Click();
                    _AvilableLoad_BrowseAndFliter.WaitUntilLoading();
                    if (_AvilableLoad_BrowseAndFliter.MakeOffer_Title.IsDisplayed() && _AvailableLoadPage.EmptyLocation.IsDisplayed())
                    {
                        Assert.IsTrue(_AvilableLoad_BrowseAndFliter.MakeOffer_EmptyLocation.IsEnabled());
                        Assert.IsTrue(_AvilableLoad_BrowseAndFliter.MakeOffer_EmptyDate.IsEnabled());
                        Assert.IsTrue(_AvilableLoad_BrowseAndFliter.MakeOffer_EmptyTime.IsEnabled());
                        Assert.IsTrue(_AvilableLoad_BrowseAndFliter.MakeOffer_EquipmentType.IsEnabled());
                        Assert.IsTrue(_AvilableLoad_BrowseAndFliter.MakeOffer_OfferAmount.IsEnabled());
                        Assert.IsTrue(_AvilableLoad_BrowseAndFliter.MakeOffer_OfferSubmit.IsEnabled());
                        return actualresult;
                    }                    
                }
                return "MakeOfferOpenFailed";
            }
            catch
            {
                return "MakeOfferOpenFailed";
            }
        }

        public string BrowseAndFilter_NavigateTo()
        {
            try
            {
                _AvilableLoad_BrowseAndFliter.Navigate();
                _AvilableLoad_BrowseAndFliter.WaitUntilLoading();
                Assert.IsTrue(_AvilableLoad_BrowseAndFliter.RecommendedLoad_Tab.WaitUntilDisplayed());
                Assert.IsTrue(_AvilableLoad_BrowseAndFliter.BrowseAndFliter_Tab.WaitUntilDisplayed());
                Assert.IsTrue(_AvilableLoad_BrowseAndFliter.PostAndMatch_Tab.WaitUntilDisplayed());
                return "AvilableLoadsOpened";
            }
            catch
            {
                return "AvilableLoadsOpenFailed";
            }
        }
        
        public string BrowseAndFilter_ClearFilterAndVerify()
        {
            try
            {
                Assert.IsTrue(_AvailableLoadPage.ClearAllSearch.IsDisplayed());
                Assert.IsTrue(_AvailableLoadPage.ClearAllSearch.Click());
                Assert.IsTrue(!_AvailableLoadPage.ClearAllSearch.IsDisplayed());
                return "RemoveFilterSuccess";
            }
            catch
            {
                return "RemoveFilterFails";
            }
        }

        public string BrowseAndFilter_VerifySearchResult()
        {
            try
            {
                return "Invalid Keyword, use Claw.Result.Verify";
            }
            catch
            {
                return "VerifyFails";
            }
        }

        public string BrowseAndFilter_VerifyFiltersAndSearch()
        {
            try
            {
                return "Invalid Keyword, use Claw.Result.Verify";
            }
            catch
            {
                return "FilterFails";
            }
        }

        public string BrowseAndFilter_MakeOffer()
        {
            try
            {
                Assert.IsTrue(_AvailableLoadPage.RecommendedLoadButton.Click());
                Assert.IsTrue(_AvailableLoadPage.SearchResults_Table.WaitUntilDisplayed());
                RecommendedLoad load;
                load = _AvailableLoadPage.RecommendedLoads.First();
                if (load.OfferDisplayAmount.IsDisplayed())
                {
                    BrowseAndFilter_RemoveOffer();
                }
                Assert.IsTrue(load.MoreInfoButton.Click());
                Thread.Sleep(Constants.Wait_Medium);
                Assert.IsTrue(_AvailableLoadPage.CurtainContainer.WaitUntilDisplayed());
                Assert.IsTrue(_AvailableLoadPage.EmptyLocation.TypeAndSelect(_AvailableLoadData.EmptyLocation));
                Assert.IsTrue(_AvailableLoadPage.CanLoadDate.ClearAndEdit(_AvailableLoadData.EmptyDate));
                Assert.IsTrue(_AvailableLoadPage.CanLoadDate.Edit(Keys.Tab));
                Assert.IsTrue(_AvailableLoadPage.CanLoadTime.ClearAndEdit(_AvailableLoadData.EmptyTime));
                Assert.IsTrue(_AvailableLoadPage.OfferEquipmentType.SelectByText(_AvailableLoadData.OfferEquipmentType));
                Assert.IsTrue(_AvailableLoadPage.OfferRate.ClearAndEdit(_AvailableLoadData.OfferAmount));
                Assert.IsTrue(_AvailableLoadPage.OfferSubmit.Click());
                Thread.Sleep(Constants.Wait_Medium);
                Assert.IsTrue(_AvailableLoadPage.OfferSubmitMsg.WaitUntilDisplayed());
                string offeredAmount = Convert.ToDouble(_AvailableLoadData.OfferAmount).ToString("#,##0.00");
                Assert.IsTrue(_AvailableLoadPage.OfferSubmitMsg.GetText(0).Equals("Your offer of $" + offeredAmount + " has been submitted."));
                Assert.IsTrue(load.OfferDisplayAmount.IsDisplayed());
                return "OfferDone";
            }
            catch
            {
                return "OfferFails";
            }
        }

        public string BrowseAndFilter_RemoveOffer()
        {
            try
            {
                Assert.IsTrue(_AvailableLoadPage.SearchResults_Table.WaitUntilDisplayed());
                RecommendedLoad load;
                load = _AvailableLoadPage.RecommendedLoads.First();
                Assert.IsTrue(load.OfferDisplayAmount.IsDisplayed());
                Assert.IsTrue(load.MoreInfoButton.Click());
                Thread.Sleep(Constants.Wait_Medium);
                Assert.IsTrue(_AvailableLoadPage.CurtainContainer.WaitUntilDisplayed());
                Assert.IsTrue(_AvailableLoadPage.RemoveOffer.IsDisplayed());
                Assert.IsTrue(_AvailableLoadPage.RemoveOffer.Click());
                Thread.Sleep(Constants.Wait_Medium);
                Assert.IsTrue(!_AvailableLoadPage.CurtainContainer.WaitUntilDisplayed());
                load = _AvailableLoadPage.RecommendedLoads.First();
                Assert.IsTrue(!load.OfferDisplayAmount.IsDisplayed());
                return "OfferRemoved";
            }
            catch
            {
                return "OfferRemovedFails";
            }
        }

        #endregion

        #region Recommended

        public string VerifyRecommendedLoads()
        {
            try
            {
                Assert.IsTrue(_AvailableLoadPage.RecommendedLoadButton.Click());
                Assert.IsTrue(_AvailableLoadPage.SearchResults_Table.WaitUntilDisplayed());
                Assert.IsTrue(_AvailableLoadPage.SearchResults_Row.GetCountOfElements().Equals(20));
                Assert.IsTrue(_AvailableLoadPage.LoadNumber.IsDisplayed());
                Assert.IsTrue(_AvailableLoadPage.Mode.IsDisplayed());
                Assert.IsTrue(_AvailableLoadPage.Equipment.IsDisplayed());
                Assert.IsTrue(_AvailableLoadPage.Stops.IsDisplayed());
                Assert.IsTrue(_AvailableLoadPage.Origin.IsDisplayed());
                Assert.IsTrue(_AvailableLoadPage.PickupDateAndTime.IsDisplayed());
                Assert.IsTrue(_AvailableLoadPage.Destination.IsDisplayed());
                Assert.IsTrue(_AvailableLoadPage.DeliveryDateAndTime.IsDisplayed());
                Assert.IsTrue(_AvailableLoadPage.Distance.IsDisplayed());
                List<RecommendedLoad> loads = _AvailableLoadPage.RecommendedLoads;
                foreach (RecommendedLoad load in loads)
                {
                    Assert.IsTrue(load.MoreInfoButton.IsDisplayed());
                    Assert.IsTrue(load.MoreInfoPopUp.Click());
                    Assert.IsTrue(load.AddToPreferedLane.IsDisplayed());
                    Assert.IsTrue(load.RemoveLoad.IsDisplayed());
                    Assert.IsTrue(load.MoreInfoPopUp.Click());
                }
                return "VerifySuccess";
            }
            catch
            {
                return "VerifyFails";
            }
        }

        public string AddToPreferredLane()
        {
            try
            {
                Assert.IsTrue(_AvailableLoadPage.RecommendedLoadButton.Click());
                Assert.IsTrue(_AvailableLoadPage.SearchResults_Table.WaitUntilDisplayed());
                RecommendedLoad load = _AvailableLoadPage.RecommendedLoads.First();
                _AvailableLoadData.Origin = load.Origin.GetText(0);
                _AvailableLoadData.Destination = load.Destination.GetText(0);
                Assert.IsTrue(load.MoreInfoButton.IsDisplayed());
                Assert.IsTrue(load.MoreInfoPopUp.Click());
                Assert.IsTrue(load.AddToPreferedLane.IsDisplayed());
                Assert.IsTrue(load.AddToPreferedLane.Click());
                Thread.Sleep(Constants.Wait_Medium);
                Assert.IsTrue(_AvailableLoadPage.OfferSubmitMsg.IsDisplayed());
                Assert.IsTrue(_AvailableLoadPage.OfferSubmitMsg.GetText(0).Trim().Equals("Lane and equipment are already a Preferred Lane.")
                                || _AvailableLoadPage.OfferSubmitMsg.GetText(0).Trim().Equals("Lane and equipment have been added to Preferred Lanes."));
                return "LaneAddedSuccessfully";
            }
            catch (Exception ex)
            {
                return "LaneAddFails";
            }
        }

        public string VerifyNewLaneOnPreferencePage()
        {
            try
            {
                Assert.IsTrue(_AvailableLoadPage.AppTitle.WaitUntilDisplayed());
                Assert.IsTrue(_AvailableLoadPage.AppTitle.GetText().Trim().Equals("Preferences"));
                List<PreferredLane> lanes = _AvailableLoadPage.PreferredLanes;
                foreach (PreferredLane lane in lanes)
                {
                    if (lane.Origin.GetValue().Trim() == _AvailableLoadData.Origin && lane.Destination.GetValue().Trim() == _AvailableLoadData.Destination)
                    {
                        return "LaneVerificationSuccess";
                    }
                }
                return "LaneVerificationFails";
            }
            catch
            {
                return "LaneVerificationFails";
            }
            
        }

        public string RemoveRecommendedLoad()
        {
            try
            {
                _AvailableLoadPage.WaitUntilLoading();
                Assert.IsTrue(_AvailableLoadPage.RecommendedLoadButton.Click());
                Assert.IsTrue(_AvailableLoadPage.SearchResults_Table.WaitUntilDisplayed());
                Assert.IsTrue(_AvailableLoadPage.SearchResults_Table.GetAllUIItems()[0].Click());
                Assert.IsTrue(_AvailableLoadPage.RemoveRecommendationsLoad.Click());
                _AvailableLoadPage.WaitUntilLoading();
                Assert.IsTrue(_AvailableLoadPage.OfferSubmitMsg.WaitUntilDisplayed());
                return "Keyword changed use...UnDoRemoveRecommendedLoad";
                //return "RemovedSuccessfully";
            }
            catch
            {
                return "RemovedFails";
            }
        }

        public string UnDoRemoveRecommendedLoad()
        {
            try
            {
                _AvailableLoadPage.WaitUntilLoading();
                Assert.IsTrue(_AvailableLoadPage.Undo_RemoveRecommendationsLoad.Click());
                string loadid = Regex.Match(_AvailableLoadPage.RemovedRecommendations_Message.GetText(), @"\b\d{3}[-.]?\d{3}[-.]?\d{4}\b").Value.ToString();
                Assert.IsTrue(_AvailableLoadPage.SearchResults_Table.UIVerify("Table.AnyRow.Column1.HasText." + loadid));
                _AvailableLoadPage.WaitUntilLoading();
                return "RemovedSuccessfully";
            }
            catch
            {
                return "RemovedFails";
            }
        }

        public string AddToPreferredLaneFromCurtain()
        {
            try
            {
                Assert.IsTrue(_AvailableLoadPage.RecommendedLoadButton.Click());
                Assert.IsTrue(_AvailableLoadPage.SearchResults_Table.WaitUntilDisplayed());
                RecommendedLoad load;
                load = _AvailableLoadPage.RecommendedLoads.First();
                Assert.IsTrue(load.MoreInfoButton.Click());
                Thread.Sleep(Constants.Wait_Medium);
                Assert.IsTrue(_AvailableLoadPage.CurtainContainer.WaitUntilDisplayed());
                Assert.IsTrue(_AvailableLoadPage.AddToPreferredLanes.Click());
                Assert.IsTrue(_AvailableLoadPage.CurtainLanesAlertMessage.WaitUntilDisplayed());
                return "AddedToPreferredLane";
            }
            catch
            {
                return "FailToAddToPreferredLane";
            }
        }
        
        #endregion 
    }
}