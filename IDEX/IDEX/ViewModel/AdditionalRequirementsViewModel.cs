using IDEX.Model;
using IDEX.Views;
using Microsoft.AppCenter.Crashes;
using ReactiveUI;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace IDEX.ViewModel
{
    class AdditionalRequirementsViewModel : BaseViewModel
    {
        public ICommand ThreeDotButtonCommand { get; set; }
        public ICommand OkButtonCommand { get; set; }
        public Level SelectedLevel { get; set; } = OverviewScreenViewModel.SelectedRoom;
        AdditionalRequirementsData RequirementsData { get; set; } = new AdditionalRequirementsData();

        #region title and subtitle
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
        #endregion

        #region test 
        private string _testMin;

        public string TestMin
        {

            get => _testMin;
            set
            {
                this.RaiseAndSetIfChanged(ref _testMin, value);
                Min = TestMin.ToString();
            }
        }
        private string _testMax;

        public string TestMax
        {

            get => _testMax;
            set
            {
                this.RaiseAndSetIfChanged(ref _testMax, value);
                Max = TestMax.ToString();
            }
        }

        #endregion

        #region class lists 
        private List<int> _popUpPickerList = new List<int>();

        public List<int> PopUpPickerList
        {
            get => _popUpPickerList;
            set
            {
                this.RaiseAndSetIfChanged(ref _popUpPickerList, value);
            }
        }
        private IList<string> _rangeList = new List<string>();

        public IList<string> RangeList
        {
            get => _rangeList;
            set
            {
                this.RaiseAndSetIfChanged(ref _rangeList, value);
            }
        }
        public IList<string> RadioButtonItemsList { get; set; } = new List<string>();

        private List<AdditionalRequirementsCheckBox> _checkBoxList = new List<AdditionalRequirementsCheckBox>();

        public List<AdditionalRequirementsCheckBox> CheckBoxList
        {
            get => _checkBoxList;
            set
            {
                this.RaiseAndSetIfChanged(ref _checkBoxList, value);
            }
        }

        #endregion

        #region
        private double _largeRangeMax = 100.5;

        public double LargeRangeMax
        {
            get => _largeRangeMax;
            set
            {
                this.RaiseAndSetIfChanged(ref _largeRangeMax, value);
            }
        }
        private double _largeRangeMin = 0.0;

        public double LargeRangeMin
        {
            get => _largeRangeMin;
            set
            {
                this.RaiseAndSetIfChanged(ref _largeRangeMin, value);
            }
        }

        private int _selectedDecimalNum;

        public int SelectedDecimalNum
        {

            get => _selectedDecimalNum;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedDecimalNum, value);
            }
        }
       

        private int _selectedIntegerNum;

        public int SelectedIntegerNum
        {

            get => _selectedIntegerNum;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedIntegerNum, value);
            }
        }

        
        private bool _pickerVisible = false;

        public bool PickerVisible
        {
            get => _pickerVisible;
            set
            {
                this.RaiseAndSetIfChanged(ref _pickerVisible, value);
            }
        }

        private bool _textBoxVisibility = false;

        public bool TextBoxVisibility
        {
            get => _textBoxVisibility;
            set
            {
                this.RaiseAndSetIfChanged(ref _textBoxVisibility, value);
            }
        }

        private string _selectedItem;
        public string SelectedItem
        {
            get => _selectedItem;
            set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
        }

        private int _pickerSelectedItem = 1;
        public int PickerSelectedItem
        {
            get => _pickerSelectedItem;
            set => this.RaiseAndSetIfChanged(ref _pickerSelectedItem, value);
        }

        private bool _largeRangeValidation;

        public bool LargeRangeValidation
        {
            get => _largeRangeValidation;
            set
            {
                this.RaiseAndSetIfChanged(ref _largeRangeValidation, value);
                if (LargeRangeValidation)
                {
                    double.TryParse(EnteredRangeData, out double mValue); 
                    RequirementsData.LargeRangeValue = mValue;
                }
            }
        }

        private bool _choosedValueValidation;

        public bool ChoosedValueValidation
        {
            get => _choosedValueValidation;
            set
            {
                this.RaiseAndSetIfChanged(ref _choosedValueValidation, value);
                if (ChoosedValueValidation)
                {
                    RequirementsData.SmallRangeValue = TextBox;
                }
            }
        }

        private string _enteredRangeData = "";

        public string EnteredRangeData
        {
            get => _enteredRangeData;
            set
            {
                this.RaiseAndSetIfChanged(ref _enteredRangeData, value);
            }
        }
       
        private string _textBox;

        public string TextBox
        {
            get => _textBox;
            set
            {
                this.RaiseAndSetIfChanged(ref _textBox, value);
            }
        }

        private string _max= "V";

        public string Max
        {
            get => _max;
            set
            {
                this.RaiseAndSetIfChanged(ref _max, value);
                SetSmallRageData();
            }
        }

        private string _min =  "A" ;

        public string Min
        {
            get => _min;
            set
            {
                this.RaiseAndSetIfChanged(ref _min, value);
                SetSmallRageData();

            }

        }

        #endregion

        private static Lazy<AdditionalRequirementsViewModel> _lazyAdditionalRequirementsViewModelInstance
            = new Lazy<AdditionalRequirementsViewModel>(() => new AdditionalRequirementsViewModel());

        public static AdditionalRequirementsViewModel Instance
        {
            get
            {
                return _lazyAdditionalRequirementsViewModelInstance.Value;
            }
        private set
            {
                _lazyAdditionalRequirementsViewModelInstance = new Lazy<AdditionalRequirementsViewModel>(() => value); 
            }
        }
        public AdditionalRequirementsViewModel()
        {
            SetDummyData();
            SetTitle();
            SetSmallRageData();
            ThreeDotButtonCommand = new Command(HandleThreeDotButton);
            OkButtonCommand = new Command(HandleOkButtonCommand);
        }

        private void HandleOkButtonCommand(object obj)
        {
            SetLargeRangeData();
            PopupNavigation.Instance.PopAsync();
        }

        private async void HandleThreeDotButton(object obj)
        {
            await PopupNavigation.Instance.PushAsync(new PopUpPickerView());
        }

        void SetTitle()
        {
            FormattedString Title = new FormattedString();
            Title.Spans.Add(new Span()
            {
                Text = "Additional Requirements",
                FontSize = 18
            });
            FormattedTitle = Title;
            Subtitle = SelectedLevel.DoorNumber + " ," + SelectedLevel.Name;
        }
        void SetDummyData()
        {
            for (int i = 0; i <= 5; i++)
            {
                RadioButtonItemsList.Add("Option " + i);
                CheckBoxList.Add(new AdditionalRequirementsCheckBox { Title = "Option " + i });
            }
            for (double i = LargeRangeMin; i <= LargeRangeMax; i++)
            {
                PopUpPickerList.Add((int)i);
            }
        }

       
        void SetSmallRageData()
        {
            List<string> result = new List<string>();
            if (string.IsNullOrEmpty(Max) || string.IsNullOrEmpty(Min))
                return;
            //this is if the input is number start check range 
            if (int.TryParse(Max, out int max) && int.TryParse(Min, out int min))
            {
                int range = max - min;
                if (range <= 10)
                {
                    PickerVisible = true;
                    TextBoxVisibility = false;
                    for (int i = min; i <= max; i++) { result.Add(i.ToString()); }
                    RangeList = result;
                }
                else
                {
                    PickerVisible = false;
                    TextBoxVisibility = true;
                }
            }
            else
            {
                //if the input is char start convert to decimal
                // and start checking for range 
                char firstChar = Min.ToCharArray()[0];
                int minChar = Convert.ToInt32(firstChar);
                char secondChar = Max.ToCharArray()[0];
                int maxChar = Convert.ToInt32(secondChar);

                int rangeChar = maxChar - minChar;
                if (rangeChar <= 10)
                {
                    char[] alphabet = Enumerable.Range(firstChar, rangeChar + 1)
                        .Select(x => (char)x).ToArray();
                    PickerVisible = true;
                    TextBoxVisibility = false;

                    foreach (char c in alphabet)
                    {
                        result.Add(c.ToString());
                    }
                    RangeList = result;
                }
                else
                {
                    PickerVisible = false;
                    TextBoxVisibility = true;
                }
            }
            result = null;
        }
        public override void OnSoftBackButtonPressed()
        {
            Navigation.GoBack();
        }
        void SetLargeRangeData()
        {
            //get data from popUp view and set it to entry 
            double.TryParse(SelectedIntegerNum.ToString() + "." + SelectedDecimalNum.ToString(), out double value);
            EnteredRangeData = string.Empty;
            EnteredRangeData = value.ToString();
        }
        public override void DisAppearing()
        {
            //i set new instance here to make sure that data will not be in other levels screen 
            RequirementsData.SelectedItem = SelectedItem;
            RequirementsData.checkBoxesList = CheckBoxList.Where(x => x.IsChecked == true).ToList();
            Instance = new AdditionalRequirementsViewModel();
            base.DisAppearing();
        }
    }

}
