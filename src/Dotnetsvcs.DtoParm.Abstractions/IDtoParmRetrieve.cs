namespace Dotnetsvcs.DtoParm.Abstractions; 
public interface IDtoParmRetrieve: IDtoParm {
    int ItemsPerPage { get; set; }
    int Page { get; set; }
    bool TotalCountRequired { get; set; }
}