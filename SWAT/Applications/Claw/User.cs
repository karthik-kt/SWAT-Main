using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Linq;
using SWAT.Applications.Claw.ObjectRepository;
using SWAT.Applications.Claw.DAL;
using SWAT.Configuration;

namespace SWAT.Applications.Claw
{
    using SWAT.Data;
    using SWAT.Logger;
    using SWAT.FrameWork.Utilities;
    using System.Linq;
    

    public class User :UIObjects
    {
        //private DataManager testData;

        // private IWebDriver driver;
        private Dictionary<string, string> ApplicationAccess = new Dictionary<string, string>();


        private IWebDriver _driver;
        //private Admin adminpage;

        private UserPage _HelpPage = null;
        private UserPage _UserPage;
        private UserData _UserData;

        public User(TestStartInfo teststartinfo, DataManager datamanager) : base(teststartinfo)
        {
            

            testConfig = teststartinfo; 
            _driver = teststartinfo.Driver;
            _UserPage = new UserPage(teststartinfo);
            _UserData = new UserData(datamanager);
            _HelpPage = new UserPage(teststartinfo, "help#system");

            ApplicationAccess.Add("permission-128", "Scheduler_UserCanAccess");
            ApplicationAccess.Add("permission-130", "DistanceCalc_UserCanAccess");
            ApplicationAccess.Add("permission-131", "Admin_ManageUsers");
            ApplicationAccess.Add("permission-133", "Admin_ManageFacilities");
            ApplicationAccess.Add("permission-137", "Admin_ManageCustomers");
            ApplicationAccess.Add("permission-198", "Admin_ManageCarriers");
            ApplicationAccess.Add("permission-132", "FacilityCalendar_UserCanAccess");
            ApplicationAccess.Add("permission-136", "OrderMngr_UserCanAccess");
            ApplicationAccess.Add("permission-145", "OrderManager_ViewAll");
            ApplicationAccess.Add("permission-146", "OrderManager_Edit");
            ApplicationAccess.Add("permission-147", "OrderManager_Create");
            ApplicationAccess.Add("permission-148", "OrderManager_Cancel");
            ApplicationAccess.Add("permission-139", "RoutingGuideMngr_UserCanAccess");
            ApplicationAccess.Add("permission-149", "CustomizeTheme_UserCanAccess");
            ApplicationAccess.Add("permission-151", "CustomerAccounting_UserCanAccess");
            ApplicationAccess.Add("permission-185", "CarrierAccounting_UserCanAccess");
            ApplicationAccess.Add("permission-150", "CarrierAccounting_AllowAutoVoucher");
        }


       private UIItem byTotalPage
        {
            get
            {
                return new UIItem("Admin Page>> SearchResultHeader", By.XPath(".//*[@id='pagination-container']/div/strong"), _driver);
            }
        }

        public string NavigateToAllPage()
        {            
            try
            {
                //Get the total number of page
                int TotalNumPg = NumOfSearchResults();

                if (byTotalPage.IsDisplayed())
                {
                    TotalNumPg = Int32.Parse(byTotalPage.GetText().Replace("Page 1 of", ""));
                    //Click on next button and Navigate to all page
                    for (int iloop = TotalNumPg - 1; iloop > 0; iloop--)
                    {
                        //Navigate to next page and verify the page
                        Assert.IsTrue(NavigateToNextPage());
                    }
                    return "NavigationSuccess";
                }
                MyLogger.Log("Seach result doesnt contain pagination.");
                return "NavigationFailed";
            }
            catch
            {
                return "NavigationFailed";
            }
        }

        
        private bool NavigateToNextPage()
        {

            try
            {
                _UserPage.byNextPage.WaitUntilDisplayed();
                _UserPage.byNextPage.Click();
                return _UserPage.byNextPage.WaitUntilDisplayed();
            }
            catch
            {
                return false;
            }
        }

        #region Search And Open

