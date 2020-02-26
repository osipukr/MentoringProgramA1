using System;
using Moq;
using ProjectName.Core.Interfaces;

namespace ProjectName.Core.Tests.Setup
{
    public class MockInitializer
    {
        public static ILogger GetLogger()
        {
            var loggerMock = new Mock<ILogger>();

            loggerMock.Setup(logger => logger.LogMessage(null)).Throws<ArgumentNullException>();
            loggerMock.Setup(logger => logger.LogMessage(It.IsNotNull<string>()));

            return loggerMock.Object;
        }
    }
}