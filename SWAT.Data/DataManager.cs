// /////////////////////////////////////////////////////////////////////////////////////
//                           Copyright (c) 2015 - 2015
//                            Coyote Logistics L.L.C.
//                          All Rights Reserved Worldwide
// 
// WARNING:  This program (or document) is unpublished, proprietary
// property of Coyote Logistics L.L.C. and is to be maintained in strict confidence.
// Unauthorized reproduction, distribution or disclosure of this program
// (or document), or any program (or document) derived from it is
// prohibited by State and Federal law, and by local law outside of the U.S.
// /////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using FluentDateTime;
using SWAT.Logger;

namespace SWAT.Data
{
    public class DataManager
    {
        private DataTable _testdata { get; set; }
        private int _rownumber { get; set; }
        private string _copycolumn { get; set; }
        public string _baseurl { get; set; }
        public string RawData { get; set; }

        private Dictionary<string, DataTable> _tables { get; set; }

        public DataManager()
        {
            _tables = new Dictionary<string, DataTable>();
        }

        public bool ImportExcel(string filepath, string Extension)
        {
            try
            {
                //string Extension = ".xls";
                string isHDR = "Yes";
                string conStr = "";
                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
                        break;

                    case ".xlsx": //Excel 07
                        conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
                        break;
                }
                if(!File.Exists(filepath))
                {
                    return false;
                }                
                conStr = String.Format(conStr, filepath, isHDR);
                using (OleDbConnection connExcel = new OleDbConnection(conStr))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        cmdExcel.Connection = connExcel;
                        connExcel.Open();
                        using (System.Data.DataTable dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null))
                        {
                            connExcel.Close();
                            foreach (DataRow row in dtExcelSchema.Rows)
                            {
                                using (System.Data.DataTable ds = new System.Data.DataTable())
                                {
                                    cmdExcel.CommandText = "SELECT * From [" + row["Table_Name"].ToString() + "]";
                                    using (OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter())
                                    {
                                        try
                                        {
                                            da.SelectCommand = cmdExcel;
                                            da.Fill(ds);
                                            _tables.Add(row["Table_Name"].ToString(), ds);
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            }
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyLogger.Log(ex.Message);
                throw ex;
            }
        }

        public void AddNewDataSheet(string sheetname, string filepath)
        {
            try
            {
                DataTable NewDatatable = ImportWorkSheet2(sheetname, filepath);
                this._tables.Add(sheetname + "$", NewDatatable);
            }
            catch (Exception)
            {
                MyLogger.Log("Error on importing data sheet - [ " + sheetname + " ]");
                //throw ex;
            }
        }

        internal DataTable ImportWorkSheet2(string sheetname, string filepath)
        {
            string Extension = ".xls";
            string isHDR = "Yes";

            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
                    break;

                case ".xlsx": //Excel 07
                    conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
                    break;
            }

            conStr = String.Format(conStr, filepath, isHDR);
            using (OleDbConnection connExcel = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmdExcel = new OleDbCommand())
                {
                    cmdExcel.Connection = connExcel;
                    connExcel.Open();
                    connExcel.Close();
                    sheetname = sheetname + "$";
                    cmdExcel.CommandText = "SELECT * From [" + sheetname + "]";
                    using (OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter())
                    {
                        using (DataTable ds = new DataTable())
                        {
                            da.SelectCommand = cmdExcel;
                            da.Fill(ds);
                            return ds;
                        }
                    }
                }
            }
        }

        public DataTable ImportWorkSheet(string sheetname, string filepath, string Extension)
        {
            string isHDR = "Yes";

            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
                    break;

                case ".xlsx": //Excel 07
                    conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
                    break;
            }

