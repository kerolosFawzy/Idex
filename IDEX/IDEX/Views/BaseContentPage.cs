using CustomControls.NavigationServices;
using IDEX.ViewModel;
using ReactiveUI.XamForms;
using System.Reactive.Disposables;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDEX.Views
{
    public class BaseContentPage<TViewModel> : ReactiveContentPage<TViewModel> where TViewModel : class
    {
        protected readonly CompositeDisposable SubscriptionDisposables = new CompositeDisposable();


        protected override void OnAppearing()
        {
           // CustomNavigationPage.SetBarBackground(this, Color.FromHex("008080"));

            //CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(20));
            if (BindingContext is BaseViewModel viewAwair)
            {
                viewAwair.OnAppearing();
            }

            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            if (BindingContext is BaseViewModel viewAwair)
            {
                viewAwair.DisAppearing();
            }

            SubscriptionDisposables.Clear();

            base.OnDisappearing();
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

        /*
         * old code not needed 
    #region Title and subTitle

    public static readonly BindableProperty FormattedTitleProperty =
         BindableProperty.Create(nameof(FormattedTitle), typeof(FormattedString), typeof(ContentPage), null);
    public FormattedString FormattedTitle
    {
        get { return (FormattedString)GetValue(FormattedTitleProperty); }
        set
        {
            SetValue(FormattedTitleProperty, value);
        }
    }

    public static readonly BindableProperty FormattedSubtitleProperty =
        BindableProperty.Create(nameof(FormattedSubtitle), typeof(FormattedString), typeof(ContentPage), null);
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
        BindableProperty.Create(nameof(Subtitle), typeof(string), typeof(ContentPage), null);
    public string Subtitle
    {
        get { return (string)GetValue(SubtitleProperty); }
        set
        {
            SetValue(SubtitleProperty, value);
        }
    }
    #endregion
    */
    }
}
