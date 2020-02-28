using System.IO;
using FileVisitor.Core.Services;
using Xunit;

namespace FileVisitor.Core.Tests.Services
{
    public class FileSystemVisitorOptionsTests
    {
        [Fact]
        public void Constructor_ShouldInitializeByDefault_IfValuesIsNotSet()
        {
            // Act
            var options = new FileSystemVisitorOptions();

            // Assert
            Assert.Null(options.SearchFilter);
            Assert.Equal("*.*", options.SearchPattern);
            Assert.Equal(SearchOption.AllDirectories, options.SearchOption);
        }
    }
}