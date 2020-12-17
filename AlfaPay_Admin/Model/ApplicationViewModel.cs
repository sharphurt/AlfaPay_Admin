﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Web.UI.WebControls;
using AlfaPay_Admin.Annotations;
using AlfaPay_Admin.Context;
using AlfaPay_Admin.Entity;
using Flurl.Http;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Application = AlfaPay_Admin.Entity.Application;

namespace AlfaPay_Admin.Model
{
    public sealed class ApplicationViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Application> _newApplications;

        public ObservableCollection<Application> NewApplications
        {
            get => _newApplications;
            private set
            {
                _newApplications = value;
                OnPropertyChanged(nameof(NewApplications));
            }
        }

        private ObservableCollection<Application> _rejectedApplications;

        public ObservableCollection<Application> RejectedApplications
        {
            get => _rejectedApplications;
            private set
            {
                _rejectedApplications = value;
                OnPropertyChanged(nameof(RejectedApplications));
            }
        }

        private ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get => _users;
            private set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        private int _selectedIndex;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set => _selectedIndex = value;
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


        private RelayCommand _refreshCommand;

        public RelayCommand RefreshCommand
        {
            get { return _refreshCommand ??= new RelayCommand(obj => { GetNewApplicationsFromServer(0, 10); }); }
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
            GetNewApplicationsFromServer(0, 100);
            GetRejectedApplicationsFromServer(0, 100);
            GetUsersFromServer(0, 100);
        }

        private void GetNewApplicationsFromServer(int from, int count)
        {
            GetApplicationsRequestManager.MakeRequest(Method.GET, $"applications/new/get?from={from}&count={count}",
                null,
                () => NewApplications = GetApplicationsRequestManager.Response.Response,
                () => ErrorMessage = GetApplicationsRequestManager.Response.ToString());
        }

        private void GetRejectedApplicationsFromServer(int from, int count)
        {
            GetApplicationsRequestManager.MakeRequest(Method.GET,
                $"applications/rejected/get?from={from}&count={count}", null,
                () => RejectedApplications = GetApplicationsRequestManager.Response.Response,
                () => ErrorMessage = GetApplicationsRequestManager.Response.ToString());
        }

        private void GetUsersFromServer(int from, int count)
        {
            UsersRequestManager.MakeRequest(Method.GET, $"users/get?from={from}&count={count}", null,
                () =>
                {
                    Users = new ObservableCollection<User>();
                    foreach (var user in UsersRequestManager.Response.Response)
                    {
                        Users.Add(user);
                        Users.Add(user);
                    }
                },
                () => ErrorMessage = GetApplicationsRequestManager.Response.ToString());
        }


        private void RejectApplication(long id)
        {
            RejectApplicationRequestManager.MakeRequest(Method.POST, $"applications/reject?id={id}", null,
                () =>
                {
                    GetNewApplicationsFromServer(0, 10);
                    GetRejectedApplicationsFromServer(0, 10);
                },
                () => ErrorMessage = RejectApplicationRequestManager.Response.ToString());
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