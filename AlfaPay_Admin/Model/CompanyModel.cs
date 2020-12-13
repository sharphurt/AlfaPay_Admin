using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using AlfaPay_Admin.Annotations;
using AlfaPay_Admin.Context;
using Dadata;
using Dadata.Model;
using Newtonsoft.Json;

namespace AlfaPay_Admin.Model
{
    public sealed class CompanyModel : IDataErrorInfo, INotifyPropertyChanged
    {
        private string _name;
        private Random _random = new Random();

        [JsonProperty("name")]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _address;

        [JsonProperty("address")]
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                GetAddressAutocomplete(_address);
                OnPropertyChanged(nameof(Address));
            }
        }

        private string _inn;

        [JsonProperty("inn")]
        public string Inn
        {
            get => _inn;
            set
            {
                _inn = value;
                OnPropertyChanged(nameof(Inn));
            }
        }

        private string _taxSystem;
        
        [JsonProperty("taxSystem")]
        public string TaxSystem
        {
            get => _taxSystem;
            set
            {
                _taxSystem = value;
                OnPropertyChanged(nameof(TaxSystem));
            }
        }

        private string _kkt;
        [JsonProperty("kkt")]
        public string Kkt
        {
            get => _kkt;
            set
            {
                _kkt = value;
                OnPropertyChanged(nameof(Kkt));
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

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private bool _isResponseEmpty;

        public bool IsResponseEmpty
        {
            get => _isResponseEmpty;
            set
            {
                _isResponseEmpty = value;
                OnPropertyChanged(nameof(IsResponseEmpty));
            }
        }

        private ObservableCollection<string> _autocompleteAddresses;

        public ObservableCollection<string> AutocompleteAddresses
        {
            get => _autocompleteAddresses;
            set
            {
                _autocompleteAddresses = value;
                OnPropertyChanged(nameof(AutocompleteAddresses));
            }
        }

        private Suggestion<Address> _selectedAddress;

        public Suggestion<Address> SelectedAddress
        {
            get => _selectedAddress;
            set
            {
                _selectedAddress = value;
                OnPropertyChanged(nameof(SelectedAddress));
            }
        }

        public CompanyModel()
        {
            IsResponseEmpty = true;
            AutocompleteAddresses = new ObservableCollection<string>();
            Kkt = _random.Next(10000000, 99999999).ToString() + _random.Next(10000000, 99999999);
        }

        private async void GetAddressAutocomplete(string input)
        {
            ResponseReceived = false;
            IsLoading = true;
            
            const string token = "e597dac2837d17460c9f3fecb15f3705dce952aa";
            var api = new SuggestClientAsync(token);
            var result = await api.SuggestAddress(input, 10);
            
            AutocompleteAddresses = new ObservableCollection<string>();
            foreach (var suggestion in result.suggestions)
                AutocompleteAddresses.Add(suggestion.value);
            
            ResponseReceived = true;
            IsLoading = false;
            IsResponseEmpty = AutocompleteAddresses.Count == 0;
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Name":
                        if (Name == "")
                            return "Название не может быть пустым";
                        break;
                    case "Inn":
                        if (!Regex.IsMatch(Inn, "[\\d]{10}"))
                            return "Введите 10 цифр";
                        break;
                    case "TaxSystem":
                        if (!Regex.IsMatch(TaxSystem, "ЕНВД|ЕСХН|СРП|УСН|ЕНВД-ЕСХН|УСН-ЕНВД|ОСН"))
                            return "Некорректный тип СНО";
                        break;
                    default:
                        return string.Empty;
                }

                return string.Empty;
            }
        }

        public string Error { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}