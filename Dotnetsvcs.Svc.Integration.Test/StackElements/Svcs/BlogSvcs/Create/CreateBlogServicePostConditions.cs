using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.Abstractions;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create;

public class CreateBlogServicePostConditions :
    PostConditions<Blog, CreateBlogParms>,
    ICreateBlogServicePostConditions
{
    public CreateBlogServicePostConditions()
    {
    }
}
