using System.Text.Json.Serialization;

namespace ContentParser.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ContentType
{   
    Csv = 0,
    InternalJson = 1
}