using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using Kaedei.AcDown.Core;
using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Interface.AcPlay;
using System.Text;
using Kaedei.AcPlay;
using System.Net;

namespace Kaedei.AcDown.UI.Components
{
	public partial class AcPlay2 : UserControl
	{
		private string lastSelectDirectory;

		#region 控件加载



		public AcPlay2()
		{
			InitializeComponent();
		}

		private void AcPlay2_Load(object sender, EventArgs e)
		{
			//设置vista效果
			DwmApi.SetListViewVisualEffect(lsv);
			//设置盾牌图标
			DwmApi.SetShieldIcon(btnStart);
			//选择下拉列表框
			cboPlayer.SelectedIndex = 0;
		}
		#endregion

		#region 外部接口
		private AcPlayConfiguration config = new AcPlayConfiguration()
		{
			HttpServerPort = new Random().Next(10001, 30000),
			ProxyServerPort = new Random().Next(30001, 60000)
		};

		/// <summary>
		/// 直接播放指定的配置文件
		/// </summary>
		/// <param name="filePath">配置文件路径</param>
		public void PlayConfig(string filePath)
		{
			try
			{
				FillFromConfig(filePath);
				Play(filePath);
			}
			catch { }
		}

		/// <summary>
		/// 从配置文件中填充UI设置
		/// </summary>
		/// <param name="filePath"></param>
		public void FillFromConfig(string filePath)
		{
			//加载设置
			LoadConfigFromFile(filePath);
			//清除Listview
			lsv.Items.Clear();
			//设置下拉列表
			switch (config.PlayerName)
			{
				case "acfun":
					cboPlayer.SelectedIndex = 0;
					break;
				case "bilibili":
					cboPlayer.SelectedIndex = 1;
					break;
				default:
					cboPlayer.SelectedIndex = 0;
					break;
			}
			//设置视频
			foreach (var v in config.Videos)
			{
				//填充listviewitem
				var lvi = new ListViewItem();
				lvi.Text = Path.GetFileName(v.FileName);
				TimeSpan ts = new TimeSpan(0, 0, 0, 0, v.Length > 1 ? v.Length : 1);
				lvi.SubItems.Add(ts.Hours.ToString() + ":" + ts.Minutes.ToString("D2") + ":" + ts.Seconds.ToString("D2") + "." + ts.Milliseconds.ToString("D3"));
				//获得完整文件名
				if (!Regex.IsMatch(v.FileName, @"^\w:\\"))
					lvi.SubItems.Add(Path.Combine(Path.GetDirectoryName(filePath), v.FileName));
				else
					lvi.SubItems.Add(v.FileName);
				//设置Group
				lvi.Group = lsv.Groups["VideoGroup"];
				//设置图标
				lvi.ImageIndex = 0;
				//添加到ListView中
				lsv.Items.Add(lvi);
			}
			//设置弹幕
			foreach (string filename in config.Subtitles)
			{
				//填充listviewitem
				var lvi = new ListViewItem();
				//文件名
				lvi.Text = Path.GetFileName(filename);
				string file = filename;
				if (!File.Exists((file)))
					file = Path.Combine(Path.GetDirectoryName(filePath), file);
				//弹幕文件
				try
				{
					if (File.OpenText(file).ReadLine().StartsWith("[{")) //acfun
					{
						lvi.SubItems.Add("Acfun弹幕文件");
					}
					else if (File.OpenText(file).ReadToEnd().Contains(@"<chatserver>chat.bilibili.tv</chatserver>"))
					{
						lvi.SubItems.Add("Bilibili弹幕文件");
					}
					else if (File.OpenText(file).ReadToEnd().Contains(@"""status"":""OK"""))
					{
						lvi.SubItems.Add("豆泡弹幕文件");
					}
					else
					{
						lvi.SubItems.Add("未知格式弹幕文件");
					}
				}
				catch (Exception)
				{
					lvi.SubItems.Add("未知格式弹幕文件");
				}

				//获得完整文件名
				if (!Regex.IsMatch(file, @"^\w:\\"))
					lvi.SubItems.Add(Path.Combine(Path.GetDirectoryName(filePath), file));
				else
					lvi.SubItems.Add(file);
				//设置Group
				lvi.Group = lsv.Groups["SubtitleGroup"];
				//设置图标
				lvi.ImageIndex = 1;
				//添加到ListView中
				lsv.Items.Add(lvi);
			}
		}
		#endregion

		#region 文件操作
		/// <summary>
		/// 从文件中读取设置(不刷新用户界面)
		/// </summary>
		/// <param name="filePath"></param>
		private void LoadConfigFromFile(string filePath)
		{
			try
			{
				using (var fs = new FileStream(filePath, FileMode.Open))
				{
					XmlSerializer s = new XmlSerializer(typeof(AcPlayConfiguration));
					config = (AcPlayConfiguration)s.Deserialize(fs);
				}
			}
			catch
			{
				MessageBox.Show("配置文件读取失败", "AcPlay", MessageBoxButtons.OK, MessageBoxIcon.Error);
				throw;
			}
		}

