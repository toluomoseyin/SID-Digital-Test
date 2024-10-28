namespace UserMgtService.Application.Abstraction.Services
{
    public interface IRateLimiterService
    {
        bool IsRateLimited(string key);
    }
}
