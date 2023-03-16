using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RESTFULAPI_Automation_Session23.DataModels;

namespace RESTFULAPI_Automation_Session23.Tests
{
    public class ApiBaseTest
    {
        public RestClient RestClient { get; set; }

        public PetModel PetDetails { get; set; }

        [TestInitialize]
        public void Initilize()
        {
            RestClient = new RestClient();
        }

        [TestCleanup]
        public void CleanUp()
        {

        }

    }
}