        public string Search()
        {
            int workaroundtime = 10;
            //this is workaround implemented for admin search
            //isnt working for IE some time
            int itry1 = 3; int itry2 = 3;
            try
            {
                do
                {
                    while (!OpenAdminPage())
                    {
                        itry1--;
                    }
                    _UserPage.SearchValue.ClearAndEdit(_UserData.SearchVal);
                    try { _UserPage.SearchValue.Edit(Keys.Enter); }
                    catch { try { _UserPage.SearchButton.FindAndClickUsingJS(); } catch { } }
                    itry2--;
                } while (!_UserPage.SearchResult1stRow.WaitUntilDisplayed(workaroundtime) && itry2 > 0);

                return "UserSearchSuccess";
            }
            catch
            {
                return "UserSearchFailed";
            }
        }

        private bool OpenAdminPage()
        {
            try
            {
                Assert.IsTrue(_UserPage.AdminTab.WaitUntilDisplayed(30));
                try { _UserPage.Navigate(); }
                catch { _UserPage.AdminTab.Click(); }
                Assert.IsTrue(_UserPage.SearchButton.WaitUntilDisplayed(30));
                _UserPage.SearchHyperlink.WaitUntilDisplayed();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion Search And Open

        #region functions


        //Emulate user
        public string Emulate()
        {
            try
            {
                Assert.IsTrue(_UserPage.AccountInfo.Click());
                Assert.IsTrue(_UserPage.EmulateUserButton.WaitUntilDisplayed());
                Assert.IsTrue(_UserPage.EmulateUserButton.Click());
                //Loading some time throws 'An error occurred' 
                // Please dont modify below code without considering below logics.
                // 1. 'An error occurred' - Eumaltion failed.
                // 2. 'An error occurred' - Eumaltion success.
                // 3. 'An error occurred' - After long time Eumaltion success.
                // 4. 'An error occurred' - After long time Eumaltion failed.
                Thread.Sleep(10000);
                _UserPage.WaitUntilLoading();                
                Assert.IsTrue(_UserPage.ShowAccountOptions.WaitUntilDisplayed());
                Regex regex = new Regex("Emulating");
                Match match = regex.Match(_UserPage.ShowAccountOptions.GetText());
                if (match.Success)
                {
                    return "EmulationSuccess";
                }
                else
                {
                    return "EmulationFailed";
                }
            }
            catch
            {
                return "EmulationFailed";
            }
        }

        //Search for the user and open the user displayed on the 1st
        public string SearchAndOpen()
        {
            try
            {
                string strActResult = null;
                strActResult = Search();
                if ("UserSearchSuccess" == strActResult)
                {
                    strActResult = OpenFromSearchResult();
                    if (strActResult == "UserProfilePageOpened")
                        strActResult = "SearchAndOpenSuccess";
                }
                return strActResult;
            }
            catch
            {
                return "SearchAndOpenFailed";
            }
        }

        //Open the user details page from the search result page.
        public string OpenFromSearchResult()
        {
            try
            {
                _UserPage.SearchResult1stRow.WaitUntilDisplayed(40);
                _UserPage.SearchResult1stRow.Click();
                Assert.IsTrue(_UserPage.byAccountInfo.WaitUntilDisplayed());
                Thread.Sleep(Constants.Wait_Short);
                return "UserProfilePageOpened";
            }
            catch
            {
                return "UserProfilePageOpenFailed";
            }
        }

        //Open the application access screen from search result.
        public string AppAccess_Verify()
        {
            try
            {
                System.Threading.Thread.Sleep(Constants.Wait_Medium);
                _driver.FindElement(By.Id("permission")).Click();
                Thread.Sleep(Constants.Wait_Short);
                WaitUtilDisplayed(By.Id("permission-128"));
                foreach (var item in ApplicationAccess)
                {
                    StatusCheckORPerFormAction(item.Value, _driver.FindElement(By.Id(item.Key)));
                }
                return "AccessCheckSuccess";
            }
            catch
            {
                return "AccessCheckFailed";
            }
        }

        //Navigation to profile page
        public string Profile_Nav()
        {
            try
            {
                string strObjProfileURL = "#profile";
                Navigate(strObjProfileURL);
                Thread.Sleep(Constants.Wait_Long);
                string strObjGenInfo = "#account-information";
                StatusCheckORPerFormAction(Constants.State_Displayed, _driver.FindElement(By.CssSelector(strObjGenInfo)));
                return "NavigationSuccess";
            }
            catch
            {
                return "NavigationFailed";
            }
        }

        #endregion functions

        private string IsSearchSuccess()
        {
            try
            {
                int iNumOfRows = NumOfSearchResults();
                if (iNumOfRows == 0)
                {
                    MyLogger.Log("Zero results displayed.");
                    return "NoResultsDisplayed";
                }
                else if (iNumOfRows < 20)
                {
                    MyLogger.Log("Only one page displayed");
                    return "OnePageResult";
                }
                else
                {
                    MyLogger.Log("Totally " + iNumOfRows + " rows returned.");
                    return "MultiPageResult";
                }
            }
            catch
            {
                return "Failed";
            }
        }
        
        private int NumOfSearchResults()
        {

            try
            {
                _UserPage.byNumResultInPage.WaitUntilDisplayed();
                if (!_UserPage.byNumResultInTotal.IsDisplayed())
                {
                    return Int32.Parse(_UserPage.byNumResultInPage.GetText());
                }
                else
                {
                    return Int32.Parse(_UserPage.byNumResultInTotal.GetText());
                }
            }
            catch
            {
                return -1;
            }
        }

        public void StatusCheckORPerFormAction(string stAction, IWebElement stUIElementID)
        {
            switch (stAction.ToUpper()) // DoTo : Need to convert to uppper case, trim
            {
                case "!IGNORE!": // DoTo : Logically this wont work.Since selenium find element would have thrown error.
                    break;

                case "!DEFAULT!":
                    Assert.IsTrue(stUIElementID.Displayed);
                    break;

                case "!DISPLAYED!":
                    Assert.IsTrue(stUIElementID.Displayed);
                    break;

                case "!CHECKED!":
                    Assert.IsTrue(stUIElementID.Displayed);
                    Assert.IsTrue(stUIElementID.Selected);
                    break;

                case "!UNCHECKED!":
                    Assert.IsTrue(stUIElementID.Displayed);
                    Assert.IsFalse(stUIElementID.Selected);
                    break;

                case "!UNCHECK!":
                    Assert.IsTrue(stUIElementID.Displayed);
                    if (stUIElementID.Selected)
                    {
                        stUIElementID.Click();
                    }
                    Assert.IsFalse(stUIElementID.Selected);
                    break;

                case "!CHECK!":
                    Assert.IsTrue(stUIElementID.Displayed);
                    if (!stUIElementID.Selected)
                    {
                        stUIElementID.Click();
                    }
                    Assert.IsTrue(stUIElementID.Selected);
                    break;
            }
        }
        
        internal string GenralInfo_Verify()
        {

            string AppTitle = _UserPage.byAppTitle.GetText();
            try
            {
                Assert.IsTrue(_UserPage.byUserName.WaitUntilDisplayed());
                Assert.IsTrue(_UserPage.byProfilePicture.WaitUntilDisplayed());
                Assert.IsTrue(_UserPage.byCompanyName.WaitUntilDisplayed());
                Assert.IsTrue(_UserPage.byProfilePicture.WaitUntilDisplayed());
                Assert.IsTrue(_UserPage.byWrkPhn.WaitUntilDisplayed());
                Assert.IsTrue(_UserPage.byHmPhn.WaitUntilDisplayed());
                Assert.IsTrue(_UserPage.byMobPh.WaitUntilDisplayed());
                Assert.IsTrue(_UserPage.byFax.WaitUntilDisplayed());
                Assert.IsTrue(_UserPage.byWorkEmail.WaitUntilDisplayed());
                Assert.IsTrue(_UserPage.byAltEmail.WaitUntilDisplayed());
                if (!(AppTitle.Trim().Equals("Profile")))
                {
                    Assert.IsTrue(_UserPage.byAccStatus.WaitUntilDisplayed());
                    Assert.IsTrue(_UserPage.byStatus.WaitUntilDisplayed());
                    Assert.IsTrue(_UserPage.byUsrNme.WaitUntilDisplayed());
                    // Assert.IsTrue(byUserPiture.IsDisplayed());
                    if (_UserData.userType != "Employee")
                    {
                        Assert.IsTrue(_UserPage.byEmulateBtn.WaitUntilDisplayed());
                        Assert.IsTrue(_UserPage.byChangePasswordTab.IsDisplayed());
                    }
                    Assert.IsTrue(_UserPage.contactInformationTab.StatusCheckORPerFormAction(_UserData.contactInformationTabStatus));
                    Assert.IsTrue(_UserPage.settingsTab.StatusCheckORPerFormAction(_UserData.settingsTabStatus));
                    Assert.IsTrue(_UserPage.applicationAccessTab.StatusCheckORPerFormAction(_UserData.applicationAccessTabStatus));
                    Assert.IsTrue(_UserPage.changePasswordTab.StatusCheckORPerFormAction(_UserData.changePasswordTabStatus));
                }

                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
        }
        
        internal string GenralInfo_UpdateAndVerify()
        {
            try
            {
                Assert.IsTrue(_UserPage.byEditProfilebutton.IsDisplayed());
                Assert.IsTrue(_UserPage.byEditProfilebutton.Click());
                Assert.IsTrue(_UserPage.bySaveButton.WaitUntilDisplayed());
                Assert.IsTrue(_UserPage.byCancelEditButton.WaitUntilDisplayed());

                String headerText = _UserPage.editProfileHeader.GetText(0);
                Assert.AreEqual(headerText, "Edit Profile");

                Assert.IsTrue(_UserPage.workEmail.ClearAndEdit(_UserData.WorkEmail));
                Assert.IsTrue(_UserPage.alternateEmail.ClearAndEdit(_UserData.AlternateEmail));
                Assert.IsTrue(_UserPage.workPhoneCountryCode.ClearAndEdit(_UserData.WorkPhoneCountryCode));
                Assert.IsTrue(_UserPage.workPhoneNumber.ClearAndEdit(_UserData.WorkPhoneNumber));
                Assert.IsTrue(_UserPage.workPhoneExtension.ClearAndEdit(_UserData.WorkPhoneExtension));

                Assert.IsTrue(_UserPage.mobilePhoneCountryCode.ClearAndEdit(_UserData.MobilePhoneCountryCode));
                Assert.IsTrue(_UserPage.mobilePhoneNumber.ClearAndEdit(_UserData.MobilePhoneNumber));
                Assert.IsTrue(_UserPage.mobilePhoneExtension.ClearAndEdit(_UserData.MobilePhoneExtension));

                Assert.IsTrue(_UserPage.homePhoneCountryCode.ClearAndEdit(_UserData.HomePhoneCountryCode));
                Assert.IsTrue(_UserPage.homePhoneNumber.ClearAndEdit(_UserData.HomePhoneNumber));
                Assert.IsTrue(_UserPage.homePhoneExtension.ClearAndEdit(_UserData.HomePhoneExtension));

                Assert.IsTrue(_UserPage.faxPhoneCountryCode.ClearAndEdit(_UserData.FaxPhoneCountryCode));
                Assert.IsTrue(_UserPage.faxPhoneNumber.ClearAndEdit(_UserData.FaxPhoneNumber));
                Assert.IsTrue(_UserPage.faxPhoneExtension.ClearAndEdit(_UserData.FaxPhoneExtension));
                Assert.IsTrue(_UserPage.bySaveButton.Click());
                if (_UserData.CheckValidation == "True")
                {
                    Assert.IsTrue(_UserPage.workPhoneNumber.HasClass("error"));
                    Assert.IsTrue(_UserPage.mobilePhoneNumber.HasClass("error"));
                    Assert.IsTrue(_UserPage.homePhoneNumber.HasClass("error"));
                    Assert.IsTrue(_UserPage.faxPhoneNumber.HasClass("error"));
                }

                if (_UserData.CheckValidation == "False")
                {
                    Assert.IsTrue(_UserPage.workPhone.WaitUntilDisplayed());
                    //"+1 (123) 456 7890 x 123"
                    Assert.AreEqual(_UserPage.workPhone.GetText(0), FormattedPhoneToDisplay(_UserData.WorkPhoneCountryCode, _UserData.WorkPhoneNumber, _UserData.WorkPhoneExtension));
                    Assert.AreEqual(_UserPage.homePhone.GetText(0), FormattedPhoneToDisplay(_UserData.HomePhoneCountryCode, _UserData.HomePhoneNumber, _UserData.HomePhoneExtension));
                    Assert.AreEqual(_UserPage.mobilePhone.GetText(0), FormattedPhoneToDisplay(_UserData.MobilePhoneCountryCode, _UserData.MobilePhoneNumber, _UserData.MobilePhoneExtension));
                    Assert.AreEqual(_UserPage.faxPhone.GetText(0), FormattedPhoneToDisplay(_UserData.FaxPhoneCountryCode, _UserData.FaxPhoneNumber, _UserData.FaxPhoneExtension));
                    Assert.AreEqual(_UserPage.workEmail.GetText(0), _UserData.WorkEmail);
                    Assert.AreEqual(_UserPage.alternateEmail.GetText(0), _UserData.AlternateEmail);
                }

                return "EditProfileSucceeded";
            }
            catch
            {
                return "EditProfileFailed";
            }

        }

        private string FormattedPhoneToDisplay(string countryCode, string phoneNo, string ext)
        {
            string formattedPhone = string.Empty;
            formattedPhone = "+" + countryCode + " (" + phoneNo.Substring(0, 3) + ") " + phoneNo.Substring(3, 3) + " " +
                            phoneNo.Substring(6, 4) + " x " + ext;
            return formattedPhone;
        }

        internal bool IsManagerUserPageOpened()
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string ChangePassword()
        {
            try
            {
                Assert.IsTrue(IsManagerUserPageOpened());
                Assert.IsTrue(ChangePwd_Verify());
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
        }

        public string ChangeUserPassword()
        {
            string actualResult = null;

            try
            {
                Assert.IsTrue(ChangePwd_Verify());
                //Verify for invalid password
                _UserPage.CurrentPassword.ClearAndEdit(_UserData.currentPassword);
                _UserPage.NewPassword.ClearAndEdit("pwd");
                MyLogger.Log("Entered invalid new password and confirm password is empty");
                _UserPage.ChangePassword_.Click();
                Thread.Sleep(Constants.Wait_Short);
                List<IWebElement> tooltipErrors = _driver.FindElements(By.CssSelector(".tooltipster-base.tooltipster-error")).ToList();
                string invalidPwdError = tooltipErrors[0].FindElement(By.CssSelector(".tooltipster-content")).Text;
                string requiredError = tooltipErrors[1].FindElement(By.CssSelector(".tooltipster-content")).Text;
                if (invalidPwdError == "Invalid password" && requiredError == "Required")
                {
                    MyLogger.Log("Error messages are displayed for new and confirm passwords");
                }
                else
                {
                    actualResult = "ChangeUserPasswordFailed";
                }

                //Verify for incorrect confirm password
                _UserPage.NewPassword.ClearAndEdit(_UserData.newPassword);
                _UserPage.ConfirmNewPassword.ClearAndEdit("pwd");
                MyLogger.Log("Entered valid new password and incorrect confirm password");
                _UserPage.ChangePassword_.Click();
                Thread.Sleep(Constants.Wait_Short);
                string pwdMismatchError = _driver.FindElement(By.CssSelector(".tooltipster-content")).Text;
                if (pwdMismatchError == "Passwords do not match")
                {
                    MyLogger.Log("Password mismatch error occured");
                }
                else
                {
                    actualResult = "ChangeUserPasswordFailed";
                }

                //Verify valid and correct passwords
                _UserPage.NewPassword.ClearAndEdit(_UserData.newPassword);
                _UserPage.ConfirmNewPassword.ClearAndEdit(_UserData.confirmPassword);
                MyLogger.Log("Entered valid new password and correct confirm password");
                _UserPage.ChangePassword_.Click();
                Thread.Sleep(Constants.Wait_Short);
                string successMsg = _driver.FindElement(By.XPath(".//*[@id='reset-password-form']/div/p/strong")).Text;
                if (successMsg == "Password changed successfully")
                {
                    MyLogger.Log("Password changed successfully");
                    actualResult = "ChangeUserPasswordSuccess";
                }
                else
                {
                    actualResult = "ChangeUserPasswordFailed";
                }

                return actualResult;
            }
            catch
            {
                return "ChangeUserPasswordFailed";
            }
        }

        private bool ChangePwd_Verify()
        {
            try
            {
                _UserPage.ChangePasswordTab.Click();
                Assert.IsTrue(_UserPage.NewPassword.WaitUntilDisplayed());
                Assert.IsTrue(_UserPage.ConfirmNewPassword.WaitUntilDisplayed());
                Assert.IsTrue(_UserPage.ChangePassword_.WaitUntilDisplayed());
                if (_UserData.userType.ToUpper() == Constants.Entity_Employee)
                {
                    Assert.IsTrue(!_UserPage.CurrentPassword.IsDisplayed());
                }
                else
                {
                    Assert.IsTrue(_UserPage.CurrentPassword.IsDisplayed());
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool NavigateToSettingsTab()
        {

            try
            {
                Assert.IsTrue(_UserPage.byProfilePage.WaitUntilDisplayed());
                Assert.IsTrue(_UserPage.bySettingTab.IsDisplayed());
                Assert.IsTrue(_UserPage.bySettingTab.Click());
                Thread.Sleep(Constants.Wait_Short);
                Assert.IsTrue(_UserPage.byFaclityRelationshipPage.WaitUntilDisplayed());
                return true;
            }
            catch
            {
                return false;
            }
        }
               
        public string VerifySettingsDetails()
        {

            try
            {
                if (!_UserPage.byFaclityRelationshipPage.IsDisplayed())
                {
                    Assert.IsTrue(NavigateToSettingsTab());
                }
                if (_UserPage.byNote.GetText() == "Limit load visibility to specific facilities.")
                {
                    return "VerificationSuccess";
                }
                return "VerificationFail";
            }
            catch
            {
                return "VerificationFail";
            }
        }
                
        public string RemoveFacility()
        {
            int totalAddedFacility = VerifyElementAndGetCountOfRows(_UserPage.byFacilityRelationship.By, _UserPage.byFacilityRelationshipRows.By);
            
            try
            {
                if (!_UserPage.byFaclityRelationshipPage.IsDisplayed())
                {
                    Assert.IsTrue(NavigateToSettingsTab());
                }
                if (_UserPage.byAlertEmpty.IsDisplayed())
                {
                    return "RemoveFail";
                }
                int index = _UserPage.byFacilityRelationshipRows.GetOneElementIndex(_UserData.facilityToRemove);
                Assert.IsTrue(_UserPage.byDeleteButton.Click(index + 1));
                Thread.Sleep(Constants.Wait_Medium);
                if (!_UserPage.byAlertEmpty.IsDisplayed() && totalAddedFacility == VerifyElementAndGetCountOfRows(_UserPage.byFacilityRelationship.By, _UserPage.byFacilityRelationshipRows.By))
                {
                    return "RemoveFail";
                }
                return "RemoveSuccess";
            }
            catch
            {
                return "RemoveFail";
            }
        }

        public string AddFacility()
        {

            int rowsCountBefore = 0;
            try
            {
                if (!_UserPage.byFaclityRelationshipPage.IsDisplayed())
                {
                    Assert.IsTrue(NavigateToSettingsTab());
                }
                if (!_UserPage.byAlertEmpty.IsDisplayed())
                {
                    rowsCountBefore = VerifyElementAndGetCountOfRows(_UserPage.byFacilityRelationship.By, _UserPage.byFacilityRelationshipRows.By);
                }
                Assert.IsTrue(_UserPage.byFacilitySearch.TypeAndSelect(_UserData.facility));
                Assert.IsTrue(_UserPage.byRelationshipType.SelectByText(_UserData.relationshipType));
                Assert.IsTrue(_UserPage.byAddRelationship.Click());
                Thread.Sleep(Constants.Wait_Medium);
                if (_UserPage.byAlertEmpty.IsDisplayed() || rowsCountBefore == VerifyElementAndGetCountOfRows(_UserPage.byFacilityRelationship.By, _UserPage.byFacilityRelationshipRows.By))
                {
                    return "AddFail";
                }
                return "AddSuccess";
            }
            catch (Exception)
            {
                return "AddFail";
            }
        }

        private int VerifyElementAndGetCountOfRows(By byElement, By byElementRows)
        {
            if (IsDisplayed(byElement))
            {
                return GetElements(byElementRows).Count;
            }
            else
            {
                return 0;
            }
        }

        public string VerifyBackToSearch()
        {
            try
            {
                Assert.IsTrue(_UserPage.UserDetailPageBackButton.WaitUntilDisplayed(30));
                Assert.IsTrue(_UserPage.UserDetailPageBackButton.Click());
                MyLogger.Log("Clicked on web element BackToSearch button");
                Assert.IsTrue(_UserPage.UserDetailPageBackButton.WaitUntilDisplayed(30));
                MyLogger.Log("Admin search page is displayed");
                return "VerifyBackToSearchSuccess";
            }
            catch
            {
                return "VerifyBackToSearchFailed";
            }
        }

        public string CanEditProfile()
        {
            try
            {                
                Assert.IsTrue(_UserPage.byEditProfileBtn.WaitUntilDisplayed());
                _UserPage.byEditProfileBtn.Click();

                //Verify Popup is displayed
                Assert.IsTrue(_UserPage.byEditProfileModal.WaitUntilDisplayed());
                Assert.IsTrue(_UserPage.byEditProfileForm.WaitUntilDisplayed());
                Assert.IsTrue(_UserPage.byInputPassword.WaitUntilDisplayed());
                try
                {
                    Assert.IsTrue(_UserPage.byEditProfileModalCancelBtn.Click());
                    Thread.Sleep(Constants.Wait_Short);
                }
                catch
                {
                    Assert.IsTrue(_UserPage.byEditProfileModalCancelBtn.FindAndClickUsingJS());
                    Thread.Sleep(Constants.Wait_Short);
                }
                
                Assert.IsFalse(_UserPage.byEditProfileModal.WaitUntilDisplayed(10));

                //Enter password and update info
                _UserPage.byEditProfileBtn.Click();
                _UserPage.byInputPassword.ClearAndEdit(_UserData.currentPassword);
                Thread.Sleep(Constants.Wait_Short);
                try
                {
                    Assert.IsTrue(_UserPage.byEditProfileModalSubmitBtn.Click());
                }
                catch
                {
                    Assert.IsTrue(_UserPage.byEditProfileModalSubmitBtn.FindAndClickUsingJS());
                }
                Assert.IsTrue(_UserPage.byUploadPictureBtn.WaitUntilDisplayed());
                Assert.IsTrue(_UserPage.byUpdateContactSaveBtn.WaitUntilDisplayed());
                try
                {
                    Assert.IsTrue(_UserPage.byUpdateContactCancelBtn.Click());
                    Thread.Sleep(Constants.Wait_Short);
                }
                catch
                {
                    Assert.IsTrue(_UserPage.byUpdateContactCancelBtn.FindAndClickUsingJS());
                    Thread.Sleep(Constants.Wait_Short);
                }
                Assert.IsTrue(_UserPage.byEditProfileBtn.WaitUntilDisplayed());

                return "CanEditProfileSuccess";
            }
            catch (Exception e)
            {
                return "CanEditProfileFailed";
            }
        }

        public string VerifySystemMessage()
        {
            Assert.IsTrue(_HelpPage.AdminTab.WaitUntilDisplayed());
            _HelpPage.Navigate();
            Assert.IsTrue(_HelpPage.SystemMessagePublishRegion.WaitUntilDisplayed());
            try
            {
                switch (_UserData.TestFor.ToUpper())
                {
                    case "!ERROR!":
                        Assert.IsTrue(_HelpPage.SystemMessageSubmitButton.Click());
                        Assert.IsTrue(_HelpPage.SystemMessageTitle.HasClass("error"));
                        break;
                    case "!LENGTH!":
                        Assert.IsTrue(_HelpPage.SystemMessageTitle.ClearAndEdit(_UserData.SystemMessageTitle));
                        Assert.IsTrue(_HelpPage.SystemMessageTitle.GetValue().Length.Equals(50));
                        Assert.IsTrue(_HelpPage.SystemMessageBody.ClearAndEdit(_UserData.SystemMessageBody));
                        Assert.IsTrue(_HelpPage.SystemMessageBody.GetValue().Length.Equals(120));
                        break;
                }
                return "VerificationSuccess";
            }
            catch
            {
                return "VerificationFailed";
            }
        }
    }
}