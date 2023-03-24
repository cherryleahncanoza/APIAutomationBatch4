using Newtonsoft.Json;

namespace HTTPClient.DataModels
{
    public class TokenModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}