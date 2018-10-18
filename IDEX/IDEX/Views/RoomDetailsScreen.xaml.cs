using IDEX.ViewModel;
using Xamarin.Forms.Xaml;

namespace IDEX.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoomDetailsScreen : BaseContentPage<RoomDetailsScreenViewModel>
	{
		public RoomDetailsScreen ()
		{
			InitializeComponent ();
        }
    }
}