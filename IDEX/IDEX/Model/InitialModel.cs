﻿using ReactiveUI;

namespace IDEX.Model
{
    public class InitialModel : BaseModel 
    {
        public int ID { get; set; }
        public string Name { get; set; }

        private bool _isChecked;

        public bool IsChecked
        {
            get => _isChecked;
            set => this.RaiseAndSetIfChanged(ref _isChecked, value);
        }
    }
}
