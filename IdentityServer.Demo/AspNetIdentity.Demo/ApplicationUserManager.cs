using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNetIdentity.Demo
{
    /*The User Manager class will be responsible to manage instances of the user class, the class will derive from “UserManager<T>”  where T will represent our “ApplicationUser” class, once it derives from the “ApplicationUser” class a set of methods will be available, those methods will facilitate managing users in our Identity system, some of the exposed methods we’ll use from the “UserManager” during this tutorial are:

METHOD NAME	USAGE
FindByIdAsync(id)	Find user object based on its unique identifier
Users	Returns an enumeration of the users
FindByNameAsync(Username)	Find user based on its Username
CreateAsync(User, Password	Creates a new user with a password
GenerateEmailConfirmationTokenAsync(Id)	Generate email confirmation token which is used in email confimration
SendEmailAsync(Id, Subject, Body)	Send confirmation email to the newly registered user
ConfirmEmailAsync(Id, token)	Confirm the user email based on the received token
ChangePasswordAsync(Id, OldPassword, NewPassword)	Change user password
DeleteAsync(User)	Delete user
IsInRole(Username, Rolename)	Check if a user belongs to certain Role
AddToRoleAsync(Username, RoleName)	Assign user to a specific Role
RemoveFromRoleAsync(Username, RoleName	Remove user from specific Role
Now to implement the “UserManager” class, we will be using the below class for the same:*/
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var appDbContext = context.Get<ApplicationDbContext>();
            var appUserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(appDbContext));

            return appUserManager;
        }
    }
}