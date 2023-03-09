using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.Collections;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using System;

namespace RESTFULAPI_Automation
{
    [TestClass]
    public class HttpClientTest
    {
        // Define static values
        private static HttpClient httpClient;
        private static readonly string BaseURL = "https://petstore.swagger.io/v2";
        private static readonly string PetsEndpoint = "/pet";
        private static string GetURL(string endpoint) => $"{BaseURL}{endpoint}";
        private static Uri GetURI(string endpoint) => new Uri(GetURL(endpoint));
        private readonly List<PetModel> cleanUpList = new List<PetModel>();

        [TestInitialize]

        public void TestInitialize()
        {
            httpClient = new HttpClient();
        }

        [TestCleanup]
        public async Task TestCleanUp()
        {
            foreach (var data in cleanUpList)
            {
                var httpResponse = await httpClient.DeleteAsync(GetURL($"{PetsEndpoint}/{data.Id}"));
            }
        }

        [TestMethod]
        public async Task GetMethod()
        {
            #region create data

            // Create Json Object
            PetModel petData = new PetModel()
            {
                Id = 1,
                Category = new Category()
                {
                    Id = 1,
                    Name = "Pokemon"
                },
                Name = "pikachu",
                PhotoUrls = new string[] 
                {
                    "http://www.petURL1.com", 
                    "http://www.petURL1.com"
                },
                Tags = new Category[]
                { 
                    new Category() { Id = 1, Name = "Pokemon"}
                },
                Status = "available",
            };

            // Serialize Content
            var request = JsonConvert.SerializeObject(petData);
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send Post Request
            await httpClient.PostAsync(GetURL(PetsEndpoint), postRequest);

            #endregion

            #region get data

            // Send Request
            var httpResponse = await httpClient.GetAsync(GetURI($"{PetsEndpoint}/{petData.Id}"));

            // Get Content
            var httpResponseMessage = httpResponse.Content;

            // Get Status Code
            var statusCode = httpResponse.StatusCode;

            // Deserialize Content
            var listPetData = JsonConvert.DeserializeObject<PetModel>(httpResponseMessage.ReadAsStringAsync().Result);

            #endregion

            #region cleanupdata

            // Add data to cleanup list
            cleanUpList.Add(listPetData);

            #endregion

            #region assertion

            // Assertions
            Assert.AreEqual(HttpStatusCode.OK, statusCode, "Status Code is not equal to 200!");
            Assert.IsNotNull(listPetData);
            Assert.IsTrue(listPetData.Id == petData.Id);
            Assert.AreSame(listPetData, petData);

            #endregion
        }

        [TestMethod]
        public async Task PostMethod()
        {
            #region create data and send post request

            // Create Json Object
            PetModel petData = new PetModel()
            {
                Id = 1,
                Category = new Category()
                {
                    Id = 1,
                    Name = "Pokemon"
                },
                Name = "pikachu",
                PhotoUrls = new string[]
                {
                    "http://www.petURL1.com",
                    "http://www.petURL1.com"
                },
                Tags = new Category[]
                {
                    new Category() { Id = 1, Name = "Pokemon"}
                },
                Status = "available",
            };

            // Serialize Content
            var request = JsonConvert.SerializeObject(petData);
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send Post Request
            var httpResponse = await httpClient.PostAsync(GetURL(PetsEndpoint), postRequest);

            // Get Status Code
            var statusCode = httpResponse.StatusCode;

            #endregion

            #region get created data

            // Get Request
            var getResponse = await httpClient.GetAsync(GetURI($"{PetsEndpoint}/{petData.Id}"));

            // Deserialize Content
            var listPetData = JsonConvert.DeserializeObject<PetModel>(getResponse.Content.ReadAsStringAsync().Result);

            // Filter pet data
            var createdPetData = listPetData.Id;

            #endregion

            #region cleanupdata

            // Add data to cleanup list
            cleanUpList.Add(listPetData);

            #endregion

            #region assertion

            // Assertions

            Assert.AreEqual(HttpStatusCode.OK, statusCode, "Status Code is not equal to 201");
            Assert.IsNotNull(listPetData);
            Assert.AreEqual(listPetData.Id, createdPetData, "Id not matching");

            #endregion
        }

