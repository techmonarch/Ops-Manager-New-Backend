using System;

namespace OpsManagerAPI.Infrastructure.Exceptions;

public class PermissionDeniedException : Exception
{
    public PermissionDeniedException(string permission)
        : base($"User does not have the required permission: {permission}")
    {
    }
}
