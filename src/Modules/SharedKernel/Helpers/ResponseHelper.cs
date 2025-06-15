using ModularMonolith.Template.SharedKernel.DTOs;

namespace ModularMonolith.Template.SharedKernel.Helpers
{
    public static class ResponseHelper
    {
        public static BaseResponse<T> Success<T>(T data, int code = 200)
        {
            return new BaseResponse<T>
            {
                code = code,
                Data = data,
                Error = null
            };
        }

        public static BaseResponse<T> Fail<T>(string error, int code = 400, object? details = null)
        {
            return new BaseResponse<T>
            {
                code = code,
                Data = default,
                Error = error,
                Details = details
            };
        }

        public static BaseResponse<T> FromException<T>(Exception ex, int code = 500)
        {
            return new BaseResponse<T>
            {
                code = code,
                Data = default,
                Error = ex.Message
            };
        }

        public static BaseResponse<object> NoContent()
        {
            return new BaseResponse<object>
            {
                code = 204,
                Data = null,
                Error = null
            };
        }

        public static BaseResponse<object> BadRequest(string error, object? details = null)
        {
            return new BaseResponse<object>
            {
                code = 400,
                Data = null,
                Error = error,
                Details = details
            };
        }

        public static BaseResponse<T> CreatedAt<T>(T data, string location)
        {
            return new BaseResponse<T>
            {
                code = 201,
                Data = data,
                Error = null,
                Details = new { Location = location }
            };
        }
    }
}
