using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ResultPattern.Application.Common;

namespace ResultPattern.Api.Filters
{
    public class ResultFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ObjectResult obj && obj.Value is IResultBase result)
            {
                context.Result = new ObjectResult(result) { StatusCode = result.StatusCode };
            }
            await next();
        }

    }
}
