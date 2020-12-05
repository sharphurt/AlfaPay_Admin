using System.ComponentModel;
using System.Drawing;
using System.Windows.Controls;
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
            if (e.PropertyName.Equals("IsLoggedInSuccessfully"))
            {
                var mainPage = new MainPage();
                var navigationService = NavigationService;
                navigationService?.Navigate(mainPage);
            }
        }
    }
}