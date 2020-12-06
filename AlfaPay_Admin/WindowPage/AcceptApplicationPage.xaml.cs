using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AlfaPay_Admin.Context;
using AlfaPay_Admin.Model;
using Application = AlfaPay_Admin.Entity.Application;

namespace AlfaPay_Admin.WindowPage
{
    public partial class AcceptApplicationPage : Page
    {
        private static FieldInfo _menuDropAlignmentField;

        public AcceptApplicationPage(RegistrationModel dataContext)
        {
            _menuDropAlignmentField =
                typeof(SystemParameters).GetField("_menuDropAlignment", BindingFlags.NonPublic | BindingFlags.Static);
            System.Diagnostics.Debug.Assert(_menuDropAlignmentField != null);

            EnsureStandardPopupAlignment();
            SystemParameters.StaticPropertyChanged += SystemParameters_StaticPropertyChanged;

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

        private void AcceptApplicationPage_OnMouseDown(object sender, MouseButtonEventArgs e) => Grid.Focus();

        private static void SystemParameters_StaticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            EnsureStandardPopupAlignment();
        }

        private static void EnsureStandardPopupAlignment()
        {
            if (SystemParameters.MenuDropAlignment && _menuDropAlignmentField != null)
                _menuDropAlignmentField.SetValue(null, false);
        }

        private void Selector_OnSelected(object sender, RoutedEventArgs e)
        {
            AddressTextBox.Text = AutocompleteListBox.SelectedItem as string ?? AddressTextBox.Text;
        }

        private void AddressTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            PopupNonTopmost.IsOpen = true;
        }

        private void ContinueButton_OnClick(object sender, RoutedEventArgs e)
        {
            ApplyWindow.Visibility = Visibility.Visible;
            ApplyWindow.Opacity = 0;
        }

        private void ApplyButton_OnClick(object sender, RoutedEventArgs e)
        {
            ApplyWindow.IsEnabled = false;
        }

        private void ApplyWindowCloseAnimation_OnCompleted(object sender, EventArgs e)
        {
            ApplyWindow.Visibility = Visibility.Hidden;
            ApplyWindow.IsEnabled = true;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            var navigationService = NavigationService;
            navigationService?.GoBack();
        }
        
        private void CloseWindowButton_OnClick(object sender, RoutedEventArgs e)
        {
            ApplyWindow.IsEnabled = false;
        }
    }
}