using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace CustomController
{
    public class CirclePieChart : ContentView
    {
        public static readonly BindableProperty SegmentsProperty =
            BindableProperty.Create(nameof(Segments)
                , typeof(IEnumerable<Segment>)
                , typeof(CirclePieChart)
                , propertyChanged: OnSegmentsPropertyChanged);
#pragma warning disable IDE0044 // Add readonly modifier
        SKSvg svg = new SKSvg();
#pragma warning restore IDE0044 // Add readonly modifier
        private static void OnSegmentsPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var circle = (CirclePieChart)bindable;

            var observableCollection = newvalue as ObservableCollection<Segment>;
            if (observableCollection != null)
            {
                observableCollection.CollectionChanged += circle.OnSegmentsCollectionChanged;
            }
            observableCollection = oldvalue as ObservableCollection<Segment>;
            if (observableCollection != null)
            {
                observableCollection.CollectionChanged -= circle.OnSegmentsCollectionChanged;
            }
            if (newvalue != null)
            {
                foreach (var segment in (IEnumerable<Segment>)newvalue)
                {
                    segment.Parent = circle;
                }
            }
            ((SKCanvasView)circle.Content).InvalidateSurface();
        }


        private void OnSegmentsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Segment segment in e.NewItems)
                {
                    segment.Parent = this;
                }
            }
            ((SKCanvasView)Content).InvalidateSurface();
        }

        public IList<Segment> Segments
        {
            get => (IList<Segment>)GetValue(SegmentsProperty);
            set => SetValue(SegmentsProperty, value);
        }

        public CirclePieChart()
        {
            var canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = canvasView;
        }

       

        private  Stream GetImageStream(string svgName)
        {
            var assembly = GetType().Assembly.GetManifestResourceStream($"CustomController.SvgImages.{svgName}");
            return assembly;
        }



        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            SKPoint center = new SKPoint(info.Width / 2F, info.Height / 2F);
            var startAngle = -90F;
            if (Segments == null)
                return;
            foreach (var segment in Segments)
            {
                var radius = Math.Min(info.Width / 2, info.Height / 2) * segment.Radius;
                var rect = new SKRect(center.X - radius, center.Y - radius,
                    center.X + radius, center.Y + radius);

                if (segment.SweepAngle == 360)
                {

                    using (var stream = GetImageStream("idex_accepted.svg")) {
                        if(stream!=null)
                            svg.Load(stream);
                    }
                    float canvasMin = Math.Min(info.Width, info.Height);
                    float svgMax = Math.Max(svg.Picture.CullRect.Width, svg.Picture.CullRect.Height);
                    float scale = canvasMin / svgMax;
                    var matrix = SKMatrix.MakeScale(scale, scale);
                    
                    canvas.Translate(0, canvasMin/5);
                    canvas.DrawPicture(svg.Picture ,ref matrix);      

                }
                else
                {
                    using (var path = new SKPath())
                    using (var fillPaint = new SKPaint())
                    {
                        fillPaint.Style = SKPaintStyle.Fill;
                        fillPaint.Color = segment.Color.ToSKColor();

                        path.MoveTo(center);
                        // ReSharper disable once CompareOfFloatsByEqualityOperator
                        path.ArcTo(rect, startAngle, segment.SweepAngle == 360F ? 359.99F : segment.SweepAngle, false);
                        startAngle += segment.SweepAngle;
                        path.Close();
                        canvas.DrawPath(path, fillPaint);
                    }
                }
            }
        }

    }

    public class Segment : BindableObject
    {

        public CirclePieChart Parent { get; set; }

        public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(Segment), Color.ForestGreen, propertyChanged: OnSegmentPropertyChanged);
        public static readonly BindableProperty SweepAngleProperty = BindableProperty.Create(nameof(SweepAngle), typeof(float), typeof(Segment), 90F, propertyChanged: OnSegmentPropertyChanged);
        public static readonly BindableProperty RadiusProperty = BindableProperty.Create(nameof(Radius), typeof(float), typeof(Segment), 1F, propertyChanged: OnSegmentPropertyChanged);

        private static void OnSegmentPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var segment = (Segment)bindable;
            ((SKCanvasView)segment?.Parent?.Content)?.InvalidateSurface();
        }


        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public float SweepAngle
        {
            get => (float)GetValue(SweepAngleProperty);
            set => SetValue(SweepAngleProperty, value);
        }
        public float Radius
        {
            get => (float)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }
    }
}
