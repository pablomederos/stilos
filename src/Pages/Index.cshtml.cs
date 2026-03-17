using Microsoft.AspNetCore.Mvc.RazorPages;
using Stilos.Models;
using Stilos.Services;

namespace Stilos.Pages;

public class IndexModel(StyleCatalogService catalog) : PageModel
{
    public IReadOnlyList<StyleFamily> Families { get; private set; } = [];
    public string? SelectedStyleId { get; private set; }

    public void OnGet(string? style)
    {
        Families = catalog.GetAllFamilies();
        SelectedStyleId = style;
    }
}
