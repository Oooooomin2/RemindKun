using Newtonsoft.Json;

namespace RemindKun.Infrastructure.Api.Response
{
    public sealed class User
    {
        [JsonProperty("login")]
        public string Login { get; set; }
    }
}
