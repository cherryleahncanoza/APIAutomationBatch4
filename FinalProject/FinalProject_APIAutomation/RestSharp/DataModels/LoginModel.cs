using Newtonsoft.Json;

namespace Rest_Sharp.DataModels
{
    public partial class LoginModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

    }
}
