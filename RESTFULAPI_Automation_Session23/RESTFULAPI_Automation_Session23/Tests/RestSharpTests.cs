using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTFULAPI_Automation_Session23.Helpers;
using RESTFULAPI_Automation_Session23.Resources;
using RESTFULAPI_Automation_Session23.DataModels;
using RestSharp;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using RESTFULAPI_Automation_Session23.Tests;
using RESTFULAPI_Automation_Session23;

namespace RESTFULAPI_Automation_Session23.Tests
{
    [TestClass]
    public class RestSharpTests : ApiBaseTest
    {
        private static List<PetModel> petCleanUpList = new List<PetModel>();

        [TestInitialize]
        public async Task TestInitialize()
        {
            PetDetails = await PetHelper.AddNewPet(RestClient);
        }

        [TestMethod]
        public async Task TestGetPet()
        {
            //Arrange
            var testGetRequest = new RestRequest(Endpoints.GetPetById(PetDetails.Id));
            petCleanUpList.Add(PetDetails);

            //Act
            var testResponse = await RestClient.ExecuteGetAsync<PetModel>(testGetRequest);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, testResponse.StatusCode, "Failed due to wrong status code.");
            Assert.AreEqual(PetDetails.Id, testResponse.Data.Id, "Id did not match.");
            Assert.AreEqual(PetDetails.Category.Id, testResponse.Data.Category.Id, "Category did not match.");
            Assert.AreEqual(PetDetails.Category.Name, testResponse.Data.Category.Name, "Category did not match.");
            Assert.AreEqual(PetDetails.Name, testResponse.Data.Name, "Name did not match.");
            CollectionAssert.AreEqual(PetDetails.PhotoUrls, testResponse.Data.PhotoUrls, "PhotoURLs did not match.");
            Assert.AreEqual(PetDetails.Tags[0].Id, testResponse.Data.Tags[0].Id, "Tags did not match.");
            Assert.AreEqual(PetDetails.Tags[0].Name, testResponse.Data.Tags[0].Name, "Tags did not match.");
            Assert.AreEqual(PetDetails.Status, testResponse.Data.Status, "Status did not match.");
            CollectionAssert.AllItemsAreInstancesOfType(PetDetails.Tags, typeof(Category));
        }

        [TestMethod]
        public async Task TestPutPet()
        {
            // Arrange
            var updatedPetDetails = await PetHelper.UpdatePetName(RestClient, "raichu");
            var testGetRequest = new RestRequest(Endpoints.PutPet()).AddJsonBody(updatedPetDetails);
            petCleanUpList.Add(updatedPetDetails);
            
            // Act
            var testResponse = await RestClient.ExecutePutAsync<PetModel>(testGetRequest);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, testResponse.StatusCode, "Failed due to wrong status code.");
            Assert.AreEqual(PetDetails.Id, testResponse.Data.Id, "Id did not match.");
            Assert.AreEqual(PetDetails.Category.Id, testResponse.Data.Category.Id, "Category did not match.");
            Assert.AreEqual(PetDetails.Category.Name, testResponse.Data.Category.Name, "Category did not match.");
            Assert.AreNotEqual(PetDetails.Name, testResponse.Data.Name, "Name did not match.");
            CollectionAssert.AreEqual(PetDetails.PhotoUrls, testResponse.Data.PhotoUrls, "PhotoURLs did not match.");
            Assert.AreEqual(PetDetails.Tags[0].Id, testResponse.Data.Tags[0].Id, "Tags did not match.");
            Assert.AreEqual(PetDetails.Tags[0].Name, testResponse.Data.Tags[0].Name, "Tags did not match.");
            Assert.AreEqual(PetDetails.Status, testResponse.Data.Status, "Status did not match.");
            CollectionAssert.AllItemsAreInstancesOfType(PetDetails.Tags, typeof(Category));
        }

        [TestMethod]
        public async Task TestDeletePet()
        {
            //Arrange
            var testDeleteRequest = new RestRequest(Endpoints.DeletePetById(PetDetails.Id));
            var testGetRequest = new RestRequest(Endpoints.GetPetById(PetDetails.Id));
            petCleanUpList.Add(PetDetails);

            //Act
            var testResponse = await RestClient.DeleteAsync(testDeleteRequest);
            var testVerifyResponse = await RestClient.ExecuteGetAsync<PetModel>(testGetRequest);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, testResponse.StatusCode, "Failed due to wrong status code.");
            Assert.AreEqual(HttpStatusCode.NotFound, testVerifyResponse.StatusCode, "Failed due to wrong status code.");

        }


        [TestCleanup]
        public async Task TestCleanUp()
        {
            foreach (var data in petCleanUpList)
            {
                var deletePetRequest = new RestRequest(Endpoints.GetPetById(data.Id));
                var deletePetResponse = await RestClient.DeleteAsync(deletePetRequest);
            }
        }
    }
}
