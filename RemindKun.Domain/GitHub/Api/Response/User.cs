using Newtonsoft.Json;

namespace RemindKun.Domain.GitHub.Models.Issues.Entities.Api.Response
{
    public sealed class User
    {
        [JsonProperty("login")]
        public string Login { get; set; }
    }
}
