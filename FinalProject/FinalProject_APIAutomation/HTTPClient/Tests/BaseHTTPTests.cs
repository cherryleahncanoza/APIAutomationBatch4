using HTTPClient.DataModels;
using HTTPClient.Helpers;

namespace HTTPClient.Tests
{
    public class BaseHTTPTests
    {
        public HttpClient httpClient { get; set; }
        public BookingModel bookingModel { get; set; }
        public BookingHelper bookingHelper { get; set; }
        public string token;

        [TestInitialize]
        public async Task Initialize()
        {
            httpClient = new HttpClient();
            bookingHelper = new BookingHelper();
            token = await bookingHelper.GetAuthorizationToken(httpClient);
        }
    }
}
