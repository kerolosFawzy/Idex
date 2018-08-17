using CustomController;
using IDEX.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDEX
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            CustomNavigationPage.SetTitleColor(this, Color.Red);
            
        }
       

    }
}