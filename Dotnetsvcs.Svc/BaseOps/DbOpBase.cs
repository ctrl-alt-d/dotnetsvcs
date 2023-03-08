using Dotnetsvcs.Svc.Abstractions.BaseOps;
using Dotnetsvcs.Svc.Abstractions.HelperInterfaces;

namespace Dotnetsvcs.Svc.BaseOps;

public abstract class DbOpBase : IDisposable, ITransferableDbCtxWrapper, IDbOpBase
{
    protected virtual IDbCtxWrapperFactory DbCtxWrapperFactory { get; }
    private IDbCtxWrapper? _myDbCtxWrapper;
    private IDbCtxWrapper? _externalDbCtxWrapper;

    protected DbOpBase(IDbCtxWrapperFactory dbCtxWrapperFactory)
    {
        DbCtxWrapperFactory = dbCtxWrapperFactory;
    }

    public void UseDbCtxWrapper(IDbCtxWrapper dbCtxWrapper)
    {
        if (_externalDbCtxWrapper == dbCtxWrapper) return;

        if (_externalDbCtxWrapper != null)
            throw new Exception("Change dbcontext is not allowed when external dbcontext is yet initialized");

        if (_myDbCtxWrapper != null)
            throw new Exception("Change dbcontext is not allowed when internal dbcontext is yet initialized");

        _externalDbCtxWrapper = dbCtxWrapper;
    }

    public void TransferDbCtxWrapper(ITransferableDbCtxWrapper to)
        =>
        to
        .UseDbCtxWrapper(this.DbCtxWrapper);


    protected IDbCtxWrapper DbCtxWrapper
    {
        get
        {
            if (_externalDbCtxWrapper != null)
                return _externalDbCtxWrapper;

            // Create if not exists
            _myDbCtxWrapper ??=
                    DbCtxWrapperFactory
                    .CreateCtx();

            return _myDbCtxWrapper;
        }
    }

    public void Dispose()
        =>
        _myDbCtxWrapper?
        .Dispose();
}