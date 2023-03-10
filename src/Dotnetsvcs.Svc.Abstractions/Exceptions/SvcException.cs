namespace Dotnetsvcs.Svc.Abstractions.Exceptions;

public class SvcException : Exception
{
    public SvcException()
    {
    }

    public SvcException(string message, string? member = null) : base(message)
    {
        Member = member;
    }

    public string? Member { get; }

}
