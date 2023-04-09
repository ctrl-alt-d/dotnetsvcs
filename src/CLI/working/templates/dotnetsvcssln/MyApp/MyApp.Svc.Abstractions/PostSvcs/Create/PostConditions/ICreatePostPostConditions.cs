using Dotnetsvcs.Svc.Abstractions;
using MyApp.DtoParm.PostParm.Create;
using MyApp.Models;

namespace MyApp.Svcs.Abstractions.PostSvcs.Create.PostConditions;
public interface ICreatePostPostConditions : IPostCondition<Post, CreatePostParms> { }
