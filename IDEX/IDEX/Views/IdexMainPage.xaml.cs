using CustomControls.NavigationServices;
using System;
using Xamarin.Forms;

namespace IDEX
{
    public partial class IdexMainPage 
    {
        public IdexMainPage(){
            InitializeComponent();
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(22));
            
        }
    }
}