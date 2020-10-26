using WbEasyCalc;

namespace WbEasyCalcRepository
{
    public interface IWbEasyCalc
    {
        EasyCalcDataOutput Calculate(EasyCalcDataInput easyCalcDataInput);
    }
}
