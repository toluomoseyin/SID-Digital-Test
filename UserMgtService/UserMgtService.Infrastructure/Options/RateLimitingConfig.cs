namespace UserMgtService.Infrastructure.Options
{
    public class RateLimitingOption
    {
        public int Limit { get; set; }
        public int TimeWindowInMinutes { get; set; }
    }
}
