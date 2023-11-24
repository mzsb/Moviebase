namespace Moviebase.API.Extensions;

public static class HttpExtensions
{
    public static void AddPaginationHeader(this HttpResponse response, int totalCount)
    {
        response.Headers.Append("Pagination", $" {{ \"{nameof(totalCount)}\": {totalCount} }} ");
        response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
    }
}
