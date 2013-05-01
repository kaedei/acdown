using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace _30edu.Common
{
	public class FlvInfo
	{
		public FlvInfo()
		{
			Header = new FLVHeader();
			TagList = new List<FLVTag>();
		}
		public FLVHeader Header { get; private set; }
		public List<FLVTag> TagList { get; private set; }
		public TimeSpan Time
		{
			get
			{
				//int time = 0;
				//foreach (FLVTag tag in this.TagList.Where(p=>p.Type)) 
				//{  
				//    byte[] tmp = new byte[4]; 
				//    tmp[3] = tag.TimeEx; 
				//    tmp[0] = tag.Time[2]; 
				//    tmp[1] = tag.Time[1]; 
				//    tmp[2] = tag.Time[0]; 
				//    time += BitConverter.ToInt32(tmp,0); 
				//} 
				FLVTag tag = null;
				foreach (var item in TagList)
				{
					if (item.Type == 9)
						tag = item;
				}
				byte[] tmp = new byte[4];
				tmp[3] = tag.TimeEx;
				tmp[0] = tag.Time[2];
				tmp[1] = tag.Time[1];
				tmp[2] = tag.Time[0];
				return new TimeSpan(0, 0, 0, 0, BitConverter.ToInt32(tmp, 0));
			}
		}
	}

	/**/
	/// <summary> 
	/// 文件头 
	/// </summary> 
	public class FLVHeader
	{
		public FLVHeader()
		{
			this.Type = new byte[3];
			this.Length = new byte[4];
		}
		/**/
		/// <summary> 
		/// 3byte 总是FLV（0x46 0x4C 0x56） 
		/// </summary> 
		public byte[] Type { get; set; }

		/**/
		/// <summary> 
		/// 版本 一般是0x01，表示FLV version 1 
		/// </summary> 
		public byte Version { get; set; }

		/**/
		/// <summary> 
		/// 流信息 倒数第一bit是1表示有视频，倒数第三bit是1表示有音频 
		/// </summary> 
		public byte Stream { get; set; }

		/**/
		/// <summary> 
		/// 长度 4byte 
		/// </summary> 
		public byte[] Length { get; set; }
	}

	public class FLVTag
	{
		public FLVTag()
		{
			this.PreviousTagSize = new byte[4];
			this.DataLength = new byte[3];
			this.Time = new byte[3];
			this.streamsID = new byte[3];

		}
		/**/
		/// <summary> 
		/// 前一个Tag长度 4byte 
		/// </summary> 
		public byte[] PreviousTagSize { get; set; }
		/**/
		/// <summary> 
		///  8 -- 音频tag 9 -- 视频tag 18 -- 脚本tag 
		/// </summary> 
		public byte Type { get; set; }

		/**/
		/// <summary> 
		/// 数据区长度 3byte 
		/// </summary> 
		public byte[] DataLength { get; set; }

		/**/
		/// <summary> 
		/// 时间戳 3byte 毫秒 
		/// </summary> 
		public byte[] Time { get; set; }

		/**/
		/// <summary> 
		/// 扩展时间戳 3byte 毫秒 作为时间戳的高位 
		/// </summary> 
		public byte TimeEx { get; set; }

		/**/
		/// <summary> 
		/// 一般为0 3byte 
		/// </summary> 
		public byte[] streamsID { get; set; }

		/**/
		/// <summary> 
		///  
		/// </summary> 
		public byte[] Data { get; set; }
	}

	public static class FlvInfoHelper
	{
		public static FlvInfo Read(string Path)
		{
			using (FileStream fs = File.OpenRead(Path))
			{
				FlvInfo aFlvInfo = new FlvInfo();
				fs.Read(aFlvInfo.Header.Type, 0, aFlvInfo.Header.Type.Length);
				aFlvInfo.Header.Version = (byte)fs.ReadByte();
				aFlvInfo.Header.Stream = (byte)fs.ReadByte();
				fs.Read(aFlvInfo.Header.Length, 0, aFlvInfo.Header.Length.Length);
				byte[] previoustagsize = new byte[4];
				while (fs.Read(previoustagsize, 0, previoustagsize.Length) != 0)
				{
					FLVTag Tag = new FLVTag();
					Tag.PreviousTagSize = previoustagsize;
					Tag.Type = (byte)fs.ReadByte();
					fs.Read(Tag.DataLength, 0, Tag.DataLength.Length);
					fs.Read(Tag.Time, 0, Tag.Time.Length);
					Tag.TimeEx = (byte)fs.ReadByte();
					fs.Read(Tag.streamsID, 0, Tag.streamsID.Length);

					byte[] tmp = new byte[4];
					tmp[3] = 0;
					tmp[2] = Tag.DataLength[0];
					tmp[1] = Tag.DataLength[1];
					tmp[0] = Tag.DataLength[2];
					int n = System.BitConverter.ToInt32(tmp, 0);
					fs.Seek(n, SeekOrigin.Current);
					aFlvInfo.TagList.Add(Tag);

				}
				return aFlvInfo;
			}
		}

		//获取视频长度
		public static TimeSpan GetVideoDuration(string filePath, string ffmpegExePath)
		{
			try
			{
				string output = "";
				if (File.Exists(ffmpegExePath))
				{
					//视频长度
					Process p = new Process();//建立外部调用线程
					p.StartInfo.FileName = ffmpegExePath;
					p.StartInfo.Arguments = string.Format(@"-i ""{0}""", filePath);
					p.StartInfo.UseShellExecute = false;//不使用操作系统外壳程序启动线程
					p.StartInfo.RedirectStandardError = true;//把外部程序错误输出写到StandardError流中
					p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
					p.StartInfo.CreateNoWindow = true;//不创建进程窗口
					p.Start();//启动线程
					p.WaitForExit(2500);//等待完成
					output = p.StandardError.ReadToEnd();//开始同步读取
					p.Close();//关闭进程
					p.Dispose();//释放资源
				}
				var reg = new Regex(@"Duration: (?<h>\d+):(?<m>\d+):(?<s>\d+)\.(?<ms>\d+)");
				var match = reg.Match(output);

				if (!match.Success)
				{
					var info = Read(filePath);
					return info.Time;
				}
				else
				{
					return new TimeSpan(0, int.Parse(match.Groups["h"].Value),
						int.Parse(match.Groups["m"].Value),
						int.Parse(match.Groups["s"].Value),
						int.Parse(match.Groups["ms"].Value));
				}
			}
			catch
			{
				return new TimeSpan(0, 0, 1);
			}
		}
	}

}