using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oauth.IdentityServer.Demo.Config
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client
                {
                    ClientId="WebApi",
                    ClientSecrets=new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName="WebApi",
                    AccessTokenType=AccessTokenType.Jwt,
                    Flow=Flows.ResourceOwner,
                //AllowedScopes=new List<string>
                //{
                //    "WebApi"
                //},
                AllowedScopes  = new List<string> { Constants.StandardScopes.OpenId,"WebApi" },
                Enabled=true
                },
                new Client
                {
                    ClientName = "UserAccountManagement",
                    ClientId = "9A71D96A-9E1F-4C85-A80C-1CA3D8CD3951",
                    Enabled = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    Flow = Flows.ResourceOwner,
                    AllowedScopes = new List<string> { "UserAccountManagement" },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("332FD1CF-9FEE-404C-93FA-66A8D465AA9F".Sha256())
                    }
                }
            };

        }
    }
}