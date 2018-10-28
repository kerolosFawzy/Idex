using IDEX.ViewModel;
using ReactiveUI;
using System.Reactive.Disposables;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.WhenActivated(disposables =>
            {
                this.BindCommand(ViewModel, x => x.ChangePageCommand, x => x.UseExtra);
                this.BindCommand(ViewModel, x => x.InstaCommand, x => x.Insta);
                this.BindCommand(ViewModel, x => x.HygieneCommand, x => x.Hygiene);
                this.BindCommand(ViewModel, x => x.AdditionalCommand, x => x.Additional);

                this.OneWayBind(ViewModel, x => x.UseExtra, x => x.UseExtra.Text).DisposeWith(SubscriptionDisposables);
                this.OneWayBind(ViewModel, x => x.ID, x => x.ID.Text).DisposeWith(SubscriptionDisposables);
                this.OneWayBind(ViewModel, x => x.DoorNo, x => x.DoorNo.Text).DisposeWith(SubscriptionDisposables);
                this.OneWayBind(ViewModel, x => x.Area, x => x.Area.Text).DisposeWith(SubscriptionDisposables);
                this.OneWayBind(ViewModel, x => x.Name, x => x.Name.Text).DisposeWith(SubscriptionDisposables);
                this.OneWayBind(ViewModel, x => x.HygLevel, x => x.HygLevel.Text).DisposeWith(SubscriptionDisposables);
                this.OneWayBind(ViewModel, x => x.HygType, x => x.HygType.Text).DisposeWith(SubscriptionDisposables);


                this.OneWayBind(ViewModel, x => x.InstaSvgPath, x => x.InstaSvgPath.Source).DisposeWith(SubscriptionDisposables);
                this.OneWayBind(ViewModel, x => x.HygSvgPath, x => x.HygSvgPath.Source).DisposeWith(SubscriptionDisposables);
                this.OneWayBind(ViewModel, x => x.AdditionalSvgPath, x => x.AdditionalSvgPath.Source).DisposeWith(SubscriptionDisposables);
                });
        }
    }
}