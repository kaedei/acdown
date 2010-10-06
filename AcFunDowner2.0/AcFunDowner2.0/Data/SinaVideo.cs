/*
 SinaVideo.cs
 * 
 * SinaVideoLoader:
 * 用来反序列化新浪视频XML描述的类
 * 
 * video:
 * 序列化/反序列化的目标类
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
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Net;
using Kaedei.AcDown;
using Kaedei.AcDown.Interface;

namespace Kaedei.AcFunDowner
{
	 public class SinaVideoLoader
	 {
		  /// <summary>
		  /// 从文件中反序列化对象
		  /// </summary>
		  /// <param name="strFile"></param>
		  /// <returns></returns>
		  public SinaVideo LoadVideo(string strFile)
			{
				SinaVideo v;
				using (FileStream fs = new FileStream(strFile, FileMode.Open))
				{
					 v=LoadVideo(fs);
				}
				return v;
		  }
		  /// <summary>
		  /// 从流中反序列化对象
		  /// </summary>
		  /// <param name="stream"></param>
		  /// <returns></returns>
		  public SinaVideo LoadVideo(Stream stream)
		  {
				SinaVideo v;
				XmlSerializer s = new XmlSerializer(typeof(SinaVideo));
				v = (SinaVideo)s.Deserialize(stream);
				return v;
		  }

		  /// <summary>
		  /// 从网络流中反序列化对象
		  /// </summary>
		  /// <param name="url">XML文件地址</param>
		  /// <param name="encode">XML文件所用的字符编码</param>
		  /// <returns></returns>
		  public SinaVideo LoadVideo(string url,Encoding encode)
		  {
			  SinaVideo v;
			  var req = WebRequest.Create(url);
			  var res = req.GetResponse();
			  using (StreamReader strm = new StreamReader(res.GetResponseStream(), encode))
			  {
				  XmlSerializer s = new XmlSerializer(typeof(SinaVideo));
				  v = (SinaVideo)s.Deserialize(strm);
			  }
			  return v;

		  }

	 }

	  [Serializable]
	  public class SinaVideo: Video
	  {
		  public string result = "";
		  public Int32 timelength = 0;
		  public Int32 framecount = 0;
		  public string vname = "";
		  public string src = "";
		  [XmlElement()]
		  public Part[] durl;
		  public string vtags = "";
		  public string ad = "";
		  public string vstr = "";
		  public string vround = "";
		  public Int32 type = 0;
		  public string message = "";
	  }

	  
}
