using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LLC_MOD_Toolbox.Helpers;

namespace LLC_MOD_Toolbox.ViewModels;

public partial class LinkViewModel() : ObservableObject
{
    [ObservableProperty]
    private Dictionary<string, Uri> _links =
        new(
            [
                new KeyValuePair<string, Uri>("都市零协会文档站", new Uri("https://www.zeroasso.top")),
                new KeyValuePair<string, Uri>(
                    "都市零协会 BiliBili",
                    new Uri("https://space.bilibili.com/1247764479")
                ),
                new KeyValuePair<string, Uri>(
                    "LLC on GitHub",
                    new Uri("https://github.com/LocalizeLimbusCompany")
                ),
                new KeyValuePair<string, Uri>("爱发电主页", new Uri("https://afdian.com/a/Limbus_zero")),
                new KeyValuePair<string, Uri>(
                    "翻译项目译者",
                    new Uri("https://paratranz.cn/projects/6860/leaderboard")
                ),
                new KeyValuePair<string, Uri>("翻译平台Paratranz", new Uri("https://paratranz.cn")),
                new KeyValuePair<string, Uri>(
                    "零协会周边微店(柔造)",
                    new Uri("https://weidian.com/?userid=1655827241")
                ),
                new KeyValuePair<string, Uri>(
                    "边狱公司灰机Wiki",
                    new Uri("https://limbuscompany.huijiwiki.com")
                ),
                new KeyValuePair<string, Uri>("免费开服平台-简幻欢", new Uri("https://simpfun.cn")),
            ]
        );

    [RelayCommand]
    private static void OpenLink(Uri link)
    {
        UrlHelper.LaunchUrl(link);
    }
}
