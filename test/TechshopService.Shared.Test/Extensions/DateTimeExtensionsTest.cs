using AutoFixture.Xunit2;
using FluentAssertions;
using System;
using TechshopService.Shared.Extensions;
using Xunit;

namespace TechshopService.Shared.Test.Extensions
{
    [Trait("Unit", nameof(DateTimeExtensions))]
    public class DateTimeExtensionsTest
    {
        [Theory, AutoData]
        public void ToUtc_GivenLocalDateTime_ThenReturnDateTimeInUniversalFormat(DateTime localDateTime)
        {
            // Arrange
            var expectedDateTime = localDateTime.ToUniversalTime();

            // Act
            var result = localDateTime.ToUtc();

            // Assert
            result.Should().Be(expectedDateTime);
        }
    }
}
