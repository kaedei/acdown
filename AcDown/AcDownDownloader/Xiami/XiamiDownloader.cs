using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface.Downloader;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;
using System.IO;

namespace Kaedei.AcDown.Downloader
{
	public class XiamiDownloader : CommonDownloader
	{
		public override bool Download()
		{
			TipText("开始分析");

			string id = Info.Url.Substring(26);
			string info = @"http://www.xiami.com/song/playlist/id/" + id + "/type/0";
			string source = Network.GetHtmlSource(info, Encoding.UTF8, Info.Proxy);

			var title = Regex.Match(source, @"<title><!\[CDATA\[(?<var>.*)\]\]></title>").Groups["var"];
			var album = Regex.Match(source, @"<album_name><!\[CDATA\[(?<var>.*)\]\]></album_name>").Groups["var"];
			var artist = Regex.Match(source, @"<artist><!\[CDATA\[(?<var>.*)\]\]></artist>").Groups["var"];
			string name = (title != null && !String.IsNullOrEmpty(title.Value) ? title.Value : "未知歌曲 (" + id + ")");
			string folder = (artist != null && !String.IsNullOrEmpty(artist.Value) ? artist.Value : "(未知艺术家)") + " - " + (album != null && !String.IsNullOrEmpty(album.Value) ? album.Value : "(未知专辑)");
			var lyric = Regex.Match(source, @"<lyric>(?<var>.*)</lyric>").Groups["var"];
			var pic = Regex.Match(source, @"<pic>(?<var>.*)</pic>").Groups["var"];

			Info.Title = name;

			string fileName = name;
			foreach (char item in System.IO.Path.GetInvalidPathChars())
			{
				fileName = fileName.Replace(item, '_');
			}

			string newdir = Path.Combine(Info.SaveDirectory.ToString(), folder);
			if (!Directory.Exists(newdir)) Directory.CreateDirectory(newdir);
			Info.SaveDirectory = new DirectoryInfo(newdir);

			string localFolder = Info.SaveDirectory.FullName;
			string filePath = System.IO.Path.Combine(localFolder, fileName);

			NewPart(1, 1);

			bool result = false;
			p.Url = @"http://dynamic.cdn.9bu.org/xiami/" + id;
			p.FilePath = filePath + ".mp3";
			p.Proxy = Info.Proxy;
			result = DownloadFile();
			if (!result) return false;

			if (lyric != null && !String.IsNullOrEmpty(lyric.Value))
			{
				TipText("下载歌词");
				p.Url = lyric.Value;
				p.FilePath = filePath + ".lrc";
				p.Proxy = Info.Proxy;
				result = DownloadFile();
				if (!result) return false;
			}

			if (pic != null && !String.IsNullOrEmpty(pic.Value))
			{
				TipText("下载封面");
				p.Url = pic.Value;
				p.FilePath = filePath + ".jpg";
				p.Proxy = Info.Proxy;
				result = DownloadFile();
				if (!result) return false;
			}

			return result;
		}
	}
}
