using CustomController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace IDEX.Model
{
    class Customer : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Scheme> Schemes { get; set; }

        private bool isChecked;

        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
