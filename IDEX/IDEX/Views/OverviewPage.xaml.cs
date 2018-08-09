using IDEX.ViewModel;
using System.Collections.Generic;
using Xamarin.Forms;

namespace IDEX.Views
{

    public partial class OverviewPage
    {
        private OverviewScreenViewModel overviewViewModel = new OverviewScreenViewModel();

        public OverviewPage()
        {
            InitializeComponent();
            BindingContext = overviewViewModel;
        }

    }
}