using Microsoft.Extensions.Configuration;
using Moq;
using RentCarCommand.Infra.Logger.Models;
using Serilog;
using TechshopService.Infra.Logger.Logging;

namespace TechshopService.Infra.Logger.Test.Commons.Builders
{
    public class LogWriterMockBuilder
    {
        private readonly Mock<ILogger> _logger;
        private readonly Mock<IConfiguration> _configuration;

        public LogWriterMockBuilder()
        {
            _logger = new Mock<ILogger>(MockBehavior.Strict);
            _configuration = new Mock<IConfiguration>();

            _configuration.SetupAllProperties();
        }

        public LogWriterMockBuilder WithException()
        {
            _logger.Setup(x => x.Error("{@LogMessage}", It.IsAny<LogMessage>()));
            _logger.Setup(x => x.Fatal("{@LogMessage}", It.IsAny<LogMessage>()));

            return this;
        }

        public LogWriterMockBuilder WithAlert()
        {
            _logger.Setup(x => x.Information("{@LogMessage}", It.IsAny<LogMessage>()));
            _logger.Setup(x => x.Warning("{@LogMessage}", It.IsAny<LogMessage>()));

            return this;
        }

        public (LogWriter, Mock<ILogger>) Build() => (
            new(_logger.Object, _configuration.Object),
            _logger);
    }
}
