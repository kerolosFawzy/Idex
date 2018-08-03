using GalaSoft.MvvmLight;

namespace IDEX.Model
{
    public class BaseModel : ObservableObject
    { 
        public int ID { get; set; }
        public string Name { get; set; }

        private bool isChecked;

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                RaisePropertyChanged();
            }
        }
    }
}
