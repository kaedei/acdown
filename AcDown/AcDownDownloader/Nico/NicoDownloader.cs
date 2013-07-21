using System;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;
using System.Net;
using Kaedei.AcDown.Interface.Forms;
using System.IO;
using Kaedei.AcDown.Interface.Downloader;

namespace Kaedei.AcDown.Downloader
{
	public class NicoDownloader : CommonDownloader
	{
		public override bool Download()
		{
			//开始下载
			TipText("正在开始任务");

			//获取视频编号
			string sm = Regex.Match(Info.Url, @"(?<=(http://www\.nicovideo\.jp/watch/|))sm\d+").Value;
			//修正简写URL
			if (Info.Url.StartsWith("sm", StringComparison.CurrentCultureIgnoreCase))
				Info.Url = "http://www.nicovideo.jp/watch/" + Info.Url;

			//获取网页源代码
			string src = Network.GetHtmlSource(Info.Url, Encoding.UTF8, Info.Proxy);
			//匹配登录按钮
			Match mLoginButton = Regex.Match(src, "(?<=アカウント新規登録へ.+?<a href=\").+?(?=\">.+?ログイン画面へ)");
			//获取登录地址
			string loginUrl = mLoginButton.Value;

			TipText("正在登录Nico");

			//向网页Post数据
			CookieContainer cookies = new CookieContainer();
			//登录NicoNico
			UserLoginInfo user;
			//检查插件配置
			try
			{
				user = new UserLoginInfo();
				user.Username = Encoding.UTF8.GetString(Convert.FromBase64String(Info.Settings["user"]));
				user.Password = Encoding.UTF8.GetString(Convert.FromBase64String(Info.Settings["password"]));
				if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
					throw new Exception();
			}
			catch
			{
				user = ToolForm.CreateLoginForm(null,"https://secure.nicovideo.jp/secure/register");
				Info.Settings["user"] = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Username));
				Info.Settings["password"] = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Password));
			}
			//Post的数据
			string postdata = string.Format("next_url={0}&mail={1}&password={2}", sm, Tools.UrlEncode(user.Username), Tools.UrlEncode(user.Password));
			byte[] data = Encoding.UTF8.GetBytes(postdata);
			//生成请求
			HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://secure.nicovideo.jp/secure/login?site=niconico");
			req.Proxy = Info.Proxy;
			req.AllowAutoRedirect = false;
			req.Method = "POST";
			req.Referer = loginUrl;
			req.ContentType = "application/x-www-form-urlencoded";
			req.ContentLength = data.Length;
			req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:16.0) Gecko/20100101 Firefox/16.0";
			req.CookieContainer = new CookieContainer();
			//发送POST数据
			using (var outstream = req.GetRequestStream())
			{
				outstream.Write(data, 0, data.Length);
				outstream.Flush();
			}
			//关闭请求
			req.GetResponse().Close();
			cookies = req.CookieContainer; //保存cookies

			TipText("正在解析视频标题");

			//解析视频标题
			HttpWebRequest reqTitle = (HttpWebRequest)HttpWebRequest.Create(Info.Url);
			reqTitle.Proxy = Info.Proxy;
			//设置cookies
			reqTitle.CookieContainer = cookies;
			//获取视频信息
			string srcTitle = Network.GetHtmlSource(reqTitle, Encoding.UTF8);
			//视频标题
			Info.Title = Regex.Match(srcTitle, @"(?<=<title>).+?(?=</title>)").Value.Replace("‐ ニコニコ動画(原宿)", "").Trim();
			//Info.Title = Regex.Match(srcTitle, @"(?<=<span class=""videoHeaderTitle"">).+?(?=</span>)").Value;
			string title = Tools.InvalidCharacterFilter(Info.Title, "");

			TipText("正在分析视频地址");

			//通过API获取视频信息
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(@"http://flapi.nicovideo.jp/api/getflv/" + sm);
			request.Method = "POST";
			request.Proxy = Info.Proxy;
			//设置cookies
			request.CookieContainer = cookies;
			//获取视频信息
			string videoinfo = Network.GetHtmlSource(request, Encoding.ASCII);
			//视频真实地址
			string video = Regex.Match(videoinfo, @"(?<=url=).+?(?=&)").Value;
			video = video.Replace("%3A", ":").Replace("%2F", "/").Replace("%3F", "?").Replace("%3D", "=");

			//下载视频
			NewPart(1, 1);

			//设置下载参数
			currentParameter = new DownloadParameter()
			{
				//文件名 例: c:\123(1).flv
				FilePath = Path.Combine(Info.SaveDirectory.ToString(),
							title + ".mp4"),
				//文件URL
				Url = video,
				//代理服务器
				Proxy = Info.Proxy,
				//Cookie
				Cookies = cookies,
				//提取缓存
				ExtractCache = Info.ExtractCache,
				ExtractCachePattern = "fla*.tmp"
			};

			//下载视频
			bool success = Network.DownloadFile(currentParameter, this.Info);
			if (!success) //未出现错误即用户手动停止
			{
				return false;
			}

			return true;
		}
	}
}
