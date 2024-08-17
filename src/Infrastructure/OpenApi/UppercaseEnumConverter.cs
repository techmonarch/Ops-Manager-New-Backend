using System.Text.Json;
using System.Text.Json.Serialization;
namespace OpsManagerAPI.Infrastructure.OpenApi;
public class UppercaseEnumConverter : JsonConverter<Enum>
{
    public override void Write(Utf8JsonWriter writer, Enum value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString().ToUpper());
    }

    public override Enum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? enumString = reader.GetString();
        return (Enum)Enum.Parse(typeToConvert, enumString!, ignoreCase: true);
    }
}
