namespace Dotnetsvcs.DtoParm.Abstractions.Criteria;

public class IntCriteriaDto
{
    public enum OperationType
    {
        Equals,
        GreatherThan,
        GreatherEqualThan,
        LessThan,
        LessEqualThan,
        InRange,
        Empty,

        NotEquals,
        NotInRange,
        NotEmpty,
    }

    public int? Value1 { get; set; }
    public int? Value2 { get; set; }

    public OperationType Operation { get; set; } = OperationType.Equals;
}
