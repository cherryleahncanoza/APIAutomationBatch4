using Newtonsoft.Json;

namespace HTTPClient.DataModels
{
    public class BookingDates
    {
        [JsonProperty("checkin")]
        public string CheckIn { get; set; }

        [JsonProperty("checkout")]
        public string CheckOut { get; set; }
    }
}