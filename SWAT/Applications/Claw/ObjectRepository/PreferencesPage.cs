using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SWAT.FrameWork.Utilities;
using SWAT.Configuration;

namespace SWAT.Applications.Claw.ObjectRepository
{
    class PreferencesPage : Page
    {
        public PreferencesPage(IWebDriver driver)
        {
            _driver = driver;
            url = "#preferences";
            //navigationlink = "#";
        }

        public PreferencesPage(TestStartInfo teststartinfo)
        {
            _driver = teststartinfo.Driver;
            _baseurl = teststartinfo.BaseURL;
            url = "#preferences";
        }

        private By preferencesTab = By.LinkText("Preferences");
        private By appTitle = By.CssSelector("#app-title");
        private By modesHeader = By.CssSelector("#preferences-modes");
        private By servicesHeader = By.CssSelector("#preferences-services");
        private By equipmentServiceHeader = By.CssSelector("#preferences-equipmentService");
        private By certificationLicenseHeader = By.CssSelector("#preferences-certificationLicense");
        private By ownershipHeader = By.CssSelector("#preferences-ownership");
        private By equipmentHeader = By.CssSelector("#preferences-equipment");
        private By authoritiesHeader = By.CssSelector("#preferences-authorities");
        private By dispatchingHoursHeader = By.CssSelector("#preferences-dispatchingHours");
        private By preferredlaneHeader = By.CssSelector("#preferences-preferredlane");
        private By paymentDocumentsHeader = By.CssSelector("#preferences-paymentDocuments");

        //Modes Section
        private By modesAir = By.CssSelector("#modes-air");
        private By modesIntermodal = By.CssSelector("#modes-imdl");
        private By modesLTL = By.CssSelector("#modes-ltl");
        private By modesOcean = By.CssSelector("#modes-ocean");
        private By modesRail = By.CssSelector("#modes-rail");
        private By modesTL = By.CssSelector("#modes-tl");
        private By modesSave = By.CssSelector("#save-modes-changes");
        private By modesSaveAlert = By.CssSelector("#save-modes-alert");

        //Available Equipment Section
        private By equipmentInputTotalPowerUnits = By.CssSelector("#input-totalPowerUnits");
        private By equipmentInputTotalDrivers = By.CssSelector("#input-totalDrivers");
        private By equipmentSave = By.CssSelector("#save-equipments-changes");
        private By equipmentSaveAlert = By.CssSelector("#save-equipment-alert");

        //Preferredlane Section
        private By _preferredlane = By.XPath(".//*[@id='view--preferredlane-itemview']/tr");
        private By preferredlaneAdd = By.CssSelector("#add-lane");
        private By preferredlaneSave = By.CssSelector("#save-lanes-changes");
        private By preferredlaneSaveAlert = By.CssSelector("#save-preferred-lane-alert");


        public UIItem PreferencesTab { get { return new UIItem("Preferences >> Tab", this.preferencesTab, _driver); } }
        public UIItem AppTitle { get { return new UIItem("Preferences >> AppTitle", this.appTitle, _driver); } }
        public UIItem ModesHeader { get { return new UIItem("Preferences >> Modes Header", this.modesHeader, _driver); } }
        public UIItem ServicesHeader { get { return new UIItem("Preferences >> Services Header", this.servicesHeader, _driver); } }
        public UIItem EquipmentServiceHeader { get { return new UIItem("Preferences >> Equipment Service Header", this.equipmentServiceHeader, _driver); } }
        public UIItem CertificationLicenseHeader { get { return new UIItem("Preferences >> Certification License Header", this.certificationLicenseHeader, _driver); } }
        public UIItem OwnershipHeader { get { return new UIItem("Preferences >> Ownership Header", this.ownershipHeader, _driver); } }
        public UIItem EquipmentHeader { get { return new UIItem("Preferences >> Equipment Header", this.equipmentHeader, _driver); } }
        public UIItem AuthoritiesHeader { get { return new UIItem("Preferences >> Authorities Header", this.authoritiesHeader, _driver); } }
        public UIItem DispatchingHoursHeader { get { return new UIItem("Preferences >> Dispatching Hours Header", this.dispatchingHoursHeader, _driver); } }
        public UIItem PreferredlaneHeader { get { return new UIItem("Preferences >> Preferredlane Header", this.preferredlaneHeader, _driver); } }
        public UIItem PaymentDocumentsHeader { get { return new UIItem("Preferences >> Payment Documents Header", this.paymentDocumentsHeader, _driver); } }

