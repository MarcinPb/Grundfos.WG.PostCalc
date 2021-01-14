using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalRepository;

namespace WpfApplication1.Ui.WaterConsumption
{
    public class RowViewModel
    {
        public DataModel.WaterConsumption Model { get; }
        public string YearName => GlobalConfig.DataRepository.YearList.FirstOrDefault(x => x.Id == Model.YearNo)?.Name;
        public string MonthName => GlobalConfig.DataRepository.MonthList.FirstOrDefault(x => x.Id == Model.MonthNo)?.Name;
        public string ZoneName => GlobalConfig.DataRepository.ZoneList.FirstOrDefault(x => x.ZoneId == Model.ZoneId)?.ZoneName;

        public RowViewModel(DataModel.WaterConsumption model)
        {
            Model = model;
        }
    }
}
