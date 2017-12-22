using ClosedXML.Excel;
using SWAT.Logger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;
using SWAT.Data;

namespace SWAT.FrameWork.Results
{
    using SWAT.FrameWork.Utilities;
    using Config = SWAT.Configuration.TestStartInfo;

    public class TestResult : IDisposable
    {
        private string testBrowser;

        private string testSuite;

        private XLWorkbook workbook;

        private string strFilePath;

        public TestResult(Config Config)
        {
            strFilePath = Config.ResultPath;
            testBrowser = Config.Browser;
            testSuite = Config.Suite;
            workbook = new XLWorkbook();
        }

        internal void SaveAs()
        {
            try
            {
                //strFilePath = @"C:\TestData\";
                strFilePath = strFilePath + "TestResult.xlsx"; //_" + fileName;
                workbook.SaveAs(strFilePath);
                MyLogger.Log("Result Stored at := [ " + strFilePath + " ]");
                Thread.Sleep(Constants.Wait_Short);
            }
            catch
            {
            }
        }

        public void ProcessResult()
        {
            string strStatus; string strAction;
            Regex start = new Regex("TESTCASE.START");
            Regex stop = new Regex("TESTCASE.STOP");
            bool falied = false;
            int intStartRow = 0;
            var dt = workbook.Worksheet("Result");
            var Summary = workbook.Worksheet("Summary");
            int iLoop = 3;
            try
            {
                for (int intRowNumber = 2; intRowNumber < dt.RowCount(); intRowNumber++)
                {
                    strAction = dt.Cell("B" + intRowNumber).Value.ToString().ToUpper();
                    strStatus = dt.Cell("G" + intRowNumber).Value.ToString().ToUpper();

                    if (Constants.ResultStatus_Failed == strStatus)
                    {
                        dt.Range("G" + intRowNumber + ":G" + intRowNumber).Style.Font.FontColor = XLColor.Red;
                        falied = true;
                    }
                    else if (Constants.ResultStatus_Passed == strStatus)  // Passed
                    {
                        dt.Range("G" + intRowNumber + ":G" + intRowNumber).Style.Font.FontColor = XLColor.Green;
                    }
                    //Test Case Start
                    if (start.Match(strAction).Success)
                    {
                        intStartRow = intRowNumber;
                        falied = false;
                    }
                    // Test Case Stop
                    if (stop.Match(strAction).Success)
                    {
                        // Update Test Case Start with status
                        if (falied)
                        {
                            dt.Cell("G" + intStartRow).Value = Constants.ResultStatus_Failed;
                            dt.Range("G" + intStartRow + ":G" + intStartRow).Style.Font.Bold = true;
                            dt.Range("G" + intStartRow + ":G" + intStartRow).Style.Font.FontColor = XLColor.Red;

                            //format summary sheet
                            Summary.Cell("D" + iLoop).Style.Font.FontColor = XLColor.Red;
                        }
                        else
                        {
                            dt.Cell("G" + intStartRow).Value = Constants.ResultStatus_Passed;
                            dt.Range("G" + intStartRow + ":G" + intStartRow).Style.Font.FontColor = XLColor.Green;

                            //format summary sheet
                            Summary.Cell("D" + iLoop).Style.Font.FontColor = XLColor.Green;
                        }

                        //Test case row formating
                        dt.Range("A" + intStartRow + ":I" + intStartRow).Style.Font.Bold = true;
                        dt.Range("A" + intStartRow + ":I" + intStartRow).Style.Fill.BackgroundColor = XLColor.FromHtml("#FFD7E4F2");
                        dt.Cell("G" + intRowNumber).Value = "";
                        dt.Range("A" + intRowNumber + ":I" + intRowNumber).Style.Font.Bold = true;
                        dt.Range("A" + intRowNumber + ":I" + intRowNumber).Style.Fill.BackgroundColor = XLColor.FromHtml("#FFD7E4F2");

                        //Summary sheet
                        Summary.Cell("C" + iLoop).Value = dt.Cell("C" + intStartRow).Value.ToString();
                        Summary.Cell("D" + iLoop).Value = dt.Cell("G" + intStartRow).Value;
                        Summary.Cell("B" + iLoop).Value = iLoop - 2;
                        iLoop++;
                    }
                }

                // Format Result Sheet
                headerFormat(dt.Range("A1:I1"));
                commonFormat(dt.RangeUsed());

                //Format Summary sheet
                Summary.Cell("C2").Value = "TESTCASE NAME";
                Summary.Cell("D2").Value = "STATUS";
                Summary.Cell("B2").Value = "#";

                commonFormat(Summary.RangeUsed());
                //Summary sheet test setting details

                Summary.Cell("F2").Value = "APPLICATION";
                Summary.Cell("F3").Value = "ENVIRONMENT";
                Summary.Cell("F4").Value = "TEST SUITE";
                Summary.Cell("F5").Value = "DATE & TIME";
                Summary.Cell("F6").Value = "BROWSER";
                Summary.Cell("F7").Value = "MACHINE";

                Summary.Cell("G2").Value = "CLAW";
                Summary.Cell("G3").Value = "STAGING";
                Summary.Cell("G4").Value = testSuite;
                Summary.Cell("G5").Value = DateTime.Now.ToString();
                Summary.Cell("G6").Value = testBrowser;
                Summary.Cell("G7").Value = Environment.MachineName.ToUpper();

                headerFormat(Summary.Range("F2:F7"));
                commonFormat(Summary.Range("F2:G7"));
                headerFormat(Summary.Range("B2:D2"));

                foreach (var Col in dt.ColumnsUsed())
                {
                    Col.AdjustToContents();
                }
                dt.ShowGridLines = false;

                foreach (var Col in Summary.ColumnsUsed())
                {
                    Col.AdjustToContents();
                }
                Summary.ShowGridLines = false;
            }
            finally
            {
                dt.Dispose();
                Summary.Dispose();
            }
        }

