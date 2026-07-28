using ContentParser.Domain.Common;
using ContentParser.Domain.Enums;
using ContentParser.Domain.Interfaces;

namespace ContentParser.Application.Interfaces;

public interface IContentParserResolver
{
    Result<IContentParserStrategy> Resolve(ContentType contentType);
}