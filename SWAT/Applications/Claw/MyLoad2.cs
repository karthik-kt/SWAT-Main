using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Net;
using System.IO;
using SWAT.Data;
using SWAT.Logger;
using SWAT.FrameWork.Utilities;
using SWAT.Configuration;
using SWAT.Applications.Claw.ObjectRepository;
using Newtonsoft.Json;

namespace SWAT.Applications
{
    public class UILoad
    {
        public string ObjectName { get; set; }
        public string LocatorType { get; set; }
        public string LocatorString { get; set; }
    }

    internal class MyLoad2Page
    {
        IWebDriver _driver;

        internal MyLoad2Page(TestStartInfo teststartinfo)
        {
            ICollection<UILoad> UILoads = JsonConvert.DeserializeObject<ICollection<UILoad>>(File.ReadAllText(@"c:\UIMap\MyLoad2.json"));
            Dictionary<string, UIItem> test = new Dictionary<string, UIItem>();
            _driver = teststartinfo.Driver;
            foreach (UILoad uiload in UILoads)
            {
                switch (uiload.LocatorType.ToLower())
                {
                    case "cssselector":
                        test.Add(uiload.ObjectName, new UIItem(uiload.ObjectName, By.CssSelector(uiload.LocatorString),_driver));
                        break;
                    case "xpath":
                        test.Add(uiload.ObjectName, new UIItem(uiload.ObjectName, By.XPath(uiload.LocatorString), _driver));
                        break;
                    case "linktext":
                        test.Add(uiload.ObjectName, new UIItem(uiload.ObjectName, By.LinkText(uiload.LocatorString), _driver));
                        break;
                }
            }            
            this.UIMap = test;
        }
        internal UIItem Fields(string Field)
        {
            return this.UIMap[Field];
        }        
        internal Dictionary<string, UIItem> UIMap {set;get; }
                
    }
    internal class MyLoad2
    {
        internal Dictionary<string, string> testdata;
        internal MyLoad2Page _MyLoad2Page;
        ISearchContext _driver;
        List<bool> result;
        internal MyLoad2(TestStartInfo teststartinfo,DataManager datamanager)
        {
            testdata = datamanager.ToDictionary();
            _MyLoad2Page = new MyLoad2Page(teststartinfo);
            result = new List<bool>();
            _driver = teststartinfo.Driver;
        }

        internal string NavigateTo()
        {
            try
            {
                //Action
                //Wait
                //Verification
                return "Done";
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal string UIVerify()
        {
            try
            {
                foreach(KeyValuePair<string,string> item in testdata)
                {
                    result.Add(_MyLoad2Page.Fields(item.Key).UIVerify(item.Value.ToString()));
                }
                if (result.Any(p => p == false))
                    return "Failed";
                return "Done";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }

        internal string Execute()
        {
            try
            {
                foreach (KeyValuePair<string, string> item in testdata)
                {
                    result.Add(_MyLoad2Page.Fields(item.Key).Execute(item.Value.ToString()));
                }
                if(result.FirstOrDefault(p => p == false))
                    return "Failed";
                return "Done";
            }
            catch (KeyNotFoundException)
            {
                MyLogger.Log("Field name is incorrect/not-mapped to UI map.");
                return "Failed";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }

    }


}
