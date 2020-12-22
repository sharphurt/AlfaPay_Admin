using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using AlfaPay_Admin.Model;
using Application = AlfaPay_Admin.Entity.Application;

namespace AlfaPay_Admin.WindowPage
{
    public partial class MainPage : Page
    {
        private readonly ApplicationViewModel _applicationModel = new ApplicationViewModel();
        private readonly DispatcherTimer _errorPlateVisibilityTimer = new DispatcherTimer();

        public MainPage()
        {
            InitializeComponent();
            _applicationModel.PropertyChanged += ApplicationModelOnPropertyChanged;
            DataContext = _applicationModel;
            _errorPlateVisibilityTimer.Interval = TimeSpan.FromSeconds(5.5);
            _errorPlateVisibilityTimer.Tick += (o, args) =>
            {
                ErrorPlate.Visibility = Visibility.Hidden;
                _errorPlateVisibilityTimer.Stop();
            };
        }

        private void ApplicationModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ErrorMessage")
            {
                ErrorPlate.Visibility = Visibility.Visible;
                _errorPlateVisibilityTimer.Start();
            }
        }

        private void MainPage_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid.Focus();
        }

        private void AcceptButton_OnClick(object sender, RoutedEventArgs e)
        {
            var navigationService = NavigationService;
            var selectedApplication = ApplicationsListBox.SelectedItem as Application;
            var user = new ClientModel
            {
                Name = selectedApplication?.Name,
                Email = selectedApplication?.Email,
                Phone = selectedApplication?.Phone,
            };

            var registrationModel = new RegistrationModel(selectedApplication);
            var acceptApplicationPage = new AcceptApplicationPage(registrationModel);
            acceptApplicationPage.Return += (o, args) =>
            {
                if (args != null && args.Result)
                    RefreshButton.Command.Execute(null);
            };

            navigationService?.Navigate(acceptApplicationPage);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var url = "mailto:someone@somewhere.com?subject=Команда AlfaPay";
            Process.Start(url);
        }

        private void ApplicationsFiltersButton_OnClick(object sender, RoutedEventArgs e)
        {
            var openAnimation = FindResource("OpenApplicationFiltersPanel") as Storyboard;
            var closeAnimation = FindResource("CloseApplicationFiltersPanel") as Storyboard;
            if ((bool) ApplicationsFiltersButton.IsChecked)
                BeginStoryboard(openAnimation);
            else
                BeginStoryboard(closeAnimation);
        }

        private void UsersFiltersButton_OnClick(object sender, RoutedEventArgs e)
        {
            var openAnimation = FindResource("OpenUserFiltersPanel") as Storyboard;
            var closeAnimation = FindResource("CloseUserFiltersPanel") as Storyboard;
            if ((bool) UsersFiltersButton.IsChecked)
                BeginStoryboard(openAnimation);
            else
                BeginStoryboard(closeAnimation);
        }

        private void SortDirectionButton_OnClick(object sender, RoutedEventArgs e)
        {
            if ((bool) ((ToggleButton) sender).IsChecked)
                ((Label)((ToggleButton) sender).Content).Content = "Я → А";
            else
                ((Label)((ToggleButton) sender).Content).Content = "A → Я";
        }
    }
}