using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AlfaPay_Admin.WindowPage
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel();
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
    }
}