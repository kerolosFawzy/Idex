using System;
using CustomController;
using CustomController.NavigationServices;
using IDEX.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDEX
{
    public partial class IdexMainPage
    {
        public IdexMainPage()
        {
            InitializeComponent();
            CustomNavigationPage.SetTitleFont(this , Font.SystemFontOfSize(24));
        }
      
    }
}