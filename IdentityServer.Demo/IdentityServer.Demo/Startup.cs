using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using IdentityServer3.Core.Configuration;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;

[assembly: OwinStartup(typeof(IdentityServer.Demo.Startup))]

namespace IdentityServer.Demo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            var certificate = Convert.FromBase64String(ConfigurationManager.AppSettings["SigningCertificate"]);
            app.Map("/core", coreApp =>
            {
                var options = new IdentityServerOptions
                {
                    SiteName="IdentityServer.Demo",
                    SigningCertificate= new X509Certificate2(certificate, ConfigurationManager.AppSettings["SigningCertificatePassword"]),
                    Factory=Factory.Configure("IdentityServerData")
                };

                coreApp.UseIdentityServer(options);
            });
        }
    }
}
