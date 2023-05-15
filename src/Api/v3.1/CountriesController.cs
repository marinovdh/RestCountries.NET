using Microsoft.AspNetCore.Mvc;

namespace Capella.RestCountries.Api.V31
{
    [ApiController]
    [Route("v3.1")]
    public class CountriesController : ControllerBase
    {
        private readonly CountriesService countriesService;

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
    }
}
