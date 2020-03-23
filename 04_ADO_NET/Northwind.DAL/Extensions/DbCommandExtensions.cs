using System;
using System.Collections.Generic;
using System.Data;

namespace Northwind.DAL.Extensions
{
    public static class DbCommandExtensions
    {
        public static void AddParameters(this IDbCommand command, IEnumerable<IDbDataParameter> parameters)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
        }
    }
}