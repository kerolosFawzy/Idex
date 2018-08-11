using IDEX.Model;
using IDEX.Views;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace IDEX.ViewModel
{
    class MainPageViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private static int flag;
        List<Customer> ts = new List<Customer>();
      

        #region Commands for the view
        public ICommand ItemSelected { get; set; }
        public ICommand NextItemClicked { get; set; }
        public ICommand BackButtonClicked { get; set; }
        #endregion

        public MainPageViewModel()
        {
            AddDummyData();
            ItemSelected = new Command(HandleItemClicked);
            NextItemClicked = new Command(HandleNextItemClicked);
            BackButtonClicked = new Command(HandleBackClicked);
        }

        #region Handle all buttons on the view and listviews

        private void HandleBackClicked(object obj)
        {
            if (flag == 3)
                flag = 2;
            flag -= 1;
            NavigationHandeler();

        }

        private void HandleNextItemClicked(object obj)
        {
            if (flag == 3)
            { }
            else
                flag += 1;
            NavigationHandeler();
        }

        private List<string> _selectedIndexs = new List<string>() { "1" };

        public List<string> SelectedIndexs
        {
            get { return _selectedIndexs; }
            set
            {
                _selectedIndexs = value;
                RaisePropertyChanged();
            }
        }


        private void HandleItemClicked(object obj)
        {
            var selectedButton = obj as Button;
            var classId = selectedButton.ClassId;
            if (classId.Equals("1"))
            {
                AddSelectedIndexs(1);
                flag = 0;

            }
            else if (classId.Equals("2"))
            {
                AddSelectedIndexs(2);
                flag = 1;
            }
            else if (classId.Equals("3"))
            {
                AddSelectedIndexs(3);
                flag = 2;
            }
            NavigationHandeler();
        }

        private IEnumerable _itemListSource = Enumerable.Empty<BaseModel>();
        public IEnumerable ItemListSource
        {
            get { return _itemListSource; }
            set
            {
                _itemListSource = value;
                RaisePropertyChanged();
            }
        }

        private string _nextButtonTitle = "Next";
        public string NextButtonTitle
        {
            get { return _nextButtonTitle; }
            set
            {
                _nextButtonTitle = value;
                RaisePropertyChanged();
            }
        }
        private bool _backBtnVisibilty;
        public bool BackBtnVisibilty
        {
            get { return _backBtnVisibilty; }
            set
            {
                _backBtnVisibilty = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region three models List Defination 
        private List<Customer> _customers = new List<Customer>();
        public List<Customer> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                RaisePropertyChanged();
            }
        }

        private List<Scheme> _Schemes = new List<Scheme>();

        public List<Scheme> Schemes
        {
            get { return _Schemes; }
            set
            {
                _Schemes = value;
                RaisePropertyChanged();
            }
        }

        private List<Inspection> _inspections = new List<Inspection>();

        public List<Inspection> Inspections
        {
            get { return _inspections; }
            set
            {
                _inspections = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region init Selected Lists 

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
            set
            {
                _schemeBindingList = value;
                RaisePropertyChanged();
            }
        }

        private List<Scheme> _selectedSchemes = new List<Scheme>();
        public List<Scheme> SelectedSchemes
        {
            get { return _selectedSchemes; }
            set
            {
                _selectedSchemes = value;
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
            set
            {
                _selectedInsepction = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        private void AddDummyData()
        {

            Customers.Add(new Customer { ID = 1, Name = "Hospital" });
            Customers.Add(new Customer { ID = 2, Name = "School" });
            Customers.Add(new Customer { ID = 3, Name = "University" });

            Schemes.Add(new Scheme { ID = 1, CustomerId = 1, Name = "Hospital scheme" });
            Schemes.Add(new Scheme { ID = 2, CustomerId = 2, Name = "School scheme" });
            Schemes.Add(new Scheme { ID = 3, CustomerId = 3, Name = "University scheme" });

            Inspections.Add(new Inspection { ID = 1, Name = "Hospital Inspection File", SchemeId = 1 });
            Inspections.Add(new Inspection { ID = 2, Name = "School Inspection File", SchemeId = 2 });
            Inspections.Add(new Inspection { ID = 3, Name = "University Inspection File", SchemeId = 3 });

            ItemListSource = Customers;
        }

        private void AddSelectedIndexs(int num)
        {
            List<string> vs = new List<string>();
            for (int i = 1; i <= num; i++)
            {
                vs.Add(i.ToString());
            }
            SelectedIndexs = vs;
        }
        private void ClearAll()
        {
            SchemeBindingList.Clear();
            InsepctionBindingList.Clear();
            SelectedInsepction.Clear();
            SelectedSchemes = null;
        }
        private void NavigationHandeler()
        {
            if (flag == 0)
            {
                BackBtnVisibilty = false;
                ItemListSource = Customers;
                NextButtonTitle = "Next";
                ClearAll();

                AddSelectedIndexs(1);
            }
            else if (flag == 1)
            {
                List<Customer> customers = Customers;
                ts = customers.Where(x => x.IsChecked == true).ToList();
                if (ts.Count != 0)
                {
                    AddSelectedIndexs(2);
                    SelectedCustomer = ts;
                    BackBtnVisibilty = true;
                    List<Scheme> scheme = new List<Scheme>();
                    for (int i = 0; i < ts.Count(); i++)
                    {
                        scheme.AddRange(Schemes.Where(x => x.CustomerId == SelectedCustomer[i].ID).ToList());
                    }

                    SchemeBindingList = scheme;
                    BackBtnVisibilty = true;
                    NextButtonTitle = "Next";
                    ItemListSource = SchemeBindingList;
                }
                else
                {

                    flag = flag - 1;
                    AddSelectedIndexs(flag + 1);
                    NavigationHandeler();
                    //DisplayAlert("Alert", "Please Select Customer(s) Frist", "ok");
                }
            }
            else if (flag == 2)
            {
                List<Scheme> schemes = SchemeBindingList.Where(x => x.IsChecked == true).ToList();
                if (schemes.Count != 0)
                {
                    AddSelectedIndexs(3);
                    SelectedSchemes = schemes;
                    List<Inspection> inspections = new List<Inspection>();
                    for (int i = 0; i < schemes.Count(); i++)
                    {
                        inspections.AddRange(Inspections.Where(x => x.SchemeId == SelectedSchemes[i].ID).ToList());
                    }
                    BackBtnVisibilty = true;

                    InsepctionBindingList = inspections;
                    NextButtonTitle = "START";
                    ItemListSource = InsepctionBindingList;
                }
                else
                {
                    flag = flag - 1;
                    AddSelectedIndexs(flag + 1);
                    NavigationHandeler();
                    // DisplayAlert("Alert", "Please Select Scheme(s) Frist", "ok");
                }
            }
            else if (flag == 3)
            {
                Navigation.PushAsync(new OverviewPage());
            }
        }

    }
}