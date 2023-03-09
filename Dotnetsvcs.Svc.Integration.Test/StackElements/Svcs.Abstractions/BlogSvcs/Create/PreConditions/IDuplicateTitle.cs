using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.DtoParm.BlogParm.Create;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Abstractions.BlogSvcs.Create.PreConditions {
    public interface IDuplicateTitle : IPreCondition<CreateBlogParms> {
    }
}