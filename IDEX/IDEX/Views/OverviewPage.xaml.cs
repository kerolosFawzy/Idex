using IDEX.ViewModel;
using ReactiveUI;
using System.Reactive.Disposables;
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

                this.OneWayBind(ViewModel, x => x.ShowAllText, x => x.ShowAll.Text).DisposeWith(SubscriptionDisposables);
                this.OneWayBind(ViewModel, x => x.IsVisible, x => x.EmptyViewLabel.IsVisible).DisposeWith(SubscriptionDisposables);
                this.OneWayBind(ViewModel, x => x.ItemListSource, x => x.OverviewListView.ItemsSource).DisposeWith(SubscriptionDisposables);
                this.OneWayBind(ViewModel, x => x.ListViewIsVisible, x => x.OverviewListView.IsVisible).DisposeWith(SubscriptionDisposables);
            });
        }

    }
}