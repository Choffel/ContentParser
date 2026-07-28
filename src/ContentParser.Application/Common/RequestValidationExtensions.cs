using ContentParser.Application.DTOs;
using ContentParser.Domain.Common;

namespace ContentParser.Application.Common;

public static class RequestValidationExtensions
{
    public static Result<ParseContentRequestDto> Validate(this ParseContentRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Content))
        {
            return Result<ParseContentRequestDto>.Failure("Content cannot be null or empty.");
        }

        if (!Enum.IsDefined(request.Type))
        {
            return Result<ParseContentRequestDto>.Failure("Invalid or unsupported content type.");
        }
        
        return Result<ParseContentRequestDto>.Success(request);
    }
}