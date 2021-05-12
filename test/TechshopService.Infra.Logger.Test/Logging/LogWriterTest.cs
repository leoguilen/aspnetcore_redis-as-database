using AutoFixture.Xunit2;
using Moq;
using RentCarCommand.Infra.Logger.Models;
using System;
using TechshopService.Infra.Logger.Logging;
using TechshopService.Infra.Logger.Test.Commons.Builders;
using Xunit;

namespace RentCarCommand.Infra.Logger.Test.Logging
{
    [Trait("Unit", nameof(LogWriter))]
    public class LogWriterTest
    {
        [Theory, AutoData]
        public void Error_GivenException_ThenLogMessageOnErrorLevel(string message, object data, Exception ex)
        {
            // Arrange
            var (sut, logger) = new LogWriterMockBuilder()
                .WithException()
                .Build();

            // Act
            sut.Error(message, data, ex);

            // Assert
            logger.Verify(x => x.Error("{@LogMessage}", It.IsAny<LogMessage>()), Times.Once);
        }

        [Theory, AutoData]
        public void Fatal_GivenException_ThenLogMessageOnFatalLevel(string message, object data, Exception ex)
        {
            // Arrange 
            var (sut, logger) = new LogWriterMockBuilder()
                .WithException()
                .Build();

            // Act
            sut.Fatal(message, data, ex);

            // Assert
            logger.Verify(x => x.Fatal("{@LogMessage}", It.IsAny<LogMessage>()), Times.Once);
        }

        [Theory, AutoData]
        public void Information_GivenAlert_ThenLogMessageOnInformationLevel(string message, object data)
        {
            // Arrange 
            var (sut, logger) = new LogWriterMockBuilder()
                .WithAlert()
                .Build();

            // Act
            sut.Info(message, data);

            // Assert
            logger.Verify(x => x.Information("{@LogMessage}", It.IsAny<LogMessage>()), Times.Once);
        }

        [Theory, AutoData]
        public void Warning_GivenAlert_ThenLogMessageOnWarningLevel(string message, object data)
        {
            // Arrange 
            var (sut, logger) = new LogWriterMockBuilder()
                .WithAlert()
                .Build();

            // Act
            sut.Warn(message, data);

            // Assert
            logger.Verify(x => x.Warning("{@LogMessage}", It.IsAny<LogMessage>()), Times.Once);
        }
    }
}
