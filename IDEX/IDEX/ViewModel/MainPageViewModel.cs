using Acr.UserDialogs;
using Autofac;
using IDEX.Model;
using IDEX.Views;
using ReactiveUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace IDEX.ViewModel
{
    class MainPageViewModel : BaseViewModel
    {
        private static int flag;
        ReactiveList<Customer> ts ;

        #region Commands for the view
        public ICommand ItemSelected { get; set; }
        public ICommand ItemTapped { get; set; }
        public ReactiveCommand ReactiveBackButtonClicked { get; set; }
        public ReactiveCommand ReactiveNextItemClicked { get; private set; }
        #endregion

        public MainPageViewModel()
        {
            flag = 0;
            AddDummyData();
            ItemSelected = new Command(HandleItemClicked);
            ItemTapped = new Command(HandleItemTapped);
            ReactiveBackButtonClicked = ReactiveCommand.Create(HandleReactiveBackButtonClicked);
            ReactiveNextItemClicked = ReactiveCommand.Create(HandleReactiveNextItemClicked);
            SetReactiveListListen();
        }

        private void HandleItemTapped(object obj)
        {
            var Elemnet = obj as BaseModel;
            Elemnet.IsChecked = !Elemnet.IsChecked;

        }

        private void SetReactiveListListen()
        {
            Customers.ItemChanged.Where(x => x.PropertyName == "IsChecked")
                            .Select(x => x.Sender)
                            .Subscribe(x =>
                            {
                                var tempList = SelectedCustomer.ToList();
                                if (x.IsChecked == false)
                                    tempList.Remove(x);
                                else
                                    tempList.Add(x);

                                SelectedCustomer = new ReactiveList<Customer>(tempList);
                            });
            Schemes.ItemChanged.Where(x => x.PropertyName == "IsChecked")
                .Select(x => x.Sender)
                .Subscribe(x =>
                {
                    var tempList = SelectedSchemes.ToList();
                    if (x.IsChecked == false)
                        tempList.Remove(x);
                    else
                        tempList.Add(x);
                    SelectedSchemes = new ReactiveList<Scheme>(tempList);
                });
        }

        #region Handle all buttons on the view and listviews

        private void HandleReactiveBackButtonClicked()
        {
            if (flag == 3)
                flag = 2;
            flag -= 1;
            NavigationHandeler();
        }

        private void HandleReactiveNextItemClicked()
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
            get => _selectedIndexs;
            set => this.RaiseAndSetIfChanged(ref _selectedIndexs, value);
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
            get => _itemListSource;
            set => this.RaiseAndSetIfChanged(ref _itemListSource, value);
        }

        private string _nextButtonTitle = "Next";
        public string NextButtonTitle
        {
            get => _nextButtonTitle;
            set => this.RaiseAndSetIfChanged(ref _nextButtonTitle, value);
        }
        private bool _backBtnVisibilty;
        public bool BackBtnVisibilty
        {
            get => _backBtnVisibilty;
            set => this.RaiseAndSetIfChanged(ref _backBtnVisibilty, value);
        }
        #endregion

        #region three models List Defination 
        private ReactiveList<Customer> _customers;
        public ReactiveList<Customer> Customers
        {
            get => _customers;
            set => this.RaiseAndSetIfChanged(ref _customers, value);
        }

        private ReactiveList<Scheme> _Schemes;

        public ReactiveList<Scheme> Schemes
        {
            get => _Schemes;
            set => this.RaiseAndSetIfChanged(ref _Schemes, value);
        }

        private ReactiveList<Inspection> _inspections;

        public ReactiveList<Inspection> Inspections
        {
            get => _inspections;
            set => this.RaiseAndSetIfChanged(ref _inspections, value);
        }
        #endregion

        #region init Selected Lists 

        private ReactiveList<Customer> _selectCustomers = new ReactiveList<Customer> ();
        public ReactiveList<Customer> SelectedCustomer
        {
            get => _selectCustomers;
            set => this.RaiseAndSetIfChanged(ref _selectCustomers, value);
        }

        private ReactiveList<Scheme> _schemeBindingList = new ReactiveList<Scheme> { ChangeTrackingEnabled = true };
        public ReactiveList<Scheme> SchemeBindingList
        {
            get => _schemeBindingList;
            set => this.RaiseAndSetIfChanged(ref _schemeBindingList, value);
        }

        private ReactiveList<Scheme> _selectedSchemes = new ReactiveList<Scheme> { ChangeTrackingEnabled = true };
        public ReactiveList<Scheme> SelectedSchemes
        {
            get => _selectedSchemes;
            set => this.RaiseAndSetIfChanged(ref _selectedSchemes, value);
        }

        private ReactiveList<Inspection> _insepctionBindingList = new ReactiveList<Inspection> { ChangeTrackingEnabled = true };
        public ReactiveList<Inspection> InsepctionBindingList
        {
            get => _insepctionBindingList;
            set => this.RaiseAndSetIfChanged(ref _insepctionBindingList, value);
        }

        private ReactiveList<Inspection> _selectedInsepction = new ReactiveList<Inspection> { ChangeTrackingEnabled = true };
        public ReactiveList<Inspection> SelectedInsepction
        {
            get => _selectedInsepction;
            set => this.RaiseAndSetIfChanged(ref _selectedInsepction, value);
        }

        #endregion
        private void AddDummyData()
        {
            Customers = new ReactiveList<Customer>() { ChangeTrackingEnabled = true };
            Customers.Add(new Customer { ID = 1, Name = "Hospital" });
            Customers.Add(new Customer { ID = 2, Name = "School" });
            Customers.Add(new Customer { ID = 3, Name = "University" });

            Schemes = new ReactiveList<Scheme>() { ChangeTrackingEnabled = true };
            Schemes.Add(new Scheme { ID = 1, CustomerId = 1, Name = "Hospital scheme" });
            Schemes.Add(new Scheme { ID = 2, CustomerId = 2, Name = "School scheme" });
            Schemes.Add(new Scheme { ID = 3, CustomerId = 3, Name = "University scheme" });

            Inspections = new ReactiveList<Inspection>() { ChangeTrackingEnabled = true };
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
            SelectedSchemes.Clear();
            SelectedCustomer.Clear();
            foreach (BaseModel customer in Customers) {
                if(customer.IsChecked)
                   customer.IsChecked = false;
            }
            foreach (BaseModel schemelist in Schemes)
            {
                if (schemelist.IsChecked)
                    schemelist.IsChecked = false;
            }
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
                ts  = new ReactiveList<Customer>() { ChangeTrackingEnabled = true };
                ts.AddRange(Customers.Where(x => x.IsChecked == true) ?? new ReactiveList<Customer>());
                if (ts.Count != 0)
                {
                    AddSelectedIndexs(2);
                    SelectedCustomer = ts;
                    BackBtnVisibilty = true;
                    ReactiveList<Scheme> scheme = new ReactiveList<Scheme>();
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
                    UserDialogs.Instance.AlertAsync("Please Select Customer(s) Frist", "Alert", "ok");
                }
            }
            else if (flag == 2)
            {
                ReactiveList<Scheme> schemes = new ReactiveList<Scheme>();
                
                schemes.AddRange(SchemeBindingList.Where(x => x.IsChecked == true)?? new ReactiveList<Scheme>());

                if (schemes.Count != 0)
                {
                    AddSelectedIndexs(3);
                    SelectedSchemes = schemes;
                    ReactiveList<Inspection> inspections = new ReactiveList<Inspection>();
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
                    UserDialogs.Instance.AlertAsync("Please Select Scheme(s) Frist", "Alert", "ok");

                }
            }
            else if (flag == 3)
            {
                ReactiveList<Inspection> inspections = new ReactiveList<Inspection>();
                inspections.AddRange(InsepctionBindingList.Where(x => x.IsChecked == true) ?? new ReactiveList<Inspection>());
                if(inspections.Count != 0 )
                Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Navigation.NavigateAsync(nameof(OverviewPage));
                    });
                else
                    UserDialogs.Instance.AlertAsync("Please Select Inspection(s) Frist", "Alert", "ok");

            }
        }

    }
}