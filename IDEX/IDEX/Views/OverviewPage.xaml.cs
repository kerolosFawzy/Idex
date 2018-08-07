using IDEX.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDEX.Views
{

    public partial class OverviewPage : ContentPage
    {
        private OverviewScreenViewModel overviewViewModel = new OverviewScreenViewModel();

        public OverviewPage()
        {
            InitializeComponent();
            BindingContext = overviewViewModel;
        }

    }
}