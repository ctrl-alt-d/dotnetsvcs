using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.DtoParm.PostParm.Create;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Abstractions.PostSvcs.Create;

public interface ICreatePostService : IDbOpCreate<Post, CreatePostParms> { }