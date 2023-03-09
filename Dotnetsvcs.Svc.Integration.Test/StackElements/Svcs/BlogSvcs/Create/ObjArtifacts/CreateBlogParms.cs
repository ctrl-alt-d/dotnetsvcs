using Dotnetsvcs.Svc.DtoParm;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.Artifacts;

public class CreateBlogParms : DtoParmCreate
{
    public string Titol { get; set; } = default!;
    public int Rating { get; set; } = default!;
    public int WithNposts { get; set; } = default!;
}
