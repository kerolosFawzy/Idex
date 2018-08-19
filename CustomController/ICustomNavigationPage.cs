using Xamarin.Forms;

namespace CustomController
{
    public interface ICustomNavigationPage
    {
         void PopCustomAsync();
        void PushCustomAsync(ContentPage page);
    }
}