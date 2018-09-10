using IDEX.Model;
using Microsoft.AppCenter.Crashes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IDEX.ViewModel
{
    class InstaPageViewModel : BaseViewModel
    {
        private Dictionary<string, string> _buttonData = new Dictionary<string, string>();
        public Level SelectedLevel { get; set; } = OverviewScreenViewModel.SelectedRoom;
        public ICommand ItemClicked { get; set; }

        public Dictionary<string, string> ButtonData
        {
            get => _buttonData;
            set
            {
                try
                {
                    this.RaiseAndSetIfChanged(ref _buttonData, value);
                    if (ButtonData != null)
                        SetInstaData();
                }
                catch (Exception exception) { Crashes.TrackError(exception); }

            }
        }
      

        private List<int> _numberPicker = new List<int>();
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

        public List<int> NumberPicker
        {
            get => _numberPicker;
            set => this.RaiseAndSetIfChanged(ref _numberPicker, value);
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
                    int.TryParse(s, out int ButtonCount);
                    Selected = ButtonCount;
                    PublicInstancePropertiesEqual(data, InstasResults, ButtonCount);
                }
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
                        InstasResults.GetType().GetProperty(pi.Name).SetValue(InstasResults, value);
                        return;
                    }

                }
            }
        }

        public InstaPageViewModel()
        {
            ItemClicked = new Command(ItemeClilckHandler);
            IsVisible = false;
            for (int i = 0; i <= 30; i++)
            {

                NumberPicker.Add(i);
            }

        }

        private void ItemeClilckHandler(object sender)
        {
            if (!IsVisible)
                IsVisible = true;
        }

        private async Task SetTitle()
        {
            try
            {

                FormattedString Title = new FormattedString();
                Title.Spans.Add(new Span()
                {
                    Text = "INSTA 800",
                    FontSize = 18
                });

                baseContentPage.FormattedTitle = Title;
                baseContentPage.Subtitle = SelectedLevel.DoorNumber + " ," + SelectedLevel.Name;
            }
            catch (Exception exception) { Crashes.TrackError(exception); }

        }

        public override void OnAppearing()
        {
            Task.Run(async () =>
            {
                await SetTitle();
            });
            base.OnAppearing();

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
