using CustomController;
using CustomController.NavigationServices;
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
        public ICommand ShowAll { get; set; }
        public ICommand ItemTapped { get; set; }
        readonly Color PieChartColor = Color.FromHex("#008080");


        public bool ShowAllFlag { get; set; }
        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        private string _formattedTitle = "Site";

        private string _showAllText = "Show UnCompleted";

        public string ShowAllText
        {
            get { return _showAllText; }
            set
            {
                _showAllText = value;
                RaisePropertyChanged();
            }
        }

        public string FormattedTitle
        {
            get { return _formattedTitle; }
            set
            {
                _formattedTitle = value;
                RaisePropertyChanged();
            }
        }

        private string _formattedSubTitle;
        private bool _isVisible = false;

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                RaisePropertyChanged();
            }
        }

        public string FormattedSubTitle
        {
            get { return _formattedSubTitle; }
            set
            {
                _formattedSubTitle = value;
                RaisePropertyChanged();
            }
        }

        #region ALL Lists Init

        private List<Level> _formattedTitlesStack = new List<Level>();

        public List<Level> FormattedTitlesStack
        {
            get { return _formattedTitlesStack; }
            set
            {
                _formattedTitlesStack = value;
                RaisePropertyChanged();
            }
        }


        private IEnumerable _allLevels = Enumerable.Empty<Level>();

        public IEnumerable AllLevels
        {
            get { return _allLevels; }
            set
            {
                _allLevels = value;
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

        List<Level> _levelListWithChildren = new List<Level>();

        public List<Level> LevelListWithChildren
        {
            get { return _levelListWithChildren; }
            set
            {
                _levelListWithChildren = value;
                RaisePropertyChanged();
            }
        }

        public List<List<Level>> SelectedListStack { get; set; } = new List<List<Level>>();

        #endregion

        public OverviewScreenViewModel()
        {
            AddDummyData();
            SetFirstListOfLevels();
            ItemTapped = new Command<Level>(HandleItemTapped);
            ShowAll = new Command(ShowAllCommand);
            ShowAllFlag = true;
            IsVisible = false;
        }

        private void ShowAllCommand(object obj)
        {
            var view = obj as MenuItem;
            ShowAllFlag = !ShowAllFlag;

            HandleMenuItemText();
            if (!ShowAllFlag)
            {
                ItemListSource = ((List<Level>)ItemListSource as List<Level>).Where(x => x.Completed == false).ToList();
            }
            else
            {
                ItemListSource = SelectedListStack.Last();
            }
        }

        private void HandleItemTapped(object obj)
        {
            if (obj is Level SelecedLevel)
            {
                FormattedTitlesStack.Add(SelecedLevel);
                NavigationHandler(SelecedLevel);
            }
            RaisePropertyChanged();
        }
        void HandleMenuItemText()
        {

            if (ShowAllFlag)
                ShowAllText = "Show UnCompleted";
            else
                ShowAllText = "Show All";

        }

        void NavigationHandler(Level SelecedLevel)
        {
            if (SelecedLevel.Children.Count() != 0)
            {
                HandleTitleSet(SelecedLevel);
                if (!ShowAllFlag)
                {
                    ItemListSource = SelecedLevel.Children.Where(x => x.Completed == false).ToList();
                }
                else
                {
                    ItemListSource = SelecedLevel.Children;
                }
                SelectedListStack.Add(ItemListSource as List<Level>);
            }
            else
                return;
        }
        public override void OnSoftBackButtonPressed()
        {
            if (SelectedListStack.Count() != 1)
            {
                SelectedListStack.Remove(SelectedListStack.Last());
                FormattedTitlesStack.Remove(FormattedTitlesStack.Last());
                if (FormattedTitlesStack.Count() != 0)
                    HandleTitleSet(FormattedTitlesStack.Last());
                else
                {
                    baseContentPage.FormattedTitle = "Site";
                }
                if (!ShowAllFlag)
                {
                    ItemListSource = SelectedListStack.Last().Where(x => x.Completed == false).ToList();
                }
                else
                    ItemListSource = SelectedListStack.Last();
            }
            else
            {
                Navigation.GoBack();
            }
        }
        private void HandleTitleSet(Level level)
        {
            var formattedTitle = new FormattedString();
            formattedTitle.Spans.Add
                (new Span
                {
                    Text = level.Name
                ,
                    FontAttributes = FontAttributes.Bold
                ,
                    FontSize = 20
                });
            baseContentPage.FormattedTitle = formattedTitle;
            Level newLevel = level;
            FormattedSubTitle = "";
            while (newLevel.Parent != null)
            {
                newLevel = newLevel.Parent;
                FormattedSubTitle += newLevel.Name;
            }
            baseContentPage.Subtitle = FormattedSubTitle;
        }
        public void GetFinishedPercentage(List<int> AllLevelTypes)
        {
            int types = AllLevelTypes.Count() - 2;
            for (int i = types; i >= 0; i--)
            {
                foreach (Level Parent in LevelListWithChildren.Where(x => x.LevelType == AllLevelTypes[i]))
                {

                    if (Parent.LevelType == AllLevelTypes[types])
                    {
                        //floor 
                        Parent.Finished = Parent.Children.Where(x => x.ControlStatus == 1 || x.ControlStatus == -1).Count();
                        Parent.ChildernCount = Parent.Children.Count();
                    }
                    else
                    {
                        foreach (Level childern in Parent.Children)
                        {
                            Parent.Finished += childern.Finished;
                            Parent.ChildernCount += childern.ChildernCount;
                        }
                    }
                }
            }

            foreach (Level level in LevelListWithChildren.Where(x => x.LevelType == AllLevelTypes.Last()))
            {
                level.ListViewModeValue = level.Area.ToString() + " m2";
                level.Segments = AddSegmentsListLastLevel(level.ControlStatus);
                if (level.ControlStatus == 1 || level.ControlStatus == -1) level.Completed = true;
            }
            foreach (Level level in LevelListWithChildren.Where(x => x.LevelType != AllLevelTypes.Last()))
            {
                level.ListViewModeValue = level.Finished.ToString() + "/" + level.ChildernCount;
                double Percentage = (double)level.Finished / level.ChildernCount;
                level.Segments = AddSegmentsList(Percentage * 360);
                if (Percentage == 1) level.Completed = true;
            }
        }

        IList<Segment> AddSegmentsList(double Angle)
        {
            IList<Segment> segments = new List<Segment>
            {
                new Segment { Color = PieChartColor, Radius = .8F, SweepAngle = (float)Angle },
                new Segment { Color = Color.LightGray, Radius = .8F, SweepAngle = 360 - (float)Angle }
            };
            return segments;
        }
        IList<Segment> AddSegmentsListLastLevel(int ControlStatus)
        {
            IList<Segment> segments = new List<Segment>
            {
                new Segment { Color = Color.LightGray, Radius = .8F, SweepAngle = 360 , ControlStatus = ControlStatus}
            };
            return segments;
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
            SelectedListStack.Add(Parents);
            SortChildren(AllLevelTypes, allLevels);
        }

        void SortChildren(List<int> AllLevelTypes, List<Level> allLevels)
        {
            for (int i = 0; i < AllLevelTypes.Count() - 1; i++)
            {
                List<Level> searchList = allLevels
                    .Where(x => x.LevelType == AllLevelTypes[i] || x.LevelType == AllLevelTypes[i + 1])
                    .ToList();
                foreach (Level level in searchList)
                {
                    if (i != 0)
                    {
                        level.Parent = allLevels
                         .Where(x => x.LevelType == AllLevelTypes[i - 1] && x.ID == level.OwnerId)
                         .FirstOrDefault();
                    }
                    level.Children = allLevels
                        .Where(x => x.OwnerId == level.ID)
                        .ToList();
                    LevelListWithChildren.Add(level);
                }
            }

            LevelListWithChildren = LevelListWithChildren.Distinct().ToList();

            int lastType = AllLevelTypes.Last();
            foreach (Level level in LevelListWithChildren.Where(x => x.LevelType == lastType))
            {
                level.Parent = LevelListWithChildren
                    .Where(x => x.LevelType == AllLevelTypes[AllLevelTypes.Count() - 2] && x.ID == level.OwnerId)
                    .FirstOrDefault();
            }

            GetFinishedPercentage(AllLevelTypes);

        }

        void AddDummyData()
        {
            List<Level> FakeList = new List<Level>
            {
                new Level { LevelType = 5, Area = 30.5, DoorNumber = "32", ID = 1, Name = "Hosptial Customer", UserId = 1 , ListViewMode="Completed :"},
                new Level { LevelType = 5, Area = 32.5, DoorNumber = "322", ID = 2, Name = "School Customer", UserId = 2, ListViewMode="Completed :" },

                new Level { LevelType = 8, Area = 30.5, DoorNumber = "32", ID = 10, Name = "Hosptial", OwnerId = 1 , ListViewMode="Completed :"},
                new Level { LevelType = 8, Area = 32.5, DoorNumber = "322", ID = 20, Name = "School building 1 ", OwnerId = 2, ListViewMode="Completed :" },
                new Level { LevelType = 8, Area = 33.5, DoorNumber = "321", ID = 30, Name = "school building 2 ", OwnerId = 2 , ListViewMode="Completed :"},

                new Level { LevelType = 11, Area = 30.5, DoorNumber = "32", ID = 100, Name = "Hosptial floor", OwnerId = 10, UserId = 1, ListViewMode="Completed :" },
                new Level { LevelType = 11, Area = 32.5, DoorNumber = "322", ID = 200, Name = "School Floor", OwnerId = 20, UserId = 2 , ListViewMode="Completed :"},
                new Level { LevelType = 11, Area = 33.5, DoorNumber = "321", ID = 300, Name = "School Floor 2", OwnerId = 30, UserId = 3 , ListViewMode="Completed :"},

                new Level { LevelType = 14, Area = 30.5, DoorNumber = "32", ID = 1000, Name = "Hosptial Room 1", OwnerId = 100, UserId = 1  , ControlStatus = 0 , ListViewMode="Area :"} ,
                new Level { LevelType = 14, Area = 32.5, DoorNumber = "322", ID = 2000, Name = "School Rooms1 1", OwnerId = 200, UserId = 2 , ControlStatus = 0, ListViewMode="Area :"},
                new Level { LevelType = 14, Area = 33.5, DoorNumber = "321", ID = 3000, Name = "School Rooms2 1 ", OwnerId = 300, UserId = 3 , ControlStatus = 1, ListViewMode="Area :"},

                new Level { LevelType = 14, Area = 30.5, DoorNumber = "32", ID = 10001, Name = "Hosptial Room 2", OwnerId = 100, UserId = 1 , ControlStatus = -1, ListViewMode="Area :"},
                new Level { LevelType = 14, Area = 32.5, DoorNumber = "322", ID = 20001, Name = "School Rooms1 2", OwnerId = 200, UserId = 2 , ControlStatus = 1, ListViewMode="Area :"},
                new Level { LevelType = 14, Area = 33.5, DoorNumber = "321", ID = 30001, Name = "School Rooms2 2", OwnerId = 300, UserId = 3 , ControlStatus = 1, ListViewMode="Area :"},

                new Level { LevelType = 14, Area = 30.5, DoorNumber = "32", ID = 432, Name = "sub Hosptial Room 2", OwnerId = 100, UserId = 1 , ControlStatus = 0, ListViewMode="Area :"},
                new Level { LevelType = 14, Area = 32.5, DoorNumber = "322", ID = 4321, Name = "sub School Rooms1 2", OwnerId = 200, UserId = 2 , ControlStatus = -1, ListViewMode="Area :"},
                new Level { LevelType = 14, Area = 33.5, DoorNumber = "321", ID = 33201, Name = "sub School Rooms2 2", OwnerId = 300, UserId = 3 , ControlStatus = 1, ListViewMode="Area :"}
            };
            AllLevels = FakeList;
        }

    }
}