using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace IDEX.ViewModel
{
    public class InstaPageViewModel : BaseViewModel
    {
        private Dictionary<string , string> _objectData = new Dictionary<string, string>();

        public Dictionary<string , string> ObjectData
        {
            get { return _objectData; }
            set { _objectData = value; }
        }

        public ICommand Show { get; set; }

        public InstaPageViewModel() {
            Show = new Command(HandleShow);
        }

        private void HandleShow(object obj)
        {
            string s = ObjectData["State"].ToString(); 
            UserDialogs.Instance.AlertAsync(s + " " + ObjectData["InstaRoomEnum"].ToString(), "Info ", "ok");
        }

        public string Easy { get; set; } = "Easy";
        public string Hard { get; set; } = "Hard";
        public override void OnSoftBackButtonPressed()
        {
            Navigation.GoBack();
        }

    }
}
