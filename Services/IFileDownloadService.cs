using System.IO;

namespace LLC_MOD_Toolbox.Services;

public enum ServiceState
{
    Regular,
    GrayRelease,
}

public interface IFileDownloadService
{
    Task<string> GetJsonAsync(string url);
    Task<Stream> InstallLanguagePackageAsync(string url, IProgress<double> progress);
}
