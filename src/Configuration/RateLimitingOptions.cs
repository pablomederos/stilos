namespace Stilos.Configuration;

public class RateLimitingOptions
{
    public const string RateLimiting = "RateLimiting";

    public int BurstMax { get; set; } = 100;
    public int TokensPerRefresh { get; set; } = 30;
    public TimeSpan RefreshInterval { get; set; } = TimeSpan.FromSeconds(5);
}
