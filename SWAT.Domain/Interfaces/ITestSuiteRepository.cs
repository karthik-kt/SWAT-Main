using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWAT.Domain.Entities;
namespace SWAT.Domain.Interfaces
{
    public interface ITestSuiteRepository
    {
        IEnumerable<TestSuite> TestSuites { get; }
    }
}
