using Newtonsoft.Json;
using System.Net;

namespace Rest_Sharp.DataModels
{
    public class BookingModel
    {
        [JsonProperty("bookingid")]
        public long BookingID { get; set; }

        [JsonProperty("booking")]
        public Booking Booking { get; set; }
        public HttpStatusCode StatusCode { get; internal set; }
    }
    public class Booking
    {

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("totalprice")]
        public long Totalprice { get; set; }

        [JsonProperty("depositpaid")]
        public bool Depositpaid { get; set; }

        [JsonProperty("bookingdates")]
        public Bookingdates Bookingdates { get; set; }

        [JsonProperty("additionalneeds")]
        public string Additionalneeds { get; set; }
    }

    public partial class Bookingdates
    {
        [JsonProperty("checkin")]
        public DateTime Checkin { get; set; }

        [JsonProperty("checkout")]
        public DateTime Checkout { get; set; }
    }
}