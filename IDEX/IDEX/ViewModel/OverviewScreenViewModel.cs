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
        private IEnumerable _allLevels = Enumerable.Empty<Level>();

        public IEnumerable AllLevels
        {
            get { return _allLevels ; }
            set { _allLevels = value;
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

        private List<Level> _levelListWithChildren = new List<Level> ();

        public List<Level> LevelListWithChildren
        {
            get { return _levelListWithChildren; }
            set { _levelListWithChildren = value;
                RaisePropertyChanged(); 
            }
        }


        #endregion

        public OverviewScreenViewModel()
        {
            AddDummyData();
            SetFirstListOfLevels();

            ItemTapped = new Command(HandleItemTapped);
        }

        private void HandleItemTapped(object obj)
        {
            if (obj is Level SelecedLevel)
                NavigationHandler(SelecedLevel);
        }

        void NavigationHandler(Level SelecedLevel) 
        {
            if (SelecedLevel.Children.Count() != 0)
                ItemListSource = SelecedLevel.Children;
            else
                return; 

        }

        void SetFirstListOfLevels()
        {
            List<Level> allLevels = new List<Level>();
            allLevels = AllLevels as List<Level>;

            List<int> AllLevelTypes = allLevels.Select(x => x.LevelType).Distinct().ToList();
            AllLevelTypes.Sort();

            List<Level> Parents = allLevels
                .Where(x => x.LevelType == AllLevelTypes.FirstOrDefault()).ToList();

            ItemListSource = Parents;
            SortChildren(AllLevelTypes , allLevels); 
        }

        void SortChildren(List<int> AllLevelTypes,List<Level> allLevels ) {
            for (int i = 0; i < AllLevelTypes.Count() -1; i++) {
                List<Level> searchList = allLevels
                    .Where(x => x.LevelType == AllLevelTypes[i] || x.LevelType == AllLevelTypes[i+1])
                    .ToList();
                foreach (Level level in searchList) {
                    if (i != 0) {
                        level.Parent = allLevels
                               .Where(x => x.LevelType == AllLevelTypes[i - 1] && x.ID == level.OwnerId)
                               .FirstOrDefault();
                    }
                    level.Children = allLevels
                        .Where(x=>x.OwnerId == level.ID)
                        .ToList();
                    LevelListWithChildren.Add(level);
                }
            }
            LevelListWithChildren = LevelListWithChildren.Distinct().ToList();

        }
        void AddDummyData()
        {
            List<Level> FakeList = new List<Level>
            {
                new Level { LevelType = 5, Area = 30.5, DoorNumber = "32", ID = 1, Name = "Hosptial Customer", UserId = 1 },
                new Level { LevelType = 5, Area = 32.5, DoorNumber = "322", ID = 2, Name = "School Customer", UserId = 2 },

                new Level { LevelType = 8, Area = 30.5, DoorNumber = "32", ID = 10, Name = "Hosptial", OwnerId = 1 },
                new Level { LevelType = 8, Area = 32.5, DoorNumber = "322", ID = 20, Name = "School", OwnerId = 2 },
                new Level { LevelType = 8, Area = 33.5, DoorNumber = "321", ID = 30, Name = "university", OwnerId = 2 },

                new Level { LevelType = 11, Area = 30.5, DoorNumber = "32", ID = 100, Name = "Hosptial floor", OwnerId = 10, UserId = 1 },
                new Level { LevelType = 11, Area = 32.5, DoorNumber = "322", ID = 200, Name = "School Floor", OwnerId = 20, UserId = 2 },
                new Level { LevelType = 11, Area = 33.5, DoorNumber = "321", ID = 300, Name = "university Floor", OwnerId = 30, UserId = 3 },

                new Level { LevelType = 14, Area = 30.5, DoorNumber = "32", ID = 1000, Name = "Hosptial Room ", OwnerId = 100, UserId = 1 },
                new Level { LevelType = 14, Area = 32.5, DoorNumber = "322", ID = 2000, Name = "School Rooms", OwnerId = 200, UserId = 2 },
                new Level { LevelType = 14, Area = 33.5, DoorNumber = "321", ID = 3000, Name = "university Rooms", OwnerId = 300, UserId = 3 },

                new Level { LevelType = 14, Area = 30.5, DoorNumber = "32", ID = 10001, Name = "Hosptial Room 2", OwnerId = 100, UserId = 1 },
                new Level { LevelType = 14, Area = 32.5, DoorNumber = "322", ID = 20001, Name = "School Rooms2", OwnerId = 200, UserId = 2 },
                new Level { LevelType = 14, Area = 33.5, DoorNumber = "321", ID = 30001, Name = "university Rooms2", OwnerId = 300, UserId = 3 }
            };
            AllLevels = FakeList;
        }
    }
}


#region
//CustomerBuildings[0].Children.Add(Buildings[0]);
//CustomerBuildings[0].Children.Add(Buildings[1]);
//CustomerBuildings[1].Children.Add(Buildings[2]);

//Buildings[0].Parent = CustomerBuildings[0];
//Buildings[1].Parent = CustomerBuildings[0];
//Buildings[2].Parent = CustomerBuildings[1];
//Buildings[0].Children.Add(Floor[0]);
//Buildings[1].Children.Add(Floor[1]);
//Buildings[2].Children.Add(Floor[2]);

//Floor[0].Parent = Buildings[0];
//Floor[1].Parent = Buildings[1];
//Floor[2].Parent = Buildings[2];
//Floor[0].Children.Add(Rooms[0]);
//Floor[1].Children.Add(Rooms[1]);
//Floor[2].Children.Add(Rooms[2]);
//Floor[0].Children.Add(Rooms[3]);
//Floor[1].Children.Add(Rooms[4]);
//Floor[2].Children.Add(Rooms[5]);

//Rooms[0].Parent = Floor[0];
//Rooms[1].Parent = Floor[1];
//Rooms[2].Parent = Floor[2];
//Rooms[3].Parent = Floor[3];
//Rooms[4].Parent = Floor[4];
//Rooms[5].Parent = Floor[5];
#endregion