using System.Text.Json;

namespace Aplication.Common.Helpers.Serealizer
{
    public class SerializerObject
    {
        public static string ConvertObjectToJsonIndented<TValue>(TValue objectToJson)
        {
            JsonSerializerOptions jsonSerializerOptions = new()
            {
                WriteIndented = true
            };

            JsonSerializerOptions options = jsonSerializerOptions;

            return JsonSerializer.Serialize(objectToJson, options);
        }
    }
}
