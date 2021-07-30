using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace KanaTester
{
    public class SerializingService : ISerializingService
    {
        private readonly JsonSerializerOptions _options = new ()
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };
        
        public string Serialize<T>(T item)
        {
            return JsonSerializer.Serialize(item, _options);
        }

        public T Deserialize<T>(string item)
        {
            return JsonSerializer.Deserialize<T>(item, _options);
        }
    }
}