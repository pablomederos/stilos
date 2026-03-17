namespace Stilos.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseStilosCachingHeaders(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            context.Response.OnStarting(() =>
            {
                if (context.Response.StatusCode == 200 && 
                    !context.Response.Headers.ContainsKey(Microsoft.Net.Http.Headers.HeaderNames.CacheControl))
                {
                    context.Response.Headers.CacheControl = "public,max-age=86400";
                }
                return Task.CompletedTask;
            });
            
            await next();
        });

        return app;
    }

    public static IApplicationBuilder UseStilosStaticFiles(this IApplicationBuilder app)
    {
        var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
        provider.Mappings[".skill"] = "application/zip";

        app.UseStaticFiles(new StaticFileOptions
        {
            ContentTypeProvider = provider,
            OnPrepareResponse = ctx =>
            {
                const int durationInSeconds = 60 * 60 * 24 * 365; // 1 año
                ctx.Context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.CacheControl] =
                    "public,max-age=" + durationInSeconds + ",immutable";
            }
        });

        return app;
    }
}
