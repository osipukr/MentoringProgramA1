using System.IO;
using FileVisitor.Core.Interfaces;
using FileVisitor.Core.Services;
using Xunit;

namespace FileVisitor.Core.Tests.Services
{
    public class FileSystemVisitorOptionsTests
    {
        private readonly IFileSystemVisitorOptions _options;

        public FileSystemVisitorOptionsTests()
        {
            _options = new FileSystemVisitorOptions();
        }

        [Fact]
        public void Constructor_ShouldInitializeByDefault_IfValuesIsNotSet()
        {
            // Assert
            Assert.Equal(null, _options.SearchFilter);
            Assert.Equal("*.*", _options.SearchPattern);
            Assert.Equal(SearchOption.AllDirectories, _options.SearchOption);
        }
    }
}