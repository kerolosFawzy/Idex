using IDEX.ViewModel;
using ReactiveUI;
using System.Reactive.Disposables;

namespace IDEX.Views
{
	public partial class InstaPage : BaseContentPage<InstaPageViewModel>
	{
        public InstaPage()
		{
            InitializeComponent();
            BindingContext = new InstaPageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.WhenActivated(disposables => {
                this.OneWayBind(ViewModel, vm => vm.IsVisible , v => v.PickerView.IsVisible).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.NumberPicker , v => v.PickerView.ItemsSource).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.InstaCategoryEnum, v => v.InstaCategoryEnum.Text).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.State, v => v.State.Text).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.InstaRoomEnum, v => v.InstaRoomEnum.Text).DisposeWith(disposables);

                this.Bind(ViewModel, vm => vm.Selected , v => v.PickerView.SelectedIndex).DisposeWith(disposables);

                this.Bind(ViewModel, vm => vm.Selected, v => v.FirstButtoncreator.PickerValue).DisposeWith(disposables);
                this.Bind(ViewModel, vm => vm.Selected, v => v.SecondButtoncreator.PickerValue).DisposeWith(disposables);
                this.Bind(ViewModel, vm => vm.Selected, v => v.ThridButtoncreator.PickerValue).DisposeWith(disposables);
                this.Bind(ViewModel, vm => vm.Selected, v => v.ForthButtoncreator.PickerValue).DisposeWith(disposables);
                this.Bind(ViewModel, vm => vm.Selected, v => v.FifthButtoncreator.PickerValue).DisposeWith(disposables);
                this.Bind(ViewModel, vm => vm.Selected, v => v.SixthButtoncreator.PickerValue).DisposeWith(disposables);
                this.Bind(ViewModel, vm => vm.Selected, v => v.SeventhButtoncreator.PickerValue).DisposeWith(disposables);
                this.Bind(ViewModel, vm => vm.Selected, v => v.EightButtoncreator.PickerValue).DisposeWith(disposables);

            });
        }
    }
}