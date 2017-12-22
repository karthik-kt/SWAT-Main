using OpenQA.Selenium;
using SWAT.FrameWork.Utilities;
using SWAT.Configuration;

namespace SWAT.Applications.Claw.ObjectRepository
{
    class UserPage : Page
    {
        public UserPage(IWebDriver driver)
        {
            _driver = driver;
            _baseurl = driver.Url;
            url = "admin";
        }

        public UserPage(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "admin";
        }

        public UserPage(TestStartInfo teststartinfo, string navigatingUrl)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = navigatingUrl;
        }

        By searchbutton = By.CssSelector("#search-term-submit-button");
        By searchhyperlink = By.CssSelector("#focus-search-term-input-button");
        By srchres1strow = By.CssSelector("#search-results-table-body>tr>td>a");
        By searchval = By.CssSelector("#search-term-input");
        By searchrestblhdr = By.CssSelector(".full-width>table>thead>tr>th");
        By userdetailpagebackbtn = By.CssSelector("#user-back-button");
        By admintab = By.LinkText("Admin");

        //Help page
        private By systemMessageTitle = By.CssSelector("#system-message-title-input");
        private By systemMessageBody = By.CssSelector("#system-message-body-textarea");
        private By systemMessageSubmitButton = By.CssSelector("#system-message-form-submit-button");
        private By systemMessagePublishRegion = By.CssSelector("#system-message-publish-region");

        public UIItem SystemMessageTitle { get { return new UIItem("Help page>>  System title textbox", this.systemMessageTitle, _driver); } }
        public UIItem SystemMessageBody { get { return new UIItem("Help page >> System body textarea", this.systemMessageBody, _driver); } }
        public UIItem SystemMessageSubmitButton { get { return new UIItem("Help page >> System submit button", this.systemMessageSubmitButton, _driver); } }
        public UIItem SystemMessagePublishRegion { get { return new UIItem("Help page >> System message region", this.systemMessagePublishRegion, _driver); } }
        //Help page ends
        public UIItem AdminTab { get { return new UIItem("Claw>> Admin Tab", this.admintab, _driver); } }
        public UIItem SearchButton { get { return new UIItem("Admin Page>>  Search Button", this.searchbutton, _driver); } }
        public UIItem SearchHyperlink { get { return new UIItem("Admin Page>>  SearchHyperlink", this.searchhyperlink, _driver); } }
        public UIItem SearchResult1stRow { get { return new UIItem("Admin Page>> SearchResult1stRow", this.srchres1strow, _driver); } }
        public UIItem SearchValue { get { return new UIItem("Admin Page>> SearchValue", this.searchval, _driver); } }
        public UIItem SearchResultHeader { get { return new UIItem("Admin Page>>  SearchResultHeader", this.searchrestblhdr, _driver); } }
        public UIItem UserDetailPageBackButton { get { return new UIItem("Admin Page>> SearchResultHeader", this.userdetailpagebackbtn, _driver); } }

