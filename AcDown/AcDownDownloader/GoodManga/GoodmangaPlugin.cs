using Kaedei.AcDown.Interface;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Kaedei.AcDown.Downloader
{
    /// <summary>
    /// 动漫之家(178漫画)下载插件
    /// </summary>
    [AcDownPluginInformation("GoodmangaDownloader", "Goodmanga漫画网下载插件", "beyond", "1.0.0.0", "Goodmanga漫画网下载插件", "http://www.cnblogs.com/beyondblog")]
    public class GoodmangaPlugin : IPlugin
    {
        public IDownloader CreateDownloader()
        {
            return new GoodmangaDownloader();
        }

        public bool CheckUrl(string url)
        {
            Regex r = new Regex(@"^http://www\.goodmanga\.net/(?<id>\S+)+");
            return r.Match(url).Success;
        }

        /// <summary>
        /// Dmzj+GUID
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetHash(string url)
        {
            if (CheckUrl(url))
            {
                return "Dmzj" + Guid.NewGuid().ToString();
            }
            else
            {
                return null;
            }
        }

        public Dictionary<string, object> Feature
        {
            get;
            private set;
        }

        public SerializableDictionary<string, string> Configuration
        {
            get;
            set;
        }
    }
}
