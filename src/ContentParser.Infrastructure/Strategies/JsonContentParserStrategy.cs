using System.Text.Json;
using ContentParser.Domain.Common;
using ContentParser.Domain.Enums;
using ContentParser.Domain.Interfaces;
using ContentParser.Domain.ValueObjects;

namespace ContentParser.Infrastructure.Strategies;

public class JsonContentParserStrategy : IContentParserStrategy
{
    public ContentType SupportedType => ContentType.InternalJson;
    public Result<ParsedDataResult> Parse(string rawContent)
    {
        if (string.IsNullOrWhiteSpace(rawContent))
        {
            return Result<ParsedDataResult>.Failure("JSON content cannot be empty.");
        }
        
        using var document = JsonDocument.Parse(rawContent);
        var root = document.RootElement;

        int processedRowsCount = 1;
        
        if (root.ValueKind == JsonValueKind.Array)
        {
            processedRowsCount = root.GetArrayLength();
        }

        var deserializedData = JsonSerializer.Deserialize<object>(rawContent);

        if (deserializedData is null)
        {
            return Result<ParsedDataResult>.Failure("Failed to deserialize JSON content.");
        }

        var result = new ParsedDataResult(processedRowsCount, deserializedData);
        return Result<ParsedDataResult>.Success(result);
    }
}