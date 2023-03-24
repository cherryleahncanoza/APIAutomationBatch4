using Rest_Sharp.DataModels;
using System;

namespace Rest_Sharp.Tests.TestData
{
    public class BookingData
    {
        public static Booking booking(bool updateBooking)
        {
            var bookingData = new Booking
            {
                Firstname = "Taylor",
                Lastname = "Swift",
                Totalprice = 1313,
                Depositpaid = true,
                Bookingdates = new Bookingdates
                {
                    Checkin = DateTime.Parse("03/17/2023"),
                    Checkout = DateTime.Parse("03/18/2023")
                },
                Additionalneeds = "Concert Tickets"
            };

            if (updateBooking)
            {
                bookingData.Firstname = "Karlie";
                bookingData.Lastname = "Kloss";
            }

            return bookingData;

        }
    }
    
}