# 都市零协会工具箱-重构分支（`wpf_v2`）

> [!IMPORTANT]
>
> 非常抱歉，经由个人评估，该项目**不再**具备重构的价值，因此决定归档。
>
> 经过重构，该项目已具备优化后的视图结构和较为规整的设计框架，但因上游API变更，代码逻辑应重新设计。
>
> 致想重启的开发者，有以下几点注意事项作为提醒（当前时间 2025-09-26）
>
> - 网络API需要在上游代码中重新寻找，Api.md 已失效（不要相信`NodeList.json`提供的）；
> - 未实现任何与Mirror酱相关的功能，不确定是否与当前设计兼容（推荐实现 [IFileDownLoadService](./Services/FileDownloadService.cs)）；
> - 存在一个已知bug，未处理异常`FileDownloadService.InstallLanguagePackageAsync`，因为代码是异步的，可能要额外处理；
> - 建议将所有文案转移到 [Assets/I18n/zh-CN.xaml](Assets/I18n/zh-CN.xaml)，即使不进行 I18n （实际上也不可能）也可以使代码更直观，由于个人进度原因从未使用；
> - 灰度测试正式名字叫 Gray Release，个人推荐改成 Canary（最符合实际流程），未完全实现这个功能，因为我个人其实不清楚为什么要做这个；
> - 热更新和实际用处没有一点关系，请不要叫这个名字，另外这个只实现了基本逻辑；
> - [docs/](docs/) 基本等同于没写；
> - 如有任何问题，欢迎通过任何主页提供的方式联系我；
> - 最后，如果愿意重构代码的“您”存在，我作为零协会的边缘成员感谢您对零协会的支持

`![LLC_MOD_Toolbox](https://socialify.git.ci/LocalizeLimbusCompany/LLC_MOD_Toolbox/image?description=1&descriptionEditable=%E9%83%BD%E5%B8%82%E9%9B%B6%E5%8D%8F%E4%BC%9A%E5%B7%A5%E5%85%B7%E7%AE%B1&font=Inter&forks=1&issues=1&language=1&logo=https%3A%2F%2Fwww.zeroasso.top%2Fimg%2Flogo.png&name=1&owner=1&pattern=Circuit%20Board&pulls=1&stargazers=1&theme=Light)`

## 介绍

具体请看[文档站](https://www.zeroasso.top)

## 构建

```cmd
git clone
cd ./LLC_MOD_Toolbox
dotnet build
```

## 贡献

请参考[贡献指南](./docs/CONTRIBUTING.md)

## 出现的较专业问题

详见[FAQ](./docs/FAQ.md)