using Capella.RestCountries.Api.V31;

using FluentAssertions;

using Xunit;

namespace Capella.RestCountries.Api.Tests
{
    public class CountriesServiceTests
    {
        [Fact]
        public void Should_Be_Able_To_Load_All_Countries()
        {
            // Arrange
            var service = new CountriesService();

            // Act
            var countries = service.GetCountries();

            // Assert
            countries.Should().NotBeEmpty();
        }
    }
}
