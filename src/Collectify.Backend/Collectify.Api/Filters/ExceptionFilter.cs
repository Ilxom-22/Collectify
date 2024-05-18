using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using Collectify.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Collectify.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly Dictionary<Type, CustomProblemDetails> _exceptionMappings;

    public ExceptionFilter()
    {
        _exceptionMappings = new Dictionary<Type, CustomProblemDetails>
        {
            { typeof(AuthenticationException), new CustomProblemDetails 
                { Status = StatusCodes.Status401Unauthorized, IsUserFriendly = true } },
            { typeof(ArgumentException), new CustomProblemDetails
                { Status = StatusCodes.Status400BadRequest, IsUserFriendly = false } },
            { typeof(FluentValidation.ValidationException), new CustomProblemDetails 
                { Status = StatusCodes.Status400BadRequest, IsUserFriendly = true } },
            { typeof(ValidationException), new CustomProblemDetails
                { Status = StatusCodes.Status400BadRequest, IsUserFriendly = true } }
        };
    }
    
    public void OnException(ExceptionContext context)
    {
        if (_exceptionMappings.TryGetValue(context.Exception.GetType(), out var problemDetails))
        {
            problemDetails.Detail = context.Exception.Message;
        }
        else
        {
            problemDetails = new CustomProblemDetails
                { Status = StatusCodes.Status500InternalServerError, Detail = context.Exception.Message, IsUserFriendly = false };
        }

        context.ExceptionHandled = true;

        context.Result = new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };
    }
}