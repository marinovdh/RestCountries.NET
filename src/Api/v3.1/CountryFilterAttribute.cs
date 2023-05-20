using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Capella.RestCountries.Api.V31
{
    public class CountryFilterAttribute : ActionFilterAttribute
    {
        private enum FilterMode
        {
            Include,
            Exclude
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var fieldsFilter = filterContext.HttpContext.Request.Query["fields"].ToString();
            if (!string.IsNullOrEmpty(fieldsFilter))
            {
                var fields = fieldsFilter
                    .Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                FilterFields(filterContext, FilterMode.Include, fields);
            }
            else
            {
                FilterFields(filterContext, FilterMode.Exclude, new List<string>
                {
                    nameof(Country.common),
                    nameof(Country.official)
                });
            }

            base.OnActionExecuted(filterContext);
        }

        private void FilterFields(ActionExecutedContext filterContext, FilterMode filterMode, List<string> filteredFields)
        {
            if (filterContext.Result is JsonResult jsonResult1 && jsonResult1.Value is IEnumerable<Country> countries)
            {
                FilterCollection(filterMode, filteredFields, countries);
                return;
            }

            if (filterContext.Result is JsonResult jsonResult2 && jsonResult2.Value is Country country)
            {
                FilterCountry(filterMode, filteredFields, country);
                return;
            }
        }

        private void FilterCollection(FilterMode filterMode, List<string> fields, IEnumerable<Country> countries)
        {
            foreach (var country in countries)
            {
                FilterCountry(filterMode, fields, country);
            }
        }

        private void FilterCountry(FilterMode filterMode, List<string> fields, Country country)
        {
            country.common = FilterProperty(filterMode, country.common, nameof(country.common), fields)!;
            country.official = FilterProperty(filterMode, country.official, nameof(country.official), fields)!;

            country.name = FilterProperty(filterMode, country.name, nameof(country.name), fields)!;
            country.tld = FilterProperty(filterMode, country.tld, nameof(country.tld), fields)!;
            country.cca2 = FilterProperty(filterMode, country.cca2, nameof(country.cca2), fields)!;
            country.cca3 = FilterProperty(filterMode, country.cca3, nameof(country.cca3), fields)!;
            country.ccn3 = FilterProperty(filterMode, country.ccn3, nameof(country.ccn3), fields)!;
            country.cioc = FilterProperty(filterMode, country.cioc, nameof(country.cioc), fields)!;
            country.independent = FilterProperty(filterMode, country.independent, nameof(country.independent), fields)!;
            country.status = FilterProperty(filterMode, country.status, nameof(country.status), fields)!;
            country.unMember = FilterProperty(filterMode, country.unMember, nameof(country.unMember), fields)!;
            country.currencies = FilterProperty(filterMode, country.currencies, nameof(country.currencies), fields)!;
            country.idd = FilterProperty(filterMode, country.idd, nameof(country.idd), fields)!;
            country.capital = FilterProperty(filterMode, country.capital, nameof(country.capital), fields)!;
            country.altSpellings = FilterProperty(filterMode, country.altSpellings, nameof(country.altSpellings), fields)!;
            country.region = FilterProperty(filterMode, country.region, nameof(country.region), fields)!;
            country.subregion = FilterProperty(filterMode, country.subregion, nameof(country.subregion), fields)!;
            country.languages = FilterProperty(filterMode, country.languages, nameof(country.languages), fields)!;
            country.translations = FilterProperty(filterMode, country.translations, nameof(country.translations), fields)!;
            country.latlng = FilterProperty(filterMode, country.latlng, nameof(country.latlng), fields)!;
            country.landlocked = FilterProperty(filterMode, country.landlocked, nameof(country.landlocked), fields)!;
            country.borders = FilterProperty(filterMode, country.borders, nameof(country.borders), fields)!;
            country.area = FilterProperty(filterMode, country.area, nameof(country.area), fields)!;
            country.demonyms = FilterProperty(filterMode, country.demonyms, nameof(country.demonyms), fields)!;
            country.flag = FilterProperty(filterMode, country.flag, nameof(country.flag), fields)!;
            country.maps = FilterProperty(filterMode, country.maps, nameof(country.maps), fields)!;
            country.population = FilterProperty(filterMode, country.population, nameof(country.population), fields)!;
            country.gini = FilterProperty(filterMode, country.gini, nameof(country.gini), fields)!;
            country.fifa = FilterProperty(filterMode, country.fifa, nameof(country.fifa), fields)!;
            country.car = FilterProperty(filterMode, country.car, nameof(country.car), fields)!;
            country.timezones = FilterProperty(filterMode, country.timezones, nameof(country.timezones), fields)!;
            country.continents = FilterProperty(filterMode, country.continents, nameof(country.continents), fields)!;
            country.flags = FilterProperty(filterMode, country.flags, nameof(country.flags), fields)!;
            country.coatOfArms = FilterProperty(filterMode, country.coatOfArms, nameof(country.coatOfArms), fields)!;
            country.startOfWeek = FilterProperty(filterMode, country.startOfWeek, nameof(country.startOfWeek), fields)!;
            country.capitalInfo = FilterProperty(filterMode, country.capitalInfo, nameof(country.capitalInfo), fields)!;
            country.postalCode = FilterProperty(filterMode, country.postalCode, nameof(country.postalCode), fields)!;
        }

        private T? FilterProperty<T>(FilterMode filterMode, T? propValue, string propName, List<string> filteredFields)
        {
            if (filterMode == FilterMode.Include)
            {
                return filteredFields.Any(f => f.Contains(propName, StringComparison.InvariantCultureIgnoreCase))
                    ? propValue
                    : default;
            }

            return filteredFields.Any(f => f.Contains(propName, StringComparison.InvariantCultureIgnoreCase))
                ? default
                : propValue;

        }
    }
}
