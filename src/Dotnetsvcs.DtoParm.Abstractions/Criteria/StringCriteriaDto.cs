namespace Dotnetsvcs.DtoParm.Abstractions.Criteria;

public class StringCriteriaDto
{
    public enum OperationType
    {
        StartsWith,
        Contains,
        Empty
    }

    public string? Pattern { get; set; }
    public OperationType Operation { get; set; } = OperationType.StartsWith;
}
