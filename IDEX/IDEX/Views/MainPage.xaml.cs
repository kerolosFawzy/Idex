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
       
        //0 , 2 , 4 numbers of index of buttons 
        // 1 , 3, 5 Class id for buttons 
        Button CustomerButton, SchemeButton , InspectionButton; 
        IList<Xamarin.Forms.View> ButtonsChildern = new List<Xamarin.Forms.View>();
        #endregion

        public MainPage()
        {
            InitializeComponent();
            BindingContext = ViewModel;
            flag = 0;

            BackButton.IsVisible = false;
            ButtonsChildern = stepProgressBar.Children.ToList();
            CustomerButton = ButtonsChildern[0] as Button;
            SchemeButton = ButtonsChildern[2] as Button;
            InspectionButton = ButtonsChildern[4] as Button;
            

        }
        
        private void MainListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as Customer;
            //selectedItem.IsChecked = !selectedItem.IsChecked; 
            //DisplayAlert("MainPage", selectedItem.IsChecked + "  " + selectedItem.Name, "oh");
        }

        private void Next_Clicked(object sender, EventArgs e)
        {
            flag = flag + 1; 
            NavigationHandeler();
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            flag = flag - 1;
            NavigationHandeler(); 
        }

      


    }
}