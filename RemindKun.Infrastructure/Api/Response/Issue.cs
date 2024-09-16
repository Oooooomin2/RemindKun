using Newtonsoft.Json;

namespace RemindKun.Infrastructure.Api.Response
{
    public sealed class Issue
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("html_url")]
        public string Url { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("labels")]
        public List<Label> Labels { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
