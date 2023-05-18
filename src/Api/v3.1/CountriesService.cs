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

        internal object GetCountryByName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null!;
            }
            
            value = value.Replace(' ', '-');
            var country = _allCountries
                .FirstOrDefault(x => string.Equals(x.name.common.Replace(' ', '-'), value, StringComparison.InvariantCultureIgnoreCase)
                    || string.Equals(x.name.official.Replace(' ', '-'), value, StringComparison.InvariantCultureIgnoreCase));
            
            if (country == null)
            {
                country = _allCountries
                    .FirstOrDefault(x => x.name.nativeName.Any(n => string.Equals(n.Value.common.Replace(' ', '-'), value, StringComparison.InvariantCultureIgnoreCase)));
            }
            
            return country;
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
