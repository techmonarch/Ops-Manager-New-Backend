using System.Net;

namespace OpsManagerAPI.Application.Common.Exceptions;
public class NotFoundException : CustomException
{
    public NotFoundException(string message)
        : base(message, null, HttpStatusCode.NotFound)
    {
    }
}

