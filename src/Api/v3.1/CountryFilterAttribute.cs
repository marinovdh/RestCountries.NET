using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Capella.RestCountries.Api.V31
{
    public class CountryFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var fieldsFilter = filterContext.HttpContext.Request.Query["fields"].ToString();
            if (!string.IsNullOrEmpty(fieldsFilter))
            {
                var fields = fieldsFilter
                    .Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                FilterFields(filterContext, fields);
            }

            base.OnActionExecuted(filterContext);
        }

        private void FilterFields(ActionExecutedContext filterContext, List<string> fields)
        {
            if (filterContext.Result is JsonResult jsonResult1 && jsonResult1.Value is IEnumerable<Country> countries2)
            {
                FilterCollection(fields, countries2);
                return;
            }

            if (filterContext.Result is JsonResult jsonResult2 && jsonResult2.Value is Country country)
            {
                FilterCountry(fields, country);
                return;
            }
        }

        private void FilterCollection(List<string> fields, IEnumerable<Country> countries)
        {
            foreach (var country in countries)
            {
                FilterCountry(fields, country);
            }
        }

        private void FilterCountry(List<string> fields, Country country)
        {
            country.common = FilterProperty(country.name?.common, nameof(country.name.common), fields)!;
            country.official = FilterProperty(country.name?.official, nameof(country.name.official), fields)!;

            country.name = FilterProperty(country.name, nameof(country.name), fields)!;
            country.tld = FilterProperty(country.tld, nameof(country.tld), fields)!;
            country.cca2 = FilterProperty(country.cca2, nameof(country.cca2), fields)!;
            country.cca3 = FilterProperty(country.cca3, nameof(country.cca3), fields)!;
            country.ccn3 = FilterProperty(country.ccn3, nameof(country.ccn3), fields)!;
            country.cioc = FilterProperty(country.cioc, nameof(country.cioc), fields)!;
            country.independent = FilterProperty(country.independent, nameof(country.independent), fields)!;
            country.status = FilterProperty(country.status, nameof(country.status), fields)!;
            country.unMember = FilterProperty(country.unMember, nameof(country.unMember), fields)!;
            country.currencies = FilterProperty(country.currencies, nameof(country.currencies), fields)!;
            country.idd = FilterProperty(country.idd, nameof(country.idd), fields)!;
            country.capital = FilterProperty(country.capital, nameof(country.capital), fields)!;
            country.altSpellings = FilterProperty(country.altSpellings, nameof(country.altSpellings), fields)!;
            country.region = FilterProperty(country.region, nameof(country.region), fields)!;
            country.subregion = FilterProperty(country.subregion, nameof(country.subregion), fields)!;
            country.languages = FilterProperty(country.languages, nameof(country.languages), fields)!;
            country.translations = FilterProperty(country.translations, nameof(country.translations), fields)!;
            country.latlng = FilterProperty(country.latlng, nameof(country.latlng), fields)!;
            country.landlocked = FilterProperty(country.landlocked, nameof(country.landlocked), fields)!;
            country.borders = FilterProperty(country.borders, nameof(country.borders), fields)!;
            country.area = FilterProperty(country.area, nameof(country.area), fields)!;
            country.demonyms = FilterProperty(country.demonyms, nameof(country.demonyms), fields)!;
            country.flag = FilterProperty(country.flag, nameof(country.flag), fields)!;
            country.maps = FilterProperty(country.maps, nameof(country.maps), fields)!;
            country.population = FilterProperty(country.population, nameof(country.population), fields)!;
            country.gini = FilterProperty(country.gini, nameof(country.gini), fields)!;
            country.fifa = FilterProperty(country.fifa, nameof(country.fifa), fields)!;
            country.car = FilterProperty(country.car, nameof(country.car), fields)!;
            country.timezones = FilterProperty(country.timezones, nameof(country.timezones), fields)!;
            country.continents = FilterProperty(country.continents, nameof(country.continents), fields)!;
            country.flags = FilterProperty(country.flags, nameof(country.flags), fields)!;
            country.coatOfArms = FilterProperty(country.coatOfArms, nameof(country.coatOfArms), fields)!;
            country.startOfWeek = FilterProperty(country.startOfWeek, nameof(country.startOfWeek), fields)!;
            country.capitalInfo = FilterProperty(country.capitalInfo, nameof(country.capitalInfo), fields)!;
            country.postalCode = FilterProperty(country.postalCode, nameof(country.postalCode), fields)!;
        }

        private T? FilterProperty<T>(T? propValue, string propName, List<string> includedFields)
        {
            return includedFields.Any(f => f.Contains(propName, StringComparison.InvariantCultureIgnoreCase))
                ? propValue
                : default;
        }
    }
}
