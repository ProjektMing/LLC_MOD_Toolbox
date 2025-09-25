using System.IO;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LLC_MOD_Toolbox.Helpers;
using LLC_MOD_Toolbox.Models;
using LLC_MOD_Toolbox.Services;
using Microsoft.Extensions.Logging;

namespace LLC_MOD_Toolbox.ViewModels;

public partial class AutoInstallerViewModel : ObservableObject
{
    private readonly ILogger<AutoInstallerViewModel> _logger;
    private readonly IFileDownloadService _fileDownloadService;
    private readonly IDialogDisplayService _dialogDisplayService;

    private readonly Config _config;

    [ObservableProperty]
    private double _percent;
    private readonly Progress<double> _installationProgress;

    public AutoInstallerViewModel(
        ILogger<AutoInstallerViewModel> logger,
        IFileDownloadService fileDownloadService,
        IDialogDisplayService dialogDisplayService,
        Config config
    )
    {
        _logger = logger;
        _fileDownloadService = fileDownloadService;
        _dialogDisplayService = dialogDisplayService;
        _config = config;

        _installationProgress = new Progress<double>(value => Percent = value);
    }

    [RelayCommand]
    private async Task ModInstallation()
    {
        _logger.LogInformation("开始安装语言包。");
        _logger.LogInformation("当前配置为：{config}", _config);

        if (ValidateHelper.CheckMelonloader(_config.GamePath))
        {
            _dialogDisplayService.ShowError("检测到 MelonLoader，请先卸载。");
            return;
        }

        if (!_dialogDisplayService.Confirm("安装前请确保游戏已经关闭。\n确定继续吗？"))
        {
            _logger.LogInformation("用户取消了安装语言包。");
            return;
        }
        try
        {
            await _fileDownloadService.InstallLanguagePackageAsync(
                UrlHelper.GetReleaseUrl(_config.DownloadNode.Endpoint),
                _installationProgress
            );
            _logger.LogInformation("文件下载成功，开始执行后续操作。");
        }
        catch (IOException ex)
        {
            MessageBox.Show("Limbus Company正在运行中，请先关闭游戏。", "警告");
            _logger.LogWarning(ex, "Limbus Company正在运行中，请先关闭游戏。");
        }
        catch (ArgumentNullException ex)
        {
            MessageBox.Show("注册表内无数据，可能被恶意修改了！", "警告");
            _logger.LogWarning(ex, "注册表内无数据，可能被恶意修改了！");
        }
        catch (HashException ex)
        {
            MessageBox.Show("文件校验失败，请检查网络连接。", "警告");
            _logger.LogWarning(ex, "文件校验失败，请检查网络连接。");
        }
        catch (Exception ex)
        {
            MessageBox.Show("未知错误，请联系开发者。", "警告");
            _logger.LogError(ex, "未知错误，请联系开发者。");
        }
    }
}
