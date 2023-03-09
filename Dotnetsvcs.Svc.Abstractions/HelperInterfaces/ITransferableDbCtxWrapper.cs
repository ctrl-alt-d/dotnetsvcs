using Dotnetsvcs.DbCtx.Abstractions;

namespace Dotnetsvcs.Svc.Abstractions.HelperInterfaces;

public interface ITransferableDbCtxWrapper {
    void UseDbCtxWrapper(IDbCtxWrapper dbCtxWrapper);
    void TransferDbCtxWrapper(ITransferableDbCtxWrapper to);
}