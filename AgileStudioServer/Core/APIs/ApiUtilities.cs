using System.Text.Json;

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
            var dto = JsonSerializer.Deserialize(
                ((JsonElement)data).GetRawText(), dtoType, 
                GetJsonSerializerOptions());

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