        public UIItem ModesAir { get { return new UIItem("Preferences >> Modes Air", this.modesAir, _driver); } }
        public UIItem ModesIntermodal { get { return new UIItem("Preferences >> Modes Intermodal", this.modesIntermodal, _driver); } }
        public UIItem ModesLTL { get { return new UIItem("Preferences >> Modes LTL", this.modesLTL, _driver); } }
        public UIItem ModesOcean { get { return new UIItem("Preferences >> Modes Ocean", this.modesOcean, _driver); } }
        public UIItem ModesRail { get { return new UIItem("Preferences >> Modes Rail", this.modesRail, _driver); } }
        public UIItem ModesTL { get { return new UIItem("Preferences >> Modes TL", this.modesTL, _driver); } }
        public UIItem ModesSave { get { return new UIItem("Preferences >> Modes Save", this.modesSave, _driver); } }
        public UIItem ModesSaveAlert { get { return new UIItem("Preferences >> Modes Save Alert", this.modesSaveAlert, _driver); } }

        public UIItem EquipmentInputTotalPowerUnits { get { return new UIItem("Preferences >> Equipment Input TotalPowerUnits", this.equipmentInputTotalPowerUnits, _driver); } }
        public UIItem EquipmentInputTotalDrivers { get { return new UIItem("Preferences >> Equipment Input TotalDrivers", this.equipmentInputTotalDrivers, _driver); } }
        public UIItem EquipmentSave { get { return new UIItem("Preferences >> Equipment Save", this.equipmentSave, _driver); } }
        public UIItem EquipmentSaveAlert { get { return new UIItem("Preferences >> Equipment Save Alert", this.equipmentSaveAlert, _driver); } }


        public UIItem PreferredlaneAdd { get { return new UIItem("Preferences >> Preferredlane Add", this.preferredlaneAdd, _driver); } }
        public UIItem PreferredlaneSave { get { return new UIItem("Preferences >> Preferredlane Save", this.preferredlaneSave, _driver); } }
        public UIItem PreferredlaneSaveAlert { get { return new UIItem("Preferences >> Preferredlane Save Alert", this.preferredlaneSaveAlert, _driver); } }
        private UIItem preferredlane { get { return new UIItem("Create Order>> Shipping Unit>> ", this._preferredlane, _driver); } }
        public List<Preferredlane> Preferredlanes
        {
            get
            {
                List<Preferredlane> preferredlanes = new List<Preferredlane>();
                foreach (var element in this.preferredlane.FindElements())
                {
                    preferredlanes.Add(new Preferredlane(element));
                }
                return preferredlanes;
            }
        }

    }

    public class Preferredlane
    {
        private IWebElement _driver;

        public Preferredlane(IWebElement element)
        {
            _driver = element;
        }

        private By preferredlaneOriginInput = By.CssSelector("#preferred-lane-orig-input");
        private By preferredlaneDestinationInput = By.CssSelector("#preferred-lane-dest-input");
        private By preferredlaneEquipmentType = By.CssSelector(".one-whole.hook--editable.hook--type");
        private By preferredlaneCapacity = By.CssSelector(".hook--lane-capacity.one-whole.hook--editable.hook--laneCapacity");
        private By preferredlaneDelete = By.CssSelector(".text-link.hook--include-delete");

        public UIItem PreferredlaneOriginInput { get { return new UIItem("Preferences >> Preferredlane OriginInput", this.preferredlaneOriginInput, _driver); } }
        public UIItem PreferredlaneDestinationInput { get { return new UIItem("Preferences >> Preferredlane DestinationInput", this.preferredlaneDestinationInput, _driver); } }
        public UIItem PreferredlaneEquipmentType { get { return new UIItem("Preferences >> Preferredlane EquipmentType", this.preferredlaneEquipmentType, _driver); } }
        public UIItem PreferredlaneCapacity { get { return new UIItem("Preferences >> Preferredlane Capacity", this.preferredlaneCapacity, _driver); } }
        public UIItem PreferredlaneDelete { get { return new UIItem("Preferences >> Preferredlane Delete", this.preferredlaneDelete, _driver); } }
    }
}
