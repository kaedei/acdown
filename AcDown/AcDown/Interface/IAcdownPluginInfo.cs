using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;

namespace Kaedei.AcDown.Interface
{
	public interface IAcdownPluginInfo
	{
		string Name{get;}
		string Author { get; }
		Version Version { get;}
		string Describe { get;}
		string SupportUrl { get;}
		IDownloader CreateDownloader();
		bool CheckUrl(string url);
		string GetHash(string url);
		//string[] GetUrlExample();
	}
}
