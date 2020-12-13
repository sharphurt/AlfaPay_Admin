using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Input;
using AlfaPay_Admin.Model;

namespace AlfaPay_Admin.WindowPage
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            var loginModel = new LoginModel();
            DataContext = loginModel;
            loginModel.PropertyChanged += LoginModelOnPropertyChanged;
        }

        private void LoginModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("LoggedInUser") )
            {
                var mainPage = new MainPage();
                var navigationService = NavigationService;
                navigationService?.Navigate(mainPage);
            }
        }

        private void LoginPage_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            MainGrid.Focus();
        }

        private void Input_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ErrorTextBlock.Text = "";
        }
    }
}