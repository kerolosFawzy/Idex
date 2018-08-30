using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace CustomControls
{
    public class ButtonCreator : StackLayout
    {
        #region view BindableProperty
        public static readonly BindableProperty StepsProperty = BindableProperty
            .Create(nameof(Steps), typeof(int), typeof(ButtonCreator), 0);

        public static readonly BindableProperty StepSelectedProperty = BindableProperty
            .Create(nameof(StepSelected), typeof(int), typeof(ButtonCreator), 0, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty PlaceHolderProperty = BindableProperty
            .Create(nameof(PlaceHolder), typeof(string), typeof(ButtonCreator), defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty ItemClickedProperty = BindableProperty
          .Create(nameof(ItemClicked), typeof(ICommand), typeof(ButtonCreator), null, defaultBindingMode: BindingMode.TwoWay);


        public static readonly BindableProperty ItemSelectedIndexProperty = BindableProperty
            .Create(nameof(ItemSelectedIndex), typeof(List<string>), typeof(ButtonCreator), null, BindingMode.OneWay);


        #endregion

        #region setter and getter for class properties
        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }

        public int Steps
        {
            get { return (int)GetValue(StepsProperty); }
            set { SetValue(StepsProperty, value); }
        }

        public int StepSelected
        {
            get { return (int)GetValue(StepSelectedProperty); }
            set { SetValue(StepSelectedProperty, value); }
        }

        public Command ItemClicked
        {
            get { return (Command)GetValue(ItemClickedProperty); }
            set { SetValue(ItemClickedProperty, value); }
        }

        public List<string> ItemSelectedIndex
        {
            get { return (List<string>)GetValue(ItemSelectedIndexProperty); }
            set { SetValue(ItemSelectedIndexProperty, value); }
        }
        #endregion

        public ButtonCreator()
        {
            Orientation = StackOrientation.Horizontal;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            Spacing = 0;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName.Equals(StepsProperty.PropertyName))
            {
                for (int i = 0; i < Steps; i++)
                {
                    var button = new Button
                    {
                        ClassId = $"{i + 1}",
                        Style = Application.Current.Resources["ButtonCreatorStyle"] as Style
                    };
                    button.Clicked += Handle_Clicked;
                    Children.Add(button);
                }

            }
            else if (propertyName.Equals(StepSelectedProperty.PropertyName) || propertyName.Equals(ItemSelectedIndexProperty.PropertyName))
            {
                SelectElement();
            }
            else if (propertyName.Equals(PlaceHolderProperty.PropertyName))
            {
                foreach (var child in Children)
                {
                    (child as Button).Text = PlaceHolder;
                }
            }
        }
        void Handle_Clicked(object sender, EventArgs e)
        {
            ItemClicked?.Execute(sender);

            SelectElement();
        }

        private void SelectElement()
        {
            var ButtonStyle = Application.Current.Resources["ButtonCreatorStyle"] as Style;

            foreach (var item in Children.Where(x => x.GetType() == typeof(Button)))
            {
                ((Button)item).Style = ButtonStyle;
            }
            if (ItemSelectedIndex != null)
            {
                var selectChildList = from x1 in Children
                                      join x2 in ItemSelectedIndex on x1.ClassId equals x2
                                      select x1;

            }
        }

    }
}
