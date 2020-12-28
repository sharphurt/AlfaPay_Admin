using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using AlfaPay_Admin.Annotations;
using AlfaPay_Admin.Context;
using AlfaPay_Admin.Enum;
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

        private RequestStatus _addressesRequestStatus;

        public RequestStatus AddressesRequestStatus
        {
            get => _addressesRequestStatus;
            set
            {
                _addressesRequestStatus = value;
                OnPropertyChanged(nameof(AddressesRequestStatus));
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

        private string _companiesSearchString;

        public string CompaniesSearchString
        {
            get => _companiesSearchString;
            set
            {
                _companiesSearchString = value;
                GetCompaniesAutocomplete(_companiesSearchString);
                OnPropertyChanged(nameof(CompaniesSearchString));
            }
        }

        private RequestStatus _companiesRequestStatus;

        public RequestStatus CompaniesRequestStatus
        {
            get => _companiesRequestStatus;
            set
            {
                _companiesRequestStatus = value;
                OnPropertyChanged(nameof(CompaniesRequestStatus));
            }
        }

        private ObservableCollection<Party> _autocompleteCompanies;

        public ObservableCollection<Party> AutocompleteCompanies
        {
            get => _autocompleteCompanies;
            set
            {
                _autocompleteCompanies = value;
                OnPropertyChanged(nameof(AutocompleteCompanies));
            }
        }

        private Party _foundedCompany;

        public Party FoundedCompany
        {
            get => _foundedCompany;
            set
            {
                _foundedCompany = value;
                OnPropertyChanged(nameof(FoundedCompany));
            }
        }
        
        private Party _selectedCompany;

        public Party SelectedCompany
        {
            get => _selectedCompany;
            set
            {
                _selectedCompany = value;
                CompaniesSearchString = _selectedCompany.name.short_with_opf;
                FillInputsWithData(_selectedCompany);
                OnPropertyChanged(nameof(SelectedCompany));
            }
        }

        private RequestStatus _companyByInnRequestStatus;

        public RequestStatus CompanyByInnRequestStatus
        {
            get => _companyByInnRequestStatus;
            set
            {
                _companyByInnRequestStatus = value;
                OnPropertyChanged(nameof(CompanyByInnRequestStatus));
            }
        }

        public CompanyModel(string inn)
        {
            AddressesRequestStatus = RequestStatus.NotStarted;
            CompanyByInnRequestStatus = RequestStatus.NotStarted;
            AutocompleteAddresses = new ObservableCollection<string>();
            Kkt = _random.Next(10000000, 99999999).ToString() + _random.Next(10000000, 99999999);
            GetCompanyInfoByInn(inn, FillInputsWithData);
        }

        private async void GetAddressAutocomplete(string input)
        {
            try
            {
                AddressesRequestStatus = RequestStatus.InProgress;
                var dadataApiKey = ConfigurationManager.AppSettings["DadataApiKey"];
                var api = new SuggestClientAsync(dadataApiKey);
                var result = await api.SuggestAddress(input, 10);
                AutocompleteAddresses = new ObservableCollection<string>(result.suggestions.Select(s => s.value));
                AddressesRequestStatus = AutocompleteAddresses.Count == 0
                    ? RequestStatus.CompletedWithError
                    : RequestStatus.CompletedSuccessfully;
            }
            catch
            {
                AddressesRequestStatus = RequestStatus.CompletedWithError;
            }
        }

        private void FillInputsWithData(Party company)
        {
            if (company == null) return;
            Inn = company.inn;
            Address = company.address.unrestricted_value;
            Name = company.name.full_with_opf;
        }


        private async void GetCompanyInfoByInn(string inn, Action<Party> onSuccessful)
        {
            CompanyByInnRequestStatus = RequestStatus.InProgress;
            try
            {
                var dadataApiKey = ConfigurationManager.AppSettings["DadataApiKey"];
                var api = new SuggestClientAsync(dadataApiKey);
                var result = await api.FindParty(inn);
                FoundedCompany = result.suggestions.Select(s => s.data).FirstOrDefault();
                CompanyByInnRequestStatus = RequestStatus.CompletedSuccessfully;
                onSuccessful.Invoke(FoundedCompany);
            }
            catch
            {
                CompanyByInnRequestStatus = RequestStatus.CompletedWithError;
            }
        }


        private async void GetCompaniesAutocomplete(string input)
        {
            try
            {
                CompaniesRequestStatus = RequestStatus.InProgress;
                var dadataApiKey = ConfigurationManager.AppSettings["DadataApiKey"];
                var api = new SuggestClientAsync(dadataApiKey);
                var result = await api.SuggestParty(input, 10);
                AutocompleteCompanies = new ObservableCollection<Party>(result.suggestions.Select(s => s.data));
                CompaniesRequestStatus = AutocompleteCompanies.Count == 0
                    ? RequestStatus.CompletedWithError
                    : RequestStatus.CompletedSuccessfully;
            }
            catch
            {
                CompaniesRequestStatus = RequestStatus.CompletedWithError;
            }
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