using IDEX.ViewModel;
using Xamarin.Forms.Xaml;
//https://github.com/rotorgames/Rg.Plugins.Popup
namespace IDEX.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopUpPickerView
	{
		public PopUpPickerView ()
		{
			InitializeComponent ();
            BindingContext = AdditionalRequirementsViewModel.Instance;
		}
	}
}