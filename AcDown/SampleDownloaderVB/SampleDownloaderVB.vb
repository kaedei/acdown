Imports Kaedei.AcDown.Interface.Downloader
Imports Kaedei.AcDown.Interface

''' <summary>
''' 每次使用SampleDownloaderVBPlugin插件新建任务时，就会实例化此类的一个新实例
''' </summary>
''' <remarks>下载器类可以选择实现IDownloader，也可以直接继承自更简单的CommonDownloader类</remarks>
Public Class SampleDownloaderVB : Inherits CommonDownloader

    ''' <summary>
    ''' 只需要重写Download()方法即可
    ''' </summary>
    ''' <returns>下载成功返回True，用户手动取消返回False</returns>
    ''' <remarks>如果下载出现错误请直接抛出异常，AcDown会自动进行处理</remarks>
    Public Overrides Function Download() As Boolean

        '提示用户下载已经开始。使用TipText函数
        TipText("开始处理")

        '与此任务有关的所有信息都存放在Info对象中
        '获取图片文件名（如abcd.jpg）
        Dim fileName = System.IO.Path.GetFileName(Info.Url)

        '获取用户想把图片下载到的位置(如C:\)
        Dim localFolder = Info.SaveDirectory.FullName '用户指定的文件夹

        '将位置和文件名合并即是最后的文件路径(如C:\abcd.jpg)
        Dim filePath = System.IO.Path.Combine(localFolder, fileName)

        '别忘了设定这个下载任务的标题
        Info.Title = "[图片]" + fileName

        '提示AcDown这个任务一共只有一个子任务，当前正在进行第一个
        NewPart(1, 1)

        '开始下载
        '设定一些下载的详细参数
        'p是当前下载的默认参数，是一个已经定义(实例化)好的DownloadParameter类型的变量，可以直接使用
        p.Url = Info.Url
        p.FilePath = filePath
        '支持用户所选择的代理服务器
        p.Proxy = Info.Proxy

        '调用DownloadFile()方法进行下载，具体下载过程不用理会
        'AcDown会自动显示下载进度与下载速度
        Dim success As Boolean = DownloadFile()

        '下载完成后success如果为True证明下载成功，如果为False证明用户取消了下载
        '如果出现了错误（如抛出网络异常），不需要使用try...catch块进行截获，请直接将异常抛给AcDown。
        '直接返回success即可
        Return success

    End Function
End Class
