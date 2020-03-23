using System;
using Northwind.DAL.Entities;
using System.Collections.Generic;
using System.Data;
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

        private const string ORDER_ID_PARAM_NAME = "@orderId";

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

            var order = ExecuteReaderInternal(command, parameters: new []
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

            ExecuteNonQueryInternal(commandText, parameters: parameters);
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

            var parameters = propertyMatcher.Select(x => CreateParameterInternal(x.Key, x.Value)).ToList();

            parameters.Add(CreateParameterInternal(ORDER_ID_PARAM_NAME, id));

            ExecuteNonQueryInternal(commandText, parameters: parameters);
        }

        public override void Delete(int id)
        {
            var order = Get(id);

            if (order.Status == OrderStatus.Completed)
            {
                throw new RepositoryException(Resources.OrderRepository_Delete_Orders_with_Completed_status_cannot_be_deleted);
            }

            var commandText = $"DELETE FROM {ORDERS_DBO_NAME} WHERE OrderID={ORDER_ID_PARAM_NAME}";

            ExecuteNonQueryInternal(commandText, parameters: new[]
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

        private static IDictionary<string, object> GetOrdersPropertyMatcher(Order order)
        {
            return new Dictionary<string, object>
            {
                {nameof(order.CustomerID), order.CustomerID},
                {nameof(order.EmployeeID), order.EmployeeID},
                {nameof(order.OrderDate), order.OrderDate.HasValue ? $"{order.OrderDate.Value:yyyy - MM - dd HH: mm:ss}" : null},
                {nameof(order.RequiredDate), order.RequiredDate.HasValue ? $"{order.RequiredDate.Value:yyyy - MM - dd HH: mm:ss}" : null},
                {nameof(order.ShippedDate), order.ShippedDate.HasValue ? $"{order.ShippedDate.Value:yyyy - MM - dd HH: mm:ss}" : null},
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