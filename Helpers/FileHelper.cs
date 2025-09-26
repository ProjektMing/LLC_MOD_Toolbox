using System.ComponentModel;
using System.IO;
using Downloader;
using Microsoft.Extensions.Logging;

namespace LLC_MOD_Toolbox.Helpers;

internal static class FileHelper
{
    private static readonly DownloadConfiguration DownloadConfig =
        new()
        {
            ChunkCount = 8,
            MaxTryAgainOnFailure = 3,
            ParallelDownload = true,
        };
    private static readonly DownloadService Downloader = new(DownloadConfig);

    private static readonly List<string> BepInExFiles =
    [
        "doorstop_config.ini",
        "Latest(框架日志).log",
        "Player(游戏日志).log",
        "winhttp.dll",
        "winhttp.dll.disabled",
        "changelog.txt",
        "BepInEx-IL2CPP-x64.7z",
        "LimbusLocalize_BIE.7z",
        "tmpchinese_BIE.7z"
    ];
    private static readonly List<string> BepInExFolders = ["BepInEx", "dotnet",];

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="url"></param>
    /// <param name="path"></param>
    /// <param name="onDownloadProgressChanged"></param>
    /// <param name="onDownloadFileCompleted"></param>
    /// <param name="logger"></param>
    /// <returns></returns>
    public static async Task DownloadFileAsync(
        string url,
        string path,
        EventHandler<DownloadProgressChangedEventArgs> onDownloadProgressChanged,
        EventHandler<AsyncCompletedEventArgs> onDownloadFileCompleted,
        ILogger logger
    )
    {
        logger.LogInformation("开始下载文件：{url}", url);
        Downloader.AddLogger(logger);
        Downloader.DownloadProgressChanged += onDownloadProgressChanged;
        Downloader.DownloadFileCompleted += onDownloadFileCompleted;
        await Downloader.DownloadFileTaskAsync(url, path);
    }

    /// <summary>
    /// 读取节点列表配置文件
    /// </summary>
    /// <returns>节点配置</returns>
    public static Task<string> LoadNodeListConfigAsync => File.ReadAllTextAsync("NodeList.json");

    /// <summary>
    /// 下载边狱公司的语言包
    /// </summary>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException">预检查流</exception>
    public static void ExtractLanguagePackage(Stream stream, string limbusCompanyPath)
    {
        if (!Directory.Exists(limbusCompanyPath))
            throw new ArgumentException("路径不存在", nameof(limbusCompanyPath));
        if (stream == null)
            throw new ArgumentNullException(nameof(stream), "流不能为空");
        using SevenZip.SevenZipExtractor extractor = new(stream);
        extractor.ExtractArchive(limbusCompanyPath);
    }

    /// <summary>
    /// 删除 Mod，删除内容为 <seealso cref="BepInExFiles"/> 和 <seealso cref="BepInExFolders"/>
    /// </summary>
    /// <exception cref="ArgumentException">路径不存在</exception>
    public static void DeleteBepInEx(string limbusCompanyPath, ILogger logger)
    {
        if (!Directory.Exists(limbusCompanyPath))
            throw new ArgumentException("路径不存在", nameof(limbusCompanyPath));

        if (!ValidateHelper.CheckBepInEx(limbusCompanyPath))
        {
            logger.LogInformation("BepInEx 未安装或已被删除。");
            return;
        }

        foreach (string file in BepInExFiles)
        {
            File.Delete(Path.Combine(limbusCompanyPath, file));
        }
        foreach (string folder in BepInExFolders)
        {
            try
            {
                Directory.Delete(Path.Combine(limbusCompanyPath, folder), true);
            }
            catch (DirectoryNotFoundException)
            {
                logger.LogInformation("{}已提前被删除。", folder);
            }
        }
    }

    /// <summary>
    /// 提取文件到指定目录
    /// </summary>
    /// <param name="filePath">压缩包位置</param>
    /// <param name="destinationPath">解压目标位置</param>
    /// <exception cref="ArgumentException">路径无效或不存在</exception>
    public static void ExtractFile(string? filePath, string? destinationPath)
    {
        if (!File.Exists(filePath))
        {
            throw new ArgumentException("文件路径无效或不存在", nameof(filePath));
        }
        if (!Directory.Exists(destinationPath))
        {
            throw new ArgumentException("目标路径无效或不存在", nameof(destinationPath));
        }

        using var archive = new SevenZip.SevenZipExtractor(filePath);
        archive.ExtractArchive(destinationPath);
    }
}
