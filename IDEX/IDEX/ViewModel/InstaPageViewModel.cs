using System;
using System.Collections.Generic;
using System.Text;

namespace IDEX.ViewModel
{
    public class InstaPageViewModel : BaseViewModel
    {

        public InstaPageViewModel() { }
        public string Easy { get; set; } = "Easy";
        public string Hard { get; set; } = "Hard";
        public override void OnSoftBackButtonPressed()
        {
            Navigation.GoBack();
        }

    }
}
