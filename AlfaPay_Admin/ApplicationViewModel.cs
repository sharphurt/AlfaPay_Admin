using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AlfaPay_Admin.Annotations;
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
        
        

        private ApplicationContext db = new ApplicationContext("http://localhost:8080/");
        public bool IsLoadedSuccessfully;


        public ApplicationViewModel()
        {
            Applications = new ObservableCollection<ClientApplication>();
            LoadApplications(0, 10);
        }

        public async void LoadApplications(int from, int count)
        {
            var result = await db.SendApiRequest($"api/applications/get?from={from}&count={count}", null);
            if (result == "Error")
            {
                IsLoadedSuccessfully = false;
            }

            var response = Deserializer.DeserializeApiResponse<List<ClientApplication>>(result);
            if (response.Response != null)
            {
                Applications = new ObservableCollection<ClientApplication>(response.Response);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}