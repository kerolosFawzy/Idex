using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;

namespace IDEX.Behavior
{
    public class LargeRangeBehavior : BehaviorBase<Entry>
    {
        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(
           "IsValid",
           typeof(bool),
           typeof(LargeRangeBehavior),
           false,
           defaultBindingMode: BindingMode.OneWayToSource
           );

        public static readonly BindableProperty MaxProperty = BindableProperty.Create(
         nameof(Max),
         typeof(double),
         typeof(SmallRangeBehavior),
         defaultBindingMode: BindingMode.OneWay
         );


        public static readonly BindableProperty MinProperty = BindableProperty.Create(
         nameof(Min),
         typeof(double),
         typeof(SmallRangeBehavior),
         defaultBindingMode: BindingMode.OneWay
         );

        public double Max
        {
            get { return (double)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }
        public double Min
        {
            get { return (double)GetValue(MinProperty); }
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
            double.TryParse(e.NewTextValue, out double inputNumber);
            
            if (inputNumber <= Max && inputNumber >= Min)
                isValid = true; 
            if (GetIsValid(sender as Entry))
                SetIsValid(sender as Entry, false);
            SetIsValid(sender as Entry, isValid);
            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }

    }
}
