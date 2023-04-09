using Dotnetsvcs.DtoParm.Abstractions;
using Dotnetsvcs.DtoParm.Abstractions.Criteria;

namespace MyApp.DtoParm.BlogParm.Retrieve;
public class RetrieveBlogParms: DtoParmRetrieve {
    public StringCriteriaDto TitolCriteria { get; set; } = default!;
    public IntCriteriaDto RatingCriteria { get; set; } = default!;
}