            conStr = String.Format(conStr, filepath, isHDR);
            using (OleDbConnection connExcel = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmdExcel = new OleDbCommand())
                {
                    cmdExcel.Connection = connExcel;
                    connExcel.Open();
                    // using (DataTable dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null))
                    // {
                    connExcel.Close();
                    sheetname = sheetname + "$";
                    cmdExcel.CommandText = "SELECT * From [" + sheetname + "] Where Actions is Not Null";
                    using (OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter())
                    {
                        using (DataTable ds = new DataTable())
                        {
                            da.SelectCommand = cmdExcel;
                            da.Fill(ds);
                            return ds;
                        }
                    }
                    //   }
                }
            }
        }

        public DataTable ImportWorkSheet(string sheetname, string filepath)
        {
            string Extension = Path.GetExtension(filepath);
            return ImportWorkSheet(sheetname, filepath, Extension);
        }

        public bool GetData(string datareference)
        {
            try
            {   // no data needed for the teststep        
                if (datareference.StartsWith("CreateTestRun :") || datareference == "")
                {
                    return true;
                }

                //Copycell between sheets.
                string copycell = @"([A-Za-z0-9]+)\;([A-Za-z0-9\#]+)\;([0-9]+)\-([A-Za-z0-9]+)\;([A-Za-z0-9\#]+)\;([0-9]+)";
                Match result = Regex.Match(datareference, copycell);
                if (result.Success)
                {
                    this.RawData = datareference;
                    return true;
                }

                Regex regex = new Regex(@"\d+-\d+");
                Match match = regex.Match(datareference);
                if (match.Success)
                {
                    this.RawData = datareference;
                    return true;
                }

                string[] datasheet = datareference.Split(';');

                if (datasheet.Count() == 2)
                {
                    _rownumber = int.Parse(datasheet[1]);
                    _testdata = _tables[datasheet[0] + "$"];
                    return true;
                }
                this.RawData = datareference;
                return true;
            }
            catch (KeyNotFoundException ex)
            {
                MyLogger.Log("Data table is not found in the data tables loaded." + datareference);
                throw ex;
            }
        }

        public bool CopyCellBetweenTables()
        {
            string datamanipulation = this.RawData;
            string copycell = @"([A-Za-z0-9]+)\;([A-Za-z0-9\ \#\ ]+)\;([0-9]+)\-([A-Za-z0-9]+)\;([A-Za-z0-9\ \#]+)\;([0-9]+)";
            Match result = Regex.Match(datamanipulation, copycell);

            if (!result.Success)
            {
                MyLogger.Log(string.Concat("Incorrect data manipulation string : [ ", datamanipulation," ]"));
                return false;                
            }

            string sourceSheet = result.Groups[1].Value.ToString();
            string sourceColumn =  result.Groups[2].Value.ToString();
            int sourceRow =  int.Parse(result.Groups[3].Value.ToString());
            string destinationSheet = result.Groups[4].Value.ToString();
            string destinationColumn = result.Groups[5].Value.ToString();
            int destinationRow = int.Parse(result.Groups[6].Value.ToString());

            try
            {
                var sourceValue = _tables[sourceSheet + "$"].Rows[sourceRow-1][sourceColumn];
                _tables[destinationSheet + "$"].Rows[destinationRow-1][destinationColumn] = sourceValue;
                return true;
            }
            catch
            {
                return false;
            }

            
        }


        public IList<string> AsList()
        {
            try
            {
                List<string> testDataAsList = new List<string>();
                foreach(DataColumn col in _testdata.Columns)
                {
                    testDataAsList.Add(_testdata.Rows[_rownumber - 1].Field<string>(col));
                }
                return testDataAsList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string Data(string columnname)
        {
            try
            {
                string value = null;
                //string tempvalue = null;
                value = _testdata.Rows[_rownumber - 1][columnname].ToString().Trim();
                if (Regex.IsMatch(value, Constants.TestData_Generate))
                {
                    value = GetRandomNumber(value);
                    _testdata.Rows[_rownumber - 1][columnname] = value;
                }
                else if (Regex.IsMatch(value, Constants.TestData_RandomText))
                {
                    value = GetRandomText(value);
                    _testdata.Rows[_rownumber - 1][columnname] = value;
                }
                else if (Regex.IsMatch(value, Constants.TestData_Date))
                {
                    value = GetDate(value);
                    _testdata.Rows[_rownumber - 1][columnname] = value;
                }
                return value;
            }
            // col is not used in this test
            catch
            {
                return Constants.Ignore;
            }
        }

        private string GetRandomNumber(string p)
        {
            p = p.Replace(Constants.TestData_Generate, "");
            return RandomNumber(Int32.Parse(p));
        }

        private string GetRandomText(string p)
        {
            p = p.Replace(Constants.TestData_RandomText, "");
            return RandomString(Int32.Parse(p));
        }

        private string GetDate(string adddays)
        {
            try
            {
                string formats = "MM/dd/yyyy";
                adddays = adddays.Replace(Constants.TestData_Date, "");
                DateTime now = DateTime.Now;
                if (adddays ==""|| adddays== null)
                {
                    return now.ToString(formats);
                }
                if (adddays == Constants.TestData_HasDate)
                {
                    return String.Concat(Constants.TestData_HasDate, now.ToString(formats));
                }
                int numberofdays = Int32.Parse(adddays);                
                return now.AddBusinessDays(numberofdays).ToString(formats);
            }
            catch
            {
                return DateTime.Now.ToString("MM/dd/yyyy");
            }
        }

        private static string RandomString(int length)
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
                
        public bool CompareColValues(Dictionary<string, string> result, string key, string val)
        {
            return (result[key] == val);
        }

        public void SetData(string colname, string value)
        {
            try
            {
                _testdata.Rows[_rownumber - 1][colname] = value;
                MyLogger.Log("Copied from Application [ " + colname + "]:=[ " + value + " ]");
            }
            catch (IndexOutOfRangeException)
            {
                DataRow toInsert = _testdata.NewRow();
                toInsert[colname] = value;
                _testdata.Rows.Add(toInsert);
                MyLogger.Log("Copied from Application [ " + colname + "]:=[ " + value + " ]");
            }

            catch
            {
            }
        }

        public bool CopyRow(string p)
        {
            try
            {
                string[] datarefrence = p.Split(';');
                Regex start1 = new Regex("-");
                string[] rows = datarefrence[1].Split('-');
                int fromRowNumber = int.Parse(rows[0]);
                int toRowNumber = int.Parse(rows[1]);
                DataRow fromRow = _testdata.Rows[fromRowNumber - 1];
                DataRow toRow = _testdata.Rows[toRowNumber - 1];
                foreach (DataColumn dr in _testdata.Columns)
                {
                    toRow[dr.ColumnName] = fromRow[dr.ColumnName];
                }
                return true;
            }
            catch
            {
                MyLogger.Log("Incorrect format. Use 'SheetName;FromRow;ToRow' ");
                return false;
            }
        }
        public bool CopyCol_Retain(string p)
        {
            string[] datarefrence = p.Split(';');
            if (datarefrence.Count() == 3)
            {
                try
                {
                    _testdata = _tables[datarefrence[0] + "$"];
                    string column = datarefrence[1];
                    string[] rows = datarefrence[2].Split('-');
                    int fromRowNumber = int.Parse(rows[0]);
                    int toRowNumber = int.Parse(rows[1]);
                    DataRow fromRow = _testdata.Rows[fromRowNumber - 1];
                    DataRow toRow = _testdata.Rows[toRowNumber - 1];
                    toRow[column] = toRow[column].ToString()+fromRow[column].ToString();
                    return true;
                }
                catch
                {
                    MyLogger.Log("Incorrect format. Use 'SheetName;ColumnName;FromRow;ToRow' ");
                    return false;
                }
            }
            else
            {
                MyLogger.Log("Incorrect format. Use 'SheetName;ColumnName;FromRow;ToRow' ");
                return false;
            }
        }

        public bool CopyCol(string p)
        {
            string[] datarefrence = p.Split(';');
            if (datarefrence.Count() == 3)
            {
                try
                {
                    _testdata = _tables[datarefrence[0] + "$"];
                    string column = datarefrence[1];
                    string[] rows = datarefrence[2].Split('-');
                    int fromRowNumber = int.Parse(rows[0]);
                    int toRowNumber = int.Parse(rows[1]);
                    DataRow fromRow = _testdata.Rows[fromRowNumber - 1];
                    DataRow toRow = _testdata.Rows[toRowNumber - 1];
                    toRow[column] = fromRow[column];
                    return true;
                }
                catch
                {
                    MyLogger.Log("Incorrect format. Use 'SheetName;ColumnName;FromRow;ToRow' ");
                    return false;
                }
            }
            else
            {
                MyLogger.Log("Incorrect format. Use 'SheetName;ColumnName;FromRow;ToRow' ");
                return false;
            }
        }

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (System.Data.DataColumn Column in _testdata.Columns)
            {
                dic.Add(Column.ColumnName.ToString().Trim(), _testdata.Rows[this._rownumber - 1][Column.ColumnName.ToString()].ToString().Trim());
            }
            return dic;
        }

        public string GetFormattedTime(string time, string format)
        {
            if (time == "!IGNORE!" || time == "")
            {
                return "!IGNORE!";
            }

            return Convert.ToDateTime(time).ToString(format);
        }

        public static string GetSpecificWeekDay(int day)
        {
            int today = (int)DateTime.Now.DayOfWeek;
            string specifiedDate = DateTime.Today.AddDays(day - today).ToString();
            return specifiedDate;
        }
    }
}