using IDEX.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace IDEX.ViewModel
{
    class SchemeViewModel : BaseViewModel
    {
        public ObservableCollection<Scheme> Schemes { set; get; }
            = new ObservableCollection<Scheme>();

        public List<Scheme> SelectedSchemes { set; get; }
            = new List<Scheme>();



        public SchemeViewModel()
        {
            
            Scheme mSchemes;
            mSchemes = new Scheme { ID = 1, CustomerId = 1 , Name = "Hospital scheme"  };
            Schemes.Add(mSchemes);

            Scheme mSchemes2;
            mSchemes2 = new Scheme { ID = 2, CustomerId = 2, Name = "School scheme" };
            Schemes.Add(mSchemes2);

            Scheme mSchemes3;
            mSchemes3 = new Scheme { ID = 3, CustomerId = 3, Name = "University scheme" };
            Schemes.Add(mSchemes3);
        }
    }
}
