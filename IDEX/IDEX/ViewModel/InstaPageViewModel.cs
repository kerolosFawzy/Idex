﻿using Acr.UserDialogs;
using IDEX.Model;
using Microsoft.AppCenter.Crashes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace IDEX.ViewModel
{
    class InstaPageViewModel : BaseViewModel
    {
        private Dictionary<string, string> _buttonData = new Dictionary<string, string>();
        public Level SelectedLevel { get; set; } = RoomDetailsScreenViewModel.SelectedLevel;
        public Dictionary<string, string> ButtonData
        {
            get => _buttonData;
            set
            {
                this.RaiseAndSetIfChanged(ref _buttonData, value);
                SetInstaData();
            }
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


        public static Insta800InseptionResult InstasResults
        { get; set; } = new Insta800InseptionResult();

        private void SetInstaData()
        {
            try
            {
                this.WhenAnyValue(x => x.ButtonData)
                .Where(x => x.Count() != 0)
                .Select(x => x)
                .Subscribe(x =>
                {
                    string data;
                    if (ButtonData.Count != 0)
                    {
                        data = ButtonData["State"];
                        State = ButtonData["State"];
                        data += ButtonData["InstaCategoryEnum"];
                        InstaCategoryEnum = ButtonData["InstaCategoryEnum"];
                        data += ButtonData["InstaRoomEnum"];
                        InstaRoomEnum = ButtonData["InstaRoomEnum"];
                        string s = ButtonData["Count"];
                        int.TryParse(s, out int value);
                        PublicInstancePropertiesEqual(data, InstasResults, value);
                    }
                });

            }
            catch (Exception exception) { Crashes.TrackError(exception); }

        }

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
                        try
                        {
                            InstasResults.GetType().GetProperty(pi.Name).SetValue(pi.Name, value);
                            return;
                        }
                        catch (Exception exception) { Crashes.TrackError(exception); }
                    }

                }
            }
        }

        public InstaPageViewModel()
        {
            try
            {
                FormattedString SubTitle = new FormattedString();
                SubTitle.Spans.Add(new Span()
                {
                    Text = SelectedLevel.DoorNumber + " ," + SelectedLevel.Name,
                    FontSize = 12
                });

                FormattedString Title = new FormattedString();
                Title.Spans.Add(new Span()
                {
                    Text = "INSTA 800",
                    FontSize = 18
                });
                baseContentPage.FormattedTitle = Title;
                baseContentPage.Subtitle = SubTitle.ToString();

            }
            catch (Exception exception) { Crashes.TrackError(exception); }

        }

        private void HandleShow(object obj)
        {
            string s = ButtonData["State"];
            UserDialogs.Instance.AlertAsync(s + " " + ButtonData["InstaRoomEnum"].ToString(), "Info ", "ok");
        }

        public string Easy { get; set; } = "Easy";
        public string Hard { get; set; } = "Hard";
        public override void OnSoftBackButtonPressed()
        {
            Navigation.GoBack();
        }
        public override void DisAppearing()
        {
            base.DisAppearing();
            SelectedLevel.insta800InseptionResult = InstasResults;
        }
    }
}