using Rest_Sharp.DataModels;
using Rest_Sharp.Resources;

namespace Rest_Sharp.Tests.TestData
{
    public class TokenData
    {
        public static LoginModel token()
        {
            var tokenData = new LoginModel
            {
                Username = Constants.AuthorizationUsername,
                Password = Constants.AuthorizationPassword
        };

            return tokenData;
        }
    }
}
