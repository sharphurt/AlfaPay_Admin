using System.Windows;
using System.Windows.Controls;

namespace AlfaPay_Admin.CustomControl
{
    public partial class InputTextBox : UserControl
    {
        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("InputTextBoxHeader", typeof(object), typeof(InputTextBox), new PropertyMetadata("Header"));

        public InputTextBox()
        {
            InitializeComponent();
        }
    }
}