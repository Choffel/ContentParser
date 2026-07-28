using ContentParser.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace ContentParser.Api.Extensions;

public static  class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(result.Value);
        }
        
        return new BadRequestObjectResult(new { error = result.ErrorMessage });
    }
}