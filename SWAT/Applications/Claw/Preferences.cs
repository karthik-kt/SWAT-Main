using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace SWAT.Applications.Claw
{
    using SWAT.Applications.Claw.DAL;
    using SWAT.Applications.Claw.ObjectRepository;
    using SWAT.Configuration;
    using SWAT.Data;
    using SWAT.Logger;
    using SWAT.FrameWork.Utilities;

    class Preferences
    {
        PreferencesData _PreferencesData;
        PreferencesPage _PreferencesPage;
        ModesData _ModesData;
        AvailableEquipmentData _AvailableEquipmentData;
        PreferredlaneData _PreferredlaneData;
        IWebDriver _driver;

        public Preferences(TestStartInfo teststartinfo, DataManager datamanager)
        {
            _PreferencesData = new PreferencesData(datamanager);
            _PreferencesPage = new PreferencesPage(teststartinfo);
            _ModesData = new ModesData(datamanager);
            _AvailableEquipmentData = new AvailableEquipmentData(datamanager);
            _PreferredlaneData = new PreferredlaneData(datamanager);
            _driver = teststartinfo.Driver;
        }

        public string Navigate()
        {
            try
            {
                Assert.IsTrue(_PreferencesPage.Navigate());
                Assert.IsTrue(_PreferencesPage.ModesHeader.WaitUntilDisplayed());
                Assert.IsTrue(_PreferencesPage.AppTitle.GetText().Trim().Equals("Preferences"));
                return "NavigationSuccess";
            }
            catch
            {
                return "NavigationFailed";
            }

        }

        public string VerifyPreferencesOptions()
        {
            try
            {
                Assert.IsTrue(_PreferencesPage.AppTitle.GetText().Trim().Equals("Preferences"));
                Assert.IsTrue(_PreferencesPage.ModesHeader.IsDisplayed());
                Assert.IsTrue(_PreferencesPage.ModesHeader.IsEnabled());
                Assert.IsTrue(_PreferencesPage.ServicesHeader.IsDisplayed());
                Assert.IsTrue(_PreferencesPage.ServicesHeader.IsEnabled());
                Assert.IsTrue(_PreferencesPage.EquipmentServiceHeader.IsDisplayed());
                Assert.IsTrue(_PreferencesPage.EquipmentServiceHeader.IsEnabled());
                Assert.IsTrue(_PreferencesPage.CertificationLicenseHeader.IsDisplayed());
                Assert.IsTrue(_PreferencesPage.CertificationLicenseHeader.IsEnabled());
                Assert.IsTrue(_PreferencesPage.OwnershipHeader.IsDisplayed());
                Assert.IsTrue(_PreferencesPage.OwnershipHeader.IsEnabled());
                Assert.IsTrue(_PreferencesPage.EquipmentHeader.IsDisplayed());
                Assert.IsTrue(_PreferencesPage.EquipmentHeader.IsEnabled());
                Assert.IsTrue(_PreferencesPage.AuthoritiesHeader.IsDisplayed());
                Assert.IsTrue(_PreferencesPage.AuthoritiesHeader.IsEnabled());
                Assert.IsTrue(_PreferencesPage.DispatchingHoursHeader.IsDisplayed());
                Assert.IsTrue(_PreferencesPage.DispatchingHoursHeader.IsEnabled());
                Assert.IsTrue(_PreferencesPage.PreferredlaneHeader.IsDisplayed());
                Assert.IsTrue(_PreferencesPage.PreferredlaneHeader.IsEnabled());
                return "VerifyPreferencesOptionsSuccess";
            }
            catch
            {
                return "VerifyPreferencesOptionsFailed";
            }
        }
        public string UpdateModes()
        {
            try
            {
                Assert.IsTrue(_PreferencesPage.AppTitle.GetText().Trim().Equals("Preferences"));
                Assert.IsTrue(_PreferencesPage.ModesHeader.IsDisplayed());
                Assert.IsTrue(_PreferencesPage.ModesHeader.IsEnabled());
                Assert.IsTrue(SelectOption(_PreferencesPage.ModesAir, _ModesData.Air));
                Assert.IsTrue(SelectOption(_PreferencesPage.ModesIntermodal, _ModesData.Intermodal));
                Assert.IsTrue(SelectOption(_PreferencesPage.ModesLTL, _ModesData.LTL));
                Assert.IsTrue(SelectOption(_PreferencesPage.ModesOcean, _ModesData.Ocean));
                Assert.IsTrue(SelectOption(_PreferencesPage.ModesRail, _ModesData.Rail));
                Assert.IsTrue(SelectOption(_PreferencesPage.ModesTL, _ModesData.TL));
                Assert.IsTrue(_PreferencesPage.ModesSave.Click());
                Assert.IsTrue(_PreferencesPage.ModesSaveAlert.WaitUntilDisplayed());
                Assert.IsTrue(_PreferencesPage.ModesSaveAlert.GetText().Trim().Equals("Your changes have been saved."));
                return "UpdateModesSuccess";
            }
            catch
            {
                return "UpdateModesFailed";
            }
        }

        private bool SelectOption(UIItem Option, string data)
        {
            if (data.Equals("!FLIP!"))
            {
                return FlipOption(Option);
            }
            return Option.SelectByText(data);
        }

        private bool FlipOption(UIItem Option)
        {
            try
            {
                string option = Option.GetSelectedItemText();
                Assert.IsNotNull(option);
                if (option.Trim().Equals("No"))
                {
                    return Option.SelectByText("Yes");
                }
                else if (option.Trim().Equals("Yes"))
                {
                    return Option.SelectByText("No");
                }
                else if (option.Trim().Equals("--"))
                {
                    return Option.SelectByText("No");
                }
                return false;
            }
            catch
            {
                return false;
            }
            
        }

        public string UpdateAvailableEquipment()
        {
            try
            {
                Assert.IsTrue(_PreferencesPage.AppTitle.GetText().Trim().Equals("Preferences"));
                Assert.IsTrue(_PreferencesPage.EquipmentHeader.IsDisplayed());
                Assert.IsTrue(_PreferencesPage.EquipmentHeader.IsEnabled());
                Assert.IsTrue(_PreferencesPage.EquipmentInputTotalPowerUnits.ClearAndEdit(_AvailableEquipmentData.TotalPowerUnits));
                Assert.IsTrue(_PreferencesPage.EquipmentInputTotalDrivers.ClearAndEdit(_AvailableEquipmentData.InputTotalDrivers));
                Assert.IsTrue(_PreferencesPage.EquipmentSave.Click());
                Assert.IsTrue(_PreferencesPage.EquipmentSaveAlert.WaitUntilDisplayed());
                Assert.IsTrue(_PreferencesPage.EquipmentSaveAlert.GetText().Trim().Equals("Your changes have been saved."));
                return "UpdateAvailableEquipmentSuccess";
            }
            catch
            {
                return "UpdateAvailableEquipmentFailed";
            }
        }

        public string AddAnotherLane()
        {
            try
            {
                Assert.IsTrue(_PreferencesPage.AppTitle.GetText().Trim().Equals("Preferences"));
                Assert.IsTrue(_PreferencesPage.PreferredlaneHeader.IsDisplayed());
                Assert.IsTrue(_PreferencesPage.PreferredlaneHeader.IsEnabled());
                Assert.IsTrue(_PreferencesPage.PreferredlaneAdd.Click());
                List<Preferredlane> Preferredlanes = _PreferencesPage.Preferredlanes;
                Preferredlane Preferredlane = Preferredlanes.Last();
                Assert.IsTrue(Preferredlane.PreferredlaneOriginInput.TypeAndSelect(_PreferredlaneData.OriginInput));
                Assert.IsTrue(Preferredlane.PreferredlaneDestinationInput.TypeAndSelect(_PreferredlaneData.DestinationInput));
                Assert.IsTrue(Preferredlane.PreferredlaneEquipmentType.SelectByText(_PreferredlaneData.EquipmentType));
                Assert.IsTrue(Preferredlane.PreferredlaneCapacity.SelectByText(_PreferredlaneData.Capacity));
                Assert.IsTrue(_PreferencesPage.PreferredlaneSave.Click());
                Assert.IsTrue(_PreferencesPage.PreferredlaneSaveAlert.WaitUntilDisplayed());
                Assert.IsTrue(_PreferencesPage.PreferredlaneSaveAlert.GetText().Trim().Equals("Your changes have been saved."));
                return "AddAnotherLaneSuccess";
            }
            catch
            {
                return "AddAnotherLaneFailed";
            }
        }

        public string DeleteLane()
        {
            try
            {
                Assert.IsTrue(_PreferencesPage.AppTitle.GetText().Trim().Equals("Preferences"));
                Assert.IsTrue(_PreferencesPage.PreferredlaneHeader.IsDisplayed());
                Assert.IsTrue(_PreferencesPage.PreferredlaneHeader.IsEnabled());
                List<Preferredlane> Preferredlanes = _PreferencesPage.Preferredlanes;
                Preferredlane Preferredlane = Preferredlanes.Last();
                Assert.IsTrue(Preferredlane.PreferredlaneDelete.Click());
                Assert.IsTrue(_PreferencesPage.PreferredlaneSave.Click());
                Assert.IsTrue(_PreferencesPage.PreferredlaneSaveAlert.WaitUntilDisplayed());
                Assert.IsTrue(_PreferencesPage.PreferredlaneSaveAlert.GetText().Trim().Equals("Your changes have been saved."));
                return "DeleteLaneSuccess";
            }
            catch
            {
                return "DeleteLaneFailed";
            }
        }
    }
}
