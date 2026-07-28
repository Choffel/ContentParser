using ContentParser.Domain.Common;
using ContentParser.Domain.Enums;
using ContentParser.Domain.ValueObjects;

namespace ContentParser.Domain.Interfaces;

public interface IContentParserStrategy
{
    ContentType SupportedType { get; }
    
    Result<ParsedDataResult> Parse(string rawContent);
}