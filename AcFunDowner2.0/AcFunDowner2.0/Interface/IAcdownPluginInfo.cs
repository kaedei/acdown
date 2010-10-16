using System;
using System.Collections.Generic;
using System.Text;

namespace Kaedei.AcFunDowner.Interface
{
	interface IAcdownPluginInfo
	{
		public string Name{get;set;}
		public string Author { get; set; }
		public Version Version { get; set; }
		public string Describe { get; set; }
	}
}
