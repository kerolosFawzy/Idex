﻿using CustomControls;
using IDEX.Model;
using IDEX.Views;
using ReactiveUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Windows.Input;
using Xamarin.Forms;

namespace IDEX.ViewModel
{
    public class OverviewScreenViewModel : BaseViewModel
    {
        public ReactiveCommand<Unit , Unit> ShowAll { get; set; }
        public ICommand ItemTapped { get; set; }
        readonly Color PieChartColor = Color.FromHex("#008080");


        public OverviewScreenViewModel(IScreen hostScreen = null) : base(hostScreen)
        {
            AddDummyData();
            SetFirstListOfLevels();
            SetFirstTitle();
            ItemTapped = new Command<Level>(HandleItemTapped);
            ShowAll = ReactiveCommand.Create<Unit, Unit>(ShowAllCommand);
            ShowAllFlag = true;
        }

        
        public static Level SelectedRoom { get; set; }

        #region class propfull(s)
        public bool ShowAllFlag { get; set; }
        private string _formattedTitle;

        public string FormattedTitle
        {
            get => _formattedTitle;
            set => this.RaiseAndSetIfChanged(ref _formattedTitle, value);
        }
        private string _showAllText = "Show UnCompleted";

        public string ShowAllText
        {
            get => _showAllText;
            set => this.RaiseAndSetIfChanged(ref _showAllText, value);
        }

        private string _subtitle;
        public string Subtitle
        {
            get => _subtitle;
            set => this.RaiseAndSetIfChanged(ref _subtitle, value);
        }

        private bool _subtitleVisible=false;

        public bool SubtitleVisible
        {
            get => _subtitleVisible;
            set => this.RaiseAndSetIfChanged(ref _subtitleVisible, value);
        }


        private bool _isVisible = false;

        public bool IsVisible
        {
            get => _isVisible;
            set => this.RaiseAndSetIfChanged(ref _isVisible, value);
        }

        private bool _listViewIsVisible = true;

        public bool ListViewIsVisible
        {
            get => _listViewIsVisible;
            set => this.RaiseAndSetIfChanged(ref _listViewIsVisible, value);
        }

        private string _formattedSubTitle;
        public string FormattedSubTitle
        {
            get => _formattedSubTitle;
            set => this.RaiseAndSetIfChanged(ref _formattedSubTitle, value);
        }
        #endregion

        #region ALL Lists Init

        private List<Level> _formattedTitlesStack = new List<Level>();

        public List<Level> FormattedTitlesStack
        {
            get => _formattedTitlesStack;
            set => this.RaiseAndSetIfChanged(ref _formattedTitlesStack, value);
        }


        private IEnumerable _allLevels = Enumerable.Empty<Level>();

        public IEnumerable AllLevels
        {
            get => _allLevels;
            set => this.RaiseAndSetIfChanged(ref _allLevels, value);
        }

        private IEnumerable _itemListSource = Enumerable.Empty<Level>();
        public IEnumerable ItemListSource
        {
            get => _itemListSource;
            set => this.RaiseAndSetIfChanged(ref _itemListSource, value);
        }

        List<Level> _levelListWithChildren = new List<Level>();

        public List<Level> LevelListWithChildren
        {
            get => _levelListWithChildren;
            set => this.RaiseAndSetIfChanged(ref _levelListWithChildren, value);
        }

        public List<List<Level>> SelectedListStack { get; set; } = new List<List<Level>>();

        #endregion

        #region Command handle 
        private void HandleItemTapped(object obj)
        {
            if (obj is Level SelecedLevel)
            {
                NavigationHandler(SelecedLevel);
            }
        }

        private Unit ShowAllCommand(Unit arg)
        {
            //    var view = obj as MenuItem;
            ShowAllFlag = !ShowAllFlag;

            HandleMenuItemText();
            if (!ShowAllFlag)
            {
                ItemListSource = ((List<Level>)ItemListSource as List<Level>).Where(x => x.Completed == false).ToList();
                SetVisiablity();
            }
            else
            {
                ItemListSource = SelectedListStack.Last();
                SetVisiablity();
            }
            return new Unit();
        }


        private void ShowAllCommand()
        {
        
        }
        #endregion