        //For common for Summary and Result sheet.
        internal void commonFormat(IXLRange range)
        {
            range.Style.Font.FontSize = 9;
            range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            range.Style.Border.InsideBorderColor = XLColor.Black;
            range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            range.Style.Border.OutsideBorderColor = XLColor.Black;
        }

        internal void headerFormat(IXLRange Range)
        {
            Range.Style.Font.Bold = true;
            Range.Style.Fill.BackgroundColor = XLColor.FromHtml("#325F7A");
            Range.Style.Font.FontColor = XLColor.White;
        }

        public DataTable Summary { get; set; }
        public DataTable Resu { get; set; }

        public void AddSheets(DataTable Result)
        {
            using (DataSet ResultDS = ResultToSummary(Result))
            {
                Summary = ResultDS.Tables["Summary"];
                Summary = ResultDS.Tables[0];
                Result = ResultDS.Tables["Result"];
                Result = ResultDS.Tables[1];
                workbook.Worksheets.Add(Summary, "Summary");
                workbook.Worksheets.Add(Result, "Result");
                // Result.Dispose();
            }
        }

        private List<int> TCStart = new List<int>();
        private List<int> TCStop = new List<int>();
        private List<int> TCFailed = new List<int>();
        private List<int> TCSkipped = new List<int>();
        private int TCTotalRow = 0;

