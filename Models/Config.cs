using System.IO;
using LLC_MOD_Toolbox.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LLC_MOD_Toolbox.Models;

public class Config(PrimaryNodeList primaryNodeList)
{
    public string? GamePath { get; set; }
    public string? Token { get; set; }
    public required NodeInformation ApiNode { get; set; }
    public required NodeInformation DownloadNode { get; set; }

    [JsonIgnore]
    public PrimaryNodeList PrimaryNodeList { get; set; } = primaryNodeList;

    public static Config ReadFrom(string path, IServiceProvider services)
    {
        var logger = services.GetRequiredService<ILogger<Config>>();
        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("从 {path} 获取设置", path);
        }
        try
        {
            var jsonPayload = File.ReadAllText(path);
            var config = JsonHelper.Deserialize<Config>(jsonPayload);
            config.PrimaryNodeList = services.GetRequiredService<PrimaryNodeList>();
            return config;
        }
        catch (FileNotFoundException)
        {
            logger.LogWarning("配置文件不存在，使用默认配置");

            var primaryNodeList = services.GetRequiredService<PrimaryNodeList>();
            return new Config(primaryNodeList)
            {
                ApiNode = primaryNodeList.ApiNode.First(n => n.IsDefault),
                DownloadNode = primaryNodeList.DownloadNode.First(n => n.IsDefault),
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "因未预期异常导致的读取配置文件失败，使用默认配置");
            var primaryNodeList = services.GetRequiredService<PrimaryNodeList>();
            return new Config(primaryNodeList)
            {
                ApiNode = primaryNodeList.ApiNode.First(n => n.IsDefault),
                DownloadNode = primaryNodeList.DownloadNode.First(n => n.IsDefault),
            };
        }
    }

    public void WriteTo(string path) => JsonHelper.Serialize(this, path);
}
