using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using IdentityServer3.EntityFramework;
using Oauth.IdentityServer.Demo.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oauth.IdentityServer.Demo
{
    public class Factory
    {
        public static IdentityServerServiceFactory Configure(string connString)
        {
            var efConfig = new EntityFrameworkServiceOptions
            {
                ConnectionString = connString,
            };

            // these two calls just pre-populate the test DB from the in-memory config
            ConfigureClients(Clients.Get(), efConfig);
            ConfigureScopes(Scopes.Get(), efConfig);

            var factory = new IdentityServerServiceFactory();

            factory.RegisterConfigurationServices(efConfig);
            factory.RegisterOperationalServices(efConfig);

            factory.UserService = new Registration<IUserService>(resolver => UserServiceFactory.Create());
            factory.CorsPolicyService = new Registration<ICorsPolicyService>(new DefaultCorsPolicyService { AllowAll = true });

            return factory;
        }

        public static void ConfigureClients(IEnumerable<Client> clients, EntityFrameworkServiceOptions options)
        {
            using (var db = new ClientConfigurationDbContext(options.ConnectionString, options.Schema))
            {
                if (db.Clients.Any()) return;
                foreach (var e in clients.Select(c => c.ToEntity()))
                {
                    db.Clients.Add(e);
                }
                db.SaveChanges();
            }
        }

        public static void ConfigureScopes(IEnumerable<Scope> scopes, EntityFrameworkServiceOptions options)
        {
            using (var db = new ScopeConfigurationDbContext(options.ConnectionString, options.Schema))
            {
                if (db.Scopes.Any()) return;
                foreach (var e in scopes.Select(s => s.ToEntity()))
                {
                    db.Scopes.Add(e);
                }
                db.SaveChanges();
            }
        }
    }
}