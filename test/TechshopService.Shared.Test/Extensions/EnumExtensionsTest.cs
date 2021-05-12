using AutoFixture.Xunit2;
using FluentAssertions;
using System;
using System.Net;
using TechshopService.Shared.Extensions;
using Xunit;

namespace TechshopService.Shared.Test.Extensions
{
    [Trait("Unit", nameof(EnumExtensions))]
    public class EnumExtensionsTest
    {
        [Theory, AutoData]
        public void ToEnum_GivenDefinedValueInEnum_ThenReturnEnumValue(HttpStatusCode httpStatus)
        {
            // Arrange
            var definedValue = httpStatus.ToString();
            var expectedResult = Enum.Parse<HttpStatusCode>(definedValue);

            // Act
            var result = definedValue.ToEnum<HttpStatusCode>();

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory, AutoData]
        public void ToEnum_GivenInvalidValue_ThenThrowArgumentException(string undefinedValue)
        {
            // Act
            Func<HttpStatusCode> act = () => undefinedValue.ToEnum<HttpStatusCode>();

            // Assert
            act.Should().Throw<ArgumentException>();
        }
    }
}
