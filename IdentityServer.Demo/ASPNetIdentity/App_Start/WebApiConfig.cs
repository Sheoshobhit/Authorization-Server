using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ASPNetIdentity.App_Start
{
    public class WebApiConfig
    {
        public const string DEFAULT_ROUTE_NAME = "DefaultApi";
        public static void Register(HttpConfiguration config)
        {

            //var cors = new EnableCorsAttribute(ConfigurationManager.AppSettings["ToolBoxUrl"], "*", "*")
            //{
            //    SupportsCredentials = true
            //};
            //config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

           // config.Services.Replace(typeof(IExceptionHandler), new UserManagementExceptionHandler());

            config.Routes.MapHttpRoute(
                name: DEFAULT_ROUTE_NAME,
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}