        [TestMethod]
        public async Task PutMethod()
        {
            #region create data and send post request
            // Create Json Object
            PetModel petData = new PetModel()
            {
                Id = 1,
                Category = new Category()
                {
                    Id = 1,
                    Name = "Pokemon"
                },
                Name = "pikachu",
                PhotoUrls = new string[]
                {
                    "http://www.petURL1.com",
                    "http://www.petURL1.com"
                },
                Tags = new Category[]
                {
                    new Category() { Id = 1, Name = "Pokemon"}
                },
                Status = "available",
            };

            // Serialize Content
            var request = JsonConvert.SerializeObject(petData);
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send Post Request
            await httpClient.PostAsync(GetURL(PetsEndpoint), postRequest);

            #endregion

            #region get Pet name  of created data

            // Get Request
            var getResponse = await httpClient.GetAsync(GetURI($"{PetsEndpoint}/{petData.Id}"));

            // Deserialize Content
            var listPetData = JsonConvert.DeserializeObject<PetModel>(getResponse.Content.ReadAsStringAsync().Result);

            var originalPetData = listPetData;
            var currentPetData = listPetData.Name;

            #endregion

            #region send put request to update data

            // Update value of user data
            petData = new PetModel()
            {
                Id = listPetData.Id,
                Category = listPetData.Category,
                Name = "raichu",
                PhotoUrls = listPetData.PhotoUrls,
                Tags = listPetData.Tags,
                Status = listPetData.Status
            };

            //Serialize Content
            request = JsonConvert.SerializeObject(petData);
            postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send Put Request
            var httpResponse = await httpClient.PutAsync(GetURL($"{PetsEndpoint}"), postRequest);

            // Get Status Code
            var statusCode = httpResponse.StatusCode;

            #endregion

            #region get updated data

            // Get Request
            getResponse = await httpClient.GetAsync(GetURI($"{PetsEndpoint}/{petData.Id}"));

            // Deserialize Content
            listPetData = JsonConvert.DeserializeObject<PetModel>(getResponse.Content.ReadAsStringAsync().Result);

            // Filter created data
            currentPetData = listPetData.Name;

            #endregion 

            #region cleanupdata
            // Add data to cleanup list
            cleanUpList.Add(listPetData);

            #endregion

            #region assertion

            // Assertions
            Assert.AreEqual(HttpStatusCode.OK, statusCode, "Status Code is not equal to 200!");
            Assert.IsNotNull(originalPetData);
            Assert.AreEqual(listPetData.Name, currentPetData, "Pet Name not matching!");
            Assert.AreNotEqual(listPetData, originalPetData, "Pet Data not updated!");

            // Negative Tests
            Assert.AreNotEqual(HttpStatusCode.OK, RandomizeNumberWithException(), "Status Code is not OK!");
            Assert.AreNotEqual(listPetData.Name, originalPetData.Name, "Pet Name not updated!");

            #endregion
        }

        [TestMethod]
        public async Task SendPostGetMethod()
        {
            #region create data

            // Create Json Object
            PetModel petData = new PetModel()
            {
                Id = 1,
                Category = new Category()
                {
                    Id = 1,
                    Name = "Pokemon"
                },
                Name = "pikachu",
                PhotoUrls = new string[]
                {
                    "http://www.petURL1.com",
                    "http://www.petURL1.com"
                },
                Tags = new Category[]
                {
                    new Category() { Id = 1, Name = "Pokemon"}
                },
                Status = "available",
            };

            #endregion

            #region send post request

            var httpResponse = await SendAsyncFunction(HttpMethod.Post, PetsEndpoint, petData);
            var statusCode = httpResponse.StatusCode;

            #endregion

            #region get created data

            // Get request
            var getResponse = await SendAsyncFunction(HttpMethod.Get, $"{PetsEndpoint}/{petData.Id}");

            // Deserialize Content
            var listPetData = JsonConvert.DeserializeObject<PetModel>(getResponse.Content.ReadAsStringAsync().Result);
            var createdPetData = petData.Name;

            #endregion

            #region cleanupdata

            // Add data to cleanup list
            cleanUpList.Add(listPetData);

            #endregion

            #region assertion
                
            // Assertion
            Assert.AreEqual(HttpStatusCode.OK, statusCode, "Status Code is not equal to 201");
            Assert.AreEqual(petData.Name, createdPetData, "Name not matching");

            #endregion
        }

