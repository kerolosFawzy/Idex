using IDEX.ViewModel;
using ReactiveUI;
using Xamarin.Forms.Xaml;

namespace IDEX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverviewPage : BaseContentPage<OverviewScreenViewModel>
    {
        public OverviewPage()
        {
            InitializeComponent();
            BindingContext = new OverviewScreenViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.WhenActivated(disposables =>
            {
                this.BindCommand(ViewModel, x => x.ShowAll, x => x.ShowAll);
            });
        }

    }
}