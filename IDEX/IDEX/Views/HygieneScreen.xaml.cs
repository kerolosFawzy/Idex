using IDEX.ViewModel;

namespace IDEX.Views
{
	public partial class HygieneScreen : BaseContentPage<HygieneScreenViewModel>
    { 
		public HygieneScreen ()
		{
			InitializeComponent();
            BindingContext =new HygieneScreenViewModel(); 
        }
	}
}