		/// <summary>
		/// 保存当前的配置到文件
		/// </summary>
		/// <returns></returns>
		private string SaveConfigToFile(string filename)
		{
			string path = String.IsNullOrEmpty(filename) ? Path.GetTempFileName() : filename;

			//播放器设置
			switch (cboPlayer.SelectedIndex)
			{
				case 1:
					config.PlayerName = "bilibili";
					config.PlayerUrl = "http://static.hdslb.com/play.swf";
					break;
				default:
					config.PlayerName = "acfun";
					config.PlayerUrl = "http://static.acfun.tv/player/ACFlashPlayer.201209271950.swf";
					break;
			}

			//代理设置
			//取得[代理设置]
			if (cboProxy.SelectedIndex <= 0) //IE设置
				config.Proxy = null;
			else if (cboProxy.SelectedIndex == 1) //直接连接
				config.Proxy = new AcDownProxy().FromWebProxy(new WebProxy());
			else if (CoreManager.ConfigManager.Settings.Proxy_Settings != null)
			{
				foreach (AcDownProxy item in CoreManager.ConfigManager.Settings.Proxy_Settings)
				{
					if (item.Name == cboProxy.SelectedItem.ToString())
					{
						config.Proxy = item;
						break;
					}
				}
			}

			//限速
			config.SpeedLimit = int.Parse(udSpeedLimit.Value.ToString());

			List<Video> videos = new List<Video>();
			List<string> subtitles = new List<string>();
			//视频&弹幕
			foreach (ListViewItem lvi in lsv.Items)
			{
				//视频
				if (lvi.Group == lsv.Groups["VideoGroup"])
				{
					Video v = new Video();
					string[] time = lvi.SubItems[1].Text.Split(':', '.');
					TimeSpan ts = new TimeSpan(0, int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2]), int.Parse(time[3]));
					v.Length = (int)ts.TotalMilliseconds;
					v.FileName = lvi.SubItems[2].Text;
					videos.Add(v);
				}
				else //弹幕
				{
					subtitles.Add(lvi.SubItems[2].Text);
				}
			}
			//设置视频弹幕
			config.Videos = videos.ToArray();
			config.Subtitles = subtitles.ToArray();

			//额外设置
			if (config.ExtraConfig == null)
				config.ExtraConfig = new SerializableDictionary<string, string>();

			//保存配置到文件
			XmlSerializer s = new XmlSerializer(typeof(AcPlayConfiguration));
			using (var fs = new FileStream(path, FileMode.Create))
			{
				s.Serialize(fs, config);
			}

