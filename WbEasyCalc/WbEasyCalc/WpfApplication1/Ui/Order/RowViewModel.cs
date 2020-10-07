using System;
using System.ComponentModel;
using DAL;
using WpfApplication1.Utility;

namespace WpfApplication1.Ui.Order
{

    public class RowViewModel : ViewModelBase
    {
        public Model.Order Order { get; set; }
        public Model.Customer Customer { get; set; }

        public RowViewModel(Model.Order order, IMainRepo mainRepo)
        {
            Customer = mainRepo.CustomerRepo.GetItem(order.CustomerId);
            Order = order;
        }
    }
}
