using Dotnetsvcs.DtoParm.Abstractions;
using Dotnetsvcs.Svc.Abstractions;

namespace MyApp.Svcs.Abstractions.Common.PreConditions;

public interface IIsLoggedPreCondition: IPreCondition<IDtoParm>
{
}