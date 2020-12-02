using System.Windows;
using System.Windows.Controls;

namespace AlfaPay_Admin.CustomControl
{
    public partial class HeadedLabel : UserControl
    {
        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public object Body
        {
            get => GetValue(BodyProperty);
            set => SetValue(BodyProperty, value);
        }
        
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("HeadedLabelHeader", typeof(object), typeof(InputTextBox), new PropertyMetadata("Header"));

        public static readonly DependencyProperty BodyProperty =
            DependencyProperty.Register("HeadedLabelBody", typeof(object), typeof(InputTextBox), new PropertyMetadata("Body"));

        
        public HeadedLabel()
        {
            InitializeComponent();
        }
    }
}