using System;
using System.Collections.Generic;

namespace Kaedei.AcPlay
{
	public class StandardItem
	{
		public String player { get; set; }
		public Int64 id { get; set; }
		public Double time { get; set; }
		public Int64 mode { get; set; }
		public Int64 color { get; set; }
		public Int64 size { get; set; }
		public Double stamp { get; set; }
		public Int64 type { get; set; }
		public String uid { get; set; }
		public String message { get; set; }
		public Dictionary<String, String> items = new Dictionary<string, string>();
	}
}