        #region View handeling 
        void SetVisiablity()
        {
            if ((ItemListSource as List<Level>).Count() == 0)
            {
                IsVisible = true;
                ListViewIsVisible = false;
            }
            else
            {
                IsVisible = false;
                ListViewIsVisible = true;
            }
        }
        private void HandleTitleSet(Level level)
        {
            //var formattedTitle = new FormattedString();
            //formattedTitle.Spans.Add
            //    (new Span
            //    {
            //        Text = level.Name
            //    ,
            //        FontAttributes = FontAttributes.Bold
            //    ,
            //        FontSize = 20
            //    ,
            //        TextColor = Color.White
            //    });
            ////FormattedTitle = formattedTitle;
            FormattedTitle = level.Name;
            Level newLevel = level;
            FormattedSubTitle = "";
            while (newLevel.Parent != null)
            {
                newLevel = newLevel.Parent;
                FormattedSubTitle += newLevel.Name + " ,";
            }
            if (!string.IsNullOrEmpty(FormattedSubTitle))
                FormattedSubTitle = FormattedSubTitle.Remove(FormattedSubTitle.Length - 1);
            Subtitle = FormattedSubTitle;

            if (string.IsNullOrEmpty(Subtitle))
                SubtitleVisible = false;
            else
                SubtitleVisible = true;
        }
        void HandleMenuItemText()
        {

            if (ShowAllFlag)
                ShowAllText = "Show UnCompleted";
            else
                ShowAllText = "Show All";

        }
        #endregion 

