namespace Stilos.Models;

public sealed record DesignStyle(
    string Id,
    string Name,
    string Emotion,
    string Sectors,
    string Description,
    string TemplateType
);

public sealed record StyleFamily(
    int Number,
    string RomanNumeral,
    string Name,
    string Icon,
    List<DesignStyle> Styles
);
