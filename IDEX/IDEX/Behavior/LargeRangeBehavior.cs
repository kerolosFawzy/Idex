using System;
using Xamarin.Forms;

namespace IDEX.Behavior
{
    public class LargeRangeBehavior : BehaviorBase<Xamarin.Forms.View>
    {
        public static readonly BindableProperty IsValidProperty =
        BindableProperty.Create(
           "IsValid",
           typeof(bool),
           typeof(LargeRangeBehavior),
           false,
           defaultBindingMode: BindingMode.OneWayToSource
           );

        public static bool GetIsValid(BindableObject view)
        {
            return (bool)view.GetValue(IsValidProperty);
        }
        public static void SetIsValid(BindableObject view, bool value)
        {
            view.SetValue(IsValidProperty, value);
        }

        void Register(Entry view) {
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
       
        private static void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            string[] splitInput = e.NewTextValue.ToString().Split('.');
            bool isValid = false;
            if (splitInput.Length != 2)
            {
                isValid = false;
            }
            else
            {
                
                if (int.TryParse(splitInput[0], out int n) && int.TryParse(splitInput[1], out int x))
                {
                    isValid = NumberValidation(splitInput);
                }
                else if (splitInput[0].Length == 1 && splitInput[1].Length == 1)
                {
                    isValid = CharValidation(splitInput);
                }
                else
                {
                    isValid = false;
                }
            }

            SetIsValid(sender as Entry, isValid);
            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }

        static bool NumberValidation(string[] Input)
        {
            int.TryParse(Input[0], out int min);
            int.TryParse(Input[1], out int max);

            return min < max; 
        }
        static bool CharValidation(string[] Input)
        {
            char[] firstChar = Input[0].ToUpper().ToCharArray();
            int min = Convert.ToInt32(firstChar[0]);

            char[] secondChar = Input[1].ToUpper().ToCharArray();
            int max = Convert.ToInt32(secondChar[0]);
            if (char.IsLetter(firstChar[0]) && char.IsLetter(secondChar[0])) {
                return min < max;
            }
            return false;
        }
    }
}
