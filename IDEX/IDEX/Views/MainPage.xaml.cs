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
        private int flag;
        List<Customer> ts = new List<Customer>();
        SchemeViewModel schemeView = new SchemeViewModel();
        // 0 , 2 , 4 numbers of index of buttons 
        Button child1, child2; 
        IList<Xamarin.Forms.View> ButtonsChildern = new List<Xamarin.Forms.View>();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = CustomerViewModel;
            flag = 0;
            BackButton.IsVisible = false;
            ButtonsChildern = stepProgressBar.Children.ToList();
            child1 = ButtonsChildern[0] as Button;
            child2 = ButtonsChildern[2] as Button;
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
            child1.BackgroundColor= Color.FromHex("#008080");
            child2.BackgroundColor = Color.Transparent;
            MainPageListView.SetBinding(ListView.ItemsSourceProperty, myBinding);
            schemeView.SelectedSchemes.Clear();
        }



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
                    }
                    Binding myBinding = new Binding("SelectedSchemes")
                    {
                        Source = schemeView
                    };
                    child2.BackgroundColor = Color.FromHex("#008080");
                    MainPageListView.SetBinding(ListView.ItemsSourceProperty, myBinding);
                }
                else
                {
                    DisplayAlert("Alert", "Please Select Customer(s) Frist", "ok");
                }

            }
        }
    }
}