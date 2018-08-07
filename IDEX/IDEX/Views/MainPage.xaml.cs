using IDEX.Model;
using IDEX.ViewModel;

using Xamarin.Forms;

namespace IDEX
{
    public partial class MainPage 
    {
        private MainPageViewModel ViewModel = new MainPageViewModel();
    
        public MainPage()
        {
            InitializeComponent();
            BindingContext = ViewModel;
        }
        
    }
}