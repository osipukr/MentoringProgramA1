using System;
using System.IO;
using ProjectName.Core.Exceptions;
using ProjectName.Core.Interfaces;
using ProjectName.Core.Services;
using ProjectName.Core.Tests.Setup;
using Xunit;

namespace ProjectName.Core.Tests.Services
{
    public class LocalFileManagerTests
    {
        private readonly IFileManager _fileManager;

        private const string RESOURCES_FOLDER_NAME = "Resources";
        private readonly string _validFilePath = Path.Combine(RESOURCES_FOLDER_NAME, "TextResource.txt");
        private readonly string _invalidFilePath = Path.Combine(RESOURCES_FOLDER_NAME, "TextResource-Invalid.txt");

        public LocalFileManagerTests()
        {
            var logger = MockInitializer.GetLogger();

            _fileManager = new LocalFileManager(logger);
        }

        [Fact]
        public void IsExist_ShouldReturnTrue_IfPathIsValid()
        {
            // Arrange
            var path = _validFilePath;
            const bool expected = true;

            // Act
            var actual = _fileManager.IsExist(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsExist_ShouldReturnFalse_IfPathIsInvalid()
        {
            // Arrange
            var path = _invalidFilePath;
            const bool expected = false;

            // Act
            var actual = _fileManager.IsExist(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetContent_ShouldReturnText_IfPathIsValid()
        {
            // Arrange
            var path = _validFilePath;
            const string expected = "text content";

            // Act
            var actual = _fileManager.GetContent(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsExist_ShouldThrowArgumentNullException_IfPathIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _fileManager.IsExist(null));
        }

        [Fact]
        public void GetContent_ShouldThrowArgumentNullException_IfPathIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _fileManager.GetContent(null));
        }

        [Fact]
        public void GetContent_ShouldThrowFileReadException_IfPathIsInvalid()
        {
            // Arrange
            var path = _invalidFilePath;

            // Act
            var exception = Assert.Throws<FileReadException>(() => _fileManager.GetContent(path));

            // Assert
            Assert.Equal(path, exception.FilePath);
        }
    }
}