using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPPlusEnumerable;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Northwind.Server.DataAccessLayer.Entities.Base;
using Northwind.Server.WebApi.ViewModels;

namespace Northwind.Server.WebApi.Formatters.Output
{
    public class ExcelOutputFormatter : OutputFormatter
    {
        private const string CONTENT_TYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public ExcelOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(CONTENT_TYPE));
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(Entity).IsAssignableFrom(type) ||
                typeof(View).IsAssignableFrom(type) ||
                typeof(IEnumerable).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var response = context.HttpContext.Response;
            var contextObject = context.Object;

            var bytes = Spreadsheet.Create(contextObject as IEnumerable<object> ?? new[] { contextObject });

            await response.Body.WriteAsync(bytes);

            response.Body.Position = 0;
        }
    }
}