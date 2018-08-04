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
        MainPageViewModel ViewModel = new MainPageViewModel();
    
        public MainPage()
        {
            InitializeComponent();
            BindingContext = ViewModel;
        }
    }
}