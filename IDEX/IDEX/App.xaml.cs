using IDEX.View;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace IDEX
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new IdexMasterDetailPage() ;
		}

		protected override void OnStart ()
		{
            AppCenter
                .Start("ios=35258c53-36f4-4949-9b4c-dc11fd63bb4e;" + "uwp={Your UWP App secret here};"
                + "android={Your Android App secret here}"
                , typeof(Analytics), typeof(Crashes));

        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
