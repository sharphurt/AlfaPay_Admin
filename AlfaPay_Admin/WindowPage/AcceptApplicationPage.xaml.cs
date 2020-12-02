using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AlfaPay_Admin.Model;
using Application = AlfaPay_Admin.Entity.Application;

namespace AlfaPay_Admin.WindowPage
{
    public partial class AcceptApplicationPage : Page
    {
        public AcceptApplicationPage(RegistrationModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
        }

        private void SearchTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == "Поиск по компаниям")
                SearchTextBox.Text = "";
            SearchIcon.Opacity = 0.7;
            SearchTextBox.Foreground = new SolidColorBrush(Color.FromRgb(11, 31, 53));
            LineUnderSearch.Stroke = new SolidColorBrush(Color.FromRgb(11, 31, 53));
        }

        private void SearchTextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
                SearchTextBox.Text = "Поиск по компаниям";
            SearchIcon.Opacity = 0.3;
            SearchTextBox.Foreground = new SolidColorBrush(Color.FromRgb(182, 188, 195));
            LineUnderSearch.Stroke = new SolidColorBrush(Color.FromRgb(182, 188, 195));
        }

        private void AcceptApplicationPage_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid.Focus();
        }
    }
}