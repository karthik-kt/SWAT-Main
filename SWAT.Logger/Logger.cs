using System;

namespace SWAT.Logger
{
    public static class MyLogger
    {
        public static event EventHandler<LogEvenArgs> LogAdded;
        private static string strLog;
        private static string strCompleteLog;
        private static bool newMessageAdded = false;        

        public static string logmessage
        {
            get
            {
                return strLog;
            }
        }

        static public void Log(string strMessage)
        {
            if (MyLogger.strLog != null)
            {
                MyLogger.strLog = MyLogger.strLog + Environment.NewLine + DateTime.Now.ToShortTimeString() + " >> " + strMessage;
                MyLogger.newMessageAdded = true;
            }
            else
            {
                MyLogger.strLog = DateTime.Now.ToShortTimeString() + " >> " + strMessage;
                MyLogger.newMessageAdded = true;
            }
            completeMessage(strMessage);
        }

        static public string Message()
        {
            string strMessage;
            strMessage = MyLogger.strLog;
            //MyLogger.strCompleteLog = MyLogger.strLog;
            MyLogger.strLog = null;
            return strMessage;
        }

        static public string completeMessage()
        {
            if (MyLogger.newMessageAdded) return strCompleteLog;
            else return "NONEWMESSAGE";
        }

        static public void completeMessage(string strMessage)
        {
            if (MyLogger.strCompleteLog != null)
            {
                MyLogger.strCompleteLog = MyLogger.strCompleteLog + Environment.NewLine + DateTime.Now.ToShortTimeString() + " >> " + strMessage;
                MyLogger.newMessageAdded = true;
            }
            else
            {
                MyLogger.strCompleteLog = DateTime.Now.ToShortTimeString() + " >> " + strMessage;
                MyLogger.newMessageAdded = true;
            }
            onLogAdded();
        }

        private static void onLogAdded()
        {
            if (LogAdded != null)
            {
                try
                {
                    LogAdded(null, new LogEvenArgs(MyLogger.strCompleteLog));
                }
                catch
                {

                }
            }
        }
        
        static public void clearMsg()
        {
            strLog = null;
        }

        static public void ClearAllMsg()
        {
            strLog = null;
            strCompleteLog = null;
        }
    }
}