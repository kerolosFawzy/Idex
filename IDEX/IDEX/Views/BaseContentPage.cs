﻿using IDEX.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDEX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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
        protected override bool OnBackButtonPressed()
        {
            // If you want to continue going back
            //base.OnBackButtonPressed();

            var viewAwair = BindingContext as BaseViewModel;
            if (viewAwair != null)
            {
                viewAwair.OnBackButtonPressed();
            }
            // If you want to stop the back button
            return true;
        }

        public static readonly BindableProperty FormattedTitleProperty =
            BindableProperty.Create(nameof(FormattedTitle), typeof(FormattedString), typeof(BaseContentPage), null);
        public FormattedString FormattedTitle
        {
            get { return (FormattedString)GetValue(FormattedTitleProperty); }
            set
            {
                SetValue(FormattedTitleProperty, value);
            }
        }

        public static readonly BindableProperty FormattedSubtitleProperty =
            BindableProperty.Create(nameof(FormattedSubtitle), typeof(FormattedString), typeof(BaseContentPage), null);
        public FormattedString FormattedSubtitle
        {
            get
            {
                return (FormattedString)GetValue(FormattedSubtitleProperty);
            }
            set
            {
                SetValue(FormattedSubtitleProperty, value);
            }
        }


        public static readonly BindableProperty SubtitleProperty =
            BindableProperty.Create(nameof(Subtitle), typeof(string), typeof(BaseContentPage), null);
        public string Subtitle
        {
            get { return (string)GetValue(SubtitleProperty); }
            set
            {
                SetValue(SubtitleProperty, value);
                
            }
        }

      
    }
}
