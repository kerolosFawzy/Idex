using Android.App;
using Android.Content.PM;
using Android.OS;
using Acr.UserDialogs;

namespace IDEX.Droid
{
    [Activity(Label = "IDEX", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            UserDialogs.Init(this);
            Rg.Plugins.Popup.Popup.Init(this, bundle);

            base.OnCreate(bundle);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
              
            }
        }

    }


}

