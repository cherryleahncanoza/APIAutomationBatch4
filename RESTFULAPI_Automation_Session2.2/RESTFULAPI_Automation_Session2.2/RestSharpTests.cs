using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]

namespace RESTFULAPI_Automation_Session22
{
    [TestClass]
    public class RestSharpTests
    {

        // Initialize static data

        private static RestClient restClient;
        private static readonly string BaseURL = "https://petstore.swagger.io/v2";
        private static readonly string PetEndpoint = "/pet";
        private static string GetURL(string endpoint) => $"{BaseURL}{endpoint}";
        private static Uri GetURI(string endpoint) => new Uri(GetURL(endpoint));
        private readonly List<PetModel> cleanUpList = new List<PetModel>();

        [TestInitialize]

        public async Task TestInitialize()
        {
            restClient = new RestClient();  
        }

        [TestCleanup]

        public async Task TestCleanup() 
        {
            foreach (var petData in cleanUpList)
            {
                var restRequest = new RestRequest(GetURI($"{PetEndpoint}/{petData.Id}"));
                var restResponse = await restClient.DeleteAsync(restRequest);
            }
        }


        [TestMethod]
        public async Task GetMethodExecuteAsync()
        {
            #region Create Pet

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

            // Send Post Request

            var temp = GetURI(PetEndpoint);
            var postRestRequest = new RestRequest(GetURI(PetEndpoint)).AddJsonBody(petData);
            var postRestResponse = await restClient.ExecutePostAsync(postRestRequest);
            
            #endregion

            #region Get Pet

            var restRequest = new RestRequest(GetURI($"{PetEndpoint}/{petData.Id}"), Method.Get);
            var restResponse = await restClient.ExecuteAsync<PetModel>(restRequest);

            #endregion

            #region Assertions

            Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode, "Status code is not equal to 200");
            Assert.AreEqual(petData.Id, restResponse.Data.Id, "Id did not match.");
            Assert.AreEqual(petData.Category.Id, restResponse.Data.Category.Id, "Category did not match.");
            Assert.AreEqual(petData.Category.Name, restResponse.Data.Category.Name, "Category did not match.");
            Assert.AreEqual(petData.Name, restResponse.Data.Name, "Name did not match.");
            CollectionAssert.AreEqual(petData.PhotoUrls, restResponse.Data.PhotoUrls, "PhotoURLs did not match.");
            //CollectionAssert.AreEqual(petData.Tags, restResponse.Data.Tags, "Tags did not match.");
            Assert.AreEqual(petData.Status, restResponse.Data.Status, "Status did not match.");

            #endregion

            #region Clean Up

            cleanUpList.Add(petData);

            #endregion
        }

        [TestMethod]
        public async Task GetMethodExecuteGetAsync()
        {
            #region Create Pet

            // Create Pet Data Object
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

            // Send Post Request
            var postRestRequest = new RestRequest(GetURI(PetEndpoint)).AddJsonBody(petData);
            var postRestResponse = await restClient.ExecutePostAsync(postRestRequest);
            #endregion

            #region Get Pet
            
            // Send Get Request
            var restRequest = new RestRequest(GetURI($"{PetEndpoint}/{petData.Id}"));
            var restResponse = await restClient.ExecuteGetAsync<PetModel>(restRequest);

            #endregion

            #region Assertions
            Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode, "Status code is not equal to 200");
            Assert.AreEqual(petData.Id, restResponse.Data.Id, "Id did not match.");
            Assert.AreEqual(petData.Category.Id, restResponse.Data.Category.Id, "Category did not match.");
            Assert.AreEqual(petData.Category.Name, restResponse.Data.Category.Name, "Category did not match.");
            Assert.AreEqual(petData.Name, restResponse.Data.Name, "Name did not match.");
            CollectionAssert.AreEqual(petData.PhotoUrls, restResponse.Data.PhotoUrls, "PhotoURLs did not match.");
            //CollectionAssert.AreEqual(petData.Tags, restResponse.Data.Tags, "Tags did not match.");
            Assert.AreEqual(petData.Status, restResponse.Data.Status, "Status did not match.");
            #endregion