        private DataSet ResultToSummary(DataTable Result)
        {
            try
            {
                using (DataTable Summary = new DataTable())
                {
                    Summary.Columns.Add("TestCases", typeof(string));
                    Summary.Columns.Add("Status", typeof(string));
                    string TCStaus = Constants.ResultStatus_Failed;
                    String Action = null;
                    int iLoop = 0; int iTCStart = 0;
                    string TCName = null;
                    foreach (DataRow ResultRow in Result.Rows)
                    {
                        Action = ResultRow[Constants.Column_Actions].ToString().ToUpper();
                        if (Action == Constants.TCStart)
                        {
                            iTCStart = iLoop;
                            TCName = ResultRow[Constants.Column_Data].ToString();
                            TCStaus = Constants.ResultStatus_Passed;
                            TCStart.Add(iLoop);
                        }
                        if (Action == Constants.TCStop)
                        {
                            Summary.Rows.Add(TCName, TCStaus);
                            Result.Rows[iTCStart][Constants.Column_Status] = TCStaus;
                            Result.Rows[iLoop][Constants.Column_Status] = TCStaus;
                            TCStop.Add(iLoop);
                        }
                        else
                        {
                            if (Constants.ResultStatus_Failed == ResultRow[Constants.Column_Status].ToString().ToUpper())
                            {
                                TCStaus = Constants.ResultStatus_Failed;
                                TCFailed.Add(iLoop);
                            }
                            if (Constants.TCResultStatus_Skipped == ResultRow[Constants.Column_Status].ToString().ToUpper())
                            {
                                TCStaus = Constants.TCResultStatus_Skipped;
                                TCFailed.Add(iLoop);
                            }
                        }
                        iLoop++;
                    }
                    TCTotalRow = iLoop;
                    using (DataSet ResultDS = new DataSet())
                    {
                        ResultDS.Tables.Add(Summary);
                        ResultDS.Tables.Add(Result);
                        return ResultDS;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ColorMe()
        {
            var dt = workbook.Worksheet("Result");
            int iHeaderPadding;
            dt.Range("G2" + ":G" + TCTotalRow + 1).Style.Font.FontColor = XLColor.Green;
            foreach (int intStartRow in TCStart)
            {
                iHeaderPadding = intStartRow + 2;
                dt.Range("A" + iHeaderPadding + ":I" + iHeaderPadding).Style.Font.Bold = true;
                dt.Range("A" + iHeaderPadding + ":I" + iHeaderPadding).Style.Fill.BackgroundColor = XLColor.FromHtml("#FFD7E4F2");
            }
            foreach (int intStartRow in TCStop)
            {
                iHeaderPadding = intStartRow + 2;
                dt.Range("A" + iHeaderPadding + ":I" + iHeaderPadding).Style.Font.Bold = true;
                dt.Range("A" + iHeaderPadding + ":I" + iHeaderPadding).Style.Fill.BackgroundColor = XLColor.FromHtml("#FFD7E4F2");
            }
            foreach (int intStartRow in TCFailed)
            {
                iHeaderPadding = intStartRow + 2;
                dt.Range("G" + iHeaderPadding + ":G" + iHeaderPadding).Style.Font.Bold = true;
                dt.Range("G" + iHeaderPadding + ":G" + iHeaderPadding).Style.Font.FontColor = XLColor.Red;
            }
            commonFormat(dt.RangeUsed());

            var Summary = workbook.Worksheet("Summary");
            commonFormat(Summary.RangeUsed());
            Summary.Cell("F2").Value = "APPLICATION";
            Summary.Cell("F3").Value = "ENVIRONMENT";
            Summary.Cell("F4").Value = "TEST SUITE";
            Summary.Cell("F5").Value = "DATE & TIME";
            Summary.Cell("F6").Value = "BROWSER";
            Summary.Cell("F7").Value = "MACHINE";

            Summary.Cell("G2").Value = "CLAW";
            Summary.Cell("G3").Value = "STAGING";
            Summary.Cell("G4").Value = testSuite;
            Summary.Cell("G5").Value = DateTime.Now.ToString();
            Summary.Cell("G6").Value = testBrowser;
            Summary.Cell("G7").Value = Environment.MachineName.ToUpper();
            headerFormat(Summary.Range("F2:F7"));
            commonFormat(Summary.Range("F2:G7"));

            Summary.Dispose();
            dt.Dispose();
        }

        public void ExportResults()
        {
            SaveAs();
        }

        public void Dispose()
        {
            if (workbook != null)
            {
                workbook.Dispose();
            }
            if (Summary != null)
            {
                Summary.Dispose();
            }
        }
    }
}