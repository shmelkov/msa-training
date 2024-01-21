using System.Text.Json;

namespace Portal.Common.Helpers
{
    public class BaseParser
    {
        public static bool DeserializeJson<T>(string json, out T deserializedObject)
        {
            try
            {
                deserializedObject = JsonSerializer.Deserialize<T>(json);

                return true;
            }
            catch(JsonException ex)
            {
                deserializedObject = default;

                return false;
            }
        }
    }
}
