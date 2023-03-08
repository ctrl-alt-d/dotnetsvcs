using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.DtoParm;

namespace Dotnetsvcs.Svc;

public class PostConditions<T, TParms> : List<IPostCondition<T, TParms>>, IPostConditions<T, TParms>
    where TParms : IDtoParm
    where T : class
{
}
