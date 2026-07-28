using ContentParser.Domain.Common;

public interface IBase64Decoder
{
    Result<string> Decode(string base64EncodedData);
}