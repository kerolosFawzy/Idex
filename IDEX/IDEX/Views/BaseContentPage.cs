using IDEX.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IDEX.Views
{
    public class BaseContentPage : ContentPage
    {
        protected override void OnAppearing()
        {
            if (BindingContext is BaseViewModel viewAwair)
            {
                viewAwair.OnAppearing();
                viewAwair.Navigation = Navigation;

            }
            base.OnAppearing();
        }

        public void OnSoftBackButtonPressed()
        {
            var bindingContext = BindingContext as BaseViewModel;
            bindingContext?.OnSoftBackButtonPressed();
        }

        public bool NeedOverrideSoftBackButton { get; set; } = false;

        // this fire when back button pressed in android only
        //protected override bool OnBackButtonPressed()
        //{
        //    // If you want to continue going back
        //    //base.OnBackButtonPressed();

        //    var viewAwair = BindingContext as BaseViewModel;
        //    if (viewAwair != null)
        //    {
        //        viewAwair.OnBackButtonPressed();
        //    }
        //    // If you want to stop the back button
        //    return true;
        //}
    }
}
