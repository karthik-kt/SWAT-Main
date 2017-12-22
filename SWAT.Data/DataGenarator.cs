using System;
using System.Linq;
using FluentDateTime;

namespace SWAT.Data
{
    internal class DataGenarator
    {
        public DataGenarator()
        {
        }

        internal string RandomNumber(string p)
        {
            p = p.Replace(Constants.TestData_Generate, "");
            return RandomNumber(Int32.Parse(p));
        }

        internal string RandomText(string p)
        {
            p = p.Replace(Constants.TestData_RandomText, "");
            return RandomString(Int32.Parse(p));
        }

        internal string AddDate(string adddays)
        {
            try
            {
                string formats = "MM/dd/yyyy";
                adddays = adddays.Replace(Constants.TestData_Date, "");
                int numberofdays = Int32.Parse(adddays);
                DateTime now = DateTime.Now;
                return now.AddBusinessDays(numberofdays).ToString(formats);
            }
            catch
            {
                return DateTime.Now.ToString("MM/dd/yyyy");
            }
        }

        private  static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static string RandomNumber(int length)
        {
            const string chars = "0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