        [TestMethod]
        public async Task PatchtMethod()
        {
            #region create data

            // Create Json Object
            PetModel petData = new PetModel()
            {
                Id = 1,
                Category = new Category()
                {
                    Id = 1,
                    Name = "Pokemon"
                },
                Name = "pikachu",
                PhotoUrls = new string[]
                {
                    "http://www.petURL1.com",
                    "http://www.petURL1.com"
                },
                Tags = new Category[]
                {
                    new Category() { Id = 1, Name = "Pokemon"}
                },
                Status = "available",
            };

            // Serialize Content
            var request = JsonConvert.SerializeObject(petData);
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send Post Request
            await httpClient.PostAsync(GetURL(PetsEndpoint), postRequest);

            #endregion

            #region get name of created data

            // Get Request
            var getResponse = await httpClient.GetAsync(GetURI($"{PetsEndpoint}/{petData.Id}"));

            // Deserialize Content
            var listPetData = JsonConvert.DeserializeObject<PetModel>(getResponse.Content.ReadAsStringAsync().Result);

            var createdPetData = listPetData.Name;

            #endregion

            #region send patch request to update data

            // Update value of user data
            petData = new PetModel()
            {
                Name = "raichu"
            };

            //Serialize Content
            request = JsonConvert.SerializeObject(petData);
            postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send Patch Request
            var httpResponse = await httpClient.PatchAsync(GetURL($"{PetsEndpoint}"), postRequest);

            // Get Status Code
            var statusCode = httpResponse.StatusCode;

            #endregion

            #region cleanupdata

            // Add data to cleanup list
            cleanUpList.Add(listPetData);

            #endregion

            #region assertion

            // Assertion
            Assert.AreEqual(HttpStatusCode.MethodNotAllowed, statusCode, "Status Code is not equal to 405");

            #endregion
        }

        [TestMethod]
        public async Task DeleteMethod()
        {
            #region create data
            // Create Json Object
            PetModel petData = new PetModel()
            {
                Id = 1,
                Category = new Category()
                {
                    Id = 1,
                    Name = "Pokemon"
                },
                Name = "pikachu",
                PhotoUrls = new string[]
                {
                    "http://www.petURL1.com",
                    "http://www.petURL1.com"
                },
                Tags = new Category[]
                {
                    new Category() { Id = 1, Name = "Pokemon"}
                },
                Status = "available",
            };

            // Serialize Content
            var request = JsonConvert.SerializeObject(petData);
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send Post Request
            await httpClient.PostAsync(GetURL(PetsEndpoint), postRequest);

            #endregion

            #region get ID  of created data

            // Get Request
            var getResponse = await httpClient.GetAsync(GetURI($"{PetsEndpoint}/{petData.Id}"));

            // Deserialize Content
            var listPetData = JsonConvert.DeserializeObject<PetModel>(getResponse.Content.ReadAsStringAsync().Result);

            // Filter created data
            var createdPetData = listPetData.Id;

            #endregion

            #region send delete request

            // Send Delete Request
            var httpResponse = await httpClient.DeleteAsync(GetURL($"{PetsEndpoint}/{createdPetData}"));

            // Get Status Code
            var statusCode = httpResponse.StatusCode;

            #endregion

            #region get updated data

            // Get Request
            getResponse = await httpClient.GetAsync(GetURI($"{PetsEndpoint}/{petData.Id}"));

            // Deserialize Content
            listPetData = JsonConvert.DeserializeObject<PetModel>(getResponse.Content.ReadAsStringAsync().Result);

            // Filter created data
            createdPetData = listPetData.Id;

            #endregion 

            #region assertion
            // Assertion
            Assert.AreEqual(HttpStatusCode.OK, statusCode, "Status Code is not equal to 200");

            #endregion
        }

        /// <summary>
        /// Reusable methods
        /// </summary>

        private async Task<HttpResponseMessage>SendAsyncFunction(HttpMethod method, string url, PetModel petData = null)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            httpRequestMessage.Method = method;
            httpRequestMessage.RequestUri = GetURI(url);
            httpRequestMessage.Headers.Add("Accept", "application/json");

            if(petData != null) 
            {
                // Serialize Content
                var request = JsonConvert.SerializeObject(petData); 
                httpRequestMessage.Content = new StringContent(request, Encoding.UTF8, "application/json");
            }

            var httpResponse = await httpClient.SendAsync(httpRequestMessage);

            return httpResponse;
        }

        private static Random randomize = new Random();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private int RandomizeNumberWithException()
        {
            var excludedNumber = new HashSet<int>() {200};
            var range = Enumerable.Range(1, 600).Where(i => !excludedNumber.Contains(i));

            int index = randomize.Next(0, 600 - excludedNumber.Count);
            return range.ElementAt(index);
        }

        private string RandomizedString(int length)
        {
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[randomize.Next(s.Length)]).ToArray());
        }
    }

}