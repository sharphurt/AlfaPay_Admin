using System.Runtime.Serialization;

namespace AlfaPay_Admin.Enum
{
    public enum ApplicationStatus
    {
        [EnumMember(Value = "NOT_CONSIDERED")]
        NotConsidered, 
        
        [EnumMember(Value = "ACCEPTED")]
        Accepted, 
        
        [EnumMember(Value = "REJECTED")]
        Rejected
    }
}