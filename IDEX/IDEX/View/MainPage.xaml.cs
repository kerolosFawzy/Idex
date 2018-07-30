using CustomController;
using IDEX.Model;
using IDEX.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IDEX
{
	public partial class MainPage : ContentPage
	{
        CustomerViewModel CustomerViewModel = new CustomerViewModel();

        public MainPage()
		{
			InitializeComponent();
            BindingContext = CustomerViewModel;
        }

        private void MainListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as Customer;

            DisplayAlert("MainPage", selectedItem.IsChecked + "  " + selectedItem.Name, "oh");
        }

        private void Next_Clicked(object sender, EventArgs e)
        {
            System.Collections.ObjectModel.ObservableCollection<Customer>
            customers = CustomerViewModel.Customers;
            List<Customer> ts = customers.Where(x=> x.IsChecked == true).ToList();

        }
    }
}