using Microsoft.AspNetCore.Mvc;

namespace Capella.RestCountries.Api.V31
{
    [ApiController]
    [Route("v3.1")]
    public class CountriesController : ControllerBase
    {
        private readonly CountriesService countriesService;
        private static readonly NotFoundObjectResult CountryNotFound = new NotFoundObjectResult(new
        {
            status = 404,
            message = "Not Found"
        });

        public CountriesController(CountriesService countriesService)
        {
            this.countriesService = countriesService;
        }

        /// <summary>
        /// Get all countries with an extended list of properties per country.
        /// </summary>
        /// <returns>List with all countries.</returns>
        [HttpGet]
        [Route("all")]
        public IEnumerable<Country> GetAllCountries()
        {
            var countries = countriesService.GetCountries();
            return countries;
        }

        /// <summary>
        /// Get a single country bij its common or official name, or its native name. The first match will be returned.
        /// </summary>
        /// <remarks>
        /// When requesting a country with blank spaces you should either use <![CDATA[%20]]> or a hyphen. Example: 
        /// <c>/name/united%20states</c> or <c>/name/united-states</c>.
        /// </remarks>
        /// <param name="countryName">The full name of the country to request.</param>
        /// <returns>The country object, or 404 status with error message if not found.</returns>
        [HttpGet]
        [Route("name/{countryName}")]
        public ActionResult GetCountryByName(string countryName)
        {
            var country = countriesService.GetCountryByName(countryName);
            if (country != null)
            {
                return new JsonResult(country);
            }

            return CountryNotFound;
        }
    }
}
