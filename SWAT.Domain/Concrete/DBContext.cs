using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWAT.Domain.Entities;

namespace SWAT.Domain.Concrete
{
    class DBContext : DbContext
    {
        public DbSet<TestSuite> TestSuites { get; set; }
    }
}
