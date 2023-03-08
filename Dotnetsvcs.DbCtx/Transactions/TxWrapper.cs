using Dotnetsvcs.DbCtx.Abstractions.Transactions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Dotnetsvcs.DbCtx.Transactions;

public class TxWrapper : ITxWrapper
{
    private IDbContextTransaction InnerTx;

    public TxWrapper(IDbContextTransaction innerTx)
    {
        InnerTx = innerTx;
    }

    public void Commit()
        =>
        InnerTx.Commit();

    public void Dispose()
        =>
        InnerTx.Dispose();

    public ValueTask DisposeAsync()
        =>
        InnerTx.DisposeAsync();

    public void Rollback()
        =>
        InnerTx.Rollback();
}
