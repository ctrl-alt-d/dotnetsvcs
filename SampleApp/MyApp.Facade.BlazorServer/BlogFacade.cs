using Dotnetsvcs.Facade.Abstractions;
using MyApp.DtoData.BlogDtosData;
using MyApp.DtoParm.BlogParm.Create;
using MyApp.Svcs.Abstractions.BlogSvcs.Create;
using MyApp.Projections.Abstractions.BlogProjections;
using Dotnetsvcs.DbCtx.Abstractions;
using MyApp.Svcs.Abstractions.BlogSvcs.Retrieve;
using MyApp.Models;
using MyApp.Facade.BlazorServer.Abstractions;
using Dotnetsvcs.Svc.Abstractions;

namespace MyApp.Facade.BlazorServer;
public class BlogFacade : IBlogFacade {
    public BlogFacade(IDbCtxWrapperFactory dbCtxWrapperFactory, ISvcFactory<ICreateBlogService> createBlogServiceFactory, IRetrieveBlogService retrieveBlogService, IProjectorFactory<IBlogDefaultProjection> blogProjectionFactory) {
        DbCtxWrapperFactory=dbCtxWrapperFactory;
        CreateBlogServiceFactory=createBlogServiceFactory;
        RetrieveBlogService=retrieveBlogService;
        BlogProjectionFactory=blogProjectionFactory;
    }

    protected virtual IDbCtxWrapperFactory DbCtxWrapperFactory { get; }
    protected virtual ISvcFactory<ICreateBlogService> CreateBlogServiceFactory { get; }
    protected virtual IRetrieveBlogService RetrieveBlogService { get; }
    protected virtual IProjectorFactory<IBlogDefaultProjection> BlogProjectionFactory { get; }

    public async Task<DtoResult<BlogDtoData>> Create(CreateBlogParms parms) {
        using var ctx = DbCtxWrapperFactory.CreateCtx();
        using var tx = ctx.BeginTransaction();
        using var svc = CreateBlogServiceFactory.Create();
        using var projection = BlogProjectionFactory.Create();
        var operation = () => svc.Do(parms, projection, ctx);
        return await operation.TryCatch(tx);
    }

    public void Dispose() {
        RetrieveBlogService.Dispose();
    }

    public async Task<IQueryable<Blog>> Retrieve() {
        var data = await RetrieveBlogService.Do();
        return data;
    }

}
