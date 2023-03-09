namespace Dotnetsvcs.DbCtx.Abstractions.Transactions;

public interface ITxWrapper : IDisposable, IAsyncDisposable {
    void Rollback();
    void Commit();

}
