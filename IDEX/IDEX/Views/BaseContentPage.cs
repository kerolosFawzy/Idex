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
            var viewAwair = BindingContext as BaseViewModel; 
            if (viewAwair !=null)
            {
                viewAwair.OnAppearing(); 
            }
            base.OnAppearing();
        }
    }
}
