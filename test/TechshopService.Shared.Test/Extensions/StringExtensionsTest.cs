using AutoFixture.Xunit2;
using FluentAssertions;
using TechshopService.Shared.Extensions;
using Xunit;

namespace TechshopService.Shared.Test.Extensions
{
    [Trait("Unit", nameof(StringExtensions))]
    public class StringExtensionsTest
    {
        [Theory, AutoData]
        public void JoinWith_GivenJoinedText_ThenReturnFullJoinedString(string actualStr, string joinedStr, char separator)
        {
            // Arrange
            var expectedText = $"{actualStr}{separator}{joinedStr}";

            // Act
            var result = actualStr.JoinWith(joinedStr, separator);

            // Assert
            result.Should().Be(expectedText);
        }

        [Theory, AutoData]
        public void FormatWith_GivenInputStringAndArgs_ThenStringFormattedWithArgs(string arg0, int arg1)
        {
            // Arrange
            const string inputStr = "{0}{1}";
            var expectedStr = $"{arg0}{arg1}";

            // Act
            var outputStr = inputStr.FormatWith(arg0, arg1);

            // Assert
            outputStr.Should().Be(expectedStr);
        }
    }
}
