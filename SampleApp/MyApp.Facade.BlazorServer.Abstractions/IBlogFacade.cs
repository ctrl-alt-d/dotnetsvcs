using Dotnetsvcs.Facade.Abstractions;
using MyApp.DtoData.BlogDtosData;
using MyApp.DtoParm.BlogParm.Create;
using MyApp.Models;

namespace MyApp.Facade.BlazorServer.Abstractions; 
public interface IBlogFacade:  IFacade {
    Task<DtoResult<BlogDtoData>> Create(CreateBlogParms parms);
    Task<IQueryable<Blog>> Retrieve();
}