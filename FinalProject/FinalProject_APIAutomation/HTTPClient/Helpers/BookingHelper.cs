using HTTPClient.DataModels;
using Newtonsoft.Json;
using System.Text;
using HTTPClient.Resources;

namespace HTTPClient.Helpers
{
    public class BookingHelper
    {
        public async Task<HttpResponseMessage> SendAsyncFunction(HttpClient client, HttpMethod method, string url, BookingModel data = null, string auth = null)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            httpRequestMessage.Method = method;
            httpRequestMessage.RequestUri = new Uri(url);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            httpRequestMessage.Headers.Add("Cookie", $"token={auth}");

            if (data != null)
            {
                var request = JsonConvert.SerializeObject(data);
                httpRequestMessage.Content = new StringContent(request, Encoding.UTF8, "application/json");
            }

            var httpResponse = await client.SendAsync(httpRequestMessage);

            return httpResponse;

        }

        public async Task<string> GetAuthorizationToken(HttpClient client)
        {

            var loginData = new LoginModels()
            {
                Username = Constants.AuthorizationUsername,
                Password = Constants.AuthorizationPassword
            };

            var request = JsonConvert.SerializeObject(loginData);
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(Endpoints.Authenticate(), postRequest);

            var result = JsonConvert.DeserializeObject<TokenModel>(response.Content.ReadAsStringAsync().Result);

            if (result.Token != null)
            {
                return result.Token;
            }
            else
            {
                throw new Exception("Invalid Credentials");
            }
        }
    }
}