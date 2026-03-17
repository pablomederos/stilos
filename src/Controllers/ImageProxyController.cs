using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Logging;

namespace Stilos.Controllers;

[ApiController]
[Route("api/img")]
public sealed partial class ImageProxyController(IHttpClientFactory httpClientFactory, ILogger<ImageProxyController> logger) : ControllerBase
{
    const string PicsumBase = "https://picsum.photos/seed";

    [HttpGet]
    [OutputCache(Duration = 86400)]
    public async Task<IActionResult> Get(

        [FromQuery] string seed,
        [FromQuery(Name = "w")] int width,
        [FromQuery(Name = "h")] int height,
        [FromQuery] bool grayscale = false)
    {
        if (string.IsNullOrWhiteSpace(seed) || width <= 0 || height <= 0)
            return BadRequest("Los parámetros seed, w y h son requeridos y deben ser válidos.");

        var suffix = grayscale ? "?grayscale" : string.Empty;
        var picsumUrl = $"{PicsumBase}/{seed}/{width}/{height}.webp{suffix}";


        LogDownloading(logger, picsumUrl);

        var client = httpClientFactory.CreateClient("picsum");
        var response = await client.GetAsync(picsumUrl);

        if (!response.IsSuccessStatusCode)
            return StatusCode((int)response.StatusCode, $"Picsum respondió con {response.StatusCode}.");

        var imageBytes = await response.Content.ReadAsByteArrayAsync();
        
        Response.Headers.CacheControl = "public,max-age=31536000,immutable";
        return File(imageBytes, "image/webp");
    }

    [LoggerMessage(Level = LogLevel.Debug, Message = "Cache hit: {CacheKey}")]
    static partial void LogCacheHit(ILogger logger, string cacheKey);

    [LoggerMessage(Level = LogLevel.Information, Message = "Descargando imagen desde Picsum: {Url}")]
    static partial void LogDownloading(ILogger logger, string url);
}

