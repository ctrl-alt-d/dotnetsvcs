using Dotnetsvcs.DtoData.Abstractions;
using Dotnetsvcs.Facade.Abstractions;
using MyApp.DtoData.BlogDtosData;
using MyApp.DtoParm.BlogParm.Create;
using MyApp.DtoParm.BlogParm.Delete;
using MyApp.DtoParm.BlogParm.Retrieve;

namespace MyApp.Facade.BlazorServer.Abstractions;
public interface IBlogFacade:  IFacade {
    Task<DtoResult<BlogDtoData>> Create(CreateBlogParms parms);
    Task<DtoResult<BlogDtoData>> CreateWithTx(CreateBlogParms parms);
    Task<DtoResult<DtoDataRetrieve<BlogDtoData>>> Retrieve(RetrieveBlogParms pams);
    Task<DtoResult<BlogDtoData>> Delete(DeleteBlogParms parms);
}