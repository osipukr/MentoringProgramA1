﻿using System;
using System.Collections.Generic;
using System.Data;
using Northwind.DAL.Extensions;
using Northwind.DAL.Interfaces;

namespace Northwind.DAL.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity, new()
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

        protected void ExecuteNonQueryInternal(string commandText, IEnumerable<IDbDataParameter> parameters = null)
        {
            using var connection = _databaseHandler.CreateConnection();

            connection.Open();

            var transactionScope = connection.BeginTransaction();

            using var command = _databaseHandler.CreateCommand(commandText, CommandType.Text, connection);

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
        }

        protected IEnumerable<TEntity> ExecuteReaderCollectionInternal(string commandText, IEnumerable<IDbDataParameter> parameters = null)
        {
            return ExecuteReaderCollectionInternal<TEntity>(commandText, parameters);
        }

        protected IEnumerable<T> ExecuteReaderCollectionInternal<T>(string commandText, IEnumerable<IDbDataParameter> parameters = null)
            where T : IEntity, new()
        {
            using var connection = _databaseHandler.CreateConnection();

            connection.Open();

            using var command = _databaseHandler.CreateCommand(commandText, CommandType.Text, connection);

            command.AddParameters(parameters);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return _dataMapper.Map<T>(reader);
            }

            _databaseHandler.CloseConnection(connection);
        }

        protected TEntity ExecuteReaderInternal(string commandText, IEnumerable<IDbDataParameter> parameters = null)
        {
            return ExecuteReaderInternal<TEntity>(commandText, parameters);
        }

        protected T ExecuteReaderInternal<T>(string commandText, IEnumerable<IDbDataParameter> parameters = null)
            where T : IEntity, new()
        {
            using var connection = _databaseHandler.CreateConnection();

            connection.Open();

            using var command = _databaseHandler.CreateCommand(commandText, CommandType.Text, connection);

            command.AddParameters(parameters);

            var reader = command.ExecuteReader();
            var entity = _dataMapper.Map<T>(reader);

            _databaseHandler.CloseConnection(connection);

            return entity;
        }

        protected IDbDataParameter CreateParameterInternal(string name, object value, DbType dbType)
        {
            return _databaseHandler.CreateParameter(name, value, dbType, ParameterDirection.Input);
        }
    }
}