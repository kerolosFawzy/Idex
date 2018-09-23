using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace CustomControls
{
    public class ButtonCreator : Grid
    {
        public enum InstaCategoryEnum
        {
            Waste,
            Dust,
            Stains,
            SurfaceSoilings
        }


        public enum InstaRoomEnum
        {
            Inv,
            Wall,
            Floor,
            Ceil
        }

        static Button LastSelectedButton;
        static string LastStackId;

        #region view BindableProperty
        public static readonly BindableProperty StepsProperty = BindableProperty
            .Create(nameof(Steps), typeof(int), typeof(ButtonCreator), 0);

        public static readonly BindableProperty InstaCategoryProperty = BindableProperty
            .Create(nameof(InstaCategory), typeof(InstaCategoryEnum), typeof(ButtonCreator), defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty PlaceHolderProperty = BindableProperty
            .Create(nameof(PlaceHolder), typeof(string), typeof(ButtonCreator), defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty ItemClickedProperty = BindableProperty
          .Create(nameof(ItemClicked), typeof(ICommand), typeof(ButtonCreator), null, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty InstaRoomEnumProperty = BindableProperty
            .Create(nameof(InstaRoom), typeof(InstaRoomEnum), typeof(ButtonCreator), null, BindingMode.OneWay);

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
        public InstaCategoryEnum InstaCategory
        {
            get { return (InstaCategoryEnum)GetValue(InstaCategoryProperty); }
            set { SetValue(StepsProperty, value); }
        }
        public InstaRoomEnum InstaRoom
        {
            get { return (InstaRoomEnum)GetValue(InstaCategoryProperty); }
            set { SetValue(StepsProperty, value); }
        }



        #endregion

        public ButtonCreator()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand;
            RowDefinition row = new RowDefinition
            {
                Height = new GridLength(1, GridUnitType.Star)
            };
            ColumnDefinition column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            ColumnSpacing = 0;
            RowSpacing = 0;
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

                    Children.AddHorizontal(button);
                }


            }
            else if (propertyName.Equals(PlaceHolderProperty.PropertyName))
            {
                foreach (var child in Children)
                {
                    (child as Button).Text = PlaceHolder;
                }
            }
            else if (propertyName.Equals(PickerValueProperty.PropertyName)
                && Data != null 
                && LastStackId == Data[nameof(LastStackId)] 
                && LastSelectedButton.Text != (PickerValue*5).ToString())
            {
                if (PickerValue == 0)
                {
                    LastSelectedButton.Text = PlaceHolder;
                    LastSelectedButton.TextColor = Color.LightGray;
                }
                else
                {
                    try
                    {
                        if (InstaCategoryEnum.SurfaceSoilings.ToString().Equals(InstaCategory.ToString()))
                            Data["Count"] = (PickerValue * 5).ToString();
                        else
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
            ItemClicked?.Execute(sender);
            int count = 0;
            if (InstaCategoryEnum.SurfaceSoilings.ToString().Equals(InstaCategory.ToString()))
            {
                if (int.TryParse(((Button)sender).Text, out count))
                {
                    if (count < 100)
                        count = count + 5;
                }
                else
                    count = 5;
            }
            else
            {
                if (int.TryParse(((Button)sender).Text, out count))
                {
                    if (count < 100)
                        count = count +1;
                }
                else
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

            if (sender.ClassId.Equals("1"))
            {
                dict.Add(nameof(InstaRoomEnum), InstaRoomEnum.Inv.ToString());
            }
            else if (sender.ClassId.Equals("2"))
            {
                dict.Add(nameof(InstaRoomEnum), InstaRoomEnum.Wall.ToString());
            }
            else if (sender.ClassId.Equals("3"))
            {
                dict.Add(nameof(InstaRoomEnum), InstaRoomEnum.Floor.ToString());
            }
            else if (sender.ClassId.Equals("4"))
            {
                dict.Add(nameof(InstaRoomEnum), InstaRoomEnum.Ceil.ToString());
            }
            try
            {
                dict.Add(nameof(InstaCategoryEnum), InstaCategory.ToString());
                dict.Add("State", PlaceHolder);
                dict.Add(nameof(LastStackId), LayoutName);
                LastStackId = dict[nameof(LastStackId)];
                Data = (Dictionary<string, string>)dict;
                LastSelectedButton = sender;
            }
            catch (Exception exception) { Crashes.TrackError(exception); }

        }

    }
}
