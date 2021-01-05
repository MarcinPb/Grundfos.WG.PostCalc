using AutoMapper;
using Grundfos.TW.DataSourceMap.Wg2Opc;
using NPOI.SS.UserModel;

namespace Grundfos.TW.DataSourceMap.MapperProfiles
{
    public class Wg2OpcXlsProfile : Profile
    {
        public Wg2OpcXlsProfile()
        {
            // TODO: make this configurable, e.g., by specifying the field/column names in app.config.
            this.CreateMap<IRow, Wg2OpcMapEntry>()
                .ForMember(x => x.ElementID, opt => opt.MapFrom(src => GetInt(src.GetCell(0))))
                .ForMember(x => x.ElementLabel, opt => opt.MapFrom(src => src.GetCell(1)))
                .ForMember(x => x.OpcTag, opt => opt.MapFrom(src => src.GetCell(3)))
                .ForMember(x => x.ResultAttributeID, opt => opt.MapFrom(src => GetInt(src.GetCell(4))));
        }

        private static int GetInt(ICell cell)
        {
            if (cell.CellType == CellType.Numeric)
            {
                return (int)cell.NumericCellValue;
            }

            if (cell.StringCellValue.Equals("NULL", System.StringComparison.OrdinalIgnoreCase))
            {
                return 0;
            }

            return int.Parse(cell.StringCellValue);
        }
    }
}
