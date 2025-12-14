namespace ResultPattern.Application.Common
{
    public class Result<T> : IResultBase
    {
        public bool Success { get; private set; }
        public string? Error { get; private set; }
        public string[]? Errors { get; private set; }
        public int StatusCode { get; private set; }
        public T? Data { get; private set; }


        private Result(bool success, T? data, string? error, string[]? errors, int statusCode)
        {
            Success = success;
            Data = data;
            Error = error;
            Errors = errors;
            StatusCode = statusCode;
        }

        public static Result<T> Ok(T data) => new(true, data, null, null, 200);
        public static Result<T> Created(T data) => new(true, data, null, null, 201);
        public static Result<T> Accepted(T data) => new(true, data, null, null, 202);
        public static Result<T> BadRequest(string error, string[]? errors = default) => new(false, default, error, errors, 400);
        public static Result<T> Unauthorized(string error = "No autorizado") => new(false, default, error, null, 401);
        public static Result<T> Forbidden(string error = "Acceso denegado") => new(false, default, error, null, 403);

        public static Result<T> NotFound(string error, string[]? errors = default) => new(false, default, error, errors, 404);
        public static Result<T> ServerError(string error, string[]? errors = default) => new(false, default, error, errors, 500);
        public static Result<T> Conflict(string error) => new(false, default, error, null, 409);

    }
}
