using Newtonsoft.Json;

namespace Rest_Sharp.DataModels
{
    public partial class TokenModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }

}