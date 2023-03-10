using Dotnetsvcs.Svc.Abstractions;
using MyApp.DtoParm.PostParm.Create;
using MyApp.Models;

namespace MyApp.Svcs.Abstractions.PostSvcs.Create;

public interface ICreatePostService : IDbOpCreate<Post, CreatePostParms> { }