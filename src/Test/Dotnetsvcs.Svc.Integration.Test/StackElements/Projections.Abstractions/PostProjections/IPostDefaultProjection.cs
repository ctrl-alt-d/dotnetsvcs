using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.DtoResult.PostDtosResult;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Projections.Abstractions.PostProjections;
public interface IPostDefaultProjection : IProjection<Post, PosTDtoData> { }