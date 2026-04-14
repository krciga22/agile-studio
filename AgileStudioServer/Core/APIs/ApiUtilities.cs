using System.Text.Json;
using System.Text.Json.Nodes;

namespace AgileStudioServer.Core.APIs
{
    public class ApiUtilities
    {
        public static TDto GetDtoFromData<TDto>(object data)
        {
            return (TDto) GetDtoFromData(data, typeof(TDto));
        }

        public static object GetDtoFromData(object data, Type dtoType)
        {
            var options = GetJsonSerializerOptions();
            var dto = (data is JsonNode) ? (
                    JsonSerializer.Deserialize(
                    ((JsonNode)data), dtoType, options)
                ) :
                (data is JsonElement) ? (
                    JsonSerializer.Deserialize(
                    ((JsonElement)data).GetRawText(), dtoType, options)
                ) : null;

            return dto == null ?
                throw new ArgumentException($"Data cannot be deserialized to a '{nameof(dtoType)}'") :
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