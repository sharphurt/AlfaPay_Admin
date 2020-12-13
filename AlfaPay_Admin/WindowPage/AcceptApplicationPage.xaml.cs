using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;
using AlfaPay_Admin.Context;
using AlfaPay_Admin.Model;
using Application = AlfaPay_Admin.Entity.Application;

namespace AlfaPay_Admin.WindowPage
{
    public partial class AcceptApplicationPage : PageFunction<bool>
    {
        private readonly DispatcherTimer _errorPlateVisibilityTimer = new DispatcherTimer ();

        public AcceptApplicationPage(RegistrationModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            
            dataContext.PropertyChanged += RegistrationModel_OnPropertyChanged;
            _errorPlateVisibilityTimer.Interval = TimeSpan.FromSeconds(5.5);
            _errorPlateVisibilityTimer.Tick += (o, args) =>
            {
                ErrorPlate.Visibility = Visibility.Hidden;
                _errorPlateVisibilityTimer.Stop();
            };
        }

        private void RegistrationModel_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ErrorMessage")
            {
                ErrorPlate.Visibility = Visibility.Visible;
                _errorPlateVisibilityTimer.Start();
            }

            if (e.PropertyName == "IsSuccessfully")
                OnReturn(new ReturnEventArgs<bool>(true));
        }

        private void AcceptApplicationPage_OnMouseDown(object sender, MouseButtonEventArgs e) => Grid.Focus();
        
        private void AutocompleteListBox_OnSelected(object sender, RoutedEventArgs e)
        {
            AddressTextBox.Text = AutocompleteListBox.SelectedItem as string ?? AddressTextBox.Text;
            if (AutocompleteListBox.Items.Count == 1)
                PopupNonTopmost.IsOpen = false;
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