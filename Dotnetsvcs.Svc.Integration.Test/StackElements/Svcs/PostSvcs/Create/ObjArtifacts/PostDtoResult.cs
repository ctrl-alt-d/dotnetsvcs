using Dotnetsvcs.Svc.DtoResult;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.PostSvcs.Create.Artifacts;

public class PostDtoResult : IDtoResult {
    public string Descripcio { get; set; } = default!;
    public bool EsVisible { get; set; } = default!;
    public string BlogDisplay { get; set; } = default!;
    public object[] BlogKey { get; set; } = default!;
}
