using Dotnetsvcs.Svc.DtoParm;

namespace Dotnetsvcs.Svc.Abstractions;

public interface IPostConditions<T, TParms> : IEnumerable<IPostCondition<T, TParms>>
    where TParms : IDtoParm
    where T : class
{ }
