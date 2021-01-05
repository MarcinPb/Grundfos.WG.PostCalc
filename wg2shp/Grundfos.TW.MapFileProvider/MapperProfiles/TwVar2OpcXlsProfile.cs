using AutoMapper;
using Grundfos.TW.DataSourceMap.Tw2Opc;
using NPOI.SS.UserModel;

namespace Grundfos.TW.DataSourceMap.MapperProfiles
{
    public class TwVar2OpcXlsProfile : Profile
    {
        public TwVar2OpcXlsProfile()
        {
            var extractor = new TwOpcTagExtractor();
            // TODO: make this configurable, e.g., by specifying the field/column names in app.config.
            this.CreateMap<IRow, TwVar2OpcMapEntry>()
                .ForMember(x => x.VariableName, opt => opt.MapFrom(src => src.GetCell(3)))
                .ForMember(x => x.TwOpcTag, opt => opt.MapFrom(src => src.GetCell(10)))
                .ForMember(x => x.OpcTag, opt => opt.MapFrom((src, dest, destProp, ctx) =>
                {
                    var rawValue = src.GetCell(10).StringCellValue;
                    extractor.TryExtract(rawValue, out string opcTag);
                    return opcTag;
                }));
        }
    }
}
