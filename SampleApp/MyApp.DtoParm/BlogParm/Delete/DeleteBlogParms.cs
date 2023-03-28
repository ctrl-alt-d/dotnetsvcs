using Dotnetsvcs.DtoParm.Abstractions;

namespace MyApp.DtoParm.BlogParm.Delete;

public class DeleteBlogParms : DtoParmDelete
{
    public override object?[] KeyValues { get; set; } = default!;
    public override object? Version { get; set; }
}
