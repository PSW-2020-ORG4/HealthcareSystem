using Newtonsoft.Json;

namespace IntegrationAdapters.Dtos
{
    public class PushPayload
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}