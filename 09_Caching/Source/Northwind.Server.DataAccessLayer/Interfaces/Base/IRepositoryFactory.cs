﻿namespace Northwind.Server.DataAccessLayer.Interfaces.Base
{
    public interface IRepositoryFactory
    {
        TRepository GetRepository<TRepository>() where TRepository : IRepository;
    }
}