﻿using System.Security.Claims;

namespace OpsManagerAPI.Infrastructure.Auth;
public interface ICurrentUserInitializer
{
    void SetCurrentUser(ClaimsPrincipal user);

    void SetCurrentUserId(string userId);
}