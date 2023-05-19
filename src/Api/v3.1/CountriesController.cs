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
        /// Gets all countries with an extended list of properties per country. By default this list
        /// is ordered by the country's 3-letter code (CCA3).
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
        /// Gets a single country bij its common or official name, or its native name. The first match will be returned.
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

        /// <summary>
        /// Gets a single country by searching for ISO 3166-1 2-letter or 3-letter country code.
        /// </summary>
        /// <param name="code">The ISO 3166-1 2-letter or 3-letter country code.</param>
        /// <returns>The country object, or 404 status with error message if not found.</returns>
        [HttpGet]
        [Route("alpha/{code}")]
        public ActionResult GetCountryByAlphaCode(string code)
        {
            var country = countriesService.GetCountryByAlphaCode(code);
            if (country != null)
            {
                return new JsonResult(country);
            }

            return CountryNotFound;
        }

        /// <summary>
        /// Search by list of ISO 3166-1 2-letter (CCA2), 3-letter (CCA3), 3-digit CCN3 or Olympic Comitee (CIOC) country code.
        /// </summary>
        /// <param name="codes">The ISO 3166-1 2-letter or 3-letter country codes, separated by semi-colon or comma.</param>
        /// <returns>The country object, or 404 status with error message if not found.</returns>
        [HttpGet]
        [Route("alpha")]
        public ActionResult FindCountriesByAlphaCodes(string codes)
        {
            var countries = countriesService.GetCountriesByAlphaCodeList(codes);
            return new JsonResult(countries);
        }

        /// <summary>
        /// Search by capital city.
        /// </summary>
        /// <param name="capital">The (part of the) capital name to search for.</param>
        /// <returns>The found country objects, or 404 status with error message if not found.</returns>
        [HttpGet]
        [Route("capital")]
        public ActionResult FindCountriesByCapital(string capital)
        {
            var countries = countriesService.GetCountriesByExpression(c =>
                c.capital.Any(ca => ca.ToUpperInvariant().Contains(capital.ToUpperInvariant())));
            return new JsonResult(countries);
        }

        /// <summary>
        /// Search by region.
        /// </summary>
        /// <param name="region">The region name to search for.</param>
        /// <returns>The found country objects, or 404 status with error message if not found.</returns>
        [HttpGet]
        [Route("region")]
        public ActionResult FindCountriesByRegion(string region)
        {
            var countries = countriesService.GetCountriesByExpression(c =>
                string.Equals(c.region, region, StringComparison.InvariantCultureIgnoreCase));
            return new JsonResult(countries);
        }

        /// <summary>
        /// Search by subregion.
        /// </summary>
        /// <param name="subregion">The subregion name to search for.</param>
        /// <returns>The found country objects, or 404 status with error message if not found.</returns>
        [HttpGet]
        [Route("subregion")]
        public ActionResult FindCountriesBySubregion(string subregion)
        {
            var countries = countriesService.GetCountriesByExpression(c =>
                string.Equals(c.subregion, subregion, StringComparison.InvariantCultureIgnoreCase));
            return new JsonResult(countries);
        }
    }
}
