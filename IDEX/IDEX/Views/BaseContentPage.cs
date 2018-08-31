using CustomControls.NavigationServices;
using IDEX.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDEX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class BaseContentPage : ContentPage
    {
 
        protected override void OnAppearing()
        {
            var startColor = Color.FromHex("#008080");
            var endColor = Color.FromHex("#008080");
            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(startColor, endColor));
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(20));
            if (BindingContext is BaseViewModel viewAwair)
            {
                viewAwair.OnAppearing();
                viewAwair.baseContentPage = this;
            }
            base.OnAppearing();
        }

        public void OnSoftBackButtonPressed()
        {
            var bindingContext = BindingContext as BaseViewModel;
            bindingContext?.OnSoftBackButtonPressed();
        }

        public bool NeedOverrideSoftBackButton { get; set; } = false;


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
