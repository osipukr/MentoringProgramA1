using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Northwind.DAL.Interfaces;

namespace Northwind.DAL.UnitTests.Stubs
{
    public class DataMapperStub : IDataMapper
    {
        public TEntity Map<TEntity>(IDataReader reader) where TEntity : IEntity
        {
            var propertyInfo = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
            var columnNames = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToArray();

            var obj = Activator.CreateInstance<TEntity>();

            foreach (var property in propertyInfo)
            {
                if (columnNames.Any(s => s.Equals(property.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    try
                    {
                        property.SetValue(obj, reader[property.Name]);
                    }
                    catch (Exception)
                    {
                        property.SetValue(obj, default);
                    }
                }
            }

            return obj;
        }
    }
}