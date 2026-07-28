using ContentParser.Application.DTOs;
using ContentParser.Domain.ValueObjects;

namespace ContentParser.Application.Common;

public static  class MappingExtensions
{
    public static ParseContentResponseDto ToDto(this ParsedDataResult result)
    {
        return new ParseContentResponseDto(
            result.ProcessedRowsCount,
            result.Data
        );
    }
}