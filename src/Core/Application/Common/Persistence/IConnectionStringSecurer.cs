﻿namespace OpsManagerAPI.Application.Common.Persistence;

public interface IConnectionStringSecurer
{
    string? MakeSecure(string? connectionString);
}