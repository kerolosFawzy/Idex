using CustomControls.NavigationServices;
using IDEX.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDEX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverviewPage 
    {
        public OverviewPage()
        {
            InitializeComponent();
            BindingContext = OverviewScreenViewModel.Instance; 
        }

        
    }
}