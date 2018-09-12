using IDEX.ViewModel;
using Xamarin.Forms.Xaml;

namespace IDEX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverviewPage 
    {
        public OverviewPage()
        {
            InitializeComponent();
            BindingContext = new OverviewScreenViewModel(); 
        }

        
    }
}