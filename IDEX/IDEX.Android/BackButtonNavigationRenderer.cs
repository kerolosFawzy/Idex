using System;
using System.Reflection;
using Android.Content.Res;
using IDEX.Droid;
using IDEX.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using AToolbar = Android.Support.V7.Widget.Toolbar;


[assembly: ExportRenderer(typeof(NavigationPage), typeof(BackButtonNavigationRenderer))]

namespace IDEX.Droid
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class BackButtonNavigationRenderer : NavigationPageRenderer, Android.Views.View.IOnClickListener
    {

        #region nav toolbar

        private static FieldInfo _toolbarFieldInfo;

        public static FieldInfo ToolbarFieldInfo
        {
            get { return _toolbarFieldInfo; }
            set { _toolbarFieldInfo = value; }
        }

        
        private bool _disposed;
        private AToolbar _toolbar;
        
        static BackButtonNavigationRenderer()
        {
            ToolbarFieldInfo = typeof(NavigationPageRenderer).GetField("_toolbar",
                    BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public new void OnClick(Android.Views.View v)
        {
            // Call the NavigationPage which will trigger the default behavior
            // The default behavior is to navigate back if the Page derived classes return true from OnBackButtonPressed override            
            var curPage = Element.CurrentPage as BaseContentPage;
            if (curPage == null)
            {
                Element.PopAsync();
            }
            //else if(Element.RootPages.Contains(Element.CurrentPage.GetType()))
            //{
            //    Element.SendBackButtonPressed();
            //}
            else
            {
                if (curPage.NeedOverrideSoftBackButton)
                    curPage.OnSoftBackButtonPressed();
                else Element.PopAsync();
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            UpdateToolbarInstance();
        }

        protected override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            UpdateToolbarInstance();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;

                RemoveToolbarInstance();
            }

            base.Dispose(disposing);
        }

        private void UpdateToolbarInstance()
        {
            RemoveToolbarInstance();
            GetToolbarInstance();
        }

        private void GetToolbarInstance()
        {
            try
            {
                //sai o cho nay nay
                //how to get toolbar navigation page
                _toolbar = (AToolbar)ToolbarFieldInfo.GetValue(this);
                _toolbar.SetNavigationOnClickListener(this);
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine($"Can'tbar with error: {exception.Message}");
            }
        }

        private void RemoveToolbarInstance()
        {
            if (_toolbar == null) return;
            _toolbar.SetNavigationOnClickListener(null);
            _toolbar = null;
        }

        #endregion
    }
}
#pragma warning restore CS0618 // Type or member is obsolete