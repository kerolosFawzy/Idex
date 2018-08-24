using CustomControls.NavigationServices;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDEX.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IdexMasterDetailPage : MasterDetailPage
    {
        public IdexMasterDetailPage()
        {
            InitializeComponent();
            CustomNavigationPage.SetHasNavigationBar(this, false);
            MasterBehavior = MasterBehavior.Popover;
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
            Detail = new CustomNavigationPage(new IdexMainPage());
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(e.SelectedItem is IdexMasterDetailPageMenuItem item))
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
                
            if (item.Title.Equals("Home")) {
                page.Title = "Home";
                Detail = new CustomNavigationPage(new IdexMainPage());
            }
            else
            {
                page.Title = item.Title;
                Detail = new CustomNavigationPage(page);
            }
                
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}