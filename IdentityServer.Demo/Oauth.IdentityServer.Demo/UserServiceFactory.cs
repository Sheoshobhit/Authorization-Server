using ASPNetIdentity.Demo;
using IdentityServer3.AspNetIdentity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oauth.IdentityServer.Demo
{
    public class UserServiceFactory
    {
        public static AspNetIdentityUserService<ApplicationUser, string> Create()
        {
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new ApplicationUserManager(userStore);
            return new AspNetIdentityUserService<ApplicationUser, string>(userManager);
        }
    }
}