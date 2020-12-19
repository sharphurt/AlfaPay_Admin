using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AlfaPay_Admin.Annotations;
using static System.String;

namespace AlfaPay_Admin.CustomControl
{
    public partial class HintedInput : UserControl
    {
        public string Hint
        {
            get => (string) GetValue(HintProperty);
            set => SetValue(HintProperty, value);
        }

        public string SearchString
        {
            get => (string) GetValue(SearchStringProperty);
            set => SetValue(SearchStringProperty, value);
        }

        public string Icon
        {
            get => (string) GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty HintProperty =
            DependencyProperty.Register("Hint", typeof(string), typeof(InputTextBox));

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(InputTextBox));

        public static readonly DependencyProperty SearchStringProperty =
            DependencyProperty.Register("SearchString", typeof(string), typeof(InputTextBox));


        public HintedInput()
        {
            InitializeComponent();
            HintedInputWindow.DataContext = SearchString;
        }

        private void SearchTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == Hint)
                SearchTextBox.Text = Empty;
            SearchTextBox.Foreground = new SolidColorBrush(Color.FromRgb(11, 31, 53));
        }

        private void SearchTextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                SearchTextBox.Text = Hint;
                SearchString = "";
            }

            SearchTextBox.Foreground = new SolidColorBrush(Color.FromRgb(182, 188, 195));
        }

        private void SearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SearchString = SearchTextBox.Text == Hint ? "" : SearchTextBox.Text;
            HintedInputWindow.DataContext = SearchString.ToLower();
        }
    }
}