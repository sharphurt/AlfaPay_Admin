using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using AlfaPay_Admin.Model;
using Application = AlfaPay_Admin.Entity.Application;

namespace AlfaPay_Admin.WindowPage
{
    public partial class MainPage : Page
    {     
        private readonly ApplicationViewModel _applicationModel = new ApplicationViewModel();

        public MainPage()
        {
            InitializeComponent();
            _applicationModel.PropertyChanged += ApplicationModelOnPropertyChanged;
            DataContext = _applicationModel;
        }

        private void ApplicationModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "LoggedInUser" && _applicationModel.LoggedInUser == null)
            {
                var mainPage = new LoginPage();
                var navigationService = NavigationService;
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

        private void MainPage_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid.Focus();
        }

        private void ApplicationsListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            for (var i = 0; i < ApplicationsListBox.Items.Count; i++)
            {
                var myListBoxItem = (ListBoxItem) ApplicationsListBox.ItemContainerGenerator.ContainerFromIndex(i);
                var myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
                var myDataTemplate = myContentPresenter.ContentTemplate;
                var target = (Line) myDataTemplate.FindName("SelectionLine", myContentPresenter);
                target.Visibility = myListBoxItem.IsSelected ? Visibility.Visible : Visibility.Hidden;
            }
        }

        private TChildItem FindVisualChild<TChildItem>(DependencyObject obj) where TChildItem : DependencyObject
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child is TChildItem item)
                    return item;

                var childOfChild = FindVisualChild<TChildItem>(child);
                if (childOfChild != null)
                    return childOfChild;
            }

            return null;
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

            var registrationModel = new RegistrationModel(user, new CompanyModel(), selectedApplication);
            navigationService?.Navigate(new AcceptApplicationPage(registrationModel));
        }
    }
}