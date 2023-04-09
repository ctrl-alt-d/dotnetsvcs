using Dotnetsvcs.DtoParm.Abstractions;

namespace MyApp.DtoParm.PostParm.Create;

public class CreatePostParms : DtoParmCreate
{
    public string Descripcio { get; set; } = default!;
    public object?[]? BlogKey { get; set; } = default!;
}
