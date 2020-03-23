using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Northwind.DAL.Interfaces;

namespace Northwind.DAL.Mapping
{
    public class DataMapper : IDataMapper
    {
        public TEntity Map<TEntity>(IDataReader reader) where TEntity : IEntity
        {
            if (reader == null)
            {
                throw new ArgumentNullException();
            }

            var propertyInfo = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
            var columnNames = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToArray();
            var obj = Activator.CreateInstance<TEntity>();

            foreach (var property in propertyInfo)
            {
                if (!columnNames.Any(s => s.Equals(property.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    continue;
                }

                try
                {
                    var value = reader[property.Name];

                    property.SetValue(obj, value, null);
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return obj;
        }
    }
}