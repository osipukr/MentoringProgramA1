using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Northwind.DAL.Interfaces;

namespace Northwind.DAL.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        protected readonly IDbConnection _dbConnection;
        protected readonly IDataMapper _dataMapper;

        protected Repository(IDbConnection dbConnection, IDataMapper dataMapper)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            _dataMapper = dataMapper ?? throw new ArgumentNullException(nameof(dataMapper));
        }

        public abstract IEnumerable<TEntity> GetAll();
        public abstract TEntity Get(int id);
        public abstract void Add(TEntity entity);
        public abstract void Update(int id, TEntity entity);
        public abstract void Delete(int id);

        protected void ExecuteNonQueryInternal(
            string commandText,
            CommandType commandType = CommandType.Text,
            IEnumerable<Tuple<string, object>> parameters = null)
        {
            _dbConnection.Open();

            var transactionScope = _dbConnection.BeginTransaction();

            using var command = CreateCommand(commandText, commandType, parameters, transactionScope);

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
                _dbConnection.Close();
            }
        }

        protected IEnumerable<TEntity> ExecuteReaderCollectionInternal(
            string commandText,
            CommandType commandType = CommandType.Text,
            IEnumerable<Tuple<string, object>> parameters = null)
        {
            return ExecuteReaderCollectionInternal<TEntity>(commandText, commandType, parameters);
        }

        protected IEnumerable<T> ExecuteReaderCollectionInternal<T>(
            string commandText,
            CommandType commandType = CommandType.Text,
            IEnumerable<Tuple<string, object>> parameters = null)
            where T : IEntity
        {
            _dbConnection.Open();

            using var command = CreateCommand(commandText, commandType, parameters);

            using var reader = command.ExecuteReader();

            var entities = new List<T>();

            while (reader.Read())
            {
                entities.Add(_dataMapper.Map<T>(reader));
            }

            _dbConnection.Close();

            return entities.Any() ? entities : null;
        }

        protected TEntity ExecuteReaderInternal(
            string commandText,
            CommandType commandType = CommandType.Text,
            IEnumerable<Tuple<string, object>> parameters = null)
        {
            return ExecuteReaderInternal<TEntity>(commandText, commandType, parameters);
        }

        protected T ExecuteReaderInternal<T>(
            string commandText,
            CommandType commandType = CommandType.Text,
            IEnumerable<Tuple<string, object>> parameters = null)
            where T : IEntity
        {
            _dbConnection.Open();

            using var command = CreateCommand(commandText, commandType, parameters);

            using var reader = command.ExecuteReader();

            var entity = default(T);

            if (reader.Read())
            {
                entity = _dataMapper.Map<T>(reader);
            }

            _dbConnection.Close();

            return entity;
        }

        protected Tuple<string, object> CreateParameterInternal<T>(string name, T value)
        {
            return Tuple.Create(name,
                EqualityComparer<T>.Default.Equals(value, default)
                    ? (object)DBNull.Value
                    : value);
        }

        private IDbCommand CreateCommand(
            string commandText,
            CommandType commandType,
            IEnumerable<Tuple<string, object>> parameters,
            IDbTransaction transactionScope = null)
        {
            var command = _dbConnection.CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Transaction = transactionScope;

            if (parameters != null)
            {
                foreach (var (name, value) in parameters)
                {
                    var parameter = command.CreateParameter();

                    parameter.ParameterName = name;
                    parameter.Value = value;

                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }
    }
}