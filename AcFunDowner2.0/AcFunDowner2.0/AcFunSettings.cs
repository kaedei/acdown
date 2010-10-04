/* AcFunSettings.cs
 * 
 * class AcFunSettings:
 * 定义AcFunDowner程序设置的类
 * 
 * class Config:
 * 设置/保存AcFunDowner程序设置的静态类
 * 
 * 最后更新：2010-1-10
 * 
 * Copyright 2010 Kaedei Software

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 * 
 * http://blog.sina.com.cn/kaedei
 * mailto:kaedei@foxmail.com
 * 
 */



using System;
using System.IO;
using System.Xml.Serialization;

namespace Kaedei.AcFunDowner
{
    [Serializable]
    public class AcFunSettings
    {
        //程序设置
		 public bool WatchClipboardEnabled = true; //监视剪贴板
        public bool DownSub=true  ; //下载字幕
        public string SavePath=Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //保存文件夹
        public bool AutoDownAllSection ; //自动下载所有分段
        public Int32 CacheSize=1; //缓存文件
        public bool OpenFolderAfterComplete; //完成后打开文件夹
        public bool PlaySound=true; //播放声音
		  public string SoundFile = ""; //声音文件路径 (wav格式)
        public bool EnableLog = false; //运行记录日志
		  public bool AutoCheckUrl = true; //自动检查URL
		  public bool ShowTrayIcon = true; //显示托盘图标
		  public bool ShowBigStartButton = true; //显示大按钮
		  public Int32 MaxRunningTaskCount = 2; //最多同时运行任务数量
		  public bool SaveWhenAbort = true; //任务停止或错误退出时保存已经下载的部分
		  public string SearchEngine = "Google"; //默认的搜索引擎。还可以是Google或Baidu
		  public string SearchQuery = @""; //搜索url
		  public bool EnableWindows7Feature = true; //允许使用Windows7特性
		  public bool DeleteTaskAndFile = false; //删除任务的同时删除文件
		 

        //全局设置
        public string ServerIP = @"220.170.79.105|220.170.79.109"; //服务器IP
        public string CheckNewXmlUri="http://blog.sina.com.cn/kaedei"; //检查新版本URL
		  

     }

    public static class Config
    {
        public static AcFunSettings setting { get; set; }
        
        /// <summary>
        /// 保存设置
        /// </summary>
        public static void SaveSettings()
        {
           
                //取得APPDATA路径名称
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                path = Path.Combine(path, @"Kaedei\AcFunDowner\");

                if (!Directory.Exists(path))
                {
                    //如果目录不存在则创建
                    Directory.CreateDirectory(path);
                }

                //序列化设置
                using (FileStream fs = new FileStream(path += @"config.xml", FileMode.Create))
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(AcFunSettings));
                    formatter.Serialize(fs, setting);
                }
            
        }

        /// <summary>
        /// 读取设置
        /// </summary>
        /// <returns></returns>
        public static AcFunSettings LoadSettings()
        {
            try
            {
                AcFunSettings s;
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                path = Path.Combine(path, @"Kaedei\AcFunDowner\config.xml");
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(AcFunSettings));
                    s = (AcFunSettings)formatter.Deserialize(fs);
                }

                if (s != null)
                    setting = s;
                else
                    throw new Exception();
            }
            catch
            {
                setting = new AcFunSettings();
                SaveSettings();
            }
            return setting; 
        }

    


            /// <summary>
        /// 判断当前系统是否为Windows7
        /// </summary>
        /// <returns></returns>
        public static bool IsWindows7()
        {
            if (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor >= 1)
                return true;
            else
                return false;
        }

        public static bool IsWindowsVista()
        {
            if (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 0)
                return true;
            else
                return false;
        }

        public static bool IsWindowsVistaOr7()
        {
            if (Environment.OSVersion.Version.Major == 6 )
                return true;
            else
                return false;
        }
    }
    //[Serializable]
    //class CheckNewXml
    //{
    //    public Version LastestVersion;
    //    public DateTime PublishDate;
    //    public string Describe;
    //}
}
