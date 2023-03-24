using RestSharp;
using System.Net;
using Rest_Sharp.Resources;
using Rest_Sharp.DataModels;
using Rest_Sharp.Helpers;
using Rest_Sharp.Tests;
using Rest_Sharp.Tests.TestData;

namespace Rest_Sharp
{
    [TestClass]
    public class BookingTestBase : BaseRestSharpTests
    {
        private readonly List<BookingModel> cleanUpList = new List<BookingModel>();

        [TestCleanup]
        public async Task TestCleanup()
        {
            foreach (var data in cleanUpList)
            {
                var deleteRestResponse = await BookingHelper.DeleteBookingByID(restClient, booking.BookingID);
            }
        }

        [TestInitialize]
        public async Task TestInitialize()
        {
            booking = await BookingHelper.CreateABooking(restClient);
        }

        [TestMethod]
        public async Task CreateBooking()
        {
            // Arrange
            var getRestRequest = new RestRequest(Endpoints.GetBookingById(booking.BookingID));

            // Act
            var getRestResponse = await restClient.ExecuteGetAsync<Booking>(getRestRequest);
            cleanUpList.Add(booking);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, getRestResponse.StatusCode, "Status code is not equal to 200!");
            Assert.AreEqual(getRestResponse.Data.Firstname, booking.Booking.Firstname, "Firstname mismatched!");
            Assert.AreEqual(getRestResponse.Data.Lastname, booking.Booking.Lastname, "Lastname mismatched!");
            Assert.AreEqual(getRestResponse.Data.Totalprice, booking.Booking.Totalprice, "Totalprice mismatched!");
            Assert.AreEqual(getRestResponse.Data.Depositpaid, booking.Booking.Depositpaid, "Depositpaid mismatched!");
            Assert.AreEqual(getRestResponse.Data.Bookingdates.Checkin, booking.Booking.Bookingdates.Checkin, "Checkin Data mismatched!");
            Assert.AreEqual(getRestResponse.Data.Bookingdates.Checkout, booking.Booking.Bookingdates.Checkout, "Checkout Data mismatched!");
            Assert.AreEqual(getRestResponse.Data.Additionalneeds, booking.Booking.Additionalneeds, "Additional Needs mismatched!");

        }

        [TestMethod]
        public async Task UpdateBookingByID()
        {
            // Arrange
            var getRestRequest = new RestRequest(Endpoints.GetBookingById(booking.BookingID));
            var getRestResponse = await restClient.ExecuteGetAsync<Booking>(getRestRequest);
            var updatedBooking = BookingData.booking(true);
            cleanUpList.Add(booking);

            // Act
            var putRestResponse = await BookingHelper.UpdateBooking(restClient, updatedBooking, booking.BookingID);
            getRestRequest = new RestRequest(Endpoints.GetBookingById(booking.BookingID));
            var getUpdatedResponse = await restClient.ExecuteGetAsync<Booking>(getRestRequest);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, putRestResponse.StatusCode, "Status code is not equal to 200!");
            Assert.AreEqual(updatedBooking.Firstname, getUpdatedResponse.Data.Firstname, "Firstname mismatched!");
            Assert.AreEqual(updatedBooking.Lastname, getUpdatedResponse.Data.Lastname, "Lastname mismatched!");
            Assert.AreEqual(updatedBooking.Totalprice, getUpdatedResponse.Data.Totalprice, "Totalprice mismatched!");
            Assert.AreEqual(updatedBooking.Depositpaid, getUpdatedResponse.Data.Depositpaid, "Depositpaid mismatched!");
            Assert.AreEqual(updatedBooking.Bookingdates.Checkin, getUpdatedResponse.Data.Bookingdates.Checkin, "Checkin Data mismatched!");
            Assert.AreEqual(updatedBooking.Bookingdates.Checkout, getUpdatedResponse.Data.Bookingdates.Checkout, "Checkout Data mismatched!");
            Assert.AreEqual(updatedBooking.Additionalneeds, getUpdatedResponse.Data.Additionalneeds, "Additional Needs mismatched!");

        }

        [TestMethod]
        public async Task DeleteBookingByID()
        {
            // Arrange
            var bookingID = booking.BookingID;

            // Act
            var deleteRestResponse = await BookingHelper.DeleteBookingByID(restClient, booking.BookingID);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, deleteRestResponse.StatusCode, "Status code is not equal to 200!");
        }

        [TestMethod]
        public async Task ValidateBookingByID()
        {
            // Arrange
            int invalidID = -1;

            // Act
            var getRequest = new RestRequest(Endpoints.GetBookingById(invalidID));
            var getResponse = await restClient.ExecuteGetAsync<Booking>(getRequest);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode, "Status code is not equal to 200!");

        }
    }
}
