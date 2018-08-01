using IDEX.Model;
using IDEX.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace IDEX
{
    public partial class MainPage : ContentPage
    {
        CustomerViewModel CustomerViewModel = new CustomerViewModel();
        private int flag;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = CustomerViewModel;
            flag = 0;
            BackButton.IsVisible = false;
        }

        private void MainListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as Customer;
            //selectedItem.IsChecked = !selectedItem.IsChecked; 
            //DisplayAlert("MainPage", selectedItem.IsChecked + "  " + selectedItem.Name, "oh");
        }

        private void Next_Clicked(object sender, EventArgs e)
        {
            NavigationHandeler();
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            flag = 0;
            BackButton.IsVisible = false;
            Binding myBinding = new Binding("Customers")
            {
                Source = CustomerViewModel
            };
            MainPageListView.SetBinding(ListView.ItemsSourceProperty, myBinding);
        }


        List<Customer> ts = new List<Customer>();
        SchemeViewModel schemeView = new SchemeViewModel();

        private void NavigationHandeler()
        {
            if (flag == 0)
            {
                ObservableCollection<Customer> customers = CustomerViewModel.Customers;
                ts = customers.Where(x => x.IsChecked == true).ToList();
                if (ts != null)
                {
                    flag = 1;
                    CustomerViewModel.SelectedCustomer = ts;
                    BackButton.IsVisible = true;
                    for (int i = 0; i < ts.Count(); i++)
                    {
                        List<Scheme> scheme = new List<Scheme>();
                        scheme = schemeView.Schemes.Where(x => x.CustomerId == ts[i].ID).ToList();
                        schemeView.SelectedSchemes.AddRange(scheme);

                        if (scheme != null)
                        {
                         //   for (int x = 0; x < scheme.Count(); x++)

                        }
                    }
                    Binding myBinding = new Binding("SelectedSchemes")
                    {
                        Source = schemeView
                    };
                    MainPageListView.SetBinding(ListView.ItemsSourceProperty, myBinding);
                }
            }
        }
    }
}