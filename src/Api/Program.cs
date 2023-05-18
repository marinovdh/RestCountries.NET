using System.Text.Json.Serialization;
using Capella.RestCountries.Api.V31;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecks();
builder.Services.AddScoped<CountriesService>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v3.1", new OpenApiInfo
    {
        Version = "v3.1",
        Title = "RestCountries.NET API",
        Description = "Web API version 3.1 for managing country items, based on previous implementations from restcountries.eu and restcountries.com.",
        Contact = new OpenApiContact
        {
            Name = "Contact",
            Url = new Uri("https://github.com/marinovdh/RestCountries.NET")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v3.1/swagger.json", "v3.1");
        options.RoutePrefix = string.Empty;
        options.InjectJavascript("/swagger/custom.js");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();
