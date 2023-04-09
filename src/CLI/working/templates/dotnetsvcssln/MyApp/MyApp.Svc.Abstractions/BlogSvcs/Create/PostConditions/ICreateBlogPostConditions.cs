using Dotnetsvcs.Svc.Abstractions;
using MyApp.DtoParm.BlogParm.Create;
using MyApp.Models;

namespace MyApp.Svcs.Abstractions.BlogSvcs.Create.PostConditions;
public interface ICreateBlogPostConditions : IPostCondition<Blog, CreateBlogParms> { }
