using Newtonsoft.Json;
using HTTPClient.DataModels;
using HTTPClient.Resources;
using HTTPClient.Tests;
using HTTPClient.Tests.TestData;
using System.Net;

namespace HTTPClient
{
    [TestClass]
    public class HTTPClientTests : BaseHTTPTests
    {
        private readonly List<BookingResponse> cleanUpList = new List<BookingResponse>();

        [TestCleanup]
        public async Task CleanUp()
        {
            foreach (var data in cleanUpList)
            {
                var deleteResponse = await bookingHelper.SendAsyncFunction(httpClient, HttpMethod.Delete, Endpoints.DeleteBookingById(data.BookingID), null, token);
            }
        }

        [TestMethod]
        public async Task CreateBooking()
        {
            // Arrange
            var postResponse = await bookingHelper.SendAsyncFunction(httpClient, HttpMethod.Post, Endpoints.CreateBooking(), BookingData.booking(false));
            var postData = JsonConvert.DeserializeObject<BookingResponse>(postResponse.Content.ReadAsStringAsync().Result);
            cleanUpList.Add(postData);

            // Act
            var getResponse = await bookingHelper.SendAsyncFunction(httpClient, HttpMethod.Get, Endpoints.GetBookingById(postData.BookingID));
            var bookingData = JsonConvert.DeserializeObject<BookingModel>(getResponse.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, postResponse.StatusCode, "Status Code is not equal to 200!");
            Assert.AreEqual(postData.BookingDetails.FirstName, bookingData.FirstName, "FirstName not matched!");
            Assert.AreEqual(postData.BookingDetails.LastName, bookingData.LastName, "LastName not matched!");
            Assert.AreEqual(postData.BookingDetails.TotalPrice, bookingData.TotalPrice, "TotalPrice not matched!");
            Assert.AreEqual(postData.BookingDetails.DepositPaid, bookingData.DepositPaid, "DepositPaid not matched!");
            Assert.AreEqual(postData.BookingDetails.BookingDates.CheckIn, bookingData.BookingDates.CheckIn, "CheckIn not matched!");
            Assert.AreEqual(postData.BookingDetails.BookingDates.CheckOut, bookingData.BookingDates.CheckOut, "CheckOut not matched!");
            Assert.AreEqual(postData.BookingDetails.AdditionalNeeds, bookingData.AdditionalNeeds, "AdditionalNeeds not matched!");
        }

        [TestMethod]
        public async Task UpdateBookingByName()
        {
            // Arrange
            var httpResponse = await bookingHelper.SendAsyncFunction(httpClient, HttpMethod.Post, Endpoints.CreateBooking(), BookingData.booking(false));
            var postData = JsonConvert.DeserializeObject<BookingResponse>(httpResponse.Content.ReadAsStringAsync().Result);
            var updatedBooking = BookingData.booking(true);

            var updateResponse = await bookingHelper.SendAsyncFunction(httpClient, HttpMethod.Put, Endpoints.UpdateBookingById(postData.BookingID), updatedBooking, token);
            var updatedData = JsonConvert.DeserializeObject<BookingModel>(updateResponse.Content.ReadAsStringAsync().Result);

            // Act
            var getResponse = await bookingHelper.SendAsyncFunction(httpClient, HttpMethod.Get, Endpoints.GetBookingById(postData.BookingID));
            var getData = JsonConvert.DeserializeObject<BookingModel>(getResponse.Content.ReadAsStringAsync().Result);
            cleanUpList.Add(postData);

            // Assert
            Assert.AreEqual(updateResponse.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(updatedData.FirstName, getData.FirstName, "FirstName not matched!");
            Assert.AreEqual(updatedData.LastName, getData.LastName, "LastName not matched!");
            Assert.AreEqual(updatedData.TotalPrice, getData.TotalPrice, "TotalPrice not matched!");
            Assert.AreEqual(updatedData.DepositPaid, getData.DepositPaid, "DepositPaid not matched!");
            Assert.AreEqual(updatedData.BookingDates.CheckIn, getData.BookingDates.CheckIn, "CheckIn not matched!");
            Assert.AreEqual(updatedData.BookingDates.CheckOut, getData.BookingDates.CheckOut, "CheckOut not matched!");
            Assert.AreEqual(updatedData.AdditionalNeeds, getData.AdditionalNeeds, "AdditionalNeeds not matched!");
        }

        [TestMethod]
        public async Task DeleteBookingByID()
        {
            // Arrange
            var httpResponse = await bookingHelper.SendAsyncFunction(httpClient, HttpMethod.Post, Endpoints.CreateBooking(), BookingData.booking(false));
            var postData = JsonConvert.DeserializeObject<BookingResponse>(httpResponse.Content.ReadAsStringAsync().Result);

            // Act
            var deleteResponse = await bookingHelper.SendAsyncFunction(httpClient, HttpMethod.Delete, Endpoints.DeleteBookingById(postData.BookingID), null, token);

            // Assert
            Assert.AreEqual(deleteResponse.StatusCode, HttpStatusCode.Created);

        }

        [TestMethod]
        public async Task GetBooking()
        {
            // Arrange
            int invalidId = -1;

            // Act
            var getResponse = await bookingHelper.SendAsyncFunction(httpClient, HttpMethod.Get, Endpoints.GetBookingById(invalidId));

            // Assert
            Assert.AreEqual(getResponse.StatusCode, HttpStatusCode.NotFound);
        }

    }
}