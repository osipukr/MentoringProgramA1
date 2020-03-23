using System;
using System.Collections.Generic;
using System.Data;
using Northwind.DAL.Entities;
using Northwind.DAL.Exceptions;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Properties;

namespace Northwind.DAL.Repositories
{
    public class CustOrderHistRepository : Repository<CustOrderHist>, ICustOrderHistRepository
    {
        private const string CUST_ORDER_HIST_DBO_NAME = "[dbo].[CustOrderHist]";

        private const string CUSTOMER_ID_PARAM_NAME = "@customerId";

        public CustOrderHistRepository(IDatabaseHandler databaseHandler, IDataMapper dataMapper)
            : base(databaseHandler, dataMapper)
        {
        }

        public override IEnumerable<CustOrderHist> GetAll()
        {
            throw new NotImplementedException();
        }

        public override CustOrderHist Get(int id)
        {
            throw new NotImplementedException();
        }

        public override void Add(CustOrderHist entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(int id, CustOrderHist entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustOrderHist> CustOrderHist(string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new RepositoryException(Resources.OrderRepository_CustOrderHist_Invalid_customer_id_);
            }

            var commandText = CUST_ORDER_HIST_DBO_NAME;

            var result = ExecuteReaderCollectionInternal(commandText, CommandType.StoredProcedure, new[]
            {
                CreateParameterInternal(CUSTOMER_ID_PARAM_NAME, customerId)
            });

            if (result == null)
            {
                throw new RepositoryException(Resources.OrderRepository_CustOrderHist_The_customer_order_hist_not_found_);
            }

            return result;
        }
    }
}