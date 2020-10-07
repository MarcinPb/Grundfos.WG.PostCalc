using System;
using Grundfos.WB.EasyCalc.Calculations.Model;

namespace Grundfos.WB.EasyCalc.Calculations
{
    public interface IEasyCalcDataReader
    {
        EasyCalcSheetData ReadSheetData(string zone, DateTime yearMonth);
        void PublishSheetData(EasyCalcSheetData data, string zoneName);
    }
}