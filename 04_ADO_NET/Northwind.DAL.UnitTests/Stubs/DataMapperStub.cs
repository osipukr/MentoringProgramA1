using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Northwind.DAL.Interfaces;

namespace Northwind.DAL.UnitTests.Stubs
{
    public class DataMapperStub : IDataMapper
    {
        public TEntity Map<TEntity>(IDataReader reader) where TEntity : IEntity, new()
        {
            var propertyInfo = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var columnNames = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
            var obj = Activator.CreateInstance<TEntity>();

            foreach (var property in propertyInfo)
            {
                if (!columnNames.Any(s => s.Equals(property.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    continue;
                }

                try
                {
                    property.SetValue(obj, reader[property.Name]);
                }
                catch (ArgumentException)
                {
                    property.SetValue(obj, default(TEntity));
                }
            }

            return obj;
        }
    }
}