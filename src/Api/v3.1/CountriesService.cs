using System.Text.Json;

namespace Capella.RestCountries.Api.V31
{
    public class CountriesService
    {
        private static List<Country> _allCountries = null!;

        public CountriesService()
        {
            _allCountries = LoadCountries()
                .OrderBy(x => x.cca3)
                .ToList();
        }

        public List<Country> GetCountries()
        {
            return _allCountries;
        }

        private List<Country> LoadCountries()
        {
            string fileName = "App_Data/countriesV3.1.json";
            string jsonString = File.ReadAllText(fileName);
            
            var countries = JsonSerializer.Deserialize<List<Country>>(jsonString);

            return countries!;
        }
    }
}
