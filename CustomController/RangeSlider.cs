using Microsoft.AppCenter.Crashes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace CustomControls
{
    public class RangeSlider : Grid
    {
        Button _lastStepSelected;
        public static readonly BindableProperty StepsProperty
            = BindableProperty.Create(nameof(Steps), typeof(int), typeof(RangeSlider), 0);
        public static readonly BindableProperty StepSelectedProperty
            = BindableProperty.Create(nameof(StepSelected), typeof(int), typeof(RangeSlider), 0, defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty StepColorProperty
            = BindableProperty.Create(nameof(StepColor), typeof(Color), typeof(RangeSlider), Color.Black, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty RangListProperty
            = BindableProperty.Create(nameof(RangList), typeof(IList<string>), typeof(RangeSlider), defaultBindingMode: BindingMode.OneWay);

        public List<string> RangList
        {
            get { return (List<string>)GetValue(RangListProperty); }
            set { SetValue(RangListProperty, value); }
        }

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


        public RangeSlider()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand;
            ColumnSpacing = 0;
            RowSpacing = 0;
            
            RowDefinition row = new RowDefinition
            {
                Height = new GridLength(1, GridUnitType.Star)
            };
            ColumnDefinition column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };

            AddStyles();

        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == StepsProperty.PropertyName)
            {
                for (int i = 0; i < Steps * 2; i =i+ 2)
                {
                    var label = new ExtLabel()
                    {
                        Text = "A"
                        ,
                        ClassId = $"{i + 1}"
                        ,
                        Style = Application.Current.Resources["RangeLabel"] as Style
                    };
                    try
                    {
                        Children.Add(label, i, 0);
                    }
                    catch (Exception exception) { Crashes.TrackError(exception); }

                    

                    var button = new Button()
                    {
                        ClassId = $"{i + 1}",
                        VerticalOptions = LayoutOptions.Center,
                        Style = Resources["unSelectedStyle"] as Style
                    };

                    button.Clicked += Handle_Clicked;

                    Children.Add(button, i, 1);

                    if (i < Steps *2  - 2)
                    {
                        var separatorLine = new StackLayout()
                        {
                            BackgroundColor = Color.Black,
                            HeightRequest = 2,
                            MinimumWidthRequest = 10,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.FillAndExpand
                        };

                        try { Children.Add(separatorLine, i + 1, 1); }
                        catch (Exception exception) { Crashes.TrackError(exception); }
                    }
                }
            }
            else if (propertyName == StepSelectedProperty.PropertyName)
            {
                var children = Children.First(p => (!string.IsNullOrEmpty(p.ClassId) && Convert.ToInt32(p.ClassId) == StepSelected));
                if (children != null) SelectElement(children as Button);

            }
            else if (propertyName == StepColorProperty.PropertyName)
            {
                AddStyles();
            }
        }
        void Handle_Clicked(object sender, EventArgs e)
        {
            SelectElement(sender as Button);
        }

        void SelectElement(Button elementSelected)
        {

            if (_lastStepSelected != null) _lastStepSelected.Style = Resources["unSelectedStyle"] as Style;

            elementSelected.Style = Resources["selectedStyle"] as Style;

            StepSelected = Convert.ToInt32(elementSelected.Text);
            _lastStepSelected = elementSelected;

        }

        void AddStyles()
        {
            var unselectedStyle = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = BackgroundColorProperty,   Value = Color.Black },
                    new Setter { Property = Button.BorderColorProperty,   Value = Color.Black },
                    new Setter { Property = Button.BorderWidthProperty,   Value = 0.5 },
                    new Setter { Property = Button.CornerRadiusProperty,   Value = 5 },
                    new Setter { Property = HeightRequestProperty,   Value = 10 },
                    new Setter { Property = WidthRequestProperty,   Value = 5 } 
            }
            };

            var selectedStyle = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = BackgroundColorProperty, Value = StepColor },
                    new Setter { Property = Button.BorderColorProperty, Value = StepColor },
                    new Setter { Property = Button.BorderWidthProperty,   Value = 0.5 },
                    new Setter { Property = Button.CornerRadiusProperty,   Value = 10 },
                    new Setter { Property = HeightRequestProperty,   Value = 20 },
                    new Setter { Property = WidthRequestProperty,   Value = 20 }
                }
            };

            Resources = new ResourceDictionary
            {
                { "unSelectedStyle", unselectedStyle },
                { "selectedStyle", selectedStyle }
            };
        }
    }
}