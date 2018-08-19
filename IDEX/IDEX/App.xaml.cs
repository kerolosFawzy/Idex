using Autofac;
using CustomController;
using CustomController.NavigationServices;
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
        public static IContainer contianer;
		public App ()
        {
            InitializeComponent();
            ExtractDI();


            NavigationService.Configure(nameof(IdexMainPage), typeof(IdexMainPage));
            NavigationService.Configure(nameof(OverviewPage), typeof(OverviewPage));
            var mainPage = ((ViewNavigationService)NavigationService).SetRootPage("IdexMainPage");



            MainPage = new IdexMasterDetailPage();

        }

        private static void ExtractDI()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<CustomNavigationPage>().As<ICustomNavigationPage>();
            contianer = builder.Build();
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
