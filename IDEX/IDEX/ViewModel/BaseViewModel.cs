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
       
        // public virtual void OnBackButtonPressed() {}
        public INavigationService Navigation { get; } = App.NavigationService;
        protected IUserDialogs Dialogs { get; }

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
