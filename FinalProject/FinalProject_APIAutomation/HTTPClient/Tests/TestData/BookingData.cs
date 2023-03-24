using HTTPClient.DataModels;

namespace HTTPClient.Tests.TestData
{
    public class BookingData
    {
        public static BookingModel booking(bool updateBooking)
        {
            var bookingData = new BookingModel
            {
                FirstName = "Taylor",
                LastName = "Swift",
                TotalPrice = 1313,
                DepositPaid = true,
                BookingDates = new BookingDates
                {
                    CheckIn = "03/17/2023",
                    CheckOut = "03/18/2023"
                },
                AdditionalNeeds = "Concert Tickets"
            };

            if (updateBooking) 
            {
                bookingData.FirstName = "Karlie";
                bookingData.LastName = "Kloss";
            }

            return bookingData;

        }
    }
}