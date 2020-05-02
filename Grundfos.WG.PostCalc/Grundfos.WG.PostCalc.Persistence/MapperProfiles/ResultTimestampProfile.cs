using AutoMapper;

namespace Grundfos.WG.PostCalc.Persistence.MapperProfiles
{
    public class ResultTimestampProfile : Profile
    {
        public ResultTimestampProfile()
        {
            this.CreateMap<Model.ResultTimestamp, SQLiteEf.Model.ResultTimestamp>()
                .ReverseMap();
        }
    }
}
