using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CustomControls
{
    public class ExtFrameLayout : Frame
    {
        public static readonly BindableProperty CommandProperty 
            = BindableProperty.Create(nameof(Command), typeof(ICommand)
                , typeof(ExtFrameLayout), null
                , propertyChanged: (bo, o, n) => ((ExtFrameLayout)bo).OnCommandChanged());

        public static readonly BindableProperty CommandParameterProperty 
            = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ExtFrameLayout), null,
            propertyChanged: (bindable, oldvalue, newvalue) 
                => ((ExtFrameLayout)bindable).CommandCanExecuteChanged(bindable, EventArgs.Empty));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
        bool IsEnabledCore
        {
            set => SetValueCore(IsEnabledProperty, value);
        }

        private void OnCommandChanged()
        {

            if (Command != null)
            {
                var tapGesture = new TapGestureRecognizer()
                {
                    NumberOfTapsRequired = 1,
                    Command = Command
                };
                GestureRecognizers.Add(tapGesture);
            }

            if (Command != null)
            {
                Command.CanExecuteChanged += CommandCanExecuteChanged;
                CommandCanExecuteChanged(this, EventArgs.Empty);
            }
            else
                IsEnabledCore = true;
        }

        void CommandCanExecuteChanged(object sender, EventArgs eventArgs)
        {
            ICommand cmd = Command;
            if (cmd != null)
                IsEnabledCore = cmd.CanExecute(CommandParameter);
        }
    }
}
