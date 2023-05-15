using System.Text.Json;

namespace Capella.RestCountries.Api.Services
{
    public static class JsonExtensions
    {
        public static readonly JsonSerializerOptions DefaultJsonOptions = new JsonSerializerOptions
        {
            WriteIndented = false,
            PropertyNameCaseInsensitive = true
        };

        public static string ToJsonString(this object obj)
        {
            string jsonString = JsonSerializer.Serialize(obj, DefaultJsonOptions);
            return jsonString;
        }
    }
}
