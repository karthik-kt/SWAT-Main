using System;

namespace SWAT.Logger
{
    public class LogEvenArgs : EventArgs
    {
        public  LogEvenArgs(string log)
        {
            Log = log;
        }
        public string Log { set; get; }
    }

    public class Logger
    {
        public event EventHandler<LogEvenArgs> LogAdded;

        private string testsuitelog = null;
        private string testcaselog = null;

        public string TestSuiteLog
        {
            get
            {
                return this.testsuitelog;
            }
        }

        public string TestCaseLog
        {
            get
            {
                return this.testcaselog;
            }
        }

        public void ClearTestCaseLog()
        {
            this.testcaselog = null;
        }

        public void ClearTestSuiteLog()
        {
            testsuitelog = null;
        }
        
        public void Log(string log)
        {
            log = Environment.NewLine + DateTime.Now.ToShortTimeString() + " >> " + log;

            onLogAdded(log);

            if (this.testcaselog != null)
            {
                this.testcaselog = this.testcaselog + Environment.NewLine + DateTime.Now.ToShortTimeString() + " >> " + log;
            }
            else
            {
                this.testcaselog = DateTime.Now.ToShortTimeString() + " >> " + log;
            }
            this.testsuitelog = this.testsuitelog + this.testcaselog;
        }

        private void onLogAdded(string log)
        {
            if (LogAdded != null)
            {
                try
                {
                    LogAdded(null, new LogEvenArgs(log));
                }
                catch
                {

                }
            }
        }        
    }

}