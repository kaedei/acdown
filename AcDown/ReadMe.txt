★请注意★
您正在浏览的是[AcDown动漫下载器]源代码，如果您希望下载可执行文件(.exe)，请访问下列网址：
http://acdown.codeplex.com/releases


此文件更新时间：2012-7-26

系统需求：
正常打开AcDown项目文件需要您的系统安装如下软件/程序：
Visual Studio 2010 (包含C#组件)；
.Net Framework 3.5 SDK；

如果您使用的是Visual Studio 2008，也可以通过打开AcDown.csproj文件打开此工程

正常运行AcDown可执行文件(.exe格式)需要:
至少Windows XP SP3；
系统中已安装.NET Framework 2.0；

正常运行“弹幕播放”组件需要：
至少Windows XP Service Pack 3；

请双击AcDown.sln文件打开项目，如果您遇到有关"无法连接到团队资源管理器"和"项目将以脱机方式打开"的提示，请忽略它；
如果您遇到"是否移除源代码控制"的询问，请选择任意一项。


源代码目录的简单说明：
AcPlay - 与弹幕播放相关的代码，包括acplay可执行文件和Flv文件分析器
bin - 编译后的AcDown应用程序，Debug为包含XML说明的调试版本，Release为编译优化的版本
Core - AcDown任务控制核心，包括插件管理器、任务管理器、设置管理器、日志管理器等等
Downloader - 官方插件
Interface - 接口
obj - 编译时的临时文件
Parser - 部分视频网站的解析器
Resorces - AcDown中用到的图片、图标、声音等资源
UI - 用户界面
AcDown.snk - 强程序集签名所用的Key
Program.cs - 程序入口


如果遇到有关源代码的问题，欢迎来信向作者询问：kaedei@foxmail.com