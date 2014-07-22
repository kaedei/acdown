using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Kaedei.AcDown.Core;
using System.Diagnostics;
using Kaedei.AcDown.Interface;

namespace Kaedei.AcDown.UI
{

	internal static class DwmApi
	{
		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmEnableBlurBehindWindow(
			 IntPtr hWnd, DWM_BLURBEHIND pBlurBehind);

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmExtendFrameIntoClientArea(
			 IntPtr hWnd, MARGINS pMargins);

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern bool DwmIsCompositionEnabled();

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmEnableComposition(bool bEnable);

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmGetColorizationColor(
			 out int pcrColorization,
			 [MarshalAs(UnmanagedType.Bool)]out bool pfOpaqueBlend);

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern IntPtr DwmRegisterThumbnail(
			 IntPtr dest, IntPtr source);

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmUnregisterThumbnail(IntPtr hThumbnail);

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmUpdateThumbnailProperties(
			 IntPtr hThumbnail, DWM_THUMBNAIL_PROPERTIES props);

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmQueryThumbnailSourceSize(
			 IntPtr hThumbnail, out Size size);

		[StructLayout(LayoutKind.Sequential)]
		public class DWM_THUMBNAIL_PROPERTIES
		{
			public uint dwFlags;
			public RECT rcDestination;
			public RECT rcSource;
			public byte opacity;
			[MarshalAs(UnmanagedType.Bool)]
			public bool fVisible;
			[MarshalAs(UnmanagedType.Bool)]
			public bool fSourceClientAreaOnly;
			public const uint DWM_TNP_RECTDESTINATION = 0x00000001;
			public const uint DWM_TNP_RECTSOURCE = 0x00000002;
			public const uint DWM_TNP_OPACITY = 0x00000004;
			public const uint DWM_TNP_VISIBLE = 0x00000008;
			public const uint DWM_TNP_SOURCECLIENTAREAONLY = 0x00000010;
		}

		[StructLayout(LayoutKind.Sequential)]
		public class MARGINS
		{
			public int cxLeftWidth, cxRightWidth,
						  cyTopHeight, cyBottomHeight;

			public MARGINS(int left, int top, int right, int bottom)
			{
				cxLeftWidth = left; cyTopHeight = top;
				cxRightWidth = right; cyBottomHeight = bottom;
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public class DWM_BLURBEHIND
		{
			public uint dwFlags;
			[MarshalAs(UnmanagedType.Bool)]
			public bool fEnable;
			public IntPtr hRegionBlur;
			[MarshalAs(UnmanagedType.Bool)]
			public bool fTransitionOnMaximized;

			public const uint DWM_BB_ENABLE = 0x00000001;
			public const uint DWM_BB_BLURREGION = 0x00000002;
			public const uint DWM_BB_TRANSITIONONMAXIMIZED = 0x00000004;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int left, top, right, bottom;

			public RECT(int left, int top, int right, int bottom)
			{
				this.left = left; this.top = top;
				this.right = right; this.bottom = bottom;
			}
		}

		[DllImport("shell32.dll", EntryPoint = "#680", CharSet = CharSet.Unicode)]
		static extern bool IsUserAnAdmin();
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern IntPtr SendMessage(HandleRef hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
		const int BCM_SETSHIELD = 0x0000160C;

		const int LVM_FIRST = 0x1000;
		const int LVM_SETEXTENDEDLISTVIEWSTYLE = LVM_FIRST + 54;
		const int LVS_EX_FULLROWSELECT = 0x00000020;
		const int LVS_EX_DOUBLEBUFFER = 0x00010000;
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
		[DllImport("uxtheme", CharSet = CharSet.Unicode)]
		extern static Int32 SetWindowTheme(IntPtr hWnd, String textSubAppName, String textSubIdList);

		[DllImport("shell32.dll")]
		static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

		/// <summary>
		/// 判断当前系统是否为Windows 7或更高版本的操作系统
		/// </summary>
		/// <returns></returns>
		[DebuggerNonUserCode()]
		public static bool IsWindows7OrHigher()
		{
			if (Environment.OSVersion.Version.Major >= 6 && Environment.OSVersion.Version.Minor >= 1)
				return true;
			else
				return false;
		}

		/// <summary>
		/// 判断当前系统是否为Windows Vista
		/// </summary>
		/// <returns></returns>
		[DebuggerNonUserCode()]
		public static bool IsWindowsVista()
		{
			if (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 0)
				return true;
			else
				return false;
		}

		/// <summary>
		/// 判断当前系统是否为Windows Vista或更高版本的操作系统
		/// </summary>
		/// <returns></returns>
		[DebuggerNonUserCode()]
		public static bool IsWindowsVistaOrHigher()
		{
			if (Tools.IsRunningOnMono)
				return false;
			if (Environment.OSVersion.Version.Major >= 6)
				return true;
			else
				return false;
		}

		/// <summary>
		/// 设置ListView控件的特殊效果（仅Vista或更高版本的Windows系统）
		/// </summary>
		/// <param name="lsv"></param>
		public static void SetListViewVisualEffect(ListView lsv)
		{
			//设置vista效果
			if (IsWindowsVistaOrHigher())
			{
				SetWindowTheme(lsv.Handle, "explorer", null);
				SendMessage(lsv.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, 0, LVS_EX_FULLROWSELECT + LVS_EX_DOUBLEBUFFER);  //Blue selection
			}
		}

		/// <summary>
		/// 判断当前用户是否为管理员
		/// </summary>
		/// <returns></returns>
		public static bool IsAdmin()
		{
			return IsUserAnAdmin();
		}

		/// <summary>
		/// 为按钮设置UAC盾牌图标
		/// </summary>
		/// <param name="btn"></param>
		public static void SetShieldIcon(Button btn)
		{
			//设置vista效果
			if (IsWindowsVistaOrHigher())
			{
				btn.FlatStyle = FlatStyle.System;
				SendMessage(new HandleRef(btn, btn.Handle), BCM_SETSHIELD, IntPtr.Zero, IsUserAnAdmin() ? new IntPtr(0) : new IntPtr(1));
			}
		}

		/// <summary>
		/// 刷新桌面
		/// </summary>
		public static void RefreshNotify()
		{
			SHChangeNotify(0x8000000, 0, IntPtr.Zero, IntPtr.Zero);
		}
	}
}
