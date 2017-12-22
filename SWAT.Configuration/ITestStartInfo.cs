using System.Data;
using OpenQA.Selenium;

namespace SWAT.Configuration
{
    public interface ITestStartInfo 
    {
         string ResultPath { get;   }

         string SuitePath { get;  }

         string DataPath { get;   }

         string BaseURL { get;   }

         IWebDriver Driver { get;   }

         string Application { get;   }

         string Environment { get;   }

         string Browser { get;   }

         string Suite { get;   }

         DataTable ResultSheet { get;  }
    }
}
