using Northwind.DAL.Exceptions;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Repositories;
using Northwind.DAL.UnitTests.Stubs;
using Xunit;
using Xunit.Priority;

namespace Northwind.DAL.UnitTests.Repositories
{
    /// <summary>
    /// Test class for <see cref="CustOrdersDetailRepository"/>.
    /// </summary>
    public class CustOrdersDetailRepositoryTests
    {
        private readonly ICustOrdersDetailRepository _repository;

        public CustOrdersDetailRepositoryTests()
        {
            _repository = new CustOrdersDetailRepository(new SqlDatabaseHandlerStub(), new DataMapperStub());
        }

        [Fact]
        [Priority(14)]
        public void CustOrderDetail_ValidOrderId_ShouldReturnCustOrderDetailList()
        {
            // Arrange
            const int orderId = 10254;

            // Act
            var result = _repository.CustOrderDetail(orderId);

            // Assert
            Assert.NotEmpty(result);
        }

        [Theory]
        [Priority(15)]
        [InlineData(0)]
        [InlineData(-100)]
        [InlineData(9000)]
        public void CustOrderHist_InvalidOrderId_ShouldThrowRepositoryException(int orderId)
        {
            // Act
            Assert.Throws<RepositoryException>(() => _repository.CustOrderDetail(orderId));
        }
    }
}