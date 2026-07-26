using ContentParser.Domain.Enums;
using ContentParser.Domain.Interfaces;

namespace ContentParser.Application.Interfaces;

public interface IContentParserResolver
{
    /// <summary>
    /// Возвращает подходящую стратегию парсинга по типу контента.
    /// </summary>
    Result<IContentParserStrategy> Resolve(ContentType contentType);
}