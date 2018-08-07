using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using CustomController;
using IDEX.Droid;

[assembly: ExportRenderer(typeof(ShapeView), typeof(ShapeRenderer))]
namespace IDEX.Droid
{
    public class ShapeRenderer : ViewRenderer<ShapeView, Shape>
    {
#pragma warning disable CS0618 // Type or member is obsolete
        public ShapeRenderer()
        {}
#pragma warning restore CS0618 // Type or member is obsolete

        protected override void OnElementChanged(ElementChangedEventArgs<ShapeView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
                return;

            SetNativeControl(new Shape(Resources.DisplayMetrics.Density, Context)
            {
                ShapeView = Element
            });
        }
    }
}