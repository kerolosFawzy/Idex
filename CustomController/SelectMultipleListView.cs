//using Android.Widget;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;


//iam not using this View
namespace CustomController
{
    public class SelectMultipleListView<T> : ContentView
    {

#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
        public class WrappedSelection<T> : INotifyPropertyChanged
#pragma warning restore CS0693 // Type parameter has the same name as the type parameter from outer type
        {
            public T Item { get; set; }
            bool isSelected = false;
            public bool IsSelected
            {
                get
                {
                    return isSelected;
                }
                set
                {
                    if (isSelected != value)
                    {
                        isSelected = value;
                        PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
                    }
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;
        }


        public class WrappedItemSelectionTemplate : ViewCell
        {
            public WrappedItemSelectionTemplate() : base() {
                ExtLabel name = new ExtLabel();
                name.SetBinding(Label.TextProperty,new Binding("Item.Name"));
                Switch mainSwitchButton = new Switch();
                mainSwitchButton.SetBinding(Switch.IsToggledProperty, new Binding("IsSelected"));
                RelativeLayout layout = new RelativeLayout();
                layout.Children.Add(name,
                    Constraint.Constant(5),
                    Constraint.Constant(5),
                    Constraint.RelativeToParent(p => p.Width - 60),
                    Constraint.RelativeToParent(p => p.Height - 10)
                );
                layout.Children.Add(mainSwitchButton,
                    Constraint.RelativeToParent(p => p.Width - 55),
                    Constraint.Constant(5),
                    Constraint.Constant(50),
                    Constraint.RelativeToParent(p => p.Height - 10)
                );
                View = layout;
            }
        }

        public List<WrappedSelection<T>> WrappedItems 
            = new List<WrappedSelection<T>>();

        public SelectMultipleListView(List<T> items) {
            WrappedItems = items.Select(item => new WrappedSelection<T>()
            { Item = item , IsSelected = false}).ToList();
            ListView mainList = new ListView() {
                ItemsSource = WrappedItems , 
                ItemTemplate = new DataTemplate(typeof(WrappedItemSelectionTemplate))
            };

            mainList.ItemSelected += (sender, e) => {
                if (e.SelectedItem == null) return;
                var o = (WrappedSelection<T>)e.SelectedItem;
                o.IsSelected = !o.IsSelected;
                ((ListView)sender).SelectedItem = null; //de-select
            };
            Content = mainList;
        }

        public List<T> GetSelection()
        {
            return WrappedItems.Where(item => item.IsSelected).Select(wrappedItem => wrappedItem.Item).ToList();
        }
    }
}
