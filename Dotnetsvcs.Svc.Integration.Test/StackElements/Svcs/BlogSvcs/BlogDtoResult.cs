using Dotnetsvcs.Svc.DtoResult;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs;

public class BlogDtoResult : IDtoResult
{
    public decimal? Preu { get; init; }
    public byte[] TimeStamp { get; init; } = default!;
    public object?[]? CategoriaKey { get; init; }
    public string CategoriaDisplay { get; init; } = default!;
    public string Titol { get; init; } = default!;
    public int Rating { get; init; }
}
