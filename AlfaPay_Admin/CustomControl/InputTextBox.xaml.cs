using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AlfaPay_Admin.CustomControl
{
    public partial class InputTextBox : UserControl, IDataErrorInfo
    {
        public string Header
        {
            get => (string) GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public string Input
        {
            get => (string) GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(InputTextBox));

        public static readonly DependencyProperty InputProperty =
            DependencyProperty.Register("Input", typeof(string), typeof(InputTextBox),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public InputTextBox()
        {
            InitializeComponent();
        }

        public string this[string columnName]
        {
            get
            {
                var be = BindingOperations.GetBindingExpression(this, InputProperty);
                return be != null && be.HasError ? "Input has Error" : null;
            }
        }

        public string Error { get; }
    }
}