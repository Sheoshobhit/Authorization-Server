using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using IdentityServer3.Core.Configuration;
using Oauth.IdentityServer.Demo.Config;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;

[assembly: OwinStartup(typeof(Oauth.IdentityServer.Demo.Startup))]

namespace Oauth.IdentityServer.Demo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            var certificate = Convert.FromBase64String(ConfigurationManager.AppSettings["SigningCertificate"]);
            //app.Map("/core", coreApp =>
            //{
                var factory = new IdentityServerServiceFactory()
                .UseInMemoryClients(Clients.Get())
                .UseInMemoryScopes(Scopes.Get())
                .UseInMemoryUsers(Users.Get());

                var options = new IdentityServerOptions()
                {
                    SiteName="Id server ",
                    Factory = Factory.Configure("IdentityServerData"),
                    RequireSsl=false,
                    SigningCertificate= new X509Certificate2(certificate, ConfigurationManager.AppSettings["SigningCertificatePassword"])
                };
                app.UseIdentityServer(options);
            //});
        }

        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\Certificates\idsrv3test.pfx",
                AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }
    }
}
