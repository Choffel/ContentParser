using System.Text.Json.Serialization;

namespace ContentParser.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ContentType
{   
    [JsonPropertyName("CSV")]
    Csv = 0,
    
    [JsonPropertyName("INTERNAL_JSON")]
    InternalJson = 1
}