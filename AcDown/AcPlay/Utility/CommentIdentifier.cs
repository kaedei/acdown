using System.IO;
using System.Text;

namespace Kaedei.AcPlay.Utility
{
	public class CommentIdentifier
	{
		public string Identify(string filepath, Encoding fileEncoding)
		{
			return Identify(File.ReadAllText(filepath, fileEncoding));
		}

		public string Identify(string content)
		{
			if (content.StartsWith("[{"))
				return "acfun";
			if (content.Contains("chat.bilibili.com"))
				return "bilibili";
			if (content.Contains(@"""status"":""OK"""))
				return "doupao";
			if (content.Contains("<d p="))
				return "bilibili";
			else
				return "unknown";
		}
	}
}
