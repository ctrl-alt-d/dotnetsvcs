namespace Dotnetsvcs.Facade.Abstractions;
public class DtoError
{
    public DtoError() {
    }

    public DtoError(string message, string? member = null) {
        Member=member;
        Message=message;
    }

    public string? Member { get; set; }
    public string Message { get; set; } = default!;

}
