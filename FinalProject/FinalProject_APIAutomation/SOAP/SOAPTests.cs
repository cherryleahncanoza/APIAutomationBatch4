using ServiceReference1;

namespace SOAP
{
    [TestClass]
    public class SOAPTests
    {
        private ServiceReference1.CountryInfoServiceSoapTypeClient countryInfoServiceSoapType;

        [TestInitialize]
        public async Task Initialize()
        {
            countryInfoServiceSoapType = new ServiceReference1.CountryInfoServiceSoapTypeClient(ServiceReference1.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);
        }

        [TestMethod]
        public void VerifyCountryNameAndIsoCode()
        {
            // Arrange
            var country = CountryNamesByCode();
            var randomCountry = RandomizeCountrySelection(country);

            // Act
            var fullCountryInfo = countryInfoServiceSoapType.FullCountryInfo(randomCountry.sISOCode);

            // Assert
            Assert.AreEqual(fullCountryInfo.sISOCode, randomCountry.sISOCode);
            Assert.AreEqual(fullCountryInfo.sName, randomCountry.sName);

        }

        [TestMethod]
        public void VerifyCountryISOCode()
        {
            // Arrange
            var country = CountryNamesByCode();
            List<tCountryCodeAndName> countryList = new List<tCountryCodeAndName>();

            for (int count = 0; count < 5; count++)
            {
                countryList.Add(RandomizeCountrySelection(country));
            }

            foreach (var countryrecord in countryList)
            {
                // Act
                var isoCode = countryInfoServiceSoapType.CountryISOCode(countryrecord.sName);

                // Assert
                Assert.AreEqual(isoCode, countryrecord.sISOCode);
            }
        }

        // Get List of Country Names by Code
        private tCountryCodeAndName[] CountryNamesByCode()
        {
            var country = countryInfoServiceSoapType.ListOfCountryNamesByCode();

            return country;
        }

        private static Random randomize = new Random();

        // Get Random Country from Country List
        private tCountryCodeAndName RandomizeCountrySelection(tCountryCodeAndName[] countryData)
        {
            int next = randomize.Next(countryData.Length);

            var country = countryData[next];

            return country;

        }




    }
}