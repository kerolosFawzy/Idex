using IDEX.Model;
using Microsoft.AppCenter.Crashes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IDEX.ViewModel
{
    class HygieneScreenViewModel : BaseViewModel
    {
        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set => this.RaiseAndSetIfChanged(ref _isVisible, value);
        }
        private List<int> _numberPicker = new List<int>();
        public List<int> NumberPicker
        {
            get => _numberPicker;
            set => this.RaiseAndSetIfChanged(ref _numberPicker, value);
        }

        private ReactiveList<string> _categoryList = new ReactiveList<string>();
        public ReactiveList<string> CategoryList
        {
            get => _categoryList;
            set => this.RaiseAndSetIfChanged(ref _categoryList, value);
        }

        public Level SelectedLevel { get; set; } = OverviewScreenViewModel.SelectedRoom;
        public HygieneScreenViewModel()
        {
            SetDummyData();

        }
        public override void OnSoftBackButtonPressed()
        {
            Navigation.GoBack();
        }
        private async Task SetTitle()
        {
            try
            {

                FormattedString Title = new FormattedString();
                Title.Spans.Add(new Span()
                {
                    Text = "Hygiene",
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
        private void SetDummyData()
        {
            for (int i = 1; i <= 4; i++)
                CategoryList.Add("PATIENTLIGHT" + i);
            for (int i = 0; i <= 30; i++)
                NumberPicker.Add(i);

            IsVisible = true; 
        }
    }
}
