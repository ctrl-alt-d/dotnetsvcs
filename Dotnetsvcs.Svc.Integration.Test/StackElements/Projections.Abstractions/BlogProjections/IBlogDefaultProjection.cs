using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.DtoResult.BlogDtosResult;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Projections.Abstractions.BlogProjections;
public interface IBlogDefaultProjection : IProjection<Blog, BlogDtoResult> { }