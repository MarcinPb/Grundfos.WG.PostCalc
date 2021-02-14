using WbEasyCalcModel;

namespace GlobalRepository
{
    public interface IWbEasyCalcExcel
    {
        void SaveToExcelFile(string excelFileName, EasyCalcModel easyCalcModel);
        EasyCalcModel LoadFromExcelFile(string excelFileName);
    }
}