        public UIItem byFacilityRelationshipRows { get { return new UIItem("Facility Relationship Rows", By.CssSelector("#facility-relationships>tr"), _driver); } }
        public UIItem byFacilityRelationship { get { return new UIItem("Facility Relationship", By.CssSelector("#facility-relationships"), _driver); } }
        public UIItem byFaclityRelationshipPage { get { return new UIItem("Facility relationship settings", By.CssSelector("#facility-relationship-setting"), _driver); } }
        public UIItem byAlertEmpty { get { return new UIItem("No relationship", By.CssSelector(".alert--empty"), _driver); } }
        public UIItem byAccountInfo { get { return new UIItem("Account Information", By.CssSelector("#account-information-details"), _driver); } }
        public UIItem contactInformationTab { get { return new UIItem("ManageUser >> ContactInformationTab", By.CssSelector("#account-information"), _driver); } }
        public UIItem settingsTab { get { return new UIItem("ManageUser >> SettingsTab", By.CssSelector("#account-settings"), _driver); } }
        public UIItem applicationAccessTab { get { return new UIItem("ManageUser >> ApplicationAccessTab", By.CssSelector("#permission"), _driver); } }
        public UIItem changePasswordTab { get { return new UIItem("ManageUser >> ChangePasswordTab", By.CssSelector("#change-password"), _driver); } }
        public UIItem EmulateUserButton { get { return new UIItem("Admin Page>> SearchResultHeader", By.Id("emulate-user"), _driver); } }
        public UIItem ShowAccountOptions { get { return new UIItem("Admin Page>> SearchResultHeader", By.Id("show-account-options"), _driver); } }
        public UIItem AccountInfo { get { return new UIItem("Admin Page>> SearchResultHeader", By.Id("account-information"), _driver); } }
        public UIItem byUserPicture { get { return new UIItem("User picture image", By.CssSelector("#user-picture-image"), _driver); } }
        public UIItem byChangePasswordTab { get { return new UIItem("Change Password tab", By.CssSelector("#change-password"), _driver); } }        
        public UIItem byUserName { get { return new UIItem("", By.CssSelector("#user-title"), _driver); } }
        public UIItem byProfilePicture { get { return new UIItem("", By.CssSelector("#user-picture-image"), _driver); } }
        public UIItem byCompanyName { get { return new UIItem("", By.CssSelector("#user-company-department-title"), _driver); } }
        public UIItem byWrkPhn { get { return new UIItem("", By.XPath(".//*[@id='user-details-view']/div[2]/div/dl[1]/dt"), _driver); } }
        public UIItem byHmPhn { get { return new UIItem("", By.XPath(".//*[@id='user-details-view']/div[2]/div/dl[2]/dt"), _driver); } }
        public UIItem byMobPh { get { return new UIItem("", By.XPath(".//*[@id='user-details-view']/div[2]/div/dl[3]/dt"), _driver); } }
        public UIItem byFax { get { return new UIItem("", By.XPath(".//*[@id='user-details-view']/div[2]/div/dl[4]/dt"), _driver); } }
        public UIItem byWorkEmail { get { return new UIItem("", By.XPath(".//*[@id='user-details-view']/div[2]/div/dl[5]/dt"), _driver); } }
        public UIItem byAltEmail { get { return new UIItem("", By.XPath(".//*[@id='user-details-view']/div[2]/div/dl[6]/dt"), _driver); } }
        public UIItem byAccStatus { get { return new UIItem("", By.XPath(".//*[@id='user-details-view']/div[2]/div/dl[7]/dt"), _driver); } }
        public UIItem byStatus { get { return new UIItem("", By.XPath(".//*[@id='user-details-view']/div[2]/div/dl[8]/dt"), _driver); } }
        public UIItem byUsrNme { get { return new UIItem("", By.XPath(".//*[@id='user-details-view']/div[2]/div/dl[9]/dt"), _driver); } }
        public UIItem byEmulateBtn { get { return new UIItem("", By.CssSelector("#emulate-user"), _driver); } }
        public UIItem byAppTitle { get { return new UIItem("", By.CssSelector("#app-title"), _driver); } }
        
