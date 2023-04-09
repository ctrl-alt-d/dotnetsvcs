using Dotnetsvcs.DtoData.Abstractions;

namespace MyApp.DtoData.BlogDtosData;

public class BlogDtoData : IDtoData
{
    public int Id { get; init; }
    public decimal? Preu { get; init; }
    public byte[] TimeStamp { get; init; } = default!;
    public object?[]? CategoriaKey { get; init; }
    public string CategoriaDisplay { get; init; } = default!;
    public string Titol { get; init; } = default!;
    public int Rating { get; init; }
    public int NumPostsCalculated { get; init; }
}
