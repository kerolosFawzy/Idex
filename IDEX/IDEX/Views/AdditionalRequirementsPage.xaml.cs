using IDEX.ViewModel;
using ReactiveUI;
using Xamarin.Forms.Xaml;

namespace IDEX.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdditionalRequirementsPage : BaseContentPage<AdditionalRequirementsViewModel>
	{
		public AdditionalRequirementsPage ()
		{
			InitializeComponent ();
            BindingContext = AdditionalRequirementsViewModel.Instance;  
		}
       
    }
}