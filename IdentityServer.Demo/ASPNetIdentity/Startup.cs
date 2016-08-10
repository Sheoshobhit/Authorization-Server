using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Linq;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;
using ASPNetIdentity.Demo;
using IdentityServer3.AccessTokenValidation;
using ASPNetIdentity.App_Start;
using System.Web.Routing;
using System.Web.Optimization;

[assembly: OwinStartup(typeof(ASPNetIdentity.Startup))]

namespace ASPNetIdentity
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            /*What worth noting here is how we are creating a fresh instance from the “ApplicationDbContext” and “ApplicationUserManager” for each request and set it in the Owin context using the extension method “CreatePerOwinContext”. 
            Both objects (ApplicationDbContext and AplicationUserManager) will be available during the entire life of the request*/
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:33317",
                //ValidationMode = ValidationMode.ValidationEndpoint,
                RequiredScopes = new[] { "UserAccountManagement" }
            }
                );
            HttpConfiguration httpConfig = new HttpConfiguration();
            //WebApiConfig.Register(httpConfig);
            ConfigureWebApi(httpConfig);
            //SwaggerConfig.Register(httpConfig);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            //ConfigureWebApi(httpConfig);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            //WebApiConfig.Register(httpConfig);
            app.UseWebApi(httpConfig);

        }

        
        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
