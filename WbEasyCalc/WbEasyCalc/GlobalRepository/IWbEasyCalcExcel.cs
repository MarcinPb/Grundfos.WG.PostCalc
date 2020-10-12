using WbEasyCalc;

namespace GlobalRepository
{
    public interface IWbEasyCalcExcel
    {
        void SaveToExcelFile(string excelFileName, EasyCalcDataInput easyCalcDataInput);
        EasyCalcDataInput LoadFromExcelFile(string excelFileName);
    }
}