using CoinConvertor.Middlewares;

namespace CoinConvertor.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseRateLimiting(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RateLimitingMiddleware>();
    }
}
