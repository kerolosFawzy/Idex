using IDEX.ViewModel;
using Xamarin.Forms.Xaml;

namespace IDEX.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoomDetailsScreen
	{
		public RoomDetailsScreen ()
		{
			InitializeComponent ();
            BindingContext = RoomDetailsScreenViewModel.Instance;
        }
    }
}