using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CustomControls
{
    public class CustomCheckBox : Image
    {
        private static readonly string ImageChecked = "checkbox_checked.png";
        private static readonly string ImageUnchecked = "checkbox_unchecked.png";

        public static BindableProperty CheckedProperty = BindableProperty
            .Create(propertyName: "Checked",returnType : typeof(bool?)
            , declaringType: typeof(CustomCheckBox) , defaultValue: null 
            , defaultBindingMode : BindingMode.TwoWay 
            , propertyChanged: CheckedValueChanged);

        public bool Checked {
            get {
                if (GetValue(CheckedProperty) == null)
                        return false;
                return (bool)GetValue(CheckedProperty); 
            }
            set
            {
                SetValue(CheckedProperty, value);
                OnPropertyChanged();
            }
        }
        private static void CheckedValueChanged(BindableObject bindable , Object oldVlave , object newValue) {
            if (newValue != null && (bool)newValue)
            {
                ((CustomCheckBox)bindable).Source = ImageChecked;
            }
            else
            {
                ((CustomCheckBox)bindable).Source = ImageUnchecked;
            }
        }
        public CustomCheckBox() {
            var tap = new TapGestureRecognizer();
            tap.Tapped += (sender, e) =>
            {
                Checked = !Checked;
            };
            Source = ImageUnchecked;
            GestureRecognizers.Add(tap);
            BackgroundColor = Color.White;
        }
        private bool _isEnabled = true;
        public new bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
                Opacity = value ? 1 : .5;
                base.IsEnabled = value;
            }
        }
        public void OnClicked(object sender, EventArgs e)
        {
            
            Checked = !Checked;
        }
    }
}
