using System.Windows;
using System.Windows.Controls;

namespace AlfaPay_Admin.CustomControl
{
    public partial class UserInfo : UserControl
    {
        public UserInfo()
        {
            InitializeComponent();
        }

        private void FrameworkElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(PatronymicLabel.Content.ToString()))
                PatronymicLabel.Content = "Нет отчества";
        }
    }
}