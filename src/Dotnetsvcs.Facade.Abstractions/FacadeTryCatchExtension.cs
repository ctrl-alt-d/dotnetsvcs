using Dotnetsvcs.DbCtx.Abstractions.Transactions;
using Dotnetsvcs.DtoData.Abstractions;
using Dotnetsvcs.Svc.Abstractions.Exceptions;
using Microsoft.Extensions.Logging;

namespace Dotnetsvcs.Facade.Abstractions;
public static class FacadeTryCatchExtension {

    public static async Task<DtoResult<TDtoData>> TryCatch<TDtoData>(this Func<Task<TDtoData>> svc, ITxWrapper? tx = null, ILogger? logger = null)
        where TDtoData : IDtoData {

        try {
            var data = await svc();
            tx?.Commit();
            return new DtoResult<TDtoData>(data);
        }
        catch (SvcException e) {
            tx?.Rollback();
            var error = new DtoError(e.Message, e.Member);
            return new DtoResult<TDtoData>(error);
        }
        catch (Exception e) {
            tx?.Rollback();
            logger?.LogError(e, "Unexpected error executing service: {}", e.Message);
            throw;
        }
    }
}


