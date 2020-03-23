using System;
using Northwind.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using Northwind.DAL.Exceptions;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Properties;

namespace Northwind.DAL.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private const string ORDERS_DBO_NAME = "[dbo].[Orders]";
        private const string ORDER_DETAILS_DBO_NAME = "[dbo].[Orders Details]";
        private const string PRODUCTS_DBO_NAME = "[dbo].[Products]";
        private const string CUST_ORDER_HIST_DBO_NAME = "[dbo].[CustOrderHist]";
        private const string CUST_ORDERS_DETAIL_DBO_NAME = "[dbo].[CustOrdersDetail]";

        private const string ORDER_ID_PARAM_NAME = "@orderId";
        private const string CUSTOMER_ID_PARAM_NAME = "@customerId";

        public OrderRepository(IDatabaseHandler databaseHandler, IDataMapper dataMapper)
            : base(databaseHandler, dataMapper)
        {
        }

        public override IEnumerable<Order> GetAll()
        {
            var command = $"SELECT * FROM {ORDERS_DBO_NAME}";

            var orders = ExecuteReaderCollectionInternal(command);

            if (orders == null)
            {
                throw new RepositoryException(Resources.OrderRepository_GetAll_The_orders_not_found_);
            }

            return orders;
        }

        public override Order Get(int id)
        {
            if (id < 1)
            {
                throw new RepositoryException(Resources.OrderRepository_Get_Invalid_order_id_);
            }

            var command = $"SELECT * FROM {ORDERS_DBO_NAME} WHERE OrderId={ORDER_ID_PARAM_NAME};"
                          + $"SELECT * FROM {ORDER_DETAILS_DBO_NAME} AS o LEFT JOIN {PRODUCTS_DBO_NAME} AS p ON o.ProductID=p.ProductID WHERE o.OrderId={ORDER_ID_PARAM_NAME}";

            var order = ExecuteReaderInternal(command, new[]
            {
                CreateParameterInternal(ORDER_ID_PARAM_NAME, id)
            });

            if (order == null)
            {
                throw new RepositoryException(string.Format(Resources.OrderRepository_Get_, id));
            }

            return order;
        }

        public override void Add(Order entity)
        {
            if (entity == null)
            {
                throw new RepositoryException(Resources.OrderRepository_Add_Invalid_order_);
            }

            var propertyMatcher = GetOrdersPropertyMatcher(entity);

            var commandText = $"INSERT INTO {ORDERS_DBO_NAME} " + $"VALUES(@{string.Join(", @", propertyMatcher.Keys)})";

            var parameters = propertyMatcher.Select(x => CreateParameterInternal(x.Key, x.Value));

            ExecuteNonQueryInternal(commandText, parameters);
        }

        public override void Update(int id, Order entity)
        {
            if (entity == null)
            {
                throw new RepositoryException(Resources.OrderRepository_Update_Invalid_order_);
            }

            var order = Get(id);

            if (order.Status != OrderStatus.New)
            {
                throw new RepositoryException(Resources.OrderRepository_Update_Only_orders_with_New_status_can_be_changed);
            }

            if (entity.OrderDate != order.OrderDate || entity.ShippedDate != order.ShippedDate)
            {
                throw new RepositoryException(Resources.OrderRepository_Update_Orders_with_statuses_InWork_and_Completed_cannot_be_changed);
            }

            var propertyMatcher = GetOrdersPropertyMatcher(entity);

            var commandText = $"UPDATE {ORDERS_DBO_NAME} " +
                              $"SET {string.Join(",", propertyMatcher.Keys.Select(key => $"{key}=@{key}"))} " +
                              $"WHERE OrderID={ORDER_ID_PARAM_NAME}";

            var parameters = propertyMatcher.Select(x => CreateParameterInternal(x.Key, x.Value));

            ExecuteNonQueryInternal(commandText, parameters);
        }

        public override void Delete(int id)
        {
            var order = Get(id);

            if (order.Status == OrderStatus.Completed)
            {
                throw new RepositoryException(Resources.OrderRepository_Delete_Orders_with_Completed_status_cannot_be_deleted);
            }

            var commandText = $"DELETE FROM {ORDERS_DBO_NAME} WHERE OrderID={ORDER_ID_PARAM_NAME}";

            ExecuteNonQueryInternal(commandText, new[]
            {
                CreateParameterInternal(ORDER_ID_PARAM_NAME, id)
            });
        }

        public void SetInWorkStatus(int id)
        {
            var order = Get(id);

            if (order.Status != OrderStatus.New)
            {
                throw new RepositoryException(Resources.OrderRepository_SetInWorkStatus_Only_in_the_order_with_the_NewStatus_can_the_status_be_changed_to_InWork_);
            }

            order.OrderDate = DateTime.UtcNow;

            Update(id, order);
        }

        public void SetCompletedStatus(int id)
        {
            var order = Get(id);

            if (order.Status != OrderStatus.InWork)
            {
                throw new RepositoryException(Resources.OrderRepository_SetCompletedStatus_Status_can_t_be_changed_to_CompletedStatus_);
            }

            order.ShippedDate = DateTime.UtcNow;

            Update(id, order);
        }

        public IEnumerable<CustOrderHist> CustOrderHist(string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new RepositoryException(Resources.OrderRepository_CustOrderHist_Invalid_customer_id_);
            }

            var commandText = $"EXEC {CUST_ORDER_HIST_DBO_NAME} @CustomerID = '{CUSTOMER_ID_PARAM_NAME}';";

            var result = ExecuteReaderCollectionInternal<CustOrderHist>(commandText, new[]
            {
                CreateParameterInternal(CUSTOMER_ID_PARAM_NAME, customerId)
            });

            if (result == null)
            {
                throw new RepositoryException(Resources.OrderRepository_CustOrderHist_The_customer_order_hist_not_found_);
            }

            return result;
        }

        public IEnumerable<CustOrdersDetail> CustOrderDetail(int orderId)
        {
            if (orderId < 1)
            {
                throw new RepositoryException(Resources.OrderRepository_CustOrderDetail_Invalid_order_id_);
            }

            var commandText = $"EXEC {CUST_ORDERS_DETAIL_DBO_NAME} @OrderId = '{ORDER_ID_PARAM_NAME}';";

            var result = ExecuteReaderCollectionInternal<CustOrdersDetail>(commandText, new[]
            {
                CreateParameterInternal(ORDER_ID_PARAM_NAME, orderId)
            });

            if (result == null)
            {
                throw new RepositoryException(Resources.OrderRepository_CustOrderDetail_The_customer_orders_details_not_found_);
            }

            return result;
        }

        private static IDictionary<string, object> GetOrdersPropertyMatcher(Order order)
        {
            return new Dictionary<string, object>
            {
                {nameof(order.CustomerID), order.CustomerID},
                {nameof(order.EmployeeID), order.EmployeeID},
                {nameof(order.OrderDate), order.OrderDate},
                {nameof(order.RequiredDate), order.RequiredDate},
                {nameof(order.ShippedDate), order.ShippedDate},
                {nameof(order.ShipVia), order.ShipVia},
                {nameof(order.Freight), order.Freight},
                {nameof(order.ShipName), order.ShipName},
                {nameof(order.ShipAddress), order.ShipAddress},
                {nameof(order.ShipCity), order.ShipCity},
                {nameof(order.ShipRegion), order.ShipRegion},
                {nameof(order.ShipPostalCode), order.ShipPostalCode},
                {nameof(order.ShipCountry), order.ShipCountry}
            };
        }
    }
}