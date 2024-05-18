using Microsoft.AspNetCore.Mvc;

namespace Collectify.Domain.Exceptions;

public class CustomProblemDetails : ProblemDetails
{
    public bool IsUserFriendly { get; set; }
}