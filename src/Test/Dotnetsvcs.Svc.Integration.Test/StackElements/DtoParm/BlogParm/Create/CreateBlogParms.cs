using Dotnetsvcs.DtoParm.Abstractions;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.DtoParm.BlogParm.Create;

public class CreateBlogParms : DtoParmCreate {
    public string Titol { get; set; } = default!;
    public int Rating { get; set; } = default!;
    public int WithNposts { get; set; } = default!;
}
