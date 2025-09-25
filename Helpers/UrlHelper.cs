using System.Text;

namespace LLC_MOD_Toolbox.Helpers
{
    internal static class UrlHelper
    {
        private static readonly StringBuilder sb = new();

        private static readonly List<string> Paths =
        [
            "BepInEx-IL2CPP-x64.7z",
            "tmpchinesefont_BIE.7z",
            "LimbusLocalize_BIE.7z",
            "Resource/LimbusLocalize_Resource_latest.7z",
        ];
        private static readonly string ThisRepo =
            "repos/LocalizeLimbusCompany/LLC_Mod_Toolbox/releases/latest";

        internal static Task<string> GetGitHubApiUrl(
            string endpoint,
            Services.IFileDownloadService fileDownloadService
        )
        {
            throw new NotImplementedException();
        }

        public static string GetReleaseUrl(string url) =>
            sb.Clear().AppendFormat(url, ThisRepo).ToString();

        public static string GetHashUrl(string url) =>
            sb.Clear().AppendFormat(url, "LimbusLocalizeHash.json").ToString();

        public static string GetTestUrl(string testCode) =>
            sb.Clear()
                .Append("https://api.zeroasso.top/v2/grey_test/get_file?code=")
                .Append(testCode)
                .ToString();

        /// <summary>
        /// 打开指定的网址
        /// </summary>
        /// <param name="url">要打开的地址</param>
        public static void LaunchUrl(string url) =>
            System.Diagnostics.Process.Start(
                new System.Diagnostics.ProcessStartInfo(url) { UseShellExecute = true }
            );

        /// <summary>
        /// 打开指定的网址
        /// </summary>
        /// <param name="url">要打开的地址</param>
        public static void LaunchUrl(Uri url) => LaunchUrl(url.ToString());
    }
}
