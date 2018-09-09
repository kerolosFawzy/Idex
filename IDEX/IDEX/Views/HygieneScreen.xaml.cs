using IDEX.ViewModel;
using Xamarin.Forms.Xaml;

namespace IDEX.Views
{

	public partial class HygieneScreen 
	{
		public HygieneScreen ()
		{
			InitializeComponent();
            BindingContext = new HygieneScreenViewModel();
        }
	}
}