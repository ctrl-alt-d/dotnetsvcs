namespace Dotnetsvcs.DtoData.Abstractions;
public class DtoDataRetrieve<TItem>: IDtoData 
    where TItem: IDtoData {
    public virtual long? TotalCount { get; set; }
    public virtual IEnumerable<TItem> Items { get; set; } = default!;
}
