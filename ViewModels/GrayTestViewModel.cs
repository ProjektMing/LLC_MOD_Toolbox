using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LLC_MOD_Toolbox.Helpers;
using LLC_MOD_Toolbox.Models;
using Microsoft.Extensions.Logging;

namespace LLC_MOD_Toolbox.ViewModels;

internal partial class GrayTestViewModel(Config config, ILogger<GrayTestViewModel> logger)
    : ObservableObject
{
    [ObservableProperty]
    public partial bool IsGrayTestValid { get; set; }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CheckGrayTestCommand))]
    public partial string? Token { get; set; }

    [RelayCommand(CanExecute = nameof(CanExecuteCheckGrayTest))]
    private void CheckGrayTest(string token)
    {
        UrlHelper.GetTestUrl(token);
        config.Token = token;
        logger.LogInformation("CheckGrayTest executed");
        IsGrayTestValid = true;
    }

    private bool CanExecuteCheckGrayTest() => !string.IsNullOrWhiteSpace(Token);
}
