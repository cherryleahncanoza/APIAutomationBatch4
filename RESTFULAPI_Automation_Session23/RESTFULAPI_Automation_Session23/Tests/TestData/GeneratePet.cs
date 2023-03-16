using RESTFULAPI_Automation_Session23.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTFULAPI_Automation_Session23.Tests.TestData
{
    public class GeneratePet
    {
        public static PetModel pet()
        {
            return new PetModel
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
                Status = "available"

            };
        }
    }
}
