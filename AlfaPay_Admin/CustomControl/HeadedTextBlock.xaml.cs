using System.Windows;
using System.Windows.Controls;

namespace AlfaPay_Admin.CustomControl
{
    public partial class HeadedTextBlock : UserControl
    {
        public string TbHeader
        {
            get => (string) GetValue(TbHeaderProperty);
            set => SetValue(TbHeaderProperty, value);
        }
        
        public string TbContent
        {
            get => (string) GetValue(TbHeaderProperty);
            set => SetValue(TbHeaderProperty, value);
        }

        
        public static readonly DependencyProperty TbHeaderProperty =
            DependencyProperty.Register("TbHeader", typeof(string), typeof(HeadedTextBlock));

        public static readonly DependencyProperty TbContentProperty =
            DependencyProperty.Register("TbContent", typeof(string), typeof(HeadedTextBlock));

        public HeadedTextBlock()
        {
            InitializeComponent();
        }
    }
}