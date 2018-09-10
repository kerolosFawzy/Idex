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
            .Create(nameof(Steps), typeof(int), typeof(ButtonCreatorHygiene), 0);

        public static readonly BindableProperty ButtonClickedCommandProperty = BindableProperty
            .Create(nameof(ButtonClickedCommand), typeof(ICommand), typeof(ButtonCreatorHygiene), null, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty DataProperty = BindableProperty
            .Create(nameof(Data), typeof(IDictionary<string, string>), typeof(ButtonCreatorHygiene), null, BindingMode.OneWayToSource);

        public static readonly BindableProperty PickerValueProperty = BindableProperty
             .Create(nameof(PickerValue), typeof(int), typeof(ButtonCreatorHygiene), null, BindingMode.TwoWay);

        public static readonly BindableProperty LayoutNameProperty = BindableProperty
             .Create(nameof(LayoutName), typeof(string), typeof(ButtonCreatorHygiene), null, BindingMode.TwoWay);


        #endregion

        public enum State
        {
            Easy, Hard
        }

        IDictionary<string, string> dict;
        Button LastSelectedButton;
        static string LastLayoutId;

        #region setter and getter for class properties
        public Command ButtonClickedCommand
        {
            get { return (Command)GetValue(ButtonClickedCommandProperty); }
            set { SetValue(ButtonClickedCommandProperty, value); }
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
                    Text = State.Hard.ToString(),
                    Style = Application.Current.Resources["ButtonCreatorHygienStyle"] as Style
                };
                FirstHardButton.Clicked += Handle_Clicked;

                Children.Add(FirstHardButton);
                for (int i = 1; i <= Steps; i++)
                {
                    var EasyButton = new Button
                    {
                        Text = State.Easy.ToString(),
                        ClassId = $"{i * i + 1}",
                        Style = Application.Current.Resources["ButtonCreatorStyle"] as Style
                    };

                    EasyButton.Clicked += Handle_Clicked;
                    var HardButton = new Button
                    {
                        Text = State.Hard.ToString(),
                        ClassId = $"{i * i + 2}",
                    };
                    Children.Add(EasyButton);

                    if (i == Steps)
                        HardButton.Style = Application.Current.Resources["ButtonCreatorStyle"] as Style;
                    else
                        HardButton.Style = Application.Current.Resources["ButtonCreatorHygienStyle"] as Style;

                    HardButton.Clicked += Handle_Clicked;

                    Children.Add(HardButton);

                }
            }
            else if (propertyName.Equals(PickerValueProperty.PropertyName) && Data != null && LastLayoutId == Data["Category"] && LastSelectedButton.Text != PickerValue.ToString())
            {
                if (PickerValue == 0)
                {
                    LastSelectedButton.TextColor = Color.LightGray;
                    switch (LastSelectedButton.ClassId)
                    {
                        case "1":
                            LastSelectedButton.Text = State.Hard.ToString();
                            break;
                        case "2":
                            LastSelectedButton.Text = State.Easy.ToString();
                            break;
                        case "3":
                            LastSelectedButton.Text = State.Hard.ToString();
                            break;
                        case "5":
                            LastSelectedButton.Text = State.Easy.ToString();
                            break;
                        case "6":
                            LastSelectedButton.Text = State.Hard.ToString();
                            break;
                    }
                }
                else
                {
                    try
                    {
                        Data["Count"] = PickerValue.ToString();
                        LastSelectedButton.Text = Data["Count"];
                        LastSelectedButton.TextColor = Color.Black;
                        GetData(LastSelectedButton);
                    }
                    catch (Exception exception) { Crashes.TrackError(exception); }
                }

            }
        }
        void Handle_Clicked(object sender, EventArgs e)
        {
            ButtonClickedCommand?.Execute(sender);
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
            dict = new Dictionary<string, string>();
            dict.Add("Count", sender.Text);
            dict.Add("Category", LayoutName);
            try
            {
                switch (sender.ClassId)
                {
                    case "1":
                        AddData(State.Hard.ToString(), "HumBio");
                        break;
                    case "2":
                        AddData(State.Easy.ToString(), "Dust");
                        break;
                    case "3":
                        AddData(State.Hard.ToString(), "Dust");
                        break;
                    case "5":
                        AddData(State.Easy.ToString(), "Wast");
                        break;
                    case "6":
                        AddData(State.Hard.ToString(), "Wast");
                        break;
                }

                Data = (Dictionary<string, string>)dict;
                LastLayoutId = dict["Category"];
                LastSelectedButton = sender;
            }
            catch (Exception exception) { Crashes.TrackError(exception); }

        }
        private void AddData(string State, string CleaningCategory)
        {
            dict.Add("State", State);
            dict.Add("CleaningCategory", CleaningCategory);
        }
        private void SelectElement(Button sender)
        {
            //var selectChildList = from x1 in Children
            //                      join x2 in ItemSelectedIndex on x1.ClassId equals x2
            //                      select x1;
        }
    }
}
