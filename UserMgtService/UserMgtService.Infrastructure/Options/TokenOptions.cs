namespace UserMgtService.Infrastructure.Options
{
    public sealed class TokenOptions
    {
        public required string Key { get; init; }
        public required int ExpirationInMinutes { get; init; }
        public required string Issuer { get; init; }
        public required string Audience { get; init; }
    }
}
