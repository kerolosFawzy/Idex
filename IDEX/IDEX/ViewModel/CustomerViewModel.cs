using IDEX.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace IDEX.ViewModel
{
    class CustomerViewModel
    {
        public ObservableCollection<Customer> Customers { set; get; }
            = new ObservableCollection<Customer>();

        public CustomerViewModel() {
            Customer customer;
            customer= new Customer { Id = 1, Name = "Hospital" };
            Customers.Add(customer);

            customer = new Customer { Id = 2, Name = "School" };
            Customers.Add(customer);

            customer = new Customer { Id = 3, Name = "University" };
            Customers.Add(customer);
        }
    }
}