            #region Clean Up
            cleanUpList.Add(petData);
            #endregion
        }

        [TestMethod]
        public async Task GetMethodGetAsync()
        {
            #region Create Pet

            // Create Pet Object Data
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

            // Send Post Request
            var postRestRequest = new RestRequest(GetURI(PetEndpoint)).AddJsonBody(petData);
            var postRestResponse = await restClient.ExecutePostAsync(postRestRequest);

            #endregion

            #region Get Pet

            //Send Get Request
            var restRequest = new RestRequest(GetURI($"{PetEndpoint}/{petData.Id}"));
            var restResponse = await restClient.GetAsync<PetModel>(restRequest);

            #endregion

            #region Assertions
            //Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode, "Status code is not equal to 200");
            Assert.AreEqual(petData.Id, restResponse.Id, "Id did not match.");
            Assert.AreEqual(petData.Category.Id, restResponse.Category.Id, "Category did not match.");
            Assert.AreEqual(petData.Category.Name, restResponse.Category.Name, "Category did not match.");
            Assert.AreEqual(petData.Name, restResponse.Name, "Name did not match.");
            CollectionAssert.AreEqual(petData.PhotoUrls, restResponse.PhotoUrls, "PhotoURLs did not match.");
            //CollectionAssert.AreEqual(petData.Tags, restResponse.Data.Tags, "Tags did not match.");
            Assert.AreEqual(petData.Status, restResponse.Status, "Status did not match.");
            #endregion

            #region CleanUp
            cleanUpList.Add(petData);
            #endregion
        }

        [TestMethod]
        public async Task PostMethod()
        {
            #region Create Pet
            
            // Create Json Pet Data
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

            // Send Post Request
            var postRestRequest = new RestRequest(GetURI(PetEndpoint)).AddJsonBody(petData);
            var postRestResponse = await restClient.ExecutePostAsync(postRestRequest);

            // Assert POST request status code
            Assert.AreEqual(HttpStatusCode.OK, postRestResponse.StatusCode, "Status code is not equal to 200");

            #endregion

            #region Get Pet Data

            // Get Pet Data
            var restRequest = new RestRequest(GetURI($"{PetEndpoint}/{petData.Id}"), Method.Get);
            var restResponse = await restClient.ExecuteAsync<PetModel>(restRequest);
            #endregion

            #region Assertions

            // Assert Get response and Pet Details
            Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode, "Status code is not equal to 200");
            Assert.AreEqual(petData.Id, restResponse.Data.Id, "Id did not match.");
            Assert.AreEqual(petData.Category.Id, restResponse.Data.Category.Id, "Category did not match.");
            Assert.AreEqual(petData.Category.Name, restResponse.Data.Category.Name, "Category did not match.");
            Assert.AreEqual(petData.Name, restResponse.Data.Name, "Name did not match.");
            CollectionAssert.AreEqual(petData.PhotoUrls, restResponse.Data.PhotoUrls, "PhotoURLs did not match.");
            Assert.AreEqual(petData.Tags[0].Id, restResponse.Data.Tags[0].Id, "Tags did not match.");
            Assert.AreEqual(petData.Tags[0].Name, restResponse.Data.Tags[0].Name, "Tags did not match.");
            Assert.AreEqual(petData.Status, restResponse.Data.Status, "Status did not match.");
            CollectionAssert.AllItemsAreInstancesOfType(petData.Tags, typeof(Category));
            //CollectionAssert.AreEqual(petData.Tags.ToList(), restResponse.Data.Tags.ToList());

            #endregion

            #region Clean Up
            cleanUpList.Add(petData);
            #endregion
        }

        [TestMethod]
        public async Task PutMethod()
        {
            #region Create Pet Data

            // Create Json Pet Object
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

            // Send Post Request
            var postRestRequest = new RestRequest(GetURI(PetEndpoint)).AddJsonBody(petData);
            var postRestResponse = await restClient.ExecutePostAsync(postRestRequest);

            #endregion

            #region Get Pet

            // Send Get Request
            var restRequest = new RestRequest(GetURI($"{PetEndpoint}/{petData.Id}"), Method.Get);
            var restResponse = await restClient.ExecuteAsync<PetModel>(restRequest);

            #endregion

            #region Assertions
            Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode, "Status code is not equal to 200");
            Assert.AreEqual(petData.Id, restResponse.Data.Id, "Id did not match.");
            Assert.AreEqual(petData.Category.Id, restResponse.Data.Category.Id, "Category did not match.");
            Assert.AreEqual(petData.Category.Name, restResponse.Data.Category.Name, "Category did not match.");
            Assert.AreEqual(petData.Name, restResponse.Data.Name, "Name did not match.");
            CollectionAssert.AreEqual(petData.PhotoUrls, restResponse.Data.PhotoUrls, "PhotoURLs did not match.");
            //CollectionAssert.AreEqual(petData.Tags, restResponse.Data.Tags, "Tags did not match.");
            Assert.AreEqual(petData.Status, restResponse.Data.Status, "Status did not match.");
            #endregion

            #region Update Pet

            // Update Pet Data
            petData.Id = restResponse.Data.Id;
            petData.Name = "Raichu";
            petData.Status = "sold";

            // Send Put request
            var restPutRequest = new RestRequest(GetURI($"{PetEndpoint}")).AddJsonBody(petData);
            var restPutResponse = await restClient.ExecutePutAsync<PetModel>(restPutRequest);

            // Assert PUT request status code
            Assert.AreEqual(HttpStatusCode.OK, restPutResponse.StatusCode, "Status code is not equal to 200");

            #endregion

            #region Get Updated Pet

            // Send Get Request
            var restRequest2 = new RestRequest(GetURI($"{PetEndpoint}/{petData.Id}"), Method.Get);
            var restResponse2 = await restClient.ExecuteAsync<PetModel>(restRequest2);

            #endregion

            #region Assertions
            Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode, "Status code is not equal to 200");
            Assert.AreEqual(petData.Id, restResponse.Data.Id, "Id did not match.");
            Assert.AreEqual(petData.Category.Id, restResponse.Data.Category.Id, "Category did not match.");
            Assert.AreEqual(petData.Category.Name, restResponse.Data.Category.Name, "Category did not match.");
            Assert.AreNotEqual(petData.Name, restResponse.Data.Name, "Name did not match.");
            CollectionAssert.AreEqual(petData.PhotoUrls, restResponse.Data.PhotoUrls, "PhotoURLs did not match.");
            //CollectionAssert.AreEqual(petData.Tags, restResponse.Data.Tags, "Tags did not match.");
            Assert.AreNotEqual(petData.Status, restResponse.Data.Status, "Status did not match.");
            #endregion

            #region Clean Up
            cleanUpList.Add(petData);
            #endregion
        }

        [TestMethod]
        public async Task DeleteMethod()
        {
            #region Create Pet
            
            // Create Pet Data
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

            // Send Post Request
            var postRestRequest = new RestRequest(GetURI(PetEndpoint)).AddJsonBody(petData);
            var postRestResponse = await restClient.ExecutePostAsync(postRestRequest);

            #endregion

            #region Delete Pet

            // Send Delete Request
            var deleteRestRequest = new RestRequest(GetURI($"{PetEndpoint}/{petData.Id}"));
            var deleteRestResponse = await restClient.DeleteAsync(deleteRestRequest);

            // Assert Delete Response
            Assert.AreEqual(HttpStatusCode.OK, deleteRestResponse.StatusCode, "Status code is not equal to 200");

            #endregion

            #region Verify if Pet exist
            var restRequest = new RestRequest(GetURI($"{PetEndpoint}/{petData.Id}"), Method.Get);
            var restResponse = await restClient.ExecuteAsync<PetModel>(restRequest);

            // Assert response
            Assert.AreEqual(HttpStatusCode.NotFound, restResponse.StatusCode, "Status code is not equal to 404. User still exist");
            #endregion

        }

    }
}