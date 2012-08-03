using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;

namespace Kaedei.AcDown.Downloader
{

	/// <summary>
	/// FLVCD解析插件
	/// </summary>
	[AcDownPluginInformation("FlvcdDownloader","在线解析插件","Kaedei","3.11.5.425","Flvcd.com解析插件","http://blog.sina.com.cn/kaedei")]
	public class FlvcdPlugin : IPlugin
	{
		const string supportedurl = @"http://www.youku.com/|http://v.youku.com/|http://www.56.com/|http://mm.56.com/|http://tieba.56.com/|http://tv.56.com/|http://v.ku6.com/|http://hd.ku6.com/|http://6.cn/|http://you.video.sina.com.cn/|http://v.sina.com.cn/|http://video.sina.com.cn/|http://sports.sina.com.cn/|http://ent.sina.com.cn/|http://eladies.sina.com.cn/|http://tieba.baidu.com/|http://mv.baidu.com/|http://boke.qq.com/|http://cgi.video.qq.com/|http://live.qq.com/|http://cgi.video.qq.com/|http://bb.news.qq.com/|http://sports.qq.com/|http://finance.qq.com/|http://space.tv.cctv.com/|http://www.cctv.com/|http://v.cctv.com/|http://video.cctv.com/|http://sports.cctv.com/|http://space.tv.cctv.com/|http://v.wangyou.com/|http://tv.mofile.com/|http://v.mofile.com/|http://www.espnstar.com.cn/|http://video.espnstar.com.cn/|http://vlog.17173.com/|http://media.17173.com/|http://www.metacafe.com/|http://www.veoh.com/|http://you.joy.cn/|http://tvplay.joy.cn/|http://movie.joy.cn/|http://db.joy.cn/|http://auto.joy.cn/|http://you.joy.cn/|http://v.joy.cn/|http://home.pomoho.com/|http://www.megavideo.com/|http://www.stage6.com/|http://movie.gougou.com/|http://www.im.tv/|http://hk.im.tv/|http://play.hupo.tv/|http://ktv.hupo.tv/|http://www.openv.com/|http://vsearch.cctv.com/|http://ent.openv.com/|http://hd.openv.com/|http://vsearch.cctv.com/|http://t.openv.com/|http://v.ifeng.com/|http://tv.v1.cn/|http://v.v1.cn/|http://shehui.v1.cn/|http://miniv.v1.cn/|http://v.v1.cn/|http://msn.v1.cn/|http://zhangmen.baidu.com/|http://news.replays.net/|http://www.wfbrood.com/|http://news.163.com/|http://ent.163.com/|http://v.163.com/|http://www.yiyi.cc/|http://www.ouou.com/|http://www.boosj.com/|http://movie.boosj.com/|http://v.pcgames.com.cn/|http://j.pcgames.com.cn/|http://www.sk-gaming.com/|http://v.zol.com.cn/|http://game.zol.com.cn/|http://info.smgbb.cn/|http://tvshow.smgbb.cn/|http://msn.smgbb.cn/|http://youku.soufun.com/|http://home.sogua.com/|http://www.acfun.tv/|http://124.228.254.229/|http://vblog.hunantv.com/|http://ent.hunantv.com/|http://vblog.hunantv.com/|http://www.top100.cn/|http://www.aipai.com/|http://art.china.cn/|http://www.letv.com/|http://news.mtime.com/|http://i.mtime.com/|http://movie.mtime.com/|http://i.mtime.com/|http://www.break.com/|http://www.stupidvideos.com/|http://www.collegehumor.com/|http://www.imgo.tv/|http://news.xinhuanet.com/|http://www.google.cn/|http://www.1ting.com/|http://www.wat.tv/|http://tv.esl.eu/|http://news.cntv.cn/|http://www.jsbc.com/|http://www.jstv.com/|http://tv.btv.com.cn/|http://space.btv.com.cn/|http://www.gztv.com/|http://taobao.joy.cn/|http://www.umiwi.com/|http://www.qiyi.com/|http://www.m1905.com/|http://www.yinyuetai.com/|http://www.11688.net/|http://v.bokecc.com/|http://union.bokecc.com/|http://dv.ce.cn/|http://www.bilibili.tv/|http://live.jingtime.com/|http://download.5721.net/|http://tv.cztv.com/|http://n.cztv.com/|http://tv.tom.com/|http://www.xiami.com/|http://www.top100.cn/|http://www.5min.com/|http://winout.net/|http://www.wzipad.com/|http://www.plu.cn/|http://www.howcast.com/|http://v.zhiyin.cn/|http://video.chaoxing.com/|http://www.taihaitv.cn/|http://video.baidu.com/|http://xiyou.cntv.cn/|http://bugu.cntv.cn/|http://www.letv.com/|http://so.letv.com/|http://www.vhxsd.com/";
		string[] urls;

		public FlvcdPlugin()
		{
			Feature = new Dictionary<string, object>();
			//GetExample
			Feature.Add("ExampleUrl", GetUrlExample());
			//AutoAnswer(不支持)
			//ConfigurationForm(不支持)
		}

		public IDownloader CreateDownloader()
		{
			return new FlvcdDownloader();
		}

		public bool CheckUrl(string url)
		{
			if (urls == null)
				urls = supportedurl.Split('|');
			foreach (string item in urls)
			{
				if (url.StartsWith(item))
					return true;
			}
			return false;
		}

		/// <summary>
		/// 规则为 flvcd + url地址
		/// 如 "flvcdhttp://v.youku.com/v_show/id_XMjE1MTYyNzAw.html"
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public string GetHash(string url)
		{
			Regex r = new Regex(@"^(?<url>http.+)");
			Match m = r.Match(url);
			if (m.Success)
			{
				return "flvcd" + m.Groups["url"].Value;
			}
			else
			{
				return null;
			}
		}

		private string[] GetUrlExample()
		{
			if (urls == null)
				urls = supportedurl.Split('|');
			List<string> t = new List<string>() { 
				"FlvCD解析插件:",
				"可以解析的地址如下：（以下列URL为开头的视频页面）"
			};
			foreach (var item in urls)
			{
				t.Add(item);
			}
			return t.ToArray();
		}


		public Dictionary<string, object> Feature { get; private set; }

		public SerializableDictionary<string, string> Configuration { get; set; }
	}
}
