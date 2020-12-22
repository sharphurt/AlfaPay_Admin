using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AlfaPay_Admin.Annotations;

namespace AlfaPay_Admin.Entity
{
    public sealed class UserFilter : INotifyPropertyChanged
    {
        private bool _includeAdmins;

        public bool IncludeAdmins
        {
            get => _includeAdmins;
            set
            {
                _includeAdmins = value;
                OnPropertyChanged(nameof(IncludeAdmins));
            }
        }

        private bool _includeClients;

        public bool IncludeClients
        {
            get => _includeClients;
            set
            {
                _includeClients = value;
                OnPropertyChanged(nameof(IncludeClients));
            }
        }

        private bool _includeBanned;

        public bool IncludeBanned
        {
            get => _includeBanned;
            set
            {
                _includeBanned = value;
                OnPropertyChanged(nameof(IncludeBanned));
            }
        }

        private bool _includeDeleted;

        public bool IncludeDeleted
        {
            get => _includeDeleted;
            set
            {
                _includeDeleted = value;
                OnPropertyChanged(nameof(IncludeDeleted));
            }
        }


        public UserFilter(params bool[] initialState)
        {      
            IncludeClients = initialState[0];
            IncludeAdmins = initialState[1];
            IncludeBanned = initialState[2];
            IncludeDeleted = initialState[3];
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!IncludeAdmins && !IncludeClients)
                GetType()
                    .GetProperties()
                    .First(f => f.Name == e.PropertyName)
                    .GetSetMethod()
                    .Invoke(this, new object[] {true});
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}