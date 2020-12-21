using System;
using AlfaPay_Admin.Enum;
using Newtonsoft.Json;

namespace AlfaPay_Admin.Converter
{
    public class ApplicationStatusStringToEnumConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var status = (ApplicationStatus)value;

            switch (status)
            {
                case ApplicationStatus.Accepted:
                    writer.WriteValue("ACCEPTED");
                    break;
                case ApplicationStatus.Rejected:
                    writer.WriteValue("REJECTED");
                    break;
                case ApplicationStatus.NotConsidered:
                    writer.WriteValue("NOT_CONSIDERED");
                    break;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var enumString = (string)reader.Value;
            switch (enumString)
            {
                case "ACCEPTED":
                    return ApplicationStatus.Accepted;
                case "REJECTED":
                    return ApplicationStatus.Rejected;
                case "NOT_CONSIDERED":
                    return ApplicationStatus.NotConsidered;
            }

            return ApplicationStatus.NotConsidered;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}