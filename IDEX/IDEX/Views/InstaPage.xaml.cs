using IDEX.ViewModel;
using ReactiveUI;

namespace IDEX.Views
{
	public partial class InstaPage : BaseContentPage<InstaPageViewModel>
	{
        public InstaPage()
		{
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.WhenActivated(disposables => {

            });
        }
    }
}