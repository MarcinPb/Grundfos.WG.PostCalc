using System;
using WbEasyCalc.Model;

namespace WbEasyCalc
{
    public interface IEasyCalcDataReader
    {
        EasyCalcSheetData ReadSheetData(string zone, DateTime yearMonth);
        void PublishSheetData(EasyCalcSheetData data, string zoneName);
    }
}