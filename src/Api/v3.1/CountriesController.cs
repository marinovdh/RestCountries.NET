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

        [HttpGet]
        [Route("all")]
        public IEnumerable<Country> GetAllCountries()
        {
            var countries = countriesService.GetCountries();
            return countries;
        }

        [HttpGet]
        [Route("name/{value}")]
        public ActionResult GetCountryByName(string value)
        {
            var country = countriesService.GetCountryByName(value);
            if (country != null)
            {
                return new JsonResult(country);
            }

            return CountryNotFound;
        }
    }
}
