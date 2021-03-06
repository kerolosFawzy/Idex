﻿using IDEX.Model;
using Microsoft.AppCenter.Crashes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace IDEX.ViewModel
{
    public class InstaPageViewModel : BaseViewModel
    {
        public Level SelectedLevel { get; set; } = OverviewScreenViewModel.SelectedRoom;
        public ICommand ItemClicked { get; set; }

        //i using this to bind easy and hard to the view 
        public string Easy { get; set; } = "Easy";
        public string Hard { get; set; } = "Hard";


        #region Title And SubTitle
        private string _subtitle;

        public string Subtitle
        {
            get { return _subtitle; }
            set { _subtitle = value; }
        }
        private FormattedString _FormattedTitle;

        public FormattedString FormattedTitle
        {
            get { return _FormattedTitle; }
            set { _FormattedTitle = value; }
        }
        private void SetTitle()
        {
            try
            {
                FormattedString Title = new FormattedString();
                Title.Spans.Add(new Span()
                {
                    Text = "INSTA 800",
                    FontSize = 18
                });

                FormattedTitle = Title;
                Subtitle = SelectedLevel.DoorNumber + " ," + SelectedLevel.Name;
            }
            catch (Exception exception) { Crashes.TrackError(exception); }
        }
        #endregion

        private Dictionary<string, string> _buttonData = new Dictionary<string, string>();
        public Dictionary<string, string> ButtonData
        {
            get => _buttonData;
            set
            {
                try
                {
                    this.RaiseAndSetIfChanged(ref _buttonData, value);
                    //fire only if there is data coming from view 
                    if (ButtonData != null)
                        SetInstaData();
                }
                catch (Exception exception) { Crashes.TrackError(exception); }

            }
        }

        #region Lists 
        private List<int> _soilingList = new List<int>();

        public List<int> SoilingList
        {
            get => _soilingList;
            set => this.RaiseAndSetIfChanged(ref _soilingList, value);
        }
        private List<int> _normalList = new List<int>();

        public List<int> NormalList
        {
            get => _normalList;
            set => this.RaiseAndSetIfChanged(ref _normalList, value);
        }

        private List<int> _numberPicker = new List<int>();
        public List<int> NumberPicker
        {
            get => _numberPicker;
            set => this.RaiseAndSetIfChanged(ref _numberPicker, value);
        }

        #endregion

        #region View binding prop 
        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set => this.RaiseAndSetIfChanged(ref _isVisible, value);
        }

        private int _selected;
        public int Selected
        {
            get => _selected;
            set => this.RaiseAndSetIfChanged(ref _selected, value);
        }

       
        private string _instaCategoryEnum;
        public string InstaCategoryEnum
        {
            get => _instaCategoryEnum;
            set => this.RaiseAndSetIfChanged(ref _instaCategoryEnum, value);
        }

        private string _state;
        public string State
        {
            get => _state;
            set => this.RaiseAndSetIfChanged(ref _state, value);
        }

        private string _instaRoomEnum;
        public string InstaRoomEnum
        {
            get => _instaRoomEnum;
            set => this.RaiseAndSetIfChanged(ref _instaRoomEnum, value);
        }

        #endregion
        public static Insta800InseptionResult InstasResults
        { get; set; } = new Insta800InseptionResult();

        private void SetInstaData()
        {
            try
            {
                string data;
                if (ButtonData.Count != 0)
                {
                    data = ButtonData["State"];
                    State = ButtonData["State"];
                    data += ButtonData["InstaCategoryEnum"];
                    InstaCategoryEnum = ButtonData["InstaCategoryEnum"];
                    data += ButtonData["InstaRoomEnum"];
                    if (InstaCategoryEnum.Equals("SurfaceSoilings"))
                    {
                        if (NumberPicker != SoilingList)
                            NumberPicker = SoilingList;
                    }
                    else
                    {
                        if (NumberPicker != NormalList)
                            NumberPicker = NormalList;
                    }

                    InstaRoomEnum = ButtonData["InstaRoomEnum"];
                    string s = ButtonData["Count"];
                    int.TryParse(s, out int ButtonCount);
                    if (InstaCategoryEnum.Equals("SurfaceSoilings"))
                    {
                        Selected = ButtonCount / 5;
                    }
                    else
                        Selected = ButtonCount;
                    PublicInstancePropertiesEqual(data, InstasResults, ButtonCount);
                }
            }
            catch (Exception exception) { Crashes.TrackError(exception); }
        }

        //method to search in modle and set value 
        public static void PublicInstancePropertiesEqual<T>(string self, T to, int value) where T : class
        {
            if (self != null && to != null)
            {
                Type type = typeof(T);
                foreach (System.Reflection.PropertyInfo pi
                    in type.GetProperties(System.Reflection.BindingFlags.Public
                    | System.Reflection.BindingFlags.Instance))
                {
                    object selfValue = self;
                    object toValue = type.GetProperty(pi.Name);
                    string s = toValue.ToString().Replace("Int32 ", "");
                    if (selfValue.ToString().Equals(s))
                    {
                        InstasResults.GetType().GetProperty(pi.Name).SetValue(InstasResults, value);
                        return;
                    }

                }
            }
        }

        public InstaPageViewModel(IScreen hostScreen = null) : base(hostScreen)
        {
            ItemClicked = new Command(ItemeClilckHandler);
            IsVisible = false;
            SetTitle();
            for (int i = 0; i <= 100; i++)
            {
                NormalList.Add(i);
            }
            for (int x = 0; x <= 100; x = x + 5)
                SoilingList.Add(x);

        }

        private void ItemeClilckHandler(object sender)
        {
            if (!IsVisible)
                IsVisible = true;
        }

        public override void OnSoftBackButtonPressed()
        {
            Navigation.GoBack();
        }

        public override void DisAppearing()
        {
            OverviewScreenViewModel.SelectedRoom.insta800InseptionResult = InstasResults;
            base.DisAppearing();
        }
    }
}
