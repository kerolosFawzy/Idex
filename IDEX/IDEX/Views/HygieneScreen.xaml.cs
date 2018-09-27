using IDEX.ViewModel;

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