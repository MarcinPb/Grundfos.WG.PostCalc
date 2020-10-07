using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using WpfApplication1.Utility;

namespace WpfApplication1.Ui.Order
{
    public class ItemViewModel : ViewModelBase
    {
        public Model.Order Model
        {
            get
            {
                return new Model.Order()
                {
                    OrderId = this.Id,
                    CustomerId = this.CustomerId,
                    OrderDesc = this.Desc,
                    Price = this.Price,
                    OrderDate = this.OrderDate,
                    IsPaid = this.IsPaid,
                };
            }
        }

        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                RaisePropertyChanged("Id");
            }
        }

        private int _customerId;
        public int CustomerId
        {
            get
            {
                return _customerId;
            }
            set
            {
                _customerId = value;
                RaisePropertyChanged("CustomerId");
            }
        }

        //private Model.Customer _customer;
        //public Model.Customer Customer
        //{
        //    get
        //    {
        //        return _customer;
        //    }
        //    set
        //    {
        //        _customer = value;
        //        RaisePropertyChanged("Customer");
        //    }
        //}

        private string _desc;
        public string Desc
        {
            get
            {
                return _desc;
            }
            set
            {
                _desc = value;
                RaisePropertyChanged("Desc");
            }
        }

        private decimal _price;
        public decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                RaisePropertyChanged("Price");
            }
        }

        private DateTime _orderDate;
        public DateTime OrderDate
        {
            get
            {
                return _orderDate;
            }
            set
            {
                _orderDate = value;
                RaisePropertyChanged("CustomerId");
            }
        }

        private bool _isPaid;
        public bool IsPaid
        {
            get
            {
                return _isPaid;
            }
            set
            {
                _isPaid = value;
                RaisePropertyChanged("IsPaid");
            }
        }

        public ItemViewModel(Model.Order order, IMainRepo mainRepo)
        {
            Id = order.OrderId;
            CustomerId = order.CustomerId;
            //Customer = mainRepo.CustomerRepo.GetItem(order.CustomerId);
            Desc = order.OrderDesc;
            OrderDate = order.OrderDate;
            Price = order.Price;
            IsPaid = order.IsPaid;
        }
    }
}
