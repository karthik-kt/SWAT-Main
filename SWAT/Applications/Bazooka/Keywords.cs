using System;
using System.IO;
using SWAT.Data;
using SWAT.Logger;
using System.Collections.Generic;
using SWAT.Configuration;
using SWAT.Applications.Claw.DAL;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SWAT.Applications.Bazooka
{
    class Keywords
    {
        public TestStartInfo testConfig { get; set; }

        public Keywords(TestStartInfo testConfig)
        {
            this.testConfig = testConfig;            
        }

        public string ExecuteTestStep_Old(string action, DataManager datamanager)
        {
            try
            {
                action = action.Replace("BAZOOKA.", "");
                string actualResult = "StepNotExecuted";
                KeywordProcesser bazookakw = new KeywordProcesser(testConfig);
                Dictionary<string, string> testparameters = new Dictionary<string, string>();
                XmlDocument wtf_testfile = new XmlDocument();
                string wtf_testfilepath = Path.Combine(ConfigManager.BazookaTestFile, "SWAT_" + action + ".xml");
                wtf_testfile.Load(wtf_testfilepath);
                XDocument document = XDocument.Load(wtf_testfilepath);
                IEnumerable<XElement> NameElements = document.Root.Element("Iterations")
                                                                    .Element("Iteration").Element("Data")
                                                                    .Element("TestData").Element("Parameters")
                                                                    .Elements("Parameter").Elements("Name");
                IEnumerable<string> testdata = NameElements.Select(i => i.Value).ToList();
                testparameters.Add("UserName", "manivas.murugaiah");
                testparameters.Add("PassWord", "#infy002");
                foreach (var data in testdata)
                {
                    if (datamanager.Data(data) != Constants.Ignore)
                        testparameters.Add(data, datamanager.Data(data));
                }
                bazookakw.StartNewInstence = true;
                bazookakw.ShutdownOnCompletion = true;
                actualResult = bazookakw.Execute(action, testparameters);
                return actualResult;
            }
            catch (Exception ex)
            {
                MyLogger.Log(ex.Message);
                return "Failed";
            }
        }

        public string ExecuteTestStep(string action, DataManager datamanager)
        {
            try
            {
                action = action.Replace("BAZOOKA.", "");
                string actualResult = "StepNotExecuted";
                KeywordProcesser bazookakw = new KeywordProcesser(testConfig);                
                string wtf_testfilepath = Path.Combine(ConfigManager.BazookaTestFile, "SWAT_" + action + ".xml");
                string wtf_temptestfilepath = Path.Combine(Path.GetTempPath(), "SWAT_" + action + ".xml");
                Dictionary<string, string> testparameters = new Dictionary<string, string>();
                XmlDocument wtf_testfile = new XmlDocument();
                wtf_testfile.Load(wtf_testfilepath);
                XDocument document = XDocument.Load(wtf_testfilepath);
                testparameters.Add("UserName", "manivas.murugaiah");
                testparameters.Add("PassWord", "#infy002");
                bazookakw.StartNewInstence = true;
                bazookakw.ShutdownOnCompletion = true;     
                //3. Get all the test data name.
                IEnumerable<XElement> NameElements = document.Root.Element("Iterations")
                                                                        .Element("Iteration").Element("Data")
                                                                        .Element("TestData").Element("Parameters")
                                                                        .Elements("Parameter").Elements("Name");
                    IList<string> testdata = NameElements.Select(p => p.Value).ToList();
                    foreach (var data in testdata)
                    {
                        // 4. Update the WTF test parameter from SWAT datamanager.
                        string currenttestdata = datamanager.Data(data);
                        if (currenttestdata != Constants.Ignore)
                            testparameters.Add(data, datamanager.Data(data));
                        else
                        {
                            // 5. Remove the test data which are marked as '!IGNORE!' in SWAT
                            //Dont update the test file if its login / shadown.
                            if (data != "UserName" && data != "PassWord")
                            {
                                string testmodelselector = "//Steps/StepModel[contains(Data,'" + data + "')]";
                                XmlNodeList testmodelNodeList = wtf_testfile.SelectNodes(testmodelselector);
                                XmlNode nodetoremove = testmodelNodeList[0];
                                if (nodetoremove != null)
                                {
                                    wtf_testfile.ChildNodes[1].ChildNodes[10].RemoveChild(nodetoremove);
                                }
                            }
                        }
                    }
                    //6. Save the test file in users temp folder.
                    wtf_testfile.Save(wtf_temptestfilepath);
                //}
                actualResult = bazookakw.Execute(action, testparameters);
                return actualResult;
            }
            catch(Exception ex)
            {
                MyLogger.Log(ex.Message);
                return "Failed";
            }
        }

        public string ExecuteTestStep(string action, DataManager datamanager, bool complete)
        {
            try
            {
                KeywordProcesser BazookaKW = new KeywordProcesser(testConfig);
                action = action.Replace("BAZOOKA.", "");
                Dictionary<string, string> testparameters = new Dictionary<string, string>();
                testparameters.Add("UserName", "manivas.murugaiah");
                testparameters.Add("PassWord", "#infy002");
                switch (action)
                {
                    case "ZOOKANDTENDERLOAD":
                        //testparameters.Add("ReferenceNumber", "ref" + datamanager.Data("Reference #"));
                        testparameters.Add("ReferenceNumber", datamanager.Data("Load #"));
                        testparameters.Add("ShipperNotes", datamanager.Data("Notes"));
                        break;
                    case "TENDERLOAD":
                        testparameters.Add("ReferenceNumber", datamanager.Data("Reference #"));
                        testparameters.Add("Notes", datamanager.Data("Notes"));
                        break;
                    case "ZOOKLOAD_VALIDATEARRIVALTIME":
                        testparameters.Add("LoadID", datamanager.Data("Load #"));                        
                        testparameters.Add("ArriveDate", DateTime.Now.AddDays(-1).ToString("MM/dd").ToString());
                        testparameters.Add("ArriveTime", "10:00");
                        testparameters.Add("DepartDate", DateTime.Now.AddDays(-1).ToString("MM/dd").ToString());
                        testparameters.Add("DepartTime", "11:00");
                        break;
                    case "ZOOKANDVERIFICATIONSCHEDULE":
                        ScheduleLoadsData _scheduleloadsdata = new ScheduleLoadsData(datamanager);
                        string Stop1_ScheduleOpen_Date = DateTime.Parse(_scheduleloadsdata.Pickup_Date).ToString("MM/dd");
                        string Stop1_ScheduleClose_Date = Stop1_ScheduleOpen_Date;
                        string Stop1_ScheduleOpen_Time = _scheduleloadsdata.Pickup_Time;
                        string Stop1_ScheduleClose_Time = Stop1_ScheduleOpen_Time;
                        string Stop2_ScheduleOpen_Date = DateTime.Parse(_scheduleloadsdata.Delivery_Date).ToString("MM/dd");
                        string Stop2_ScheduleClose_Date = Stop2_ScheduleOpen_Date;
                        string Stop2_ScheduleOpen_Time = _scheduleloadsdata.Delivery_Time;
                        string Stop2_ScheduleClose_Time = Stop2_ScheduleOpen_Time;

                        testparameters.Add("LoadID", datamanager.Data("Load #"));
                        testparameters.Add("Stop1_ScheduleOpen_Date", Stop1_ScheduleOpen_Date);
                        testparameters.Add("Stop1_ScheduleClose_Date", Stop1_ScheduleClose_Date);
                        testparameters.Add("Stop1_ScheduleOpen_Time", Stop1_ScheduleOpen_Time);                       
                        testparameters.Add("Stop1_ScheduleClose_Time", Stop1_ScheduleClose_Time);
                        testparameters.Add("Stop2_ScheduleOpen_Date", Stop2_ScheduleOpen_Date);
                        testparameters.Add("Stop2_ScheduleClose_Date", Stop2_ScheduleClose_Date);
                        testparameters.Add("Stop2_ScheduleOpen_Time", Stop2_ScheduleOpen_Time);
                        testparameters.Add("Stop2_ScheduleClose_Time", Stop2_ScheduleClose_Time);
                        break;
                    case "ZOOKANDVERIFICATIONSCHEDULE_TIME":
                        _scheduleloadsdata = new ScheduleLoadsData(datamanager);
                         Stop1_ScheduleOpen_Date = DateTime.Parse(_scheduleloadsdata.Pickup_Date).ToString("MM/dd");
                         Stop1_ScheduleClose_Date = Stop1_ScheduleOpen_Date;
                         Stop1_ScheduleOpen_Time = _scheduleloadsdata.Pickup_Time;
                         Stop1_ScheduleClose_Time = Stop1_ScheduleOpen_Time;
                         Stop2_ScheduleOpen_Date = DateTime.Parse(_scheduleloadsdata.Delivery_Date).ToString("MM/dd");
                         Stop2_ScheduleClose_Date = Stop2_ScheduleOpen_Date;
                         Stop2_ScheduleOpen_Time = _scheduleloadsdata.Delivery_Time;
                         Stop2_ScheduleClose_Time = Stop2_ScheduleOpen_Time;

                        testparameters.Add("LoadID", datamanager.Data("Load #"));
                        testparameters.Add("Stop1_ScheduleOpen_Date", Stop1_ScheduleOpen_Date);
                        testparameters.Add("Stop1_ScheduleClose_Date", Stop1_ScheduleClose_Date);
                        testparameters.Add("Stop1_ScheduleOpen_Time", Stop1_ScheduleOpen_Time);
                        testparameters.Add("Stop1_ScheduleClose_Time", Stop1_ScheduleClose_Time);
                        testparameters.Add("Stop2_ScheduleOpen_Date", Stop2_ScheduleOpen_Date);
                        testparameters.Add("Stop2_ScheduleClose_Date", Stop2_ScheduleClose_Date);
                        testparameters.Add("Stop2_ScheduleOpen_Time", Stop2_ScheduleOpen_Time);
                        testparameters.Add("Stop2_ScheduleClose_Time", Stop2_ScheduleClose_Time);
                        break;
                    case "ZOOKLOADANDECONFIRM":
                        testparameters.Add("LoadID", datamanager.Data("Load #"));
                        break;
                    case "VERIFYORIGINFACILITYADDRESS":
                        testparameters.Add("LoadId", datamanager.Data("Load #"));
                        testparameters.Add("FaclityName", datamanager.Data("OrginFacility"));
                        testparameters.Add("Address", datamanager.Data("OrginFacilityAddress"));
                        testparameters.Add("CityAndState", datamanager.Data("OriginCityAndState"));
                        testparameters.Add("Zip", datamanager.Data("OrginZip"));
                        break;
                    case "VERIFYDESTINATIONFACILITYADDRESS":
                        testparameters.Add("LoadId", datamanager.Data("Load #"));
                        testparameters.Add("FaclityName", datamanager.Data("DestinationFacility"));
                        testparameters.Add("Address", datamanager.Data("DestinationFacilityAddress"));
                        testparameters.Add("CityAndState", datamanager.Data("DestinationCityAndState"));
                        testparameters.Add("Zip", datamanager.Data("DestinationZip"));
                        break;
                    case "ALLOWDUPLICATESHIPMENTID":
                        testparameters.Add("PartnerName", datamanager.Data("PartnerName"));
                        break;
                    case "DISALLOWDUPLICATESHIPMENTID":
                        testparameters.Add("PartnerName", datamanager.Data("PartnerName"));
                        break;
                    case "VERIFYTARPDETAILS":
                        testparameters.Add("LoadId", datamanager.Data("Load #"));
                        testparameters.Add("TarpType", datamanager.Data("TarpType"));
                        testparameters.Add("TarpQuantity", datamanager.Data("TarpQuantity"));
                        break;
                    case "VERIFYEXPECTEDPAYDATE":
                        testparameters.Add("LoadId", datamanager.Data("Load #"));
                        testparameters.Add("PayDate", datamanager.Data("PayDate"));
                        break;
                    case "ZOOKANDTENDERSPOTLOAD":
                        testparameters.Add("LoadId", datamanager.Data("Load #"));
                        testparameters.Add("LoadType", "Managed");
                        testparameters.Add("OverridTenderMethod", "Spot");
                        testparameters.Add("StateActive", "Active");
                        break;
                    default:
                        return "StepNotImplemented";
                }
                BazookaKW.StartNewInstence = true;
                BazookaKW.ShutdownOnCompletion = true;
                string result = BazookaKW.Execute(action, testparameters);
                return result;
            }
            catch(Exception ex)
            {
                MyLogger.Log(ex.Message);
                return "Failed";
            }
        }

        #region Run range test cases.

        private KeywordProcesser _bazookaKW { get; set; }

        private Dictionary<string, string> _testparameters { get; set; }

        private DataManager _datamanager { get; set; }

        private string _action { get; set; }

        private bool ExecuteTest()
        {
            try
            {
                if ("Succeeded" == _bazookaKW.Execute(_action, _testparameters))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        private string TenderSpotLoad()
        {
            try
            {
                _bazookaKW.StartingStep = 1;
                _bazookaKW.EndingStep = 3;
                _bazookaKW.ShouldCreateResultFiles = false;
                _testparameters.Add("TotalRate", _datamanager.Data("Cost"));
                _testparameters.Add("LoadID", _datamanager.Data("Load #"));
                _testparameters.Add("Notes", _datamanager.Data("Notes"));
                Assert.IsTrue(ExecuteTest());
                return "LoadZooked";
            }
            catch
            {
                return "LoadOpenFailed";
            }
        }

        private string ZookLoad()
        {
            try
            {
                _bazookaKW.StartingStep = 2;
                _bazookaKW.EndingStep = 3;
                _bazookaKW.ShouldCreateResultFiles = false;
                _bazookaKW.StartNewInstence = false;
                _testparameters.Add("LoadID", _datamanager.Data("Load #"));
                Assert.IsTrue(ExecuteTest());
                return "LoadZooked";
            }
            catch
            {
                return "Failed";
            }
        }

        private string ZookAndTenderLoad()
        {
            try
            {
                _bazookaKW.StartingStep = 4;
                _bazookaKW.EndingStep = 7;
                _bazookaKW.ShouldCreateResultFiles = false;
                _testparameters.Add("ReferenceNumber", _datamanager.Data("Load #"));
                _testparameters.Add("ShipperNotes", _datamanager.Data("Notes"));
                Assert.IsTrue(ExecuteTest());
                return "TenderedLoad";
            }
            catch
            {
                return "FailedToTendered";
            }
        }

        private string Login()
        {
            try
            {
                _bazookaKW.StartNewInstence = true;
                _bazookaKW.StartingStep = 1;
                _bazookaKW.EndingStep = 3;
                _bazookaKW.ShouldCreateResultFiles = true;
                _testparameters.Add("UserName", "manivas.murugaiah");
                _testparameters.Add("PassWord", "#infy002");
                Assert.IsTrue(ExecuteTest());
                return "LoginSuccess";
            }
            catch
            {
                return "LoginFailed";
            }
        }

        private string Shutdown()
        {
            try
            {
                _bazookaKW.ShutdownOnCompletion = true;
                _bazookaKW.StartingStep = 1;
                _bazookaKW.EndingStep = 2;
                Assert.IsTrue(ExecuteTest());
                return "ShutdownSuccess";
            }
            catch
            {
                return "ShutdownFailed";
            }
        }

        private string Close()
        {
            try
            {
                _bazookaKW.ShutdownOnCompletion = true;
                _bazookaKW.StartingStep = 1;
                _bazookaKW.EndingStep = 3;
                Assert.IsTrue(ExecuteTest());
                return "LoginSuccess";
            }
            catch
            {
                return "LoginFailed";
            }
        }

        private string VerifyTotalRate()
        {
            try
            {
                _bazookaKW.StartingStep = 2;
                _bazookaKW.EndingStep = 3;
                _bazookaKW.ShouldCreateResultFiles = false;
                _testparameters.Add("TotalRate", _datamanager.Data("Cost"));
                _testparameters.Add("CarrierName", _datamanager.Data("Carrier"));
                Assert.IsTrue(ExecuteTest());
                return "TenderedLoad";
            }
            catch
            {
                return "FailedToTendered";
            }
        }

        public string ExecuteBazStep(string action, DataManager datamanager)
        {
            try
            {
                string actualresults = null;
                _bazookaKW = new KeywordProcesser(testConfig);
                _datamanager = datamanager;
                _testparameters = new Dictionary<string, string>();
                _action = action.Replace("BAZ.", "");
                switch (_action)
                {
                    case "LOGIN":
                        actualresults = Login();
                        break;
                    case "ZOOKANDTENDERLOAD":
                        actualresults = ZookAndTenderLoad();
                        break;
                    case "ZOOKLOAD":
                        actualresults = ZookLoad();
                        break;
                    case "VERIFYTOTALRATE":
                        actualresults = VerifyTotalRate();
                        break;
                    case "SHUTDOWN":
                        actualresults = Shutdown();
                        break;
                    case "CLOSE":
                        actualresults = Close();
                        break;
                    case "TENDERSPOTLOAD":
                        actualresults = TenderSpotLoad();
                        break;
                    case "VERIFYLTLLOADCREATED":
                        actualresults = Close();
                        break;

                }
                _testparameters = null;
                return actualresults;
            }
            catch (Exception ex)
            {
                MyLogger.Log(ex.Message);
                return "Failed";
            }
        }
        #endregion
    }


}
