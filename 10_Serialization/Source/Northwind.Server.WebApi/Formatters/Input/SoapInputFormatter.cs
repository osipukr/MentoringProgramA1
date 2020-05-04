using System;
using System.Net.Mime;
using System.Runtime.Serialization.Formatters.Soap;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Northwind.Server.WebApi.ViewModels;

namespace Northwind.Server.WebApi.Formatters.Input
{
    public class SoapInputFormatter : InputFormatter
    {
        public SoapInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeNames.Application.Soap);
        }

        protected override bool CanReadType(Type type)
        {
            if (typeof(View).IsAssignableFrom(type))
            {
                return base.CanReadType(type);
            }

            return false;
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;

            var soapFormatter = new SoapFormatter();

            try
            {
                var result = soapFormatter.Deserialize(request.Body);

                return await InputFormatterResult.SuccessAsync(result);
            }
            catch
            {
                return await InputFormatterResult.FailureAsync();
            }
        }
    }
}