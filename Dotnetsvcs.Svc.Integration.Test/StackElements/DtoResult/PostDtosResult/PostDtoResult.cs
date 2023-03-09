using Dotnetsvcs.DtoResult;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.DtoResult.PostDtosResult;

public class PostDtoResult : IDtoResult {
    public string Descripcio { get; set; } = default!;
    public bool EsVisible { get; set; } = default!;
    public string BlogDisplay { get; set; } = default!;
    public object[] BlogKey { get; set; } = default!;
    public int StatisticsTotalBlogs { get; set; } = default!;
    public int NumberTwoFromRandomService { get; set; } = default!;
}
