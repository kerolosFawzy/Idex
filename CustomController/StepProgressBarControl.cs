using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
//ref 
//https://xamgirl.com/step-bar-in-xamarin-forms/
namespace CustomControls
{
    public class StepProgressBarControl : StackLayout
    {
        //it work for any number of buttons (steps) 
        //send button color from the view 
        #region view BindableProperty
        public static readonly BindableProperty StepsProperty = BindableProperty
            .Create(nameof(Steps), typeof(int), typeof(StepProgressBarControl), 0);

        public static readonly BindableProperty StepSelectedProperty = BindableProperty
            .Create(nameof(StepSelected), typeof(int), typeof(StepProgressBarControl), 0, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty StepColorProperty = BindableProperty
            .Create(nameof(StepColor), typeof(Color), typeof(StepProgressBarControl), Color.LightGreen, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty ItemClickedProperty = BindableProperty
            .Create(nameof(ItemClicked), typeof(ICommand), typeof(StepProgressBarControl), null, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty ItemSelectedIndexProperty = BindableProperty
            .Create(nameof(ItemSelectedIndex), typeof(List<string>), typeof(StepProgressBarControl), null , BindingMode.OneWay );

        #endregion

        #region setter and getter for class properties
        public Color StepColor
        {
            get { return (Color)GetValue(StepColorProperty); }
            set { SetValue(StepColorProperty, value); }
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

        public StepProgressBarControl()
        {
            Orientation = StackOrientation.Horizontal;
            HorizontalOptions = LayoutOptions.CenterAndExpand;
            Padding = new Thickness(10, 0);
            Spacing = 0;
            AddStyle();
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
                        Style = Resources["unselectedStyle"] as Style
                    };
                    button.Clicked += Handle_Clicked;
                    Children.Add(button);
                    if (i < Steps - 1)   //for drawing lines between buttons  
                    {
                        var separatorLine = new Frame()
                        {
                            BackgroundColor = Color.Transparent,
                            Padding = 2,
                            Margin = -1,
                            HeightRequest = 2,
                            WidthRequest = 50,
                            BorderColor = Color.Silver,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.FillAndExpand
                        };
                        Children.Add(separatorLine);
                    }
                }
            }
            else if (propertyName.Equals(StepSelectedProperty.PropertyName) || propertyName.Equals(ItemSelectedIndexProperty.PropertyName))
            {
                SelectElement();
            }
            else if (propertyName.Equals(StepColorProperty.PropertyName))
            {
                AddStyle();
            }
     

        }
        void Handle_Clicked(object sender, EventArgs e)
        {
              ItemClicked?.Execute(sender);
              SelectElement();
        }

        private void SelectElement()
        {
            var selectedStyle = Resources["selectedStyle"] as Style;
            var unSelectedStyle = Resources["unselectedStyle"] as Style;
            foreach (var item in Children.Where(x => x.GetType() == typeof(Button)))
            {
                ((Button)item).Style = unSelectedStyle;
            }
            if (ItemSelectedIndex != null)
            {
                var selectChildList = from x1 in Children
                                      join x2 in ItemSelectedIndex on x1.ClassId equals x2
                                      select x1;
                foreach (var item in selectChildList)
                {
                    ((Button)item).Style = selectedStyle;
                }
            }
        }
        private void AddStyle()
        {
            var unselectedStyle = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = BackgroundColorProperty,   Value = Color.Transparent },
                    new Setter { Property = Button.BorderColorProperty,   Value = StepColor },
                    new Setter { Property = Button.BorderWidthProperty,   Value = 0.5 },
                    new Setter { Property = Button.CornerRadiusProperty,   Value = 30 },
                    new Setter { Property = HeightRequestProperty,   Value = 30 },
                    new Setter { Property = WidthRequestProperty,   Value = 30 }
            }
            };

            var selectedStyle = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = BackgroundColorProperty, Value = StepColor },
                    new Setter { Property = Button.BorderColorProperty, Value = StepColor },
                    new Setter { Property = Button.BorderWidthProperty,   Value = 0.5 },
                    new Setter { Property = Button.CornerRadiusProperty,   Value = 30 },
                    new Setter { Property = HeightRequestProperty,   Value = 30 },
                    new Setter { Property = WidthRequestProperty,   Value = 30 }
            }
            };

            Resources = new ResourceDictionary
            {
                {nameof(unselectedStyle), unselectedStyle },
                { nameof(selectedStyle) , selectedStyle }
            };
        }
    }
}
