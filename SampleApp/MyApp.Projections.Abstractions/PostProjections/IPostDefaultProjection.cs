using Dotnetsvcs.Svc.Abstractions;
using MyApp.DtoData.PostDtosData;
using MyApp.Models;

namespace MyApp.Projections.Abstractions.PostProjections;
public interface IPostDefaultProjection : IProjection<Post, PostDtoData> { }