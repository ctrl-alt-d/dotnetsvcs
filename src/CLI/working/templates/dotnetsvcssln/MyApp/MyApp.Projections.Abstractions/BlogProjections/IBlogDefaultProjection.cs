using Dotnetsvcs.Svc.Abstractions;
using MyApp.DtoData.BlogDtosData;
using MyApp.Models;

namespace MyApp.Projections.Abstractions.BlogProjections;
public interface IBlogDefaultProjection : IProjection<Blog, BlogDtoData> { }