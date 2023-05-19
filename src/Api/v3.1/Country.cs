using System.Text.Json.Serialization;

namespace Capella.RestCountries.Api.V31;

public class Country
{
    public ExtendedName name { get; set; }
    public string[] tld { get; set; }
    public string cca2 { get; set; }
    public string ccn3 { get; set; }
    public string cca3 { get; set; }
    public string cioc { get; set; }
    public bool? independent { get; set; }
    public string status { get; set; }
    public bool UnMember { get; set; }
    public Dictionary<string, Currency> currencies { get; set; }
    public Idd idd { get; set; }
    public string[] capital { get; set; }
    public string[] altSpellings { get; set; }
    public string region { get; set; }
    public string subregion { get; set; }
    public Dictionary<string, string> languages { get; set; }
    public Dictionary<string, Name> translations { get; set; }
    public decimal[] latlng { get; set; }
    public bool landlocked { get; set; }
    public string[] borders { get; set; }
    public float area { get; set; }
    public Dictionary<string, Demonym> demonyms { get; set; }
    public string flag { get; set; }
    public Maps maps { get; set; }
    public int population { get; set; }
    public object gini { get; set; }
    public string fifa { get; set; }
    public Car car { get; set; }
    public string[] timezones { get; set; }
    public string[] continents { get; set; }
    public Flags flags { get; set; }
    public CoatOfArms coatOfArms { get; set; }
    public string startOfWeek { get; set; }
    public Capitalinfo capitalInfo { get; set; }
    public Postalcode postalCode { get; set; }
}

public class ExtendedName : Name
{
    [JsonPropertyOrder(3)] public Dictionary<string, Name> nativeName { get; set; }
}

public class Name
{
    [JsonPropertyOrder(1)] public string common { get; set; }
    [JsonPropertyOrder(2)] public string official { get; set; }
}

public class Currency
{
    public string name { get; set; }
    public string symbol { get; set; }
}

public class Idd
{
    public string root { get; set; }
    public string[] suffixes { get; set; }
}

public class Demonym
{
    public string f { get; set; }
    public string m { get; set; }
}

public class Maps
{
    public string googleMaps { get; set; }
    public string openStreetMaps { get; set; }
}

public class Gini
{
    public int _2018 { get; set; }
}

public class Car
{
    public string[] signs { get; set; }
    public string side { get; set; }
}

public class Flags
{
    public string png { get; set; }
    public string svg { get; set; }
    public string alt { get; set; }
}

public class CoatOfArms
{
    public string png { get; set; }
    public string svg { get; set; }
}

public class Capitalinfo
{
    public float[] latlng { get; set; }
}

public class Postalcode
{
    public string format { get; set; }
    public string regex { get; set; }
}
