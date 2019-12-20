using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Dracula.Core
{
	public static class SeriDeseriXmlUtil
	{
		public static T XmlToObject<T>(string _xml)
		{
			XmlDocument xml = new XmlDocument();

			xml.LoadXml(_xml);
			var t = Type.GetType(xml.DocumentElement.Name);
			XmlSerializer serializer = new XmlSerializer(t);
			MemoryStream memStream = new MemoryStream(Encoding.BigEndianUnicode.GetBytes(xml.InnerXml));
			T resultingMessage = (T)serializer.Deserialize(memStream);
			return resultingMessage;
		}

		public static string ObjectToXml<T>(T obj)
			where T : new()
		{
			XmlSerializer xsSubmit = new XmlSerializer(typeof(T));

			var xml = "";

			using (var sww = new StringWriter())
			{
				using (XmlWriter writer = XmlWriter.Create(sww))
				{
					xsSubmit.Serialize(writer, obj);
					xml = sww.ToString(); // Your XML
				}
			}

			return xml;
		}
	}
}
