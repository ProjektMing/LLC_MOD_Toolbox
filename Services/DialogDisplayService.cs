using System.Windows;
using Microsoft.Extensions.Logging;

namespace LLC_MOD_Toolbox.Services;

public class DialogDisplayService(ILogger<DialogDisplayService> logger) : IDialogDisplayService
{
    public void ShowError(string message)
    {
        MessageBox.Show(message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public bool Confirm(string message)
    {
        logger.LogInformation("显示确认对话框，内容：{message}", message);
        if (
            MessageBox.Show(message, "确认", MessageBoxButton.YesNo, MessageBoxImage.Question)
            == MessageBoxResult.Yes
        )
        {
            logger.LogInformation("用户确认了操作。");
            return true;
        }
        logger.LogInformation("用户取消了操作。");
        return false;
    }
}
