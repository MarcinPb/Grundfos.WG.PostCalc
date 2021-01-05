using AutoMapper;
using Grundfos.TW.XML;
using Grundfos.WG2TW.XML.ConsoleApp.Configuration;

namespace Grundfos.WG2TW.XML.ConsoleApp.Mappings
{
    public class XmlProcessorConfigurationMapping : Profile
    {
        public XmlProcessorConfigurationMapping()
        {
            this.CreateMap<XmlProcessorConfiguration, SheetXmlProcessorConfiguration>();
            this.CreateMap<Target, TargetConfiguration>();
        }
    }
}
