using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using AutoMapper;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Northwind.Server.BusinessLayer.Interfaces;
using Northwind.Server.BusinessLayer.Services;
using Northwind.Server.DataAccessLayer.Contexts;
using Northwind.Server.DataAccessLayer.Interfaces.Base;
using Northwind.Server.DataAccessLayer.Services;
using Northwind.Server.WebApi.Filters;
using Northwind.Server.WebApi.Formatters.Input;
using Northwind.Server.WebApi.Formatters.Output;

namespace Northwind.Server.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOData();

            services
                .AddDbContext<NorthwindContext>(options =>
                {
                    options.UseLazyLoadingProxies();

                    options.UseSqlServer(
                        _configuration.GetConnectionString("SqlServer"),
                        sqlServerOptions =>
                        {
                            sqlServerOptions.EnableRetryOnFailure();
                        });
                })
                .AddAutoMapper(typeof(Startup))
                .AddControllers()
                .AddNewtonsoftJson()
                .AddXmlSerializerFormatters()
                .AddMvcOptions(options =>
                {
                    options.Filters.Add<GlobalExceptionFilter>();
                    options.Filters.Add<ValidatorFilter>();

                    // Workaround: https://github.com/OData/WebApi/issues/1177
                    var odataMediaType = new MediaTypeHeaderValue("application/prs.odatatestxx-odata");

                    foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(x => !x.SupportedMediaTypes.Any()))
                    {
                        outputFormatter.SupportedMediaTypes.Add(odataMediaType);
                    }

                    foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(x => !x.SupportedMediaTypes.Any()))
                    {
                        inputFormatter.SupportedMediaTypes.Add(odataMediaType);
                    }

                    // Input formatters
                    options.InputFormatters.Add(new SoapInputFormatter());

                    // Output formatters
                    options.OutputFormatters.Add(new ExcelOutputFormatter());
                    options.OutputFormatters.Add(new SoapOutputFormatter());

                    options.OutputFormatters.RemoveType<StringOutputFormatter>();
                    options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

            services.AddSwaggerGen(options =>
            {
                // Add the XML comment file for this assembly, so it's contents can be displayed.
                var file = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, file);

                options.IncludeXmlComments(filePath, includeControllerXmlComments: true);

                options.SwaggerDoc(name: "v1", new OpenApiInfo
                {
                    Title = "Northwind API",
                    Description = "The current version of the API",
                    Version = "v1"
                });
            });

            services.AddScoped<IUnitOfWork<NorthwindContext>, NorthwindUnitOfWork>();
            services.AddScoped<IOrderService, OrderService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (_environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = "Northwind - API Endpoints";

                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.Select().Filter().OrderBy().MaxTop(null).Count();
                endpoints.EnableDependencyInjection();
            });
        }
    }
}