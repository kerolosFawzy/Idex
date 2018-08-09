using GalaSoft.MvvmLight.Command;
using IDEX.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace IDEX.ViewModel
{
    class OverviewScreenViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public ICommand ItemTapped { get; set; }
       
        #region ALL Lists Init
        private List<Level> _customerBuildings = new List<Level>();

        public List<Level> CustomerBuildings
        {
            get { return _customerBuildings; }

            set
            {
                _customerBuildings = value;
                RaisePropertyChanged();
            }
        }

        private List<Level> _buildings = new List<Level>();

        public List<Level> Buildings
        {
            get { return _buildings; }
            set
            {
                _buildings = value;
                RaisePropertyChanged();
            }
        }
        private List<Level> _floors = new List<Level>();

        public List<Level> Floor
        {
            get { return _floors; }
            set
            {
                _floors = value;
                RaisePropertyChanged();
            }
        }
        private List<Level> _rooms = new List<Level>();

        public List<Level> Rooms
        {
            get { return _rooms; }
            set
            {
                _rooms = value;
                RaisePropertyChanged();
            }
        }

        private IEnumerable _itemListSource = Enumerable.Empty<Level>();
        public IEnumerable ItemListSource
        {
            get { return _itemListSource; }
            set
            {
                _itemListSource = value;
                RaisePropertyChanged();
            }
        }

        #region selected lists 
        private List<Level> _selectedBuilding;

        public List<Level> SelectedBuilding
        {
            get { return _selectedBuilding; }
            set { _selectedBuilding = value; }
        }

        private List<Level> _selectedFloor;

        public List<Level> SelectedFloor
        {
            get { return _selectedFloor; }
            set { _selectedFloor = value; }
        }

        private List<Level> _selectedRoom;

        public List<Level> SelectedRoom
        {
            get { return _selectedRoom; }
            set { _selectedRoom = value; }
        }



        #endregion

        #endregion

        public OverviewScreenViewModel()
        {
            AddDummyData();
            ItemTapped = new Command(HandleItemTapped);
        }

        private void HandleItemTapped(object obj)
        {
            if (obj is Level SelecedLevel)
                NavigationHandler(SelecedLevel);
        }

        void NavigationHandler(Level SelecedLevel) 
        {
            switch (SelecedLevel.LevelType) {
                case 5:
                    SelectedBuilding = Filtering(SelecedLevel.ID).ToList();
                    ItemListSource = SelectedBuilding;
                    break;
                case 8:
                    SelectedFloor = Filtering(SelecedLevel.ID).ToList();
                    ItemListSource = SelectedFloor; 
                    break;
                case 11:
                    SelectedRoom = Filtering(SelecedLevel.ID).ToList();
                    ItemListSource = SelectedRoom;
                    break;
            }

        }

        IEnumerable<Level> Filtering(int id)
        {
            List<Level> Result = new List<Level>();
            List<Level> search = ((List<Level>)ItemListSource);
            switch (search[0].LevelType)
            {
                case 5:
                    Result = Buildings.Where(x=> x.OwnerId == id).ToList();
                    break;
                case 8:
                    Result = Floor.Where(x => x.OwnerId == id).ToList();
                    break;
                case 11:
                    Result = Rooms.Where(x => x.OwnerId == id).ToList();
                    break;
            }
            return Result; 
        }

        void AddDummyData()
        {
            CustomerBuildings.Add(new Level { LevelType = 5, Area = 30.5, DoorNumber = "32", ID = 1, Name = "Hosptial Customer", OwnerId = 1, UserId = 1 });
            CustomerBuildings.Add(new Level { LevelType = 5, Area = 32.5, DoorNumber = "322", ID = 2, Name = "School Customer", OwnerId = 2, UserId = 2 });
            CustomerBuildings.Add(new Level { LevelType = 5, Area = 33.5, DoorNumber = "321", ID = 3, Name = "university Customer", OwnerId = 3, UserId = 3 });

            Buildings.Add(new Level { LevelType = 8, Area = 30.5, DoorNumber = "32", ID = 10, Name = "Hosptial", OwnerId = 1 });
            Buildings.Add(new Level { LevelType = 8, Area = 32.5, DoorNumber = "322", ID = 20, Name = "School", OwnerId = 2 });
            Buildings.Add(new Level { LevelType = 8, Area = 33.5, DoorNumber = "321", ID = 30, Name = "university", OwnerId = 3 });

            Floor.Add(new Level { LevelType = 11, Area = 30.5, DoorNumber = "32", ID = 100, Name = "Hosptial floor", OwnerId = 10, UserId = 1 });
            Floor.Add(new Level { LevelType = 11, Area = 32.5, DoorNumber = "322", ID = 200, Name = "School Floor", OwnerId = 20, UserId = 2 });
            Floor.Add(new Level { LevelType = 11, Area = 33.5, DoorNumber = "321", ID = 300, Name = "university Floor", OwnerId = 30, UserId = 3 });

            Rooms.Add(new Level { LevelType = 14, Area = 30.5, DoorNumber = "32", ID = 1000, Name = "Hosptial Room ", OwnerId = 100, UserId = 1 });
            Rooms.Add(new Level { LevelType = 14, Area = 32.5, DoorNumber = "322", ID = 2000, Name = "School Rooms", OwnerId = 200, UserId = 2 });
            Rooms.Add(new Level { LevelType = 14, Area = 33.5, DoorNumber = "321", ID = 3000, Name = "university Rooms", OwnerId = 300, UserId = 3 });

            ItemListSource = CustomerBuildings;
        }
    }
}
