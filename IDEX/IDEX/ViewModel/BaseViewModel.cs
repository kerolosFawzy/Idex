using Acr.UserDialogs;
using CustomControls;
using GalaSoft.MvvmLight;
using IDEX.Views;


namespace IDEX.ViewModel
{
    public class BaseViewModel : ViewModelBase
    {
        public  virtual void OnAppearing() {}
        public virtual void OnSoftBackButtonPressed() {}
        public virtual void DisAppearing() {}
       // public virtual void OnBackButtonPressed() {}
        public INavigationService Navigation { get; } = App.NavigationService;
        protected IUserDialogs Dialogs { get; }
        public BaseContentPage baseContentPage { get; set; } 
    }
}
