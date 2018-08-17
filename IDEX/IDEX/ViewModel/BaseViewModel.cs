using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace IDEX.ViewModel
{
    public class BaseViewModel : ViewModelBase
    {
        public  virtual void OnAppearing() {}
        public virtual void OnSoftBackButtonPressed() {}
        public virtual void DisAppearing() {}
        public virtual void OnBackButtonPressed() {}
        public INavigation Navigation { get; set; }

    }
}
