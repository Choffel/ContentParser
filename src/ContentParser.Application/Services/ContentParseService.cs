using ContentParser.Application.Common;
using ContentParser.Application.DTOs;
using ContentParser.Application.Interfaces;
using ContentParser.Domain.Common; 

namespace ContentParser.Application.Services;

public class ContentParseService : IContentParseService
{
    private readonly IContentParserResolver _parserResolver;
    private readonly IBase64Decoder _base64Decoder;

    public ContentParseService(IContentParserResolver resolver, IBase64Decoder base64Decoder)
    {
        _parserResolver = resolver;
        _base64Decoder = base64Decoder;
    }

    public Result<ParseContentResponseDto> ProcessPayload(ParseContentRequestDto request)
    {
        var validationResult = request.Validate();
        if (!validationResult.IsSuccess)
        {
            return Result<ParseContentResponseDto>.Failure(validationResult.ErrorMessage!);
        }
        
        var resolverResult = _parserResolver.Resolve(request.Type);
        if (!resolverResult.IsSuccess)
        {
            return Result<ParseContentResponseDto>.Failure(resolverResult.ErrorMessage!);
        }

        var strategy = resolverResult.Value!;
        
        var decodeResult = _base64Decoder.Decode(request.Content);
        if (!decodeResult.IsSuccess)
        {
            return Result<ParseContentResponseDto>.Failure(decodeResult.ErrorMessage!);
        }
        
        var parseResult = strategy.Parse(decodeResult.Value!);
        if (!parseResult.IsSuccess)
        {
            return Result<ParseContentResponseDto>.Failure(parseResult.ErrorMessage!);
        }
        
        var responseDto = parseResult.Value!.ToDto();

        return Result<ParseContentResponseDto>.Success(responseDto);
    }
}