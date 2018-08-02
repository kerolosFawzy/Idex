using IDEX.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace IDEX.ViewModel
{
    class MainPageViewModel : BaseViewModel
    {
        #region //three models List Defination 
        private List<Customer> _customers = new List<Customer>();
        public List<Customer> Customers
        {
            get { return _customers; }
            set { _customers = value;
                RaisePropertyChanged();
            }
        }

        private List<Scheme> _Schemes = new List<Scheme>();

        public List<Scheme> Schemes
        {
            get { return _Schemes; }
            set { _Schemes  = value;
                RaisePropertyChanged();
            }
        }

        private List<Inspection> _inspections = new List<Inspection>();

        public List<Inspection> Inspections
        {
            get { return _inspections = new List<Inspection>(); }
            set { _inspections = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region // init Selected Lists 

        private List<Customer> _selectCustomers;
        public List<Customer> SelectedCustomer
        {
            get { return _selectCustomers; }
            set
            {
                _selectCustomers = value;
                RaisePropertyChanged();
            }
        }

        private List<Scheme> _schemeBindingList = new List<Scheme>();
        public List<Scheme> SchemeBindingList
        {
            get { return _schemeBindingList; }
            set { _schemeBindingList = value;
                RaisePropertyChanged();
            }
        }

        private List<Scheme> _selectedSchemes = new List<Scheme>();
        public List<Scheme> SelectedSchemes
        {
            get { return _selectedSchemes; }
            set { _selectedSchemes = value;
                RaisePropertyChanged();
            }
        }

        private List<Inspection> _insepctionBindingList = new List<Inspection>();
        public List<Inspection> InsepctionBindingList
        {
            get { return _insepctionBindingList; }
            set
            {
                _insepctionBindingList = value;
                RaisePropertyChanged();
            }
        }

        private List<Inspection> _selectedInsepction = new List<Inspection>();
        public List<Inspection> SelectedInsepction
        {
            get { return _selectedInsepction; }
            set { _selectedInsepction = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        public MainPageViewModel()
        {
            AddDummyData();
        }

        private void AddDummyData() {
             
            Customers.Add(new Customer { ID = 1, Name = "Hospital" });
            Customers.Add(new Customer { ID = 2, Name = "School" });
            Customers.Add(new Customer { ID = 3, Name = "University" });

            Schemes.Add(new Scheme { ID = 1, CustomerId = 1, Name = "Hospital scheme" });
            Schemes.Add(new Scheme { ID = 2, CustomerId = 2, Name = "School scheme" });
            Schemes.Add(new Scheme { ID = 3, CustomerId = 3, Name = "University scheme" });

            Inspections.Add(new Inspection { ID = 1, Name = "Hospital Inspection File", SchemeId = 1 });
            Inspections.Add(new Inspection { ID = 2, Name = "School Inspection File", SchemeId = 2 });
            Inspections.Add(new Inspection { ID = 3, Name = "University Inspection File", SchemeId = 3 });
        }

    }
}
