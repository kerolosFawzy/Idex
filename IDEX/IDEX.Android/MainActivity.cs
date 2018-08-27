using Android.App;
using Android.Content.PM;
using Android.OS;
using Acr.UserDialogs;
using SVG.Forms.Plugin.Droid;

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
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            SvgImageRenderer.Init();
            LoadApplication(new App());
        }

        

    }


}

