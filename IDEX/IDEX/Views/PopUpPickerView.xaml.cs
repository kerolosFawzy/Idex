using IDEX.ViewModel;
using Xamarin.Forms.Xaml;

namespace IDEX.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopUpPickerView
	{
		public PopUpPickerView ()
		{
			InitializeComponent ();
            BindingContext = new AdditionalRequirementsViewModel();
		}
	}
}