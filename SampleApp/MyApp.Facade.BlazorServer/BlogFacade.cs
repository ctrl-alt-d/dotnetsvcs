using Dotnetsvcs.Facade.Abstractions;
using MyApp.DtoData.BlogDtosData;
using MyApp.DtoParm.BlogParm.Create;
using MyApp.Svcs.Abstractions.BlogSvcs.Create;
using MyApp.Projections.Abstractions.BlogProjections;
using Dotnetsvcs.DbCtx.Abstractions;
using MyApp.Svcs.Abstractions.BlogSvcs.Retrieve;
using MyApp.Facade.BlazorServer.Abstractions;
using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.DtoData.Abstractions;
using MyApp.DtoParm.BlogParm.Retrieve;
using MyApp.DtoParm.BlogParm.Delete;
using MyApp.Svcs.Abstractions.BlogSvcs.Delete;

namespace MyApp.Facade.BlazorServer;
public class BlogFacade : IBlogFacade {
    public BlogFacade(
        IDbCtxWrapperFactory dbCtxWrapperFactory,
        ISvcFactory<ICreateBlogService> createBlogServiceFactory,
        ISvcFactory<IRetrieveBlogService> retrieveBlogServiceFactory,
        IProjectionFactory<IBlogDefaultProjection> blogProjectionFactory,
        ISvcFactory<IDeleteBlogService> deleteBlogServiceFactory)
    {
        DbCtxWrapperFactory=dbCtxWrapperFactory;
        CreateBlogServiceFactory=createBlogServiceFactory;
        RetrieveBlogServiceFactory=retrieveBlogServiceFactory;
        BlogProjectionFactory=blogProjectionFactory;
        DeleteBlogServiceFactory=deleteBlogServiceFactory;
    }


    protected virtual IDbCtxWrapperFactory DbCtxWrapperFactory { get; }
    protected virtual ISvcFactory<ICreateBlogService> CreateBlogServiceFactory { get; }
    protected virtual ISvcFactory<IRetrieveBlogService> RetrieveBlogServiceFactory { get; }
    protected virtual IProjectionFactory<IBlogDefaultProjection> BlogProjectionFactory { get; }
    protected virtual ISvcFactory<IDeleteBlogService> DeleteBlogServiceFactory { get; }

    public virtual async Task<DtoResult<BlogDtoData>> CreateWithTx(CreateBlogParms parms) {
        using var ctx = DbCtxWrapperFactory.CreateCtx();
        using var tx = ctx.BeginTransaction();
        using var svc = CreateBlogServiceFactory.Create();
        using var projection = BlogProjectionFactory.Create();
        var operation = () => svc.Do(parms, projection, ctx);
        return await operation.TryCatch(tx);
    }

    public virtual async Task<DtoResult<BlogDtoData>> Create(CreateBlogParms parms) {
        using var svc = CreateBlogServiceFactory.Create();
        using var projection = BlogProjectionFactory.Create();
        var operation = () => svc.Do(parms, projection);
        return await operation.TryCatch();
    }

    public void Dispose() {
    }

    public virtual async Task<DtoResult<DtoDataRetrieve<BlogDtoData>>> Retrieve(RetrieveBlogParms parms) {
        using var svc = RetrieveBlogServiceFactory.Create();
        using var projection = BlogProjectionFactory.Create();
        var operation = () => svc.Do(parms, projection);
        return await operation.TryCatch();
    }

    public virtual async Task<DtoResult<BlogDtoData>> Delete(DeleteBlogParms parms)
    {
        using var svc = DeleteBlogServiceFactory.Create();
        using var projection = BlogProjectionFactory.Create();
        var operation = () => svc.Do(parms, projection);
        return await operation.TryCatch();
    }
}
