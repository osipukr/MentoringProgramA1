﻿using System;
using System.Linq;
using Northwind.DAL.Entities;
using Northwind.DAL.Exceptions;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Repositories;
using Northwind.DAL.UnitTests.Stubs;
using Xunit;
using Xunit.Priority;

namespace Northwind.DAL.UnitTests.Repositories
{
    /// <summary>
    /// Test class for <see cref="OrderRepository"/>.
    /// </summary>
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class OrderRepositoryTests
    {
        private readonly IOrderRepository _orderRepository;

        public OrderRepositoryTests()
        {
            _orderRepository = new OrderRepository(new SqlDatabaseHandlerStub(), new DataMapperStub());
        }

        [Fact]
        [Priority(1)]
        public void GetAll_EmptyParams_ShouldReturnOrderList()
        {
            // Act
            var orders = _orderRepository.GetAll().ToArray();

            // Assert
            Assert.NotEmpty(orders);
            Assert.All(orders, Assert.NotNull);
        }

        [Fact]
        [Priority(2)]
        public void Get_ExistingId_ShouldReturnOrderDetails()
        {
            // Arrange
            const int expectedId = 10248;

            // Act
            var order = _orderRepository.Get(expectedId);

            // Assert
            Assert.NotNull(order);
            Assert.NotNull(order.OrderDetails);
            Assert.Equal(expectedId, order.OrderId);
            Assert.All(order.OrderDetails, orderDetails => Assert.NotNull(orderDetails.Product));
        }

        [Theory]
        [Priority(3)]
        [InlineData(0)]
        [InlineData(-100)]
        [InlineData(1000000)]
        [InlineData(666666)]
        public void Get_InvalidId_ShouldThrowRepositoryException(int id)
        {
            // Act & Assert
            Assert.Throws<RepositoryException>(() => _orderRepository.Get(id));
        }

        [Fact]
        [Priority(4)]
        public void Add_ValidOrder_ShouldBeInsertedSuccessfully()
        {
            // Arrange
            var orderCountBeforeInsert = _orderRepository.GetAll().Count();
            var order = new Order
            {
                CustomerID = "WARTH",
                EmployeeID = 5,
                Freight = 100,
                ShipAddress = "",
                ShipCity = "Cowes",
                ShipCountry = "UK",
                ShipName = "Island Trading",
                ShipPostalCode = "05033",
                ShipRegion = "OR",
                ShipVia = 3
            };

            // Act
            _orderRepository.Add(order);

            var orderCountAfterInsert = _orderRepository.GetAll().Count();

            // Assert
            Assert.Equal(orderCountBeforeInsert + 1, orderCountAfterInsert);
        }

        [Fact]
        [Priority(5)]
        public void Add_InvalidOrder_ShouldThrowRepositoryException()
        {
            // Act & Assert
            Assert.Throws<RepositoryException>(() => _orderRepository.Add(null));
        }

        [Fact]
        [Priority(6)]
        public void Update_ValidOrder_ShouldBeUpdatedSuccess()
        {
            // Arrange
            const int id = 10257;

            var order = new Order
            {
                CustomerID = "TOMSP"
            };

            // Act
            _orderRepository.Update(id, order);

            var result = _orderRepository.Get(id);

            // Assert
            Assert.Equal(order.CustomerID, result.CustomerID);
        }

        [Fact]
        [Priority(7)]
        public void Update_InvalidOrder_ShouldThrowRepositoryException()
        {
            // Act & Assert
            Assert.Throws<RepositoryException>(() => _orderRepository.Update(1, null));
        }

        [Fact]
        [Priority(8)]
        public void Update_InvalidOrderId_ShouldThrowRepositoryException()
        {
            // Act & Assert
            Assert.Throws<RepositoryException>(() => _orderRepository.Update(-10, new Order()));
        }

        [Fact]
        [Priority(9)]
        public void Update_OrderWithInWorkStatus_ShouldThrowRepositoryException()
        {
            // Act & Assert
            Assert.Throws<RepositoryException>(() => _orderRepository.Update(11077, new Order
            {
                CustomerID = "TOMSP",
                OrderDate = new DateTime(2020, 05, 06, 00, 00, 00),
                RequiredDate = new DateTime(2020, 05, 06, 00, 00, 00)
            }));
        }

        [Fact]
        [Priority(10)]
        public void Delete_ValidOrderId_ShouldBeDeleteOrderById()
        {
            // Arrange
            var id = _orderRepository.GetAll().Last().OrderId;

            var ordersCountBeforeRemove = _orderRepository.GetAll().Count();

            // Act
            _orderRepository.Delete(id);

            var ordersCountAfterRemove = _orderRepository.GetAll().Count();

            // Assert
            Assert.Equal(ordersCountBeforeRemove, ordersCountAfterRemove + 1);
        }

        [Fact]
        [Priority(11)]
        public void Delete_OrderWithStatusIsCompleted_ShouldThrowRepositoryException()
        {
            // Act & Assert
            Assert.Throws<RepositoryException>(() => _orderRepository.Delete(11101));
        }

        //[Fact]
        //[Priority(12)]
        //public void CustOrderHist_ValidCustomerId_ShouldReturnCustOrderHistList()
        //{
        //    // Arrange
        //    const string customerId = "39";

        //    // Act
        //    var result = _orderRepository.CustOrderHist(customerId);

        //    // Assert
        //    Assert.NotEmpty(result);
        //}

        //[Theory]
        //[Priority(13)]
        //[InlineData(null)]
        //[InlineData("")]
        //[InlineData("         ")]
        //[InlineData("ASDASDASDASDASD")]
        //public void CustOrderHist_InvalidCustomerId_ShouldThrowRepositoryException(string customerId)
        //{
        //    // Act
        //    Assert.Throws<RepositoryException>(() => _orderRepository.CustOrderHist(customerId));
        //}

        //[Fact]
        //[Priority(14)]
        //public void CustOrderDetail_ValidOrderId_ShouldReturnCustOrderDetailList()
        //{
        //    // Arrange
        //    const int orderId = 10254;

        //    // Act
        //    var result = _orderRepository.CustOrderDetail(orderId);

        //    // Assert
        //    Assert.NotEmpty(result);
        //}

        //[Theory]
        //[Priority(15)]
        //[InlineData(0)]
        //[InlineData(-100)]
        //[InlineData(10000000)]
        //public void CustOrderHist_InvalidOrderId_ShouldThrowRepositoryException(int orderId)
        //{
        //    // Act
        //    Assert.Throws<RepositoryException>(() => _orderRepository.CustOrderDetail(orderId));
        //}
    }
}