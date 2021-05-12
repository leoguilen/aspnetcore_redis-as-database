using AutoFixture;
using FluentAssertions;
using System;
using TechshopService.Shared.Attributes;
using Xunit;

namespace TechshopService.Shared.Test.Attributes
{
    [Trait("Unit", nameof(RequiredGuidAttribute))]
    public class RequiredGuidAttributeTest
    {
        private readonly IFixture _fixture;

        public RequiredGuidAttributeTest() => _fixture = new Fixture();

        [Fact]
        public void IsValid_GivenValueIsNull_ThenThrowArgumentNullException()
        {
            // Arrange
            object nullValue = null;
            var requiredGuidAttribute = new RequiredGuidAttribute();

            // Act
            Func<bool> func = () => requiredGuidAttribute.IsValid(nullValue);

            // Assert
            func.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void IsValid_GivenValueIsEmptyGuid_ThenReturnFalse()
        {
            // Arrange
            var emptyGuid = Guid.Empty;
            var requiredGuidAttribute = new RequiredGuidAttribute();

            // Act
            var validationResult = requiredGuidAttribute.IsValid(emptyGuid);

            // Assert
            validationResult.Should().BeFalse();
        }

        [Fact]
        public void IsValid_GivenValueIsValidGuid_ThenReturnTrue()
        {
            // Arrange
            var validGuid = _fixture.Create<Guid>();
            var requiredGuidAttribute = new RequiredGuidAttribute();

            // Act
            var validationResult = requiredGuidAttribute.IsValid(validGuid);

            // Assert
            validationResult.Should().BeTrue();
        }
    }
}
