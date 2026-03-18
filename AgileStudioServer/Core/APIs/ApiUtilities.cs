using System.Text.Json;

namespace AgileStudioServer.Core.APIs
{
    public class ApiUtilities
    {
        public static TDto GetDtoFromData<TDto>(object data)
        {
            var dto = JsonSerializer.Deserialize<TDto>(
                ((JsonElement)data).GetRawText(), GetJsonSerializerOptions());

            return dto == null ?
                throw new ArgumentException($"Data cannot be deserialized to a '{typeof(TDto)}'") :
                dto;
        }

        public static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }
    }
}