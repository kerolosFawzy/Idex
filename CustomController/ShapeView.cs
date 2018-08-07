using System;
using Xamarin.Forms;

namespace CustomController
{
    public class ShapeView : BoxView
    {
        public static readonly BindableProperty ViewShapeTypeProperty = BindableProperty.Create(nameof(ViewShapeType), typeof(ShapeType), typeof(ShapeView), ShapeType.Box);

        public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create(nameof(StrokeColor) , typeof(Color) , typeof(ShapeView) , Color.FromHex("#ECECEC"));

        public static readonly BindableProperty StrokeWidthProperty = BindableProperty.Create(nameof(StrokeWidth) , typeof(float), typeof(ShapeView) , 1f);

        public static readonly BindableProperty IndicatorPercentageProperty = BindableProperty.Create(nameof(IndicatorPercentage), typeof(float), typeof(ShapeView), 0f);

        public new static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(ShapeView), 1f);

        public static readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(CornerRadius), typeof(Thickness), typeof(ShapeView), default(Thickness));

        #region setter and getter for class Properties 

        public ShapeType ViewShapeType
        {
            get { return (ShapeType)GetValue(ViewShapeTypeProperty); }
            set { SetValue(ViewShapeTypeProperty, value); }
        }

        public Color StrokeColor
        {
            get { return (Color)GetValue(StrokeColorProperty); }
            set { SetValue(StrokeColorProperty, value); }
        }

        public float StrokeWidth
        {
            get { return (float)GetValue(StrokeWidthProperty); }
            set { SetValue(StrokeWidthProperty, value); }
        }

        public float IndicatorPercentage
        {
            get { return (float)GetValue(IndicatorPercentageProperty); }
            set
            { 
                SetValue(IndicatorPercentageProperty, value);
            }
        }

        public new float CornerRadius
        {
            get { return (float)GetValue(CornerRadiusProperty); }
            set
            {
                if (ViewShapeType != ShapeType.Box)
                {
                    throw new ArgumentException("Can only specify this property with Box");
                }

                SetValue(CornerRadiusProperty, value);
            }
        }

        public Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }
        #endregion 

        public ShapeView()
        {
        }
    }
    public enum ShapeType
    {
        Box,
        Circle,
        CircleIndicator
    }
}
