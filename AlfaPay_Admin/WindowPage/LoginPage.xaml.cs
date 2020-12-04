using System.Windows.Controls;
using AlfaPay_Admin.Model;

namespace AlfaPay_Admin.WindowPage
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            DataContext = new LoginModel();
        }
    }
}