using Acr.UserDialogs;
using CustomControls;
using IDEX.Views;
using ReactiveUI;
using Splat;
using Xamarin.Forms;

namespace IDEX.ViewModel
{
    public class BaseViewModel : ReactiveObject, IRoutableViewModel, ISupportsActivation
    {
        public  virtual void OnAppearing() {}
        public virtual void OnSoftBackButtonPressed() {}
        public virtual void DisAppearing() {}

        //private FormattedString _FormattedTitle;
        //public FormattedString FormattedTitle
        //{
        //    get => _FormattedTitle;
        //    set => this.RaiseAndSetIfChanged(ref _FormattedTitle, value);
        //}

        //private FormattedString _FormattedSubtitle;
        //public FormattedString FormattedSubtitle
        //{
        //    get => _FormattedSubtitle;
        //    set => this.RaiseAndSetIfChanged(ref _FormattedSubtitle, value);
        //}

        //private string _Subtitle;
        //public string Subtitle
        //{
        //    get => _Subtitle;
        //    set => this.RaiseAndSetIfChanged(ref _Subtitle, value);
        //}

        // public virtual void OnBackButtonPressed() {}
        public INavigationService Navigation { get; } = App.NavigationService;
        protected IUserDialogs Dialogs { get; }
        public BaseContentPage baseContentPage { get; set; }

        public string UrlPathSegment
        {
            get;
            protected set;
        }

        public IScreen HostScreen
        {
            get;
            protected set;
        }

        public ViewModelActivator Activator
        {
            get { return viewModelActivator; }
        }

        protected readonly ViewModelActivator viewModelActivator = new ViewModelActivator();

        public BaseViewModel(IScreen hostScreen = null)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
        }
    }
}
