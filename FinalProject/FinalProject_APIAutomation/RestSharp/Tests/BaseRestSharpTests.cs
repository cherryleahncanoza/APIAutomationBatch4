using RestSharp;
using Rest_Sharp.DataModels;

namespace Rest_Sharp.Tests
{
    public class BaseRestSharpTests
    {
        public RestClient restClient { get; set; }

        public BookingModel booking { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            restClient = new RestClient();
        }
    }
}
