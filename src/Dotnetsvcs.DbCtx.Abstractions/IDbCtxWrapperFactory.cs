namespace Dotnetsvcs.DbCtx.Abstractions;

public interface IDbCtxWrapperFactory {
    IDbCtxWrapper CreateCtx();
}