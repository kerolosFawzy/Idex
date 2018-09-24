using IDEX.Model;
using Microsoft.AppCenter.Crashes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace IDEX.ViewModel
{
    class HygieneScreenViewModel : BaseViewModel
    {
        private Dictionary<string, string> _buttonData = new Dictionary<string, string>();
        public Level SelectedLevel { get; set; } = OverviewScreenViewModel.SelectedRoom;
        public ICommand ItemClicked { get; set; }
        public static HygieneInsepectionResult InsepectionResult { get; set; }
            = new HygieneInsepectionResult();

        private List<HygieneInsepectionResult> _hygieneInsepectionList = new List<HygieneInsepectionResult>();

        public List<HygieneInsepectionResult> HygieneInsepectionList
        {
            get => _hygieneInsepectionList;
            set => this.RaiseAndSetIfChanged(ref _hygieneInsepectionList, value);
        }
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

        public Dictionary<string, string> ButtonData
        {
            get => _buttonData;
            set
            {
                try
                {
                    this.RaiseAndSetIfChanged(ref _buttonData, value);
                    if (ButtonData != null)
                        SetData();
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

        private string _cleaningCategory;
        public string CleaningCategory
        {
            get => _cleaningCategory;
            set => this.RaiseAndSetIfChanged(ref _cleaningCategory, value);
        }

        private string _state;
        public string State
        {
            get => _state;
            set => this.RaiseAndSetIfChanged(ref _state, value);
        }

        private string _category;
        public string Category
        {
            get => _category;
            set => this.RaiseAndSetIfChanged(ref _category, value);
        }


        private ReactiveList<string> _categoryList = new ReactiveList<string>();
        public ReactiveList<string> CategoryList
        {
            get => _categoryList;
            set => this.RaiseAndSetIfChanged(ref _categoryList, value);
        }

        public HygieneScreenViewModel()
        {
            SetDummyData();
            IsVisible = false;
            ItemClicked = new Command(ItemeClilckHandler);
            for (int i = 0; i < CategoryList.Count(); i++)
                HygieneInsepectionList.Add(new HygieneInsepectionResult {
                    CategoryName = CategoryList[i]
                });
            SetTitle();
        }

        public override void OnSoftBackButtonPressed()
        {
            Navigation.GoBack();
        }
        private void ItemeClilckHandler(object sender)
        {
            if (!IsVisible)
                IsVisible = true;
        }

        private void SetData()
        {
            try
            {
                string data = "";
                if (ButtonData.Count != 0)
                {
                    State = ButtonData["State"];
                    data = State;
                    Category = ButtonData["Category"];
                    CleaningCategory = ButtonData["CleaningCategory"];
                    data += CleaningCategory;
                    string s = ButtonData["Count"];
                    int.TryParse(s, out int ButtonCount);
                    Selected = ButtonCount;

                    data = data.Trim();
                    InsepectionResult = HygieneInsepectionList.Where(x => x.CategoryName.Equals(Category)).FirstOrDefault();
                    HygieneInsepectionList.Remove(InsepectionResult); 
                    PublicInstancePropertiesEqual(data, InsepectionResult, ButtonCount);
                    HygieneInsepectionList.Add(InsepectionResult);
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
                        InsepectionResult.GetType().GetProperty(pi.Name).SetValue(InsepectionResult, value);
                        return;
                    }

                }
            }
        }

        private void SetTitle()
        {
            try
            {
                FormattedString Title = new FormattedString();
                Title.Spans.Add(new Span()
                {
                    Text = "Hygiene",
                    FontSize = 18
                });

                FormattedTitle = Title;
                Subtitle = SelectedLevel.DoorNumber + " ," + SelectedLevel.Name;
            }
            catch (Exception exception) { Crashes.TrackError(exception); }
        }

        public override void DisAppearing()
        {
            OverviewScreenViewModel.SelectedRoom.HygieneInsepectionResults = HygieneInsepectionList; 
            base.DisAppearing();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
        }
        private void SetDummyData()
        {
            for (int i = 1; i <= 10; i++)
                CategoryList.Add("PATIENTLIGHT " + i);
            for (int i = 0; i <= 100; i++)
                NumberPicker.Add(i);
        }
    }
}
