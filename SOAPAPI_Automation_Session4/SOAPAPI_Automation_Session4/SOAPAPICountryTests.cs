using Newtonsoft.Json.Linq;
using ServiceReference1;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace SOAPAPI_Automation_Session4
{
    [TestClass]
    public class SOAPAPICountryTests
    {
        // Initialize 
        private readonly CountryInfoServiceSoapTypeClient countriesAPITest = new CountryInfoServiceSoapTypeClient(CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

        [TestMethod]
        public void CountryCodeAscending()
        {

            #region Arrange variables

            // Validate ListOfCountryNamesByCode() is in ascending order by Country Code
            var listOfCountryNamesByCode = countriesAPITest.ListOfCountryNamesByCode();

            #endregion

            #region Assertions

            // Assertions
            Assert.IsTrue(Enumerable.SequenceEqual(listOfCountryNamesByCode, listOfCountryNamesByCode.OrderBy(cc => cc.sISOCode)), "Country Codes not in ascending order!");

            #endregion

        }


        [TestMethod]
        public void ValidCountryCode()
        {

            #region Arrange variables

            // validate passing of invalid Country Code 
            var validCountry = countriesAPITest.CountryName("US");
            var validCountryName = "United States";
            var invalidCountry = countriesAPITest.CountryName("TEST");

            #endregion

            #region Assertions

            // Assertions

            Assert.AreEqual(validCountry, validCountryName, "Country Code mismatched");
            Assert.AreEqual(invalidCountry, "Country not found in the database", "Validation message mismatched!");

            #endregion

        }

        [TestMethod]
        public void VerifyLastEntryCountryCode()
        {

            #region Arrange variables

            // get the last entry from ‘ListOfCountryNamesByCode()’ 
            var lastCountryNameByCode = countriesAPITest.ListOfCountryNamesByCode().Last();

            // pass the return value Country Code to ‘CountryName()’ API
            var countryName = countriesAPITest.CountryName(lastCountryNameByCode.sISOCode);

            // pass the return value Country Name
            var countryCodeByCountryName = countriesAPITest.CountryISOCode(countryName);
            var countryNameByISOCode = countriesAPITest.CountryName(countryCodeByCountryName);


            #endregion

            #region Assertions

            // Assertions

            Assert.AreEqual(countryName, countryNameByISOCode, "Country mismatched");

            #endregion



        }
    }
}