Imports Kaedei.AcDown.Interface

''' <summary>
''' 示例AcDown插件，可以下载网络上的jpg文件
''' </summary>
''' <remarks>SampleDownloaderVBPlugin为AcDown寻找此插件的入口，这个类必须继承IPlugin接口，而且必须有AcDownPluginInformation属性</remarks>
<AcDownPluginInformation("SampleDownloaderVB", "VB.NET编写的AcDown示例插件", "Kaedei", "1.0.0.0", "这是一个用VB.NET编写的AcDown示例插件，用来下载网络上的jpg文件", "http://blog.sina.com.cn/kaedei")> _
Public Class SampleDownloaderVBPlugin : Implements IPlugin '在此处按下回车键能够让VB自动生成实现接口的代码

    ''' <summary>
    ''' 判断是否支持给定的URL
    ''' </summary>
    ''' <param name="url"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckUrl(url As String) As Boolean Implements Kaedei.AcDown.Interface.IPlugin.CheckUrl
        '如果url以 http:// 开始并以 .jpg 结束则支持下载，否则不支持
        If url.StartsWith("http://") AndAlso url.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' 插件独立存储
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Configuration As Kaedei.AcDown.Interface.SerializableDictionary(Of String, String) Implements Kaedei.AcDown.Interface.IPlugin.Configuration


    Private _Feature As System.Collections.Generic.Dictionary(Of String, Object)
    ''' <summary>
    ''' 插件支持的特性
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Feature As System.Collections.Generic.Dictionary(Of String, Object) Implements Kaedei.AcDown.Interface.IPlugin.Feature
        Get
            Return _Feature
        End Get
    End Property


    ''' <summary>
    ''' 针对给定的Url生成唯一的Hash(标识)，每个url必须针对一个固定的标识。
    ''' </summary>
    ''' <param name="url"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHash(url As String) As String Implements Kaedei.AcDown.Interface.IPlugin.GetHash
        '当前规则是 SampleDownloaderVB+给定的url
        Return "SampleDownloaderVB" + url
    End Function

    ''' <summary>
    ''' 当AcDown决定下载这个Url时就会调用CreateDownloader方法来创建一个新的Downloader
    ''' </summary>
    ''' <returns>一个SampleDownloaderVB类的新实例</returns>
    ''' <remarks></remarks>
    Public Function CreateDownloader() As Kaedei.AcDown.Interface.IDownloader Implements Kaedei.AcDown.Interface.IPlugin.CreateDownloader
        Return New SampleDownloaderVB()
    End Function
End Class
