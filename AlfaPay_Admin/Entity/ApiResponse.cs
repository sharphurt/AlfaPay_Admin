namespace AlfaPay_Admin.Model
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
    }
}