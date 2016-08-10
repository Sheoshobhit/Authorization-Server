using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.Core.Models;
using IdentityServer3.Core;

namespace IdentityServer.Demo.Config
{
    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new[]
            {
                new Scope
                {
                    Name="WebApi",
                    Claims=new List<ScopeClaim>()
                    {
                        new ScopeClaim(Constants.ClaimTypes.Email,true),
                        new ScopeClaim(Constants.ClaimTypes.Role,true)
                    }
                },
                new Scope
                {
                    Name="SPAClient",
                    Claims=new List<ScopeClaim>()
                    {
                        new ScopeClaim(Constants.ClaimTypes.Email,true),
                        new ScopeClaim(Constants.ClaimTypes.Role,true)
                    }
                },
                new Scope
                {
                    Name="UserAccountManagement",
                    Claims=new List<ScopeClaim>()
                    {
                        new ScopeClaim(Constants.ClaimTypes.Email,true),
                        new ScopeClaim(Constants.ClaimTypes.Role,true)
                    }
                },
                StandardScopes.OfflineAccess
            };
        }
    }
}