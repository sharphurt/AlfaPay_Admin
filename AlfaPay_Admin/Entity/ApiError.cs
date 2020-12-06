using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using AlfaPay_Admin.Annotations;

namespace AlfaPay_Admin.Entity
{
    public class ApiError : INotifyPropertyChanged
    {
        private string _message;

        [JsonPropertyName("message")]
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        private int _code;

        [JsonPropertyName("errorCode")]
        public int Code
        {
            get => _code;
            set
            {
                _code = value;
                OnPropertyChanged(nameof(Code));
            }
        }

        private string _httpCode;

        [JsonPropertyName("httpCode")]
        public string HttpCode
        {
            get => _httpCode;
            set
            {
                _httpCode = value;
                OnPropertyChanged(nameof(HttpCode));
            }
        }

        public ApiError(string message, int code, string httpCode)
        {
            Message = message;
            Code = code;
            HttpCode = httpCode;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}