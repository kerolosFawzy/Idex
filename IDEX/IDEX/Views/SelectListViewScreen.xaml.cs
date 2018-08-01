using IDEX.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDEX.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectListViewScreen : ContentView
	{
		public SelectListViewScreen ()
		{
			InitializeComponent ();
          //  BindingContext = new CustomerViewModel();
        }
    }
}