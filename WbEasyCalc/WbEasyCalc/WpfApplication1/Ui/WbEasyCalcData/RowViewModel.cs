using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalRepository;

namespace WpfApplication1.Ui.WbEasyCalcData
{
    public class RowViewModel
    {
        public DataModel.WbEasyCalcData WbEasyCalcData { get; }
        public string YearName => GlobalConfig.DataRepository.YearList.FirstOrDefault(x => x.Id == WbEasyCalcData.YearNo)?.Name;
        public string MonthName => GlobalConfig.DataRepository.MonthList.FirstOrDefault(x => x.Id == WbEasyCalcData.MonthNo)?.Name;
        public string ZoneName => GlobalConfig.DataRepository.ZoneList.FirstOrDefault(x => x.ZoneId == WbEasyCalcData.ZoneId)?.ZoneName;

        public RowViewModel(DataModel.WbEasyCalcData wbEasyCalcData)
        {
            WbEasyCalcData = wbEasyCalcData;
        }
    }
}
