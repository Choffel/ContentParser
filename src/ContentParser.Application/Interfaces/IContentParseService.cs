using ContentParser.Application.DTOs;

namespace ContentParser.Application.Interfaces;

public interface IContentParseService
{
    Result<ParseContentResponseDto> ProcessPayload(ParseContentRequestDto request);
}