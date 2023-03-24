using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPClient.Resources
{
    public class Endpoints
    {
        private const string BaseUrl = "https://restful-booker.herokuapp.com";
        public static string Authenticate() => $"{BaseUrl}/auth";
        public static string CreateBooking() => $"{BaseUrl}/booking";
        public static string GetBookingById(long bookingId) => $"{BaseUrl}/booking/{bookingId}";
        public static string UpdateBookingById(long bookingId) => $"{BaseUrl}/booking/{bookingId}";
        public static string DeleteBookingById(long bookingId) => $"{BaseUrl}/booking/{bookingId}";

    }
}