using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDEX.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IdexMasterDetailPageMaster : ContentPage
    {
        public ListView ListView;

        public IdexMasterDetailPageMaster()
        {
            InitializeComponent();

            BindingContext = new IdexMasterDetailPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class IdexMasterDetailPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<IdexMasterDetailPageMenuItem> MenuItems { get; set; }
            
            public IdexMasterDetailPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<IdexMasterDetailPageMenuItem>(new[]
                {
                    new IdexMasterDetailPageMenuItem { Id = 0, Title = "Home" },
                    new IdexMasterDetailPageMenuItem { Id = 1, Title = "Settings" },
                    new IdexMasterDetailPageMenuItem { Id = 2, Title = "Help" },
                    new IdexMasterDetailPageMenuItem { Id = 3, Title = "Drawing" },
                    new IdexMasterDetailPageMenuItem { Id = 4, Title = "Statistics" },
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}