        void NavigationHandler(Level SelecedLevel)
        {
            if (SelecedLevel.Children.Count() != 0)
            {
                FormattedTitlesStack.Add(SelecedLevel);
                HandleTitleSet(SelecedLevel);
                if (!ShowAllFlag)
                {
                    ItemListSource = SelecedLevel.Children.Where(x => x.Completed == false).ToList();
                }
                else
                {
                    ItemListSource = SelecedLevel.Children;
                }
                SetVisiablity();

                SelectedListStack.Add(ItemListSource as List<Level>);
            }
            else
            {
                SelectedRoom = SelecedLevel;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.NavigateAsync(nameof(RoomDetailsScreen));
                });
            }
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
                    SetFirstTitle();
                }
                if (!ShowAllFlag)
                {
                    ItemListSource = SelectedListStack.Last().Where(x => x.Completed == false).ToList();
                }
                else
                    ItemListSource = SelectedListStack.Last();
                SetVisiablity();

            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.GoBack();
                });

            }
        }
        void SetFirstTitle()
        {
            //var formattedTitle = new FormattedString();
            //formattedTitle.Spans.Add(new Span
            //{
            //    Text = "Site",
            //    FontSize = 20,
            //    TextColor = Color.White,
            //    FontAttributes = FontAttributes.Bold
            //});
            FormattedTitle = "Site";
        }

        #region setting level data 
        public void GetFinishedPercentage(List<int> AllLevelTypes)
        {
            //this code get first floor levels (level before last level) and get finished rooms out of it 
            //then set how many childern in this level and how many finsished (ControlStatus = 1 or ControlStatus = -1)
            
            int types = AllLevelTypes.Count() - 2;
            for (int i = types; i >= 0; i--)
            {
                foreach (Level Parent in LevelListWithChildren.Where(x => x.LevelType == AllLevelTypes[i]))
                {

                    if (Parent.LevelType == AllLevelTypes[types])
                    {
                        //this sorting depend on ControlStatus so i check if he in floor or not 

                        //floor 
                        Parent.Finished = Parent.Children.Where(x => x.ControlStatus == 1 || x.ControlStatus == -1).Count();
                        Parent.ChildernCount = Parent.Children.Count();
                    }
                    else
                    {
                        //here sorting depond on Finished and ChildernCount
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
                //this to set binding label depond on level type and data 
                level.ListViewModeValue = level.Area.ToString() + " m2";
                //this code to set segments list to draw svg images or empty circle 
                level.Segments = AddSegmentsListLastLevel(level.ControlStatus);
                if (level.ControlStatus == 1 || level.ControlStatus == -1) level.Completed = true;
            }
            foreach (Level level in LevelListWithChildren.Where(x => x.LevelType != AllLevelTypes.Last()))
            {
                level.ListViewModeValue = level.Finished.ToString() + "/" + level.ChildernCount;
                //this get percentage to convert it to angle for dawing 
                double Percentage = (double)level.Finished / level.ChildernCount;
                level.Segments = AddSegmentsList(Percentage * 360);
                if (Percentage == 1) level.Completed = true;
            }
        }

        IList<Segment> AddSegmentsList(double Angle)
        {
            //frist set segment that you need . 
            //set color and angle as float then set last segment as 360 - total used angle   
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
            //get all levels types and sort it 
            List<int> AllLevelTypes = allLevels.Select(x => x.LevelType).Distinct().ToList();
            AllLevelTypes.Sort();
            //get first levels depend on LevelTypes first type means this level have no parent 
            List<Level> Parents = allLevels
                .Where(x => x.LevelType == AllLevelTypes.FirstOrDefault()).ToList();

            ItemListSource = Parents;
            //put it in stack that used in navigate back 
            SelectedListStack.Add(Parents);

            SortChildren(AllLevelTypes, allLevels);

            allLevels = null;
            Parents = null;
          
        }
        void SortChildren(List<int> AllLevelTypes, List<Level> allLevels)
        {
            //this code to sort all levels expect last level (Room level)
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
                searchList = null;
            }

            LevelListWithChildren = LevelListWithChildren.Distinct().ToList();
            //this level have no childern so it only gets parent level 
            int lastType = AllLevelTypes.Last();
            foreach (Level level in LevelListWithChildren.Where(x => x.LevelType == lastType))
            {
                level.Parent = LevelListWithChildren
                    .Where(x => x.LevelType == AllLevelTypes[AllLevelTypes.Count() - 2] && x.ID == level.OwnerId)
                    .FirstOrDefault();
            }
            GetFinishedPercentage(AllLevelTypes);
            allLevels = null;
        }
        #endregion
        //put the real data instead @FakeList and he will do the reset 
        //it will start sorting parent level first and set it as ItemSource 
        //then he will start sorting all levels and set childern 
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

                new Level { LevelType = 14, Area = 30.5, DoorNumber = "32", ID = 1000, Name = "Hosptial Room 1", OwnerId = 100, UserId = 1  , ControlStatus =1 , ListViewMode="Area :"} ,
                new Level { LevelType = 14, Area = 32.5, DoorNumber = "322", ID = 2000, Name = "School Rooms1 1", OwnerId = 200, UserId = 2 , ControlStatus = 0, ListViewMode="Area :"},
                new Level { LevelType = 14, Area = 33.5, DoorNumber = "321", ID = 3000, Name = "School Rooms2 1 ", OwnerId = 300, UserId = 3 , ControlStatus = 1, ListViewMode="Area :"},

                new Level { LevelType = 14, Area = 30.5, DoorNumber = "32", ID = 10001, Name = "Hosptial Room 2", OwnerId = 100, UserId = 1 , ControlStatus = -1, ListViewMode="Area :"},
                new Level { LevelType = 14, Area = 32.5, DoorNumber = "322", ID = 20001, Name = "School Rooms1 2", OwnerId = 200, UserId = 2 , ControlStatus = 1, ListViewMode="Area :"},
                new Level { LevelType = 14, Area = 33.5, DoorNumber = "321", ID = 30001, Name = "School Rooms2 2", OwnerId = 300, UserId = 3 , ControlStatus = 1, ListViewMode="Area :"},

                new Level { LevelType = 14, Area = 30.5, DoorNumber = "32", ID = 432, Name = "sub Hosptial Room 2", OwnerId = 100, UserId = 1 , ControlStatus = 1, ListViewMode="Area :"},
                new Level { LevelType = 14, Area = 32.5, DoorNumber = "322", ID = 4321, Name = "sub School Rooms1 2", OwnerId = 200, UserId = 2 , ControlStatus = -1, ListViewMode="Area :"},
                new Level { LevelType = 14, Area = 33.5, DoorNumber = "321", ID = 33201, Name = "sub School Rooms2 2", OwnerId = 300, UserId = 3 , ControlStatus = 1, ListViewMode="Area :"}
            };
            AllLevels = FakeList;
            FakeList = null;
        }

    }
}