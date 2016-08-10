using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
namespace ASPNetIdentity.Demo
{
    public class ApplicationUser: IdentityUser
    {
        public ApplicationUser()
        { }

        public ApplicationUser(string userName):base(userName)
        { }
    }
}