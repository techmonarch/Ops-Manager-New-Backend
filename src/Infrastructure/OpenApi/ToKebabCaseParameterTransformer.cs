using Microsoft.AspNetCore.Routing;
using System.Text.RegularExpressions;

namespace OpsManagerAPI.Infrastructure.OpenApi;

public class ToKebabCaseParameterTransformer : IOutboundParameterTransformer
{
    public string TransformOutbound(object? value)
    {
        if (value is null) throw new ApplicationException("Controller ToKebab Case conversion fail");
        string str = value?.ToString()!;
        return Regex.Replace(str, "([a-z])([A-Z])", "$1-$2").ToLower();
    }
}