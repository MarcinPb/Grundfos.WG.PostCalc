using System;
using System.Xml;
using Svg;

namespace Grundfos.SVG
{
    public class TwSvgDocument : SvgDocument
    {
        protected override void WriteStartElement(XmlTextWriter writer)
        {
            if (this.ElementName != String.Empty)
            {
                writer.WriteStartElement(this.ElementName);
            }

            this.WriteAttributes(writer);

            foreach (var ns in SvgAttributeAttribute.Namespaces)
            {
                if (ns.Key == "xml")
                {
                    continue;
                }

                if (string.IsNullOrEmpty(ns.Key))
                {
                    writer.WriteAttributeString("xmlns", ns.Value);
                }
                else
                {
                    writer.WriteAttributeString("xmlns:" + ns.Key, ns.Value);
                }
            }

            writer.WriteAttributeString("version", "1.1");
        }
    }
}
