using System.Text;
using ContentParser.Domain.Common;

namespace ContentParser.Application.Services;

public class Base64Decoder : IBase64Decoder
{
    public Result<string> Decode(string base64EncodedData)
    {
        if (string.IsNullOrEmpty(base64EncodedData))
        {
            return Result<string>.Failure("base64EncodedData cannot be empty");
        }

        byte[] bytes = Convert.FromBase64String(base64EncodedData);
        
        string decodedText = Encoding.UTF8.GetString(bytes);
        
        return Result<string>.Success(decodedText);
    }
}