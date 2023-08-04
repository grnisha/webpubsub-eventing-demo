using Newtonsoft.Json;

namespace Demo.WebPubSub.Web.Models
{public class Negotiate
    {
        [JsonProperty("baseUrl")]
        public required string BaseUrl { get; set; }

        [JsonProperty("url")]
        public required string Url { get; set; }

        [JsonProperty("accessToken")]
        public required string AccessToken { get; set; }
    }
}