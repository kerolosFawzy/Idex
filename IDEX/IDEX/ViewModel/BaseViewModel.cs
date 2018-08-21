using CustomController;
using GalaSoft.MvvmLight;
using IDEX.Views;
using Xamarin.Forms;

namespace IDEX.ViewModel
{
    public class BaseViewModel : ViewModelBase
    {
        public  virtual void OnAppearing() {}
        public virtual void OnSoftBackButtonPressed() {}
        public virtual void DisAppearing() {}
       // public virtual void OnBackButtonPressed() {}
        public INavigationService Navigation { get; } = App.NavigationService;

        public BaseContentPage baseContentPage { get; set; } 
    }
}
