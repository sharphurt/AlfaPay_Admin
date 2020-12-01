using AlfaPay_Admin.Model;

namespace AlfaPay_Admin.Entity
{
    public class ApiResponse<T>
    {
        public T Response;
        public ApiError Error;

        public ApiResponse(T response, ApiError error)
        {
            Response = response;
            Error = error;
        }

        public bool IsSuccessfully => Response != null;
    }
}