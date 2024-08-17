using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace OpsManagerAPI.Infrastructure.Validations;
public static class Extensions
{
    public static IServiceCollection AddBehaviours(this IServiceCollection services) => services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
}
