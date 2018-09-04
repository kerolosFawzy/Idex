using CustomControls;
using CustomControls.NavigationServices;
using IDEX.View;
using IDEX.Views;
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
        public static INavigationService NavigationService { get; } = new ViewNavigationService();
		public App ()
        {
            InitializeComponent();
           // ExtractDI();
            RegisterPages();

            SetRootPage(nameof(IdexMasterDetailPage));

        }

        public void RegisterPages()
        {
            NavigationService.Configure(nameof(IdexMainPage), typeof(IdexMainPage));
            NavigationService.Configure(nameof(IdexMasterDetailPage), typeof(IdexMasterDetailPage));
            NavigationService.Configure(nameof(OverviewPage), typeof(OverviewPage));
            NavigationService.Configure(nameof(RoomDetailsScreen), typeof(RoomDetailsScreen));
            NavigationService.Configure(nameof(InstaPage), typeof(InstaPage));
        }

        public void SetRootPage(string rootPageName)
        {
            var mainPage = ((ViewNavigationService)NavigationService).SetRootPage(rootPageName);
            MainPage = mainPage;
        }


        //public static IContainer contianer;

        //private static void ExtractDI()
        //{
        //    ContainerBuilder builder = new ContainerBuilder();

        //    contianer = builder.Build();
        //}



        protected override void OnStart ()
		{
            AppCenter
                .Start("ios=35258c53-36f4-4949-9b4c-dc11fd63bb4e;" + "uwp={Your UWP App secret here};"
                + "android=058169e8-5915-44d8-a96e-c10a957d3d33;"
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
