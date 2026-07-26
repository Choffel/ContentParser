using ContentParser.Domain.Enums;
using ContentParser.Domain.ValueObjects;

namespace ContentParser.Domain.Interfaces;

public interface IContentParserStrategy
{
    /// <summary>
    /// Тип контента, который умеет обрабатывать данная стратегия.
    /// </summary>
    ContentType SupportedType { get; }

    /// <summary>
    /// Парсит декодированную из Base64 строку в доменный результат.
    /// </summary>
    /// <param name="rawContent">Декодированный текст (CSV или JSON)</param>
    Result<ParsedDataResult> Parse(string rawContent);
}