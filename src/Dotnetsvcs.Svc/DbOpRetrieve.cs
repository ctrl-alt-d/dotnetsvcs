using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.BaseOps;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc;

public abstract class DbOpRetrieve<T> : 
    DbOpBase, 
    IDbOpRetrieve<T> 
    
    where T : class
{

    public DbOpRetrieve(IDbCtxWrapperFactory dbCtxWrapperFactory)
        : base(dbCtxWrapperFactory) {
    }

    protected abstract Task<Expression<Func<T, bool>>> GetFilterExpression();

    public async Task<IQueryable<T>> Do() {

        var where = await GetFilterExpression();

        var result =
            DbCtxWrapper
            .SetAsNoTracking<T>()
            .Where(where)
            ;

        return result;

    }

}
