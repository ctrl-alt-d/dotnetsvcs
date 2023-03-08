using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.DtoParm;

namespace Dotnetsvcs.Svc;

public class PreConditions<TParms> : List<IPreCondition<TParms>>, IPreConditions<TParms>
    where TParms : IDtoParm
{
}
