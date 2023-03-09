using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.PostSvcs.Create.Artifacts;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.PostSvcs.Create.Abstractions;

public interface ICreatePostService : IDbOpCreate<Post, CreatePostParms> { }