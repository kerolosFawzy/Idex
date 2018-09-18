using IDEX.Model;
using Microsoft.AppCenter.Crashes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace IDEX.ViewModel
{
    class AdditionalRequirementsViewModel : BaseViewModel
    {
        public ReactiveCommand GetDataCommand { get; set; }
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
        #region 
        private List<AdditionalRequirementsCheckBox> _checkBoxList = new List<AdditionalRequirementsCheckBox>();

        public List<AdditionalRequirementsCheckBox> CheckBoxList
        {
            get => _checkBoxList;
            set
            {
                this.RaiseAndSetIfChanged(ref _checkBoxList, value);
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

        public IList<string> RadioButtonItemsList { get; set; } = new List<string>();

        private string _selectedItem;
        public string SelectedItem
        {
            get => _selectedItem;
            set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
        }

        private bool _largeRangeValidation;

        public bool LargeRangeValidation
        {
            get => _largeRangeValidation;
            set
            {
                this.RaiseAndSetIfChanged(ref _largeRangeValidation, value);
                if (LargeRangeValidation) {
                    FliteringIncomeData();
                }
            }
        }

        private string _enteredRangeData;

        public string EnteredRangeData
        {
            get => _enteredRangeData;
            set
            {
                this.RaiseAndSetIfChanged(ref _enteredRangeData, value);
            }
        }
        private List<string> _rangeList;

        public List<string> RangeList
        {
            get => _rangeList;
            set
            {
                this.RaiseAndSetIfChanged(ref _rangeList, value);
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

        #endregion
        public AdditionalRequirementsViewModel()
        {
            SetDummyData();
            SetTitle();
            

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
        }

        void FliteringIncomeData() {
            string[] splitInput = EnteredRangeData.ToString().Split('.');
            if (int.TryParse(splitInput[0], out int min) 
                && int.TryParse(splitInput[1], out int max))
            {
                int range = max - min;
                if (range <= 10)
                {
                    for (int i = 0; i < range; i++)
                        RangeList.Add(i.ToString());
                    PickerVisible = true;
                }
                else
                {

                }
            }
        }

        public override void OnSoftBackButtonPressed()
        {
            Navigation.GoBack();
        }

        public override void DisAppearing()
        {
            RequirementsData.SelectedItem = SelectedItem;
            RequirementsData.checkBoxesList = CheckBoxList.Where(x => x.IsChecked == true).ToList();
            base.DisAppearing();
        }
    }

}
