using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace IDEX.ViewModel
{
    public class BaseViewModel : ViewModelBase
    {
        public  virtual void OnAppearing() {
            
        }
        public virtual void DisAppearing() { }

        public INavigation Navigation { get; set; }

    }
}
