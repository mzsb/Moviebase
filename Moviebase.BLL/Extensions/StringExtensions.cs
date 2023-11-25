namespace Moviebase.BLL.Extensions;

public static class StringExtensions
{
    public static IEnumerable<string> CommaSplit(this string str) => str
        .Split(',')
        .Select(element => element.Trim());
}
