using IDEX.Model;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace IDEX.ViewModel
{
    class OverviewScreenViewModel : BaseViewModel, INotifyPropertyChanged
    {
        #region the Main Lists init
        private List<Level> _customerBuildings = new List<Level>();

        public List<Level> CustomerBuildings
        {
            get { return _customerBuildings; }

            set { _customerBuildings = value;
                RaisePropertyChanged();
            }
        }

        private List<Level> _buildings=new List<Level>();

        public List<Level> Buildings
        {
            get { return _buildings; }
            set { _buildings = value;
                RaisePropertyChanged();
            }
        }
        private List<Level> _floors = new List<Level>();

        public List<Level> Floor
        {
            get { return _floors; }
            set { _floors = value;
                RaisePropertyChanged();
            }
        }
        private List<Level> _rooms = new List<Level>();

        public List<Level> Rooms 
        {
            get { return _rooms; }
            set { _rooms = value;
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
        #endregion

        public OverviewScreenViewModel() { AddDummyData(); }

        void AddDummyData() {
            CustomerBuildings.Add(new Level { LevelType = 5 , Area = 30.5 , DoorNumber ="32" , ID=1 , Name = "Hosptial Customer" , OwnerId =1 , UserId = 1  });
            CustomerBuildings.Add(new Level { LevelType = 5 , Area = 32.5 , DoorNumber ="322" , ID=2 , Name = "School Customer" , OwnerId =2 , UserId = 2 });
            CustomerBuildings.Add(new Level { LevelType = 5 , Area = 33.5 , DoorNumber ="321" , ID=3 , Name = "university Customer" , OwnerId =3 , UserId = 3 });

            Buildings.Add(new Level { LevelType = 8, Area = 30.5, DoorNumber = "32", ID = 1, Name = "Hosptial", OwnerId = 1, UserId = 1 });
            Buildings.Add(new Level { LevelType = 8, Area = 32.5, DoorNumber = "322", ID = 2, Name = "School", OwnerId = 2, UserId = 2});
            Buildings.Add(new Level { LevelType = 8, Area = 33.5, DoorNumber = "321", ID = 3, Name = "university", OwnerId = 3, UserId = 3});

            Floor.Add(new Level { LevelType = 11, Area = 30.5, DoorNumber = "32", ID = 1, Name = "Hosptial floor", OwnerId = 1, UserId = 1});
            Floor.Add(new Level { LevelType = 11, Area = 32.5, DoorNumber = "322", ID = 2, Name = "School Floor", OwnerId = 2, UserId = 2});
            Floor.Add(new Level { LevelType = 11, Area = 33.5, DoorNumber = "321", ID = 3, Name = "university Floor", OwnerId = 3, UserId = 3});

            Rooms.Add(new Level { LevelType = 14, Area = 30.5, DoorNumber = "32", ID = 1, Name = "Hosptial Room ", OwnerId = 1, UserId = 1 });
            Rooms.Add(new Level { LevelType = 14, Area = 32.5, DoorNumber = "322", ID = 2, Name = "School Rooms", OwnerId = 2, UserId = 2 });
            Rooms.Add(new Level { LevelType = 14, Area = 33.5, DoorNumber = "321", ID = 3, Name = "university Rooms", OwnerId = 3, UserId = 3 });

            ItemListSource = CustomerBuildings;
        }
    }
}
