
using System;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace CustomController
{
    public class StepProgressBarControl : StackLayout
    {
        Button _lastButtonSelcted;
        public static readonly BindableProperty StepsProperty = BindableProperty
            .Create(nameof(Steps), typeof(int), typeof(StepProgressBarControl), 0);

        public static readonly BindableProperty StepSelectedProperty = BindableProperty
            .Create(nameof(StepSelected), typeof(int), typeof(StepProgressBarControl), 0, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty StepColorProperty = BindableProperty
            .Create(nameof(StepColor), typeof(Color), typeof(StepProgressBarControl), Color.LightGreen, defaultBindingMode: BindingMode.TwoWay);

        #region //setter and getter for class properties
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
        #endregion

        public StepProgressBarControl() {
            Orientation = StackOrientation.Horizontal;
            HorizontalOptions = LayoutOptions.CenterAndExpand;
            Padding = new Thickness(10, 0);
            Spacing = 0;
            AddStyle();
        }

        protected override void OnPropertyChanged(string propertyName = null) {
            base.OnPropertyChanged(propertyName);
            if (propertyName.Equals(StepsProperty.PropertyName))
            {
                for (int i = 0; i < Steps; i++)
                {
                    var button = new Button
                    {
                        ClassId = $"{i + 1}",
                        Style = Resources["unSelectedStyle"] as Style
                    };
                    button.Clicked += Handle_Clicked;
                    Children.Add(button);
                    if (i < Steps - 1)
                    {
                        var separatorLine = new Frame()
                        {
                            BackgroundColor = Color.Transparent,
                            Padding=2,
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
            else if (propertyName.Equals(StepSelectedProperty.PropertyName))
            {
                var children = Children.First(p => (!string.IsNullOrEmpty(p.ClassId).Equals(StepSelected)));
                if (children != null)
                    SelectElement(children as Button);
            }
            else if (propertyName.Equals(StepColorProperty.PropertyName)) {
                AddStyle();
            }
         
        }
        void Handle_Clicked(object sender, System.EventArgs e) => SelectElement(sender as Button);

        private void SelectElement(Button elementSelected)
        {
            if (_lastButtonSelcted != null)
                _lastButtonSelcted.Style = Resources["unSelectedStyle"] as Style;
            elementSelected.Style = Resources["selectedStyle"] as Style;
            _lastButtonSelcted = elementSelected;
        }
        //todo change text size and buttons 
        private void AddStyle()
        {
            var unselectedStyle = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = BackgroundColorProperty,   Value = Color.Transparent },
                    new Setter { Property = Button.BorderColorProperty,   Value = StepColor },
                    new Setter { Property = Button.BorderWidthProperty,   Value = 0.5 },
#pragma warning disable CS0618 // Type or member is obsolete
                    new Setter { Property = Button.BorderRadiusProperty,   Value = 15 },
#pragma warning restore CS0618 // Type or member is obsolete
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
#pragma warning disable CS0618 // Type or member is obsolete
                    new Setter { Property = Button.BorderRadiusProperty,   Value = 15 },
#pragma warning restore CS0618 // Type or member is obsolete
                    new Setter { Property = HeightRequestProperty,   Value = 30 },
                    new Setter { Property = WidthRequestProperty,   Value = 30 },
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
