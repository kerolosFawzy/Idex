using IDEX.ViewModel;

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