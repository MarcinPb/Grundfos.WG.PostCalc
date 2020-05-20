using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Grundfos.WG.PostCalc
{
    public class Helper
    {
        public static void DumpToFile(object inputObject, string fileName)
        {
            using (var file = new StreamWriter(fileName))
            {
                file.Write(ObjectToXml( inputObject));
            }
        }
        //public static void DumpToFile(string text, string fileName)
        //{
        //    using (var file = new StreamWriter(fileName))
        //    {
        //        file.Write(text);
        //    }
        //}

        public static string ObjectToXml(object inputObject)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            string objectAsXmlString;

            XmlSerializer xs = new XmlSerializer(inputObject.GetType());
            using (StringWriter sw = new StringWriter())
            {
                try
                {
                    xs.Serialize(sw, inputObject, ns);
                    objectAsXmlString = sw.ToString();
                }
                catch (Exception ex)
                {
                    objectAsXmlString = ex.ToString();
                }
            }

            return objectAsXmlString;
        }
    }
}
