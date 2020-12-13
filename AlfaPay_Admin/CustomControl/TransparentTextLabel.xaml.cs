using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using VerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment;

namespace AlfaPay_Admin.CustomControl
{
  public class HollowTextBlock : FrameworkElement
    {
        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(HollowTextBlock),
                new FrameworkPropertyMetadata(string.Empty,
                    FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure,
                    OnTextChanged, CoerceText));

        public Brush Background
        {
            get => (Brush) GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public static readonly DependencyProperty BackgroundProperty =
            TextElement.BackgroundProperty.AddOwner(typeof(HollowTextBlock),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public double FontSize
        {
            get => (double) GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public static readonly DependencyProperty FontSizeProperty =
            TextElement.FontSizeProperty.AddOwner(typeof(HollowTextBlock));

        public FontFamily FontFamily
        {
            get => (FontFamily) GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public static readonly DependencyProperty FontFamilyProperty =
            TextElement.FontFamilyProperty.AddOwner(typeof(HollowTextBlock));

        public FontStyle FontStyle
        {
            get => (FontStyle) GetValue(FontStyleProperty);
            set => SetValue(FontStyleProperty, value);
        }

        public static readonly DependencyProperty FontStyleProperty =
            TextElement.FontStyleProperty.AddOwner(typeof(HollowTextBlock));

        public FontWeight FontWeight
        {
            get => (FontWeight) GetValue(FontWeightProperty);
            set => SetValue(FontWeightProperty, value);
        }

        public static readonly DependencyProperty FontWeightProperty =
            TextElement.FontWeightProperty.AddOwner(typeof(HollowTextBlock));

        public FontStretch FontStretch
        {
            get => (FontStretch) GetValue(FontStretchProperty);
            set => SetValue(FontStretchProperty, value);
        }

        public static readonly DependencyProperty FontStretchProperty =
            TextElement.FontStretchProperty.AddOwner(typeof(HollowTextBlock));

        public TextAlignment TextAlignment
        {
            get => (TextAlignment) GetValue(TextAlignmentProperty);
            set => SetValue(TextAlignmentProperty, value);
        }

        public static readonly DependencyProperty TextAlignmentProperty =
            Block.TextAlignmentProperty.AddOwner(typeof(HollowTextBlock));

        public VerticalAlignment VerticalTextAlignment
        {
            get => (VerticalAlignment) GetValue(VerticalTextAlignmentProperty);
            set => SetValue(VerticalTextAlignmentProperty, value);
        }

        public static readonly DependencyProperty VerticalTextAlignmentProperty =
            DependencyProperty.Register("VerticalTextAlignment", typeof(VerticalAlignment), typeof(HollowTextBlock),
                new FrameworkPropertyMetadata(System.Windows.Forms.VisualStyles.VerticalAlignment.Top, FrameworkPropertyMetadataOptions.AffectsRender));

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OnTextChanged(d, (string) e.NewValue);
        }

        private static void OnTextChanged(DependencyObject d, string newText)
        {
        }

        private static object CoerceText(DependencyObject d, object baseValue)
        {
            return baseValue;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var face = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);
            var size = FontSize;
            var ft = new FormattedText(Text, Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, face,
                size, Brushes.Black);
            return new Size(ft.Width, ft.Height);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            var extent = new RectangleGeometry(new Rect(0.0, 0.0, RenderSize.Width, RenderSize.Height));
            var face = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);
            var size = FontSize;
            var ft = new FormattedText(Text, Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, face,
                size, Brushes.Black);
            var originX = GetHorizontalOrigin(ft.Width, RenderSize.Width);
            var originY = GetVerticalOrigin(ft.Height, RenderSize.Height);
            var hole = ft.BuildGeometry(new Point(originX, originY));
            var combined = new CombinedGeometry(GeometryCombineMode.Exclude, extent, hole);
            drawingContext.PushClip(combined);
            drawingContext.DrawRectangle(Background, null, new Rect(0.0, 0.0, RenderSize.Width, RenderSize.Height));
            drawingContext.Pop();
        }

        private double GetHorizontalOrigin(double textWidth, double renderWidth)
        {
            switch (TextAlignment)
            {
                case TextAlignment.Center:
                    return (renderWidth - textWidth) / 2;
                case TextAlignment.Left:
                    return 0;
                case TextAlignment.Right:
                    return renderWidth - textWidth;
            }

            return 0;
        }

        private double GetVerticalOrigin(double textHeight, double renderHeight)
        {
            switch (VerticalTextAlignment)
            {
                case System.Windows.Forms.VisualStyles.VerticalAlignment.Center:
                    return (renderHeight - textHeight) / 2;
                case System.Windows.Forms.VisualStyles.VerticalAlignment.Top:
                    return 0;
                case System.Windows.Forms.VisualStyles.VerticalAlignment.Bottom:
                    return renderHeight - textHeight;
            }

            return 0;
        }
    }
}