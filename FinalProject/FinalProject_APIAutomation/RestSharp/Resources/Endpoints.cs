using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_Sharp.Resources
{
    public static class Endpoints
    {
        public const string BaseURL = "https://restful-booker.herokuapp.com";
        public const string BookingEndpoint = "booking";
        public const string AuthenticationEndpoint = "auth";

        public static string LoginEndpoint() => $"{BaseURL}/{AuthenticationEndpoint}";
        public static string GetBookingById(long id) => $"{BaseURL}/{BookingEndpoint}/{id}";
        public static string PostBooking() => $"{BaseURL}/{BookingEndpoint}";
        public static string PutBookingById(long id) => $"{BaseURL}/{BookingEndpoint}/{id}";
        public static string DeleteBookingById(long id) => $"{BaseURL}/{BookingEndpoint}/{id}";
    }
}