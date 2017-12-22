using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWAT.Data;

namespace SWAT.Applications.Claw.DAL
{
    interface IDataPage
    {
        void FillData(DataManager datamanager);
    }
}
