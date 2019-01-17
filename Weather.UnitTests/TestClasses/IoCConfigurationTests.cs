using FluentAssertions;
using MvvmCross.Tests;
using System.Net.Http;
using Weather.Api.Clients;
using Weather.Core;
using Weather.Core.Services;
using Xunit;

namespace Weather.UnitTests.TestClasses
{
    public class IoCConfigurationTests : MvxIoCSupportingTest
    {
        [Fact]
        public void IoC_container_must_register_proper_types_on_initialization()
        {
            // Arrange
            Setup();

            var configuration = new IoCConfiguration();

            // Act
            configuration.Initialize();

            // Assert
            Ioc.CanResolve<HttpClient>().Should().BeTrue($"{nameof(HttpClient)} must be registered in IoC");
            Ioc.CanResolve<IApiClient>().Should().BeTrue($"{nameof(IApiClient)} must be registered in IoC");
            Ioc.CanResolve<IWeatherService>().Should().BeTrue($"{nameof(IWeatherService)} must be registered in IoC");
        }
    }
}
