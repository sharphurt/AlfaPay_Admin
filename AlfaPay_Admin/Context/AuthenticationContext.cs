using AlfaPay_Admin.Entity;
using AlfaPay_Admin.Model;

namespace AlfaPay_Admin.Context
{
    public class AuthenticationContext
    {
        public static LoggedUser LoggedUser;
        public static JwtContainer Token;
        public static DeviceInfo DeviceInfo;

        public static void ClearAuthentication()
        {
            LoggedUser = null;
            Token = null;
        }
    }
}