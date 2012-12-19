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
		protected string DecodeXiamiURL(string s)
		{
			s = s.Trim ();
			if (s.Length == 0) return null;
			List<string> result = new List<string>();
			int line = Int32.Parse(s[0].ToString());
			s = s.Substring (1);
			int rows = s.Length / line;
			int extra = s.Length % line;
			
			for (int x = 0; x < extra; x++) {
				result.Add(s.Substring ((rows + 1) * x, rows + 1));
			}
			
			for (int x = 0; x < line - extra; x++) {
				result.Add(s.Substring ((rows + 1) * extra + (rows * x), rows));
			}
			
			StringBuilder url = new StringBuilder();
			for (int x = 0; x < rows + 1; x++) {
				for (int y = 0; y < line; y++) {
					if (x < result[y].Length)
						url.Append (result[y][x]);
				}
			}

			return System.Uri.UnescapeDataString(url.ToString()).Replace ('^', '0');
		}
		
		protected class Track
		{
			public string Title;
			public string Album;
			public string Artist;
			public string LyricURL;
			public string PictureURL;
			public string SongURL;
			public string Filename;
		}
		
		protected enum PlaylistType
		{
			Song = 0,
			Album = 1,
			Artist = 2,
			Collect = 3,
		}
		
		protected Track[] GetPlaylist(string id, PlaylistType type)
		{
			string source = Network.GetHtmlSource(@"http://www.xiami.com/song/playlist/id/" + id + "/type/" + ((int)type).ToString(), Encoding.UTF8, Info.Proxy);
			MatchCollection matches = Regex.Matches(source, "<track>(.*?)</track>", RegexOptions.Singleline);
			List<Track> tracks = new List<Track>();
			foreach (Match item in matches) {
				string src = item.Groups[1].Value;
				Track track = new Track();
				var title = Regex.Match(src, @"<title><!\[CDATA\[(?<var>.*)\]\]></title>").Groups["var"];
				var songid = Regex.Match(src, @"<song_id>(?<var>.*)</song_id>").Groups["var"];
				track.Title = (title != null && !String.IsNullOrEmpty(title.Value) ? title.Value : "未知歌曲" + (
					(songid != null && !String.IsNullOrEmpty(songid.Value)) ? " (" + songid.Value + ")" : ""
				));
				
				var album = Regex.Match(src, @"<album_name><!\[CDATA\[(?<var>.*)\]\]></album_name>").Groups["var"];
				track.Album = (album != null && !String.IsNullOrEmpty(album.Value) ? album.Value : "(未知专辑)");
				
				var artist = Regex.Match(src, @"<artist><!\[CDATA\[(?<var>.*)\]\]></artist>").Groups["var"];
				track.Artist = (artist != null && !String.IsNullOrEmpty(artist.Value) ? artist.Value : "(未知艺术家)");
				
				var lyric = Regex.Match(src, @"<lyric>(?<var>.*)</lyric>").Groups["var"];
				if (lyric != null && !String.IsNullOrEmpty(lyric.Value))
					track.LyricURL = lyric.Value;
				
				var pic = Regex.Match(src, @"<pic>(?<var>.*)</pic>").Groups["var"];
				if (pic != null && !String.IsNullOrEmpty(pic.Value))
					track.PictureURL = pic.Value;
				
				var location = Regex.Match(src, @"<location>(?<var>.*)</location>").Groups["var"];
				if (location != null && !String.IsNullOrEmpty(location.Value))
					track.SongURL = DecodeXiamiURL (location.Value);
				
				track.Filename = track.Title + " - " + track.Album + " - " + track.Artist;
				
				tracks.Add (track);
			}
			
			return tracks.ToArray();
		}
		
		public override bool Download()
		{
			TipText("开始分析");
			Track[] tracks;
			string folder = "";
			Match match;
			
			Dictionary<string, string> additionalFiles = new Dictionary<string, string>();
			
			match = Regex.Match(Info.Url, XiamiPlugin.RegexPatternSong);
			if (match.Groups["var"].Success)
			{
				tracks = GetPlaylist(match.Groups["var"].Value, PlaylistType.Song);
				if (tracks.Length > 0)
				{
					Info.Title = tracks[0].Filename;
				}
				else
					return true;
			}
			else
			{
				match = Regex.Match(Info.Url, XiamiPlugin.RegexPatternArtist);
				if (match.Groups["var"].Success)
				{
					tracks = GetPlaylist(match.Groups["var"].Value, PlaylistType.Artist);
					if (tracks.Length > 0)
					{
						Info.Title = tracks[0].Artist;
						folder = Info.Title;
					}
					else
						return true;
				}
				else
				{
					match = Regex.Match(Info.Url, XiamiPlugin.RegexPatternAlbum);
					if (match.Groups["var"].Success)
					{
						tracks = GetPlaylist(match.Groups["var"].Value, PlaylistType.Album);
						if (tracks.Length > 0)
						{
							for (int i = 0; i < tracks.Length; i++) {
								tracks[i].Filename = String.Format("{0:D2} {1} - {2}", i + 1, tracks[i].Title, tracks[i].Artist);
							}								
							
							Info.Title = tracks[0].Artist + " - " + tracks[0].Album;
							folder = Info.Title;
						}
						else
							return true;
					}
					else
					{
						match = Regex.Match(Info.Url, XiamiPlugin.RegexPatternCollect);
						if (match.Groups["var"].Success)
						{
							tracks = GetPlaylist(match.Groups["var"].Value, PlaylistType.Collect);
							if (tracks.Length == 0) return true;
							
							for (int i = 0; i < tracks.Length; i++) {
								tracks[i].Filename = String.Format("{0:D2} {1}", i + 1, tracks[i].Filename);
							}
							
							string source = Network.GetHtmlSource(@"http://www.xiami.com/song/showcollect/id/" + match.Groups["var"].Value, Encoding.UTF8, Info.Proxy);
							var title = Regex.Match(source, @"<title>(?<var>.*)</title>").Groups["var"];
							Info.Title = (title != null && !String.IsNullOrEmpty(title.Value) ? title.Value : "未知精选集 (" + match.Groups["var"].Value + ")");
							folder = Info.Title;
							
							var cover = Regex.Match(source, "<a class=\"bigImgCover\" rel=\"nofollow\" target=\"_blank\" href=\"(?<var>.*)\">").Groups["var"];
							if (cover != null && !String.IsNullOrEmpty(cover.Value))
								additionalFiles.Add ("精选集封面.jpg", cover.Value);
							
							var pics = Regex.Matches(source, "<a href=\"(?<var>.*)\" target=\"_blank\" rel=\"nofollow\" class=\"bigImg\">");
							int count = 1;
							foreach (Match item in pics) {
								additionalFiles.Add (String.Format("精选集图片 ({0:D2}).jpg", count++), item.Groups["var"].Value);
							}
						}
						else
						{
							return false;
						}
					}
				}
			}
			if (tracks.Length > 1 && String.IsNullOrEmpty (folder))
				folder = Info.Title;
			
			string localFolder = Info.SaveDirectory.FullName;
			if (!String.IsNullOrEmpty (folder))
			{
				string newdir = Path.Combine(Info.SaveDirectory.ToString(), folder);
				if (!Directory.Exists(newdir)) Directory.CreateDirectory(newdir);
				localFolder = newdir;
			}
			
			if (additionalFiles.Count > 0)
			{
				foreach (KeyValuePair<string, string> item in additionalFiles) {
					p = new DownloadParameter();
					p.Url = item.Value;
					p.FilePath = System.IO.Path.Combine(localFolder, Tools.InvalidCharacterFilter (item.Key, "_"));
					p.Proxy = Info.Proxy;
					try{
						DownloadWithRescue(p);
					}catch{}
				}
			}
			
			int part = 1;
			foreach (Track track in tracks) {
				string filePath = System.IO.Path.Combine(localFolder, Tools.InvalidCharacterFilter (track.Filename, "_"));
	
				NewPart(part++, tracks.Length);
	
				if (!String.IsNullOrEmpty(track.LyricURL))
				{
					p = new DownloadParameter();
					p.Url = track.LyricURL;
					p.FilePath = filePath + ".lrc";
					p.Proxy = Info.Proxy;
					if (!DownloadWithRescue(p)) return false;
				}
				
				if (!String.IsNullOrEmpty(track.PictureURL))
				{
					p = new DownloadParameter();
					p.Url = track.PictureURL;
					p.FilePath = filePath + ".jpg";
					p.Proxy = Info.Proxy;
					if (!DownloadWithRescue(p)) return false;
				}
				
				if (!String.IsNullOrEmpty(track.SongURL))
				{
					p = new DownloadParameter();
					p.Url = track.SongURL;
					p.FilePath = filePath + ".mp3";
					p.Proxy = Info.Proxy;
					if (!DownloadWithRescue(p)) return false;
				}
			}
			
			return true;
		}
		
		protected bool DownloadWithRescue(DownloadParameter p)
		{
			//下载视频
			try
			{
				this.currentParameter = p;
				if (!Network.DownloadFile(currentParameter, this.Info)) //未出现错误即用户手动停止
				{
					return false;
				}
			}
			catch (Exception ex) //下载文件时出现错误
			{
				//如果此任务由一个视频组成,则报错（下载失败）
				if (Info.PartCount == 1)
				{
					throw ex;
				}
				else //否则继续下载，设置“部分失败”状态
				{
					Info.PartialFinished = true;
					Info.PartialFinishedDetail += "\r\n文件: " + currentParameter.Url + " 下载失败";
				}
			}
			return true;
		}
	}
}
