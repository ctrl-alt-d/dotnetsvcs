using Dotnetsvcs.Svc.Abstractions.BaseOps;
using Dotnetsvcs.Svc.Abstractions.HelperInterfaces;

namespace Dotnetsvcs.Svc.BaseOps;

public abstract class DbOpBase : IDisposable, IDbOpBase {
    protected virtual IDbCtxWrapperFactory DbCtxWrapperFactory { get; }
    private IDbCtxWrapper? _myDbCtxWrapper;
    private IDbCtxWrapper? _externalDbCtxWrapper;

    protected DbOpBase(IDbCtxWrapperFactory dbCtxWrapperFactory) {
        DbCtxWrapperFactory = dbCtxWrapperFactory;
    }

    protected virtual void UseDbCtxWrapper(IDbCtxWrapper dbCtxWrapper) {
        _externalDbCtxWrapper = dbCtxWrapper;
    }

    protected virtual IDbCtxWrapper DbCtxWrapper {
        get {
            if (_externalDbCtxWrapper != null)
                return _externalDbCtxWrapper;

            // Create if not exists
            _myDbCtxWrapper ??=
                    DbCtxWrapperFactory
                    .CreateCtx();

            return _myDbCtxWrapper;
        }
    }

    public virtual void Dispose()
        =>
        _myDbCtxWrapper?
        .Dispose();
}