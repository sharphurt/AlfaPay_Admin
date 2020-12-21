using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AlfaPay_Admin.Annotations;

namespace AlfaPay_Admin.Entity
{
    public sealed class ApplicationFilter : INotifyPropertyChanged
    {
        private bool _includeNew;

        public bool IncludeNew
        {
            get => _includeNew;
            set
            {
                _includeNew = value;
                OnPropertyChanged(nameof(IncludeNew));
            }
        }

        private bool _includeAccepted;

        public bool IncludeAccepted
        {
            get => _includeAccepted;
            set
            {
                _includeAccepted = value;
                OnPropertyChanged(nameof(IncludeAccepted));
            }
        }

        private bool _includeRejected;

        public bool IncludeRejected
        {
            get => _includeRejected;
            set
            {
                _includeRejected = value;
                OnPropertyChanged(nameof(IncludeRejected));
            }
        }


        public ApplicationFilter(params bool[] initialState)
        {
            IncludeNew = initialState[0];
            IncludeAccepted = initialState[1];
            IncludeRejected = initialState[2];
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!IncludeNew && !IncludeAccepted && !IncludeRejected)
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