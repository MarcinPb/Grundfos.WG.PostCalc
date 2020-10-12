using WbEasyCalc;

namespace GlobalRepository
{
    public interface IOpcServer
    {
        void RunOpcPublish(string zoneRomanNo, EasyCalcDataInput easyCalcDataInput, EasyCalcDataOutput easyCalcDataOutput);
    }
}