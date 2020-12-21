using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Web.UI.WebControls;
using System.Windows.Controls;
using System.Windows.Data;
using AlfaPay_Admin.Annotations;
using AlfaPay_Admin.Context;
using AlfaPay_Admin.Entity;
using AlfaPay_Admin.Enum;
using Flurl.Http;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Application = AlfaPay_Admin.Entity.Application;

namespace AlfaPay_Admin.Model
{
    public sealed class ApplicationViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Application> _applications;

        public ObservableCollection<Application> Applications
        {
            get => _applications;
            private set
            {
                _applications = value;
                ApplicationsView = CollectionViewSource.GetDefaultView(Applications);
                OnPropertyChanged(nameof(Applications));
            }
        }

        private ICollectionView _applicationsView;

        public ICollectionView ApplicationsView
        {
            get => _applicationsView;
            set
            {
                _applicationsView = value;
                OnPropertyChanged(nameof(ApplicationsView));
            }
        }

        private ComboBoxItem _applicationsSortMethod;

        public ComboBoxItem ApplicationsSortMethod
        {
            get => _applicationsSortMethod;
            set
            {
                _applicationsSortMethod = value;
                SortApplications(_applicationsSortMethod.Content.ToString());
                OnPropertyChanged(nameof(ApplicationsSortMethod));
            }
        }

        private ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get => _users;
            private set
            {
                _users = value;
                UsersView = CollectionViewSource.GetDefaultView(Users);
                OnPropertyChanged(nameof(Users));
            }
        }

        private ICollectionView _usersView;

        public ICollectionView UsersView
        {
            get => _usersView;
            set
            {
                _usersView = value;
                OnPropertyChanged(nameof(UsersView));
            }
        }

        private string _applicationsSearch;

        public string ApplicationsSearch
        {
            get => _applicationsSearch;
            set
            {
                _applicationsSearch = value;
                FilterApplications(_applicationsSearch);
                OnPropertyChanged(nameof(ApplicationsSearch));
            }
        }

        private ApplicationFilter _applicationFilter;

        public ApplicationFilter ApplicationFilter
        {
            get => _applicationFilter;
            set
            {
                _applicationFilter = value;
                OnPropertyChanged(nameof(ApplicationFilter));
            }
        }


        private string _usersSearch;

        public string UsersSearch
        {
            get => _usersSearch;
            set
            {
                _usersSearch = value;
                FilterUsers(_usersSearch);
                OnPropertyChanged(nameof(UsersSearch));
            }
        }

        private Application _selectedApplication;

        public Application SelectedApplication
        {
            get => _selectedApplication;
            set
            {
                _selectedApplication = value;
                OnPropertyChanged(nameof(SelectedApplication));
            }
        }

        private User _selectedUser;

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        private RelayCommand _refreshCommand;

        public RelayCommand RefreshCommand
        {
            get { return _refreshCommand ??= new RelayCommand(obj => { GetApplicationsFromServer(0, 10); }); }
        }

        private RelayCommand _rejectCommand;

        public RelayCommand RejectCommand
        {
            get { return _rejectCommand ??= new RelayCommand(obj => { RejectApplication(SelectedApplication.Id); }); }
        }

        private RelayCommand _sendMessageCommand;

        public RelayCommand SendMessageCommand
        {
            get { return _sendMessageCommand ??= new RelayCommand(obj => { SendEmail(); }); }
        }

        private ApiRequestManager<ObservableCollection<Application>> _getApplicationsRequestManager;

        public ApiRequestManager<ObservableCollection<Application>> GetApplicationsRequestManager
        {
            get => _getApplicationsRequestManager;
            set
            {
                _getApplicationsRequestManager = value;
                OnPropertyChanged(nameof(GetApplicationsRequestManager));
            }
        }

        private ApiRequestManager<string> _rejectApplicationRequestManager;

        public ApiRequestManager<string> RejectApplicationRequestManager
        {
            get => _rejectApplicationRequestManager;
            set
            {
                _rejectApplicationRequestManager = value;
                OnPropertyChanged(nameof(RejectApplicationRequestManager));
            }
        }


        private ApiRequestManager<ObservableCollection<User>> _usersRequestManager;

        public ApiRequestManager<ObservableCollection<User>> UsersRequestManager
        {
            get => _usersRequestManager;
            set
            {
                _usersRequestManager = value;
                OnPropertyChanged(nameof(UsersRequestManager));
            }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ApplicationViewModel()
        {
            GetApplicationsRequestManager = new ApiRequestManager<ObservableCollection<Application>>();
            RejectApplicationRequestManager = new ApiRequestManager<string>();
            UsersRequestManager = new ApiRequestManager<ObservableCollection<User>>();
            ApplicationFilter = new ApplicationFilter(true, false, false);
            ApplicationFilter.PropertyChanged += (sender, args) => FilterApplications(ApplicationsSearch); 
            GetApplicationsFromServer(0, 100);
            GetUsersFromServer(0, 100);
        }

        private void ApplicationsOnSuccessfulLoading()
        {
            Applications = GetApplicationsRequestManager.Response.Response;
            FilterApplications(ApplicationsSearch); 
            SortApplications(ApplicationsSortMethod.Content.ToString());
        }

        private void GetApplicationsFromServer(int from, int count)
        {
            GetApplicationsRequestManager.MakeRequest(Method.GET, $"applications/get?from={from}&count={count}",
                null, ApplicationsOnSuccessfulLoading,
                () => ErrorMessage = GetApplicationsRequestManager.Response.ToString());
        }

        private void GetUsersFromServer(int from, int count)
        {
            UsersRequestManager.MakeRequest(Method.GET, $"users/get?from={from}&count={count}", null,
                () => Users = UsersRequestManager.Response.Response,
                () => ErrorMessage = GetApplicationsRequestManager.Response.ToString());
        }

        private void RejectApplication(long id)
        {
            RejectApplicationRequestManager.MakeRequest(Method.POST, $"applications/reject?id={id}", null,
                () => { GetApplicationsFromServer(0, 10); },
                () => ErrorMessage = RejectApplicationRequestManager.Response.ToString());
        }

        private void FilterApplications(string searchString)
        {
            if (GetApplicationsRequestManager.Status != RequestStatus.CompletedSuccessfully)
                return;
            ApplicationsView.Filter =
                o => o is Application application && application.MatchToSearchString(searchString) &&
                     application.MatchToFilter(ApplicationFilter);
        }

        private void FilterUsers(string searchString)
        {
            UsersView.Filter =
                o => o is User user && user.MatchToSearchString(searchString);
        }

        private void SortApplications(string sortMethod)
        {
            ApplicationsView.SortDescriptions.Clear();
            switch (sortMethod)
            {
                case "По имени":
                    ApplicationsView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                    break;
                case "По email":
                    ApplicationsView.SortDescriptions.Add(new SortDescription("Email", ListSortDirection.Ascending));
                    break;
                case "По ИНН":
                    ApplicationsView.SortDescriptions.Add(new SortDescription("Inn", ListSortDirection.Ascending));
                    break;
                case "По статусу":
                    ApplicationsView.SortDescriptions.Add(new SortDescription("Status", ListSortDirection.Ascending));
                    break;
                case "По дате подачи":
                    ApplicationsView.SortDescriptions.Add(new SortDescription("CreatedAt", ListSortDirection.Ascending));
                    break;
            }

            ApplicationsView.Refresh();
        }

        public void SendEmail()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}