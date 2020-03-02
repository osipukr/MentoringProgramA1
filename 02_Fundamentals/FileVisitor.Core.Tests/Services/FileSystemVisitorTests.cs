using System;
using System.IO;
using System.Linq;
using FileVisitor.Core.Interfaces;
using FileVisitor.Core.Models;
using FileVisitor.Core.Services;
using Moq;
using Xunit;

namespace FileVisitor.Core.Tests.Services
{
    public class FileSystemVisitorTests
    {
        private static readonly string DirectoryPath = Path.Combine(Environment.CurrentDirectory, "TestResources");

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_IfOptionsIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new FileSystemVisitor(null));
        }

        [Fact]
        public void Options_ShouldReturnNotNullOptions_IfOptionIsSet()
        {
            // Act
            var options = new FileSystemVisitor(GetMockedOptions()).Options;

            // Assert
            Assert.NotNull(options);
        }

        [Fact]
        public void Visit_ShouldReturnAllItems_IfFilterIsNull()
        {
            // Arrange
            var visitor = new FileSystemVisitor(GetMockedOptions());
            var directoryPath = DirectoryPath;

            // Act
            var items = visitor.Visit(directoryPath).ToArray();

            // Assert
            Assert.Equal(6, items.Length);
        }

        [Fact]
        public void Visit_ShouldReturnItemsByFilter_IfFilterIsNotNull()
        {
            // Arrange
            var visitor = new FileSystemVisitor(GetMockedOptions(filter: file => file.Extension == ".json"));
            var directoryPath = DirectoryPath;

            // Act
            var items = visitor.Visit(directoryPath).ToArray();

            // Assert
            Assert.Equal(1, items.Length);
        }

        [Fact]
        public void Visit_ShouldCallFileFindedEvent_IfFileIsFound()
        {
            // Arrange
            var delegateCallCount = 0;
            var visitor = new FileSystemVisitor(GetMockedOptions());
            var directoryPath = DirectoryPath;

            // Act
            visitor.FileFinded += (s, e) => ++delegateCallCount;

            var items = visitor.Visit(directoryPath).ToArray();

            // Assert
            Assert.Equal(4, delegateCallCount);
            Assert.Equal(6, items.Length);
        }

        [Fact]
        public void Visit_ShouldCallDirectoryFindedEvent_IfDirectoryIsFound()
        {
            // Arrange
            var delegateCallCount = 0;
            var visitor = new FileSystemVisitor(GetMockedOptions());
            var directoryPath = DirectoryPath;

            // Act
            visitor.DirectoryFinded += (s, e) => ++delegateCallCount;

            var items = visitor.Visit(directoryPath).ToArray();

            // Assert
            Assert.Equal(2, delegateCallCount);
            Assert.Equal(6, items.Length);
        }

        [Fact]
        public void Visit_ShouldCallFilteredFileFindedEvent_IfFileIsFoundByFilter()
        {
            // Arrange
            var delegateCallCount = 0;
            var visitor = new FileSystemVisitor(GetMockedOptions(filter: file => file.Extension.Equals(".txt")));
            var directoryPath = DirectoryPath;

            // Act
            visitor.FilteredFileFinded += (s, e) => ++delegateCallCount;

            var items = visitor.Visit(directoryPath).ToArray();

            // Assert
            Assert.Equal(2, delegateCallCount);
            Assert.Equal(2, items.Length);
        }

        [Fact]
        public void Visit_ShouldCallFilteredDirectoryFindedEvent_IfDirectoryIsFoundByFilter()
        {
            // Arrange
            var delegateCallCount = 0;
            var visitor = new FileSystemVisitor(GetMockedOptions(filter: file => file.Name.EndsWith("Folder")));
            var directoryPath = DirectoryPath;

            // Act
            visitor.FilteredDirectoryFinded += (s, e) => ++delegateCallCount;

            var items = visitor.Visit(directoryPath).ToArray();

            // Assert
            Assert.Equal(1, delegateCallCount);
            Assert.Equal(1, items.Length);
        }

        [Fact]
        public void Visit_ShouldContinueSearch_IfActionTypeIsContinueSearch()
        {
            // Arrange
            var delegateCallCount = 0;
            var visitor = new FileSystemVisitor(GetMockedOptions());
            var directoryPath = DirectoryPath;

            // Act
            visitor.DirectoryFinded += (s, e) =>
            {
                ++delegateCallCount;

                e.ActionType = ActionTypeEnum.ContinueSearch;
            };

            var items = visitor.Visit(directoryPath).ToArray();

            // Assert
            Assert.Equal(2, delegateCallCount);
            Assert.Equal(6, items.Length);
        }

        [Fact]
        public void Visit_ShouldSkipElement_IfActionTypeIsSkipElement()
        {
            // Arrange
            var delegateCallCount = 0;
            var visitor = new FileSystemVisitor(GetMockedOptions());
            var directoryPath = DirectoryPath;

            // Act
            visitor.FileFinded += (s, e) =>
            {
                ++delegateCallCount;

                if (e.FindedItem.Extension.Equals(".xml"))
                {
                    e.ActionType = ActionTypeEnum.SkipElement;
                }
            };

            var items = visitor.Visit(directoryPath).ToArray();

            // Assert
            Assert.Equal(4, delegateCallCount);
            Assert.Equal(5, items.Length);
        }

        [Fact]
        public void Visit_ShouldStopSearch_IfActionTypeIsStopSearch()
        {
            // Arrange
            var delegateCallCount = 0;
            var visitor = new FileSystemVisitor(GetMockedOptions());
            var directoryPath = DirectoryPath;

            // Act
            visitor.FileFinded += (s, e) =>
            {
                ++delegateCallCount;

                if (e.FindedItem.Extension.Equals(".xml"))
                {
                    e.ActionType = ActionTypeEnum.StopSearch;
                }
            };

            var items = visitor.Visit(directoryPath).ToArray();

            // Assert
            Assert.Equal(2, delegateCallCount);
            Assert.Equal(3, items.Length);
        }

        private static IFileSystemVisitorOptions GetMockedOptions(
            Func<FileSystemInfo, bool> filter = null,
            string searchPattern = "*.*",
            SearchOption searchOption = SearchOption.AllDirectories)
        {
            var optionsMock = new Mock<IFileSystemVisitorOptions>();

            optionsMock.Setup(x => x.SearchFilter).Returns(filter);
            optionsMock.Setup(x => x.SearchPattern).Returns(searchPattern);
            optionsMock.Setup(x => x.SearchOption).Returns(searchOption);

            return optionsMock.Object;
        }
    }
}