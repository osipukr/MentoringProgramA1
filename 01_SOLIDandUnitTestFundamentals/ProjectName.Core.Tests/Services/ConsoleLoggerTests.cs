using System;
using ProjectName.Core.Interfaces;
using ProjectName.Core.Services;
using Xunit;

namespace ProjectName.Core.Tests.Services
{
    public class ConsoleLoggerTests
    {
        private readonly ILogger _logger;

        public ConsoleLoggerTests()
        {
            _logger = new ConsoleLogger();
        }

        [Fact]
        public void LogMessage_ShouldFinishedSuccess_IfMessageIsValid()
        {
            // Arrange
            var message = string.Empty;

            // Act
            _logger.LogMessage(message);

            // Assert
            Assert.True(true);
        }

        [Fact]
        public void LogMessage_ShouldThrowArgumentNullException_IfMessageIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _logger.LogMessage(null));
        }
    }
}