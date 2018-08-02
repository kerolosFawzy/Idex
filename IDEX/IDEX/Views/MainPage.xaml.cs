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
        #region
        MainPageViewModel ViewModel = new MainPageViewModel();
        private int flag;
        List<Customer> ts = new List<Customer>();
        // 0 , 2 , 4 numbers of index of buttons 
        Button child1, child2; 
        IList<Xamarin.Forms.View> ButtonsChildern = new List<Xamarin.Forms.View>();
        #endregion

        public MainPage()
        {
            InitializeComponent();
            BindingContext = ViewModel;
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
            Binding myBinding = new Binding("Customers");
            child1.BackgroundColor= Color.FromHex("#008080");
            child2.BackgroundColor = Color.Transparent;
            MainPageListView.SetBinding(ListView.ItemsSourceProperty, myBinding);
            ViewModel.SchemeBindingList.Clear();
        }



        private void NavigationHandeler()
        {
            if (flag == 0)
            {
                List<Customer> customers = ViewModel.Customers;
                ts = customers.Where(x => x.IsChecked == true).ToList();
                if (ts != null)
                {
                    flag = 1;
                    ViewModel.SelectedCustomer = ts;
                    BackButton.IsVisible = true;
                    List<Scheme> scheme = new List<Scheme>();
                    for (int i = 0; i < ts.Count(); i++)
                    {
                        scheme.AddRange( ViewModel.Schemes.Where(x => x.CustomerId == ViewModel.SelectedCustomer[i].ID).ToList());
                    }
                    ViewModel.SchemeBindingList = scheme;
                    Binding myBinding = new Binding("SchemeBindingList");
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