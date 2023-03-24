using Newtonsoft.Json;

namespace HTTPClient.DataModels
{
    public class BookingResponse
    {
        [JsonProperty("bookingid")]
        public long BookingID { get; set; }

        [JsonProperty("booking")]
        public BookingModel BookingDetails { get; set; }
    }
}