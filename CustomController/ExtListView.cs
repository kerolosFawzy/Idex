﻿using System.Windows.Input;
using Xamarin.Forms;

namespace CustomControls
{
    // iam using behavior to make item tapped event to command 
    public class ExtListView : ListView
    {
        //public ExtListView() : base(ListViewCachingStrategy.RecycleElement)
        //{
        //    ItemTapped += (s, e) =>
        //    {
        //        TapCommand?.Invoke(e.Item);
        //    };
        //}

        //public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
        //  nameof(TapCommand),
        //  typeof(ICommand),
        //  typeof(ExtListView)
        //);

        //public ICommand TapCommand
        //{
        //    get
        //    {
        //        return (ICommand)GetValue(TapCommandProperty);
        //    }
        //    set
        //    {
        //        SetValue(TapCommandProperty, value);

        //    }
        //}
    }
}
