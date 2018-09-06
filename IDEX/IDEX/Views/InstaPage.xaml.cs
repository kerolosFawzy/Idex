using IDEX.ViewModel;

namespace IDEX.Views
{
	public partial class InstaPage  
	{
		public InstaPage()
		{
            InitializeComponent();
            BindingContext = InstaPageViewModel.Instance; 

        }
	}
}