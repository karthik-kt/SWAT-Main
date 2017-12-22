namespace SWAT.Applications.Claw
{
    using SWAT.Data;
    using SWAT.FrameWork.Utilities;
    using Config = SWAT.Configuration.TestStartInfo;

    internal class LoadSearch : UIObjects
    {
        private DataManager testData;

        public LoadSearch(Config c, DataManager t)
            : base(c)
        {
            testConfig = c;
            driver = testConfig.Driver;
            testData = t;
        }
    }
}