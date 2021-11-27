using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Technobee.Marketing.Counters.DataModels;
using Technobee.Marketing.Counters.DataModels.Services;
using TechnoBee.Marketing.App.CountersApi.Controllers;

namespace TechnoBee.Marketing.App.CountersApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            ServiceCollection services = new ServiceCollection();

            var controllerTypes = Assembly.GetExecutingAssembly().GetExportedTypes()
              .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
              .Where(t => typeof(ApiController).IsAssignableFrom(t)
        || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase));
            foreach (var type in controllerTypes)
            {
                services.AddTransient(type);
            }

            IServiceProvider serviceProvider = new ServiceCollection()
             .AddRecordService()
             .AddTransient<ValuesController>(sp => { return new ValuesController(sp.GetRequiredService<ILineRecordService>()); })
            .BuildServiceProvider();

 config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.DependencyResolver = new DefaultDependencyResolver(serviceProvider);

           

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
