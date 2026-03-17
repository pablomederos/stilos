using Stilos.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStilosCoreServices();
builder.Services.AddStilosCaching();
builder.Services.AddStilosHttpClients();
builder.Services.AddStilosForwardedHeaders();
builder.Services.AddStilosRateLimiting(builder.Configuration);
builder.Services.AddStilosCompressionAndBundling(builder.Environment);

var app = builder.Build();

app.UseForwardedHeaders();

bool isWatch = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DOTNET_WATCH"));

if (!app.Environment.IsDevelopment() && !isWatch) {
    app.UseExceptionHandler("/Error");
    app.UseResponseCompression();
}

app.UseWebOptimizer();
app.UseStilosStaticFiles();
app.UseStilosCachingHeaders();

app.MapControllers().CacheOutput();
app.MapRazorPages().CacheOutput();

app.Run();
