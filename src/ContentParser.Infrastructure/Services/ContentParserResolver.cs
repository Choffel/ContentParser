using ContentParser.Application.Interfaces;
using ContentParser.Domain.Common;
using ContentParser.Domain.Enums;
using ContentParser.Domain.Interfaces;

namespace ContentParser.Infrastructure.Services;

public class ContentParserResolver : IContentParserResolver
{
    private readonly IEnumerable<IContentParserStrategy> _strategies;

    public ContentParserResolver(IEnumerable<IContentParserStrategy> strategies)
    {
        _strategies = strategies;
    }
    
    public Result<IContentParserStrategy> Resolve(ContentType contentType)
    {
        var strategy = _strategies.FirstOrDefault(s => s.SupportedType == contentType);

        if (strategy is null)
        {
            return Result<IContentParserStrategy>.Failure($"No parser strategy registered for content type: {contentType}");
        }

        return Result<IContentParserStrategy>.Success(strategy);
    }
}