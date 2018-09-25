using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;

namespace IDEX.Behavior
{
    /*
   * you must send min and max values and make sure min <= max or app will crash 
   * it will take the entry value and return valid or not valid 
   * as boolean if not valid text will be red 
   * Note: the viewmodel code will not fire till isVaild = true 
   * Note: he will hendel if you send number or char and return isvaild value 
   * dont worry about it its tested :)
   */
    public class SmallRangeBehavior : BehaviorBase<Xamarin.Forms.View>
    {
        #region BindableProperty
        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(
         "IsValid",
         typeof(bool),
         typeof(SmallRangeBehavior),
         false,
         defaultBindingMode: BindingMode.OneWayToSource
         );

        public static readonly BindableProperty MinProperty = BindableProperty.Create(
         nameof(Min),
         typeof(string),
         typeof(SmallRangeBehavior),
         defaultBindingMode: BindingMode.OneWay
         );


        public static readonly BindableProperty MaxProperty = BindableProperty.Create(
         nameof(Max),
         typeof(string),
         typeof(SmallRangeBehavior),
         defaultBindingMode: BindingMode.OneWay
         );

        public string Max
        {
            get { return (string)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }
        public string Min
        {
            get { return (string)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }

        public static bool GetIsValid(BindableObject view)
        {
            return (bool)view.GetValue(IsValidProperty);
        }
        public static void SetIsValid(BindableObject view, bool value)
        {
            try
            {
                view.SetValue(IsValidProperty, value);
            }
            catch (Exception exception) { Crashes.TrackError(exception); }
        }
        #endregion

        void Register(Entry view)
        {
            if (!(view is Entry entry))
            {
                return;
            }
            entry.TextChanged += OnTextChanged;
        }
        void Detaching(Entry view)
        {
            if (!(view is Entry entry))
            {
                return;
            }
            entry.TextChanged -= OnTextChanged;
        }

        protected override void OnAttachedTo(BindableObject bindable)
        {
            Register(bindable as Entry);
            base.OnAttachedTo(bindable);
        }
        protected override void OnDetachingFrom(BindableObject bindable)
        {
            Detaching(bindable as Entry);
            base.OnDetachingFrom(bindable);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            bool isValid = false;
            if (int.TryParse(Min, out int min) && int.TryParse(Max, out int max) && int.TryParse(e.NewTextValue, out int input))
            {
                if ( min <= input && max >= input)
                    isValid = true;
            }
            else if (e.NewTextValue.Length == 1 && char.IsLetter(e.NewTextValue.ToCharArray()[0]))
            {
                isValid = CharValidation(e.NewTextValue);
            }
            else
            {
                isValid = false;
            }
            if (GetIsValid(sender as Entry))
                SetIsValid(sender as Entry, false);
            SetIsValid(sender as Entry, isValid);
            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }

        private bool CharValidation(string input)
        {
            int inputValue= Convert.ToInt32(input.ToCharArray()[0]);
            int min = Convert.ToInt32(Min.ToCharArray()[0]);
            int max = Convert.ToInt32(Max.ToCharArray()[0]);
            if (min <= inputValue && max >= inputValue)
                return true;
            return false;
        }
    }
}
