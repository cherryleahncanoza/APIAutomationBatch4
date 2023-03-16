using RestSharp;
using RESTFULAPI_Automation_Session23.DataModels;
using RESTFULAPI_Automation_Session23.Resources;
using RESTFULAPI_Automation_Session23.Tests.TestData;
using System.Threading.Tasks;
using System.Collections.Generic;
using RESTFULAPI_Automation_Session23.Resources;

namespace RESTFULAPI_Automation_Session23.Helpers
{
    /// <summary>
    /// Class containing all methods for pets
    /// </summary>
    public class PetHelper
    {
        /// <summary>
        /// Send POST request to add new pet
        /// </summary>
        ///

        public static async Task<PetModel> AddNewPet(RestClient client)
        {
            var newPetData = GeneratePet.pet();
            var postRequest = new RestRequest(Endpoints.PostPet());

            //Send Post Request to add new pet
            postRequest.AddJsonBody(newPetData);
            var postResponse = await client.ExecutePostAsync<PetModel>(postRequest);

            var createdPetData = newPetData;
            return createdPetData;
        }

        public static async Task<PetModel> UpdatePetName(RestClient client, string petName = "")
        {
            var newPetData = GeneratePet.pet();
            newPetData.Name = petName;
            var postRequest = new RestRequest(Endpoints.PostPet());

            //Send Post Request to add new pet
            postRequest.AddJsonBody(newPetData);
            var postResponse = await client.ExecutePostAsync<PetModel>(postRequest);

            var createdPetData = newPetData;
            return createdPetData;
        }
    }
}
