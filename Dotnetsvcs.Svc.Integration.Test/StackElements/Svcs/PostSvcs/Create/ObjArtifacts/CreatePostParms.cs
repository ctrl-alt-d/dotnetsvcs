using Dotnetsvcs.Svc.DtoParm;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.PostSvcs.Create.Artifacts;

public class CreatePostParms : DtoParmCreate
{
    public string Descripcio { get; set; } = default!;
    public object?[]? BlogKey { get; set; } = default!;
}
