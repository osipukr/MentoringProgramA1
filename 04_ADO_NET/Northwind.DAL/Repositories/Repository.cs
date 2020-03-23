using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Northwind.DAL.Extensions;
using Northwind.DAL.Interfaces;

namespace Northwind.DAL.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        protected readonly IDatabaseHandler _databaseHandler;
        protected readonly IDataMapper _dataMapper;

        protected Repository(IDatabaseHandler databaseHandler, IDataMapper dataMapper)
        {
            _databaseHandler = databaseHandler;
            _dataMapper = dataMapper;
        }

        public abstract IEnumerable<TEntity> GetAll();
        public abstract TEntity Get(int id);
        public abstract void Add(TEntity entity);
        public abstract void Update(int id, TEntity entity);
        public abstract void Delete(int id);

        protected void ExecuteNonQueryInternal(string commandText, CommandType commandType = CommandType.Text, IEnumerable<IDbDataParameter> parameters = null)
        {
            using var connection = _databaseHandler.CreateConnection();

            connection.Open();

            var transactionScope = connection.BeginTransaction();

            using var command = _databaseHandler.CreateCommand(commandText, commandType, connection, transactionScope);

            command.AddParameters(parameters);

            try
            {
                command.ExecuteNonQuery();
                transactionScope.Commit();
            }
            catch (Exception)
            {
                transactionScope.Rollback();
            }
            finally
            {
                connection.Close();
            }

            _databaseHandler.CloseConnection(connection);
        }

        protected IEnumerable<TEntity> ExecuteReaderCollectionInternal(string commandText, CommandType commandType = CommandType.Text, IEnumerable<IDbDataParameter> parameters = null)
        {
            using var connection = _databaseHandler.CreateConnection();

            connection.Open();

            using var command = _databaseHandler.CreateCommand(commandText, commandType, connection);

            command.AddParameters(parameters);

            using var reader = command.ExecuteReader();

            var entities = new List<TEntity>();

            while (reader.Read())
            {
                entities.Add(_dataMapper.Map<TEntity>(reader));
            }

            _databaseHandler.CloseConnection(connection);

            return !entities.Any() ? null : entities;
        }

        protected TEntity ExecuteReaderInternal(string commandText, CommandType commandType = CommandType.Text, IEnumerable<IDbDataParameter> parameters = null)
        {
            using var connection = _databaseHandler.CreateConnection();

            connection.Open();

            using var command = _databaseHandler.CreateCommand(commandText, commandType, connection);

            command.AddParameters(parameters);

            using var reader = command.ExecuteReader();

            var entity = default(TEntity);

            if (reader.Read())
            {
                entity = _dataMapper.Map<TEntity>(reader);
            }

            _databaseHandler.CloseConnection(connection);

            return entity;
        }

        protected IDbDataParameter CreateParameterInternal<T>(string name, T value)
        {
            return _databaseHandler.CreateParameter(name,
                EqualityComparer<T>.Default.Equals(value, default)
                    ? (object)DBNull.Value
                    : value);
        }
    }
}