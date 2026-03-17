using System.Threading.RateLimiting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using Stilos.Configuration;

namespace Stilos.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStilosCoreServices(this IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddControllers();
        services.AddSingleton<Stilos.Services.StyleCatalogService>();
        
        return services;
    }

    public static IServiceCollection AddStilosCaching(this IServiceCollection services)
    {
        services.AddOutputCache(options =>
        {
            options.AddBasePolicy(builder => 
                builder.Expire(TimeSpan.FromDays(7)));
        });

        return services;
    }

    public static IServiceCollection AddStilosHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient("picsum", client =>
        {
            client.BaseAddress = new Uri("https://picsum.photos");
            client.DefaultRequestHeaders.Add("User-Agent", "Stilos/1.0");
        });

        return services;
    }

    public static IServiceCollection AddStilosForwardedHeaders(this IServiceCollection services)
    {
        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.KnownIPNetworks.Clear();
            options.KnownProxies.Clear();
        });

        return services;
    }

    public static IServiceCollection AddStilosRateLimiting(this IServiceCollection services, IConfiguration configuration)
    {
        var rateLimitingOptions = new RateLimitingOptions();
        configuration.GetSection(RateLimitingOptions.RateLimiting).Bind(rateLimitingOptions);

        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
            {
                var ip = context.Request.Headers["CF-Connecting-IP"].FirstOrDefault() ?? 
                         context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                return RateLimitPartition.GetTokenBucketLimiter(ip, _ =>
                    new TokenBucketRateLimiterOptions
                    {
                        TokenLimit = rateLimitingOptions.BurstMax,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 0,
                        ReplenishmentPeriod = rateLimitingOptions.RefreshInterval,
                        TokensPerPeriod = rateLimitingOptions.TokensPerRefresh,
                        AutoReplenishment = true
                    });
            });
        });

        return services;
    }

    public static IServiceCollection AddStilosCompressionAndBundling(this IServiceCollection services, IWebHostEnvironment environment)
    {
        bool isWatch = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DOTNET_WATCH"));

        if (!environment.IsDevelopment() && !isWatch)
        {
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    ["text/css", "application/javascript", "text/javascript", "image/svg+xml", "application/json", "text/plain", "image/webp"]);
            });
        }

        services.AddWebOptimizer(pipeline =>
        {
            pipeline.MinifyCssFiles("css/**/*.css");
            pipeline.MinifyJsFiles("js/**/*.js");
        }, options =>
        {
            options.EnableCaching = !environment.IsDevelopment();
            options.EnableDiskCache = false;
        });

        return services;
    }
}
