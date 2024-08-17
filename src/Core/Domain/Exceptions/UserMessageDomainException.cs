namespace OpsManagerAPI.Domain.Exceptions;
public class UserMessageDomainException : Exception
{
    public UserMessageDomainException()
    {
    }

    public UserMessageDomainException(string message)
        : base(message)
    {
    }

    public UserMessageDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}