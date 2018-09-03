using Java.Util;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace CustomControls
{
    public class ButtonCreator : StackLayout
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
            .Create(nameof(Data), typeof(IDictionary<string , string>), typeof(ButtonCreator), null, BindingMode.OneWayToSource);
        #endregion

        #region setter and getter for class properties
        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
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

        public Command ItemClicked
        {
            get { return (Command)GetValue(ItemClickedProperty); }
            set { SetValue(ItemClickedProperty, value); }
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

            if (sender.ClassId.Equals("1"))
            {
                dict.Add(nameof(InstaRoomEnum), InstaRoomEnum.Inv.ToString());
            }
            else if (sender.ClassId.Equals("2")) {
                dict.Add(nameof(InstaRoomEnum), InstaRoomEnum.Wall.ToString());
            }
            else if (sender.ClassId.Equals("3")) {
                dict.Add(nameof(InstaRoomEnum), InstaRoomEnum.Floor.ToString());
            }
            else if (sender.ClassId.Equals("4"))
            {
                dict.Add(nameof(InstaRoomEnum), InstaRoomEnum.Ceil.ToString());
            }


            //if (InstaCategory.ToString().Equals("1"))
            //{
            //    dict.Add(nameof(InstaCategoryEnum), InstaCategoryEnum.Waste.ToString());

            //}
            //else if (InstaCategory.ToString().Equals("2"))
            //{
            //    dict.Add(nameof(InstaCategoryEnum), InstaCategoryEnum.Dust.ToString());

            //}
            //else if (InstaCategory.ToString().Equals("3"))
            //{
            //    dict.Add(nameof(InstaCategoryEnum), InstaCategoryEnum.Stains.ToString());

            //}
            //else if (InstaCategory.ToString().Equals("4"))
            //{
            //    dict.Add(nameof(InstaCategoryEnum), InstaCategoryEnum.SurfaceSoilings.ToString());

            //}
            dict.Add(nameof(InstaCategoryEnum), InstaCategory.ToString());
            dict.Add("State" , PlaceHolder);
            Data = (Dictionary<string , string>)dict; 
        }
        private void SelectElement(Button sender )
        {
                //var selectChildList = from x1 in Children
                //                      join x2 in ItemSelectedIndex on x1.ClassId equals x2
                //                      select x1;

                int count=0;
                try {
                    Int32.TryParse(sender.Text, out count);
                    count = count + 1; 
                } catch {
                    count = 1; 
                }
             sender.Text = count.ToString();
            
        }

    }
}
