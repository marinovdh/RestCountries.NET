using System.Text.Json;

namespace Capella.RestCountries.Api.V31
{
    /// <summary>
    /// Service to get country objects.
    /// </summary>
    public class CountriesService
    {
        private static List<Country> _allCountries = null!;

        /// <summary>
        /// ctor for <see cref="CountriesService"/>
        /// </summary>
        public CountriesService()
        {
            _allCountries = LoadCountries()
                .OrderBy(x => x.cca3)
                .ToList();
        }

        /// <summary>
        /// Get all available countries, ordered by the 3-letter code (CCA3).
        /// </summary>
        public List<Country> GetCountries()
        {
            return _allCountries;
        }

        /// <summary>
        /// Gets a country by name. This should be either the common or official name, or the native common 
        /// or official name. It is supposed to return only 1 country, but the return type is a list though.
        /// </summary>
        public List<Country> GetCountryByFullName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return new List<Country>();
            }

            value = value.Replace(' ', '-');
            var countries = _allCountries
                .Where(x => string.Equals(x.name.common.Replace(' ', '-'), value, StringComparison.InvariantCultureIgnoreCase)
                    || string.Equals(x.name.official.Replace(' ', '-'), value, StringComparison.InvariantCultureIgnoreCase))
                .ToList();

            if (!countries.Any())
            {
                countries = _allCountries
                    .Where(x => x.name.nativeName.Any(n =>
                    string.Equals(n.Value.common.Replace(' ', '-'), value, StringComparison.InvariantCultureIgnoreCase)
                    || string.Equals(n.Value.official.Replace(' ', '-'), value, StringComparison.InvariantCultureIgnoreCase)))
                    .ToList();
            }

            return countries;
        }

        /// <summary>
        /// Searches countries by a given part of the name. This should be either the common or official name, or the native 
        /// common or official name. It is supposed to return only 1 country, but the return type is a list though.
        /// </summary>
        public List<Country> SearchCountryByNamePart(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return new List<Country>();
            }

            value = value.Replace(' ', '-');
            var countries = _allCountries
                .Where(x => x.name.common.Replace(' ', '-').Contains(value, StringComparison.InvariantCultureIgnoreCase)
                    || x.name.official.Replace(' ', '-').Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .ToList();

            if (!countries.Any())
            {
                countries = _allCountries
                    .Where(x => x.name.nativeName.Any(n =>
                        n.Value.common.Replace(' ', '-').Contains(value, StringComparison.InvariantCultureIgnoreCase)
                        || n.Value.official.Replace(' ', '-').Contains(value, StringComparison.InvariantCultureIgnoreCase)))
                    .ToList();
            }

            return countries;
        }

        /// <summary>
        /// Gets a single country by searching for ISO 3166-1 2-letter, 3-letter (CCA3), 3-digit (CCN3) or Olympic Comitee (CIOC) country code.
        /// </summary>
        /// <param name="value">The ISO 3166-1 2-letter or 3-letter country code.</param>
        /// <returns>The country object, or 404 status with error message if not found.</returns>
        public Country? GetCountryByAlphaCode(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null!;
            }

            if (value.Length == 2)
            {
                // Search by CCA2
                return _allCountries.SingleOrDefault(x => string.Equals(x.cca2, value, StringComparison.InvariantCultureIgnoreCase));
            }

            if (value.Length == 3)
            {
                if (int.TryParse(value, out _))
                {
                    // Search by CCN3
                    return _allCountries.SingleOrDefault(x => string.Equals(x.ccn3, value, StringComparison.Ordinal));
                }

                // Search by CCA3
                return _allCountries.SingleOrDefault(x => string.Equals(x.cca3, value, StringComparison.InvariantCultureIgnoreCase))
                    // Search by CIOC
                    ?? _allCountries.SingleOrDefault(x => string.Equals(x.cioc, value, StringComparison.InvariantCultureIgnoreCase));
            }

            return null;
        }

        /// <summary>
        /// Search by list of ISO 3166-1 2-letter (CCA2), 3-letter (CCA3), 3-digit (CCN3) or Olympic Comitee (CIOC) country code.
        /// </summary>
        /// <param name="codes">The country codes, separated by semi-colon or comma.</param>
        /// <returns>The country objects that were found found, or an empty list.</returns>
        public List<Country> GetCountriesByAlphaCodeList(string codes)
        {
            if (codes == null)
            {
                return new List<Country>();
            }

            var codeList = codes.ToUpperInvariant()
                .Replace(" ", "")
                .Split(new char[] { ';', ',' })
                .Where(x => x.Length == 2 || x.Length == 3)
                .ToList();

            return _allCountries
                .Where(x => codeList.Contains(x.cca2.ToUpperInvariant())
                            || codeList.Contains(x.cca3.ToUpperInvariant())
                            || codeList.Contains(x.ccn3)
                            || codeList.Contains(x.cioc.ToUpperInvariant()))
                .ToList();
        }

        /// <summary>
        /// Generic search the country list.
        /// </summary>
        /// <param name="expr">A generic expression to search the list of countries with.</param>
        /// <returns>The country objects that were found found, or an empty list.</returns>
        public List<Country> GetCountriesByExpression(Func<Country, bool> expr)
        {
            return _allCountries.Where(expr).ToList();
        }

        private List<Country> LoadCountries()
        {
            string fileName = "App_Data/countriesV3.1.json";
            string jsonString = File.ReadAllText(fileName);

            var countries = JsonSerializer.Deserialize<List<Country>>(jsonString);

            foreach (var country in countries)
            {
                country.common = country.name.common;
                country.official = country.name.official;
            }

            return countries!;
        }
    }
}
