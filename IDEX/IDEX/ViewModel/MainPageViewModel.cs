using IDEX.Model;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace IDEX.ViewModel
{
    class MainPageViewModel : BaseViewModel
    {
        private int flag;
        List<Customer> ts = new List<Customer>();
        Color SelectedButtonColor = Color.FromHex("#008080");
        Color UnSelectedButtonColor = Color.Transparent;

        public ICommand ItemSelected { get; set; }
        public ICommand NextItemClicked { get; set; }

        public MainPageViewModel()
        {
            AddDummyData();
            ItemSelected = new Command(handleItemClicked);
            NextItemClicked = new Command(handleNextItemClicked);
        }

        private void handleNextItemClicked(object obj)
        {
           
        }

        #region //three models List Defination 
        private List<Customer> _customers = new List<Customer>();



        public List<Customer> Customers
        {
            get { return _customers; }
            set { _customers = value;
                RaisePropertyChanged();
            }
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

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
            get { return _inspections; }
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

      
        
        private void handleItemClicked(object obj)
        {
            var selectedButton = obj as Button;
            var classId = selectedButton.ClassId;
            if (classId.Equals("1")) {

            } else if (classId.Equals("3")) {
            } else {
            }
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


        private bool _backBtnVisibilty;

        public bool BackBtnVisibilty
        {
            get { return _backBtnVisibilty; }
            set { _backBtnVisibilty = value;
                RaisePropertyChanged();
            }
        }

        #region step bar background Color 

        private Color _customerButtonBackgroundColor ;

        public Color CustomerButtonBg
        {
            get { return _customerButtonBackgroundColor ; }
            set { _customerButtonBackgroundColor  = value;
                RaisePropertyChanged();
            }
        }

        private Color _schemeButtonBackgroundColor;

        public Color SchemeButtonBg
        {
            get { return _schemeButtonBackgroundColor; }
            set
            {
                _schemeButtonBackgroundColor = value;
                RaisePropertyChanged();
            }
        }

        private Color _inspectionButtonBackgroundColor;

        public Color InspectionButtonBg
        {
            get { return _inspectionButtonBackgroundColor; }
            set
            {
                _inspectionButtonBackgroundColor = value;
                RaisePropertyChanged();
            }
        }

        #endregion


        private void ClearAll()
        {
            SchemeBindingList.Clear();
            InsepctionBindingList.Clear();
            SelectedCustomer.Clear();
            SelectedSchemes.Clear();
            SelectedInsepction.Clear();
        }

        private void NavigationHandeler()
        {
            if (flag == 0)
            {
                BackBtnVisibilty = false;
                Binding myBinding = new Binding("Customers");
                MainPageListView.SetBinding(ListView.ItemsSourceProperty, myBinding);
                ClearAll();

                CustomerButtonBg = SelectedButtonColor;
                SchemeButtonBg = UnSelectedButtonColor;
                InspectionButtonBg = UnSelectedButtonColor;
            }
            else if (flag == 1)
            {
                List<Customer> customers = ViewModel.Customers;
                ts = customers.Where(x => x.IsChecked == true).ToList();
                if (ts.Count != 0)
                {
                    ViewModel.SelectedCustomer = ts;
                    BackButton.IsVisible = true;
                    List<Scheme> scheme = new List<Scheme>();
                    for (int i = 0; i < ts.Count(); i++)
                    {
                        scheme.AddRange(ViewModel.Schemes.Where(x => x.CustomerId == ViewModel.SelectedCustomer[i].ID).ToList());
                    }
                    ViewModel.SchemeBindingList = scheme;
                    Binding myBinding = new Binding("SchemeBindingList");
                    CustomerButton.BackgroundColor = SelectedButtonColor;
                    SchemeButton.BackgroundColor = SelectedButtonColor;
                    InspectionButton.BackgroundColor = UnSelectedButtonColor;
                    ViewModel.InsepctionBindingList.Clear();
                    ViewModel.SelectedSchemes.Clear();
                    MainPageListView.SetBinding(ListView.ItemsSourceProperty, myBinding);
                }
                else
                {
                    flag = flag - 1;
                    DisplayAlert("Alert", "Please Select Customer(s) Frist", "ok");
                }
            }
            else if (flag == 2)
            {
                List<Scheme> schemes = ViewModel.SchemeBindingList.Where(x => x.IsChecked == true).ToList();
                if (schemes.Count != 0)
                {
                    ViewModel.SelectedSchemes = schemes;
                    List<Inspection> inspections = new List<Inspection>();
                    for (int i = 0; i < schemes.Count(); i++)
                    {
                        inspections.AddRange(ViewModel.Inspections.Where(x => x.SchemeId == ViewModel.SelectedSchemes[i].ID).ToList());
                    }
                    ViewModel.InsepctionBindingList = inspections;
                    Binding myBinding = new Binding("InsepctionBindingList");
                    CustomerButton.BackgroundColor = SelectedButtonColor;
                    SchemeButton.BackgroundColor = SelectedButtonColor;
                    InspectionButton.BackgroundColor = SelectedButtonColor;
                    NextButton.Text = "START";
                    MainPageListView.SetBinding(ListView.ItemsSourceProperty, myBinding);
                }
                else
                {
                    flag = flag - 1;
                    DisplayAlert("Alert", "Please Select Scheme(s) Frist", "ok");
                }
            }
        }

    }
}
