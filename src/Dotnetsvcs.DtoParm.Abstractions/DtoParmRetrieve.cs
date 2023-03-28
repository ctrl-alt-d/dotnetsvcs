namespace Dotnetsvcs.DtoParm.Abstractions;

public abstract class DtoParmRetrieve :
 IDtoParmRetrieve {
    public int Page { get; set; }
    public int ItemsPerPage { get; set; }
    public bool TotalCountRequired { get; set; }
}
