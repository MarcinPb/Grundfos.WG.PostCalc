using Database.DataRepository;
using GeometryModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.Utility;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace WpfApplication1.Ui.Designer
{
    public class ItemViewModel : ViewModelBase
    {

        private int _id;
        [Category("General")]
        [DisplayName("ID")]
        public int Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged("Id"); }
        }

        private string _name;
        [Category("General")]
        [DisplayName("Label")]
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("Name"); }
        }


        protected DomainObjectData _model;
        public ItemViewModel(int id)
        {
            _model = MainRepo.GetItem(id);

            Id = _model.ID;
            Name = _model.Label;
        }
    }
}
