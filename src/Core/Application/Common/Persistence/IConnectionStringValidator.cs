﻿namespace OpsManagerAPI.Application.Common.Persistence;

public interface IConnectionStringValidator
{
    bool TryValidate(string connectionString);
}