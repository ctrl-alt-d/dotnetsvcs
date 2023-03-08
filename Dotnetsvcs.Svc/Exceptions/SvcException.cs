using System.Runtime.Serialization;

namespace Dotnetsvcs.Svc.Exceptions;

public class SvcException : Exception
{
    public SvcException()
    {
    }

    public SvcException(string? message) : base(message)
    {
    }

    public SvcException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected SvcException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