        public UIItem CurrentPassword { get { return new UIItem("Admin>> ManageUser>> Password>> CurrentPassword", this.byCurPwd, _driver); } }
        public UIItem byEditProfilebutton { get { return new UIItem("edit-profile", By.CssSelector("#edit-profile"), _driver); } }
        public UIItem bySaveButton { get { return new UIItem("save-profile", By.CssSelector("#save"), _driver); } }
        public UIItem byCancelEditButton { get { return new UIItem("cancel-profile", By.CssSelector("#cancel"), _driver); } }
        public UIItem workPhone { get { return new UIItem("Work Phone", By.CssSelector("#work-phone"), _driver); } }
        public UIItem mobilePhone { get { return new UIItem("Work Phone", By.CssSelector("#mobile-phone"), _driver); } }
        public UIItem homePhone { get { return new UIItem("Work Phone", By.CssSelector("#home-phone"), _driver); } }
        public UIItem faxPhone { get { return new UIItem("Work Phone", By.CssSelector("#fax-phone"), _driver); } }
        public UIItem alternateEmail { get { return new UIItem("Alternate Email", By.CssSelector("#alternate-email"), _driver); } }
        public UIItem workEmail { get { return new UIItem("Work Email", By.CssSelector("#work-email"), _driver); } }
        public UIItem workPhoneCountryCode { get { return new UIItem("Work phone country code", By.CssSelector("#work-phone-country-code"), _driver); } }
        public UIItem workPhoneNumber { get { return new UIItem("Work phone number", By.CssSelector("#work-phone-number"), _driver); } }
        public UIItem workPhoneExtension { get { return new UIItem("Work phone extension", By.CssSelector("#work-phone-extension"), _driver); } }
        public UIItem mobilePhoneCountryCode { get { return new UIItem("Mobile phone country code", By.CssSelector("#mobile-phone-country-code"), _driver); } }
        public UIItem mobilePhoneNumber { get { return new UIItem("Mobile phone number", By.CssSelector("#mobile-phone-number"), _driver); } }
        public UIItem mobilePhoneExtension { get { return new UIItem("Mobile phone extension", By.CssSelector("#mobile-phone-extension"), _driver); } }
        public UIItem homePhoneCountryCode { get { return new UIItem("Home phone country code", By.CssSelector("#home-phone-country-code"), _driver); } }
        public UIItem homePhoneNumber { get { return new UIItem("Home phone number", By.CssSelector("#home-phone-number"), _driver); } }
        public UIItem homePhoneExtension { get { return new UIItem("Home phone extension", By.CssSelector("#home-phone-extension"), _driver); } }
        public UIItem faxPhoneCountryCode { get { return new UIItem("Fax phone country code", By.CssSelector("#fax-phone-country-code"), _driver); } }
        public UIItem faxPhoneNumber { get { return new UIItem("Fax phone number", By.CssSelector("#fax-phone-number"), _driver); } }
        public UIItem faxPhoneExtension { get { return new UIItem("Fax phone extension", By.CssSelector("#fax-phone-extension"), _driver); } }
        public UIItem editProfileHeader { get { return new UIItem("Edit profile header", By.CssSelector("#user-details-view>div.grid__cell>div.panel__heading>h2.panel__title"), _driver); } }

        By byCurPwd = By.CssSelector("#existing-password");
        By byNwPwd = By.CssSelector("#new-password");
        By byCnfNwPwd = By.CssSelector("#confirm-new-password");
        By byChnPwdBtn = By.CssSelector("#cui-reset-password");

        public UIItem CurrentPassword1 { get { return new UIItem("Admin>> ManageUser>> Password>> CurrentPassword", this.byCurPwd, _driver); } }
        public UIItem NewPassword { get { return new UIItem("Admin>> ManageUser>> Password>> NewPassword", this.byNwPwd, _driver); } }
        public UIItem ConfirmNewPassword { get { return new UIItem("Admin>> ManageUser>> Password>> ConfrimNewPassword", this.byCnfNwPwd, _driver); } }
        public UIItem ChangePassword_ { get { return new UIItem("Admin>> ManageUser>> Password>> ChangePassword", this.byChnPwdBtn, _driver); } }

        By byChnPwdTab = By.CssSelector("#change-password");
        public UIItem ChangePasswordTab { get { return new UIItem("Admin>> ManageUser>> Password>> ChangePassword", this.byChnPwdTab, _driver); } }

        public UIItem byDeleteButton { get { return new UIItem("Delete symbol", By.CssSelector(".hook--delete-relationship"), _driver); } }

