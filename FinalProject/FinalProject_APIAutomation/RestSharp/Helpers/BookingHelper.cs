using RestSharp;
using Rest_Sharp.DataModels;
using Rest_Sharp.Resources;
using Rest_Sharp.Tests.TestData;

namespace Rest_Sharp.Helpers
{
    public class BookingHelper
    {

        private static async Task<string> GetAuthorizationToken(RestClient restClient)
        {

            var restRequest = new RestRequest(Endpoints.LoginEndpoint()).AddJsonBody(TokenData.token());
            var postRestRespons = await restClient.ExecutePostAsync<TokenModel>(restRequest);

            return postRestRespons.Data.Token;
        }

        public static async Task<BookingModel>CreateABooking(RestClient restClient)
        {
            var token = await GetAuthorizationToken(restClient);
            restClient.AddDefaultHeader("Accept", "application/json");
            restClient.AddDefaultHeader("Cookie", $"token={token}");
            
            var createBookingData = BookingData.booking(false);

            var restRequest = new RestRequest(Endpoints.PostBooking()).AddJsonBody(createBookingData);
            var postRestResponse = await restClient.ExecutePostAsync<BookingModel>(restRequest);

            return postRestResponse.Data;
        }

        public static async Task<RestResponse> UpdateBooking(RestClient restClient, Booking updatedBookingData, long bookingId)
        {

            var restRequest = new RestRequest(Endpoints.PutBookingById(bookingId)).AddJsonBody(updatedBookingData);
            var putRestResponse = await restClient.ExecutePutAsync<Booking>(restRequest);

            return putRestResponse;
        }
        public static async Task<RestResponse> DeleteBookingByID(RestClient restClient, long bookingId)
        {

            var restRequest = new RestRequest(Endpoints.DeleteBookingById(bookingId));
            var deleteRestResponse = await restClient.DeleteAsync(restRequest);

            return deleteRestResponse;
        }

    }
}