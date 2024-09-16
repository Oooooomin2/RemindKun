using Newtonsoft.Json;

namespace RemindKun.Domain.GitHub.Models.Issues.Entities.Api.Response
{
    public sealed class Label
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
