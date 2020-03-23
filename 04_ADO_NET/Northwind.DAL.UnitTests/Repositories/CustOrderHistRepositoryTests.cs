using Northwind.DAL.Exceptions;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Repositories;
using Northwind.DAL.UnitTests.Stubs;
using Xunit;
using Xunit.Priority;

namespace Northwind.DAL.UnitTests.Repositories
{
    /// <summary>
    /// Test class for <see cref="CustOrderHistRepository"/>.
    /// </summary>
    public class CustOrderHistRepositoryTests
    {
        private readonly ICustOrderHistRepository _repository;

        public CustOrderHistRepositoryTests()
        {
            _repository = new CustOrderHistRepository(new SqlDatabaseHandlerStub(), new DataMapperStub());
        }

        [Fact]
        [Priority(12)]
        public void CustOrderHist_ValidCustomerId_ShouldReturnCustOrderHistList()
        {
            // Arrange
            const string customerId = "TOMSP";

            // Act
            var result = _repository.CustOrderHist(customerId);

            // Assert
            Assert.NotEmpty(result);
        }

        [Theory]
        [Priority(13)]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("         ")]
        [InlineData("ASDASDASDASDASD")]
        public void CustOrderHist_InvalidCustomerId_ShouldThrowRepositoryException(string customerId)
        {
            // Act
            Assert.Throws<RepositoryException>(() => _repository.CustOrderHist(customerId));
        }
    }
}