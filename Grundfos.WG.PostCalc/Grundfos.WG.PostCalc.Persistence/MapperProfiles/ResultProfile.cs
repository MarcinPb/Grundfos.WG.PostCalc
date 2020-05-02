using AutoMapper;

namespace Grundfos.WG.PostCalc.Persistence.MapperProfiles
{
    public class ResultProfile : Profile
    {
        public ResultProfile()
        {
            this.CreateMap<Model.Result, SQLiteEf.Model.Result>()
                .ReverseMap();
        }
    }
}
