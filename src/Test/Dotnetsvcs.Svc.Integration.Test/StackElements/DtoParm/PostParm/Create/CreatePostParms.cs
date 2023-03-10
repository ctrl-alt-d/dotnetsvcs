using Dotnetsvcs.DtoParm.Abstractions;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.DtoParm.PostParm.Create;

public class CreatePostParms : DtoParmCreate {
    public string Descripcio { get; set; } = default!;
    public object?[]? BlogKey { get; set; } = default!;
}
