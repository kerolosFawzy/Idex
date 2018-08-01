using GalaSoft.MvvmLight;
using IDEX.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace IDEX.ViewModel
{
    class CustomerViewModel : BaseViewModel
    {
        public ObservableCollection<Customer> Customers { set; get; }
            = new ObservableCollection<Customer>();

        
        private List<Customer> _selectCustomers;

        public List<Customer> SelectedCustomer
        {
            get { return _selectCustomers; }
            set { _selectCustomers = value;
                RaisePropertyChanged();
            }
        }

        public CustomerViewModel() {
            Customer customer;
            customer= new Customer { ID = 1, Name = "Hospital" };
            Customers.Add(customer);

            customer = new Customer { ID = 2, Name = "School" };
            Customers.Add(customer);

            customer = new Customer { ID = 3, Name = "University" };
            Customers.Add(customer);
        }
    }
}
