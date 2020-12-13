using System.ComponentModel;
using System.Runtime.CompilerServices;
using AlfaPay_Admin.Annotations;
using AlfaPay_Admin.Context;
using RestSharp;

namespace AlfaPay_Admin.Model
{
    public sealed class ServiceModel: INotifyPropertyChanged
    {
        private ApiRequestManager<string> _logoutRequestManager;

        public ApiRequestManager<string> LogoutRequestManager
        {
            get => _logoutRequestManager;
            set
            {
                _logoutRequestManager = value;
                OnPropertyChanged(nameof(LogoutRequestManager));
            }
        }
        
        private LoggedUser _loggedInUser;

        public LoggedUser LoggedInUser
        {
            get => _loggedInUser;
            set
            {
                _loggedInUser = value;
                OnPropertyChanged(nameof(LoggedInUser));
            }
        }
        
        private RelayCommand _logoutCommand;

        public RelayCommand LogoutCommand
        {
            get { return _logoutCommand ??= new RelayCommand(obj => { Logout(); }); }
        }

        public ServiceModel()
        {
            LoggedInUser = AuthenticationContext.LoggedUser;
            LogoutRequestManager = new ApiRequestManager<string>();
        }
        
        private void Logout()
        {
            LogoutRequestManager.MakeRequest(Method.POST, "auth/logout",
                new {deviceInfo = AuthenticationContext.DeviceInfo}, OnLogoutSuccessfully, null);
            /*() => ErrorMessage = LogoutRequestManager.Response.ToString());*/
        }

        private void OnLogoutSuccessfully()
        {
            AuthenticationContext.ClearAuthentication();   
            LoggedInUser = null;
        }
        
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}