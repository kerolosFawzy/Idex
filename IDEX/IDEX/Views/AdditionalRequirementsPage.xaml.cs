using IDEX.ViewModel;
using Xamarin.Forms.Xaml;

namespace IDEX.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdditionalRequirementsPage 
	{
		public AdditionalRequirementsPage ()
		{
			InitializeComponent ();
            BindingContext = new AdditionalRequirementsViewModel();  
		}
	}
}