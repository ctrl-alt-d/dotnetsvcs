using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.Abstractions;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create;

public class CreateBlogServicePreConditions :
    PreConditions<CreateBlogParms>,
    ICreateBlogServicePreConditions
{
    public CreateBlogServicePreConditions()
    {
    }
}
