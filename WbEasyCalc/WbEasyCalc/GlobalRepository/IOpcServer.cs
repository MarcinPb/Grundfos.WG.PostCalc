using Grundfos.WB.EasyCalc.Calculations;

namespace GlobalRepository
{
    public interface IOpcServer
    {
        void RunOpcPublish(string zoneRomanNo, EasyCalcDataInput easyCalcDataInput, EasyCalcDataOutput easyCalcDataOutput);
    }
}