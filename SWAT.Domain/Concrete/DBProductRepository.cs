using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWAT.Domain.Entities;
using SWAT.Domain.Interfaces;
namespace SWAT.Domain.Concrete
{
    public class DBProductRepository : ITestSuiteRepository
    {
        private readonly DBContext _Context = new DBContext();
        public IEnumerable<TestSuite> TestSuites
        {
            get
            {
                return _Context.TestSuites;
            }
        }
    }
}
