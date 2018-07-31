using IDEX.Model;
using IDEX.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

          //  var listview = ListView.FindName
        }

        private void MainListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as Customer;
            //selectedItem.IsChecked = !selectedItem.IsChecked; 
            //DisplayAlert("MainPage", selectedItem.IsChecked + "  " + selectedItem.Name, "oh");
        }

        private void Next_Clicked(object sender, EventArgs e)
        {
            ObservableCollection<Customer> customers = CustomerViewModel.Customers;
            List<Customer> ts = customers.Where(x => x.IsChecked == true).ToList();
            DisplayAlert("selected Items " , ts.Count.ToString() , "Ok");
            if (ts != null)
                CustomerViewModel.SelectedCustomer = ts;

            // var listview = this.FindByName<>("");
        }
    }
}