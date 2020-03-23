using System;
using System.Collections.Generic;
using System.Data;
using Northwind.DAL.Entities;
using Northwind.DAL.Exceptions;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Properties;

namespace Northwind.DAL.Repositories
{
    public class CustOrdersDetailRepository : Repository<CustOrdersDetail>, ICustOrdersDetailRepository
    {
        private const string CUST_ORDERS_DETAIL_DBO_NAME = "[dbo].[CustOrdersDetail]";

        private const string ORDER_ID_PARAM_NAME = "@orderId";

        public CustOrdersDetailRepository(IDatabaseHandler databaseHandler, IDataMapper dataMapper)
            : base(databaseHandler, dataMapper)
        {
        }

        public override IEnumerable<CustOrdersDetail> GetAll()
        {
            throw new NotImplementedException();
        }

        public override CustOrdersDetail Get(int id)
        {
            throw new NotImplementedException();
        }

        public override void Add(CustOrdersDetail entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(int id, CustOrdersDetail entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustOrdersDetail> CustOrderDetail(int orderId)
        {
            if (orderId < 1)
            {
                throw new RepositoryException(Resources.OrderRepository_CustOrderDetail_Invalid_order_id_);
            }

            var commandText = CUST_ORDERS_DETAIL_DBO_NAME;

            var result = ExecuteReaderCollectionInternal(commandText, CommandType.StoredProcedure, new[]
            {
                CreateParameterInternal(ORDER_ID_PARAM_NAME, orderId)
            });

            if (result == null)
            {
                throw new RepositoryException(Resources.OrderRepository_CustOrderDetail_The_customer_orders_details_not_found_);
            }

            return result;
        }
    }
}