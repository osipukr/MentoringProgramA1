using FileVisitor.Core.Interfaces;
using FileVisitor.Core.Services;
using Xunit;

namespace FileVisitor.Core.Tests.Services
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
    }
}