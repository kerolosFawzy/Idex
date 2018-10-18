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
            });
        }
    }
}