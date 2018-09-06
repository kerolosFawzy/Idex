using IDEX.ViewModel;

namespace IDEX
{
    public partial class IdexMainPage 
    {
        public IdexMainPage(){
            InitializeComponent();            
            BindingContext =  MainPageViewModel.Instance;
        }
    }
}