			return path;
		}

		#endregion

		#region 更新播放器缓存
		private void lnkPlayerCache_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			string player = "acfun";

			//建立文件夹
			string dir = Path.GetDirectoryName(Path.Combine(appdata, @"Kaedei" + Path.DirectorySeparatorChar + "AcPlay" + Path.DirectorySeparatorChar + "Cache" + Path.DirectorySeparatorChar));
			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);

			//更新播放器
			switch (cboPlayer.SelectedIndex)
			{
				case 0:
					player = "acfun";
					break;
				case 1:
					player = "bilibili";
					break;
			}

			lnkPlayerCache.Enabled = false;
			lnkPlayerCache.Text = "正在更新播放器缓存";
			//启动新线程
			Thread t = new Thread(new ThreadStart(new MethodInvoker(() =>
			{
				try
				{
					//播放器地址
					if (player == "acfun")
					{
						//播放器文件
						string swf = @"http://static.acfun.tv/player/ACFlashPlayer.201209271950.swf";
						string fileSwf = Path.Combine(dir, Path.GetFileNameWithoutExtension(swf) + ".swf");
						bool r1 = Network.DownloadFile(new DownloadParameter()
						{
							Url = swf,
							FilePath = fileSwf
						});

						//皮肤文件
						string skin = @"http://static.acfun.tv/skin/20120927.zip";
						string fileSkin = Path.Combine(dir, Path.GetFileName(skin));
						bool r2 = Network.DownloadFile(new DownloadParameter()
						{
							Url = skin,
							FilePath = fileSkin
						});

						//如果下载失败则删除文件
						if (!r1 || !r2)
						{
							File.Delete(fileSwf);
							File.Delete(fileSkin);
						}
					}
					else if (player == "bilibili")
					{
						string swf = "http://static.hdslb.com/play.swf";
						//播放器缓存位置
						string file = Path.Combine(dir, Path.GetFileNameWithoutExtension(swf) + ".swf");

						bool r = Network.DownloadFile(new DownloadParameter()
						{
							Url = swf,
							FilePath = file
						});

						//如果下载失败则删除文件
						if (!r)
						{
							File.Delete(file);
						}
					}
				}
				catch { }
				this.Invoke(new MethodInvoker(() =>
				{
					lnkPlayerCache.Enabled = true;
					lnkPlayerCache.Text = "(离线播放)更新此播放器缓存";
				}));
			})));
			t.IsBackground = true;
			t.Start();
		}
		#endregion

		#region 双击更改视频属性
		private void lsv_DoubleClick(object sender, EventArgs e)
		{
			if (lsv.SelectedIndices.Count > 0)
			{
				int index = lsv.SelectedIndices[0];
				ListViewItem lvi = lsv.Items[index];
				if (lvi.Group == lsv.Groups["VideoGroup"])
				{
					//显示设置窗口
					Video v = new Video();
					string[] time = lsv.Items[index].SubItems[1].Text.Split(':', '.');
					TimeSpan ts = new TimeSpan(0, int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2]), int.Parse(time[3]));
					v.Length = (int)ts.TotalMilliseconds;
					v.FileName = lsv.Items[index].SubItems[2].Text;
					AcPlayItem form = new AcPlayItem(v);
					form.ShowDialog();
					//重新设置ListViewItem
					lsv.Items[index].SubItems[0].Text = Path.GetFileName(v.FileName);
					ts = new TimeSpan(0, 0, 0, 0, v.Length);
					lsv.Items[index].SubItems[1].Text = ts.Hours.ToString() + ":" + ts.Minutes.ToString("D2") + ":" + ts.Seconds.ToString("D2") + "." + ts.Milliseconds.ToString("D3");
					lsv.Items[index].SubItems[2].Text = v.FileName;
				}
			}
		}
		#endregion

		#region 添加视频/弹幕
		private void btnAdd_Click(object sender, EventArgs e)
		{
			//选择文件
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "视频/弹幕文件(*.flv;*.mp4;*.hlv;*.f4v;*.xml;*.json)|*.flv;*.mp4;*.hlv;*.f4v;*.xml;*.json|视频文件(*.flv;*.mp4;*.hlv;*.f4v)|*.flv;*.mp4;*.hlv;*.f4v|弹幕文件(*.xml;*.json)|*.xml;*.json";
			ofd.Title = "请选择视频/弹幕文件";
			ofd.Multiselect = true;
			ofd.InitialDirectory = lastSelectDirectory;
			if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
			{
				lastSelectDirectory = Path.GetDirectoryName(ofd.FileNames[0]);
				foreach (string file in ofd.FileNames)
				{
					AddFile(file);
				}
			}
		}

		private void AddFile(string file)
		{
			var lvi = new ListViewItem();
			file = file.ToUpper();
			//判断文件格式
			if (file.EndsWith(".XML") || file.EndsWith(".JSON")) //如果是弹幕文件
			{
				//文件名
				lvi.Text = Path.GetFileName(file);
				//弹幕文件
				try
				{
					if (File.OpenText(file).ReadLine().StartsWith("[{")) //acfun
					{
						lvi.SubItems.Add("Acfun弹幕文件");
					}
					else if (File.OpenText(file).ReadToEnd().Contains(@"<chatserver>chat.bilibili.tv</chatserver>"))
					{
						lvi.SubItems.Add("Bilibili弹幕文件");
					}
					else if (File.OpenText(file).ReadToEnd().Contains(@"""status"":""OK"""))
					{
						lvi.SubItems.Add("豆泡弹幕文件");
					}
					else
					{
						lvi.SubItems.Add("未知格式弹幕文件");
					}
				}
				catch (Exception)
				{
					lvi.SubItems.Add("未知格式弹幕文件");
				}
				lvi.SubItems.Add(file);
				//设置Group
				lvi.Group = lsv.Groups["SubtitleGroup"];
				//设置图标
				lvi.ImageIndex = 1;
			}
			else if (file.EndsWith(".FLV") || file.EndsWith(".MP4") || file.EndsWith(".HLV") || file.EndsWith(".F4V")) //如果是视频文件
			{
				Video v = new Video();
				v.FileName = file;
				//判断文件长度
				//v.Length = 100;
				v.Length = 0;
				AcPlayItem form = new AcPlayItem(v);
				form.ShowDialog();
				//设置文件名和长度
				lvi.Text = Path.GetFileName(v.FileName);
				TimeSpan ts = new TimeSpan(0, 0, 0, 0, v.Length);
				lvi.SubItems.Add(ts.Hours.ToString() + ":" + ts.Minutes.ToString("D2") + ":" + ts.Seconds.ToString("D2") + "." + ts.Milliseconds.ToString("D3"));
				lvi.SubItems.Add(v.FileName);
				//设置Group
				lvi.Group = lsv.Groups["VideoGroup"];
				//设置图标
				lvi.ImageIndex = 0;
			}
			else
			{
				return;
			}

			//添加listviewitem
			lsv.Items.Add(lvi);
		}
		#endregion

		#region 删除
		private void btnDel_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in lsv.SelectedItems)
			{
				lsv.Items.Remove(item);
			}
		}
		#endregion

		#region 清空
		private void btnClear_Click(object sender, EventArgs e)
		{
			lsv.Items.Clear();
		}
		#endregion

		#region 播放


		private void btnStart_Click(object sender, EventArgs e)
		{
			//验证文件
			if (lsv.Items.Count == 0) return;
			//保存配置文件
			string acplayFile = SaveConfigToFile("");
			//播放文件
			Play(acplayFile);
		}

		private void Play(string path)
		{
			//禁用面板
			this.Enabled = false;

			//如果AcPlay.exe文件不存在
			string exeFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Kaedei" + Path.DirectorySeparatorChar + "AcPlay" + Path.DirectorySeparatorChar + "acplay.exe");
			if (!File.Exists(exeFile))
			{
				var helper = new AcPlayHelper();
				helper.ReleaseAcPlayFile();
			}

			//调用外部程序
			Process p = new Process();
			p.StartInfo = new ProcessStartInfo()
			{
				FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Kaedei" + Path.DirectorySeparatorChar + "AcPlay" + Path.DirectorySeparatorChar + "acplay.exe"),
				Arguments = "\"" + path + "\"",
				Verb = "runas"
			};

			//调试模式
			if (chkDebug.Checked)
				p.StartInfo.Arguments += " /debug";

			//WinXP不使用Verb
			if (!DwmApi.IsWindowsVistaOrHigher())
				p.StartInfo.Verb = "";

			new Thread(new ThreadStart(() =>
			{
				try
				{
					p.Start();
					p.WaitForExit();
				}
				catch { }
				finally
				{
					this.Invoke(new MethodInvoker(() => { this.Enabled = true; }));
				}
			})).Start();


		}

		#endregion

		#region 导入/导出
		private void lnkImport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			//选择文件
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "AcPlay播放配置(*.acplay)|*.acplay";
			ofd.Title = "导入AcPlay播放配置";
			ofd.Multiselect = false;
			ofd.InitialDirectory = lastSelectDirectory;
			if (ofd.ShowDialog() != DialogResult.Cancel)
			{
				lastSelectDirectory = Path.GetDirectoryName(ofd.FileName);
				//导入配置
				FillFromConfig(ofd.FileName);
			}
		}

		private void lnkExport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			//选择文件
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "AcPlay播放配置(*.acplay)|*.acplay";
			sfd.Title = "导出AcPlay播放配置";
			sfd.AddExtension = true;
			sfd.InitialDirectory = lastSelectDirectory;
			if (sfd.ShowDialog() != DialogResult.Cancel)
			{
				lastSelectDirectory = Path.GetDirectoryName(sfd.FileName);
				//导出配置
				SaveConfigToFile(sfd.FileName);
			}
		}
		#endregion

		#region 拖拽添加文件
		private void lsv_DragDrop(object sender, DragEventArgs e)
		{
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			foreach (string file in files)
			{
				if (file.EndsWith(".acplay", StringComparison.CurrentCultureIgnoreCase))
					FillFromConfig(file);
				else
					AddFile(file);
			}

		}


		private void lsv_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}
		#endregion

		#region 链接到帮助页面

		private void lnkAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				Process.Start("http://blog.sina.com.cn/s/blog_58c506600101b4xd.html");
			}
			catch { }
		}
		#endregion

		private void chkOptions_CheckedChanged(object sender, EventArgs e)
		{
			splitContainer1.Panel2Collapsed = !chkOptions.Checked;
			if (chkOptions.Checked)
			{
				udSpeedLimit.Value = 0;
				cboProxy.Items.Clear();
				if (!AcDown.Interface.Tools.IsRunningOnMono)
					cboProxy.Items.Add("使用IE设置");
				else
					cboProxy.Items.Add("使用系统设置");
				cboProxy.Items.Add("直接连接");
				if (CoreManager.ConfigManager.Settings.Proxy_Settings != null)
				{
					foreach (AcDownProxy item in CoreManager.ConfigManager.Settings.Proxy_Settings)
					{
						cboProxy.Items.Add(item.Name);
					}
				}
				cboProxy.SelectedIndex = 0;

			}
		}

		private void lnkDandanplay_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				Process.Start("http://www.dandanplay.com/?from=acdown");
			}
			catch { }
		}


		



	}
}
