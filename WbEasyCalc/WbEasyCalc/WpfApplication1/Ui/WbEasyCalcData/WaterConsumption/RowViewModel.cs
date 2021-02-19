﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalRepository;

namespace WpfApplication1.Ui.WbEasyCalcData.WaterConsumption
{
    public class RowViewModel
    {
        public DataModel.WaterConsumption Model { get; }
        public string WaterConsumptionCategoryName => GlobalConfig.DataRepository.WaterConsumptionCategoryList.FirstOrDefault(x => x.Id == Model.WaterConsumptionCategoryId)?.Name;
        public string WaterConsumptionStatusName => GlobalConfig.DataRepository.WaterConsumptionStatusList.FirstOrDefault(x => x.Id == Model.WaterConsumptionStatusId)?.Name;
        public string ZoneName => GlobalConfig.DataRepository.ZoneList.FirstOrDefault(x => x.ZoneId == Model.ZoneId)?.ZoneName;

        public RowViewModel(DataModel.WaterConsumption model)
        {
            Model = model;
        }
    }
}
