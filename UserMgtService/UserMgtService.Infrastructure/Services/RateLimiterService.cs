using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using UserMgtService.Application.Abstraction.Services;
using UserMgtService.Infrastructure.Options;

namespace UserMgtService.Infrastructure.Services
{
    public class RateLimiterService(
        IMemoryCache cache,
        IOptions<RateLimitingOption> options) : IRateLimiterService
    {
        private readonly IOptions<RateLimitingOption> _options = options;
        private readonly IMemoryCache _cache = cache;
        private readonly int _limit = options.Value.Limit;
        private readonly TimeSpan _timeWindow = TimeSpan.
                FromMinutes(options
                .Value
                .TimeWindowInMinutes);



        public bool IsRateLimited(string key)
        {
            var count = _cache.GetOrCreate(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _timeWindow;
                return 0;
            });

            if (count >= _limit)
                return true;

            _cache.Set(key, count + 1);
            return false;
        }
    }
}