        public UIItem byFacilitySearch { get { return new UIItem("Facility Search box", By.CssSelector("#facility-search"), _driver); } }
        public UIItem byAddRelationship { get { return new UIItem("Add relationship button", By.CssSelector("#add-relationship"), _driver); } }
        public UIItem byRelationshipType { get { return new UIItem("Relationship type", By.CssSelector("#relationship-type"), _driver); } }

        By byeditprofilebtn = By.CssSelector("#edit-profile");
        By byeditprofilemodal = By.CssSelector("#modal-inner-container");
        By byeditprofileform = By.CssSelector("#validate-user-form");
        By byinputpassword = By.CssSelector("#input-password");
        By byeditprofilemodalsubmitbtn = By.XPath(".//*[@id='validate-user-form']/ul/li[3]/button[1]");
        By byeditprofilemodalcancelbtn = By.XPath(".//*[@id='validate-user-form']/ul/li[3]/button[2]");
        By byupdatecontactsavebtn = By.XPath(".//*[@id='user-details-view']/div/div[2]/form/ul/li[7]/button[1]");
        By byupdatecontactcancelbtn = By.XPath(".//*[@id='user-details-view']/div/div[2]/form/ul/li[7]/button[2]");
        By byuploadpicturebtn = By.CssSelector("#upload-user-picture-button");


        public UIItem byEditProfileBtn { get { return new UIItem("Claw>> Admin Tab", this.byeditprofilebtn, _driver); } }
        public UIItem byEditProfileModal { get { return new UIItem("Claw>> Admin Tab", this.byeditprofilemodal, _driver); } }
        public UIItem byEditProfileForm { get { return new UIItem("Claw>> Admin Tab", this.byeditprofileform, _driver); } }
        public UIItem byEditProfileModalSubmitBtn { get { return new UIItem("Claw>> Admin Tab", this.byeditprofilemodalsubmitbtn, _driver); } }
        public UIItem byEditProfileModalCancelBtn { get { return new UIItem("Claw>> Admin Tab", this.byeditprofilemodalcancelbtn, _driver); } }
        public UIItem byUpdateContactSaveBtn { get { return new UIItem("Claw>> Admin Tab", this.byupdatecontactsavebtn, _driver); } }
        public UIItem byInputPassword { get { return new UIItem("Claw>> Admin Tab", this.byinputpassword, _driver); } }
        public UIItem byUpdateContactCancelBtn { get { return new UIItem("Claw>> Admin Tab", this.byupdatecontactcancelbtn, _driver); } }
        public UIItem byEditProfileBtbyUploadPictureBtnn { get { return new UIItem("Claw>> Admin Tab", this.byuploadpicturebtn, _driver); } }
        public UIItem byUploadPictureBtn { get { return new UIItem("Claw>> Admin Tab", this.byuploadpicturebtn, _driver); } }
        public UIItem bySettingTab { get { return new UIItem("Setting tab", By.CssSelector("#account-settings"), _driver); } }
        public UIItem byProfilePage { get { return new UIItem("Profile page", By.CssSelector("#user-management-main"), _driver); } }

        By bynextpage = By.CssSelector("#next-page");
        public UIItem byNextPage { get { return new UIItem("Admin Page>> SearchResultHeader", this.bynextpage, _driver); } }
        public UIItem byNumResultInPage{get{return new UIItem("User picture image", By.XPath(".//*[@id='result-range-container']/div/strong[1]"), _driver); }}
        public  UIItem byNumResultInTotal{get {return new UIItem("User picture image", By.XPath(".//*[@id='result-range-container']/div/strong[2]"), _driver);}}
       // public UIItem byDeleteButton { get { return new UIItem("Delete symbol", By.CssSelector(".hook--delete-relationship"), _driver); } }
        public UIItem byNote { get { return new UIItem("Notes", By.XPath("//*[@id='facility-relationship-setting']/div/div[2]/p"), _driver); } }

    }
}
