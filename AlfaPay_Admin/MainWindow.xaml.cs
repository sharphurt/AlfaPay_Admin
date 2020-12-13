using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using AlfaPay_Admin.Context;
using AlfaPay_Admin.Model;
using AlfaPay_Admin.WindowPage;

namespace AlfaPay_Admin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnServiceModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "LoggedInUser" && AuthenticationContext.LoggedUser == null)
            {
                var mainPage = new LoginPage();
                var navigationService = MainFrame.NavigationService;
                navigationService?.Navigate(mainPage);
            }
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

        private void MainWindow_OnLoaded(object sender, EventArgs args)
        {
            MinWidth = Width;
            MinHeight = Height;
        }

        private void MainFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            var serviceModel = new ServiceModel();
            SearchPanel.DataContext = serviceModel;
            serviceModel.PropertyChanged += OnServiceModelPropertyChanged;
            SearchPanel.Visibility = MainFrame.Content.GetType() == typeof(LoginPage)
                ? Visibility.Collapsed
                : Visibility.Visible;
        }
    }
}