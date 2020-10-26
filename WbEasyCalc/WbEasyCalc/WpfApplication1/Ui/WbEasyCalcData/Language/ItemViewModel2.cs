using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WbEasyCalcModel;
using WpfApplication1.Utility;

namespace WpfApplication1.Ui.WbEasyCalcData.Language
{
    public class ItemViewModel2 : ViewModelBase
    {
        public int Order { get; set; }

        public int Id { get; set; }

        public string Desc { get; set; }

        private string _us;
        public string Us
        {
            get { return _us; }
            set { _us = value; RaisePropertyChanged(); }
        }

        private string _pl;

        public string Pl
        {
            get { return _pl; }
            set { _pl = value; RaisePropertyChanged(); }
        }


        private string _result;
        public string Result
        {
            get
            {
                return _result;
            }
            set { _result = value; RaisePropertyChanged(); }
        }

        public ItemViewModel2(LanguageItem model)
        {
            Id = model.Id;
            Us = model.Us;
            Pl = model.Pl;
        }

    }
}
