using IDEX.Model;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IDEX.ViewModel
{
    class AdditionalRequirementsViewModel : BaseViewModel
    {
        public Level SelectedLevel { get; set; } = OverviewScreenViewModel.SelectedRoom;
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

        public AdditionalRequirementsViewModel()
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

        
        
        public override void OnSoftBackButtonPressed()
        {
            Navigation.GoBack();
        }
    }
}
