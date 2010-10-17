using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;

namespace Kaedei.AcDown.Interface
{
	interface IAcdownPluginInfo
	{
		string Name{get;set;}
		string Author { get; set; }
		Version Version { get; set; }
		string Describe { get; set; }
		string SupportUrl { get; set; }
		IDownloader CreateDownloader(); 
	}
}
