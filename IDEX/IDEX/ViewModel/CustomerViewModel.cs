using IDEX.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace IDEX.ViewModel
{
    class CustomerViewModel
    {
        ObservableCollection<Customer> customers { set; get; }
            = new ObservableCollection<Customer>();

        public CustomerViewModel() {
            Customer customer = new Customer { Id = 10 , Name = "kero" };
            customers.Add(customer); 
        }
    }
}
