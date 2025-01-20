using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanArchCQRS.API.Filters;

public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is FluentValidation.ValidationException validationException)
        {
            var errors = validationException.Errors
                .Select(error => $"{error.PropertyName}: {error.ErrorMessage}")
                .ToList();

            var result = new ObjectResult(new { Errors = errors })
            {
                StatusCode = 400, // Bad Request
            };

            context.Result = result;
            context.ExceptionHandled = true;
        }
        else if (context.Exception is ArgumentNullException || context.Exception is KeyNotFoundException)
        {
            context.Result = new NotFoundObjectResult(new { Error = "Resource not found." }); // 404
            context.ExceptionHandled = true;
        }
        else if (context.Exception is HttpRequestException || context.Exception is InvalidOperationException)
        {
            context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError); // 500
            context.ExceptionHandled = true;
        }
    }
}
