using System;
using System.Collections.Generic;
using System.Data;

namespace Northwind.DAL.Helpers
{
    public static class SystemTypeToDbTypeHelper
    {
        private static readonly Dictionary<Type, DbType> TypeMapper = new Dictionary<Type, DbType>();

        static SystemTypeToDbTypeHelper()
        {
            Initialize();
        }

        public static DbType GetComponentType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!TypeMapper.ContainsKey(type))
            {
                throw new ArgumentException(string.Empty, nameof(type));
            }

            return TypeMapper[type];
        }

        private static void Initialize()
        {
            TypeMapper.Add(typeof(int), DbType.Int32);
            TypeMapper.Add(typeof(int?), DbType.Int32);
            TypeMapper.Add(typeof(decimal), DbType.Decimal);
            TypeMapper.Add(typeof(decimal?), DbType.Decimal);
            TypeMapper.Add(typeof(string), DbType.String);
            TypeMapper.Add(typeof(DateTime), DbType.DateTime);
            TypeMapper.Add(typeof(DateTime?), DbType.DateTime);
            TypeMapper.Add(typeof(short), DbType.Int16);
            TypeMapper.Add(typeof(float), DbType.Single);
            TypeMapper.Add(typeof(bool), DbType.Boolean);
        }
    }
}