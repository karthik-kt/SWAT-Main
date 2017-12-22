using System;

namespace SWAT.Data
{
    public class Constants
    {        
        //WaitTime
        public readonly static int Wait_Long = 10000;
        public readonly static int Wait_Medium = 5000;
        public readonly static int Wait_Short = 3000;

        //Option
        
        public readonly static string Option_ExitOnError = "EXITONERROR";
        public readonly static string Option_DependsOnAbove = "DEPENDSONABOVE";
        public readonly static string Option_Retry = "RETRY";
        public readonly static string Option_Ignore = "IGNORE";


        public readonly static string Ignore = "!IGNORE!";
        //Actions
        public readonly static string Action_Default = "!DEFAULT!";
        public readonly static string Actions_UnCheck = "!UNCHECK!";
        public readonly static string Actions_Check = "!CHECK!";

        //State
        public readonly static string State_Checked = "!CHECKED!";
        public readonly static string State_UnChecked = "!UNCHECKED!";
        public readonly static string State_Displayed = "!DISPLAYED!";
        public readonly static string State_NotDisplayed = "!NOTDISPLAYED!";

        //Roles
        public readonly static string Entity_Carrier = "CARRIER";
        public readonly static string Entity_Customer = "CUSTOMER";
        public readonly static string Entity_Admin = "ADMIN";
        public readonly static string Entity_People = "PEOPLE";
        public readonly static string Entity_Employee = "EMPLOYEE";

        //TestStatus
        public readonly static string ResultStatus_Passed = "PASSED";
        public readonly static string ResultStatus_Failed = "FAILED";
        public readonly static string ResultStatus_Ignored = "IGNORED";
        public readonly static string TCResultStatus_Skipped = "SKIPPED";

        //TestData
        public readonly static string TestData_RandomText = "!RANDOM!";
        public readonly static string TestData_Date = "!TODAY!";
        public readonly static string TestData_Generate = "!GENERATE!";
        public readonly static string TestData_HasDate = "HasDate";
        public readonly static string TestData_Contains = "Contains";
        public readonly static string TestData_HasText = "HasText";
        public readonly static string TestData_Edit = "Edit";
        public readonly static string TestData_DDOptionsContains = "DDOptionsContains";
        public readonly static string TestData_DDOptionSelected = "DDOptionSelected";
        public readonly static string HasClass = "HasClass";


        public readonly static string InstructionSheet = "Instructions";
        public readonly static string Column_Data = "Data";
        public readonly static string Column_Status = "Status";
        public readonly static string Column_Actions = "Actions";
        public readonly static string Column_Comment = "Comment";
        public readonly static string Column_Option = "Option";
        public readonly static string Column_ExpectedResult = "ExpectedResult";
        public readonly static string Column_ActulaResult = "ActulaResult";


        public readonly static string Bazooka = "BAZOOKA";
        public readonly static string TCStart = "TESTCASE.START";
        public readonly static string TCStop = "TESTCASE.STOP";

        //public static string InstructionSheet
        //{
        //    get
        //    {
        //        return "Instructions";
        //    }
        //}

        //public static string DataCol
        //{
        //    get
        //    {
        //        return "Data";
        //    }
        //}

        //public static string StatusCol
        //{
        //    get
        //    {
        //        return "Status";
        //    }
        //}

        //public static string ActionsCol
        //{
        //    get
        //    {
        //        return "Actions";
        //    }
        //}

        //public static string CommentCol
        //{
        //    get
        //    {
        //        return "Comment";
        //    }
        //}

        //public static string OptionCol
        //{
        //    get
        //    {
        //        return "Option";
        //    }
        //}

        //public static string ExpectedResultCol
        //{
        //    get
        //    {
        //        return "ExpectedResult";
        //    }
        //}

        //public static string ActulaResultCol
        //{
        //    get
        //    {
        //        return "ActulaResult";
        //    }
        //}

        //public static string Ignore
        //{
        //    get
        //    {
        //        return "!IGNORE!";
        //    }
        //}

        //public static string Retry
        //{
        //    get
        //    {
        //        return "RETRY";
        //    }
        //}

        //public static string Bazooka
        //{
        //    get
        //    {
        //        return "BAZOOKA";
        //    }
        //}

        //public static string TCStart
        //{
        //    get
        //    {
        //        return "TESTCASE.START";
        //    }
        //}

        //public static string TCStop
        //{
        //    get
        //    {
        //        return "TESTCASE.STOP";
        //    }
        //}
    }
}