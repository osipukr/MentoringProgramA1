using System;
using System.IO;
using System.Linq;
using FileVisitor.Core.Models;
using FileVisitor.Core.Services;
using Xunit;

namespace FileVisitor.Core.Tests.Services
{
    public class FileSystemVisitorTests
    {
        private const string RESOURCES_DIRECTORY_NAME = "TestResources";

        private static readonly string DirectoryPath = Path.Combine(Environment.CurrentDirectory, RESOURCES_DIRECTORY_NAME);

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

        [Fact]
        public void Options_ShouldReturnCurrentOptions_IfOptionIsSet()
        {
            // Arrange
            var options = new FileSystemVisitorOptions
            {
                SearchFilter = null,
                SearchPattern = "SEARCH_PATTERN",
                SearchOption = SearchOption.TopDirectoryOnly
            };

            // Act
            var visitor = new FileSystemVisitor(DirectoryPath, options);

            // Assert
            Assert.Equal(options, visitor.Options);
        }

        [Fact]
        public void Visit_ShouldReturnAllFiles_IfFilterIsEmpty()
        {
            // Arrange
            var visitor = new FileSystemVisitor(DirectoryPath, new FileSystemVisitorOptions());

            // Act
            var files = visitor.Visit().ToArray();

            // Assert
            Assert.Equal(6, files.Length);
        }

        [Fact]
        public void Visit_ShouldReturnFilesByFilter_IfFilterIsNotEmpty()
        {
            // Arrange
            var visitor = new FileSystemVisitor(DirectoryPath, new FileSystemVisitorOptions
            {
                SearchFilter = file => file.Extension == ".json"
            });

            // Act
            var files = visitor.Visit().ToArray();

            // Assert
            Assert.Equal(1, files.Length);
        }

        [Fact]
        public void Visit_ShouldCallFileFindedEvent_IfFileIsFound()
        {
            // Arrange
            var delegateCallCount = 0;
            var visitor = new FileSystemVisitor(DirectoryPath, new FileSystemVisitorOptions());

            visitor.FileFinded += (s, e) => ++delegateCallCount;

            // Act
            var files = visitor.Visit().ToArray();

            // Assert
            Assert.Equal(4, delegateCallCount);
        }

        [Fact]
        public void Visit_ShouldCallDirectoryFindedEvent_IfDirectoryIsFound()
        {
            // Arrange
            var delegateCallCount = 0;
            var visitor = new FileSystemVisitor(DirectoryPath, new FileSystemVisitorOptions());

            visitor.DirectoryFinded += (s, e) => ++delegateCallCount;

            // Act
            var files = visitor.Visit().ToArray();

            // Assert
            Assert.Equal(2, delegateCallCount);
        }

        [Fact]
        public void Visit_ShouldCallFilteredFileFindedEvent_IfFileIsFoundByFilter()
        {
            // Arrange
            var delegateCallCount = 0;
            var visitor = new FileSystemVisitor(DirectoryPath, new FileSystemVisitorOptions
            {
                SearchFilter = file => file.Extension.Equals(".txt")
            });

            visitor.FilteredFileFinded += (s, e) => ++delegateCallCount;

            // Act
            var files = visitor.Visit().ToArray();

            // Assert
            Assert.Equal(2, delegateCallCount);
        }

        [Fact]
        public void Visit_ShouldCallFilteredDirectoryFindedEvent_IfDirectoryIsFoundByFilter()
        {
            // Arrange
            var delegateCallCount = 0;
            var visitor = new FileSystemVisitor(DirectoryPath, new FileSystemVisitorOptions
            {
                SearchFilter = file => file.Name.EndsWith("Folder")
            });

            visitor.FilteredDirectoryFinded += (s, e) => ++delegateCallCount;

            // Act
            var files = visitor.Visit().ToArray();

            // Assert
            Assert.Equal(1, delegateCallCount);
        }

        [Fact]
        public void Visit_ShouldContinueSearch_IfActionTypeIsContinueSearch()
        {
            // Arrange
            var delegateCallCount = 0;
            var visitor = new FileSystemVisitor(DirectoryPath, new FileSystemVisitorOptions());

            visitor.DirectoryFinded += (s, e) =>
            {
                ++delegateCallCount;

                e.ActionType = ActionTypeEnum.ContinueSearch;
            };

            // Act
            var files = visitor.Visit().ToArray();

            // Assert
            Assert.Equal(2, delegateCallCount);
            Assert.Equal(6, files.Length);
        }

        [Fact]
        public void Visit_ShouldSkipElement_IfActionTypeIsSkipElement()
        {
            // Arrange
            var delegateCallCount = 0;
            var visitor = new FileSystemVisitor(DirectoryPath, new FileSystemVisitorOptions());

            visitor.FileFinded += (s, e) =>
            {
                ++delegateCallCount;

                if (e.FindedItem.Extension.Equals(".xml"))
                {
                    e.ActionType = ActionTypeEnum.SkipElement;
                }
            };

            // Act
            var files = visitor.Visit().ToArray();

            // Assert
            Assert.Equal(4, delegateCallCount);
            Assert.Equal(5, files.Length);
        }

        [Fact]
        public void Visit_ShouldStopSearch_IfActionTypeIsStopSearch()
        {
            // Arrange
            var delegateCallCount = 0;
            var visitor = new FileSystemVisitor(DirectoryPath, new FileSystemVisitorOptions());

            visitor.FileFinded += (s, e) =>
            {
                ++delegateCallCount;

                if (e.FindedItem.Extension.Equals(".xml"))
                {
                    e.ActionType = ActionTypeEnum.StopSearch;
                }
            };

            // Act
            var files = visitor.Visit().ToArray();

            // Assert
            Assert.Equal(2, delegateCallCount);
            Assert.Equal(3, files.Length);
        }
    }
}