using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.Serialization.Formatters.Soap;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Northwind.Server.WebApi.ViewModels;

namespace Northwind.Server.WebApi.Formatters.Output
{
    public class SoapOutputFormatter : OutputFormatter
    {
        public SoapOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeNames.Application.Soap);
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(View).IsAssignableFrom(type) || typeof(IEnumerable<View>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var response = context.HttpContext.Response;
            var contextObject = context.Object;

            var soapFormatter = new SoapFormatter();

            soapFormatter.Serialize(response.Body, contextObject);

            await response.Body.FlushAsync();
        }
    }
}