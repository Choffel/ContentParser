using ContentParser.Application.DTOs;
using ContentParser.Domain.Common;

namespace ContentParser.Application.Interfaces;

public interface IContentParseService
{
    Result<ParseContentResponseDto> ProcessPayload(ParseContentRequestDto request);
}