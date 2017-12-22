using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAT.Domain.Entities
{
    public class TestProject
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public IEnumerable<TestPlan> TestPlans { get; set; }
    }
}
