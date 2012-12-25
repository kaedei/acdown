★请注意★
您正在浏览的是[AcDown动漫下载器]源代码，如果您希望下载可执行文件(.exe)，请访问下列网址：
http://acdown.codeplex.com/releases

★此文件更新时间★
2012-11-12

★系统需求★
正常打开AcDown项目文件需要您的系统安装如下软件/程序：
至少Windows XP SP3
Visual Studio 2010 SP1 至少Professional版本(必须包含C#组件)；
.Net Framework 3.5 SDK；

★如何打开项目文件★
请双击AcDown.sln文件打开项目，如果您遇到有关"无法连接到团队资源管理器"和"项目将以脱机方式打开"的提示，请忽略它；
如果您遇到"是否移除源代码控制"的询问，请选择任意一项。

★源代码各个项目与目录的简单说明★
AcDown - AcDown用户界面
AcDownCore - AcDown任务控制核心，包括插件管理器、任务管理器、设置管理器、日志管理器等等
AcDownDownloader - 官方插件
AcDownInterface - 接口
AcDownParser - 部分视频网站的解析器
FlvCombine - 视频合并组件
AcPlay - 与弹幕播放相关的代码，包括acplay可执行文件和Flv文件分析器
Release - 将AcDown编译版本合并为单exe文件
SampleDownloaderVB - 使用VB.NET开发的AcDown示例插件
BingEveryday - 使用C#开发的Bing壁纸下载插件，这个插件是一个外部插件，需要通过双击生成的.acp文件才可以被加载到AcDown中

★如何生成项目★
请在“解决方案资源管理器”中右键点击AcDown项目，在弹出菜单中选择“生成”

★如何生成单EXE★
Release项目使用ILMerge(http://research.microsoft.com/en-us/people/mbarnett/ilmerge.aspx)来合并程序集。
请先在“生成”菜单-配置管理器 中将活动解决方案切换到“Release”，
然后在“解决方案资源管理器”中右键点击Release项目，在弹出菜单中选择“生成”。
合并后的EXE文件位置是【解决方案文件夹\Release\bin\Release\Public\AcDown.exe】

★其他★
如果遇到有关源代码的问题，欢迎来信向作者询问：kaedei@foxmail.com