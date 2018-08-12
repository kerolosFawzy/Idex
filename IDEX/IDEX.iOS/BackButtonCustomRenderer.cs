
using IDEX.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace IDEX.iOS
{
    class BackButtonCustomRenderer : PageRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var page = Element as BaseContentPage;
            if (page == null) return;

            #region for soft back button

            var root = NavigationController.TopViewController;
            if (!page.NeedOverrideSoftBackButton) return;
            var title = NavigationPage.GetBackButtonTitle(Element);

            root.NavigationItem.SetLeftBarButtonItem(
                new UIBarButtonItem(title, UIBarButtonItemStyle.Plain, (sender, args) =>
                {
                    page.OnSoftBackButtonPressed();
                }), true);

            #endregion
        }
    }
}