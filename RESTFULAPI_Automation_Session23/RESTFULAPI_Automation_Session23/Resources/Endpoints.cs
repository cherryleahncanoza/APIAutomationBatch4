using System;
using System.Collections.Generic;
using System.Text;

namespace RESTFULAPI_Automation_Session23.Resources
{
    /// <summary>
    /// Class containing all endpoints used in API tests
    /// </summary>
    public class Endpoints
    {
        //Base URL
        public const string baseURL = "https://petstore.swagger.io/v2";

        public static string GetPetById(long id) => $"{baseURL}/pet/{id}";

        public static string PostPet() => $"{baseURL}/pet";

        public static string PutPet() => $"{baseURL}/pet";

        public static string DeletePetById(long id) => $"{baseURL}/pet/{id}";
    }
}
