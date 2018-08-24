using CustomController.NavigationServices;
using Xamarin.Forms;

namespace IDEX.Views
{
    public partial class OverviewPage
    {
        public OverviewPage()
        {
            InitializeComponent();
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(20));
        }

        public OverviewPage(string navigationParameter) : this()
        {
        }

    }
}