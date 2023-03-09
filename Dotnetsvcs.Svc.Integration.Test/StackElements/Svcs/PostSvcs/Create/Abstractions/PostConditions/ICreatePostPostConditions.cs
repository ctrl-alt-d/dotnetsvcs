using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.PostSvcs.Create.Artifacts;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.PostSvcs.Create.Abstractions.PostConditions;
public interface ICreatePostPostConditions : IPostCondition<Post, CreatePostParms> { }
