using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ResultPattern.Application.Common;

namespace ResultPattern.Api.Filters
{
    public class GlobalExceptionFilter: IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            IResultBase response;

            // Mapear excepciones de aplicación a Result<T> con el status code correcto
            if (context.Exception is AppValidationException)
            {
                response = Result<object>.BadRequest(context.Exception.Message);
            }
            else if (context.Exception is AppNotFoundException)
            {
                response = Result<object>.NotFound(context.Exception.Message);
            }
            else if (context.Exception is AppConflictException)
            {
                response = Result<object>.Conflict(context.Exception.Message);
            }
            else
            {
                response = Result<object>.ServerError("Error interno del servidor");
            }

            context.Result = new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };

            context.ExceptionHandled = true;

        }

    }
}
