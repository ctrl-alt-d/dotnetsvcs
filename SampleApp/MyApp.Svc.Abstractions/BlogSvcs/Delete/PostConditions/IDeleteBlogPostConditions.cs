using Dotnetsvcs.Svc.Abstractions;
using MyApp.DtoParm.BlogParm.Delete;
using MyApp.Models;

namespace MyApp.Svcs.Abstractions.BlogSvcs.Create.PostConditions;
public interface IDeleteBlogPostConditions : IPostCondition<Blog, DeleteBlogParms> { }
