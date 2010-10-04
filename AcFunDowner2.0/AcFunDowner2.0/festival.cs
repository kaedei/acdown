/*festival.cs
 * 
 * class festival:
 * 用来返回当前日期对应的节日
 * 
 * 最后更新日期：2010-1-9
 *
 * Copyright 2010 Kaedei Software

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 * 
 * http://blog.sina.com.cn/kaedei
 * mailto:kaedei@foxmail.com
 *
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Kaedei.AcFunDowner
{
	public static class festival
	{
		public static string GetString()
		{
			//元旦(1.1~1.3)
			if (IfIs(1, 1) || IfIs(1, 2) || IfIs(1, 3))
				return DateTime.Now.Year.ToString() + "新年快乐!";
			//2010新年(2.13)
			if (IfIs(2010,2,13))
				return "2010除夕快乐";
			//2010新年+情人节
			if (IfIs(2010,2,14))
				return "2010新年快乐&情人节快乐";
			//情人节
			if (IfIs(2010,2,14))
				return "情人节快乐";
			//植树节
			if (IfIs(3, 12))
				return "今天是植树节哦~";
			//愚人节
			if (IfIs(4, 1))
				return "其实，你最帅了";
			//地球日
			if (IfIs(4, 22))
				return "世界地球日，我们一起来保护环境";
			//劳动节
			if (IfIs(5, 1))
				return "劳动人民最高尚";
			//儿童节
			if (IfIs(6, 1))
				return "永远的节日，永远的快乐！";
			//爱眼日
			if (IfIs(6, 6))
				return "世界爱眼日，请爱护你的眼睛";


			//万圣节
			if (IfIs(10, 31) || IfIs(11,1))
				return "Trick Or Treat~";
			//圣诞节
			if (IfIs(12,24) || IfIs(12, 25))
				return "Merry Christmas~";

			//2012
			if (IfIs(2012, 12, 22))
				return "…………";
			if (IfIs(12, 22) || IfIs(12, 23))
				return "2012还会远吗？";
			

			return "";
		}

		private static  bool IfIs(int year,int month,int day)
		{
			DateTime d = DateTime.Now;
			if (d.Year == year && d.Month == month && d.Day == day)
				return true;
			else
				return false;
		}

		private static bool IfIs(int month, int day)
		{
			return IfIs(DateTime.Now.Year, month, day);
		}

	}
}
