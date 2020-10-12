using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using DataRepository.WbEasyCalcData;

namespace DataRepository
{
    public interface IDataRepository
    {
        IWbEasyCalcDataListRepository WbEasyCalcDataListRepository { get; }

        List<IdNamePair> YearList { get; }
        List<IdNamePair> MonthList { get; }
        List<ZoneItem> ZoneList { get; }

        GisModelScadaData GetGisModelScadaData(int yearNo, int monthNo, int zoneId);
    }
}
