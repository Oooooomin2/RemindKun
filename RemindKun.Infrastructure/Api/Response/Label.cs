using Newtonsoft.Json;

namespace RemindKun.Infrastructure.Api.Response
{
    public sealed class Label
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
