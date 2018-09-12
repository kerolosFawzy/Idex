using IDEX.Model;
using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace IDEX.ViewModel
{
    class AdditionalRequirementsViewModel : BaseViewModel
    {
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

        public IList<string> RadioButtonItemsList { get; set; } = new List<string>() ;

        private string _selectedItem;
        public string SelectedItem
        {
            get => _selectedItem;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedItem, value);
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
