﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Kaedei.AcDown.Parser
{
	public interface IParser
	{
		string[] Parse(string[] parameters, WebProxy proxy);
	}
}
