using Dotnetsvcs.Svc.Abstractions;
using MyApp.DtoParm.BlogParm.Create;
using MyApp.Models;

namespace MyApp.Svcs.Abstractions.BlogSvcs.Create;

public interface ICreateBlogService : IDbOpCreate<Blog, CreateBlogParms> { }