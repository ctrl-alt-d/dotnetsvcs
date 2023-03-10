using Dotnetsvcs.DtoData.Abstractions;

namespace Dotnetsvcs.Facade.Abstractions;

public class DtoResult<TDtoData>
    where TDtoData : IDtoData {

    public DtoResult() {
    }

    public DtoResult(List<DtoError> errors) {
        Errors.AddRange(errors);
    }

    public DtoResult(DtoError error) {
        Errors.Add(error);
    }

    public DtoResult(TDtoData? data, List<string> info, List<string> warnings) {
        Data = data;
        Info.AddRange(info);
        Warnings.AddRange(warnings);
    }

    public DtoResult(TDtoData? data) {
        Data=data;
    }

    public bool HasData => Data != null;
    public TDtoData GetData() => Data!;

    public TDtoData? Data { get; set; }
    public List<DtoError> Errors { get; set; } = new();
    public List<string> Info { get; set; } = new();
    public List<string> Warnings { get; set; } = new();

}
