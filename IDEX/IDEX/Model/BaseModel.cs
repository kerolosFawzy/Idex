using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace IDEX.Model
{
    public class BaseModel : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string Name { get; set; }

        private bool isChecked;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                NotifyPropertyChanged();
            }
        }
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
