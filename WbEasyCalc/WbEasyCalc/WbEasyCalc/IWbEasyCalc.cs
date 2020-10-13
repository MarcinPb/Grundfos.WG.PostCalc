using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WbEasyCalc
{
    public interface IWbEasyCalc
    {
        EasyCalcDataOutput Calculate(EasyCalcDataInput easyCalcDataInput);
    }
}
