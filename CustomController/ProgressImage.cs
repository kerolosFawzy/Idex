using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CustomController
{
    class ProgressImage : BoxView
    {
        public static readonly BindableProperty ViewShapeTypeProperty = BindableProperty.Create(nameof(ViewShapeType), typeof(ShapeType), typeof(ProgressImage), ShapeType.Box);

        public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create(nameof(StrokeColor) , typeof(Color) , typeof(ProgressImage) , Color.FromHex("#ECECEC"));

        public static readonly BindableProperty StrokeWidthProperty = BindableProperty.Create(nameof(StrokeWidth) , typeof(float), typeof(ProgressImage) , 1f);

        public static readonly BindableProperty IndicatorPercentageProperty = BindableProperty.Create(nameof(IndicatorPercentage), typeof(float), typeof(ProgressImage), 0f);

        public new static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(ProgressImage), 1f);

        public static readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(CornerRadius), typeof(Thickness), typeof(ProgressImage), default(Thickness));


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

        public ProgressImage()
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
