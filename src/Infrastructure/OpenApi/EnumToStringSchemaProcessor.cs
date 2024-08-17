using Microsoft.OpenApi.Any;
using NJsonSchema;
using NJsonSchema.Generation;

namespace OpsManagerAPI.Infrastructure.OpenApi;
public class EnumToStringSchemaProcessor : ISchemaProcessor
{
    public void Process(SchemaProcessorContext context)
    {
        if (context.Schema.Enumeration.Any())
        {
            context.Schema.Type = JsonObjectType.String;
            context.Schema.Enumeration.Clear();
            context.Schema.Enumeration.ToList().AddRange(
                context.Schema.Enumeration.Select(e => new OpenApiString(e.ToString())));
        }
    }
}