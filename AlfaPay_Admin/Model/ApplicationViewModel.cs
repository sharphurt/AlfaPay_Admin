﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
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
        private ObservableCollection<Application> _applications;
        private Application _selectedApplication;

        public ObservableCollection<Application> Applications
        {
            get => _applications;
            private set
            {
                _applications = value;
                OnPropertyChanged(nameof(Applications));
            }
        }

        private int _selectedIndex;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set => _selectedIndex = value;
        }

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
            get { return _refreshCommand ??= new RelayCommand(obj => { GetApplicationsFromServer(0, 10); }); }
        }

        private RelayCommand _rejectCommand;

        public RelayCommand RejectCommand
        {
            get { return _rejectCommand ??= new RelayCommand(obj => { RejectApplication(SelectedApplication.Id); }); }
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
            GetApplicationsFromServer(0, 10);
        }

        private void GetApplicationsFromServer(int from, int count)
        {
            GetApplicationsRequestManager.MakeRequest(Method.GET, $"applications/get?from={from}&count={count}", null,
                () => Applications = GetApplicationsRequestManager.Response.Response,
                () => ErrorMessage = GetApplicationsRequestManager.Response.ToString());
        }

        private void RejectApplication(long id)
        {
            RejectApplicationRequestManager.MakeRequest(Method.POST, $"applications/reject?id={id}", null,
                () => GetApplicationsFromServer(0, 10),
                () => ErrorMessage = RejectApplicationRequestManager.Response.ToString());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}