using Dotnetsvcs.DtoData.Abstractions;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.DtoResult.BlogDtosResult;

public class BlogDtoResult : IDtoData {
    public decimal? Preu { get; init; }
    public byte[] TimeStamp { get; init; } = default!;
    public object?[]? CategoriaKey { get; init; }
    public string CategoriaDisplay { get; init; } = default!;
    public string Titol { get; init; } = default!;
    public int Rating { get; init; }
    public int NumPostsCalculated { get; init; }
}
