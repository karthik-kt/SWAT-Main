using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWAT.Data;

namespace SWAT.Applications.Claw.DAL
{
    class UserData
    {
        //DataManager _datamanager;
        public UserData(DataManager datamanager)
        {
            UsersEmailid = datamanager.Data("UsersEmailid");
            SearchType = datamanager.Data("FullName");
            SearchBy = datamanager.Data("SearchBy");
            SearchVal = datamanager.Data(SearchBy);
            facility = datamanager.Data("FacilityToAdd");
            facilityToRemove = datamanager.Data("FacilityToRemove");
            relationshipType = datamanager.Data("RelationshipType");
            WorkEmail = datamanager.Data("WorkEmail");
            AlternateEmail = datamanager.Data("AlternateEmail");
            WorkPhoneCountryCode = datamanager.Data("WorkPhoneCountryCode");
            WorkPhoneNumber = datamanager.Data("WorkPhoneNumber");
            WorkPhoneExtension = datamanager.Data("WorkPhoneExtension");
            MobilePhoneCountryCode = datamanager.Data("MobilePhoneCountryCode");
            MobilePhoneNumber = datamanager.Data("MobilePhoneNumber");
            MobilePhoneExtension = datamanager.Data("MobilePhoneExtension");
            HomePhoneCountryCode = datamanager.Data("HomePhoneCountryCode");
            HomePhoneNumber = datamanager.Data("HomePhoneNumber");
            HomePhoneExtension = datamanager.Data("HomePhoneExtension");
            FaxPhoneCountryCode = datamanager.Data("FaxPhoneCountryCode");
            FaxPhoneNumber = datamanager.Data("FaxPhoneNumber");
            FaxPhoneExtension = datamanager.Data("FaxPhoneExtension");
            CheckValidation = datamanager.Data("CheckValidation");
            strFullName = datamanager.Data("FullName");
            strCompany = datamanager.Data("Company");
            currentPassword = datamanager.Data("Password");
            newPassword = datamanager.Data("NewPassword");
            confirmPassword = datamanager.Data("ConfirmPassword");
            userType = datamanager.Data("Type");
            SearchVal = strFullName;
            if (strCompany != "!IGNORE!")
                SearchVal = strFullName + " " + strCompany;
            contactInformationTabStatus = datamanager.Data("ContactInformation");
            settingsTabStatus = datamanager.Data("Settings");
            applicationAccessTabStatus = datamanager.Data("ApplicationAccess");
            changePasswordTabStatus = datamanager.Data("ChangePassword");
            //Help data
            SystemMessageTitle = datamanager.Data("SystemMessageTitle");
            SystemMessageBody = datamanager.Data("SystemMessageBody");
            TestFor = datamanager.Data("TestFor");
            //Help data ends
        }

        public string UsersEmailid{get;set;}
        public string SearchType{get;set;}
        public string SearchBy{get;set;}
        public string SearchVal{get;set;}
        public string strBaseURL{get;set;}
        public string strFullName{get;set;}
        public string strCompany{get;set;}
        public string facility{get;set;}
        public string relationshipType{get;set;}
        public string facilityToRemove{get;set;}
        public string userType{get;set;}
        public string WorkEmail{get;set;}
        public string AlternateEmail{get;set;}
        public string WorkPhoneCountryCode{get;set;}
        public string WorkPhoneNumber{get;set;}
        public string WorkPhoneExtension{get;set;}
        public string MobilePhoneCountryCode{get;set;}
        public string MobilePhoneNumber{get;set;}
        public string MobilePhoneExtension{get;set;}
        public string HomePhoneCountryCode{get;set;}
        public string HomePhoneNumber{get;set;}
        public string HomePhoneExtension{get;set;}
        public string FaxPhoneCountryCode{get;set;}
        public string FaxPhoneNumber{get;set;}
        public string FaxPhoneExtension{get;set;}
        public string CheckValidation{get;set;}
        public string contactInformationTabStatus{get;set;}
        public string settingsTabStatus{get;set;}
        public string applicationAccessTabStatus{get;set;}
        public string changePasswordTabStatus{get;set;}
        public string currentPassword{get;set;}
        public string newPassword{get;set;}
        public string confirmPassword{get;set;}
        //Help data
        public string SystemMessageTitle { get; set; }
        public string SystemMessageBody { get; set; }
        public string TestFor { get; set; }
        //Help data ends
    }
}
