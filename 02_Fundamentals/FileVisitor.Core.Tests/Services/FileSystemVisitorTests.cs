using System;
using System.IO;
using FileVisitor.Core.Interfaces;
using FileVisitor.Core.Services;
using Xunit;

namespace FileVisitor.Core.Tests.Services
{
    public class FileSystemVisitorTests
    {
        private readonly IFileSystemVisitor _visitor;

        public FileSystemVisitorTests()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "TestResources");
            var options = new FileSystemVisitorOptions();

            _visitor = new FileSystemVisitor(path, options);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_IfPathIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new FileSystemVisitor(null));
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_IfParametersIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new FileSystemVisitor(null, null));
        }
    }
}