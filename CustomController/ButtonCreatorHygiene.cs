
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace CustomControls
{
    public class ButtonCreatorHygiene : StackLayout
    {
        #region view BindableProperty
        public static readonly BindableProperty StepsProperty = BindableProperty
            .Create(nameof(Steps), typeof(int), typeof(ButtonCreator), 0);

        public static readonly BindableProperty PlaceHolderProperty = BindableProperty
            .Create(nameof(PlaceHolder), typeof(string), typeof(ButtonCreator), defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty ItemClickedProperty = BindableProperty
          .Create(nameof(ItemClicked), typeof(ICommand), typeof(ButtonCreator), null, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty DataProperty = BindableProperty
            .Create(nameof(Data), typeof(IDictionary<string, string>), typeof(ButtonCreator), null, BindingMode.OneWayToSource);

        public static readonly BindableProperty PickerValueProperty = BindableProperty
             .Create(nameof(PickerValue), typeof(int), typeof(ButtonCreator), null, BindingMode.TwoWay);

        public static readonly BindableProperty LayoutNameProperty = BindableProperty
             .Create(nameof(LayoutName), typeof(string), typeof(ButtonCreator), null, BindingMode.TwoWay);


        #endregion

        #region setter and getter for class properties
        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }
        public Command ItemClicked
        {
            get { return (Command)GetValue(ItemClickedProperty); }
            set { SetValue(ItemClickedProperty, value); }
        }
        public string LayoutName
        {
            get { return (string)GetValue(LayoutNameProperty); }
            set { SetValue(LayoutNameProperty, value); }
        }

        public int PickerValue
        {
            get { return (int)GetValue(PickerValueProperty); }
            set { SetValue(PickerValueProperty, value); }
        }

        public Dictionary<string, string> Data
        {
            get { return (Dictionary<string, string>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public int Steps
        {
            get { return (int)GetValue(StepsProperty); }
            set { SetValue(StepsProperty, value); }
        }


        #endregion

        public ButtonCreatorHygiene()
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
                var FirstHardButton = new Button
                {
                    ClassId = "1",
                    Text = "Hard",
                    Style = Application.Current.Resources["ButtonCreatorHygienStyle"] as Style
                };
                FirstHardButton.Clicked += Handle_Clicked;

                Children.Add(FirstHardButton);
                for (int i = 1; i <= Steps; i++)
                {
                    var EasyButton = new Button
                    {
                        Text="Easy",
                        ClassId = $"{i * i + 1}",
                        Style = Application.Current.Resources["ButtonCreatorStyle"] as Style
                    };
                    EasyButton.Clicked += Handle_Clicked;

                    Children.Add(EasyButton);
                    var HardButton = new Button
                    {
                        Text = "Hard",
                        ClassId = $"{i * i + 2}",
                        Style = Application.Current.Resources["ButtonCreatorHygienStyle"] as Style
                    };
                    HardButton.Clicked += Handle_Clicked;

                    Children.Add(HardButton);
                }


            }
            else if (propertyName.Equals(PlaceHolderProperty.PropertyName))
            {
                foreach (var child in Children)
                {
                    (child as Button).Text = PlaceHolder;
                }
            }
            else if (propertyName.Equals(PickerValueProperty.PropertyName) && Data != null)
            {


            }
        }
        void Handle_Clicked(object sender, EventArgs e)
        {
            ItemClicked?.Execute(sender);
            int count = 0;
            try
            {
                Int32.TryParse(((Button)sender).Text, out count);
                count = count + 1;
            }
            catch
            {
                count = 1;
            }
            ((Button)sender).Text = count.ToString();
            ((Button)sender).TextColor = Color.Black;
            GetData(sender as Button);

        }
        void GetData(Button sender)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("Count", sender.Text);
            try
            {

            }
            catch (Exception exception) { Crashes.TrackError(exception); }

        }
        private void SelectElement(Button sender)
        {
            //var selectChildList = from x1 in Children
            //                      join x2 in ItemSelectedIndex on x1.ClassId equals x2
            //                      select x1;

            int count = 0;
            try
            {
                Int32.TryParse(sender.Text, out count);
                count = count + 1;
            }
            catch
            {
                count = 1;
            }
            sender.Text = count.ToString();

        }
    }
}
