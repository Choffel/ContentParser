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

        List<Dictionary<string, JsonElement>>? items;
        try
        {
            items = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(
                rawContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (JsonException ex)
        {
            return Result<ParsedDataResult>.Failure($"Invalid JSON format: {ex.Message}");
        }

        if (items is null || items.Count == 0)
        {
            return Result<ParsedDataResult>.Failure("JSON array is empty or null.");
        }

        var rows = items.Select(item =>
        {
            var row = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var kvp in item)
            {
                row[kvp.Key] = kvp.Value.ToString();
            }
            return row;
        }).ToList();

        return Result<ParsedDataResult>.Success(new ParsedDataResult(rows.Count, rows));
    }
}