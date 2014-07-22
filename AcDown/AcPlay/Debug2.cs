using System;

namespace Kaedei.AcPlay.Diagnostics
{
	public static class Debug2
	{
		public static void WriteLine(string text)
		{
			if (Program.DebugWindow != null)
			{
				Program.DebugWindow.WriteLine(text);
			}
			else
			{
				Console.WriteLine(text);
			}
		}
	}
}
