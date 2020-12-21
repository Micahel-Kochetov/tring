namespace Assets.Scripts.Common.AppVersion
{
    public class AppVersionModel
    {
        private string fullVersionFormat = "v{0}.{1}-{2}";
        public string Version = "2.1";
        public int BuildNumber = 0;
        public string BranchName;

        public string GetAppFullVersion()
        {
            return string.Format(fullVersionFormat, Version, BuildNumber, BranchName);
        }

        public string GetAppVersion()
        {
            return Version;
        }

        public int GetBuildNumber()
        {
            return BuildNumber;
        }
    }
}