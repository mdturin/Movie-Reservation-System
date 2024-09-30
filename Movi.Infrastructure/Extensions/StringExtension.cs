using System.Security.Cryptography;
using System.Text;

namespace Movi.Infrastructure.Extensions;

public static class StringExtension
{
    public static string ToCheckSumId(this string input)
    {
        var bytes = MD5.HashData(Encoding.UTF8.GetBytes(input));
        var builder = new StringBuilder();
        for (var i = 0; i < bytes.Length; ++i)
            builder.Append(bytes[i].ToString("x2"));
        return builder.ToString();
    }
}
