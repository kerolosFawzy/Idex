﻿
using ReactiveUI;

namespace IDEX.Model
{
    //this model for test delete it when there are real data 
    public class AdditionalRequirementsCheckBox : BaseModel
    {
        public string  Title { get; set; }
        private bool _isChecked;

        public bool IsChecked
        {
            get => _isChecked;
            set => this.RaiseAndSetIfChanged(ref _isChecked, value);
        }
    }
}
