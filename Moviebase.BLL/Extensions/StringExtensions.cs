using System.Text.RegularExpressions;

namespace Moviebase.BLL.Extensions;

public static partial class StringExtensions
{
    private static readonly Regex _emailRegex = EmailRegex();

    [GeneratedRegex(".+@.+..+", RegexOptions.Compiled)]
    private static partial Regex EmailRegex();

    public static IEnumerable<string> CommaSplit(this string str) => str
        .Split(',')
        .Select(element => element.Trim());

    public static bool IsValidEmail(this string str) => 
        _emailRegex.IsMatch(str);
}
