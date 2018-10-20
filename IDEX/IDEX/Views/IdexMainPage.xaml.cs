using IDEX.ViewModel;
using IDEX.Views;
using ReactiveUI;
using System.Reactive.Disposables;

namespace IDEX
{
    public partial class IdexMainPage : BaseContentPage<MainPageViewModel>
    {
        public IdexMainPage(){
            InitializeComponent();            
            BindingContext =  MainPageViewModel.Instance;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.WhenActivated(disposables =>
            {
                this.BindCommand(ViewModel, x => x.ReactiveNextItemClicked, x => x.NextButton);
                this.BindCommand(ViewModel, x => x.ReactiveBackButtonClicked, x => x.BackButton);
                this.BindCommand(ViewModel, x => x.ItemTapped, x => x.ItemTapped);

                this.OneWayBind(ViewModel, x => x.SelectedIndexs, x => x.StepProgressBarControl.ItemSelectedIndex).DisposeWith(SubscriptionDisposables);
                this.OneWayBind(ViewModel, x => x.SelectedCustomer, x => x.SelectedCustomer.ItemsSource).DisposeWith(SubscriptionDisposables);
                this.OneWayBind(ViewModel, x => x.SelectedSchemes, x => x.SelectedSchemes.ItemsSource).DisposeWith(SubscriptionDisposables);
                this.OneWayBind(ViewModel, x => x.ItemListSource, x => x.MainPageListView.ItemsSource).DisposeWith(SubscriptionDisposables);

                this.OneWayBind(ViewModel, x => x.BackBtnVisibilty, x => x.BackButton.IsVisible).DisposeWith(SubscriptionDisposables);
            });
        }
    }
}