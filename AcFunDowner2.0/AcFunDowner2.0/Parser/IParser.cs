using System;
using System.Collections.Generic;
using System.Text;

namespace Kaedei.AcDown.Parser
{
	public interface IParser
	{
		string[] Parse(string[] parameters);
	}
}
