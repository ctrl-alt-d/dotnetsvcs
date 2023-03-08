using Dotnetsvcs.Svc.DtoParm;

namespace Dotnetsvcs.Svc.Abstractions;

public interface IPreConditions<TParms> : IEnumerable<IPreCondition<TParms>>
    where TParms : IDtoParm
{ }
