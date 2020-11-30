using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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


        private void SearchTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (searchTextBox.Text == "Поиск по компаниям")
                searchTextBox.Text = "";
            searchTextBox.Focus();
        }
    }
}