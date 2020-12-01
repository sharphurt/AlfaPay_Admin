using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using AlfaPay_Admin.Annotations;
using AlfaPay_Admin.Context;
using AlfaPay_Admin.Model;

namespace AlfaPay_Admin
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ClientApplication> _applications;
        private ClientApplication _selectedApplication;

        public ObservableCollection<ClientApplication> Applications
        {
            get => _applications;
            private set
            {
                _applications = value;
                OnPropertyChanged(nameof(Applications));
            }
        }

        public ClientApplication SelectedApplication
        {
            get => _selectedApplication;
            set
            {
                _selectedApplication = value;
                OnPropertyChanged(nameof(SelectedApplication));
            }
        }

        private bool _responseReceived;

        public bool ResponseReceived
        {
            get => _responseReceived;
            set
            {
                _responseReceived = value;
                OnPropertyChanged(nameof(ResponseReceived));
            }
        }

        private ApiError _error;

        public ApiError Error
        {
            get => _error;
            set
            {
                _error = value;
                OnPropertyChanged(nameof(Error));
            }
        }

        private RelayCommand _refreshCommand;

        public RelayCommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = new RelayCommand(obj =>
                {
                    GetApplicationsFromServer(0, 10);
                }));
            }
        }


        private ApplicationContext db = new ApplicationContext("http://localhost:8080/");


        public ApplicationViewModel()
        {
            Applications = new ObservableCollection<ClientApplication>();
            GetApplicationsFromServer(0, 10);
        }

        private async void GetApplicationsFromServer(int from, int count)
        {
            ResponseReceived = false;
            Error = null;
            Applications = new ObservableCollection<ClientApplication>();
            var result = await db.LoadApplications(from, count);
            if (result.Error != null)
            {
                Error = result.Error;
                ResponseReceived = true;
            }
            else
            {
                Applications = result.Response;
                Error = null;
                ResponseReceived = true;
            }
        }

        /*
        private async void CheckApplicationDataCorrectness(ClientApplication application)
        {
            var isEmailCorrect = await db.CheckProperty("email", application.Email);
            var isPhoneCorrect = await db.CheckProperty("phone", application.Email);
            var isInnCorrect = await db.CheckProperty("inn", application.Email);
        }
        */

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}